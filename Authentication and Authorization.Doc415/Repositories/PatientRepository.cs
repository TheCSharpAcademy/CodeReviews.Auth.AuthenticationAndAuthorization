using Microsoft.EntityFrameworkCore;
using RadiologyPatientsExams.Data;
using RadiologyPatientsExams.Models;

namespace RadiologyPatientsExams.Repositories;

public class PatientRepository : IRadiologyPatientRepository
{
    //private readonly RadiologyDb _context;

    private readonly IDbContextFactory<RadiologyDb> _fcontext;

    public PatientRepository(IDbContextFactory<RadiologyDb> fcontext)
    {
        _fcontext = fcontext;
    }

    //public PatientRepository(RadiologyDb context)
    //{
    //    _context = context;
    //}

    public async Task<List<Patient>> GetAllPatients(string name, string surname)
    {
        using var _context = _fcontext.CreateDbContext();
        IQueryable<string> patientQuery = from m in _context.Patients
                                          orderby m.Name
                                          select m.Name;
        var patients = from m in _context.Patients
                       select m;

        if (!string.IsNullOrEmpty(surname))
        {
            patients = patients.Where(s => s.Surname == surname);
        }

        if (!string.IsNullOrEmpty(name))
        {
            patients = patients.Where(x => x.Name == name);
        }

        patients = patients.Where(x => x.NotDeleted == true);


        return await patients.ToListAsync();
    }

    public async Task<Patient> GetPatientById(int id)
    {
        using var _context = _fcontext.CreateDbContext();
        var patient = await _context.Patients.FirstOrDefaultAsync(x => x.Id == id && x.NotDeleted == true);
        return patient!;
    }

    public async Task DeletePatient(int id)
    {
        using var _context = _fcontext.CreateDbContext();
        var patient = await _context.Patients.FirstOrDefaultAsync(x => x.Id == id && x.NotDeleted == true);
        patient.NotDeleted = false;

        _context.Patients.Entry(patient).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task InsertPatient(Patient patient)
    {
        using var _context = _fcontext.CreateDbContext();
        await _context.AddAsync(patient);
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePatient(int id, Patient patient)
    {
        using var _context = _fcontext.CreateDbContext();
        var toUpdate = await _context.Patients.FirstOrDefaultAsync(x => x.Id == id && x.NotDeleted == true);
        toUpdate!.BirthDate = patient.BirthDate;
        toUpdate.Name = patient.Name;
        toUpdate.Phone = patient.Phone;
        toUpdate.Surname = patient.Surname;
        toUpdate.Email = patient.Email;
        toUpdate.NotDeleted = true;

        _context.Update(toUpdate);
        //_context.Patients.Entry(toUpdate).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}
