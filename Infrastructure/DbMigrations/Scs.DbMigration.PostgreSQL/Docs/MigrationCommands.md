# ⚙️ EF Core Migration Commands (PostgreSQL)

This document provides a complete reference for managing EF Core migrations in the **Scs** project. The commands are configured for the scenario where **`Infrastructure/DbMigrations/Scs.DbMigration.PostgreSQL`** serves as both the migration source and the startup project.

---

## 📌 Project Configuration

| Setting | Value |
| :--- | :--- |
| **Migration Project Path** | `Infrastructure/DbMigrations/Scs.DbMigration.PostgreSQL` |
| **Startup Project Path** | `Infrastructure/DbMigrations/Scs.DbMigration.PostgreSQL` |
| **DB Context Name** | `PostgresScsDbContext` |

---

## ▶️ Visual Studio (Package Manager Console) Commands

1. Open Visual Studio.
2. Go to **Tools** > **NuGet Package Manager** > **Package Manager Console**.
3. Set the **Default Project** dropdown to: **`Scs.DbMigration.PostgreSQL`**.

### ➕ Add New Migration (PMC)

```powershell
Add-Migration [MigrationName] -Project Infrastructure\DbMigrations\Scs.DbMigration.PostgreSQL -StartupProject Infrastructure\DbMigrations\Scs.DbMigration.PostgreSQL -Context PostgresScsDbContext
```

### ⬆️ Update Database (PMC)
```
Update-Database -Project Infrastructure\DbMigrations\Scs.DbMigration.PostgreSQL -StartupProject Infrastructure\DbMigrations\Scs.DbMigration.PostgreSQL -Context PostgresScsDbContext
```
### ➖ Remove Last Migration (PMC)
```
Remove-Migration -Project Infrastructure\DbMigrations\Scs.DbMigration.PostgreSQL -StartupProject Infrastructure\DbMigrations\Scs.DbMigration.PostgreSQL -Context PostgresScsDbContext
```

#### 📝 Generate SQL Script for Migration (PMC)
```
Script-Migration -Idempotent -Project Infrastructure\DbMigrations\Scs.DbMigration.PostgreSQL -StartupProject Infrastructure\DbMigrations\Scs.DbMigration.PostgreSQL -Context PostgresScsDbContext -Output "Infrastructure/DbMigrations/Scs.DbMigration.PostgreSQL/Scripts/Migration.sql"
```

### 🔍 List All Migrations (PMC)
```
Get-Migration -Project Infrastructure\DbMigrations\Scs.DbMigration.PostgreSQL -StartupProject Infrastructure\DbMigrations\Scs.DbMigration.PostgreSQL -Context PostgresScsDbContext
```
```
💻 Terminal (.NET CLI) Commands
These commands work in PowerShell, CMD, Bash, or Zsh. Ensure you are in the solution root directory.

Prerequisite (run once)
Bash
dotnet tool install --global dotnet-ef
```

➕ Add New Migration (CLI)
Bash
```
dotnet ef migrations add [MigrationName] --project Infrastructure/DbMigrations/Scs.DbMigration.PostgreSQL --startup-project Infrastructure/DbMigrations/Scs.DbMigration.PostgreSQL --context PostgresScsDbContext
```
⬆️ Update Database (CLI)
Bash
```
dotnet ef database update --project Infrastructure/DbMigrations/Scs.DbMigration.PostgreSQL --startup-project Infrastructure/DbMigrations/Scs.DbMigration.PostgreSQL --context PostgresScsDbContext
```
❌ Remove Last Migration (CLI)
Bash
```
dotnet ef migrations remove --project Infrastructure/DbMigrations/Scs.DbMigration.PostgreSQL --startup-project Infrastructure/DbMigrations/Scs.DbMigration.PostgreSQL --context PostgresScsDbContext
```
📝 Script Migration (Idempotent) (CLI)
Scripts are typically used for deployment pipelines.

Bash
```
dotnet ef migrations script --idempotent --project Infrastructure/DbMigrations/Scs.DbMigration.PostgreSQL --startup-project Infrastructure/DbMigrations/Scs.DbMigration.PostgreSQL --context PostgresScsDbContext --output Infrastructure/DbMigrations/Scs.DbMigration.PostgreSQL/Scripts/Migration.sql
```
📜 List Migrations (CLI)
Bash
```
dotnet ef migrations list --project Infrastructure/DbMigrations/Scs.DbMigration.PostgreSQL --startup-project Infrastructure/DbMigrations/Scs.DbMigration.PostgreSQL --context PostgresScsDbContext
```
💣 Drop Database (CLI)
⚠️ WARNING: This command will permanently delete your database. Use with extreme caution, especially outside of development environments.

Bash
```
dotnet ef database drop --force --project Infrastructure/DbMigrations/Scs.DbMigration.PostgreSQL --startup-project Infrastructure/DbMigrations/Scs.DbMigration.PostgreSQL --context PostgresScsDbContext
```
📎 References
EF Core CLI Documentation

EF Core Migrations Documentation