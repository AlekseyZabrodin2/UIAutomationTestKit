using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Collections.ObjectModel;
using System.ComponentModel;
using UIAutomationTestKit.Model;
using UIAutomationTestKit.Models;

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
        public partial DateTime SelectedCalendarDate { get; set; }

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
        public partial string CreateAddressUser { get; set; }

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

        [ObservableProperty]
        public partial ObservableCollection<User> UsersCollection { get; set; } = new();

        [ObservableProperty]
        public partial bool RowVirtualization { get; set; }

        private User _user;

        [ObservableProperty]
        public partial int SliderValue { get; set; }

        [ObservableProperty]
        public partial int ProgressBarValue { get; set; }

        [ObservableProperty]
        public partial bool IsCalendarOpen {  get; set; }

        [ObservableProperty]
        public partial Documents SelectedDocument {  get; set; }


        public UserRegistrationViewModel()
        {
            InitializeAppConfig();
            InitialiseProperties();

            PropertyChanged += UserRegistrationViewModel_OnPropertyChanged;
        }



        private void UserRegistrationViewModel_OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            CanRegistratUser();
        }

        public void InitializeAppConfig()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("testClientProperties.json")
                .Build();

            IServiceCollection services = new ServiceCollection();

            services.Configure<TestClientProperties>(configuration.GetSection(nameof(TestClientProperties)));

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            var testClientProperties = serviceProvider.GetRequiredService<IOptions<TestClientProperties>>().Value;

            RowVirtualization = testClientProperties.EnableRowVirtualization;
        }

        private void InitialiseProperties()
        {
            IsEnabled = true;
            IsBusy = false;
            OpacityProperty = 1;
            CreateGenderUser = new List<string> { "Man", "Female", "Other", "string - Empty" };
            UseBirthDateUser = false;
            CreateUserBirthDate = DateTime.Now;
            SelectedCalendarDate = DateTime.Now;
            ProgressBarValue = 0;
        }

        [RelayCommand]
        private void CleanUpFields()
        {
            ResetData();

            UpdateText = "All fields are cleared !!!";
        }

        [RelayCommand(CanExecute = nameof(RegistrationUserCanExecute))]
        private async Task RegistrationUser()
        {
            IsEnabled = false;
            OpacityProperty = 0.9;

            await UpdateUserCollection();

            //ResetData();

            OpacityProperty = 1;
            IsEnabled = true;

            UpdateText = $"[{UsersCollection.Count}] Users is registered";
        }

        [RelayCommand]
        private void CalendarPopupOpen()
        {
            IsCalendarOpen = true;
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
                !string.IsNullOrWhiteSpace(CreateAddressUser) &&
                CreatePhoneUser != 0 &&
                !string.IsNullOrWhiteSpace(CreateInfoUser);
        }

        private async Task UpdateUserCollection()
        {
            for (int i = 0; i < SliderValue; i++)
            {
                _user = new()
                {
                    Id = CreateUserId,
                    LastName = CreateUserLastName,
                    Name = CreateUserName,
                    MiddleName = CreateUserMiddleName,
                    Gender = SelectedGender,
                    BirthDate = CreateUserBirthDate.ToShortDateString(),
                    Adress = CreateAddressUser,
                    Phone = CreatePhoneUser,
                    Info = CreateInfoUser,
                    CalendarDate = SelectedCalendarDate.ToShortDateString(),
                    Document = SelectedDocument
                };

                UsersCollection.Add(_user);

                ProgressBarValue = i + 1;

                await Task.Delay(1000);
            }            
        }

        private void ResetData()
        {
            CreateUserId = string.Empty;
            CreateUserLastName = string.Empty;
            CreateUserName = string.Empty;
            CreateUserMiddleName = string.Empty;
            SelectedGender = CreateGenderUser[3];

            UseBirthDateUser = false;
            CreateUserBirthDate = DateTime.Now;

            CreateAddressUser = string.Empty;
            CreatePhoneUser = 0;
            CreateInfoUser = string.Empty;

            ProgressBarValue = 0;
        }

    }
}
