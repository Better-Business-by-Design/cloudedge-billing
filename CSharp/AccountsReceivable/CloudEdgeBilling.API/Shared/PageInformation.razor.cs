using Microsoft.AspNetCore.Components;

namespace AccountsReceivable.API.Shared;

partial class PageInformation
{
    [Parameter]
    public RenderFragment? Title { get; set; }

    [Parameter]
    public RenderFragment? Description { get; set; }

    [Parameter]
    public RenderFragment? Interactions { get; set; }
}