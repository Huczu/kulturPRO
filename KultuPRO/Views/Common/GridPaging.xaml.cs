// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Bursatec" file="GridPaging.xaml.cs">
//   JAGG
// </copyright>
// <summary>
// Interaction logic for GridPaging.xaml.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace KulturPRO.Views.Common
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public partial class GridPaging
    {
        public static readonly DependencyProperty TotalCountProperty;

        public static readonly DependencyProperty PageIndexProperty;

        public static readonly DependencyProperty PageSizeProperty;

        public static readonly DependencyProperty ChangedIndexCommandProperty;

        public int TotalCount
        {
            get
            {
                return (int)GetValue(GridPaging.TotalCountProperty);
            }

            set
            {
                SetValue(GridPaging.TotalCountProperty, value);
            }
        }

        public int PageIndex
        {
            get
            {
                return (int)GetValue(GridPaging.PageIndexProperty);
            }

            set
            {
                SetValue(GridPaging.PageIndexProperty, value);
            }
        }

        public int PageSize
        {
            get
            {
                return (int)GetValue(GridPaging.PageSizeProperty);
            }

            set
            {
                SetValue(GridPaging.PageSizeProperty, value);
            }
        }

        static GridPaging()
        {
            UIPropertyMetadata md = new UIPropertyMetadata(0, PropertyTotalCountChanged);
            GridPaging.TotalCountProperty = DependencyProperty.Register("TotalCount", typeof(int), typeof(GridPaging), md);
            UIPropertyMetadata md1 = new UIPropertyMetadata(0, PropertyPageIndexChanged);
            GridPaging.PageIndexProperty = DependencyProperty.Register("PageIndex", typeof(int), typeof(GridPaging), md1);
            UIPropertyMetadata md2 = new UIPropertyMetadata(0, PropertyPageSizeChanged);
            GridPaging.PageSizeProperty = DependencyProperty.Register("PageSize", typeof(int), typeof(GridPaging), md2);

            ChangedIndexCommandProperty =
                DependencyProperty.Register("ChangedIndexCommand", typeof(ICommand), typeof(GridPaging), new UIPropertyMetadata(null));
        }

        public GridPaging()
        {
            InitializeComponent();
            this.TotalCount = 0;
            this.PageIndex = 1;
            this.cbPageSize.SelectedIndex = 1; // Default 100
            this.IsControlVisible = true;
            this.HasNextPage = false;
            this.HasPreviousPage = false;
        }

        public ICommand ChangedIndexCommand
        {
            get { return (ICommand)GetValue(ChangedIndexCommandProperty); }
            set { SetValue(ChangedIndexCommandProperty, value); }
        }

        public bool IsControlVisible
        {
            get { return this.Visibility == Visibility.Visible; }
            set { this.Visibility = value ? Visibility.Visible : Visibility.Collapsed; }
        }

        public int TotalPages
        {
            get
            {
                if (this.PageSize > 0)
                {
                    var tc = this.TotalCount / this.PageSize;
                    tc = tc * this.PageSize < this.TotalCount ? tc + 1 : tc;
                    return tc;
                }

                return 1;
            }
        }

        public bool HasPreviousPage
        {
            get { return btnFirst.IsEnabled; }
            internal set { btnFirst.IsEnabled = btnPrevious.IsEnabled = value; }
        }

        public bool HasNextPage
        {
            get { return btnLast.IsEnabled; }
            internal set { btnLast.IsEnabled = btnNext.IsEnabled = value; }
        }

        public void ResetPageIndex()
        {
            this.PageIndex = 1;
        }

        private static void ConfigureInternalValues(GridPaging gp)
        {
            gp.lTotalPages.Content = gp.TotalPages;

            foreach (ComboBoxItem comboBoxItem in gp.cbPageSize.Items)
            {
                int cbi = Convert.ToInt32(comboBoxItem.Content);
                if (cbi == gp.PageSize)
                {
                    gp.cbPageSize.SelectedItem = comboBoxItem;
                    break;
                }
            }

            ComboBoxItem sel = (ComboBoxItem)gp.cbPageSize.SelectedItem;
            gp.PageSize = Convert.ToInt32(sel.Content);

            gp.ButtonGrid.Visibility = gp.TotalCount > gp.PageSize ?
                Visibility.Visible :
                Visibility.Hidden;

            gp.HasPreviousPage = gp.PageIndex > 1;
            gp.HasNextPage = gp.TotalPages > gp.PageIndex;
        }

        private void ExecuteCommandChangeIndex()
        {
            if (this.ChangedIndexCommand != null)
            {
                this.ChangedIndexCommand.Execute(null);
            }
        }

        private static void PropertyPageSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            GridPaging gp = (GridPaging)d;

            ConfigureInternalValues(gp);
            gp.PageIndex = 1;
        }

        private static void PropertyPageIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            GridPaging gp = (GridPaging)d;
            int actualPage = (int)e.NewValue;
            gp.lPage.Content = actualPage;
            ConfigureInternalValues(gp);
        }

        private static void PropertyTotalCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            GridPaging gp = (GridPaging)d;
            gp.lTotal.Content = e.NewValue;
            ConfigureInternalValues(gp);
        }

        private void ComboBoxSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var items = e.AddedItems;
            if (items != null && items.Count > 0)
            {
                var value = ((ComboBoxItem)items[0]).Content;
                this.PageSize = Convert.ToInt32(value);
                if (this.TotalCount > 0)
                {
                    this.ExecuteCommandChangeIndex();
                }
            }
        }

        private void BtnNextClick(object sender, RoutedEventArgs e)
        {
            if (this.PageIndex < this.TotalPages)
            {
                this.PageIndex++;
                this.ExecuteCommandChangeIndex();
            }
        }

        private void BtnLastClick(object sender, RoutedEventArgs e)
        {
            int page = this.TotalPages;
            this.PageIndex = page;
            this.ExecuteCommandChangeIndex();
        }

        private void BtnPreviousClick(object sender, RoutedEventArgs e)
        {
            if (this.PageIndex > 1)
            {
                this.PageIndex--;
                this.ExecuteCommandChangeIndex();
            }
        }

        private void BtnFirstClick(object sender, RoutedEventArgs e)
        {
            const int Page = 1;
            this.PageIndex = Page;
            this.ExecuteCommandChangeIndex();
        }
    }
}
