﻿using EmployeeManager.Commands;
using EmployeeManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

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
			Initialize();
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
					_selectedEmployee = value;
					Mediator.GetInstance().OnSelectedEmployeeChanged(this, _selectedEmployee);
					OnPropertyChanged("SelectedCustomer");
				}
			}
		}

		private ICommand _newEmployeeCommand;
		public ICommand NewEmployeeCommand
		{
			get { return _newEmployeeCommand; }
			set
			{
				_newEmployeeCommand = value;
				OnPropertyChanged("NewEmployeeCommand");
			}
		}

		private void Initialize()
		{
			NewEmployeeCommand = new NewEmployeeCommand(NewEmployee);
		}

		private void NewEmployee()
		{
			Mediator.GetInstance().OnSelectedEmployeeChanged(this, null);
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
