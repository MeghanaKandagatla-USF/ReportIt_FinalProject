using ReportIt.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ReportIt.Data
{
    public class CrimeEventRepository : ICrimeEventRepository
    {
        private readonly ReportItContext _context;

        public CrimeEventRepository(ReportItContext context)
        {
            _context = context;
        }

        // return all crime events, including fetching from the API even if the database is not empty
        List<CrimeEvent> ICrimeEventRepository.GetAll()
        {
            CrimeEventRepository crimeEventRepository = new CrimeEventRepository(_context);
            
            
            var crimeEvents = _context.CrimeEvent.ToList();

            return crimeEvents;
        }

        // get an individual crime event using the id and return details from the API
        CrimeEvent? ICrimeEventRepository.GetById(int id)
        {
            var crimeEvent = _context.CrimeEvent
                .FirstOrDefault(m => m.Id == id);
            return crimeEvent;
        }


        // create a crime event and log the action in the audit log table
        CrimeEvent ICrimeEventRepository.Create(CrimeEvent crimeEvent)
        {
            _context.Add(crimeEvent);
            _context.SaveChanges();
            AuditLog auditLog = new AuditLog { ActionType = "Crime event with ID: " + crimeEvent.Id + " created", ActionDateTime = DateTime.Now, CrimeEventID = crimeEvent.Id };
            _context.Add(auditLog);
            _context.SaveChanges();
            return crimeEvent;
        }

        // update a crime event and log the action in the audit log table
        CrimeEvent ICrimeEventRepository.Update(CrimeEvent crimeEvent)
        {
            _context.Update(crimeEvent);
            _context.SaveChanges();
            AuditLog auditLog = new AuditLog { ActionType = "Crime event with ID: " + crimeEvent.Id + " updated", ActionDateTime = DateTime.Now, CrimeEventID = crimeEvent.Id };
            _context.Add(auditLog);
            _context.SaveChanges();
            return crimeEvent;
        }

        // delete a crime event and log the action in the audit log table
        CrimeEvent? ICrimeEventRepository.DeleteById(int id)
        {
            var crimeEvent = _context.CrimeEvent
                .FirstOrDefault(m => m.Id == id);
            if (crimeEvent == null)
            {
                return null;
            }
            AuditLog auditLog = new AuditLog { ActionType = "Crime event with ID: " + crimeEvent.Id + " deleted", ActionDateTime = DateTime.Now };
            _context.Add(auditLog);
            // before we delete the crime event we must remove the explicit id reference from all previous audit log entries
            // (the ID will remain recorded in the audit log's 'ActionType' string)
            _context.AuditLog
                .Where(existingAuditLog => existingAuditLog.CrimeEventID == crimeEvent.Id)
                .ForEachAsync(existingAuditLog => existingAuditLog.CrimeEventID = null).Wait();
            _context.CrimeEvent.Remove(crimeEvent);
            _context.SaveChanges();
            return crimeEvent;
        }

        // Search the crime events by location area, town or victim name.
        // note:
        // Abstracting the database operations into repositories enables unit testing
        // of the controllers with a mock repository.
        // However with this search logic now in the repository, it can't be unit tested.
        // As an improvement I would suggest moving the search logic into the controller, but 
        // thought must be given to performance to ensure the database is used for querying, rather than
        // filtering the results in-memory. One alternative is to add integration tests that test the search
        // logic end to end.
        List<CrimeEvent> ICrimeEventRepository.Search(string queryString)
        {
            if(_context.CrimeEvent.Count() <= 6)
            {
                // Fetch additional results from the API
                string BASE_URL = $"https://data.cityofchicago.org/resource/t7ek-mgzi.json";
                string apiPath = $"{BASE_URL}?$limit=50"; // Grab more at once  
                var request = new HttpRequestMessage(HttpMethod.Get, apiPath);
                request.Headers.Add("X-App-Token", "AtQP41DGwCjdG9OiLkRDchH0y");

                using var client = new HttpClient();
                HttpResponseMessage response = client.Send(request);

                if (response.IsSuccessStatusCode)
                {
                    string crimeData = response.Content.ReadAsStringAsync().Result;
                    var apiCrimeEvents = JsonConvert.DeserializeObject<List<CrimeEventApi>>(crimeData);

                    if (apiCrimeEvents != null && apiCrimeEvents.Any())
                    {
                        var fetchedCrimeEvents = apiCrimeEvents.Select(apiCrime => new CrimeEvent
                        {
                            //Id = apiCrime.Id,
                            CrimeDate = apiCrime.date,
                            LocationArea = apiCrime.block,
                            LocationTown = "Unknown", // Default value as Town is not available in API data
                            VictimName = "Unknown", // Default value as Victim Name is not available in API data
                            CrimeType = Enum.TryParse<CrimeTypeEnum>(apiCrime.primary_type, true, out var crimeType) ? crimeType : CrimeTypeEnum.Other,
                            Notes = apiCrime.description + " " + apiCrime.primary_type,
                            latitude = apiCrime.latitude,
                            longitude = apiCrime.longitude
                        }).ToList();

                        // Merge API results with database results, avoiding duplicates
                        foreach (var fetchedCrimeEvent in fetchedCrimeEvents)
                        {
                            _context.CrimeEvent.Add(fetchedCrimeEvent); // Add to database
                        }
                        // Save changes to the database
                        _context.SaveChanges();
                    }
                }
            }
            
            var crimes = from crime in _context.CrimeEvent
                         select crime;

            if (!String.IsNullOrEmpty(queryString))
            {
                crimes = crimes.Where(crime =>
                    crime.LocationArea.Contains(queryString) ||
                    crime.LocationTown.Contains(queryString) ||
                    crime.VictimName.Contains(queryString)
                );
            }

            var crimesToReturn = crimes.ToList();
            return crimesToReturn;
        }
    }
}
