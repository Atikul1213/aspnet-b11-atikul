namespace DevSkill.Inventory.Application.Exceptions
{
    public class DuplicateProductBarCodeException : Exception
    {
        public DuplicateProductBarCodeException() : base("Duplicate product BarCode exception")
        {
        }
        public DuplicateProductBarCodeException(string msg) : base(msg)
        {
        }
    }
}
