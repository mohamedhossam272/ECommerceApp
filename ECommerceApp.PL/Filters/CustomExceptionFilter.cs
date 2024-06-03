using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace RestaurantReservationSystem.PL.Filters
{
	public class CustomExceptionFilter : IExceptionFilter
	{
		public void OnException(ExceptionContext context)
		{
			if (context.Exception is UnauthorizedAccessException)
			{
				context.Result = new RedirectToActionResult("AccessDenied", "Account", null);
			}
			else
			{
				// Handle other exceptions if needed
				context.Result = new RedirectToActionResult("Error", "Home", null);
			}
			context.ExceptionHandled = true;
		}
	}
}
