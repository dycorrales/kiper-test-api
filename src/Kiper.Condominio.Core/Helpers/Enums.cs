using System.ComponentModel;

namespace Kiper.Condominio.Core.Helpers
{
    public enum NotificationType { Error, Info }

    public enum Status
    {
        Active = 1, Inactive = 2, Deleted = 3
    };

    public enum TransactionType
    {
        Income = 1,
        Expense = 2,
        Transaction = 3
    }

    public enum InvoiceFilterType
    {
        [Description("Fatura fechada")]
        ClosedInvoice = 1,
        [Description("Fatura em aberto")]
        OpenInvoice = 2
    }

    public enum FlagType
    {
        [Description("Visa")]
        Visa = 1,
        [Description("Master Card")]
        MasterCard = 2,
        [Description("Elo")]
        Elo = 3,
        [Description("Outros")]
        Other = 4
    }

    public enum CategoryType
    {
        [Description("Receita")]
        Income = 1,
        [Description("Despeça")]
        Expense = 2,
        [Description("Ambos")]
        Unclassified = 3
    }

    public enum AgentType
    {
        [Description("Fornecedor")]
        Provider = 1,
        [Description("Cliente")]
        Client = 2,
        [Description("Ambos")]
        Unclassified = 3
    }

    public enum RecurrencyPeriodType
    {
        [Description("Diario")]
        Daily = 1,
        [Description("Semanal")]
        Weekly = 2,
        [Description("Mensal")]
        Monthly = 3,
        [Description("Anual")]
        Yearly = 4
    }

    public enum AccountType
    {
        [Description("Conta corrente")]
        CurrentAccount = 1,
        [Description("Dinheiro")]
        Cash = 2,
        [Description("Poupança")]
        SavaingAccount = 3,
        [Description("Investimento")]
        InvestmentAccount = 4,
        [Description("Outros")]
        Unclassified = 5
    }
}
