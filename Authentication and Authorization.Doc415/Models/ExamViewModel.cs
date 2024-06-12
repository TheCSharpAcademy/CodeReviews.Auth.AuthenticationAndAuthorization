using RadiologyPatientsExams.Validations;
using System.ComponentModel.DataAnnotations;

namespace RadiologyPatientsExams.Models;

public class ExamViewModel
{

    public int Id { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
    [BirthDateValidation(ErrorMessage = "Examination date can not be in the future.")]
    public string Date { get; set; }

    public string Modality { get; set; }
    [Display(Name = "Patient Name")]
    public string PatientName { get; set; }
    [Display(Name = "Patient Surname")]
    public string PatientSurname { get; set; }

    public string Doctor { get; set; }

    public string? Comments { get; set; }

    public string Diagnosis { get; set; }

    public int RefPatientId { get; set; }
}
