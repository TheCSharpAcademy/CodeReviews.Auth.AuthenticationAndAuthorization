using RadiologyPatientsExams.Models;
namespace RadiologyPatientsExams.DTOTransformations;

static public class DTOTransform
{
    static public ExamListViewModel GetExamsViewList(List<Exam> examList, List<(string, string)> nameList)
    {
        var viewModel = new ExamListViewModel();
        var index = 0;
        foreach (var exam in examList)
        {
            var examView = new ExamViewModel()
            {
                Id = exam.Id,
                PatientName = nameList[index].Item1,
                PatientSurname = nameList[index].Item2,
                Date = exam.Date.ToShortDateString(),
                Modality = exam.Type,
                Doctor = exam.Doctor,
                Comments = exam.Comments
            };
            viewModel.ExamList.Add(examView);
            index++;
        }
        return viewModel;
    }

    static public ExamViewModel ConvertExamToView(Exam exam, string name, string surname)
    {
        var viewModel = new ExamViewModel()
        {
            Id = exam.Id,
            PatientName = name,
            PatientSurname = surname,
            Date = exam.Date.ToShortDateString(),
            Modality = exam.Type,
            Doctor = exam.Doctor,
            Comments = exam.Comments,
            Diagnosis = exam.Diagnosis,
            RefPatientId = exam.RefPatientId

        };
        return viewModel;
    }

    static public Exam ExamViewToDb(ExamViewModel examView)
    {
        var dbModel = new Exam()
        {
            Id = examView.Id,
            RefPatientId = examView.RefPatientId,
            Date = DateTime.Parse(examView.Date),
            Type = examView.Modality,
            Doctor = examView.Doctor,
            Comments = examView.Comments,
            Diagnosis = examView.Diagnosis,
        };
        return dbModel;
    }
}
