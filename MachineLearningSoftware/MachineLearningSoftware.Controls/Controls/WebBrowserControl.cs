using CefSharp;
using CefSharp.Wpf;
using System;
using System.IO;
using System.Windows;

namespace MachineLearningSoftware.Controls
{
    public class WebBrowserControl : ChromiumWebBrowser
    {
        public static readonly DependencyProperty LoadStartActionProperty =
            DependencyProperty.Register(nameof(LoadStartAction), typeof(object), typeof(WebBrowserControl), new UIPropertyMetadata(null));

        public static readonly DependencyProperty LoadEndActionProperty =
            DependencyProperty.Register(nameof(LoadEndAction), typeof(object), typeof(WebBrowserControl), new UIPropertyMetadata(null));

        public static readonly DependencyProperty LoadWebPageProperty =
            DependencyProperty.Register(nameof(LoadPage), typeof(Action<string>), typeof(WebBrowserControl), new UIPropertyMetadata(null));
        
        public object LoadStartAction
        {
            get { return GetValue(LoadStartActionProperty); }
            set { SetValue(LoadStartActionProperty, value); }
        }

        public object LoadEndAction
        {
            get { return GetValue(LoadEndActionProperty); }
            set { SetValue(LoadEndActionProperty, value); }
        }
        
        public Action<string> LoadPage
        {
            get { return (Action<string>)GetValue(LoadWebPageProperty); }
            set { SetValue(LoadWebPageProperty, value); }
        }
        
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            
            if (Template != null)
            {
                LoadError += new EventHandler<LoadErrorEventArgs>(OnError);
                FrameLoadStart += new EventHandler<FrameLoadStartEventArgs>(OnLoadStart);
                FrameLoadEnd += new EventHandler<FrameLoadEndEventArgs>(OnLoadEnd);
                LoadPage = NowLoadPage;
            }
        }

        private void NowLoadPage(string page)
        {
            Load(page);
        }

        private void OnLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            Dispatcher.Invoke(() => (LoadEndAction as Action).Invoke());
        }

        private void OnLoadStart(object sender, FrameLoadStartEventArgs e)
        {
            Dispatcher.Invoke(() => (LoadStartAction as Action).Invoke());
        }

        private void OnError(object sender, LoadErrorEventArgs e)
        {
            Load($"{Directory.GetCurrentDirectory()}\\Assets\\WebPages\\FailedLoad\\LoadFail.html");
        }
    }
}
