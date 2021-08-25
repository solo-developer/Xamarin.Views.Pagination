using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace Xam.Plugins.Pagination.ViewModels
{
    public class PaginationViewModel : ViewModelBase
    {
        public IAsyncValueCommand MoveToFirstPageCommand { get; set; }
        public IAsyncValueCommand MoveToPreviousPageCommand { get; set; }
        public IAsyncValueCommand MoveToNextPageCommand { get; set; }
        public IAsyncValueCommand MoveToLastPageCommand { get; set; }

        public PaginationViewModel()
        {
            MoveToFirstPageCommand = new AsyncValueCommand(() => GetFirstPageData(), allowsMultipleExecutions: false);
            MoveToPreviousPageCommand = new AsyncValueCommand(() => GetPreviousPageData(), allowsMultipleExecutions: false);
            MoveToNextPageCommand = new AsyncValueCommand(() => GetNextPageData(), allowsMultipleExecutions: false);
            MoveToLastPageCommand = new AsyncValueCommand(() => GetLastPageData(), allowsMultipleExecutions: false);
            DisabledColor = Color.Gray;

        }
        public IAsyncCommand<int> OnPaginated { get; set; }

        public Color DisabledColor
        {
            get => GetValue<Color>();
            set => SetValue(value);
        }

        public Color IconBackgroundColor
        {
            get => GetValue<Color>();
            set => SetValue(value);
        }

        public int CurrentPage
        {
            get => GetValue<int>();
            set => SetValue(value);
        }

        public int PageCount
        {
            get => GetValue<int>();
            set => SetValue(value);
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

        public void SetPageNavigationValues()
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
