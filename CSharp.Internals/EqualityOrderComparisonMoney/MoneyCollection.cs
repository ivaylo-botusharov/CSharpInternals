using System;
using System.Collections.ObjectModel;

namespace EqualityOrderComparisonMoney
{
    public class MoneyCollection : Collection<Money>
    {
        private readonly Currency _currency;

        public MoneyCollection(string currencyCode)
        {
            _currency = new Currency(currencyCode);
        }
        
        public MoneyCollection(Currency currency)
        {
            _currency = currency;
        }
        
        protected override void InsertItem (int index, Money item)
        {
            Validate(item);
            base.InsertItem (index, item);
        }
        
        protected override void SetItem (int index, Money item)
        {
            Validate(item);
            base.SetItem (index, item);
        }
        
        private void Validate(Money item)
        {
            if (item.Currency != _currency)
            {
                throw new InvalidOperationException(
                    $"Money with currency different than the default: {_currency.Code} can't be added to MoneyCollection");
            }
        }
    }
}