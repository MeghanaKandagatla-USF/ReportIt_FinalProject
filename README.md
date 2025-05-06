# 🚨 ReportIt

**ReportIt** is a web-based crime reporting and analysis system. It allows users to log crime incidents, view trends through visualizations, and track system changes via audit logs. The application also integrates with the Chicago Open Data API to enrich its crime dataset.

---

## 🎯 Features

- 📝 CRUD operations for crime event records
- 📊 Data visualizations for crime trends and audit activities
- 🧾 Audit logs for all create, update, and delete actions
- 🔍 Search functionality by area, town, or victim name
- 🌐 API integration with Chicago Crime Data Portal
- 🧭 MVC pattern with proper repository abstraction

---

## 🧱 Tech Stack

- ASP.NET Core MVC (C#)
- Entity Framework Core
- Razor Views
- SQL Server (.mdf test database)
- Chart.js (or any front-end chart library)
- Chicago Open Data API

---

## 📂 Folder Structure
---
ReportIt/
├── Controllers/                      # MVC controllers for features
│   ├── CrimeEventsController.cs      # Handles crime data CRUD and API integration
│   ├── AuditLogsController.cs        # Handles audit log display and visualization
│   └── HomeController.cs             # Static routes (Home, About, Privacy)
│
├── Models/                           # Application data models
│   ├── CrimeEvent.cs
│   ├── AuditLog.cs
│   └── ErrorViewModel.cs
│
├── Data/                             # Database context and repositories
│   ├── ReportItContext.cs            # EF Core DbContext
│   ├── CrimeEventRepository.cs
│   ├── AuditLogRepository.cs
│   └── Interfaces/
│       ├── ICrimeEventRepository.cs
│       └── IAuditLogRepository.cs
│
├── Views/                            # Razor views
│   ├── CrimeEvents/                  # Index, Create, Edit, Delete, Details, Visualization
│   ├── AuditLogs/                    # Index, Details, Visualization
│   ├── Home/                         # Index.cshtml, About.cshtml, Privacy.cshtml
│   └── Shared/                       # Layout, error pages, partials
│
├── wwwroot/                          # Static files (JS, CSS, images)
│
├── appsettings.json                 # Database and app config
├── Program.cs                       # App startup
├── ReportIt.csproj                  # Project file
└── README.md                        # Project documentation
---

## ⚙️ Setup Instructions

1. Clone the repository:
   ```
   git clone https://github.com/MeghanaKandagatla-USF/ReportIt_FinalProject.git
   ```

2. Open `index.html` in your browser to explore the platform.

## 📈 Logical Data Design

Our ER diagram models the relationship between `CrimeEvent` and `AuditLog`. Each crime event may have multiple related audit entries that track changes made to it. 

You can view the ER diagram in the **About** section of the application.
![image](https://github.com/user-attachments/assets/956916f0-c9a0-461a-8ac8-4631705819fb)


## 👨‍💻 Team Members

- Ravi teja Modupalli 
- Venkata Sai Kolla
- Srikar Kamatham
- Meghana Kandagatla 



