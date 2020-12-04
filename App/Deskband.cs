using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CSDeskBand;
using CSDeskBand.ContextMenu;



namespace App
{
    [ComVisible(true)]
    [Guid("AA01ACB3-6CCC-497C-9CE6-9211F2EDFC15")]
    [CSDeskBandRegistration(Name = "WitBar")]
    public class Deskband : CSDeskBandWpf
    {
        protected override UIElement UIElement => new MainPanel();

        public Deskband()
        {
            Options.ContextMenuItems = ContextMenuItems;
            Options.MinHorizontalSize = new DeskBandSize(100, -1);
        }

        private List<DeskBandMenuItem> ContextMenuItems
        {
            get
            {
                var action = new DeskBandMenuAction("Manage WitBar");

                //var separator = new DeskBandMenuSeparator();
                //var submenuAction = new DeskBandMenuAction("Submenu Action - Toggle checkmark");
                //var submenu = new DeskBandMenu("Submenu")
                //{
                //    Items = { submenuAction }
                //};
                //action.Clicked += (sender, args) => submenu.Enabled = !submenu.Enabled;
                //submenuAction.Clicked += (sender, args) => submenuAction.Checked = !submenuAction.Checked;
                //return new List<DeskBandMenuItem>() { action, separator, submenu };
                action.Clicked += (sender, args) => {
                    var window = new ManageWindow();
                    window.Show();
                };

                return new List<DeskBandMenuItem>() { action };
            }
        }
    }

}
