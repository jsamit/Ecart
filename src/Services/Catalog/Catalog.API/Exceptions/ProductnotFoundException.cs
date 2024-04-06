namespace Catalog.API.Exceptions
{
    public class ProductnotFoundException : Exception
    {
        public ProductnotFoundException() : base("Product Not Found!") { }
    }
}
