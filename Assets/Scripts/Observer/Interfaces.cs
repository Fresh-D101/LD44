namespace Observer
{
    public interface ISubject
    {
        void RegisterObserver(IDataObserver o);
        void RemoveObserver(IDataObserver o);
        void NotifyMoneyUpdate();
        void NotifyDebtUpdate();
    }

    public interface IDataObserver
    {
        void UpdateMoney(float moneyAmount);
        void UpdateDebt(float debtAmount);
    }
}
