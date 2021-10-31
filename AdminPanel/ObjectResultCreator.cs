using Microsoft.AspNetCore.Mvc;
using AdminPanel.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.WEB
{
	public class ObjectResultCreator
	{
		public ObjectResult CreateObjectResult(RequestResult requestResult)
		{
			switch (requestResult.status)
			{
				case ResultStatus.Ok:
					var Ok = new ObjectResult(new CustomOK(requestResult.message, requestResult.result));
					Ok.StatusCode = (int)requestResult.status;
					return Ok;

				case ResultStatus.Created:
					var Created = new ObjectResult(new CustomCreated(requestResult.message, requestResult.result));
					Created.StatusCode = (int)requestResult.status;
					return Created;

				case ResultStatus.Accepted:
					var Accepted = new ObjectResult(new CustomAccepted(requestResult.message));
					Accepted.StatusCode = (int)requestResult.status;
					return Accepted;

				case ResultStatus.BadRequest:
					var BadRequst = new ObjectResult(new CustomBadRequest(requestResult.message, requestResult.result));
					BadRequst.StatusCode = (int)requestResult.status;
					return BadRequst;

				case ResultStatus.Unauthorized:
					var Unauthorized = new ObjectResult(new CustomUnauthorized(requestResult.message));
					Unauthorized.StatusCode = (int)requestResult.status;
					return Unauthorized;

				case ResultStatus.Forbidden:
					var Forbidden = new ObjectResult(new CustomForbidden(requestResult.message));
					Forbidden.StatusCode = (int)requestResult.status;
					return Forbidden;

				case ResultStatus.UnprocessableEntity:
					var UnprocessableEntity = new ObjectResult(new CustomUnprocessableEntity(requestResult.message));
					UnprocessableEntity.StatusCode = (int)requestResult.status;
					return UnprocessableEntity;

				default:
					var InternalServerError = new ObjectResult(new CustomInternalServerError("", requestResult.message));
					InternalServerError.StatusCode = (int)requestResult.status;
					return InternalServerError;
			}
		}
	}
}
