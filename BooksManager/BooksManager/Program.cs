using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BooksManager.Controllers;
using BooksManager.Services;
using BooksManager.Views;

namespace BooksManager
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var context = new ProgramContext();
            var form = new SignInForm();
            var _ = new SignInController(form, context);
            Application.Run(form);
        }
    }
}
