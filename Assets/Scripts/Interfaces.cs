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

public interface IDeBuildable<T>
{
    void DeBuild (T amountDeconstructed);
}

public interface IBuildable<T>
{
    void Build (T amountDeconstructed);
}

public interface ICanAttack<T>
{
    void Attack ();
}