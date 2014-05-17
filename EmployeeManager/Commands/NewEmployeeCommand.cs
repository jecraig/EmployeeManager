using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmployeeManager.Commands
{
	public class NewEmployeeCommand : ICommand
	{
		Action executeMethod;

		public NewEmployeeCommand(Action newEmployee)
		{
			executeMethod = newEmployee;
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
