//-----------------------------------------------------------------------
// <copyright file="ReadOnlyPage.cs" company="Genesys Source">
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
using Framework.Applications;
using Genesys.Extensions;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Framework.Pages
{
    /// <summary>
    /// Screen base for authenticated users
    /// </summary>    
    public abstract class ReadOnlyPage : Page
    {
        /// <summary>
        /// New model to load
        /// </summary>
        public event NewModelReceivedEventHandler NewModelReceived;

        /// <summary>
        /// OnNewModelReceived()
        /// </summary>
        protected virtual void OnNewModelReceived(object newModel)
        {
            if (this.NewModelReceived != null)
            {
                NewModelReceived(this, new NewModelReceivedEventArgs() { NewModelData = newModel });
            }
        }

        /// <summary>
        /// Workflow beginning. No custom to return.
        /// </summary>
        /// <param name="sender">Sender of event</param>
        /// <param name="e">Event arguments</param>
        public delegate void NewModelReceivedEventHandler(object sender, NewModelReceivedEventArgs e);

        /// <summary>
        /// Event Args for loading and binding new model data
        /// </summary>
        public class NewModelReceivedEventArgs : EventArgs
        {
            /// <summary>
            /// New model data
            /// </summary>
            public object NewModelData { get; set; }
        }

        /// <summary>
        /// Application instance
        /// </summary>
        public UniversalApplication MyApplication { get { return (UniversalApplication)Windows.UI.Xaml.Application.Current; } }

        /// <summary>
        /// Name of the controller used in web service calls
        /// </summary>
        public abstract string ControllerName { get; }

        /// <summary>
        /// Uri to currently active frame/page
        /// </summary>
        public Type CurrentSource { get { return MyApplication.RootFrame.SourcePageType; } }

        /// <summary>
        /// Throws Exception if any UI elements overrun their text max length
        /// </summary>
        public bool ThrowExceptionOnTextOverrun { get; set; } = TypeExtension.DefaultBoolean;

        /// <summary>
        /// Constructor
        /// </summary>
        protected ReadOnlyPage() : base()
        {
#if (DEBUG)
            ThrowExceptionOnTextOverrun = true;
#endif
            Loaded += Page_Loaded;
            SizeChanged += Page_SizeChanged;
            NewModelReceived += Page_ModelReceived;
        }

        /// <summary>
        /// Binds all model data to the screen controls and sets MyViewModel.Model property
        /// </summary>
        /// <param name="modelData">Model data to bind</param>
        protected abstract void BindModel(object modelData);

        /// <summary>
        /// Binds all model data to the screen controls and sets MyViewModel.Model property
        /// </summary>
        /// <param name="sender">Sender of event</param>
        /// <param name="e">Event arguments</param>
        protected abstract void Page_ModelReceived(object sender, NewModelReceivedEventArgs e);

        /// <summary>
        /// Page_Load event handler
        /// </summary>
        /// <param name="sender">Sender of event</param>
        /// <param name="e">Event arguments</param>
        protected virtual void Page_Loaded(object sender, RoutedEventArgs e)
        {
        }

        /// <summary>
        /// Page_SizeChanged event handler
        /// </summary>
        /// <param name="sender">Sender of event</param>
        /// <param name="e">Event arguments</param>
        protected virtual void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {            
        }

        /// <summary>
        /// Navigated to. Add handlers for charms bar
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {            
            base.OnNavigatedTo(e);
            OnNewModelReceived(e.Parameter);
        }

        /// <summary>
        /// Leaving screen
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }

        /// <summary>
        /// Handles for size changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BasePage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
        }
    }
}
