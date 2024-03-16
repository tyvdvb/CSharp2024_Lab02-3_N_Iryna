using Lab02.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lab02.Tools
{
    internal class PersonManager
    {
        internal static Person Current { get; set; }
        internal static void CloseApp()
        {
            MessageBox.Show("Close");
            Environment.Exit(1);
        }
    }
}
