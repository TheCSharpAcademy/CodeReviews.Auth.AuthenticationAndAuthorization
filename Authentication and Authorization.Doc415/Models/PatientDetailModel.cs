namespace RadiologyPatientsExams.Models
{
    public class PatientDetailModel
    {
        public Patient Patient { get; set; }
        public List<ExamViewModel> PatientsExamsList { get; set; }
    }
}
