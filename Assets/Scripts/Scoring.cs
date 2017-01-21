using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoring : MonoBehaviour
{
    public long score = 0;

    public long scoreForLeavingGuest = 100;

    public void guestLeft()
    {
        score += scoreForLeavingGuest;
    }
}
