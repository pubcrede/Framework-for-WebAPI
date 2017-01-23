//-----------------------------------------------------------------------
// <copyright file="CustomerSummary.cs" company="Genesys Source">
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
using Framework.UserControls;
using Framework.ViewModels;
using Genesys.Extensions;
using Genesys.Foundation.Process;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Framework.Pages
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class CustomerSummary : SaveablePage
    {
        /// <summary>
        /// Uri to this resource
        /// </summary>
        public static Uri Uri = new Uri("/Pages/Customer/CustomerSummary.xaml", UriKind.RelativeOrAbsolute);

        /// <summary>
        /// Controller route that handles requests for this page
        /// </summary>
        public override string ControllerName { get; } = "Customer";

        /// <summary>
        /// ViewModel holds model and is responsible for server calls, navigation, etc.
        /// </summary>
        public WpfViewModel<CustomerModel> MyViewModel { get; } = new WpfViewModel<CustomerModel>("Customer");

        /// <summary>
        /// Link actual XAML buttons to base class
        ///  A XAML template will eliminate need for this.
        /// </summary>
        protected override OkCancel OkCancel
        {
            get { return ButtonOkCancel; }
            set { ButtonOkCancel = value; }
        }

        /// <summary>
        /// Page and controls have been loaded
        /// </summary>
        /// <param name="sender">Sender of this event call</param>
        /// <param name="e">Event arguments</param>
        protected override void Page_Loaded(object sender, EventArgs e)
        {
            base.Page_Loaded(sender, e);
        }        

        /// <summary>
        /// Sets casing
        /// </summary>
        /// <param name="sender">Sender of event</param>
        /// <param name="e">Event arguments</param>
        private void TextAll_LostFocus(object sender, RoutedEventArgs e)
        {
            TextFirstName.Text = this.TextFirstName.Text.ToPascalCase();
            TextLastName.Text = this.TextLastName.Text.ToPascalCase();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public CustomerSummary()
        {
            InitializeComponent();
            TextFirstName.LostFocus += TextAll_LostFocus;
            TextLastName.LostFocus += TextAll_LostFocus;
        }

        /// <summary>
        /// Sets model data, binds to controls and handles event that introduce new model data to page
        /// </summary>
        /// <param name="sender">Sender of event</param>
        /// <param name="e">Event arguments</param>
        protected override async void Page_ModelReceived(object sender, NewModelReceivedEventArgs e)
        {
            this.OkCancel.StartProcessing("Loading data...");
            CustomerModel model = await MyViewModel.GetByID(e.NewModelData.ToString().TryParseInt32());
            BindModel(model);
            this.OkCancel.CancelProcessing();
        }

        /// <summary>
        /// Binds new model data to screen
        /// </summary>
        /// <param name="modelData"></param>
        protected override void BindModel(object modelData)
        {
            MyViewModel.Model = modelData.DirectCastSafe<CustomerModel>();
            DataContext = MyViewModel.Model;
            SetBinding(ref this.TextID, MyViewModel.Model.ID.ToString(), "ID");
            SetBinding(ref this.TextKey, MyViewModel.Model.Key.ToString(), "Key");
            SetBinding(ref this.TextFirstName, MyViewModel.Model.FirstName, "FirstName");
            SetBinding(ref this.TextLastName, MyViewModel.Model.LastName, "LastName");
            SetBinding(ref this.TextBirthDate, MyViewModel.Model.BirthDate.ToString(), "BirthDate");
            this.TextGender.Text = MyViewModel.Model.GenderSelections().Find(x => x.Key == MyViewModel.Model.GenderID).Value;
        }
        
        /// <summary>
        /// Processes any page data via workflow
        /// </summary>
        public override async Task<ProcessResult> Process(object sender, RoutedEventArgs e)
        {
            var newComponent = System.Windows.Application.LoadComponent(CustomerEdit.Uri);
            var navService = NavigationService.GetNavigationService(this);
            var returnValue = new ProcessResult();

            await Task.Delay(1);
            if (newComponent is ReadOnlyPage)
            {
                navService.LoadCompleted += new LoadCompletedEventHandler(((ReadOnlyPage)newComponent).NavigationService_LoadCompleted);
            }
            navService.Navigate(((Page)newComponent), MyViewModel.Model.ID);

            return returnValue;
        }

        /// <summary>
        /// Cancels the  and/or process
        /// </summary>
        /// <param name="sender">Sender of event</param>
        /// <param name="e">Event arguments</param>
        public override void Cancel(object sender, RoutedEventArgs e)
        {
            var newComponent = System.Windows.Application.LoadComponent(CustomerDelete.Uri);
            var navService = NavigationService.GetNavigationService(this);
            var returnValue = new ProcessResult();

            if (newComponent is ReadOnlyPage)
            {
                navService.LoadCompleted += new LoadCompletedEventHandler(((ReadOnlyPage)newComponent).NavigationService_LoadCompleted);
            }
            navService.Navigate(((Page)newComponent), MyViewModel.Model.ID);
        }        
    }
}
