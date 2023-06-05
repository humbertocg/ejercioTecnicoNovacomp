using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ejercioTecnico1.ViewModels;
using Xamarin.Forms;

namespace ejercioTecnico1
{
    public partial class MainPage : ContentPage
    {
        private MainPageVM _mainPageVM;
        public MainPage()
        {
            InitializeComponent();
            var mainPageVM = new MainPageVM();
            this.BindingContext = mainPageVM;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            (this.BindingContext  as MainPageVM)?.OnViewAppearing();
        }
    }
}

