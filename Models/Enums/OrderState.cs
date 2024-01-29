using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPMVC.Models.Enums
{
	public enum OrderState
	{
		Nowe = 1,          
		Przetwarzane = 2,    
		Wyslane = 3,   
		Anulowane = 4
	}
}