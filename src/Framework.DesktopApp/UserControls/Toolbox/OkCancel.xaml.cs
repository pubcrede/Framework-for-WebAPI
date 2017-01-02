//-----------------------------------------------------------------------
// <copyright file="OkCancel.cs" company="Genesys Source">
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
using System.Windows;
using System.Windows.Controls;
using Genesys.Foundation.Process;

namespace Framework.UserControls
{
    /// <summary>
    /// OK and cancel buttons
    /// </summary>
    public sealed partial class OkCancel : ReadOnlyControl
    {
        /// <summary>
        /// OnOKClickedEventHandler
        /// </summary>
        /// <param name="sender">Sender of event</param>
        /// <param name="e">Event arguments</param>
        public delegate void OnOKClickedEventHandler(object sender, RoutedEventArgs e);

        /// <summary>
        /// OnOKClickedEventHandler
        /// </summary>
        public event OnOKClickedEventHandler OnOKClicked;

        /// <summary>
        /// OnCancelClickedEventHandler
        /// </summary>
        /// <param name="sender">Sender of event</param>
        /// <param name="e">Event arguments</param>
        public delegate void OnCancelClickedEventHandler(object sender, RoutedEventArgs e);

        /// <summary>
        /// OnCancelClickedEventHandler
        /// </summary>
        public event OnCancelClickedEventHandler OnCancelClicked;

        /// <summary>
        /// Shows/hides the map
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        public Visibility VisibilityButtons
        {
            get
            {
                return ButtonOK.Visibility;
            }
            set
            {
                ButtonOKControl.Visibility = value;
                ButtonCancelControl.Visibility = value;
                // Only show progress if processing
                if (value == System.Windows.Visibility.Collapsed)
                {
                    ProgressProcessing.Visibility = value;
                }
            }
        }

        /// <summary>
        /// HorizontalAlignment
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        public new HorizontalAlignment HorizontalAlignment
        {
            get
            {
                return StackButtons.HorizontalAlignment;
            }
            set
            {
                StackButtons.HorizontalAlignment = value;
            }
        }

        /// <summary>
        /// Orientation
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        public Orientation Orientation
        {
            get
            {
                return StackButtons.Orientation;
            }
            set
            {
                StackButtons.Orientation = value;
            }
        }

        /// <summary>
        /// Progress text
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        public string TextProcessing
        {
            get
            {
                return ProgressProcessing.TextProgress;
            }
            set
            {
                ProgressProcessing.TextProgress = value;
            }
        }

        /// <summary>
        /// Progress text
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        public string TextSuccessful
        {
            get
            {
                return ProgressProcessing.TextSuccess;
            }
            set
            {
                ProgressProcessing.TextSuccess = value;
            }
        }

        /// <summary>
        /// Progress text
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        public string TextErrored
        {
            get
            {
                return ProgressProcessing.TextError;
            }
            set
            {
                ProgressProcessing.TextError = value;
            }
        }

        /// <summary>
        /// Progress text
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        public string TextCanceled
        {
            get
            {
                return ProgressProcessing.TextCancel;
            }
            set
            {
                ProgressProcessing.TextCancel = value;
            }
        }      

        /// <summary>
        /// OK Button
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        public Button ButtonOK
        {
            get
            {
                return ButtonOKControl;
            }
            set
            {
                ButtonOKControl = value;
            }
        }

        /// <summary>
        /// OK Button content
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        public object ButtonOKContent
        {
            get
            {
                return ButtonOKControl.Content;
            }
            set
            {
                ButtonOKControl.Content = value;
            }
        }

        /// <summary>
        /// Cancel Button
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        public Button ButtonCancel
        {
            get
            {
                return ButtonCancelControl;
            }
            set
            {
                ButtonCancelControl = value;
            }
        }

        /// <summary>
        /// OK Button content
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        public object ButtonCancelContent
        {
            get
            {
                return ButtonCancelControl.Content;
            }
            set
            {
                ButtonCancelControl.Content = value;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public OkCancel()
        {
            InitializeComponent();
            ButtonOKControl.Click += OK_Click;
            ButtonCancelControl.Click += Cancel_Click;
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
        /// Ok button
        /// </summary>
        /// <param name="sender">Sender of event</param>
        /// <param name="e">Event arguments</param>
        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (OnOKClicked != null)
                OnOKClicked(sender, e);
        }

        /// <summary>
        /// Cancel Button
        /// </summary>
        /// <param name="sender">Sender of event</param>
        /// <param name="e">Event arguments</param>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (OnCancelClicked != null)
                OnCancelClicked(sender, e);
        }

        /// <summary>
        /// Starts the processing
        /// </summary>
        public void StartProcessing(string processingMessage = "")
        {
            StackButtons.Visibility = Visibility.Collapsed;
            ProgressProcessing.StartProcessing(processingMessage);
        }

        /// <summary>
        /// Stops processing, and displays an error or optional success message
        /// </summary>
        /// <param name="results"></param>
        /// <param name="successMessage"></param>
        public void StopProcessing(ProcessResult results)
        {
            StackButtons.Visibility = Visibility.Visible;
            ProgressProcessing.StopProcessing(results, TextSuccessful);
        }

        /// <summary>
        /// Stops processing, and displays an error or optional success message
        /// </summary>
        /// <param name="cancelMessage"></param>
        public void CancelProcessing(string cancelMessage = "")
        {
            StackButtons.Visibility = Visibility.Visible;
            ProgressProcessing.StopProcessing(new ProcessResult(), cancelMessage);
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
