namespace SpendManagement.Receipts.Api.Models
{
    public interface ISettings
    {
        SpendManagementIdentityApi? SpendManagementIdentityApi { get; }
        ReceiptsCommandHandlerApi? ReceiptsCommandHandlerApi { get; }
        ReceiptsQueryHandlerApi? ReceiptsQueryHandlerApi { get; }
        ReceiptsDomainApi? ReceiptsDomainApi { get; }
        ReceiptsEventHandler? ReceiptsEventHandler { get; }
    }
    public record Settings : ISettings
    {
        public SpendManagementIdentityApi? SpendManagementIdentityApi { get; set; }
        public ReceiptsCommandHandlerApi? ReceiptsCommandHandlerApi { get; set; }
        public ReceiptsQueryHandlerApi? ReceiptsQueryHandlerApi { get; set; }
        public ReceiptsDomainApi? ReceiptsDomainApi { get; set; }
        public ReceiptsEventHandler? ReceiptsEventHandler { get; set; }
    }
}
