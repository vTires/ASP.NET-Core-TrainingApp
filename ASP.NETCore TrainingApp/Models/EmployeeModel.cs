using System;

namespace ASP.NETCore_TrainingApp.Models
{
    public class EmployeeModel
    {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Position { get; set; }
            public bool IsAgreement { get; set; }
            public DateTime Date { get; set; }
    }
}
