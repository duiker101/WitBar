using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    [ComVisible(true)]
    [Guid("FB17B6DA-E3D7-4D17-9E43-3416983372A8")]
    [CSDeskBand.CSDeskBandRegistration(Name = "WitBar", ShowDeskBand = false)]
    public class Deskband : CSDeskBand.CSDeskBandWin
    {
        private static Control _control;

        public Deskband()
        {
            Options.MinHorizontalSize = new Size(100, 30);
            _control = new MainPanel();
        }

        protected override Control Control => _control;
    }
}
