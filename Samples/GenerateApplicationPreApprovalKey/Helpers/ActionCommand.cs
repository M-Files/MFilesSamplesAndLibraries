using System;
using System.Windows.Input;

namespace GenerateApplicationPreApprovalKey.App.Helpers
{
	/// <summary>
	/// An implemenation of <see cref="ICommand"/> that allows an <see cref="Action{T}"/>
	/// to be called in response to an event binding.
	/// </summary>
	public class ActionCommand
		: ICommand
	{
		/// <summary>
		/// The action to call when the event is fired.
		/// </summary>
		private Action<object> Action { get; set; }

		/// <summary>
		/// Instantiates an <see cref="ActionCommand"/>.
		/// </summary>
		/// <param name="action">The action to execute.</param>
		public ActionCommand(Action<object> action)
		{
			// Assign.
			this.Action = action ?? throw new ArgumentNullException(nameof(action));
		}

		/// <inheritdoc />
		public bool CanExecute(object parameter)
		{
			return true;
		}

		/// <inheritdoc />
		public void Execute(object parameter)
		{
			this.Action(parameter);
		}

		/// <inheritdoc />
		public event EventHandler CanExecuteChanged;
	}
}
