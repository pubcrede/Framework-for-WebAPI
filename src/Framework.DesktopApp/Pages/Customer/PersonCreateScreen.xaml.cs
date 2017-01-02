//-----------------------------------------------------------------------
// <copyright file="PersonCreateScreen.cs" company="Genesys Source">
//      Copyright (c) Genesys Source. All rights reserved.
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
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using Framework.Partials;
using Genesys.Foundation.Process;
using Framework.Person;
using Framework.Controllers;

namespace Framework.Views
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class PersonCreateScreen : SaveableView
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PersonCreateScreen()
        {
            this.InitializeComponent();
            base.InitializeEvents();
        }
        
        /// <summary>
        /// Subscribe to events (called via base class)
        /// </summary>
        protected override void EventsSubscribe()
        {
            if (EventsSubscribed)
                return;
            else
                EventsSubscribed = true;
            // Map to base-global events
            this.OkCancel.OnOKClicked += OK_Click;
            this.OkCancel.OnCancelClicked += Cancel_Click;
            // Map to screen-specific events
            this.TextFirstName.LostFocus += TextAll_LostFocus;
            this.TextLastName.LostFocus += TextAll_LostFocus;
            this.TextFirstName.KeyDown += MapEnterKey;
            this.TextLastName.KeyDown += MapEnterKey;
            this.TextBirthDate.KeyDown += MapEnterKey;
        }
        
        /// <summary>
        /// Link actual XAML buttons to base class
        ///  A XAML template will eliminate need for this.
        /// </summary>
        protected override OkCancel OkCancel
        {
            get { return this.ButtonOkCancel; }
            set { this.ButtonOkCancel = value; }
        }
        
        /// <summary>
        /// Map the enter key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MapEnterKey(Object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && e.IsRepeat == false)
            {
                this.OK_Click(sender, e);
            }
        }

        /// <summary>
        /// Show terms popup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LinkViewTerms_Click(object sender, RoutedEventArgs e)
        {
            //await Windows.System.Launcher.LaunchUriAsync(new Uri(AppController.ApplicationInfo.URLTerms));
        }

        /// <summary>
        /// Show terms popup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LinkLogon_Click(object sender, RoutedEventArgs e)
        {
            this.Navigate(typeof(PersonCreateScreen));
        }

        /// <summary>
        /// Sets casing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextAll_LostFocus(object sender, RoutedEventArgs e)
        {
            //this.TextFirstName.Text = StringHelper.FormatCase(this.TextFirstName.Text, StringHelper.CaseTypes.Pascal);
            //this.TextLastName.Text = StringHelper.FormatCase(this.TextLastName.Text, StringHelper.CaseTypes.Pascal);
        }
        
        /// <summary>
        /// Loads page with data and binds controls
        /// </summary>
        /// <param name="e"></param>
        public override void Load(NavigationEventArgs e)
        {
            //this.TextBirthDate.Year = DateTime.UtcNow.AddYears(-25).Year;
        }        

        /// <summary>
        /// Processes any page data via workflow
        /// </summary>
        public override async Task<ProcessResult> Process(object sender, RoutedEventArgs e)
        {
            
            ProcessResult returnValue = new ProcessResult();
            PersonModel ScreenData = new PersonModel() { FirstName = this.TextFirstName.Text, LastName = this.TextLastName.Text, BirthDate = this.TextBirthDate.SelectedDate.Value };

            returnValue = await PersonCreateController.Current.Create(ScreenData);
            return returnValue;
        }

        /// <summary>
        /// Cancels the screen and/or process
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void Cancel(object sender, RoutedEventArgs e)
        {
            this.MyApplication.Navigate(typeof(PersonCreateScreen));
        }
    }
}
