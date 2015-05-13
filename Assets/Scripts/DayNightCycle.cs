using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DayNightCycle : MonoBehaviour {

    public float degPerSec;
    public Text timeText;
    public float counter;

    private string remainingTime;
    
    public float timeSpeed = 1f;
    
	void Start () {
        transform.localEulerAngles = new Vector3(180-counter, 90f, 0f);
	}
	
	// Update is called once per frame
	void Update () {

        //transform.Rotate(-degPerSec * Time.deltaTime, 0f, 0f);
        transform.localEulerAngles = new Vector3(180 - counter, 90f, 0f);
        counter += Time.deltaTime;
        if (counter > 360) counter -= 360f;

        int lightAngle = Mathf.RoundToInt(counter);

        if (lightAngle < 180)
        {
            remainingTime = lightAngle + " in Daytime" + System.Environment.NewLine 
                + (180 - lightAngle) + " seconds until nightfall";
            timeText.color = Color.black;
        }
        else
        {
            remainingTime = lightAngle + " in Nighttime" + System.Environment.NewLine 
                + (360 - lightAngle) + " seconds until daybreak";
            timeText.color = Color.white;
        }

        timeText.text = System.DateTime.Now.ToString("T") + System.Environment.NewLine
            + remainingTime;

        Time.timeScale = timeSpeed;
	}
}
