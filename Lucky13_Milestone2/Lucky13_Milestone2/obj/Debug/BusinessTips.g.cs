﻿#pragma checksum "..\..\BusinessTips.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "43FD4BAC438F784A19CB656E0D7CFCB40483CAD7CC91F340A09F615EB7B4D69C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Lucky13_Milestone2;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Lucky13_Milestone2 {
    
    
    /// <summary>
    /// BusinessTips
    /// </summary>
    public partial class BusinessTips : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\BusinessTips.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label BusinessName;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\BusinessTips.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox busName;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\BusinessTips.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tipTextBox;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\BusinessTips.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label newTip;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\BusinessTips.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addTipButton;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\BusinessTips.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid tipGrid;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\BusinessTips.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GroupBox Business_Tips;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\BusinessTips.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GroupBox groupBox;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\BusinessTips.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid friendDataGrid;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\BusinessTips.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button likeTipButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Lucky13_Milestone2;component/businesstips.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\BusinessTips.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.BusinessName = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.busName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.tipTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 12 "..\..\BusinessTips.xaml"
            this.tipTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.tipTextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.newTip = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.addTipButton = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\BusinessTips.xaml"
            this.addTipButton.Click += new System.Windows.RoutedEventHandler(this.addTipButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.tipGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 15 "..\..\BusinessTips.xaml"
            this.tipGrid.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.tipGrid_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.Business_Tips = ((System.Windows.Controls.GroupBox)(target));
            return;
            case 8:
            this.groupBox = ((System.Windows.Controls.GroupBox)(target));
            return;
            case 9:
            this.friendDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 10:
            this.likeTipButton = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\BusinessTips.xaml"
            this.likeTipButton.Click += new System.Windows.RoutedEventHandler(this.likeTipButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

