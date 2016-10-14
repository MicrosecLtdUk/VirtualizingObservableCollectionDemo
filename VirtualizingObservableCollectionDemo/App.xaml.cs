// <copyright file="App.xaml.cs" company = "Microsec Ltd">
// Copyright Microsec Ltd.
// </copyright>
using System;
using AlphaChiTech.Virtualization;
using FreshMvvm;
using Xamarin.Forms;

namespace VirtualizingObservableCollectionDemo
{
    public partial class App : Application
    {
        public App ()
        {
            InitializeComponent ();

            // Initialise the VirtualizationManager - this is used to clean up pages when they're no longer needed
            if (!VirtualizationManager.IsInitialized) {
                VirtualizationManager.Instance.UIThreadExcecuteAction = (a) => Device.BeginInvokeOnMainThread (a);
                Device.StartTimer (
                    TimeSpan.FromSeconds (1),
                    () => {
                        VirtualizationManager.Instance.ProcessActions (); return true;
                    });
            }

            // Get our main page, and navigator
            var page = FreshPageModelResolver.ResolvePageModel<MainPageModel> ();
            var navContainer = new FreshNavigationContainer (page);
            MainPage = navContainer;
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

