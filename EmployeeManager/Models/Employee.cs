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
	public class Employee : INotifyPropertyChanged, IDataErrorInfo
	{
		public const double MinimumWage = 7.25;

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

		private bool _isHourly;
		public bool IsHourly
		{
			get { return _isHourly; }
			set
			{
				_isHourly = value;
				OnPropertyChanged("IsHourly");
			}
		}

		private double _wage;
		public double Wage
		{
			get { return _wage; }
			set
			{
				_wage = value;
				OnPropertyChanged("Wage");
			}
		}

		public Employee()
		{
			IsHourly = true;
			Id = "New Employee";
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

		static List<string> ValidatedProperties
		{
			get
			{
				return new List<string>{
					"FirstName",
					"LastName",
					"Email",
					"PhoneNumber",
					"Wage",
				};
			}
		}

		/// <summary>
		/// Returns true if this object has no validation errors.
		/// </summary>
		public bool IsValid
		{
			get
			{
				foreach (string property in ValidatedProperties)
				{

					if (this[property] != null) // there is an error
						return false;
				}

				return true;
			}
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

				if (columnName == "Wage")
				{
					if (Wage < MinimumWage)
						result = "Enter a valid wage";
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
}
