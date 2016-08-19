// <copyright file="App.xaml.cs" company = "Microsec Ltd">
// Copyright Microsec Ltd.
// </copyright>
using Xamarin.Forms;

namespace VirtualizingObservableCollectionDemo
{
    public partial class App : Application
    {
        public App ()
        {
            InitializeComponent ();

            MainPage = new VirtualizingObservableCollectionDemoPage ();
        }

        protected override void OnStart ()
        {
            // Handle when your app starts
        }

        protected override void OnSleep ()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume ()
        {
            // Handle when your app resumes
        }
    }
}

