using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System.Collections;

namespace UiAutoTests.TestCasesData
{
    public static class MainWindowTestCases
    {        

        public static IEnumerable ValidRegistrationFieldCases
        {
            get
            {
                yield return new TestCaseData (new RegistrationCaseDto
                {
                    Id = "01",
                    LastName = "Ivanov",
                    FirstName = "Ivan",
                    MiddleName = "Ivanovich",
                    Address = "Minsk",
                    Phone = "769879879",
                    Info = "Some text"
                }).SetName("Test04-1: Valid first");

                yield return new TestCaseData( new RegistrationCaseDto
                {
                    Id = "02",
                    LastName = "Petrov",
                    FirstName = "Petr",
                    MiddleName = "Petrovich",
                    Address = "Brest",
                    Phone = "564654321",
                    Info = "Just some text"
                }).SetName("Test04-2: Valid next");

                yield return new TestCaseData(new RegistrationCaseDto
                {
                    Id = "03",
                    LastName = "Vasin",
                    FirstName = "Vasia",
                    MiddleName = "Vasinovich",
                    Address = "Gdanovichi",
                    Phone = "769879879",
                    Info = "Some text"
                }).SetName("Test04-3: Valid next next");
            }
        }

        public static IEnumerable IgnoreSetUpCases
        {
            get
            {
                yield return new TestCaseData(new RegistrationCaseDto
                {
                    Id = "01",
                    LastName = "Ivanov",
                    FirstName = "Ivan",
                    MiddleName = "Ivanovich",
                    Address = "Minsk",
                    Phone = "769879879",
                    Info = "Some text"
                }).SetName("Test05-1: Valid first");

                yield return new TestCaseData(new RegistrationCaseDto
                {
                    Id = "02",
                    LastName = "Petrov",
                    FirstName = "Petr",
                    MiddleName = "Petrovich",
                    Address = "Brest",
                    Phone = "564654321",
                    Info = "Just some text"
                }).SetName("Test05-2: Valid next");
            }
        }

        public static IEnumerable ValidRegistrationCases
        {
            get
            {
                yield return new TestCaseData(new RegistrationCaseDto
                {
                    Id = "01",
                    LastName = "Ivanov",
                    FirstName = "Vasila",
                    MiddleName = "Petrovich",

                    BirthdateUse = true,
                    BirthdateText = "19.01.2021",

                    SelectedGender = 1,

                    Address = "Minsk",
                    Phone = "769879879",
                    Info = "Testing text"
                }).SetName("Test07-1: Valid Registration");

                yield return new TestCaseData(new RegistrationCaseDto
                {
                    Id = "02",
                    LastName = "Johnson",
                    FirstName = "Emily",
                    MiddleName = "Grace",

                    BirthdateUse = true,
                    BirthdateText = "25.12.1995",

                    SelectedGender = 2,

                    Address = "London, Baker Street 221B",
                    Phone = "123456789",
                    Info = "Second test case with different data"
                }).SetName("Test07-2: Valid Registration");
            }
        }


        public static IEnumerable ValidRegistrationCasesFromJson
        {
            get
            {
                var testData = TestDataFromJson.TestDataInitialize();

                yield return new TestCaseData(testData.TestValidRegistration)
                        .SetName("Test08-1: Valid Registration from Json");

                yield return new TestCaseData(testData.TestInvalidRegistration)
                        .SetName("Test08-2: Invalid Registration from Json");
            }
        }










    }
}
