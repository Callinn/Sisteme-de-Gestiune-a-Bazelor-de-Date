
using LABORATOR1.ui;

namespace LABORATOR1
{
    internal static class Program
    {
   
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new UI());
        }
    }
}