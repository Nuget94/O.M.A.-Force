using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrannyTurner : Turner {
	override public void TurnDone(bool lookRight)
	{
		foreach (GameObject taggedObject in GameObject.FindGameObjectsWithTag("Attack"))
		{
			if (lookRight)
			{
				taggedObject.transform.localPosition = new Vector3(2.26f, 0, 0);
			}
			else
			{
				taggedObject.transform.localPosition = new Vector3(0, 0, 0);
			}
		}
	}
}
