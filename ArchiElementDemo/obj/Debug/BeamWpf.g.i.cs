﻿#pragma checksum "..\..\BeamWpf.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "A2AA6D208AB4680210574E9771FCF47AA8EC2E24"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using ArichWpf;
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


namespace ArchiElementDemo {
    
    
    /// <summary>
    /// BeamWpf
    /// </summary>
    public partial class BeamWpf : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 24 "..\..\BeamWpf.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox DataBase;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\BeamWpf.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox DataTable;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\BeamWpf.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CreateEnter;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\BeamWpf.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DeleteEnter;
        
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
            System.Uri resourceLocater = new System.Uri("/ArchiElementDemo;component/beamwpf.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\BeamWpf.xaml"
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
            this.DataBase = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.DataTable = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.CreateEnter = ((System.Windows.Controls.Button)(target));
            
            #line 42 "..\..\BeamWpf.xaml"
            this.CreateEnter.Click += new System.Windows.RoutedEventHandler(this.CreateEnter_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.DeleteEnter = ((System.Windows.Controls.Button)(target));
            
            #line 46 "..\..\BeamWpf.xaml"
            this.DeleteEnter.Click += new System.Windows.RoutedEventHandler(this.DeleteEnter_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

