using Microsoft.CodeAnalysis;
using RadiologyPatientsExams.Models;

namespace RadiologyPatientsExams.Data
{
    public class Seeder
    {
        private readonly RadiologyDb _context;
        public Seeder(RadiologyDb context)
        {
            _context = context;
        }

        public void SeedPatients()
        {
            if (_context.Patients.Any())

            {
                return;   // DB has been seeded
            }
            var nameList = new List<string> { "John", "Pablo", "Steve", "Gavin", "Kronyer", "Brodin", "Xarvus" };
            var surnameList = new List<string> { "Wick", "Rambo", "Kent", "Smith", "Corleone", "Anderson" };
            var random = new Random();
            DateTime startDate = new DateTime(1955, 1, 1);
            DateTime endDate = new DateTime(1990, 1, 1);

            for (int i = 0; i < 15; i++)
            {
                var newDate = startDate.AddHours(random.Next(0, (int)(endDate - startDate).TotalHours));
                var name = nameList[random.Next(0, nameList.Count)];
                var surname = surnameList[random.Next(0, surnameList.Count)];
                var phone = random.Next(1111111111, 2022222222);

                _context.Patients.Add(
               new Patient
               {
                   Name = name,
                   Surname = surname,
                   BirthDate = newDate,
                   Email = $"{name}@{surname}.com",
                   Phone = phone.ToString(),
                   NotDeleted = true,
               });
            }
            _context.SaveChanges();
        }

        public void SeedExams()
        {

            if (_context.Exams.Any())

            {
                return;   // DB has been seeded
            }

            var idList = new List<int>();
            var patients = _context.Patients.ToList();
            var doctorList = new List<string> { "Serdar", "Semra", "Mehmet", "Ali" };
            var modalities = new List<string> { "MRI", "CT", "Ultrasound", "X-Ray" };
            var diagnosisList = new List<string>() { "Gall stone", "Pneumonia", "Kidney stone", "Liver abcess", "Disc hernia" };
            var random = new Random();
            foreach (var pat in patients)
            {
                idList.Add(pat.Id);
            }

            foreach (var id in idList)
            {
                for (int i = 0; i < 6; i++)
                {
                    var newExam = new Exam
                    {
                        RefPatientId = id,
                        Date = DateTime.Today.Subtract(TimeSpan.FromDays(random.Next(3, 20))),
                        Doctor = doctorList[random.Next(0, doctorList.Count)],
                        Type = modalities[random.Next(0, modalities.Count)],
                        Diagnosis = diagnosisList[random.Next(0, diagnosisList.Count - 1)],
                        Comments = "...",
                        NotDeleted = true
                    };
                    _context.Exams.Add(newExam);
                }
            }
            _context.SaveChanges();
        }
    }
}

