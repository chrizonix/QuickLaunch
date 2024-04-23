using System;
using System.Reflection;
using System.Windows.Forms;

using QuickLaunch.Libraries;

namespace QuickLaunch
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Load Embedded Assemblies
            LoadEmbeddedAssemblies();

            // Set Options and Start Application 
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }

        public static void LoadEmbeddedAssemblies()
        {
            // Load Assemblies from Embedded Resources in Project
            EmbeddedAssembly.Load("QuickLaunch.Resources.AnySerializer.dll", "AnySerializer.dll");
            EmbeddedAssembly.Load("QuickLaunch.Resources.TypeSupport.dll", "TypeSupport.dll");

            // Attach Event Handler for Assembly Resolve Lookups
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
        }

        public static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            // Handle Assembly Resolve Lookups
            return EmbeddedAssembly.Get(args.Name);
        }
    }
}
