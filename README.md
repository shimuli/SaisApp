# SAIS App - Applicant Registration System

SAIS (Social Assistance Information System) App is a .NET MAUI cross-platform application used to register and manage applicants for social programs. It integrates with a backend API to store and retrieve applicant data, cascading location lookups, and program information.

---

## ✨ Features

- 📋 Register new applicants with full personal, address, and program details
- 🌍 Cascading dropdowns for County, SubCounty, Location, SubLocation, and Village
- ✅ Form validation for all required fields
- 🧭 Page-based navigation (without Shell)
- 🔄 Integration with RESTful APIs
- 📦 Dependency Injection for services
- 🔎 Lookup data fetching (e.g., Gender, Marital Status, Locations, Programs)

---

## 📱 Technologies Used

- [.NET MAUI](https://learn.microsoft.com/dotnet/maui/)
- MVVM Architecture (`ObservableObject`, Commands)
- REST API integration via `HttpClient`
- XAML UI
- C# backend logic
- DI using `Microsoft.Extensions.DependencyInjection`

---

## 🛠 Setup Instructions

1. **Clone the Repository**
   ```bash
   git clone https://github.com/your-org/sais-app.git
   cd sais-app
