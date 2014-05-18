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

		private Employee _savedEmployee;
		public Employee SavedEmployee
		{
			get { return _savedEmployee; }
			set
			{
				_savedEmployee = value;
				OnPropertyChanged("SavedEmployee");
			}
		}

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

		private ICommand _cancelSaveEmployeeCommand;
		public ICommand CancelSaveEmployeeCommand
		{
			get { return _cancelSaveEmployeeCommand; }
			set
			{
				_cancelSaveEmployeeCommand = value;
				OnPropertyChanged("CancelSaveEmployeeCommand");
			}
		}

		private ICommand _getWeeklyRateCommand;
		public ICommand GetWeeklyRateCommand
		{
			get { return _getWeeklyRateCommand; }
			set
			{
				_getWeeklyRateCommand = value;
				OnPropertyChanged("GetHourlyRateCommand");
			}
		}

		private decimal _weeklyHours;
		public decimal WeeklyHours
		{
			get { return _weeklyHours; }
			set
			{
				_weeklyHours = value;
				OnPropertyChanged("WeeklyHours");
			}
		}

		private decimal _weeklyRate;
		public decimal WeeklyRate
		{
			get { return _weeklyRate; }
			set
			{
				_weeklyRate = value;
				OnPropertyChanged("WeeklyRate");
			}
		}

		public ViewEmployeeVM()
		{
			Initialize();
			LoadEmployee(null);
		}

		private void Initialize()
		{
			SaveEmployeeCommand = new SaveEmployeeCommand(SaveEmployee);
			CancelSaveEmployeeCommand = new CancelSaveEmployeeCommand(ReloadEmployee);
			GetWeeklyRateCommand = new GetWeeklyRateCommand(CalculateWeeklyRate);
			Mediator.GetInstance().SelectedEmployeeChanged += (s, e) =>
			{
				LoadEmployee(e.Employee);
			};
		}

		private void CalculateWeeklyRate()
		{
			decimal rate;
			decimal hours = WeeklyHours;
			if (!ModelEmployee.IsHourly)
			{
				rate = ModelEmployee.Wage / 52;
			}
			else
			{
				if (hours > 40)
				{
					rate = 40 * ModelEmployee.Wage;
					rate += (hours - 40) * ModelEmployee.Wage * 1.5M;
				}
				else
				{
					rate = hours * ModelEmployee.Wage;
				}
			}

			WeeklyRate = rate;
		}

		private void ReloadEmployee()
		{
			ModelEmployee = SavedEmployee.Copy();
		}

		private void SaveEmployee()
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
					SavedEmployee = ModelEmployee.Copy();
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
				SavedEmployee = ModelEmployee.Copy();
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
