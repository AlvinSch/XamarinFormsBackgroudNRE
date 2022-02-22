using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinFormsBackgroudNRE
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
           
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            labelWithBackground.Background = null;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Collect();
        }

        public static void Collect()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage2());
        }
    }
}
