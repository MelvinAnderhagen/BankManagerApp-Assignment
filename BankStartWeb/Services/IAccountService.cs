namespace BankStartWeb.Services
{
    public interface IAccountService
    {
        ErrorCode Deposit(int accountId, decimal amount);
        ErrorCode WithDraw(int accountId, decimal amount, string type);
        public enum ErrorCode
        {
            ok,
            BalanceIsToLow,
            AmountIsNegative
        }
    }
}
