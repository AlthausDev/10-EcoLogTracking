using BlazorBootstrap;
using Microsoft.JSInterop;

namespace EcoLogTracking.Client.Components
{
    public partial class ConfigPanel
    {
        public int DeleteFrecuencyDays { get; set; }
        public string UserName { get; set; } = "Default";
        public string Email { get; set; } = "Default@ecoalgo.com";


        public string newUserName { get; set; }
        public string newEmail { get; set; }
        public string newPassword { get; set; }


        record TabMessage(string Event, string ActiveTabTitle, string PreviousActiveTabTitle);

        List<TabMessage> messages = new List<TabMessage>();

        private void OnTabShowingAsync(TabsEventArgs args)
            => messages.Add(new("OnShowing", args.ActiveTabTitle, args.PreviousActiveTabTitle));

        private void OnTabShownAsync(TabsEventArgs args)
            => messages.Add(new("OnShown", args.ActiveTabTitle, args.PreviousActiveTabTitle));

        private void OnTabHidingAsync(TabsEventArgs args)
            => messages.Add(new("OnHiding", args.ActiveTabTitle, args.PreviousActiveTabTitle));

        private void OnTabHiddenAsync(TabsEventArgs args)
            => messages.Add(new("OnHidden", args.ActiveTabTitle, args.PreviousActiveTabTitle));
     
    }

}
