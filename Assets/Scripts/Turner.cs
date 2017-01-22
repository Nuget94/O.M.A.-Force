using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class Turner : MonoBehaviour
{
    public float rotationStepEulerDegrees = 10f;

    private float lastX = 10000.0f;

	// Update is called once per frame
	void FixedUpdate () {

        checkFacingDirection();
    }

    private Coroutine turnAround = null;
    private float rotationTarget = 0.0f;

    // rotation 0 means facing left, rotation 1 means facing right
    IEnumerator TurnToRotationY(float rotation)
    {
        var sprite = GetComponentInChildren<SpriteRenderer>();
        rotationTarget = rotation;
        var ownRotation = sprite.transform.rotation.eulerAngles.y;
        if (ownRotation < 0.0f
            || FloatComparer.AreEqual(ownRotation, rotation * 180f, rotationStepEulerDegrees * 1.001f))
        {
            sprite.transform.localRotation = Quaternion.Euler(new Vector3(0f, rotation * 180f, 0f));
            turnAround = null;
        }
        else
        {
            if (rotation < 0.5f)
            {
                // rotate towards 0
                sprite.transform.Rotate(Vector3.up, rotationStepEulerDegrees, Space.Self);
            }
            else
            {
                // rotate towards 1
                sprite.transform.Rotate(Vector3.up, rotationStepEulerDegrees, Space.Self);
            }
            yield return new WaitForFixedUpdate();
            turnAround = StartCoroutine(TurnToRotationY(rotation));
        }
    }

    public void stopTurn(Boolean setToRotationTarget)
    {
        if (turnAround != null)
        {
            StopCoroutine(turnAround);
        }
        if (setToRotationTarget)
        {
            var sprite = GetComponentInChildren<SpriteRenderer>();
            sprite.transform.localRotation = Quaternion.AngleAxis(rotationTarget * 180f, Vector3.up);
        }
    }

    public void checkFacingDirection()
    {
        var walker = gameObject.GetComponent<Walker>();
        var dancer = gameObject.GetComponent<Dancer>();
        if (dancer != null && dancer.isDead && walker != null && !walker.isActiveAndEnabled)
        {
            return;
        }

        var diff = transform.position.x - lastX;
        var threshold = 0.003f;
        if (diff > threshold && rotationTarget < 0.5f)
        {
            stopTurn(false);
            turnAround = StartCoroutine(TurnToRotationY(1.0f));
        }
        else if (diff < -threshold && rotationTarget > 0.5f)
        {
            stopTurn(false);
            turnAround = StartCoroutine(TurnToRotationY(0.0f));
        }
        lastX = transform.position.x;
    }


}
