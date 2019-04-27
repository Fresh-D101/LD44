using System.Collections.Generic;
using Observer;


public class PlayerData : ISubject
{
    public static PlayerData Instance;

    private static int s_currentMoney = 0;

    private static readonly List<IDataObserver> s_observers = new List<IDataObserver>();

    public PlayerData()
    {
        Instance = this;
    }

    public PlayerData(int initialMoneyAmount) : this()
    {
        s_currentMoney = initialMoneyAmount;   
    }
    
    
    public int CurrentMoney
    {
        get => s_currentMoney;
        private set
        {
            s_currentMoney = value;
            NotifyMoneyUpdate();
        }
    }


    public void AddMoney(int amount)
    {
        CurrentMoney += amount;
    }

    public void SubstractMoney(int amount)
    {
        CurrentMoney -= amount;
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
}
