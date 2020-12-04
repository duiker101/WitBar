using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    public partial class ScriptControl : UserControl
    {
        private ScriptExecuter exe;
        private System.Timers.Timer aTimer;
        private delegate void SafeCallTextDelegate(string text);
        private delegate void SafeCallMenuDelegate(List<Entry> entries);

        public ScriptControl(String file)
        {
            InitializeComponent();
            this.exe = new ScriptExecuter(file);
            aTimer = new System.Timers.Timer(60000);
            aTimer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
            this.ExecuteCommand();
        }

        private void OnTimedEvent(object sender, EventArgs e)
        {
            this.ExecuteCommand();
        }

        private void ExecuteCommand()
        {
            RootEntry root = exe.ExecuteCommand();

            WriteTextSafe(root.children[0].text);
            MakeMenuSafe(root.menu);
        }

        private void MakeMenuSafe(List<Entry> entries)
        {
            if (outputLabel.InvokeRequired)
            {
                var d = new SafeCallMenuDelegate(MakeMenuSafe);
                outputLabel.Invoke(d, new object[] { entries });
            }
            else
            {
                contextMenu.Items.Clear();
                buildMenu(contextMenu.Items, entries);
            }
        }

        private void buildMenu(ToolStripItemCollection menu, List<Entry> entries)
        {

            foreach (var entry in entries)
            {
                if (entry.isSeparator)
                {
                    menu.Add(new ToolStripSeparator());
                    continue;
                }
                ToolStripMenuItem item = new ToolStripMenuItem(entry.text);
                menu.Add(item);

                if (entry.children.Count > 0)
                {
                    buildMenu(item.DropDown.Items, entry.children);
                }

            }
        }

        private void WriteTextSafe(string text)
        {
            if (outputLabel.InvokeRequired)
            {
                var d = new SafeCallTextDelegate(WriteTextSafe);
                outputLabel.Invoke(d, new object[] { text });
            }
            else
            {
                outputLabel.Text = text;
            }
        }
    }
}
