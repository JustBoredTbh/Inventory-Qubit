using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using Microsoft.UI.Windowing;
using System;

using Windows.Foundation;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.Foundation.Collections;
using Windows.Networking.Proximity;
using System.Drawing;
using Windows.Graphics;
using Windows.UI.ViewManagement;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Npgsql;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Inventory_Qubit
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            base.OnLaunched(args);
            if (AesOperation.ReadFromFile(LoginWindow.Directory) != "false")
            {
                var ConnectionString = AesOperation.ReadFromFile("/InternalData/UserData.iqs");
                if (ConnectionString != null)
                {
                    if (DbOperations.ConnectToDb(ConnectionString) == true)
                    {
                        if(DbOperations.GetCurrentUserRole() == "Admin")
                        {
                            m_window = new IQWindow();
                            // Create a Frame to act as the navigation context and navigate to the first page
                            Frame rootFrame = new Frame();
                            m_window.Activate();
                        }
                        else
                        {
                            m_window = new BranchWindow();
                            // Create a Frame to act as the navigation context and navigate to the first page
                            Frame rootFrame = new Frame();
                            m_window.Activate();
                        }
                    }
                }
            }
            else
            {
                // Perform login and get user role
                m_window = new LoginWindow();
                Frame rootFrame = new Frame();
                m_window.Activate();
                m_window.ExtendsContentIntoTitleBar = true;
                Windows.Graphics.SizeInt32 size = new SizeInt32(400, 700);
                m_window.AppWindow.Resize(size);
            }
        }


        private Window m_window;
    }
}
