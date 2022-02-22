using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinFormsBackgroudNRE
{
    public partial class ErrorPage : ContentPage
    {
        public ErrorPage()
        {
            InitializeComponent();
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
    }
}
