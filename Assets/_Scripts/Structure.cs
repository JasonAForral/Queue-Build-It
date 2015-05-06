using UnityEngine;
using System.Collections;

public class Structure : MonoBehaviour, ISelectable, IConstructable<float>, IDestructable<float>, IDamageable<float>
{

    // Use this for initialization
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {

    }

    public void Select ()
    {
    }

    public void DisplayUI ()
    {
    }

    public void Damage (float damageTaken) { }
    public void Destruct (float amountDeconstructed) { }

    public void Construct (float amountDeconstructed) { }
}
