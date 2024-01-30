using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EOL_GND
{
    internal class program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // File association.
            string file = null;
            var args = Environment.GetCommandLineArgs();
            if (args != null && args.Length > 1)
            {
                file = args[1];
            }

            var dialog = new EOL_GND.View.SequenceForm(file, null, false, 0, 0, 0);
            dialog.StartPosition = FormStartPosition.CenterScreen;
            Application.Run(dialog);
        }
    }
}
