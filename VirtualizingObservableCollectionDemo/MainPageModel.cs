// <copyright file="MainPageModel.cs" company = "Microsec Ltd">
// Copyright Microsec Ltd.
// </copyright>
using System;
using AlphaChiTech.Virtualization;
using FreshMvvm;
using PropertyChanged;
using Xamarin.Forms;

namespace VirtualizingObservableCollectionDemo
{
    /// <summary>
    /// The PageModel for MainPage
    /// </summary>
    /// <remarks>
    /// We are using FreshMvvm to give a simple MVVM implementation, and Fody to implement the INotifyPropertyChanged interface for the PageModel
    /// </remarks>
    [ImplementPropertyChanged]
    public class MainPageModel: FreshBasePageModel
    {
        //#region Constructors
        //public MainPageModel ()
        //{
        //}
        //#endregion Constructors

        #region Private Fields
        /// <summary>
        /// The source for the Virtualizing Observable Collection
        /// </summary>
        private PagedEmployeesDatasource Source;
        #endregion Private Fields

        #region Properties


        /// <summary>
        /// An ObservableCollection to which the ListView is bound. It is, of course, a 
        /// Virtualizing Observable Collection - that's the whole point of this demo project!
        /// </summary>
        private VirtualizingObservableCollection<Employee> _Employees = null;

        public VirtualizingObservableCollection<Employee> Employees {
            get {
                int MaxPages = 3;
                int PageSize = 100;
                // Initialise the backing field and the source if it is not already initialised
                if (_Employees == null) {
                    // Create a new source, passing in the number of records that it will serve up to us (in reality, this will be fed to us
                    // from a database or other real datasource
                    Source = new PagedEmployeesDatasource (10000);
                    // Create a new VirtualizingObservableCollection of Employees, passing in the Source, the number of pages to
                    // hold in memory, and the number of records per page
                    _Employees =new VirtualizingObservableCollection<Employee> (
                            new PaginationManager<Employee> (Source,
                                                             maxPages: MaxPages,
                                                             pageSize: PageSize));

                    // Update the count property so that it is displayed on the Page
                    this.Count = Source.Count;

                    // Display an initial message
                    this.Message = String.Format("Fetching first page of {0} records",PageSize);
                }

                return _Employees;
            }
        }

        // The number of Employees in the database
        public int Count {
            get;
            set;
        }

        // The message to display at the bottom of the screen
        public String Message {
            get;
            set;
        }

        #endregion Properties

        #region Overrides
        // This override is provided by FreshMvvm to allow us to do stuff when the page is appearing
        protected override void ViewIsAppearing (object sender, EventArgs e)
        {
            base.ViewIsAppearing (sender, e);

            // Register the event handler for the data source's FetchEvent
            // We're only using this event so that we can update the UI with messages as the data source fetches pages
            PagedEmployeesDatasource.FetchEvent += PagedEmployeesDatasource_FetchEvent;

        }

        // This override is provided by FreshMvvm to allow us to do stuff when the page is disappearing
        protected override void ViewIsDisappearing (object sender, EventArgs e)
        {
            base.ViewIsDisappearing (sender, e);

            // De-register the FetchEvent handler to avoid memory leaks
            PagedEmployeesDatasource.FetchEvent -= PagedEmployeesDatasource_FetchEvent;
        }
        #endregion Overrides

        #region Event Handlers
        // Raised when the Data Source fetches data from the database - we're just using this to display a message on the screen
        void PagedEmployeesDatasource_FetchEvent (object sender, PagedEmployeesDataSourceFetchEventArgs e)
        {

            // This might be occurring on non-UI thread, so invoke it on the main thread to ensure the UI is updated
            Device.BeginInvokeOnMainThread (() => {
                // Update the bound message property with the message
                this.Message = string.Format ("Fetching page starting at {0}", e.Count);
            });
        }
        #endregion Event Handlers
    }
}

