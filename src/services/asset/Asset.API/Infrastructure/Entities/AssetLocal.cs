namespace Asset.API.Infrastructure.Entities
{
    public class AssetLocal
    {
        public AssetLocal(string name, string description = null)
        {
            Name = name;
            Description = description;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
    }
}
