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

		private Employee _modelEmployee;
		public Employee ModelEmployee
		{
			get { return _modelEmployee; }
			set
			{
				_modelEmployee = value;
				OnPropertyChanged("ModelEmployee");
			}
		}

		private ICommand _saveEmployeeCommand;
		public ICommand SaveEmployeeCommand
		{
			get { return _saveEmployeeCommand; }
			set
			{
				_saveEmployeeCommand = value;
				OnPropertyChanged("SaveEmployeeCommand");
			}
		}

		public ViewEmployeeVM()
		{
			Initialize();
			LoadEmployee(null);
		}

		private void Initialize()
		{
			SaveEmployeeCommand = new SaveEmployeeCommand(UpdatePerson);
			Mediator.GetInstance().SelectedEmployeeChanged += (s, e) =>
			{
				LoadEmployee(e.Employee);
			};
		}

		private void UpdatePerson()
		{
			if (CanSave)
			{
				var employee = Employee.GetEmployees().FirstOrDefault(e => e.Id == ModelEmployee.Id);

				if (employee == null)
				{
					ModelEmployee.Id = ModelEmployee.GenerateUniqueId(random, Employee.GetEmployees());
					Employee.GetEmployees().Add(ModelEmployee);
				}
				else
				{
					employee.Save(ModelEmployee);
				}
			}
		}

		private void LoadEmployee(Employee employee)
		{
			if (employee == null || employee.Id == null)
			{
				ModelEmployee = new Employee();
				return;
			}

			var selectedEmployee = Employee.GetEmployees().FirstOrDefault(e => e.Id == employee.Id);
			if (selectedEmployee == null)
			{
				ModelEmployee = new Employee();
				return;
			}
			else
			{
				ModelEmployee = selectedEmployee.Copy();
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
