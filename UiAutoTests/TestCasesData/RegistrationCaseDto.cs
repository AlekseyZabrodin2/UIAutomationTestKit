using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiAutoTests.TestCasesData
{
    public class RegistrationCaseDto
    {
        public string Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }

        public bool BirthdateUse { get; set; }
        public string BirthdateText { get; set; }

        public int SelectedGender { get; set; }

        public string Address { get; set; }
        public string Phone { get; set; }
        public string Info { get; set; }
    }
}
