using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : MonoBehaviour
{
    public Boolean isLeaving = true;
    public float walkingSpeed = 4f;
    public float walkingHeight = 1.6f;
    public List<GameObject> WayPointsLeave = new List<GameObject>();
    public List<GameObject> WayPointsEnter = new List<GameObject>();
    public List<GameObject> RoomStartLocations = new List<GameObject>();

    private int wayPointIdx = 0;

    public void reset(Boolean isLeaving)
    {
        wayPointIdx = 0;
        this.isLeaving = isLeaving;
        if (!isLeaving)
        {
            // select the location where we start dancing
            var roomLocation = UnityEngine.Random.Range(0, RoomStartLocations.Count);
            WayPointsEnter.Add(RoomStartLocations[roomLocation]);
        }
    }

    void OnEnable()
    {
        // we need to disable all physics while walking
        var rBody = gameObject.GetComponent<Rigidbody2D>();
        if (rBody != null)
        {
            Destroy(rBody);
        }
        foreach (var collider in gameObject.GetComponents<Collider2D>())
        {
            collider.enabled = false;
        }

        var spriteAnim = GetComponentInChildren<Animator>();
        if (isLeaving)
        {
            spriteAnim.SetInteger("State", 5);
        }
        else
        {
            spriteAnim.SetInteger("State", 3);
        }
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
                    exitComplete();
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
                    enterComplete();
                    return;
                }
                nextWaypoint = WayPointsLeave[wayPointIdx].transform.position;
            }
        }
        nextWaypoint += Vector3.up * walkingHeight;
        var direction = (nextWaypoint - transform.position).normalized;
        var translation = direction * walkingSpeed * Time.fixedDeltaTime;
        this.transform.position += translation;
    }

    public void exitComplete()
    {
        Destroy(gameObject);
    }

    public void enterComplete()
    {
        this.enabled = false;
        this.gameObject.AddComponent<Rigidbody2D>();
        
        // need to setup rigisbody here
        this.gameObject.GetComponent<Dancer>().setupRigidBody();
        var spriteAnim = GetComponentInChildren<Animator>();
        spriteAnim.SetInteger("State", 0);

        FindObjectOfType<PartyGuestController>().InitializeWithRandomDanceMove(gameObject.GetComponent<Dancer>());
    }
}
