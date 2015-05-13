using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DayNightCycle : MonoBehaviour {

    public float degPerSec;
    public Text timeText;
    public float counter;

    private string remainingTime;
    private bool dayTime;

    public float timeSpeed = 1f;
    
	void Start () {
        transform.localEulerAngles = new Vector3(180-counter, 90f, 0f);
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(-degPerSec * Time.deltaTime, 0f, 0f);
        counter += Time.deltaTime;
        if (counter > 360) counter -= 360f;

        int lightAngle = Mathf.RoundToInt(counter);

        if (lightAngle < 180)
        {
            remainingTime = (180 - lightAngle) + " seconds until nightfall";
            timeText.color = Color.black;
        }
        else
        {
            remainingTime = (360 - lightAngle) + " seconds until daybreak";
            timeText.color = Color.white;
        }

        timeText.text = System.DateTime.Now.ToString("T") + System.Environment.NewLine
            + lightAngle+ (dayTime ? " in Daytime" : " in Nighttime") + System.Environment.NewLine
            + remainingTime;

        Time.timeScale = timeSpeed;
	}
}
