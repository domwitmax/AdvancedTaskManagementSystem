namespace AdvancedTaskManagementSystemCRUD.WebApi.Request;

public class UpdateLabelRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
}
