using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
	public float lifetime = 2000;
	private float createTime;

	// Use this for initialization
	void Start()
	{
		createTime = Time.realtimeSinceStartup;
	}

	// Update is called once per frame
	void Update()
	{
		gameObject.GetComponent<RectTransform>().transform.Translate(new Vector3(0, 0.05f, 0));
		gameObject.GetComponent<CanvasRenderer>().SetAlpha(1 - (Time.realtimeSinceStartup - createTime) / lifetime);

		if (Time.realtimeSinceStartup > createTime + lifetime)
		{
			Destroy(gameObject);
		}
	}
}
