using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HashIDs : MonoBehaviour {

    public GameObject guiStructure;
    public static GameObject GuiStructure;
    public Text structureText;
    public static Text StructureText;

    public GameObject guiUnit;
    public static GameObject GuiUnit;
    public Text unitText;
    public static Text UnitText;
    
    // Use this for initialization
	void Awake () {
        GuiStructure = guiStructure;
        StructureText = structureText;

        GuiUnit = guiUnit;
        UnitText = unitText;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
