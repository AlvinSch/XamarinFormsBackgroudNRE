# XamarinForms Background NRE Test Project

Reproducing https://github.com/xamarin/Xamarin.Forms/issues/15177

1. Open the TestApp
2. Navigate to Next Page
3. Press Button "Trigger Garbage Collection"

In this testcase the background of a label is definied in the Xaml of the ErrorPage. On Initialization the Background is set to NULL. On the following execution of the GarbageCollector the unhandled exception is reported and displayed.
