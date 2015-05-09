using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Structure : MonoBehaviour, ISelectable, IConstructable<float>, IDestructable<float>, IDamageable<float>
{

    public GameObject guiPanel;
    private Text guiTextDisplay;

    // Use this for initialization
    void Awake ()
    {
        guiPanel = GameObject.FindGameObjectWithTag("GUIStructure");
        guiTextDisplay = guiPanel.GetComponentInChildren<Text>();

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
        guiTextDisplay.text = "Structure: " + name;
        guiPanel.SetActive(true);

    }

    public void Damage (float damageTaken) { }
    public void Destruct (float amountDeconstructed) { }

    public void Construct (float amountDeconstructed) { }
}
