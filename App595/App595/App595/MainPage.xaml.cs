using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App595
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();


            myViewModel vm = new myViewModel();

            this.BindingContext = vm;
            vm.PerformShimmerAsyncTask("123");
        }
    }

    public class myViewModel : BaseViewModel
    {

        private MyModel _attend;
        private bool _isLoadBusy = false;

        public MyModel Attend
        {
            get { return _attend; }
            set { SetProperty(ref _attend, value); }
        }

        public bool IsLoadBusy
        {
            get { return _isLoadBusy; }
            set
            {
                _isLoadBusy = value;
                OnPropertyChanged();
            }
        }

        public async Task PerformShimmerAsyncTask(string id)
        {

            this.Attend = new MyModel
            {
                AddressDetail = "x",
                Created = DateTime.Now,
                Activity = "x",
                Note = "x"
            };

            this.IsLoadBusy = true;
            await Task.Delay(10000);
            this.IsLoadBusy = false;

            this.Attend = new MyModel
            {
                AddressDetail = "asdasdasda",
                Created = DateTime.Now,
                Activity = "sadasdasdasfacf",
                Note = "asuuusfasfa"
            };
        }

    }

    public class MyModel 
    {
        public string AddressDetail { get; set; }
        public DateTime Created { get; set; }
        public string Activity { get; set; }
        public string Note { get; set; }
    }

    public class BaseViewModel : INotifyPropertyChanged
    {
        bool isBusy = false;

        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
