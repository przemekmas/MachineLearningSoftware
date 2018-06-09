using System.Diagnostics;

namespace MachineLearningSoftware.Common
{
    public static class HyperlinkNavigation
    {
        public static void NavigateTo(string website)
        {
            Process.Start(website);
        }
    }
}
