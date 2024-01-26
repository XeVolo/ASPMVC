using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPMVC.Models.Enums
{
	public enum OrderState
	{
		New = 1,          
		InProgress = 2,    
		Completed = 3,   
		Canceled = 4
	}
}