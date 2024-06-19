using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace EcoLogTracking.Client.Components
{
    public static class JSInteropHelper
    {
        private static IJSRuntime _jsRuntime;

        public static void Initialize(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public static async Task ToggleDangerTab(bool isActive)
        {
            await _jsRuntime.InvokeVoidAsync("applyFilter", isActive);
        }
    }
}
