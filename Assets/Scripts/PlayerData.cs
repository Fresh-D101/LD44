using System.Collections.Generic;
using Observer;


public class PlayerData : ISubject
{
    public static PlayerData Instance;

    private static float s_currentMoney = 0f;
    private static float s_totalDebt = 0f;

    private static readonly List<IDataObserver> s_observers = new List<IDataObserver>();

    public PlayerData()
    {
        Instance = this;
    }

    public PlayerData(float initialMoneyAmount) : this()
    {
        s_currentMoney = initialMoneyAmount;   
    }
    
    
    public float CurrentMoney
    {
        get => s_currentMoney;
        private set
        {
            s_currentMoney = value;
            NotifyMoneyUpdate();
        }
    }

    public float TotalDebt
    {
        get => s_totalDebt;
        private set
        {
            s_totalDebt = value;
            NotifyDebtUpdate();
        }
    }

    public void AddMoney(float amount)
    {
        CurrentMoney += amount;
    }

    public void SubstractMoney(float amount)
    {
        CurrentMoney -= amount;
    }

    public void AddToDebt(float amount)
    {
        TotalDebt += amount;
    }

    public void RemoveFromDebt(float amount)
    {
        TotalDebt -= amount;
    }

    public void RegisterObserver(IDataObserver o)
    {
        s_observers.Add(o);
    }

    public void RemoveObserver(IDataObserver o)
    {
        s_observers.Remove(o);
    }

    public void NotifyMoneyUpdate()
    {
        foreach (var observer in s_observers)
        {
            observer.UpdateMoney(CurrentMoney);
        }
    }

    public void NotifyDebtUpdate()
    {
        foreach (var observer in s_observers)
        {
            observer.UpdateDebt(TotalDebt);
        }
    }
}
