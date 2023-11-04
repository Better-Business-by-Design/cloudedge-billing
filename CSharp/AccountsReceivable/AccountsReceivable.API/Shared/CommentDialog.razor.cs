using AccountsReceivable.BL.Models.Account;
using AccountsReceivable.BL.Models.Application;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AccountsReceivable.API.Shared;

partial class CommentDialog : ComponentBase
{
    [CascadingParameter]
    private MudDialogInstance MudDialog { get; set; } = null!;


    
    [Parameter]
    public string DocumentId { get; set; } = null!;
    
    [Parameter]
    public ICollection<Comment> Comments { get; set; } = null!;

    [Parameter]
    public ICollection<User> Users { get; set; } = null!;
    
    [Parameter]
    public string CurrentUser { get; set; } = null!;
    
    private string CommentInput { get; set; } = string.Empty;

    private void Save()
    {
        if (string.IsNullOrWhiteSpace(CommentInput)) return;
        
        var comment = new Comment
        {
            DocumentId = DocumentId,
            UserEmailAddress = CurrentUser,
            Content = CommentInput,
            Timestamp = DateTime.Now
        };
        
        Comments.Add(comment);
        CommentInput = string.Empty;
        
        MudDialog.Close();
    }
}