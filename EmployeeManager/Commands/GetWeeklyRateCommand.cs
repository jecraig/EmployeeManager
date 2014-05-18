using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmployeeManager.Commands
{
	public class GetWeeklyRateCommand : ICommand
	{
		Action executeMethod;

		public GetWeeklyRateCommand(Action calculateRate)
		{
			executeMethod = calculateRate;
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
