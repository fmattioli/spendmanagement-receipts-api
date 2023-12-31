namespace SpendManagement.ApiGateway.Models
{
    public interface ISettings
    {
        SpendManagementIdentity? SpendManagementIdentity { get;}
        SpendManagementApi? SpendManagementApi { get;}
        SpendManagementReadModel? SpendManagementReadModel { get;}
        SpendManagementDomain? SpendManagementDomain { get; }
        SpendManagementEventHandler? SpendManagementEventHandler { get; }
    }
    public record Settings : ISettings
    {
        public SpendManagementIdentity? SpendManagementIdentity { get; set; }
        public SpendManagementApi? SpendManagementApi { get; set; }
        public SpendManagementReadModel? SpendManagementReadModel { get; set; }
        public SpendManagementDomain? SpendManagementDomain { get; set; }
        public SpendManagementEventHandler? SpendManagementEventHandler { get; set; }
    }
}
