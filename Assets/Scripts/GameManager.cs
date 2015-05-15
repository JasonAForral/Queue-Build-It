using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    void Awake ()
    {
        if (null == instance)
            instance = this;
        else if (this != instance)
        {
            Destroy(this);
        }
    }
}