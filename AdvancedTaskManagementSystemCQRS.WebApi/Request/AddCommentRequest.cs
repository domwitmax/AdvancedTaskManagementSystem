namespace AdvancedTaskManagementSystemCQRS.WebApi.Request;

public class AddCommentRequest
{
    public int TaskId { get; set; }
    public string Content { get; set; }
}
