using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public GameObject canvas;

    public GameObject infoPanel;
    public Text nameText;
    public Text statusText;
    public GameObject interfacePanel;
    public GameObject unitButtons;
    public GameObject structureButtons;

    
    void Awake ()
    {
    
        ResetUI();
        canvas.SetActive(true);

        Debug.Log("UI ready");

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
        if (Selected == GameManager.inputManager.selectedUnit)
        {
            nameText.text = Selected.name;
            statusText.text = Selected.Status;
        }
    }

}

