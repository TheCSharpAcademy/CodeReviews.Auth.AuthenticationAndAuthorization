using RadiologyPatientsExams.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RadiologyPatientsExams.Models;

public class Exam
{
    public int Id { get; set; }
    [Required]
    [ForeignKey("Patient")]
    public int RefPatientId { get; set; }
    public Patient Patient { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
    [BirthDateValidation(ErrorMessage = "Examination date can not be in the future.")]
    public DateTime Date { get; set; } = DateTime.Now.Date;

    [Required]
    public string Type { get; set; }

    [Required]
    public string Diagnosis { get; set; }

    [Required]
    public string Doctor { get; set; }

    public string? Comments { get; set; }

    public bool NotDeleted { get; set; } = true;
}

