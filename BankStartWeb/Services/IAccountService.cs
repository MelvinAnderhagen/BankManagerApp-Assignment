namespace BankStartWeb.Services
{
    public interface IAccountService
    {
        ErrorCode Deposit(int accountId, decimal amount);
        ErrorCode WithDraw(int accountId, decimal amount);
        ErrorCode Transfer(int accountId, decimal amount, int recieverId);
        public enum ErrorCode
        {
            ok,
            BalanceIsToLow,
            AmountIsNegative
        }
    }
}
