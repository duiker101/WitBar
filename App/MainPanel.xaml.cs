using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace App
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class MainPanel : UserControl
    {
        public MainPanel()
        {
            InitializeComponent();
            this.loadScripts();
        }

        private void onAddScript(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "bat files (*.bat)|*.bat";
            if (dialog.ShowDialog() == true)
            {
                scriptsPanel.Children.Add(new ScriptControl(dialog.FileName));
                addScriptBtn.Visibility = Visibility.Hidden;
                Properties.Settings.Default.Scripts.Add(dialog.FileName);
                Properties.Settings.Default.Save();
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ManageWindow w = new ManageWindow();
            w.updateMainPanel += loadScripts;
            w.Show();
        }

        private void loadScripts()
        {
            scriptsPanel.Children.Clear();
            var scripts = Properties.Settings.Default.Scripts;
            if (scripts.Count > 0)
                addScriptBtn.Visibility = Visibility.Hidden;

            foreach (var script in scripts)
            {
                scriptsPanel.Children.Add(new ScriptControl(script));
            }
        }
    }
}
