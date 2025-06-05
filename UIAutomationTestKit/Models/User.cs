using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public partial DateTime BirthDate { get; set; }

        [ObservableProperty]
        public partial string Gender { get; set; }
       
        [ObservableProperty]
        public partial string Adress { get; set; }

        [ObservableProperty]
        public partial int Phone { get; set; }

        [ObservableProperty]
        public partial string Info { get; set; }

    }
}
