﻿namespace Observer
{
    public interface ISubject
    {
        void RegisterObserver(IDataObserver o);
        void RemoveObserver(IDataObserver o);
        void NotifyMoneyUpdate();
    }

    public interface IDataObserver
    {
        void UpdateMoney(float moneyAmount);
    }
}
