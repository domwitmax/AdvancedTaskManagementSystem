namespace AdvancedTaskManagementSystemCQRS.WebApi.Request;

public class UpdateContentCommentRequest
{
    public int CommentId { get; set; }
    public string Content { get; set; }
}
