namespace Playground.Domain.Entities;
public sealed class CodeBar
{
    public static class Rules 
    {
        public const int CODE_MAX_LENGTH = 50;
    }

    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
}