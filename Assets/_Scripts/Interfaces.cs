using UnityEngine;
using System.Collections;



public interface ISelectable
{
     void Select ();
     void DisplayUI ();
}

public interface IDamageable<T>
{
    void Damage (T damageTaken);
}

public interface IDestructable<T>
{
    void Destruct (T amountDeconstructed);
}

public interface IConstructable<T>
{
    void Construct (T amountDeconstructed);
}