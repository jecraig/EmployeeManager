using EmployeeManager.Commands;
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
	public class ViewEmployeeVM : INotifyPropertyChanged
	{
		private Random random = new Random();

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
			LoadEmployee(null);
		}

		private void InitializeCommand()
		{
			SaveEmployeeCommand = new SaveEmployeeCommand(UpdatePerson);
		}

		private void UpdatePerson()
		{
			if (CanSave)
			{
				if (!Employee.GetEmployees().Contains(ModelEmployee))
				{
					ModelEmployee.Id = ModelEmployee.GenerateUniqueId(random, Employee.GetEmployees());
					Employee.GetEmployees().Add(ModelEmployee);
				}
			}
		}

		private void LoadEmployee(string employeeId)
		{
			if (employeeId == null)
			{
				ModelEmployee = new Employee();
				return;
			}

			var selectedEmployee = Employee.GetEmployees().FirstOrDefault(e => e.Id == employeeId);
			if (selectedEmployee == null)
			{
				ModelEmployee = new Employee();
				return;
			}
			else
			{
				ModelEmployee = selectedEmployee;
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged(string property)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(property));
		}

		protected bool CanSave
		{
			get
			{
				return ModelEmployee.IsValid;
			}
		}
	}
}
