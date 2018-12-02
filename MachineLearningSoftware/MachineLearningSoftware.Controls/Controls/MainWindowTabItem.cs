using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MachineLearningSoftware.Controls
{
    [TemplatePart(Name = "PART_CloseButton", Type = typeof(Button))]    
    public class MainWindowTabItem : TabItem
    {
        private Button _closeButton;

        public static readonly DependencyProperty TitleProperty =
               DependencyProperty.Register(nameof(Title), typeof(string), typeof(MainWindowTabItem), new UIPropertyMetadata(null));


        public static readonly DependencyProperty IsClosableProperty =
               DependencyProperty.Register(nameof(IsClosable), typeof(bool), typeof(MainWindowTabItem), new UIPropertyMetadata(null));

        public string Title
        {
            get { return GetValue(TitleProperty).ToString(); }
            set { SetValue(TitleProperty, value); }
        }

        public bool IsClosable
        {
            get { return (bool)GetValue(IsClosableProperty); }
            set { SetValue(IsClosableProperty, value); }
        }

        static MainWindowTabItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MainWindowTabItem), new FrameworkPropertyMetadata(typeof(MainWindowTabItem)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            
            if (IsClosable)
            {
                _closeButton = (Button)Template.FindName("PART_CloseButton", this);
                MouseDown += new MouseButtonEventHandler(OnMiddleClick);
                _closeButton.Click += new RoutedEventHandler(Onclose);
            }
        }

        private void Onclose(object sender, RoutedEventArgs e)
        {
            (Parent as TabControl).Items.Remove(this);
        }

        private void OnMiddleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle &&
                e.ButtonState == MouseButtonState.Pressed)
            {
                (Parent as TabControl).Items.Remove(this);
            }
        }
    }
}
