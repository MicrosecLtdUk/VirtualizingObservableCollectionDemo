# VirtualizingObservableCollectionDemo
This is a simple Xamarin.Forms demonstration project, showing how to use the [Virtualizing Observable Collection NuGet Package](https://www.nuget.org/packages/VirtualizingObservableCollection/), which is available on GitHub [here] (https://github.com/anagram4wander/VirtualizingObservableCollection), with Xamarin.Forms.

##Documentation
Documentation for Virtualizing Observable Collection itself is available on [Alpha Chi Technology's blog](https://alphachitech.wordpress.com/2015/01/31/virtualizing-observable-collection/)

##Implementation
### Platforms
The solution has projects for iOS and Android - no Windows I'm afraid!

### Data Source
The demo is a simple Xamarin.Forms application with a main page displaying a ListView backed by a virtualizing collection. In order to keep things simple, the data source for the collection isn't backed by a database, but just creates objects in memory.

### Supporting Frameworks
The demo uses [FreshMvvm](https://github.com/rid00z/FreshMvvm) to provide a simple MVVM implementation - I've kept this as lightweight as possible, as the main point of the project is to demonstrate the use of Virtualizing Observable Collections!

