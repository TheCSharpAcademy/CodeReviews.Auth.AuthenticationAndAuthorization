using RadiologyPatientsExams.Models;

namespace RadiologyPatientsExams.Repositories
{
    public interface IRadiologyPatientRepository
    {
        Task<List<Patient>> GetAllPatients(string name, string surname);
        Task<Patient> GetPatientById(int id);
        Task DeletePatient(int id);
        Task InsertPatient(Patient patient);
        Task UpdatePatient(int id, Patient patient);
    }
}
