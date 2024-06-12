using Microsoft.AspNetCore.Mvc.Rendering;
using RadiologyPatientsExams.Models;

namespace RadiologyPatientsExams.Repositories;

public interface IRadiologyExamRepository
{
    Task<List<Exam>> GetAllExams(string ExamDoctor, string ExamModality);
    Task<Exam> GetExamById(int id);
    Task DeleteExam(int id);
    Task InsertExam(Exam exam);
    Task UpdateExam(int id, Exam exam);
    Task<List<Exam>> GetPatientsExams(int PatientRefId);
    Task DeletePatientsExams(int id);
    Task<SelectList> GetDoctors();
    Task<SelectList> GetModalities();
}
