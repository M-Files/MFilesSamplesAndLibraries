using MFilesAPI;
using System;
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

namespace COMAPI.Controls
{
	/// <summary>
	/// Interaction logic for MFConnectionDetails.xaml
	/// </summary>
	public partial class MFConnectionDetails : UserControl
	{
		public string UserName => "";
		public string Domain => "";
		public string VaultGuid {
			get
			{
				var guid = this.vaultGuid?.Text;
				if (Guid.TryParse(guid, out Guid result))
					return result.ToString("B");
				return string.Empty;
			}
		}
		public string ProtocolSequence => (this.protocolSequence?.SelectedItem as ComboBoxItem)?.Tag?.ToString() ?? string.Empty;
		public string NetworkAddress => this.networkAddress?.Text ?? string.Empty;
		public string Endpoint => this.endpoint?.Text ?? string.Empty;
		public bool EncryptedConnection => this.encryptedConnection?.IsChecked ?? false;
		public MFConnectionDetails()
		{
			InitializeComponent();
		}
	}
}
