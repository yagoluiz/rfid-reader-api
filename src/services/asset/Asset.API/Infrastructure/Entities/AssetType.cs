namespace Asset.API.Infrastructure.Entities
{
    public class AssetType
    {
        public AssetType(string name)
        {
            Name = name;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
    }
}
