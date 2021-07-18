﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RESTAPI.Controls
{
	/// <summary>
	/// Interaction logic for MFConnectionDetails.xaml
	/// </summary>
	public partial class MFConnectionDetails : UserControl
	{
		public string NetworkAddress => this.networkAddress?.Text ?? string.Empty;
		public MFConnectionDetails()
		{
			InitializeComponent();
		}
	}
}
