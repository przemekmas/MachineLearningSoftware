using System.Diagnostics;

namespace ObjectRecognitionSoftware.Common
{
    public static class HyperlinkNavigation
    {
        public static void NavigateTo(string website)
        {
            Process.Start(website);
        }
    }
}
