using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Boolean doorIsOpen = false;

    void FixedUpdate()
    {
        Boolean doorHastoBeOpen = false;
        var walkers = FindObjectsOfType<Walker>();
        if (walkers != null)
        {
            foreach (var walker in walkers)
            {
                if (!walker.isActiveAndEnabled || !walker.needsToBeTestedAgainstDoor()) continue;

                var collider = GetComponent<BoxCollider2D>();
                var colliderLeftEdge = transform.position.x;
                var colliderRightEdge = transform.position.x + collider.size.x * transform.localScale.x;
                if (walker.transform.position.x > colliderLeftEdge && walker.transform.position.x < colliderRightEdge)
                {
                    doorHastoBeOpen = true;
                }
            }
        }
        if (doorHastoBeOpen && !doorIsOpen)
        {
            var doorSprite = gameObject.GetComponentInChildren<SpriteRenderer>();
            doorSprite.enabled = true;
            doorIsOpen = true;
        }
        else if (!doorHastoBeOpen && doorIsOpen)
        {
            var doorSprite = gameObject.GetComponentInChildren<SpriteRenderer>();
            doorSprite.enabled = false;
            doorIsOpen = false;
        }
    }
}
