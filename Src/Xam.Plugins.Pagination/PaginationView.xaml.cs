using System.ComponentModel;
using Xam.Plugins.Pagination.Models;
using Xam.Plugins.Pagination.ViewModels;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("FontAwesomeSolid.otf", Alias = "FontAwesomeSolid")]
namespace Xam.Plugins.Pagination
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginationView : ContentView, INotifyPropertyChanged
    {
        const string PAGE_NUM_CHANGED_KEY = "page_number_changed";
        public PaginationView()
        {
            InitializeComponent();
            var vm = new PaginationViewModel(); ;
            BindingContext = vm;
            MessagingCenter.Subscribe<PageNumberModel>(this, PAGE_NUM_CHANGED_KEY, SubscribeToEvents);
            NumbersCollectionView.WidthRequest = 50;
        }

        public static readonly BindableProperty CurrentPageProperty = BindableProperty.Create(nameof(CurrentPage), typeof(int), typeof(PaginationView), defaultValue: 1, propertyChanged: CurrentPagePropertyChanged);

        public static readonly BindableProperty PageCountProperty = BindableProperty.Create(nameof(PageCount), typeof(int), typeof(PaginationView), defaultValue: 1, propertyChanged: PageCountPropertyChanged);


        public static readonly BindableProperty DisabledColorProperty = BindableProperty.Create(nameof(DisabledColor), typeof(Color), typeof(PaginationView), defaultValue: Color.Gray, propertyChanged: OnDisabledColorPropertyChanged);


        public static readonly BindableProperty IconBackgroundColorProperty = BindableProperty.Create(nameof(IconBackgroundColor), typeof(Color), typeof(PaginationView), defaultValue: Color.Green, propertyChanged: IconBackgroundColorPropertyChanged);


        public static readonly BindableProperty OnPaginatedProperty = BindableProperty.Create(nameof(OnPaginated), typeof(IAsyncCommand<int>), typeof(PaginationView), propertyChanged: OnPaginatedCommandChanged);

        public static readonly BindableProperty NumberNavigationEnabledProperty = BindableProperty.Create(nameof(NumberNavigationEnabled), typeof(bool), typeof(PaginationView), propertyChanged: OnNumberNavigationEnabledChanged);

        private static void OnNumberNavigationEnabledChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var vm = (PaginationViewModel)bindable.BindingContext;
            vm.NumberNavigationEnabled = (bool)newValue;
            vm.SetPageNavigationValues();
        }

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
            ((PaginationView)bindable).NumbersCollectionView.WidthRequest = ((int)newValue) * 50 + 5;
        }

        private void ScrollToCurrentPage(PageNumberModel vm)
        {
            if (vm != null)
                NumbersCollectionView.ScrollTo(vm, null, position: ScrollToPosition.Center);
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
        public bool NumberNavigationEnabled
        {
            get => (bool)GetValue(NumberNavigationEnabledProperty);
            set => SetValue(NumberNavigationEnabledProperty, value);
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

        private void SubscribeToEvents(PageNumberModel model)
        {
            MessagingCenter.Unsubscribe<PageNumberModel>(this, PAGE_NUM_CHANGED_KEY);
            this.ScrollToCurrentPage(model);
            MessagingCenter.Subscribe<PageNumberModel>(this, PAGE_NUM_CHANGED_KEY, SubscribeToEvents);
        }
    }
}