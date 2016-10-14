using System;
using System.Collections.Generic;
using AlphaChiTech.Virtualization;

namespace VirtualizingObservableCollectionDemo
{
    // Event arguments class for the FetchEvent - just takes an integer count
    public class PagedEmployeesDataSourceFetchEventArgs : EventArgs
    {
        public int Count { get; set; }
    }

    /// <summary>
    /// A Data Source which provides Employees in a paged manner.
    /// </summary>
    /// <remarks>
    /// Here we're just serving up objects in memory - of course a real implementation would be
    /// more likely to be retrieving from a database.
    /// </remarks>
    public class PagedEmployeesDatasource : IPagedSourceProvider<Employee>
    {
        // An event to raise when fetching records. This is only used for this demo, so that we can
        // inform the user when a page is being fetched - it's not needed in a real-world implementation.
        public static event EventHandler<PagedEmployeesDataSourceFetchEventArgs> FetchEvent;

        // The maximum number of records to serve up (for the demo)
        private int _MaxRecords;

        #region Constructors
        // Takes the maximum number of records to serve up for the demo
        public PagedEmployeesDatasource (int MaxRecords)
        {
            _MaxRecords = MaxRecords;
        }
        #endregion Constructors

        #region IPagedSourceProvider Implementation
        // Called whenever a page of data is needed
        public PagedSourceItemsPacket<Employee> GetItemsAt (int pageoffset, int count, bool usePlaceholder)
        {

            // So that we can see when we're getting records, raise an event which can be caught by the MainPageModel
            FetchEvent?.Invoke (this, new PagedEmployeesDataSourceFetchEventArgs () { Count = pageoffset });

            // Create a list of Employees to display.
            // In a real world application, this would come from the database
            List<Employee> ItemsToReturn = new List<Employee> ();
            int MaxRecordNumberToReturn;

            // Limit the total number being returned
            if (pageoffset + count > _MaxRecords) {
                MaxRecordNumberToReturn = _MaxRecords;
            } else {
                MaxRecordNumberToReturn = pageoffset + count;
            }

            for (int i = pageoffset; i < MaxRecordNumberToReturn; i++) {
                ItemsToReturn.Add (new Employee () { Name = String.Format ("Employee {0}", i + 1), Telephone = String.Format ("07745 {0}", i) });
            }

            // Return a new packet of data
            return new PagedSourceItemsPacket<Employee> () {
                LoadedAt = DateTime.Now,
                Items = ItemsToReturn,
            };
        }

        //  Supply the maximum number of records being served - this would usually come from a database query.
        public int Count {
            get {
                return _MaxRecords;
            }
        }

        // Optional method to give the index of an item
        public int IndexOf (Employee item)
        {
            return -1;
        }

        // Callback raised when the Data Source has been reset
        public void OnReset (int count)
        {
        }
        #endregion IPagedSourceProvider Implementation
    }
}