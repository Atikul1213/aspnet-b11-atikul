namespace DevSkill.Inventory.Application.Exceptions
{
    public class DuplicateProductSkuException : Exception
    {
        public DuplicateProductSkuException() : base("Duplicate product sku exception")
        {
        }
        public DuplicateProductSkuException(string msg) : base(msg)
        {
        }
    }
}
