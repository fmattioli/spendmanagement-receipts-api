namespace SpendManagement.Receipts.Api.Models
{
    public interface ISettings
    {
        ReceiptsCommandHandlerApi ReceiptsCommandHandlerApi { get; }
        ReceiptsQueryHandlerApi ReceiptsQueryHandlerApi { get; }
        ReceiptsDomainApi ReceiptsDomainApi { get; }
        ReceiptsEventHandler ReceiptsEventHandler { get; }
        public MltConfigsSettings MltConfigsSettings { get; }
    }
    public record Settings : ISettings
    {
        public required ReceiptsCommandHandlerApi ReceiptsCommandHandlerApi { get; set; }
        public required ReceiptsQueryHandlerApi ReceiptsQueryHandlerApi { get; set; }
        public required ReceiptsDomainApi ReceiptsDomainApi { get; set; }
        public required ReceiptsEventHandler ReceiptsEventHandler { get; set; }
        public required MltConfigsSettings MltConfigsSettings { get; set; }
    }
}
