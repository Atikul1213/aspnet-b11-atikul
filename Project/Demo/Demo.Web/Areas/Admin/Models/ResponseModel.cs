namespace Demo.Web.Areas.Admin.Models
{
    public class ResponseModel
    {
        public string? Message { get; set; }
        public ResponseTypes Type { get; set; }
    }

    public enum ResponseTypes
    {
        Success,
        Error,
    }
}
