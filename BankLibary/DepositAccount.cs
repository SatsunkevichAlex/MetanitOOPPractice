namespace BankLibary
{
    public class DepositAccount : Account
    {
        public DepositAccount(decimal sum, int percentage) : base(sum, percentage)
        {
        }

        protected internal override void Open()
        {
            base.OnOpened(new AccountEventArgs("Opened new deposit account! Account id : " + _id, _sum));
        }

        public override void Put(decimal sum)
        {
            if (_days > 30)
            {
                base.Put(sum);
            }
            else
            {
                base.OnAdded(new AccountEventArgs("You can put money on deposit account only after 30 days", 0));
            }
        }

        public override decimal Withdraw(decimal sum)
        {
            if (_days >= 30)
            {
                return base.Withdraw(sum);
            }
            else
            {
                base.OnWithdrawed(new AccountEventArgs("You can withdrawed money only after 30 days", 0));
                return 0;
            }
        }

        protected internal override void Calculate()
        {
            if (_days % 30 == 0)
            {
                base.Calculate();
            }
        }
    }
}
