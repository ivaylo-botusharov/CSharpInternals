namespace EqualityOrderComparisonMoney
{
    public class BankAccount
    {
        private AccountType AccountType { get; }
        public Money Balance { get; private set; }
        private Currency Currency { get; }
        
        public BankAccount(AccountType accountType, Currency currency)
        {
            AccountType = accountType;
            Currency = currency;
            Balance = new Money(0M, currency);
        }
        
        public BankAccount(AccountType accountType, string currencyCode)
        {
            var currency = new Currency(currencyCode);
            AccountType = accountType;
            Currency = currency;
            Balance = new Money(0M, currency);
        }
        
        public BankAccount(AccountType accountType, Money balanceAmount)
        {
            AccountType = accountType;
            Currency = balanceAmount.Currency;
            Balance = balanceAmount;
        }

        public void Deposit(Money money)
        {
            Balance += money;
        }

        public Notification Withdraw(Money money)
        {
            Notification notification = ValidateWithdrawal(money);
    
            if (notification.HasErrors)
            {
                return notification;
            }
            
            Balance -= money;

            return notification;
        }

        private Notification ValidateWithdrawal(Money money)
        {
            var note = new Notification();
            ValidateMoneyAvailability(note, money);
            return note;
        }

        private void ValidateMoneyAvailability(Notification note, Money money)
        {
            if (AccountType != AccountType.CreditAccount && Balance < money)
            {
                note.AddError(
                    $"The Balance value: {Balance.Amount} is less than withdrawal amount: {money.Amount}");
            }
        }
    }
}