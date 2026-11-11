namespace MatResurs.Models;

public sealed class SiteTheme
{
    public string Title { get; set; } = "МатРесурс";
    public string Primary { get; set; } = "#366cf0";
    public string Accent { get; set; } = "#13b8a7";
    public string Sidebar { get; set; } = "#10213f";
    public string Background { get; set; } = "#f3f7fc";
    public string Font { get; set; } = "system";
    public int Radius { get; set; } = 16;

    public SiteTheme Copy() => new()
    {
        Title = Title, Primary = Primary, Accent = Accent, Sidebar = Sidebar,
        Background = Background, Font = Font, Radius = Radius
    };
}
