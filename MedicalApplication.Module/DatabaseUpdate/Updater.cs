using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.EF;
using DevExpress.Persistent.BaseImpl.EF;
using MedicalApplication.Module.BusinessObjects;

namespace MedicalApplication.Module.DatabaseUpdate;

// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Updating.ModuleUpdater
public class Updater : ModuleUpdater {
    public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
        base(objectSpace, currentDBVersion) {
    }
    public override void UpdateDatabaseAfterUpdateSchema() {
        base.UpdateDatabaseAfterUpdateSchema();

        // Seed Specialties
        var specCardio = ObjectSpace.FirstOrDefault<Specializare>(s => s.Denumire == "Cardiologie") ?? ObjectSpace.CreateObject<Specializare>();
        specCardio.Denumire = "Cardiologie";
        var specDerm = ObjectSpace.FirstOrDefault<Specializare>(s => s.Denumire == "Dermatologie") ?? ObjectSpace.CreateObject<Specializare>();
        specDerm.Denumire = "Dermatologie";
        var specOrto = ObjectSpace.FirstOrDefault<Specializare>(s => s.Denumire == "Ortopedie") ?? ObjectSpace.CreateObject<Specializare>();
        specOrto.Denumire = "Ortopedie";

        // Seed Doctors
        var m1 = ObjectSpace.FirstOrDefault<Medic>(m => m.Nume == "Dr. Popescu Ion") ?? ObjectSpace.CreateObject<Medic>();
        m1.Nume = "Dr. Popescu Ion"; m1.Specializare = specCardio;
        var m2 = ObjectSpace.FirstOrDefault<Medic>(m => m.Nume == "Dr. Ionescu Maria") ?? ObjectSpace.CreateObject<Medic>();
        m2.Nume = "Dr. Ionescu Maria"; m2.Specializare = specDerm;
        var m3 = ObjectSpace.FirstOrDefault<Medic>(m => m.Nume == "Dr. Georgescu Andrei") ?? ObjectSpace.CreateObject<Medic>();
        m3.Nume = "Dr. Georgescu Andrei"; m3.Specializare = specOrto;
        var m4 = ObjectSpace.FirstOrDefault<Medic>(m => m.Nume == "Dr. Vlad Ana") ?? ObjectSpace.CreateObject<Medic>();
        m4.Nume = "Dr. Vlad Ana"; m4.Specializare = specCardio;

        // Seed Patients
        var p1 = ObjectSpace.FirstOrDefault<Pacient>(p => p.Nume == "Stancu Mircea") ?? ObjectSpace.CreateObject<Pacient>();
        p1.Nume = "Stancu Mircea"; p1.Email = "mircea.stancu@example.com"; p1.NumarTelefon = "+40744111222"; p1.CNP = "1960523123457";
        var p2 = ObjectSpace.FirstOrDefault<Pacient>(p => p.Nume == "Iacob Elena") ?? ObjectSpace.CreateObject<Pacient>();
        p2.Nume = "Iacob Elena"; p2.Email = "elena.iacob@example.com"; p2.NumarTelefon = "+40755111222"; p2.CNP = "2910315123453";
        var p3 = ObjectSpace.FirstOrDefault<Pacient>(p => p.Nume == "Munteanu Raul") ?? ObjectSpace.CreateObject<Pacient>();
        p3.Nume = "Munteanu Raul"; p3.Email = "raul.m@example.com"; p3.NumarTelefon = "+40733111222"; p3.CNP = "1800101123456";
        var p4 = ObjectSpace.FirstOrDefault<Pacient>(p => p.Nume == "Dobre Alina") ?? ObjectSpace.CreateObject<Pacient>();
        p4.Nume = "Dobre Alina"; p4.Email = "alina.dobre@example.com"; p4.NumarTelefon = "+40722111222"; p4.CNP = "2981212123451";

        ObjectSpace.CommitChanges();

        // Seed Appointments (now + offsets in Bucharest)
        var tz = TimeZoneInfo.FindSystemTimeZoneById("Europe/Bucharest");
        var nowRo = TimeZoneInfo.ConvertTime(DateTime.UtcNow, tz).Date.AddHours(10);

        void EnsureProgramare(DateTime start, Specializare s, Medic m, Pacient p)
        {
            var existing = ObjectSpace.FirstOrDefault<Programare>(x => x.DataOra == start && x.Medic == m && x.Pacient == p);
            if (existing == null)
            {
                var pr = ObjectSpace.CreateObject<Programare>();
                pr.DataOra = start;
                pr.Status = StatusProgramare.Planificata;
                pr.Specializare = s;
                pr.Medic = m;
                pr.Pacient = p;
            }
        }

        EnsureProgramare(nowRo.AddDays(1), specCardio, m1, p1);
        EnsureProgramare(nowRo.AddDays(1).AddMinutes(30), specCardio, m4, p2);
        EnsureProgramare(nowRo.AddDays(2).AddHours(2), specDerm, m2, p3);
        EnsureProgramare(nowRo.AddDays(3).AddHours(3), specOrto, m3, p4);

        ObjectSpace.CommitChanges();
    }
    public override void UpdateDatabaseBeforeUpdateSchema() {
        base.UpdateDatabaseBeforeUpdateSchema();
    }
}
