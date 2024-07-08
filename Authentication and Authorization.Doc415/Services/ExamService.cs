using RadiologyPatientsExams.DTOTransformations;
using RadiologyPatientsExams.Models;
using RadiologyPatientsExams.Repositories;

namespace RadiologyPatientsExams.Services;

public class ExamService
{
    private readonly IRadiologyExamRepository _examRepository;
    private readonly IRadiologyPatientRepository _patientRepository;

    public ExamService(IRadiologyExamRepository examRepository, IRadiologyPatientRepository patientRepository)
    {
        _examRepository = examRepository;
        _patientRepository = patientRepository;
    }

    public async Task<ExamListViewModel> GetAllExams(string ExamDoctor, string ExamModality)
    {
        var examListFromDb = await _examRepository.GetAllExams(ExamDoctor, ExamModality);
        examListFromDb = examListFromDb.OrderByDescending(x => x.Date).ToList();
        var patientNameList = new List<(string, string)>();
        foreach (var exam in examListFromDb)
        {
            int patientRefId = exam.RefPatientId;
            var patient = await _patientRepository.GetPatientById(patientRefId);
            patientNameList.Add((patient.Name, patient.Surname));
        }
        var examListToController = DTOTransform.GetExamsViewList(examListFromDb, patientNameList);
        examListToController.Doctors = await _examRepository.GetDoctors();
        examListToController.Modalities = await _examRepository.GetModalities();

        return examListToController;

    }

    public async Task<ExamViewModel> GetExamById(int id)  // BURASI DUZENLENECEK
    {
        var examFromDb = await _examRepository.GetExamById(id);
        int patientRefId = examFromDb.RefPatientId;
        var patient = await _patientRepository.GetPatientById(patientRefId);
        var examToView = DTOTransform.ConvertExamToView(examFromDb, patient.Name, patient.Surname);
        return examToView;
    }

    public async Task DeleteExam(int id)
    {
        await _examRepository.DeleteExam(id);
    }


    public async Task InsertExam(ExamViewModel examVM)
    {
        var exam = DTOTransform.ExamViewToDb(examVM);
        await _examRepository.InsertExam(exam);
    }

    public async Task UpdateExam(int id, ExamViewModel exam)
    {
        var examToDb = DTOTransform.ExamViewToDb(exam);
        await _examRepository.UpdateExam(id, examToDb);
    }

    public async Task<List<Exam>> GetPatientsExams(int PatientRefId)
    {
        var patientExams = await _examRepository.GetPatientsExams(PatientRefId);
        patientExams = patientExams.OrderByDescending(x => x.Date).ToList();
        return patientExams;
    }

    public async Task<List<ExamViewModel>> GetPatientsExamsForView(int PatientRefId)
    {
        var exams = await GetPatientsExams(PatientRefId);
        var patientNameList = new List<(string, string)>();
        foreach (var exam in exams)
        {
            int patientRefId = exam.RefPatientId;
            var patient = await _patientRepository.GetPatientById(patientRefId);
            patientNameList.Add((patient.Name, patient.Surname));
        }
        var examListToDetailView = DTOTransform.GetExamsViewList(exams, patientNameList).ExamList;

        return examListToDetailView;

    }
}
