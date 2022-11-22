﻿using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
	[ApiController]
	[ApiExplorerSettings(IgnoreApi = true)]
	public class ErrorController : ControllerBase
	{
		[Route("/error-development")]
		public IActionResult HandleErrorDevelopment(
			[FromServices] IHostEnvironment hostEnvironment)
		{
			if (!hostEnvironment.IsDevelopment())
			{
				return NotFound();
			}

			var exceptionHandlerFeature =
				HttpContext.Features.Get<IExceptionHandlerFeature>()!;

			return Problem(
				detail: exceptionHandlerFeature.Error.StackTrace,
				title: exceptionHandlerFeature.Error.Message);
		}

		[Route("/error")]
		public IActionResult HandleError() =>
			Problem(title: HttpContext.Features.Get<IExceptionHandlerFeature>().Error.Message);
	}
}
