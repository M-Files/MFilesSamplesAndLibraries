using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using GenerateApplicationPreApprovalKey.App.Helpers;
using MFaaP.MFilesAPI;
using MFilesAPI;
using Microsoft.Win32;

namespace GenerateApplicationPreApprovalKey.App.ViewModels
{
	/// <summary>
	/// The viewmodel used for the main window.
	/// </summary>
	public class MainWindowViewModel
		: INotifyPropertyChanged
	{
		/// <summary>
		/// The connection details for the M-Files server.
		/// </summary>
		private ConnectionDetails connectionDetails = new ConnectionDetails();

		/// <summary>
		/// A collection of all vaults returned when connecting to the server.
		/// </summary>
		private ObservableCollection<VaultOnServer> allVaults = new ObservableCollection<VaultOnServer>();

		/// <summary>
		/// The GUID of the vault to select.
		/// </summary>
		private string vaultGuid;

		/// <summary>
		/// The currently-selected vault.
		/// </summary>
		private Vault selectedVault;

		/// <summary>
		/// The currently-selected index in the main screen.
		/// </summary>
		/// <remarks>0 = Connection/Authentication, 1 = Applications</remarks>
		private int currentTabIndex = 0;

		/// <summary>
		/// The UIX applications in the currently-selected vault.
		/// </summary>
		private ObservableCollection<CustomApplication> customApplications = new ObservableCollection<CustomApplication>();

		/// <summary>
		/// A dialog used to prompt the user where to save the *.reg file.
		/// </summary>
		private readonly SaveFileDialog saveFileDialog = new SaveFileDialog()
		{
			AddExtension = true,
			DefaultExt = ".reg",
			Filter = "Registry files|*.reg",
			OverwritePrompt = true
		};

		private Window window;

		/// <summary>
		/// The main application window.
		/// </summary>
		/// <remarks>Used to ensure dialogs/messages are modal.</remarks>
		public Window Window
		{
			get { return this.window; }
			set { this.window = value;
				this.OnPropertyChanged();
			}
		}

		/// <summary>
		/// The connection details for the M-Files server.
		/// </summary>
		public ConnectionDetails ConnectionDetails
		{
			get { return this.connectionDetails; }
			set
			{
				this.connectionDetails = value;
				this.OnPropertyChanged();
			}
		}

		/// <summary>
		/// A collection of all vaults returned when connecting to the server.
		/// </summary>
		public ObservableCollection<VaultOnServer> AllVaults
		{
			get { return this.allVaults; }
			set
			{
				this.allVaults = value;
				this.OnPropertyChanged();
			}
		}

		/// <summary>
		/// Identifies whether <see cref="AllVaults"/>
		/// has any items.
		/// </summary>
		/// <remarks>Used for binding.</remarks>
		public bool HasVaults => this.AllVaults.Count > 0;

		/// <summary>
		/// The currently-selected vault.
		/// </summary>
		public Vault SelectedVault
		{
			get { return this.selectedVault; }
			set
			{
				this.selectedVault = value;
				this.OnPropertyChanged();
			}
		}

		/// <summary>
		/// The GUID of the vault to select.
		/// </summary>
		public string VaultGuid
		{
			get { return this.vaultGuid; }
			set
			{
				this.vaultGuid = value;
				this.OnPropertyChanged();
			}
		}

		/// <summary>
		/// The currently-selected index in the main screen.
		/// </summary>
		/// <remarks>0 = Connection/Authentication, 1 = Applications</remarks>
		public int CurrentTabIndex
		{
			get { return this.currentTabIndex; }
			set
			{
				this.currentTabIndex = value;
				this.OnPropertyChanged();
			}
		}

		/// <summary>
		/// The UIX applications in the currently-selected vault.
		/// </summary>
		public ObservableCollection<CustomApplication> CustomApplications
		{
			get { return this.customApplications; }
			set
			{
				this.customApplications = value;
				this.OnPropertyChanged();
			}
		}

		/// <summary>
		/// A command to execute <see cref="GenerateRegistryFile"/>.
		/// </summary>
		/// <remarks>Used for binding.</remarks>
		public ICommand GenerateRegistryFileCommand { get; private set; }

		/// <summary>
		/// A command to execute <see cref="RetrieveVaults"/>.
		/// </summary>
		/// <remarks>Used for binding.</remarks>
		public ICommand RetrieveVaultsCommand { get; private set; }

		/// <summary>
		/// A command to execute <see cref="LoadApplications"/>.
		/// </summary>
		/// <remarks>Used for binding.</remarks>
		public ICommand LoadApplicationsCommand { get; private set; }

		public MainWindowViewModel(Window window)
		{
			// Sanity.
			if(null == window)
				throw new ArgumentNullException(nameof(window));

			// Set the commands.
			this.GenerateRegistryFileCommand = new ActionCommand(p => this.GenerateRegistryFile(p as CustomApplication));
			this.RetrieveVaultsCommand = new ActionCommand(p => this.RetrieveVaults());
			this.LoadApplicationsCommand = new ActionCommand(p => this.LoadApplications());

			// Ensure we have a window reference.
			this.Window = window;
		}

		/// <summary>
		/// Generates the registry file.
		/// </summary>
		/// <param name="application">The custom application that should be within the registry file.</param>
		public void GenerateRegistryFile(CustomApplication application)
		{
			// Sanity.
			if (null == application)
				throw new ArgumentNullException(nameof(application));

			// Let them choose a file.
			if (this.saveFileDialog.ShowDialog(this.Window) != true)
				return;

			// Does it exist?
			var fileInfo = new System.IO.FileInfo(this.saveFileDialog.FileName);
			if(fileInfo.Exists)
				fileInfo.Delete();

			// Create it.
			using (var textWriter = fileInfo.CreateText())
			{
				textWriter.WriteLine("Windows Registry Editor Version 5.00");
				textWriter.WriteLine();
				textWriter.WriteLine($@"[HKEY_LOCAL_MACHINE\SOFTWARE\Motive\M-Files\{this.SelectedVault.GetServerVersionOfVault().Display}\Client\MFClient\ApplicationAccess\{this.SelectedVault.GetGUID()}]");
				textWriter.WriteLine($"\"{application.ID}\"=\"{application.ChecksumHash}\"");
			}
		}

		/// <summary>
		/// Retrieves the vaults from the server
		/// and populates <see cref="AllVaults"/>.
		/// </summary>
		public void RetrieveVaults()
		{
			// Clear the vaults and applications.
			this.AllVaults.Clear();
			this.CustomApplications.Clear();

			// Attempt to load the vaults.
			IEnumerable<VaultOnServer> vaults;
			var success = this.ConnectionDetails.TryGetOnlineVaults(out vaults);

			// If successful, populate the vaults.
			if (success)
			{
				foreach (var vault in vaults)
				{
					this.AllVaults.Add(vault);
				}
			}
			else
			{
				MessageBox.Show(this.Window, "Please check connection/authentication details");
			}

			// Also notify that the flag on whether we have vaults may have changed.
			// ReSharper disable once ExplicitCallerInfoArgument
			this.OnPropertyChanged(nameof(this.HasVaults));
		}

		public void LoadApplications()
		{
			// Sanity.
			var vaultOnServer = this.AllVaults?
				.FirstOrDefault(v => v.GUID == this.VaultGuid);
			if (null == vaultOnServer)
				return;

			// Log into the vault.
			this.SelectedVault = vaultOnServer.LogIn();

			// Load the custom applications.
			this.CustomApplications.Clear();
			foreach (var app in this.SelectedVault
				.CustomApplicationManagementOperations
				.GetCustomApplications()
				.Cast<CustomApplication>())
			{
				this.CustomApplications.Add(app);
			}

			// Move to the next tab.
			this.CurrentTabIndex = 1;
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
