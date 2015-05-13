using UnityEngine;
using System.Collections;

public class Builder : Unit {
    public Transform[] jobWaypoints;

    public short JobIndex = 0;

	protected override void Start ()
    {
        base.Start();
    }
	
	protected override void Update ()
    {
        base.Update();

        
        
        // builder has jobWaypoints
        if (0 < jobWaypoints.Length)
        {
        
        }

    }

    public void BuildInput ()
    {
        Debug.Log("Where should " + name + " build?");
    }

    public void BuildOrder ()
    {
        
    }

    IEnumerable BuildJob ()
    {
        yield return null;
    }
}
