namespace AdvancedTaskManagementSystemCRUD.Domain.Constants;

public static class RoleNames
{
    public const string Administrator = "Administrator";
    public const string ProjectManager = "ProjectManager";
    public const string TeamMember = "TeamMember";
    public const string ProjectManagement = Administrator + "," + ProjectManager;
}
