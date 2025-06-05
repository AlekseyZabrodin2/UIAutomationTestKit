using CommunityToolkit.Mvvm.ComponentModel;
using UIAutomationTestKit.Models;

namespace UIAutomationTestKit.Model
{
    public partial class User : ObservableObject
    {
        [ObservableProperty]
        public partial string Id { get; set; }

        [ObservableProperty]
        public partial string LastName { get; set; }

        [ObservableProperty]
        public partial string Name { get; set; }

        [ObservableProperty]
        public partial string MiddleName { get; set; }

        [ObservableProperty]
        public partial string BirthDate { get; set; }

        [ObservableProperty]
        public partial string Gender { get; set; }
       
        [ObservableProperty]
        public partial string Adress { get; set; }

        [ObservableProperty]
        public partial int Phone { get; set; }

        [ObservableProperty]
        public partial string Info { get; set; }

        [ObservableProperty]
        public partial string CalendarDate { get; set; }

        [ObservableProperty]
        public partial Documents Document { get; set; }

    }
}
