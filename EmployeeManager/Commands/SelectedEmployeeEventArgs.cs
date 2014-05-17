using EmployeeManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmployeeManager.Commands
{
	public class SelectedEmployeeEventArgs : EventArgs
	{
		public Employee Employee { get; set; }
	}
}
