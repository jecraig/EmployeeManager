using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmployeeManager.Commands
{
	/// <summary>
	/// The command to cancel the saving of an employee
	/// </summary>
	public class CancelSaveEmployeeCommand : ICommand
	{
		Action executeMethod;

		public CancelSaveEmployeeCommand(Action cancelSave)
		{
			executeMethod = cancelSave;
		}

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public event EventHandler CanExecuteChanged;
		public void Execute(object parameter)
		{
			executeMethod.Invoke();
		}
	}
}
