namespace NextAmongUsLauncher.Core.Base;

public class TextNode : IDisposable
{
    public static List<TextNode> AllNodes { get; set; } = new();
    
    public TextNode? Parent { get; set; }
    
    public TextNode[] Children { get; set; } = Array.Empty<TextNode>();

    public string Text;

    public string Key;

    public int Id { get; private set; }

    public TextNode(string text, string key)
    {
        Text = text;
        Key = key;
        Id = GetId();        
        AllNodes.Add(this);
    }

    private static int GetId()
    {
        var id = AllNodes.Count;
        while (AllNodes.Any(n => n.Id == id))
        {
            id++;
        }

        return id;
    }

    public void Dispose()
    {
        AllNodes.Remove(this);
        Parent = null;
        Children = Array.Empty<TextNode>();
        Text = string.Empty;
        Key = string.Empty;
        Id = -1;
    }
    
    ~TextNode()
    {
        Dispose();
    }
}