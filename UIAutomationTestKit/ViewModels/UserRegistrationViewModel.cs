using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows.Input;
using System.Windows;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using System.ComponentModel;

namespace UIAutomationTestKit.ViewModels
{
    public partial class UserRegistrationViewModel : ObservableObject
    {

        [ObservableProperty]
        public partial string UpdateText { get; set; }

        [ObservableProperty]
        public partial string CreateUserId { get; set; }

        [ObservableProperty]
        public partial string CreateUserLastName { get; set; }

        [ObservableProperty]
        public partial string CreateUserName { get; set; }

        [ObservableProperty]
        public partial string CreateUserMiddleName { get; set; }

        [ObservableProperty]
        public partial DateTime CreateUserBirthDate { get; set; }

        [ObservableProperty]
        public partial bool UseBirthDateUser { get; set; }

        [ObservableProperty]
        public partial DateTime SelectedBirthDatePatient { get; set; }

        [ObservableProperty]
        public partial List<string> CreateGenderUser { get; set; }

        private string? _gender;
        public string? SelectedGender
        {
            get => _gender;

            set
            {
                if (value == CreateGenderUser[0])
                {
                    value = "M";
                }
                else if (value == CreateGenderUser[1])
                {
                    value = "F";
                }
                else if (value == CreateGenderUser[2])
                {
                    value = "O";
                }
                else if (value == CreateGenderUser[3])
                {
                    value = null;
                }
                SetProperty(ref _gender, value);
            }
        }

        [ObservableProperty]
        public partial string CreateAdressUser { get; set; }

        [ObservableProperty]
        public partial int CreatePhoneUser { get; set; }

        [ObservableProperty]
        public partial string CreateInfoUser { get; set; }

        [ObservableProperty]
        public partial bool IsEnabled { get; set; }

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RegistrationUserCommand))]
        public partial bool IsBusy { get; set; }

        [ObservableProperty]
        public partial double OpacityProperty { get; set; }

        public bool RegistrationUserCanExecute => IsBusy;



        public UserRegistrationViewModel()
        {
            InitialiseProperties();

            PropertyChanged += UserRegistrationViewModel_OnPropertyChanged;
        }



        private void UserRegistrationViewModel_OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            CanRegistratUser();
        }

        private void InitialiseProperties()
        {
            IsEnabled = true;
            IsBusy = false;
            OpacityProperty = 1;
            CreateGenderUser = new List<string> { "Man", "Female", "Other", "string - Empty" };
            UseBirthDateUser = false;
            CreateUserBirthDate = DateTime.Now;
        }

        [RelayCommand]
        private void CleanUpFields()
        {
            CreateUserId = string.Empty;
            CreateUserLastName = string.Empty;
            CreateUserName = string.Empty;
            CreateUserMiddleName = string.Empty;
            SelectedGender = CreateGenderUser[3];

            UseBirthDateUser = false;
            CreateUserBirthDate = DateTime.Now;

            CreateAdressUser = string.Empty;
            CreatePhoneUser = 0;
            CreateInfoUser = string.Empty;

            UpdateText = "All fields are cleared !!!";
        }

        [RelayCommand(CanExecute = nameof(RegistrationUserCanExecute))]
        private async Task RegistrationUser()
        {
            IsEnabled = false;
            OpacityProperty = 0.3;

            await Task.Delay(1000);

            OpacityProperty = 1;
            IsEnabled = true;

            UpdateText = "User is registered";
        }

        private void CanRegistratUser()
        {
            IsBusy =
                !string.IsNullOrWhiteSpace(CreateUserId) &&
                !string.IsNullOrWhiteSpace(CreateUserLastName) &&
                !string.IsNullOrWhiteSpace(CreateUserName) &&
                !string.IsNullOrWhiteSpace(CreateUserMiddleName) &&
                !string.IsNullOrWhiteSpace(SelectedGender) &&
                UseBirthDateUser &&
                !string.IsNullOrWhiteSpace(CreateAdressUser) &&
                CreatePhoneUser != 0 &&
                !string.IsNullOrWhiteSpace(CreateInfoUser);
        }

    }
}
