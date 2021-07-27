using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("FontAwesomeSolid.otf", Alias = "FontAwesomeSolid")]
namespace Xam.Views.Pagination
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginationView : ContentView, INotifyPropertyChanged
    {
        public IAsyncValueCommand MoveToFirstPageCommand { get; set; }
        public IAsyncValueCommand MoveToPreviousPageCommand { get; set; }
        public IAsyncValueCommand MoveToNextPageCommand { get; set; }
        public IAsyncValueCommand MoveToLastPageCommand { get; set; }

        public PaginationView()
        {
            InitializeComponent();
            MoveToFirstPageCommand = new AsyncValueCommand(() => GetFirstPageData(), allowsMultipleExecutions: false);
            MoveToPreviousPageCommand = new AsyncValueCommand(() => GetPreviousPageData(), allowsMultipleExecutions: false);
            MoveToNextPageCommand = new AsyncValueCommand(() => GetNextPageData(), allowsMultipleExecutions: false);
            MoveToLastPageCommand = new AsyncValueCommand(() => GetLastPageData(), allowsMultipleExecutions: false);
            BindingContext = this;
        }

        public static readonly BindableProperty CurrentPageProperty = BindableProperty.Create(nameof(CurrentPage), typeof(int), typeof(PaginationView), defaultValue: 1, propertyChanged: CurrentPagePropertyChanged);

        public static readonly BindableProperty PageCountProperty = BindableProperty.Create(nameof(PageCount), typeof(int), typeof(PaginationView), defaultValue: 1, propertyChanged: PageCountPropertyChanged);


        public static readonly BindableProperty DisabledColorProperty = BindableProperty.Create(nameof(DisabledColor), typeof(Color), typeof(PaginationView), defaultValue: Color.Gray, propertyChanged: OnDisabledColorPropertyChanged);


        public static readonly BindableProperty IconBackgroundColorProperty = BindableProperty.Create(nameof(IconBackgroundColor), typeof(Color), typeof(PaginationView), defaultValue: Color.Green, propertyChanged: IconBackgroundColorPropertyChanged);


        public static readonly BindableProperty OnPaginatedProperty = BindableProperty.Create(nameof(OnPaginated), typeof(IAsyncCommand<int>), typeof(PaginationView));


        private static void OnDisabledColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((PaginationView)bindable).SetPageNavigationValues();
        }
        private static void IconBackgroundColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((PaginationView)bindable).SetPageNavigationValues();
        }

        private static void PageCountPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((PaginationView)bindable).SetPageNavigationValues();
        }

        private static void CurrentPagePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((PaginationView)bindable).SetPageNavigationValues();
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

        private Color _firstPageForegroundColor;
        public Color FirstPageButtonForegroundColor
        {
            get => _firstPageForegroundColor;
            set
            {
                _firstPageForegroundColor = value;
                OnPropertyChanged(nameof(FirstPageButtonForegroundColor));
            }
        }
        private Color _previousPageForegroundColor;
        public Color PreviousPageButtonForegroundColor
        {
            get => _previousPageForegroundColor;
            set
            {
                _previousPageForegroundColor = value;
                OnPropertyChanged(nameof(PreviousPageButtonForegroundColor));
            }
        }

        private Color _nextPageForegroundColor;
        public Color NextPageButtonForegroundColor
        {
            get => _nextPageForegroundColor;
            set
            {
                _nextPageForegroundColor = value;
                OnPropertyChanged(nameof(NextPageButtonForegroundColor));
            }
        }
        private Color _lastPageForegroundColor;
        public Color LastPageButtonForegroundColor
        {
            get => _lastPageForegroundColor;
            set
            {
                _lastPageForegroundColor = value;
                OnPropertyChanged(nameof(LastPageButtonForegroundColor));
            }
        }

        private bool _allowPreviousPageNavigation;
        public bool AllowPreviousPageNavigation
        {
            get => _allowPreviousPageNavigation;
            set
            {
                _allowPreviousPageNavigation = value;
                PreviousPageButtonForegroundColor = value ? IconBackgroundColor : DisabledColor;
                OnPropertyChanged(nameof(AllowPreviousPageNavigation));
            }
        }
        private bool _allowFirstPageNavigation;
        public bool AllowFirstPageNavigation
        {
            get => _allowFirstPageNavigation;
            set
            {
                _allowFirstPageNavigation = value;

                FirstPageButtonForegroundColor = value ? IconBackgroundColor : DisabledColor;
                OnPropertyChanged(nameof(AllowFirstPageNavigation));
            }
        }

        private bool _allowNextPageNavigation;
        public bool AllowNextPageNavigation
        {
            get => _allowNextPageNavigation;
            set
            {
                _allowNextPageNavigation = value;
                NextPageButtonForegroundColor = value ? IconBackgroundColor : DisabledColor;
                OnPropertyChanged(nameof(AllowNextPageNavigation));
            }
        }
        private bool _allowLastPageNavigation;
        public bool AllowLastPageNavigation
        {
            get => _allowLastPageNavigation;
            set
            {
                _allowLastPageNavigation = value;
                LastPageButtonForegroundColor = value ? IconBackgroundColor : DisabledColor;
                OnPropertyChanged(nameof(AllowLastPageNavigation));
            }
        }

        private void SetPageNavigationValues()
        {
            AllowFirstPageNavigation = true;
            AllowNextPageNavigation = true;
            AllowLastPageNavigation = true;
            AllowPreviousPageNavigation = true;
            if (CurrentPage == 1)
            {
                AllowPreviousPageNavigation = false;
                AllowFirstPageNavigation = false;
            }
            if (CurrentPage == PageCount)
            {
                AllowNextPageNavigation = false;
                AllowLastPageNavigation = false;
            }
        }

        private async ValueTask GetLastPageData()
        {
            if (CurrentPage == PageCount)
                return;
            CurrentPage = PageCount;
            SetPageNavigationValues();
            await ExecuteCommand();
        }

        private async ValueTask GetNextPageData()
        {
            if (CurrentPage == PageCount)
                return;
            CurrentPage += 1;
            SetPageNavigationValues();
            await ExecuteCommand();
        }

        private async ValueTask GetPreviousPageData()
        {
            if (CurrentPage == 1)
                return;
            CurrentPage -= 1;
            SetPageNavigationValues();
            await ExecuteCommand();
        }

        private async ValueTask GetFirstPageData()
        {
            if (CurrentPage == 1)
                return;
            CurrentPage = 1;
            SetPageNavigationValues();
            await ExecuteCommand();
        }

        private async Task ExecuteCommand()
        {
            if (OnPaginated != null)
                await OnPaginated.ExecuteAsync(CurrentPage);
        }
    }
}