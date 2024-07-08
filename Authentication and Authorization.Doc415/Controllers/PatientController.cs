using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RadiologyPatientsExams.Models;
using RadiologyPatientsExams.Services;

namespace RadiologyPatientsExams.Controllers
{
    [Authorize]
    public class PatientController : Controller
    {
        private readonly PatientService _patientService;
        private readonly ExamService _examService;
        private bool validPatient = true;

        public PatientController(PatientService patientService, ExamService examService)
        {
            _patientService = patientService;
            _examService = examService;
        }

        public async Task<IActionResult> Index(string name, string surname)
        {


            var patientList = await _patientService.GetAllExams(name, surname);
            ViewBag.ValidPatient = TempData["validPatient"];
            return View(patientList);
        }

        public async Task<IActionResult> PatientDetail(int id)
        {
            ViewBag.RequestFrom = "PatientDetail";
            int queryId = -1;
            if (TempData["Id"] != null)
            {
                queryId = int.Parse(TempData["Id"].ToString());
            }
            else
            {
                queryId = id;
            }

            var patient = await _patientService.GetPatientById(queryId);
            var exams = await _examService.GetPatientsExamsForView(queryId);
            var patientDetailVM = new PatientDetailModel()
            {
                Patient = patient,
                PatientsExamsList = exams
            };
            return View(patientDetailVM);
        }

        public async Task<IActionResult> AddPatient()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPatient(Patient patient)
        {
            if (!ModelState.IsValid)
            {
                TempData["ValidPatient"] = false;
                return RedirectToAction("Index", "Patient");
            }

            try
            {
                await _patientService.AddPatient(patient);
                TempData["SuccessMessage"] = "Patient added successfully!";
                TempData["ValidPatient"] = true;
                return RedirectToAction("Index", "Patient");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error adding patient: {ex.Message}";
                return View("Index", "Patient");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePatient(int id, [Bind("Id,Name,Surname,BirthDate,Email,Phone,NotDeleted")] Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return View("PatientDetail", id);
            }

            try
            {
                await _patientService.UpdatePatient(id, patient);
                TempData["SuccessMessage"] = "Patient updated successfully!";
                return RedirectToAction("Index", "Patient");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error updating patient: {ex.Message}";
                return View("PatientDetail", id);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeletePatient(int id)
        {
            await _patientService.DeletePatient(id);
            return RedirectToAction("Index", "Patient");
        }
    }
}
