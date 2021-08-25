using System;
using System.ComponentModel;
using Xam.Views.Pagination.ViewModels;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("FontAwesomeSolid.otf", Alias = "FontAwesomeSolid")]
namespace Xam.Views.Pagination
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginationView : ContentView, INotifyPropertyChanged
    {
        public PaginationView()
        {
            InitializeComponent();
            BindingContext = new PaginationViewModel();
        }

        public static readonly BindableProperty CurrentPageProperty = BindableProperty.Create(nameof(CurrentPage), typeof(int), typeof(PaginationView), defaultValue: 1, propertyChanged: CurrentPagePropertyChanged);

        public static readonly BindableProperty PageCountProperty = BindableProperty.Create(nameof(PageCount), typeof(int), typeof(PaginationView), defaultValue: 1, propertyChanged: PageCountPropertyChanged);


        public static readonly BindableProperty DisabledColorProperty = BindableProperty.Create(nameof(DisabledColor), typeof(Color), typeof(PaginationView), defaultValue: Color.Gray, propertyChanged: OnDisabledColorPropertyChanged);


        public static readonly BindableProperty IconBackgroundColorProperty = BindableProperty.Create(nameof(IconBackgroundColor), typeof(Color), typeof(PaginationView), defaultValue: Color.Green, propertyChanged: IconBackgroundColorPropertyChanged);


        public static readonly BindableProperty OnPaginatedProperty = BindableProperty.Create(nameof(OnPaginated), typeof(IAsyncCommand<int>), typeof(PaginationView),propertyChanged: OnPaginatedCommandChanged);

        private static void OnPaginatedCommandChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var vm = (PaginationViewModel)bindable.BindingContext;
            vm.OnPaginated = (IAsyncCommand<int>)newValue;
        }

        private static void OnDisabledColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var vm = (PaginationViewModel)bindable.BindingContext;
            vm.DisabledColor = (Color)newValue;
            vm.SetPageNavigationValues();
        }
        private static void IconBackgroundColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var vm = (PaginationViewModel)bindable.BindingContext;
            vm.IconBackgroundColor = (Color)newValue;
            vm.SetPageNavigationValues();
        }

        private static void PageCountPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var vm = (PaginationViewModel)bindable.BindingContext;
            vm.PageCount = (int)newValue;
            vm.SetPageNavigationValues();
        }

        private static void CurrentPagePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var vm = (PaginationViewModel)bindable.BindingContext;
            vm.CurrentPage = (int)newValue;
            vm.SetPageNavigationValues();
        }
        public Color DisabledColor
        {
            get => (Color)GetValue(DisabledColorProperty);
            set => SetValue(DisabledColorProperty, value);
        }

        public Color IconBackgroundColor
        {
            get => (Color)GetValue(IconBackgroundColorProperty);
            set => SetValue(IconBackgroundColorProperty, value);
        }

        public int CurrentPage
        {
            get => (int)GetValue(CurrentPageProperty);
            set => SetValue(CurrentPageProperty, value);
        }

        public int PageCount
        {
            get => (int)GetValue(PageCountProperty);
            set => SetValue(PageCountProperty, value);
        }

        public IAsyncCommand<int> OnPaginated
        {
            get => (IAsyncCommand<int>)GetValue(OnPaginatedProperty);
            set => SetValue(OnPaginatedProperty, value);
        }

    }
}