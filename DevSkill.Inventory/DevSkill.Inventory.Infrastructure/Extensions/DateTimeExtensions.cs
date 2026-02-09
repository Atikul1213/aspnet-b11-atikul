namespace DevSkill.Inventory.Infrastructure.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToTableDateString(this DateTime date)
        {
            DateTime defaultDate = default;
            return date <= defaultDate ? string.Empty : date.ToString("dd MM yyyy");
        }
    }
}
