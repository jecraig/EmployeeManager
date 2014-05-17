using EmployeeManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EmployeeManager.ViewModels
{
	public class EmployeeListVM : INotifyPropertyChanged
	{
		private ObservableCollection<Employee> _modelEmployeeList;
		public ObservableCollection<Employee> ModelEmployeeList
		{
			get { return _modelEmployeeList; }
			set
			{
				_modelEmployeeList = value;
				OnPropertyChanged("ModelEmployeeList");
			}
		}

		public EmployeeListVM()
		{
			LoadEmployeeList();
		}

		private Employee _selectedEmployee;
		public Employee SelectedEmployee
		{
			get
			{
				return _selectedEmployee;
			}
			set
			{
				if (value != _selectedEmployee)
				{
					var originalValue = _selectedEmployee;
					_selectedEmployee = value;

					OnPropertyChanged("SelectedCustomer");
				}
			}
		}

		private void LoadEmployeeList()
		{
			ModelEmployeeList = Employee.GetEmployees();
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged(string property)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(property));
		}
	}
}
