//-----------------------------------------------------------------------
// <copyright file="WpfViewModel.cs" company="Genesys Source">
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
using Framework.Application;
using Framework.Pages;
using Genesys.Extensions;
using Genesys.Extras.Net;
using Genesys.Foundation.Entity;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Framework.ViewModels
{
    /// <summary>
    /// Interface to enforce ViewModel division of responsibilities
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public interface IViewModel<TModel> : INotifyPropertyChanged
    {
        TModel Model { get; set; }
    }

    /// <summary>
    /// ViewModel holds model and is responsible for server calls, navigation, etc.
    /// </summary>
    public class WpfViewModel<TModel> : IViewModel<TModel> where TModel : ModelEntity<TModel>, new()
    {
        /// <summary>
        /// Model data
        /// </summary>
        public TModel Model { get; set; } = new TModel();

        /// <summary>
        /// Name of the controller to use in web service call
        /// </summary>
        public string ControllerName { get; } = TypeExtension.DefaultString;

        /// <summary>
        /// Property changed event handler for INotifyPropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Property changed event for INotifyPropertyChanged
        /// </summary>
        /// <param name="propertyName">String name of property</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Send is about to begin
        /// </summary>
        public event SendBeginEventHandler SendBegin;

        /// <summary>
        /// Send is complete
        /// </summary>
        public event SendBeginEventHandler SendEnd;

        /// <summary>
        /// Workflow beginning. No custom to return.
        /// </summary>
        /// <param name="sender">Sender of event</param>
        /// <param name="e">Event arguments</param>
        public delegate void SendBeginEventHandler(object sender, EventArgs e);

        /// <summary>
        /// OnSendBegin()
        /// </summary>
        public virtual void OnSendBegin() { if (this.SendBegin != null) { SendBegin(this, EventArgs.Empty); } }

        /// <summary>
        /// OnSendEnd()
        /// </summary>
        public virtual void OnSendEnd() { if (this.SendEnd != null) { SendEnd(this, EventArgs.Empty); } }

        /// <summary>
        /// Application instance
        /// </summary>
        public WpfApplication MyApplication { get { return (WpfApplication)System.Windows.Application.Current; } }

        /// <summary>
        /// Parameterless constructor
        /// </summary>
        private WpfViewModel()
            : base()
        {
        }

        /// <summary>
        /// Sets controller that handles this ViewModel 4 CRUD behaviors
        /// </summary>
        /// <param name="controllerName"></param>
        public WpfViewModel(string controllerName) : this()
        {
            this.ControllerName = controllerName;
        }

        /// <summary>
        /// Pulls data by ID
        /// This is synonymous to ControllerName's HttpGet behavior
        /// </summary>
        /// <param name="id">id of the record to retrieve</param>
        /// <returns></returns>
        public async Task<TModel> GetByID(int id)
        {
            TModel returnData = new TModel();
            string fullUrl = String.Format("{0}/{1}/{2}", MyApplication.MyWebService.ToString(), this.ControllerName, id.ToString());
            returnData = await this.SendGetAsync<TModel>(fullUrl);
            return returnData;
        }

        /// <summary>
        /// Pulls data by ID
        /// This is synonymous to ControllerName's HttpGet behavior
        /// </summary>
        /// <param name="id">id of the record to retrieve</param>
        /// <returns></returns>
        public async Task<TModel> Create(TModel screenData)
        {
            TModel returnData = new TModel();
            string fullUrl = String.Format("{0}/{1}", MyApplication.MyWebService.ToString(), this.ControllerName);
            returnData = await this.SendPutAsync<TModel>(fullUrl, screenData);
            return returnData;
        }

        /// <summary>
        /// Pulls data by ID
        /// This is synonymous to ControllerName's HttpGet behavior
        /// </summary>
        /// <param name="id">id of the record to retrieve</param>
        /// <returns></returns>
        public async Task<TModel> Edit(TModel screenData)
        {
            TModel returnData = new TModel();
            string fullUrl = String.Format("{0}/{1}", MyApplication.MyWebService.ToString(), this.ControllerName);
            returnData = await this.SendPostAsync<TModel>(fullUrl, screenData);
            return returnData;
        }

        /// <summary>
        /// Pulls data by ID
        /// This is synonymous to ControllerName's HttpGet behavior
        /// </summary>
        /// <param name="id">id of the record to retrieve</param>
        /// <returns></returns>
        public async Task<TModel> Delete(int id)
        {
            TModel returnData = new TModel();
            string fullUrl = String.Format("{0}/{1}/{2}", MyApplication.MyWebService.ToString(), this.ControllerName, id.ToString());
            returnData = await this.SendDeleteAsync<TModel>(fullUrl);
            return returnData;
        }

        /// <summary>
        /// CanGoBack if backward path exists, not at root or invalid path
        /// </summary>
        public virtual bool CanGoBack { get { return MyApplication.RootFrame.CanGoBack; } }

        /// <summary>
        /// Navigates back one screen
        /// </summary>
        public virtual void GoBack() { MyApplication.Navigate(MyApplication.HomePage.ToString()); }

        /// <summary>
        /// Navigates to any screen
        /// </summary>
        /// <param name="destinationUri">Destination Uri, absolute or relative</param>
        /// <param name="dataToPass">Screen data</param>
        public virtual void Navigate(string destinationUri, object dataToPass = null)
        {
            Navigate(new Uri(destinationUri, UriKind.RelativeOrAbsolute), dataToPass);
        }

        /// <summary>
        /// Navigates to any screen
        /// </summary>
        /// <param name="destinationUri">Destination Uri, absolute or relative</param>
        /// <param name="dataToPass">Screen data</param>
        public virtual void Navigate(Uri destinationUri, object dataToPass = null)
        {
            var navService = NavigationService.GetNavigationService(MyApplication.RootFrame);
            this.Navigate(destinationUri, dataToPass, navService);
        }

        /// <summary>
        /// Navigates to any screen
        /// </summary>
        /// <param name="destinationUri">Destination Uri, absolute or relative</param>
        /// <param name="dataToPass">Screen data</param>
        public virtual void Navigate(Uri destinationUri, object dataToPass, NavigationService navService)
        {
            var newComponent = System.Windows.Application.LoadComponent(destinationUri);

            if (newComponent is ReadOnlyPage)
            {
                navService.LoadCompleted += new LoadCompletedEventHandler(((ReadOnlyPage)newComponent).NavigationService_LoadCompleted);
            }
            navService.Navigate(((Page)newComponent), dataToPass);
        }

        /// <summary>
        /// Instantiates and transmits all data to the middle tier web service that will execute the workflow
        /// </summary>
        /// <returns></returns>
        public virtual async Task<TDataOut> SendGetAsync<TDataOut>(string fullUrl) where TDataOut : new()
        {
            OnSendBegin();
            TDataOut returnValue = default(TDataOut);
            HttpRequestGet<TDataOut> request = new HttpRequestGet<TDataOut>(fullUrl);
            returnValue = await request.SendAsync();
            OnSendEnd();

            return returnValue;
        }

        /// <summary>
        /// Instantiates and transmits all data to the middle tier web service that will execute the workflow
        /// </summary>
        /// <returns></returns>
        public virtual async Task<TDataInOut> SendPutAsync<TDataInOut>(string fullUrl, TDataInOut itemToSend)
        {
            return await this.SendPutAsync<TDataInOut, TDataInOut>(fullUrl, itemToSend);
        }

        /// <summary>
        /// Instantiates and transmits all data to the middle tier web service that will execute the workflow
        /// </summary>
        /// <returns></returns>
        public virtual async Task<TDataOut> SendPutAsync<TDataIn, TDataOut>(string fullUrl, TDataIn itemToSend)
        {
            OnSendBegin();
            TDataOut returnValue = default(TDataOut);
            HttpRequestPut<TDataIn, TDataOut> request = new HttpRequestPut<TDataIn, TDataOut>(fullUrl, itemToSend);
            returnValue = await request.SendAsync();
            OnSendEnd();
            return returnValue;
        }

        /// <summary>
        /// Instantiates and transmits all data to the middle tier web service that will execute the workflow
        /// </summary>
        /// <returns></returns>
        public virtual async Task<TDataInOut> SendPostAsync<TDataInOut>(string fullUrl, TDataInOut itemToSend)
        {
            return await this.SendPostAsync<TDataInOut, TDataInOut>(fullUrl, itemToSend);
        }

        /// <summary>
        /// Instantiates and transmits all data to the middle tier web service that will execute the workflow
        /// </summary>
        /// <returns></returns>
        public virtual async Task<TDataOut> SendPostAsync<TDataIn, TDataOut>(string fullUrl, TDataIn itemToSend)
        {
            OnSendBegin();
            TDataOut returnValue = default(TDataOut);
            HttpRequestPost<TDataIn, TDataOut> request = new HttpRequestPost<TDataIn, TDataOut>(fullUrl, itemToSend);
            returnValue = await request.SendAsync();
            OnSendEnd();

            return returnValue;
        }

        /// <summary>
        /// Instantiates and transmits all data to the middle tier web service that will execute the workflow
        /// </summary>
        /// <returns></returns>
        public virtual async Task<TDataOut> SendDeleteAsync<TDataOut>(string fullUrl) where TDataOut : new()
        {
            OnSendBegin();
            TDataOut returnValue = default(TDataOut);
            HttpRequestDelete<TDataOut> request = new HttpRequestDelete<TDataOut>(fullUrl);
            returnValue = await request.SendAsync();
            OnSendEnd();
            return returnValue;
        }
    }
}
