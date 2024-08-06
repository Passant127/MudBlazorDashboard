using MudBlazor;
using MudBlazor.Utilities;

namespace BlazorApp1.Components.Index;

public partial class MudBlazorCard : MudComponentBase
{
    private string Classname =>
        new CssBuilder()
            .AddClass(Class)
            .Build();
}