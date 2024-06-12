using RadiologyPatientsExams.Models;
using RadiologyPatientsExams.Repositories;

namespace RadiologyPatientsExams.Services
{
    public class PatientService
    {
        private readonly IRadiologyPatientRepository _repository;
        private readonly IRadiologyExamRepository _examRepository;

        public PatientService(IRadiologyPatientRepository repository, IRadiologyExamRepository examRepository)
        {
            _repository = repository;
            _examRepository = examRepository;
        }

        public async Task<PatientListModel> GetAllExams(string name, string surname)
        {
            var patientList = await _repository.GetAllPatients(name, surname);
            var viewList = new PatientListModel() { PatientList = patientList };

            return viewList;

        }

        public async Task<Patient> GetPatientById(int id)
        {
            var patient = await _repository.GetPatientById(id);
            return patient;
        }

        public async Task AddPatient(Patient patient)
        {
            await _repository.InsertPatient(patient);
        }

        public async Task UpdatePatient(int id, Patient patient)
        {
            await _repository.UpdatePatient(id, patient);
        }

        public async Task DeletePatient(int id)
        {
            _examRepository.DeletePatientsExams(id);
            await _repository.DeletePatient(id);

        }
    }
}
