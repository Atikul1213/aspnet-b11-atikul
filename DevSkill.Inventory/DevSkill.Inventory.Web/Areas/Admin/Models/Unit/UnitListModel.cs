namespace DevSkill.Inventory.Web.Areas.Admin.Models.Unit
{
    public class UnitListModel
    {
        public UnitListModel()
        {
            AddUnitModel = new AddUnitModel();
            UpdateUnitModel = new UpdateUnitModel();
            Units = new List<UnitModel>();
        }
        public AddUnitModel AddUnitModel { get; set; }
        public UpdateUnitModel UpdateUnitModel { get; set; }
        public IList<UnitModel> Units { get; set; }
    }
}
