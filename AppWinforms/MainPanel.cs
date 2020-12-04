using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace App
{
    public partial class MainPanel: UserControl
    {
        public MainPanel()
        {
            InitializeComponent();
        }
        private void addNewScript()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "bat files (*.bat)|*.bat";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                scriptsPanel.Controls.Add(new ScriptControl(dialog.FileName));
                addScriptBtn.Visible = false;
            }
        }

        private void addScriptBtn_Click(object sender, EventArgs e)
        {
            this.addNewScript();
        }

        private void addScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.addNewScript();

        }
    }
}
