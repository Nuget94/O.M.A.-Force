using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : MonoBehaviour
{
    public Boolean isLeaving = true;
    public float walkingSpeed = 1.6f;
    public List<GameObject> WayPointsLeave = new List<GameObject>();
    public List<GameObject> WayPointsEnter = new List<GameObject>();

    private int wayPointIdx = 0;

    void OnEnable()
    {
        // we need to disable all physics
        var rBody = gameObject.GetComponent<Rigidbody2D>();
        if (rBody != null)
        {
            Destroy(rBody);
        }
    }

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Boolean needsToBeTestedAgainstDoor()
    {
        return isLeaving && wayPointIdx <= 1 || !isLeaving && wayPointIdx >= 4;
    }

    void FixedUpdate()
    {
        Vector3 nextWaypoint;
        if (isLeaving)
        {
            nextWaypoint = WayPointsLeave[wayPointIdx].transform.position;
            var distanceToWayPoint = Mathf.Abs(this.transform.position.x - nextWaypoint.x);
            if (distanceToWayPoint < 0.1f)
            {
                wayPointIdx++;
                if (wayPointIdx >= WayPointsLeave.Count)
                {
                    Destroy(gameObject);
                    return;
                }
                nextWaypoint = WayPointsLeave[wayPointIdx].transform.position;
            }   
        }
        else
        {
            nextWaypoint = WayPointsEnter[wayPointIdx].transform.position;
            var distanceToWayPoint = Mathf.Abs(this.transform.position.x - nextWaypoint.x);
            if (distanceToWayPoint < 0.1f)
            {
                wayPointIdx++;
                if (wayPointIdx >= WayPointsEnter.Count)
                {
                    this.enabled = false;
                    this.gameObject.AddComponent<Rigidbody2D>();
                    this.gameObject.GetComponent<Dancer>().setupRigidBody();
                    return;
                }
                nextWaypoint = WayPointsLeave[wayPointIdx].transform.position;
            }
        }
        var direction = (nextWaypoint - transform.position).normalized;
        var translation = direction * walkingSpeed * Time.fixedDeltaTime;
        this.transform.position += translation;
    }
}
