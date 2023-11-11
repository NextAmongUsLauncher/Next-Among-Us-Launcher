namespace NextAmongUsLauncher.Core.Base;

public class TextNode
{
    public List<TextNode> AllNodes { get; set; } = new();
    
    public TextNode? Parent { get; set; }
    
    public TextNode[] Children { get; set; } = Array.Empty<TextNode>();

    public string Text;

    public string Key;

    public int Id;

    public TextNode(string text, string key)
    {
        Text = text;
        Key = key;
        Id = AllNodes.Count;        
        AllNodes.Add(this);
    }

    public override string? ToString()
    {
        return null;
    }
}