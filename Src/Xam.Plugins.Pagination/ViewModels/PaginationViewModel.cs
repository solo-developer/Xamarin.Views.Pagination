using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xam.Plugins.Pagination.Models;
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
        public IAsyncCommand<PageNumberModel> NavigatedThroughPageNumberCommand { get; set; }

        public PaginationViewModel()
        {
            MoveToFirstPageCommand = new AsyncValueCommand(() => MoveToFirstPage(), allowsMultipleExecutions: false);
            MoveToPreviousPageCommand = new AsyncValueCommand(() => MoveToPreviousPage(), allowsMultipleExecutions: false);
            MoveToNextPageCommand = new AsyncValueCommand(() => MoveToNextPage(), allowsMultipleExecutions: false);
            MoveToLastPageCommand = new AsyncValueCommand(() => MoveToLastPage(), allowsMultipleExecutions: false);
            NavigatedThroughPageNumberCommand = new AsyncCommand<PageNumberModel>((i) => NavigateToPageDirectly(i), allowsMultipleExecutions: false);
            DisabledColor = Color.Gray;
            PageNumbers = new ObservableRangeCollection<PageNumberModel>();
            CurrentPage = 1;
            PageCount = 1;

        }

        /// <summary>
        /// Page nubers that are available for traversal
        /// </summary>
        public ObservableRangeCollection<PageNumberModel> PageNumbers
        {
            get => GetValue<ObservableRangeCollection<PageNumberModel>>();
            set => SetValue(value);
        }
        public IAsyncCommand<int> OnPaginated { get; set; }

        public PageNumberModel SelectedPageDetail
        {
            get
            {
                return PageNumbers.FirstOrDefault(a => a.Number == CurrentPage);
            }
        }
        /// <summary>
        /// nagivation button's (forward and backward) background colour when it is disabled
        /// </summary>
        public Color DisabledColor
        {
            get => GetValue<Color>();
            set => SetValue(value);
        }

        /// <summary>
        /// nagivation button's (forward and backward) background colour when it is enabled
        /// </summary>
        public Color IconBackgroundColor
        {
            get => GetValue<Color>();
            set => SetValue(value);
        }


        /// <summary>
        /// page number we are in
        /// </summary>
        public int CurrentPage
        {
            get => GetValue<int>();
            set => SetValue(value);
        }

        /// <summary>
        /// total number of pages 
        /// </summary>
        public int PageCount
        {
            get => GetValue<int>();
            set => SetValue(value);
        }

        /// <summary>
        /// boolean to state whether to show page numbers that users can use to traverse
        /// if set to false, page number navigation is not allowed. Only page info is shown
        /// </summary>
        public bool NumberNavigationEnabled
        {
            get => GetValue<bool>();
            set => SetValue(value);
        }

        private bool _allowFirstPageNavigation;
        public bool AllowFirstPageNavigation
        {
            get => _allowFirstPageNavigation;
            set
            {
                _allowFirstPageNavigation = value;
                OnPropertyChanged(nameof(AllowFirstPageNavigation));
            }
        }

        private bool _allowPreviousPageNavigation;
        public bool AllowPreviousPageNavigation
        {
            get => _allowPreviousPageNavigation;
            set
            {
                _allowPreviousPageNavigation = value;
                OnPropertyChanged(nameof(AllowPreviousPageNavigation));
            }
        }
      

        private bool _allowNextPageNavigation;
        public bool AllowNextPageNavigation
        {
            get => _allowNextPageNavigation;
            set
            {
                _allowNextPageNavigation = value;
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
                OnPropertyChanged(nameof(AllowLastPageNavigation));
            }
        }

        /// <summary>
        /// set navigation button status and publish selected page detail to subscriber
        /// </summary>
        public void SetPageNavigationValues()
        {
            if (PageCount == 0 || CurrentPage == 0)
                return;
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
            PubishSelectedPageToConsumer();

        }

        /// <summary>
        /// publishes an event  through messagingCenter whenever page number is selected
        /// </summary>
        private void PubishSelectedPageToConsumer()
        {
            var selected = PageNumbers.FirstOrDefault(a => a.Number == CurrentPage);
            if (selected == null)
                return;
            MessagingCenter.Send<PageNumberModel>(selected, "page_number_changed");
        }

        private async ValueTask MoveToLastPage()
        {
            if (CurrentPage == PageCount)
                return;
            CurrentPage = PageCount;
            SetPageNavigationValues();
            await ExecuteCommand();
        }

        /// <summary>
        /// Used to traverse to page using page numbers whenever user selects a page number from available pages
        /// </summary>
        /// <param name="selectedPageDetail"></param>
        /// <returns></returns>
        private async Task NavigateToPageDirectly(PageNumberModel selectedPageDetail)
        {
            bool isSamePageNumberPressed = CurrentPage == selectedPageDetail.Number;
            if (isSamePageNumberPressed)
                return;
            CurrentPage = selectedPageDetail.Number;
            SetPageNavigationValues();
            await OnPaginated?.ExecuteAsync(selectedPageDetail.Number);
        }

        /// <summary>
        /// Sets available page numbers from 1 to total number of pages
        /// </summary>
        public void InitPageNumbers()
        {
            if (CurrentPage == 0 || PageCount == 0)
                return;
            SetPageNavigationValues();
            PageNumbers.Clear();
            List<PageNumberModel> pageNums = new List<PageNumberModel>();
            for (var i = 1; i <= PageCount; i++)
            {
                var model = new PageNumberModel()
                {
                    Number = i
                };
                pageNums.Add(model);
            }
            this.PageNumbers.AddRange(pageNums);          
        }

        private async ValueTask MoveToNextPage()
        {
            if (CurrentPage == PageCount)
                return;
            CurrentPage += 1;
            SetPageNavigationValues();

            await ExecuteCommand();
        }

        private async ValueTask MoveToPreviousPage()
        {
            if (CurrentPage == 1)
                return;
            CurrentPage -= 1;
            SetPageNavigationValues();
            await ExecuteCommand();
        }

        private async ValueTask MoveToFirstPage()
        {
            if (CurrentPage == 1)
                return;
            CurrentPage = 1;
            SetPageNavigationValues();
            await ExecuteCommand();
        }

        /// <summary>
        /// trigger user-defined command on paginated
        /// </summary>
        /// <returns></returns>
        private async Task ExecuteCommand()
        {
            if (OnPaginated != null)
                await OnPaginated.ExecuteAsync(CurrentPage);
        }
    }
}
