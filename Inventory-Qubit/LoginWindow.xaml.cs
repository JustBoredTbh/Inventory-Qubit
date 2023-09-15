using System;
using Npgsql;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.UI.ViewManagement;
using Microsoft.UI;
using Microsoft.UI.Windowing;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Inventory_Qubit
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginWindow : Window
    {
        string DataServer;
        string Port;
        string Database;
        string Username;
        string password;
        public string ConnectionString;
        public static string Directory = @"UserData.iqs";
        public static string key = "e3dfd3g98a4e41332e5gfs50xsb219da";

        public LoginWindow()
        {
            this.InitializeComponent();
        }

    
    public void loadAdminWindow()
        {
            m_window = new IQWindow();
            // Create a Frame to act as the navigation context and navigate to the first page
            Frame rootFrame = new Frame();
            m_window.Activate();
        }

        public void loadBranchWindow()
        {
            m_window = new BranchWindow();
            // Create a Frame to act as the navigation context and navigate to the first page
            Frame rootFrame = new Frame();
            m_window.Activate();
        }

        private Window m_window;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            DataServer = DataserverString.Text;
            Port = PortString.Text;
            Database = DatabaseString.Text;
            Username = UsernameString.Text;
            password = passworBoxString.Password;
            ConnectionString = $"Server={DataServer};Database={Database};Port={Port};User Id ={Username};Password={password};";
            var encryptedString = AesOperation.EncryptString(key, ConnectionString);
            AesOperation.WriteToFile(encryptedString, Directory);
            if (ConnectionString != null)
            {
                if (DbOperations.ConnectToDb(ConnectionString) == true)
                {
                    if (DbOperations.GetCurrentUserRole() == "Admin")
                    {
                        loadAdminWindow();
                        this.Close();
                    }
                    else
                    {
                        loadBranchWindow();
                        this.Close();
                    }
                }
            }
        }

        private void RevealModeCheckbox_Changed(object sender, RoutedEventArgs e)
        {
            if (revealModeCheckBox.IsChecked == true)
            {
                passworBoxString.PasswordRevealMode = PasswordRevealMode.Visible;
            }
            else
            {
                passworBoxString.PasswordRevealMode = PasswordRevealMode.Hidden;
            }
        }
    }
}

