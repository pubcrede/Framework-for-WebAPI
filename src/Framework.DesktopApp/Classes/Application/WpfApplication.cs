//-----------------------------------------------------------------------
// <copyright file="WpfApplication.cs" company="Genesys Source">
//      Licensed to the Apache Software Foundation (ASF) under one or more 
//      contributor license agreements.  See the NOTICE file distributed with 
//      this work for additional information regarding copyright ownership.
//      The ASF licenses this file to You under the Apache License, Version 2.0 
//      (the 'License'); you may not use this file except in compliance with 
//      the License.  You may obtain a copy of the License at 
//       
//        http://www.apache.org/licenses/LICENSE-2.0 
//       
//       Unless required by applicable law or agreed to in writing, software  
//       distributed under the License is distributed on an 'AS IS' BASIS, 
//       WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  
//       See the License for the specific language governing permissions and  
//       limitations under the License. 
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using Genesys.Extensions;
using Genesys.Extras.Configuration;
using Genesys.Extras.Net;

namespace Framework.Applications
{
    /// <summary>
    /// Global application information
    /// </summary>
    public abstract class WpfApplication : System.Windows.Application
    {
        private ConfigurationManagerFull configuration = new ConfigurationManagerFull();

        /// <summary>
        /// MyWebService
        /// </summary>
        public virtual Uri MyWebService { get { return new Uri(this.configuration.AppSettingValue("MyWebService"), UriKind.RelativeOrAbsolute); } }

        /// <summary>
        /// Entry point Screen (Typically login screen)
        /// </summary>
        public abstract Uri LandingPage { get; }

        /// <summary>
        /// Home dashboard screen
        /// </summary>
        public abstract Uri HomePage { get; }

        /// <summary>
        /// Error screen
        /// </summary>
        public abstract Uri ErrorPage { get; }

        /// <summary>
        /// Returns currently active window
        /// </summary>
        public static new Window Current
        {
            get
            {
                return System.Windows.Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public WpfApplication() : base()
        {
            InitializeAsync();
            StartupUri = this.LandingPage;
        }

        /// <summary>
        /// Initializes this class with data, and allows for proper async behavior
        /// </summary>
        /// <returns></returns>f
        public virtual async Task InitializeAsync(Boolean hasServices)
        {
            if (hasServices == true) await this.WakeServicesAsync();
        }

        /// <summary>
        /// Wakes up any sleeping processes, and MyWebService chain
        /// </summary>
        /// <returns></returns>
        public virtual async Task WakeServicesAsync()
        {
            if (this.MyWebService.ToString() == TypeExtension.DefaultString)
            {
                HttpRequestGetString Request = new HttpRequestGetString(this.MyWebService.ToString());
                Request.ThrowExceptionWithEmptyReponse = false;
                await Request.SendAsync();
            }
        }

        /// <summary>
        /// Init all locally stored data
        /// </summary>
        protected abstract Task InitializeAsync();

        /// <summary>
        /// Can this screen go back
        /// </summary>
        protected bool CanGoBack { get { return RootFrame.CanGoBack; } }

        /// <summary>
        /// Navigates back to previous screen
        /// </summary>
        public virtual void GoBack() { Navigate(HomePage.ToString()); }

        /// <summary>
        /// Navigates to any screen
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="dataToPass"></param>
        public void Navigate(string destination, object dataToPass = null) { RootFrame.Navigate(destination, dataToPass); }

        /// <summary>
        /// Gets the root frame of the application
        /// </summary>
        /// <returns></returns>
        public Frame RootFrame
        {
            get
            {
                Frame returnValue = new Frame();

                if (Current.Content is Frame)
                {
                    returnValue = (Frame)Current.Content;
                }
                return returnValue;
            }
        }
    }
}
