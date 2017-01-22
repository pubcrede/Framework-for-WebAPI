//-----------------------------------------------------------------------
// <copyright file="UniversalApplication.cs" company="Genesys Source">
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
using Framework.Themes;
using Genesys.Extensions;
using Genesys.Extras.Configuration;
using Genesys.Extras.Net;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Framework.Applications
{
    /// <summary>
    /// Global application information
    /// </summary>
    public abstract class UniversalApplication : Windows.UI.Xaml.Application
    {
        /// <summary>
        /// Configuration data, XML .config style
        /// </summary>
        public ConfigurationManagerSafe Configuration { get; protected set; } = new ConfigurationManagerSafe();

        /// <summary>
        /// MyWebService
        /// </summary>
        public virtual Uri MyWebService { get { return new Uri(this.Configuration.AppSettingValue("MyWebService"), UriKind.RelativeOrAbsolute); } }

        /// <summary>
        /// Entry point Screen (Typically login screen)
        /// </summary>
        public abstract Type LandingPage { get; }

        /// <summary>
        /// Home dashboard screen
        /// </summary>
        public abstract Type HomePage { get; }

        /// <summary>
        /// Error screen
        /// </summary>
        public abstract Type ErrorPage { get; }

        /// <summary>
        /// Returns currently active window
        /// </summary>
        public static new Window Current
        {
            get
            {
                return Window.Current;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public UniversalApplication() : base()
        {
            InitializeAsync();
        }

        /// <summary>
        /// Initializes this class with data, and allows for proper async behavior
        /// </summary>
        /// <returns></returns>f
        public virtual async Task InitializeAsync(Boolean hasServices)
        {
            await this.LoadConfigAsync();
            if (hasServices == true) await this.WakeServicesAsync();            
        }

        /// <summary>
        /// Loads config data
        /// </summary>
        /// <returns></returns>
        private async Task LoadConfigAsync()
        {
            var localFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            localFolder = await localFolder.GetFolderAsync("Local");
            var appSettingsFile = await localFolder.GetFileAsync("AppSettings.config");
            var appSettingsStream = await Windows.Storage.FileIO.ReadTextAsync(appSettingsFile);
            var connectStringsFile = await localFolder.GetFileAsync("ConnectionStrings.config");
            var connectStringsStream = await Windows.Storage.FileIO.ReadTextAsync(connectStringsFile);
            Configuration = new ConfigurationManagerSafe(appSettingsStream, connectStringsStream);
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
        /// Gets the root frame of the application
        /// </summary>
        /// <returns></returns>
        public Frame RootFrame
        {
            get
            {
                var returnValue = new Frame();
                var masterLayout = new GenericLayout();
                if (Current.Content is GenericLayout)
                {
                    masterLayout = (GenericLayout)Current.Content;
                    returnValue = masterLayout.ContentFrame;
                }
                else if(Current.Content is Frame)
                {
                    returnValue = (Frame)Current.Content;
                }                
                return returnValue;
            }
        }
    }
}
