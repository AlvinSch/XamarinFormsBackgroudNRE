using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinFormsBackgroudNRE
{
    public partial class App : Application
    {
        private readonly string KEY_NAVI_MAIN = "NavigationStack.Main";

        private readonly string KEY_NAVI_MAIN_SIZE = "size";

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }


        protected override void OnStart()
        {
            base.OnStart();
            //await RestoreNavigationStack();
        }
        protected override void OnSleep()
        {
            //SaveNavigationStack();
            base.OnSleep();
        }
        protected override void OnResume()
        {
            base.OnResume();
        }

        private async Task RestoreNavigationStack()
        {
            var navigationPage = this.MainPage as NavigationPage;
            var navigation = navigationPage.Navigation;

            // 1. Loading stored states and instanciating pages
            if (Application.Current.Properties.TryGetValue($"{KEY_NAVI_MAIN}.{KEY_NAVI_MAIN_SIZE}", out object propertyNaviSize))
            {
                var naviSize = (int)propertyNaviSize;
                var pages = new List<Page>(naviSize);
                for (int i = 0; i < naviSize; i++)
                {
                    if (Application.Current.Properties.TryGetValue($"{KEY_NAVI_MAIN}.{i}", out object propertyNaviType))
                    {
                        var pagetype = propertyNaviType as string;
                        pages.Add(Activator.CreateInstance(Type.GetType(pagetype)) as Page);
                    }
                }

                if (pages.Count() > 1) // Only if we have stack to restore
                {
                    var initialPages = navigation.NavigationStack.ToList();

                    // 2. Pushing pages into existing navigation stack
                    var last = pages.LastOrDefault();
                    pages.RemoveAt(pages.Count - 1);
                    await navigation.PushAsync(last, false); //not animated
                    foreach (var page in pages)
                    {
                        navigation.InsertPageBefore(page, last);
                    }

                    // 3. Removing existing pages that was present before restoration
                    foreach (var page in initialPages)
                    {
                        navigation.RemovePage(page);
                    }
                }
            }
        }

        private void SaveNavigationStack()
        {
            var navigationPage = this.MainPage as NavigationPage;

            var pages = navigationPage.Navigation.NavigationStack;
            var pageTypes = pages.Select((p) => p.GetType());
            var naviSize = pageTypes.Count();

            for (int i = 0; i < naviSize; i++)
            {
                var page = pages[i];
                Application.Current.Properties[$"{KEY_NAVI_MAIN}.{i}"] = page.GetType().AssemblyQualifiedName;
            }
            Application.Current.Properties[$"{KEY_NAVI_MAIN}.{KEY_NAVI_MAIN_SIZE}"] = naviSize;
        }


    }
}
