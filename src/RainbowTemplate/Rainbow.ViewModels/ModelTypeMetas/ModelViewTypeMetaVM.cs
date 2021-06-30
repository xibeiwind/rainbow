using System.Collections.Generic;

namespace Rainbow.ViewModels.ModelTypeMetas
{
    public abstract class ModelViewTypeMetaVM
    {
        public ViewType Type { get; set; }
        public string Url { get; set; }
    }
    public class ListModelViewTypeMetaVM : ModelViewTypeMetaVM
    {
        public ListModelViewTypeMetaVM()
        {
            Type = ViewType.List;
        }
        public List<ListFieldVM> Fields { get; set; }
    }
    public class DetailModelViewTypeMetaVM : ModelViewTypeMetaVM
    {
        public DetailModelViewTypeMetaVM()
        {
            Type = ViewType.Detail;
        }
        public List<DetailFieldVM> Fields { get; set; }
    }

    public class CreateModelViewTypeMetaVM : ModelViewTypeMetaVM
    {
        public CreateModelViewTypeMetaVM()
        {
            Type = ViewType.Create;
        }

        public List<EditFieldVM> Fields { get; set; }
    }

    public class EditModelViewTypeMetaVM : ModelViewTypeMetaVM
    {
        public EditModelViewTypeMetaVM()
        {
            Type = ViewType.Edit;
        }

        public List<EditFieldVM> Fields { get; set; }
    }

    public class DetailFieldVM
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public FieldInputType InputType { get; set; }
    }

    public class EditFieldVM
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public FieldInputType InputType { get; set; }
    }
    public class ListFieldVM
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public int? Width { get; set; }
        public FieldInputType InputType { get; set; }
    }
}
