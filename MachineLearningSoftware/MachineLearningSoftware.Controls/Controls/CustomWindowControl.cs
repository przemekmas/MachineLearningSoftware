using MachineLearningSoftware.Controls.Entities;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MachineLearningSoftware.Controls
{
    [TemplatePart(Name = "PART_Header", Type = typeof(Grid))]
    [TemplatePart(Name = "PART_MinimiseWindowButton", Type = typeof(Button))]
    [TemplatePart(Name = "PART_MaximiseWindowButton", Type = typeof(Button))]
    [TemplatePart(Name = "PART_ResizeButton", Type = typeof(Button))]
    public class CustomWindowControl : Window
    {
        private bool _isWindowMaximised;
        private double _windowHeight;
        private double _windowWidth;
        private double _windowLeft;
        private double _windowTop;

        public static readonly DependencyProperty ExitWindowCommandProperty =
               DependencyProperty.Register(nameof(ExitWindowCommand), typeof(ICommand), typeof(CustomWindowControl), new UIPropertyMetadata(null));

        public static readonly DependencyProperty CanResizeWindowProperty =
               DependencyProperty.Register(nameof(CanResizeWindow), typeof(bool), typeof(CustomWindowControl), new UIPropertyMetadata(null));

        public static readonly DependencyProperty WindowTitleProperty =
               DependencyProperty.Register(nameof(WindowTitle), typeof(string), typeof(CustomWindowControl), new UIPropertyMetadata(null));

        public static readonly DependencyProperty IsMinimiseButtonVisibleProperty =
               DependencyProperty.Register(nameof(IsMinimiseButtonVisible), typeof(bool), typeof(CustomWindowControl), new UIPropertyMetadata(null));

        public static readonly DependencyProperty IsMaximiseButtonVisibleProperty =
               DependencyProperty.Register(nameof(IsMaximiseButtonVisible), typeof(bool), typeof(CustomWindowControl), new UIPropertyMetadata(null));

        public static readonly DependencyProperty IsCloseButtonVisibleProperty =
               DependencyProperty.Register(nameof(IsCloseButtonVisible), typeof(bool), typeof(CustomWindowControl), new UIPropertyMetadata(null));

        public static readonly DependencyProperty HeaderContentProperty =
               DependencyProperty.Register(nameof(HeaderContent), typeof(object), typeof(CustomWindowControl), new UIPropertyMetadata(null));

        public ICommand ExitWindowCommand
        {
            get { return (ICommand)GetValue(ExitWindowCommandProperty); }
            set { SetValue(ExitWindowCommandProperty, value); }
        }

        public bool CanResizeWindow
        {
            get { return (bool)GetValue(CanResizeWindowProperty); }
            set { SetValue(CanResizeWindowProperty, value); }
        }

        public string WindowTitle
        {
            get { return GetValue(WindowTitleProperty).ToString(); }
            set { SetValue(WindowTitleProperty, value); }
        }

        public bool IsMinimiseButtonVisible
        {
            get { return (bool)GetValue(IsMinimiseButtonVisibleProperty); }
            set { SetValue(IsMinimiseButtonVisibleProperty, value); }
        }

        public bool IsMaximiseButtonVisible
        {
            get { return (bool)GetValue(IsMaximiseButtonVisibleProperty); }
            set { SetValue(IsMaximiseButtonVisibleProperty, value); }
        }

        public bool IsCloseButtonVisible
        {
            get { return (bool)GetValue(IsCloseButtonVisibleProperty); }
            set { SetValue(IsCloseButtonVisibleProperty, value); }
        }

        public object HeaderContent
        {
            get { return GetValue(HeaderContentProperty); }
            set { SetValue(HeaderContentProperty, value); }
        }

        public ICommand ExitDialogCommand
        {
            get { return new CommandDelegate(CloseWindowButtonCommand, (_) => true); }
        }

        static CustomWindowControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomWindowControl), new FrameworkPropertyMetadata(typeof(CustomWindowControl)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var minimiseButton = (Button)Template.FindName("PART_MinimiseWindowButton", this);
            var maximiseButton = (Button)Template.FindName("PART_MaximiseWindowButton", this);
            var resizeButton = (Button)Template.FindName("PART_ResizeButton", this);
            var windowHeader = (Grid)Template.FindName("PART_Header", this);
            windowHeader.MouseLeftButtonDown += new MouseButtonEventHandler(HeaderLeftMouseButtonDown);
            minimiseButton.Click += new RoutedEventHandler(OnMinimiseButtonClick);
            maximiseButton.Click += new RoutedEventHandler(OnMaximiseWindowClick);
            resizeButton.PreviewMouseMove += new MouseEventHandler(OnResizeButtonMouseMove);
            ExitWindowCommand = ExitDialogCommand;
            Title = WindowTitle;
        }

        private void CloseWindowButtonCommand(object parameter)
        {
            Close();
        }

        private void OnResizeButtonMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var mousePosX = Mouse.GetPosition(this).X;
                var mousePosY = Mouse.GetPosition(this).Y;
                if (mousePosX > 0)
                {
                    Width = Mouse.GetPosition(this).X;
                }
                if (mousePosY > 0)
                {
                    Height = Mouse.GetPosition(this).Y;
                }
            }
        }

        private void OnMaximiseWindowClick(object sender, RoutedEventArgs e)
        {
            if (!_isWindowMaximised)
            {
                MaximiseWindow();
            }
            else
            {
                RestoreWindow();
            }
        }

        private void RestoreWindow()
        {
            Width = _windowWidth;
            Height = _windowHeight;
            Left = _windowLeft;
            Top = _windowTop;
            _isWindowMaximised = false;
        }

        private void MaximiseWindow()
        {
            SaveWindowDimensions(Height, Width, Left, Top);
            Width = SystemParameters.WorkArea.Width;
            Height = SystemParameters.WorkArea.Height;
            Left = 0;
            Top = 0;
            _isWindowMaximised = true;
        }

        private void SaveWindowDimensions(double height, double width, double left, double top)
        {
            _windowHeight = height;
            _windowWidth = width;
            _windowLeft = left;
            _windowTop = top;
        }

        private void OnMinimiseButtonClick(object sender, RoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void HeaderLeftMouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }
    }
}
