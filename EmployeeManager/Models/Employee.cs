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
		public const decimal MinimumWage = 7.25M;

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

		private decimal _wage;
		public decimal Wage
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

		public bool IsValid
		{
			get
			{
				foreach (string property in ValidatedProperties)
				{

					if (this[property] != null)
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
					if (string.IsNullOrWhiteSpace(Email) || !Regex.IsMatch(Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
						result = "Please enter an email address";
				}

				if (columnName == "PhoneNumber")
				{
					if (string.IsNullOrEmpty(PhoneNumber) || !Regex.IsMatch(PhoneNumber, @"^[2-9]\d{2}[-\s.]{0,1}\d{3}[-\s.]{0,1}\d{4}$"))
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

		public string GenerateUniqueId(Random random, ICollection<Employee> collection)
		{
			if (string.IsNullOrWhiteSpace(this.FirstName) || string.IsNullOrWhiteSpace(this.LastName))
				return null;

			string idBeginning = this.FirstName[0].ToString() + this.LastName[0].ToString();
			string id = "";
			do
			{
				id = idBeginning + random.Next(10000, 100000);
			} while (collection.FirstOrDefault(e => e.Id == id) != null);
			return id;
		}

		private static List<string> RandomNames = new List<string>
		{
			"Brandee","Fouts","Zachery","Lopresti",
			"Daniell","Praylow","Francisca","Cowans",
			"Marsha","Pasquariello","Brandon","Lamirande",
			"Jong","Premo","Franklin","Perry",
			"Preston","Malm","Nancie","Ramsey",
			"Flo","Cheney","Melda","Raynes",
			"Janee","Hedlund","Milly","Purcell",
			"Erasmo","Candelario","Percy","Greg",
			"Kristel","Maynes","Mariann","Petrarca",
			"Ross","Brewton","Darlena","Nordyk",
		};

		private static Employee GenerateRandomEmployee(Random random, ICollection<Employee> collection)
		{
			var newEmployee = new Employee();

			newEmployee.FirstName = RandomNames[random.Next(RandomNames.Count)];
			newEmployee.LastName = RandomNames[random.Next(RandomNames.Count)];
			newEmployee.Email = newEmployee.FirstName + newEmployee.LastName + "@email.com";
			newEmployee.PhoneNumber = random.Next(100, 1000).ToString() + "-" + random.Next(100, 1000).ToString() + "-" + random.Next(1000, 10000).ToString();
			newEmployee.IsHourly = random.Next(2) == 0;
			newEmployee.Wage = random.Next(8, 100000);
			newEmployee.Id = newEmployee.GenerateUniqueId(random, collection);

			return newEmployee;
		}

		private static ObservableCollection<Employee> getEmployees;
		public static ObservableCollection<Employee> GetEmployees()
		{
			if (getEmployees == null)
			{
				var employees = new ObservableCollection<Employee>();

				var random = new Random();
				for (int i = 0; i < 5; i++)
					employees.Add(GenerateRandomEmployee(random, employees));

				getEmployees = employees;
			}
			return getEmployees;
		}

		public override string ToString()
		{
			return this.LastName + ", " + this.FirstName;
		}

		public Employee Copy()
		{
			var copy = new Employee();
			copy.Id = this.Id;
			copy.FirstName = this.FirstName;
			copy.LastName = this.LastName;
			copy.IsHourly = this.IsHourly;
			copy.Email = this.Email;
			copy.PhoneNumber = this.PhoneNumber;
			copy.Wage = this.Wage;
			return copy;
		}

		public void Save(Employee newValues)
		{
			this.FirstName = newValues.FirstName;
			this.LastName = newValues.LastName;
			this.IsHourly = newValues.IsHourly;
			this.Email = newValues.Email;
			this.PhoneNumber = newValues.PhoneNumber;
			this.Wage = newValues.Wage;
		}
	}
}
