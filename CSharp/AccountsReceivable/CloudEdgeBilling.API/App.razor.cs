using MudBlazor;

namespace CloudEdgeBilling.API;

partial class App
{
    private readonly MudTheme _currentTheme = new()
    {
        Palette = new PaletteLight
        {
            Dark = "#00263E",
            AppbarBackground = Colors.Blue.Lighten2
        }
    };
}