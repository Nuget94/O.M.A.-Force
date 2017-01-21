using System;
using System.Collections.Generic;
using UnityEngine;

public class DanceMoveList : DanceMove
{
    public List<DanceMoveAndTime> DanceMoves = new List<DanceMoveAndTime>();

    private int internalIdx = 0;

    override public void reset()
    {
        internalIdx = 0;
    }

    override public Boolean hasNext()
    {
        var newIdx = internalIdx + 1;
        if (newIdx >= DanceMoves.Count)
        {
            return false;
        }
        return true;
    }

    override public DanceMoveAndTime nextMove()
    {
        return DanceMoves[internalIdx];
    }

    override public void nextIdx()
    {
        internalIdx += 1;
    }
}