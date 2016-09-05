namespace MvcApp.ViewModels
{
    public class FullProfileInfo
    {
        public MvcProfile Profile { get; set; }
        public GenericPaginationModel<MvcCategory> Categories { get; set; }
    }
}