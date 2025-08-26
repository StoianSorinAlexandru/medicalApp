using DevExpress.ExpressApp.EFCore.Updating;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.ExpressApp.Design;
using DevExpress.ExpressApp.EFCore.DesignTime;
using System.Text.Json;
using System.IO;
using System.ComponentModel.DataAnnotations;
using MedicalApplication.Module.DatabaseUpdate;

namespace MedicalApplication.Module.BusinessObjects;

// This code allows our Model Editor to get relevant EF Core metadata at design time.
// For details, please refer to https://supportcenter.devexpress.com/ticket/details/t933891/core-prerequisites-for-design-time-model-editor-with-entity-framework-core-data-model.
public class MedicalApplicationContextInitializer : DbContextTypesInfoInitializerBase {
    protected override DbContext CreateDbContext() {
        var optionsBuilder = new DbContextOptionsBuilder<MedicalApplicationEFCoreDbContext>()
            .UseSqlServer(";")//.UseSqlite(";") wrong for a solution with SqLite, see https://isc.devexpress.com/internal/ticket/details/t1240173
            .UseChangeTrackingProxies()
            .UseObjectSpaceLinkProxies();
        return new MedicalApplicationEFCoreDbContext(optionsBuilder.Options);
    }
}
//This factory creates DbContext for design-time services. For example, it is required for database migration.
public class MedicalApplicationDesignTimeDbContextFactory : IDesignTimeDbContextFactory<MedicalApplicationEFCoreDbContext> {
    public MedicalApplicationEFCoreDbContext CreateDbContext(string[] args) {
        var connectionString = ResolveConnectionString();
        ArgumentNullException.ThrowIfNull(connectionString, nameof(connectionString));

        var optionsBuilder = new DbContextOptionsBuilder<MedicalApplicationEFCoreDbContext>();
        optionsBuilder.UseSqlServer(connectionString);
        optionsBuilder.UseChangeTrackingProxies();
        optionsBuilder.UseObjectSpaceLinkProxies();
        return new MedicalApplicationEFCoreDbContext(optionsBuilder.Options);
    }

    private static string ResolveConnectionString() {
        // Check environment variables first (supports ASP.NET Core convention: ConnectionStrings__ConnectionString)
        var envPrimary = Environment.GetEnvironmentVariable("ConnectionStrings__ConnectionString");
        var envEasyTest = Environment.GetEnvironmentVariable("ConnectionStrings__EasyTestConnectionString");
        if (!string.IsNullOrEmpty(envPrimary)) return envPrimary;
        if (!string.IsNullOrEmpty(envEasyTest)) return envEasyTest;

        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var candidateBasePaths = new[] {
            Directory.GetCurrentDirectory(),
            Path.Combine(Directory.GetCurrentDirectory(), "..", "MedicalApplication.Blazor.Server"),
            AppContext.BaseDirectory
        };

        foreach (var basePath in candidateBasePaths) {
            var main = Path.Combine(basePath, "appsettings.json");
            var envFile = string.IsNullOrEmpty(env) ? null : Path.Combine(basePath, $"appsettings.{env}.json");
            if (File.Exists(main)) {
                try {
                    using var mainDoc = JsonDocument.Parse(File.ReadAllText(main));
                    string? fromMain = TryGetConnectionString(mainDoc);
                    if (!string.IsNullOrEmpty(envFile) && File.Exists(envFile)) {
                        using var envDoc = JsonDocument.Parse(File.ReadAllText(envFile));
                        // Environment-specific overrides main if present
                        string? fromEnv = TryGetConnectionString(envDoc);
                        if (!string.IsNullOrEmpty(fromEnv)) return fromEnv!;
                    }
                    if (!string.IsNullOrEmpty(fromMain)) return fromMain!;
                } catch {
                    // ignore and try next base path
                }
            }
        }
        return null!; // will be validated by ThrowIfNull
    }

    private static string? TryGetConnectionString(JsonDocument doc) {
        if (doc.RootElement.TryGetProperty("ConnectionStrings", out var csSection)) {
            if (csSection.TryGetProperty("ConnectionString", out var primary)) {
                return primary.GetString();
            }
            if (csSection.TryGetProperty("EasyTestConnectionString", out var easy)) {
                return easy.GetString();
            }
        }
        return null;
    }
}
[TypesInfoInitializer(typeof(MedicalApplicationContextInitializer))]
public class MedicalApplicationEFCoreDbContext : DbContext {
    public MedicalApplicationEFCoreDbContext(DbContextOptions<MedicalApplicationEFCoreDbContext> options) : base(options) {
    }
    //public DbSet<ModuleInfo> ModulesInfo { get; set; }
    public DbSet<ReportDataV2> ReportDataV2 { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Pacient> Pacienti { get; set; }
    public DbSet<Medic> Medici { get; set; }
    public DbSet<Programare> Programari { get; set; }
    public DbSet<Specializare> Specializari { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        modelBuilder.UseDeferredDeletion(this);
        modelBuilder.UseOptimisticLock();
        modelBuilder.SetOneToManyAssociationDeleteBehavior(DeleteBehavior.SetNull, DeleteBehavior.Cascade);
        modelBuilder.HasChangeTrackingStrategy(ChangeTrackingStrategy.ChangingAndChangedNotificationsWithOriginalValues);
        modelBuilder.UsePropertyAccessMode(PropertyAccessMode.PreferFieldDuringConstruction);

        // Domain relationships and indexes
        modelBuilder.Entity<Medic>()
            .HasOne(m => m.Specializare)
            .WithMany(s => s.MedicList)
            .HasForeignKey(m => m.SpecializareId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Programare>()
            .HasOne(p => p.Specializare)
            .WithMany()
            .HasForeignKey(p => p.SpecializareId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Programare>()
            .HasOne(p => p.Medic)
            .WithMany()
            .HasForeignKey(p => p.MedicId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Programare>()
            .HasOne(p => p.Pacient)
            .WithMany()
            .HasForeignKey(p => p.PacientId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Programare>()
            .HasIndex(p => p.DataOra);
        modelBuilder.Entity<Programare>()
            .HasIndex(p => p.MedicId);
        modelBuilder.Entity<Programare>()
            .HasIndex(p => new { p.SpecializareId, p.MedicId });

        ReportDbInitializer.Initialize(modelBuilder);
    }

    #region Public - Save Pipeline Validation
    /// <summary>
    /// Validates appointments for domain rules before saving changes.
    /// </summary>
    public override int SaveChanges()
    {
        ValidateAppointments();
        return base.SaveChanges();
    }

    /// <summary>
    /// Validates appointments for domain rules before saving changes (async).
    /// </summary>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ValidateAppointments();
        return await base.SaveChangesAsync(cancellationToken);
    }
    #endregion

    #region Private - Validation Logic
    /// <summary>
    /// Applies domain validations: future date on create, doctor-specialty consistency, and overlap checks.
    /// </summary>
    private void ValidateAppointments()
    {
        var tz = TimeZoneInfo.FindSystemTimeZoneById("Europe/Bucharest");
        var nowRo = TimeZoneInfo.ConvertTime(DateTime.UtcNow, tz);

        var entries = ChangeTracker.Entries<Programare>()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
            .Select(e => e.Entity)
            .ToList();

        foreach (var appt in entries)
        {
            // future date rule on create only
            if (Entry(appt).State == EntityState.Added && appt.DataOra < nowRo)
            {
                throw new ValidationException("Programarea nu poate fi în trecut.");
            }

            // Doctor must belong to selected Specialty
            if (appt.MedicId == Guid.Empty || appt.SpecializareId == Guid.Empty)
                throw new ValidationException("Selectați specializarea și medicul.");

            var medic = Medici.AsNoTracking().FirstOrDefault(m => m.Id == appt.MedicId);
            if (medic == null)
                throw new ValidationException("Medicul selectat nu există.");
            if (medic.SpecializareId != appt.SpecializareId)
                throw new ValidationException("Medicul nu aparține specializării selectate.");

            // Overlap checks (30 min default duration). Ignore canceled appointments.
            var start = appt.DataOra;
            var end = appt.DataOra.AddMinutes(30);

            bool doctorOverlap = Programari.AsNoTracking()
                .Where(p => p.Id != appt.Id
                            && p.MedicId == appt.MedicId
                            && p.Status != StatusProgramare.Anulata)
                .Any(p => start < p.DataOra.AddMinutes(30) && p.DataOra < end);
            if (doctorOverlap)
                throw new ValidationException("Medicul are deja o programare care se suprapune în acest interval.");

            bool patientOverlap = Programari.AsNoTracking()
                .Where(p => p.Id != appt.Id
                            && p.PacientId == appt.PacientId
                            && p.Status != StatusProgramare.Anulata)
                .Any(p => start < p.DataOra.AddMinutes(30) && p.DataOra < end);
            if (patientOverlap)
                throw new ValidationException("Pacientul are deja o programare care se suprapune în acest interval.");

        }
    }
    #endregion
}
