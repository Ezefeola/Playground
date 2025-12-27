namespace Playground.Domain.Entities;
public sealed class Product
{
    public static class Rules
    {
        public const int SKU_MAX_LENGTH = 20;
        public const int DESCRIPTION_MAX_LENGTH = 200;
    }

    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Sku { get; set; } = string.Empty;
    public int CodeBarId { get; set; }
    public CodeBar? CodeBar { get; set; }
}