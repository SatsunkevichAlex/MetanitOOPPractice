namespace BankLibary
{
    public class Account : IAccount
    {
        protected internal event AccountStateHandler Withdrawed;
        protected internal event AccountStateHandler Added;
        protected internal event AccountStateHandler Opened;
        protected internal event AccountStateHandler Closed;
        protected internal event AccountStateHandler Calculated;

        protected int _id;

        protected decimal _sum;
        protected int _percentage;

        protected static int counter = 0; 

        protected int _days = 0;

        public Account(decimal sum, int percentage)
        {
            _sum = sum;
            _percentage = percentage;
            _id = ++counter;
        }

        public decimal CurrentSum => _sum;

        public decimal Percentage => _percentage;

        public int Id => _id;

        private void CallEvent(AccountEventArgs e, AccountStateHandler handler)
        {
            if (handler != null && e != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnOpened(AccountEventArgs e)
        {
            CallEvent(e, Opened);
        }

        protected virtual void OnWithdrawed(AccountEventArgs e)
        {
            CallEvent(e, Withdrawed);
        }

        protected virtual void OnAdded(AccountEventArgs e)
        {
            CallEvent(e, Added);
        }

        protected virtual void OnClosed(AccountEventArgs e)
        {
            CallEvent(e, Closed);
        }

        protected virtual void OnCalculated(AccountEventArgs e)
        {
            CallEvent(e, Calculated);
        }

        public virtual void Put(decimal sum)
        {
            _sum += sum;
            OnAdded(new AccountEventArgs("On account added " + sum, sum));
        }

        // Не уверен в этом методе. Нужно потом проверить в консоли.
        public virtual decimal Withdraw(decimal sum)
        {
            decimal accountBalance = 0;
            if (_sum > sum)
            {
                accountBalance  = _sum - sum;
                OnWithdrawed(new AccountEventArgs("Sum " + sum + " withdrawed from account " + _id, accountBalance));
                return accountBalance;
            }
            else
            {
                OnWithdrawed(new AccountEventArgs("Not enought money on account " + _id, 0));
                return 0;
            }
        }

        protected internal virtual void Open()
        {
            OnOpened(new AccountEventArgs("Opened new account! Account id : " + this._id, this._sum));
        }

        protected internal virtual void Close()
        {
            OnClosed(new AccountEventArgs("Account " + _id + " closed. Final sum : " + this.CurrentSum, this.CurrentSum));
        }

        protected internal void IncremeneDays() => _days++;

        protected internal virtual void Calculate()
        {
            decimal increment = _sum * _percentage / 100;
            _sum += increment;
            OnCalculated(new AccountEventArgs("Add percents in amout of" + increment, increment));
        }
    }
}