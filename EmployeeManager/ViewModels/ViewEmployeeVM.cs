using EmployeeManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmployeeManager.ViewModels
{
	public class ViewEmployeeVM
	{
		private Employee modelEmployee;
		public Employee ModelEmployee
		{
			get { return modelEmployee; }
			set
			{
				modelEmployee = value;
				OnPropertyChanged("ModelEmployee");
			}
		}

		private ICommand saveEmployeeCommand;
		public ICommand SaveEmployeeCommand
		{
			get { return saveEmployeeCommand; }
			set
			{
				saveEmployeeCommand = value;
				OnPropertyChanged("SaveEmployeeCommand");
			}
		}

		public ViewEmployeeVM()
		{
			InitializeCommand();
			LoadEmployee();
		}

		private void InitializeCommand()
		{
			SaveEmployeeCommand = new SaveEmployeeCommand(UpdatePerson);
		}

		private void UpdatePerson()
		{
			if (ModelEmployee.IsHourly)
				ModelEmployee.FirstName += "Hourly";
			if (!ModelEmployee.IsHourly)
				ModelEmployee.FirstName += "Salary";
		}

		private void LoadEmployee()
		{
			ModelEmployee = new Employee()
			{
				FirstName = "Brian",
				LastName = "Lagunas",
			};
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged(string property)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(property));
		}
	}

	public class SaveEmployeeCommand : ICommand
	{
		Action executeMethod;

		public SaveEmployeeCommand(Action updateEmployee)
		{
			executeMethod = updateEmployee;
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
