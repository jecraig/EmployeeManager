using EmployeeManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmployeeManager.Commands
{
	/// <summary>
	/// Event args for the OnSelectedEmployeeChanged event
	/// </summary>
	public class SelectedEmployeeEventArgs : EventArgs
	{
		public Employee Employee { get; set; }
	}
}
