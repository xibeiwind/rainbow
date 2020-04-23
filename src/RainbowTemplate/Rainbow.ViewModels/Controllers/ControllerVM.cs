namespace Rainbow.ViewModels.Controllers
{
    public class ControllerVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class ControllerAction
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public string ReturnType { get; set; }
        public string Argument { get; set; }
    }
}