using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevSkill.Inventory.Infrastructure.Extensions
{
    public static class EnumHelper
    {
        public static List<SelectListItem> PrepareSelectList<TEnum>() where TEnum : Enum
        {
            var selectLists = Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .Select(e => new SelectListItem
                {
                    Value = Convert.ToInt32(e).ToString(),  // or e.ToString() if you want names
                    Text = e.ToString()
                }).ToList();

            selectLists.Insert(0, new SelectListItem
            {
                Text = "Select One",
                Value = "0"
            });

            return selectLists;
        }

        public static List<SelectListItem> PrepareSelectListFromEntities<TEntity, TKey>(IEnumerable<TEntity> entities,
        Func<TEntity, TKey> getValue,
        Func<TEntity, string> getText)
        {
            var selectLists = entities.Select(e => new SelectListItem
            {
                Value = getValue(e).ToString(),
                Text = getText(e)
            }).ToList();
            selectLists.Insert(0, new SelectListItem
            {
                Text = "Select One",
                Value = Guid.Empty.ToString()
            });

            return selectLists;
        }
    }
}
