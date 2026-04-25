# Dokan Project - Creation & Setup Guide

## Project Overview
**Dokan** is an ASP.NET Core Razor Pages e-commerce application built with .NET 10, featuring product management, database integration, and web scaffolding.

---

## Phase 1: Solution & Project Structure Setup

### Step 1: Create Solution
1. Open Visual Studio Community 2026
2. Create new **Solution** named `Dokan`
3. Location: `D:\Asp.net core\Mega Project\Dokan Project\Dokan\`

### Step 2: Create Projects
Create 4 class library/web projects with the following structure:

#### 2.1 Dokan.Models (Class Library)
- **Purpose**: Contains data models and view models
- **.NET Target**: net10.0
- **Key Dependencies**:
  - `Microsoft.AspNetCore.Mvc v2.3.9`

#### 2.2 Dokan.DataAccess (Class Library)
- **Purpose**: Database context and data access logic
- **.NET Target**: net10.0
- **Key Dependencies**:
  - `Microsoft.EntityFrameworkCore v10.0.6`
  - `Microsoft.EntityFrameworkCore.SqlServer v10.0.6`
- **Project References**: Dokan.Models

#### 2.3 Dokan.Utilities (Class Library)
- **Purpose**: Helper functions and utilities
- **.NET Target**: net10.0
- **No external dependencies**

#### 2.4 Dokan.Web (ASP.NET Core Razor Pages Web App)
- **Purpose**: Web UI layer with Razor Pages
- **.NET Target**: net10.0
- **Key Dependencies**:
  - `Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation v10.0.6`
  - `Microsoft.EntityFrameworkCore.SqlServer v10.0.6`
  - `Microsoft.EntityFrameworkCore.Tools v10.0.6`
  - `Microsoft.VisualStudio.Web.CodeGeneration.Design v10.0.2`
  - `NuGet.Packaging v7.3.1`
  - `NuGet.Protocol v7.3.1`
- **Project References**: Dokan.DataAccess, Dokan.Models

---

## Phase 2: Models & Data Structures

### Step 3: Create Domain Models
Located in `Dokan.Models/Models/`

**Create Product.cs model** with:
```csharp
- Id (int) - Primary Key
- Title (string)
- Description (string)
- Price (decimal)
- CategoryId (int) - Foreign Key
- Category (navigation property)
- ImageUrl (string)
- [ValidateNever] attributes for excluded validation fields
```

### Step 4: Create View Models
Located in `Dokan.Models/ViewModels/`

**Create ProductVM.cs** with:
```csharp
- Product model properties
- CategoryList (IEnumerable<SelectListItem>) - for dropdown binding
- Relationship mapping to database model
```

---

## Phase 3: Database Configuration

### Step 5: Setup Entity Framework Core
In `Dokan.DataAccess/`:

**Create ApplicationDbContext.cs**:
1. Inherit from `DbContext`
2. Configure SQL Server connection
3. Create `DbSet<Product>` property
4. Override `OnModelCreating()` for relationships

### Step 6: Configure Connection String
In `Dokan.Web/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=DokanDb;Trusted_Connection=true;Encrypt=false;"
  }
}
```

### Step 7: Create Initial Migration
1. Set `Dokan.DataAccess` as default project in Package Manager Console
2. Run: `Add-Migration InitialCreate`
3. Run: `Update-Database`

---

## Phase 4: Project Configuration & Startup

### Step 8: Configure Dependency Injection
In `Dokan.Web/Program.cs`:
```csharp
// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Razor Pages
builder.Services.AddRazorPages();
```

### Step 9: Configure Middleware Pipeline
In `Dokan.Web/Program.cs`:
```csharp
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});
```

### Step 10: Enable Razor Runtime Compilation
In `Dokan.Web/Program.cs`:
```csharp
builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();
```

---

## Phase 5: Web Scaffolding & Pages

### Step 11: Generate Razor Pages with Scaffolding
1. Right-click `Dokan.Web` → **Add** → **New Scaffolded Item**
2. Select **Razor Pages**
3. Choose Model: `Product`
4. Select `ApplicationDbContext`
5. Generate: Create, Edit, Delete, Details, Index pages
6. Output location: `Pages/Products/`

### Step 12: Configure Static Assets
Create folders in `Dokan.Web/wwwroot/`:
- `Images/Products/` - for product images
- `plugins/jquery/` - for jQuery library

---

## Phase 6: Build & Validation

### Step 13: Build Solution
1. Press `Ctrl + Shift + B` or **Build** → **Build Solution**
2. Verify no compilation errors
3. All 4 projects should build successfully

### Step 14: Run Application
1. Set `Dokan.Web` as startup project
2. Press `F5` or **Debug** → **Start Debugging**
3. Verify Razor Pages load correctly
4. Test CRUD operations on scaffolded pages

---

## Phase 7: Security & Dependencies

### Step 15: NuGet Vulnerability Fixes
**Issue**: `Microsoft.AspNetCore.Mvc.ViewFeatures v2.3.9` was outdated

**Resolution**:
- Updated to compatible version: `Microsoft.AspNetCore.Mvc v2.3.9`
- Ensured all EntityFramework packages aligned to v10.0.6
- Verified no security vulnerabilities remain

---

## Project Structure Summary

```
Dokan/
├── Dokan.Models/
│   ├── Models/
│   │   └── Product.cs
│   ├── ViewModels/
│   │   └── ProductVM.cs
│   └── Dokan.Models.csproj
├── Dokan.DataAccess/
│   ├── Data/
│   │   └── ApplicationDbContext.cs
│   ├── Migrations/
│   └── Dokan.DataAccess.csproj
├── Dokan.Utilities/
│   └── Dokan.Utilities.csproj
├── Dokan.Web/
│   ├── Pages/
│   │   ├── Products/
│   │   │   ├── Index.cshtml
│   │   │   ├── Create.cshtml
│   │   │   ├── Edit.cshtml
│   │   │   ├── Delete.cshtml
│   │   │   └── Details.cshtml
│   │   └── (other pages)
│   ├── wwwroot/
│   │   ├── Images/Products/
│   │   └── plugins/jquery/
│   ├── Program.cs
│   ├── appsettings.json
│   └── Dokan.Web.csproj
└── Dokan.sln
```

---

## Key Technologies & Versions

| Component | Version |
|-----------|---------|
| .NET | 10.0 |
| ASP.NET Core | 10.0.6 |
| Entity Framework Core | 10.0.6 |
| SQL Server | Latest via EF Core |
| Visual Studio | 2026 (18.4.3) |
| Runtime Compilation | Enabled |

---

## Troubleshooting

### Connection String Issues
- Verify SQL Server is running
- Check server name and authentication mode (Windows/SQL Server)
- Use `Encrypt=false` for local development if needed

### Migration Issues
- Ensure `Dokan.DataAccess` is default project in PMC
- Delete `Migrations/` folder if conflicts occur
- Re-run: `Add-Migration InitialCreate` → `Update-Database`

### NuGet Restore Failures
- Clear NuGet cache: `nuget locals all -clear`
- Check internet connection
- Verify all package versions are compatible with net10.0

---

## Next Steps

1. **Add Business Logic**: Create service layer in `Dokan.Utilities`
2. **Enhance UI**: Customize Razor Pages layout and styling
3. **Add Validation**: Implement data annotations and server-side validation
4. **Authentication**: Integrate Identity for user management
5. **Deployment**: Prepare for production environment

---

## Notes

- All scaffolding has been applied
- Middleware pipeline configured for Razor Pages routing
- Database migrations applied to SQL Server
- NuGet packages updated to secure versions
- Static asset folders pre-created

**Created**: During initial project setup
**Last Updated**: After NuGet security remediation
