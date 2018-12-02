using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MachineLearningSoftware.Controls
{
    [TemplatePart(Name = "PART_SearchTextBox", Type = typeof(TextBox))]
    [TemplatePart(Name = "PART_SearchButton", Type = typeof(Button))]
    public class SearchControl : Control
    {
        private TextBox _searchTextBox;

        public static readonly DependencyProperty SearchActionProperty =
           DependencyProperty.Register(nameof(SearchAction), typeof(Action<string>), typeof(SearchControl), new UIPropertyMetadata(null));

        public Action<string> SearchAction
        {
            get { return (Action<string>)GetValue(SearchActionProperty); }
            set { SetValue(SearchActionProperty, value); }
        }

        static SearchControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SearchControl), new FrameworkPropertyMetadata(typeof(SearchControl)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _searchTextBox = (TextBox)Template.FindName("PART_SearchTextBox", this);
            var searchButton = (Button)Template.FindName("PART_SearchButton", this);

            searchButton.Click += new RoutedEventHandler(OnButtonClick);
            _searchTextBox.KeyDown += new KeyEventHandler(OnEnter);
            _searchTextBox.GotFocus += new RoutedEventHandler(OnSearchBoxFocus);
            _searchTextBox.LostFocus += new RoutedEventHandler(OnSearchBoxLostFocus);
            SetDefaultWaterMark();
        }

        private void OnSearchBoxLostFocus(object sender, RoutedEventArgs e)
        {
            SetDefaultWaterMark();
        }

        private void SetDefaultWaterMark()
        {
            _searchTextBox.Foreground = new SolidColorBrush(Color.FromRgb(80, 80, 80));
            _searchTextBox.Text = string.Equals(_searchTextBox.Text, "Search", StringComparison.OrdinalIgnoreCase) 
                || string.Equals(_searchTextBox.Text, string.Empty, StringComparison.OrdinalIgnoreCase) ? "Search" : _searchTextBox.Text;
        }

        private void OnSearchBoxFocus(object sender, RoutedEventArgs e)
        {
            _searchTextBox.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            _searchTextBox.Text = string.Equals(_searchTextBox.Text, "Search", StringComparison.OrdinalIgnoreCase) ? string.Empty : _searchTextBox.Text;
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            SearchAction.Invoke(_searchTextBox.Text);
        }

        private void OnEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SearchAction.Invoke(_searchTextBox.Text);
            }
        }

    }
}