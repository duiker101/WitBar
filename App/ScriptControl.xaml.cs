using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Threading;

namespace App
{
    /// <summary>
    /// Interaction logic for ScriptControl.xaml
    /// </summary>
    public partial class ScriptControl : UserControl
    {
        private ScriptExecuter exe;
        private System.Threading.Timer aTimer;
        private delegate void SafeCallResultDelegate(RootEntry entry);

        public ScriptControl(String file)
        {
            InitializeComponent();
            this.exe = new ScriptExecuter(file);
            aTimer = new Timer(new TimerCallback(OnTimedEvent), null, 0, 6000);
        }

        private void OnTimedEvent(object stateInfo)
        {
            RootEntry root = exe.ExecuteCommand();

            this.Dispatcher.Invoke(() =>
            {
                outputLabel.ContextMenu = new ContextMenu();
                if (root.error)
                {
                    outputLabel.Content = "Error \u26A0";
                    var item = new MenuItem();
                    item.Header = root.errorMessage;
                    outputLabel.ContextMenu.Items.Add(item);
                }
                else
                {
                    outputLabel.Content = root.children[0].text;
                    this.buildMenu(outputLabel.ContextMenu.Items, root.menu);
                }
            }
            );
        }


        private void buildMenu(ItemCollection menu, List<Entry> entries)
        {

            foreach (var entry in entries)
            {
                if (entry.isSeparator)
                {
                    menu.Add(new Separator());
                    continue;
                }
                MenuItem item = new MenuItem();
                item.Header = entry.text;
                menu.Add(item);

                if (entry.children.Count > 0)
                {
                    buildMenu(item.Items, entry.children);
                }
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            aTimer.Dispose();
        }
    }
}
