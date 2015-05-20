using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectableObject : MonoBehaviour, ISelectable
{

    public SelectType selectType;

    public virtual void Select ()
    {
    }

    public virtual string Status()
    {
         return System.String.Empty; 
    }
}

public enum SelectType
{
    NameOnly,
    Structure,
    Unit,
    Placeholder
}

