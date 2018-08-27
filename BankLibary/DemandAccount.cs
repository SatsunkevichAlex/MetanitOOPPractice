namespace BankLibary
{
    public class DemandAccount : Account
    {
        public DemandAccount(decimal sum, int percentage) : base(sum, percentage)
        {
        }

        protected internal override void Open()
        {
            base.OnOpened(new AccountEventArgs("New demand account opened! Id account : " + this._id, this._sum));
        }
    }
}
