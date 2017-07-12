using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using GenerateApplicationPreApprovalKey.App.ViewModels;
using MFilesAPI;

namespace GenerateApplicationPreApprovalKey.App
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
		: INotifyPropertyChanged
	{
		/// <summary>
		/// Our application data context.
		/// </summary>
		public new ViewModels.MainWindowViewModel DataContext
		{
			get { return base.DataContext as MainWindowViewModel; }
			set
			{
				base.DataContext = value;
				this.OnPropertyChanged();
			}
		}

		/// <summary>
		/// Instantiates the main window and assigns a data context.
		/// </summary>
		public MainWindow()
		{
			this.InitializeComponent();
			this.DataContext = new MainWindowViewModel(this);
		}

		#region INotifyPropertyChanged

		/// <inheritdoc cref="INotifyPropertyChanged.PropertyChanged"/>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Notifies subscribers that a property value has changed.
		/// </summary>
		/// <param name="propertyName">The name of the property that changed.</param>
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion

	}
}
