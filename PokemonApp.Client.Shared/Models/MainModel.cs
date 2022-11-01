using KVerbruggen.Framework.Models;

namespace PokemonApp.Client.Shared.Models
{
    /// <summary>
    /// The model for the main window
    /// </summary>
    public class MainModel : ModelBase
    {
        protected override IEnumerable<IModelBase> ChildModels => new IModelBase[] { };

        public MainModel()
        {
        }
    }
}
