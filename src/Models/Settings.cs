namespace SpendManagement.Receipts.Api.Models
{
    public interface ISettings
    {
        ReceiptsCommandHandlerApi ReceiptsCommandHandlerApi { get; }
        ReceiptsQueryHandlerApi ReceiptsQueryHandlerApi { get; }
        ReceiptsDomainApi ReceiptsDomainApi { get; }
        ReceiptsEventHandler ReceiptsEventHandler { get; }
    }
    public record Settings : ISettings
    {
        public ReceiptsCommandHandlerApi ReceiptsCommandHandlerApi { get; set; } = null!;
        public ReceiptsQueryHandlerApi ReceiptsQueryHandlerApi { get; set; } = null!;
        public ReceiptsDomainApi ReceiptsDomainApi { get; set; } = null!;
        public ReceiptsEventHandler ReceiptsEventHandler { get; set; } = null!;
    }
}
