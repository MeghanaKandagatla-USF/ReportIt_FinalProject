using System.ComponentModel.DataAnnotations;

namespace ReportIt.Models
{
    public class CrimeEvent
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Crime Date")]
        public DateTime CrimeDate { get; set; }
        [Display(Name = "Area")]
        public required string LocationArea { get; set; }
        [Display(Name = "Town")]
        public required string LocationTown { get; set; }
        [Display(Name = "Victim Name")]
        public required string VictimName { get; set; }
        [Display(Name = "Crime Type")]
        public required CrimeTypeEnum CrimeType { get; set; }
        public string? Notes { get; set; }
        [Display(Name = "Latitude")]
        public double latitude { get; set; }
        [Display(Name = "Longitude")]
        public double longitude { get; set; }

        public CrimeEvent() { }
    }
    public class CrimeEventApi
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Crime Date")]
        public DateTime date { get; set; }
        [Display(Name = "Area")]
        public required string block { get; set; }
        [Display(Name = "Crime Type")]
        public required string primary_type { get; set; }
        public string? description { get; set; }
        [Display(Name = "Latitude")]
        public double latitude { get; set; }
        [Display(Name = "Longitude")]
        public double longitude { get; set; }
    }
}
