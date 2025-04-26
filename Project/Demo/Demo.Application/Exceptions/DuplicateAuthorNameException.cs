namespace Demo.Application.Exceptions
{
    public class DuplicateAuthorNameException : Exception
    {
        public DuplicateAuthorNameException() : base("Duplicate Author name Exception")
        {

        }

        public DuplicateAuthorNameException(string msg) : base(msg)
        {

        }
    }
}
