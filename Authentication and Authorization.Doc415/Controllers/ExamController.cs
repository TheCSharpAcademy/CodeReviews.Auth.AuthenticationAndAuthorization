using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RadiologyPatientsExams.Models;
using RadiologyPatientsExams.Services;

namespace RadiologyPatientsExams.Controllers
{
    [Authorize]
    public class ExamController : Controller
    {
        private readonly ExamService _service;
        private readonly PatientService _patientService;


        public ExamController(ExamService service, PatientService patientservice)
        {
            _service = service;
            _patientService = patientservice;
        }

        public async Task<IActionResult> Index(string ExamDoctor, string ExamModality)
        {
            var examList = await _service.GetAllExams(ExamDoctor, ExamModality);
            return View(examList);
        }

        public async Task<IActionResult> ExamDetail(int id)
        {
            var referer = Request.Headers["Referer"].ToString().Split('/', '?')[3];
            if (referer == "Exam")
                ViewBag.RequestFrom = "Exam";
            else
                ViewBag.RequestFrom = "Patient";
            var exam = await _service.GetExamById(id);
            return View(exam);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int Id, ExamViewModel examFromView)
        {
            if (!ModelState.IsValid)
            {
                return View("ExamDetail", examFromView);
            }

            try
            {
                await _service.UpdateExam(Id, examFromView);
                TempData["SuccessMessage"] = "Examination updated successfully!";
                TempData["Id"] = Id;
                return RedirectToAction("PatientDetail", "Patient");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error updating session: {ex.Message}";
                return View("ExamDetail", examFromView);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteExam(int id)
        {
            await _service.DeleteExam(id);
            var referer = Request.Headers["Referer"].ToString().Split('/', '?')[3];
            if (referer == "Exam")
                ViewBag.RequestFrom = "Exam";
            else
                ViewBag.RequestFrom = "Patient";
            return new OkResult();
        }

        [HttpGet]
        public async Task<IActionResult> AddExam(int Id)
        {
            var patient = await _patientService.GetPatientById(Id);
            var examVM = new ExamViewModel()
            {
                RefPatientId = Id,
                PatientName = patient.Name,
                PatientSurname = patient.Surname
            };

            return View(examVM);
        }

        [HttpPost]
        public async Task<IActionResult> AddExam(ExamViewModel examVM)
        {

            if (!ModelState.IsValid)
            {
                return View("AddExam", examVM);
            }

            try
            {
                await _service.InsertExam(examVM);
                TempData["Id"] = examVM.Id;
                return RedirectToAction("PatientDetail", "Patient");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error adding exaö: {ex.Message}";
                return View("AddExam", examVM);
            }
        }
    }
}
