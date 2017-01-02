//-----------------------------------------------------------------------
// <copyright file="TopMenu.cs" company="Genesys Source">
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
using Framework.Entity;
using Framework.Pages;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Framework.UserControls
{
    /// <summary>
    /// Top bar with title and back button
    /// </summary>
    
    public sealed partial class TopMenu : ReadOnlyControl
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public TopMenu()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Binds controls to the data 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="modelData"></param>
        protected override void BindModelData(object modelData)
        {
        }

        /// <summary>
        /// Partial and controls have been loaded
        /// </summary>
        /// <param name="sender">Sender of this event call</param>
        /// <param name="e">Event arguments</param>
        protected override void Partial_Loaded(object sender, EventArgs e)
        {
            base.Partial_Loaded(sender, e);
        }

        /// <summary>
        /// Navigates to CustomerSummary screen, passing a test Customer to bind and display
        /// </summary>
        /// <param name="sender">Sender of event</param>
        /// <param name="e">Event arguments</param>
        private void Search_Click(object sender, RoutedEventArgs e)
        {            
            var newComponent = System.Windows.Application.LoadComponent(CustomerSummary.Uri);
            var navService = NavigationService.GetNavigationService(this);

            if (newComponent is ReadOnlyPage)
            {
                navService.LoadCompleted += new LoadCompletedEventHandler(((ReadOnlyPage)newComponent).NavigationService_LoadCompleted);
            }
            navService.Navigate(((Page)newComponent), new CustomerModel() { FirstName = "John", LastName = "Smith", BirthDate = new DateTime(1982, 5, 19) });
        }

        /// <summary>
        /// Navigates to CustomerCreate screen, passing a test Customer to bind and display
        /// </summary>
        /// <param name="sender">Sender of event</param>
        /// <param name="e">Event arguments</param>
        private void Create_Click(object sender, RoutedEventArgs e)
        {
            var newComponent = System.Windows.Application.LoadComponent(CustomerCreate.Uri);
            var navService = NavigationService.GetNavigationService(this);

            if (newComponent is ReadOnlyPage)
            {
                navService.LoadCompleted += new LoadCompletedEventHandler(((ReadOnlyPage)newComponent).NavigationService_LoadCompleted);
            }
            navService.Navigate(((Page)newComponent), new CustomerModel() { FirstName = "John", LastName = "Smith", BirthDate = new DateTime(1982, 5, 19) });
        }

        /// <summary>
        /// Navigates to CustomerEdit screen, passing a test Customer to bind and display
        /// </summary>
        /// <param name="sender">Sender of event</param>
        /// <param name="e">Event arguments</param>
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var newComponent = System.Windows.Application.LoadComponent(CustomerEdit.Uri);
            var navService = NavigationService.GetNavigationService(this);

            if (newComponent is ReadOnlyPage)
            {
                navService.LoadCompleted += new LoadCompletedEventHandler(((ReadOnlyPage)newComponent).NavigationService_LoadCompleted);
            }
            navService.Navigate(((Page)newComponent), new CustomerModel() { FirstName = "John", LastName = "Smith", BirthDate = new DateTime(1982, 5, 19) });
        }

        /// <summary>
        /// Navigates to CustomerDelete screen, passing a test Customer to bind and display
        /// </summary>
        /// <param name="sender">Sender of event</param>
        /// <param name="e">Event arguments</param>
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var newComponent = System.Windows.Application.LoadComponent(CustomerDelete.Uri);
            var navService = NavigationService.GetNavigationService(this);

            if (newComponent is ReadOnlyPage)
            {
                navService.LoadCompleted += new LoadCompletedEventHandler(((ReadOnlyPage)newComponent).NavigationService_LoadCompleted);
            }
            navService.Navigate(((Page)newComponent), new CustomerModel() { FirstName = "John", LastName = "Smith", BirthDate = new DateTime(1982, 5, 19) });
        }

        /// <summary>
        /// Validate this control
        /// </summary>
        /// <returns></returns>
        public override bool Validate()
        {
            return true;
        }
    }
}