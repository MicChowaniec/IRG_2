using System;
using static UnityEngine.EventSystems.EventTrigger;

public interface IDamagable
{
    public void Grow(int Addedprotein)
    {
        throw new NotImplementedException();

    }

    public void ReceiveDamage(int amount)
    {
        throw new NotImplementedException();
    }

    public void Disease(int diseaseLvl)
    {
        throw new NotImplementedException();
    }

    public void WaterUpdate(int water)
    {
        throw new NotImplementedException();
    }
    public void Refill(int water)
    {
        throw new NotImplementedException();
    }

    public void CheckForDeath()
    {
        throw new NotImplementedException();
    }
    
}