using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevSkill.Inventory.Infrastructure.Extensions
{
    public static class EnumHelper
    {
        public static List<SelectListItem> PrepareSelectList<TEnum>() where TEnum : Enum
        {
            return Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .Select(e => new SelectListItem
                {
                    Value = Convert.ToInt32(e).ToString(),  // or e.ToString() if you want names
                    Text = e.ToString()
                }).ToList();
        }

        public static List<SelectListItem> PrepareSelectListFromEntities<TEntity, TKey>(IEnumerable<TEntity> entities,
        Func<TEntity, TKey> getValue,
        Func<TEntity, string> getText)
        {
            return entities.Select(e => new SelectListItem
            {
                Value = getValue(e).ToString(),
                Text = getText(e)
            }).ToList();
        }
    }
}
