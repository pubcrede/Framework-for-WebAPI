//-----------------------------------------------------------------------
// <copyright file="SaveableViewModel.cs" company="Genesys Source">
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
using Genesys.Extensions;
using Genesys.Foundation.Entity;
using System;
using System.Threading.Tasks;

namespace Framework.ViewModels
{
    /// <summary>
    /// ViewModel holds model and is responsible for server calls, navigation, etc.
    /// </summary>
    public class SaveableViewModel<TModel> : ReadOnlyViewModel<TModel> where TModel : ModelEntity<TModel>, new()
    {
        /// <summary>
        /// Name of the controller to use in web service call
        /// </summary>
        public string ControllerName { get; } = TypeExtension.DefaultString;
        
        /// <summary>
        /// Parameterless constructor
        /// </summary>
        protected SaveableViewModel()
            : base()
        {
        }

        /// <summary>
        /// Sets controller that handles this ViewModel 4 CRUD behaviors
        /// </summary>
        /// <param name="controllerName"></param>
        public SaveableViewModel(string controllerName) : this()
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
    }
}
