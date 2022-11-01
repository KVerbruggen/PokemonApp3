using KVerbruggen.Framework.Models;

namespace PokemonApp.Client.Shared.Models
{
    public class ProfileModel : SubmitModelBase, ISubmitModelBase
    {
        private string? _name;

        protected override IEnumerable<IModelBase> ChildModels => Array.Empty<IModelBase>();

        public string? Name
        {
            get => _name;
            set
            {
                if (value != Name)
                {
                    InvokePropertyChanged(nameof(Name), countAsUnsavedChange: true);
                    _name = value;
                }
            }
        }

        public ProfileModel()
        {
        }

        protected override Task LoadDetailsInternal()
        {
            Name = "Test";
            Key = Guid.NewGuid();
            return Task.CompletedTask;
            // throw new NotImplementedException();
        }

        protected override Task<Guid> SubmitChangesImplementation()
        {
            return Task.FromResult(Guid.Empty);
            // throw new NotImplementedException();
        }
    }
}
