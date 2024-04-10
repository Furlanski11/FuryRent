using System.Net;

namespace FuryRent.Core.Exceptions
{
	public class AlreadyVipException : Exception
	{
		public AlreadyVipException(string message) : base(message)
		{
			HttpStatusCode = HttpStatusCode.OK;
		}

		public HttpStatusCode HttpStatusCode { get; }
	}
}
