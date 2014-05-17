using EmployeeManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Commands
{
	class Mediator
	{
		private static readonly Mediator _instance = new Mediator();
		private Mediator() { }

		public static Mediator GetInstance()
		{
			return _instance;
		}

		public event EventHandler<SelectedEmployeeEventArgs> SelectedEmployeeChanged;

		public void OnSelectedEmployeeChanged(object sender, Employee employee)
		{
			var selectedEmployeeChangedDelegate = SelectedEmployeeChanged as EventHandler<SelectedEmployeeEventArgs>;
			if (selectedEmployeeChangedDelegate != null)
			{
				selectedEmployeeChangedDelegate(sender, new SelectedEmployeeEventArgs { Employee = employee });
			}
		}
	}
}
