/// <summary>
/// Representa una cuenta bancaria con operaciones de débito y crédito.
/// </summary>
public class BankAccount
{
    /// <summary>
    /// Mensaje para cuando el monto a debitar excede el saldo.
    /// </summary>
    public const string DebitAmountExceedsBalanceMessage = "Debit amount exceeds balance";

    /// <summary>
    /// Mensaje para cuando el monto a debitar es menor a cero.
    /// </summary>
    public const string DebitAmountLessThanZeroMessage = "Debit amount is less than zero";

    /// <summary>
    /// Mensaje para cuando el monto del crédito es menor a cero.
    /// </summary>
    public const string CreditAmountLessThanOrEqualToZeroMessage = "Credit amount must be positive";

    private readonly string m_customerName = string.Empty;
    private double m_balance;

    /// <summary>
    /// Constructor para pruebas internas.
    /// </summary>
    private BankAccount() { }

    /// <summary>
    /// Inicializa una nueva instancia de <see cref="BankAccount"/>.
    /// </summary>
    public BankAccount(string customerName, double balance)
    {
        m_customerName = customerName;
        m_balance = balance;
    }

    /// <summary>
    /// Obtiene el nombre del cliente.
    /// </summary>
    public string CustomerName => m_customerName;

    /// <summary>
    /// Obtiene el saldo actual.
    /// </summary>
    public double Balance => m_balance;

    /// <summary>
    /// Debita una cantidad de la cuenta.
    /// </summary>
    public void Debit(double amount)
    {
        if (amount > m_balance)
            throw new ArgumentOutOfRangeException("amount", amount, DebitAmountExceedsBalanceMessage);
        if (amount < 0)
            throw new ArgumentOutOfRangeException("amount", amount, DebitAmountLessThanZeroMessage);
        m_balance -= amount;
    }

    /// <summary>
    /// Acredita una cantidad a la cuenta.
    /// </summary>
    public void Credit(double amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException("amount", amount, "Credit amount must be positive");
        m_balance += amount;
    }
}