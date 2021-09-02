# Xam.Views.Pagination
Web-like pagination component for Xamarin Forms     
 
**Usage**   

1. Import namespace in your xaml file   

       xmlns:customViews="clr-namespace:Xam.Plugins.Pagination;assembly=Xam.Plugins.Pagination"   

2. Use CustomControl wherever required   

       <customViews:PaginationView 
                            CurrentPage="10"
                            PageCount="15" 
                            OnPaginated="{Binding Source={x:Reference Page}, Path=BindingContext.OnPaginatedCommand}"
                            IconBackgroundColor="Red"
                            DisabledColor="Gray"
                            NumberNavigationEnabled="True"/>   

**Available Bindable Properties**   

 ***CurrentPage***   
 Takes integer value and used to set page number initially.
 
 ***OnPaginated***   
 Of Type Command<int>. It is a callback function that gets trigger once pagination is performed     
 
 ***IconBackgroundColor***   
Set color of navigation button in pagination component     
  
  ***DisabledColor***     
Sets color of button when they are disabled      
 ***NumberNavigationEnabled***     
 Boolean value that represents whether to navigate using page numbers 
   
**Working Screenshot**  
![alt text][screenshot]

[screenshot]: https://github.com/solo-developer/Xamarin.Views.Pagination/blob/main/xam.plugins.pagination.gif "Xamarin Pagination"
