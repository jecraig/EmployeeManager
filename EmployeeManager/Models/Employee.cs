using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EmployeeManager.Models
{
	class Employee : INotifyPropertyChanged, IDataErrorInfo
	{
		private string _id;
		public string Id
		{
			get { return _id; }
			set
			{
				_id = value;
				OnPropertyChanged("Id");
			}
		}

		private string _firstName;
		public string FirstName
		{
			get { return _firstName; }
			set
			{
				_firstName = value;
				OnPropertyChanged("FirstName");
			}
		}

		private string _lastName;
		public string LastName
		{
			get { return _lastName; }
			set
			{
				_lastName = value;
				OnPropertyChanged("LastName");
			}
		}

		private string _email;
		public string Email
		{
			get { return _email; }
			set
			{
				_email = value;
				OnPropertyChanged("Email");
			}
		}

		private string _phoneNumber;
		public string PhoneNumber
		{
			get { return _phoneNumber; }
			set
			{
				_phoneNumber = value;
				OnPropertyChanged("PhoneNumber");
			}
		}

		private EmploymentType _employmentType;
		public EmploymentType EmploymentType
		{
			get { return _employmentType; }
			set
			{
				_employmentType = value;
				OnPropertyChanged("EmploymentType");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged(string property)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(property));
		}

		public string Error
		{
			get { return null; }
		}

		public string this[string columnName]
		{
			get
			{
				string result = null;

				if (columnName == "Id")
				{
					if (Id.Count() != 7 || !Regex.IsMatch(Id, "[a-zA-Z]{2}[\\d]{5}"))
						result = "Id is invalid";
				}

				if (columnName == "FirstName")
				{
					if (string.IsNullOrEmpty(FirstName))
						result = "Please enter a first name";
				}

				if (columnName == "LastName")
				{
					if (string.IsNullOrEmpty(LastName))
						result = "Please enter a last name";
				}

				if (columnName == "EmploymentType")
				{
					if (EmploymentType == null)
						result = "Please select an employment type";
				}

				if (columnName == "Email")
				{
					if (string.IsNullOrEmpty(Email))
						result = "Please enter an email address";
				}

				if (columnName == "PhoneNumber")
				{
					if (string.IsNullOrEmpty(PhoneNumber))
						result = "Please enter a phone number";
				}

				return result;
			}
		}

		public static ObservableCollection<Employee> GetEmployees()
		{
			var employees = new ObservableCollection<Employee>();



			return employees;
		}
	}


	public enum EmploymentType
	{
		Hourly,
		Salary,
	}
}
