using System.Net;

namespace FuryRent.Core.Exceptions
{
	public class NoSuchCarException : Exception
	{
		public NoSuchCarException(string message) : base(message)
		{
			HttpStatusCode = HttpStatusCode.NotFound;
		}

		public HttpStatusCode HttpStatusCode { get; }
	}
}
