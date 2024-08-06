using Microsoft.AspNetCore.Components;
using BlazorApp1.Models;

namespace BlazorApp1.Components.Shared;

public partial class UserMenu
{
    [Parameter] public string Class { get; set; }
    [EditorRequired] [Parameter] public UserModel User { get; set; }
}