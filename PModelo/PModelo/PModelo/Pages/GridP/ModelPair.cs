using System;
using System.Collections.ObjectModel;

namespace PModelo.Pages.GridP
{
    public class ModelPair : Tuple<MyModel, MyModel>
    {
        public ModelPair(MyModel item1, MyModel item2)
            : base(item1, item2 ?? CreateEmptyModel()) { }

        private static MyModel CreateEmptyModel()
        {
            return new MyModel
            {
                Id = -1 // -1 indicates that view should not be rendered
            };
        }
    }
}
