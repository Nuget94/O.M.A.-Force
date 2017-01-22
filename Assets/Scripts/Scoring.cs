using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    public long scoreForLeavingGuest = 100;

	public GameObject PrefabText = null;

    public void guestStuck()
    {
        // TODO more points for guests you catapulted so hard they got stuck
    }

    public void guestKnockedOut()
    {
		FindObjectOfType<PlayerScorePersistenceManager>().Score.AddScore(scoreForLeavingGuest);

		gameObject.GetComponent<Text>().text = "Score  " + FindObjectOfType<PlayerScorePersistenceManager>().Score.Score;
        gameObject.GetComponent<AudioSource>().PlayDelayed(0.005f);


        GameObject player = GameObject.Find("Player");
		GameObject uiCanvas = GameObject.Find("UI Canvas");
		RectTransform uiTransform = uiCanvas.GetComponent<RectTransform>();
		Vector3 playerViewport = Camera.current.WorldToViewportPoint(player.transform.position - Vector3.down * -1.9f);
		Vector2 playerScreen = new Vector2(
			((playerViewport.x * uiTransform.sizeDelta.x) - (uiTransform.sizeDelta.x * 0.5f)),
			((playerViewport.y * uiTransform.sizeDelta.y) - (uiTransform.sizeDelta.y * 0.5f)));

		GameObject newScore = Instantiate(PrefabText);
		newScore.transform.SetParent(uiTransform);
		newScore.GetComponent<RectTransform>().anchoredPosition = playerScreen;
	}
}
