using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Extensions
{
	public class RequestResult
	{
		public ResultStatus status { get; set; }
		public string message { get; set; }
		public object result { get; set; }
	}

	public class CustomOK
	{
		public string status { get; set; } = ResultStatus.Ok.GetText();
		public string type { get; set; } = "https://tools.ietf.org/html/rfc7231#section-6.3.1";
		public string title { get; set; }
		public object result { get; set; }
		public CustomOK(string title, object result)
		{
			this.title = title;
			this.result = result;
		}
	}
	public class CustomCreated
	{
		public string status { get; set; } = ResultStatus.Created.GetText();
		public string type { get; set; } = "https://tools.ietf.org/html/rfc7231#section-6.3.2";
		public string title { get; set; }
		public object result { get; set; }
		public CustomCreated(string title, object result)
		{
			this.title = title;
			this.result = result;
		}
	}
	public class CustomAccepted
	{
		public string status { get; set; } = ResultStatus.Accepted.GetText();
		public string type { get; set; } = "https://tools.ietf.org/html/rfc7231#section-6.3.3";
		public string title { get; set; }
		public CustomAccepted(string title)
		{
			this.title = title;
		}
	}
	public class CustomBadRequest
	{
		public string status { get; set; } = ResultStatus.BadRequest.GetText();
		public string type { get; set; } = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
		public string title { get; set; }
		public object errors { get; set; }
		public CustomBadRequest(string title, object result)
		{
			this.title = title;
			this.errors = result;
		}
	}
	public class CustomUnauthorized
	{
		public string status { get; set; } = ResultStatus.Unauthorized.GetText();
		public string type { get; set; } = "https://tools.ietf.org/html/rfc7231#section-6.5.3";
		public string title { get; set; }
		public CustomUnauthorized(string title)
		{
			this.title = title;
		}
	}
	public class CustomForbidden
	{
		public string status { get; set; } = ResultStatus.Forbidden.GetText();
		public string type { get; set; } = "https://tools.ietf.org/html/rfc7231#section-6.5.3";
		public string title { get; set; }
		public CustomForbidden(string title)
		{
			this.title = title;
		}
	}
	public class CustomUnprocessableEntity
	{
		public string status { get; set; } = ResultStatus.UnprocessableEntity.GetText();
		public string type { get; set; } = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
		public string title { get; set; }
		public CustomUnprocessableEntity(string title)
		{
			this.title = title;
		}
	}
	public class CustomInternalServerError
	{
		public string status { get; set; } = ResultStatus.InternalServerError.GetText();
		public string type { get; set; } = "https://tools.ietf.org/html/rfc7231#section-6.6.1";
		public string title { get; set; }
		public string error { get; set; }
		public CustomInternalServerError(string title, string error)
		{
			this.title = title;
			this.error = error;
		}
	}


	public enum ResultStatus
	{
		[Description("200 Ok")]
		Ok = 200,
		[Description("201 Created")]
		Created = 201,
		[Description("202 Accepted")]
		Accepted = 202,
		[Description("400 Bad Request")]
		BadRequest = 400,
		[Description("401 Unauthorized")]
		Unauthorized = 401,
		[Description("403 Forbidden")]
		Forbidden = 403,
		[Description("422 Unprocessable Entity")]
		UnprocessableEntity = 422,
		[Description("500 Internal Server Error")]
		InternalServerError = 500
	}
	public static class ResultStatusEnumer
	{

		public static string GetText(this ResultStatus environment)
		{
			// get the field 
			var field = environment.GetType().GetField(environment.ToString());
			var customAttributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

			if (customAttributes.Length > 0)
				return (customAttributes[0] as DescriptionAttribute).Description;
			else
				return environment.ToString();
		}
	}
}
