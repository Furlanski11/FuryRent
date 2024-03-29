namespace FuryRent.Core.Exceptions
{
	public class NoSuchCarException : Exception
	{
		public NoSuchCarException() { }

        public NoSuchCarException(string message) 
            : base(message)
        {
            
        }
    }
}
