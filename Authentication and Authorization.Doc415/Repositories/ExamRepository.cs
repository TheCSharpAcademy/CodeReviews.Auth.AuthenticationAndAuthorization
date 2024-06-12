using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RadiologyPatientsExams.Data;
using RadiologyPatientsExams.Models;

namespace RadiologyPatientsExams.Repositories
{
    public class ExamRepository : IRadiologyExamRepository
    {
        private readonly RadiologyDb _context;
        public ExamRepository(RadiologyDb context)
        {
            _context = context;
        }

        public async Task<List<Exam>> GetAllExams(string ExamDoctor, string ExamModality)
        {
            var exams = from m in _context.Exams
                        select m;

            if (!string.IsNullOrEmpty(ExamDoctor))
            {
                exams = exams.Where(s => s.Doctor == ExamDoctor);
            }

            if (!string.IsNullOrEmpty(ExamModality))
            {
                exams = exams.Where(s => s.Type == ExamModality);
            }

            return await exams.Where(x => x.NotDeleted == true).ToListAsync();
        }

        public async Task<SelectList> GetDoctors()
        {
            var doctorList = new SelectList(await _context.Exams.Select(x => x.Doctor).Distinct().ToListAsync());
            return doctorList;
        }

        public async Task<SelectList> GetModalities()
        {
            var modalityList = new SelectList(await _context.Exams.Select(x => x.Type).Distinct().ToListAsync());
            return modalityList;
        }


        public async Task<Exam> GetExamById(int id)
        {
            var exam = await _context.Exams.FirstOrDefaultAsync(x => x.Id == id && x.NotDeleted == true);
            return exam!;
        }

        public async Task DeleteExam(int id)
        {
            var exam = _context.Exams.FirstOrDefault(x => x.Id == id && x.NotDeleted == true);
            exam!.NotDeleted = false;

            _context.Exams.Entry(exam).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task InsertExam(Exam exam)
        {
            var newExam = new Exam()
            {
                Diagnosis = exam.Diagnosis,
                Type = exam.Type,
                Date = exam.Date,
                RefPatientId = exam.RefPatientId,
                Patient = exam.Patient,
                Doctor = exam.Doctor,
                Comments = exam.Comments
            };
            _context.Add(newExam);
            _context.SaveChanges();
        }

        public async Task UpdateExam(int id, Exam exam)
        {
            var toUpdate = _context.Exams.FirstOrDefault(x => x.Id == id && x.NotDeleted == true);
            toUpdate!.Doctor = exam.Doctor;
            toUpdate.NotDeleted = true;
            toUpdate.RefPatientId = exam.RefPatientId;
            toUpdate.Date = exam.Date;
            toUpdate.Diagnosis = exam.Diagnosis;
            toUpdate.Comments = exam.Comments;
            toUpdate.Type = exam.Type;
            toUpdate.Patient = exam.Patient;

            _context.Update(toUpdate);
            // _context.Exams.Entry(toUpdate).State = EntityState.Modified;
            _context.SaveChangesAsync();
        }

        public async Task<List<Exam>> GetPatientsExams(int PatientRefId)
        {
            var patientExams = await _context.Exams.Where(x => x.RefPatientId == PatientRefId && x.NotDeleted == true).ToListAsync();
            return patientExams;
        }

        public async Task DeletePatientsExams(int id)
        {
            var examsToDelete = _context.Exams.Where(x => x.RefPatientId == id).ToList();
            foreach (var exam in examsToDelete)
            {
                exam.NotDeleted = false;
            }
        }
    }
}
