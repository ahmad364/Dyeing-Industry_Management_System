using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Industrial_Mangement_System
{
    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Progress_Bar_Form obj = new Progress_Bar_Form();
            obj.ShowDialog();

            if(obj.AppRunSuccessfully==true)
            Application.Run(new MenuForm());
        }
    }
}
