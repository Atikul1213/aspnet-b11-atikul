using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models.Unit
{
    public class UpdateUnitModel
    {
        public UpdateUnitModel()
        {
            Status = new List<SelectListItem>();
        }
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int StatusId { get; set; }
        public DateTime CreateOnUtc { get; set; }
        public IEnumerable<SelectListItem> Status { get; set; }
    }
}
