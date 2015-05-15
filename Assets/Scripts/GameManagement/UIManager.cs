using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject canvas;

    public GameObject infoPanel;
    public Text nameText;
    public Text statusText;
    public GameObject interfacePanel;
    public GameObject unitButtons;
    public GameObject structureButtons;
    
    void Awake ()
    {
        if (null == instance)
            instance = this;
        else if (this != instance)
        {
            Destroy(this);
        }


        ResetUI();
        canvas.SetActive(true);

    }

    public void ResetUI ()
    {
        infoPanel.SetActive(false);
        interfacePanel.SetActive(false);
        unitButtons.SetActive(false);
    }

    public void DisplayUI (SelectableObject Selected)
    {
        ResetUI();
        UpdateUI(Selected);
        
        infoPanel.SetActive(true);

        if (SelectType.Unit == Selected.selectType)
        {
            interfacePanel.SetActive(true);
            unitButtons.SetActive(true);

        }
    }

    public void UpdateUI (SelectableObject Selected)
    {
        if (Selected == InputManager.instance.selectedUnit)
        {
            nameText.text = Selected.name;
            statusText.text = Selected.Status;
        }
    }

}

