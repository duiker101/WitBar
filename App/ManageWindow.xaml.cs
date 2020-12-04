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
using System.Windows.Shapes;

namespace App
{
    /// <summary>
    /// Interaction logic for ManageWindow.xaml
    /// </summary>
    public partial class ManageWindow : Window
    {

        public delegate void UpdateMainPanel();
        public event UpdateMainPanel updateMainPanel;

        public ManageWindow()
        {
            InitializeComponent();
            scriptsBox.ItemsSource = Properties.Settings.Default.Scripts.Cast<string>().ToArray();
        }
        private void updated()
        {
            Properties.Settings.Default.Save();
            scriptsBox.ItemsSource = Properties.Settings.Default.Scripts.Cast<string>().ToArray();
            if (this.updateMainPanel != null)
            {
                this.updateMainPanel();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "bat files (*.bat)|*.bat";
            if (dialog.ShowDialog() == true)
            {
                Properties.Settings.Default.Scripts.Add(dialog.FileName);
                this.updated();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (scriptsBox.SelectedIndex >= 0)
            {
                Properties.Settings.Default.Scripts.RemoveAt(scriptsBox.SelectedIndex);
                this.updated();
            }
        }
    }
}
