using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectableObject : MonoBehaviour, ISelectable
{

    public GameObject guiPanel;
    protected Text guiTextDisplay;
    

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

    public virtual void DisplayUI ()
    {
        guiPanel.SetActive(true);
    }

}
