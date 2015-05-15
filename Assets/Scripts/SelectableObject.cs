using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectableObject : MonoBehaviour, ISelectable
{

    public SelectType selectType;

    // Use this for initialization
    protected virtual void Awake ()
    {

    }

    protected virtual void Start ()
    {

    }

    // Update is called once per frame
    protected virtual void Update ()
    {

    }

    public virtual void Select ()
    {
    }

    public virtual string Status
    {
        get
        {
            return System.String.Empty;
        }
    }
}

public enum SelectType
{
    NameOnly,
    Structure,
    Unit,
    Placeholder
}

