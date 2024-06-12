using Microsoft.AspNetCore.Mvc.Rendering;

namespace RadiologyPatientsExams.Models;

public class ExamListViewModel
{
    public List<ExamViewModel> ExamList { get; set; } = new List<ExamViewModel>();
    public SelectList? Doctors { get; set; }
    public SelectList? Modalities { get; set; }
    public string? ExamDoctor { get; set; }
    public string? ExamModality { get; set; }
}
