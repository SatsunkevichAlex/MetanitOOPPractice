namespace BankLibary
{
    public interface IAccount
    {
        void Put(decimal sum);

        decimal Withdraw(decimal sum);
    }
}
