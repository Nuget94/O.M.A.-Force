using System;
using UnityEngine;

public class DanceMove : MonoBehaviour {

    public Vector2 Jump = Vector2.zero;

    virtual public void reset()
    {
        
    }

    virtual public Boolean hasNext()
    {
        return false;
    }

    virtual public DanceMoveAndTime nextMove()
    {
        return null;
    }

    virtual public void nextIdx()
    {
        
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
