using UnityEngine;
using System.Collections;

public class PlayerScorePersistenceManager : MonoBehaviour {
	private static PlayerScorePersistenceManager _instance = null;
	private PlayerScore _score = new PlayerScore();

	public static PlayerScorePersistenceManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<PlayerScorePersistenceManager>();
				DontDestroyOnLoad(_instance.gameObject);
			}
			return _instance;
		}
	}

	public PlayerScore Score {
		get
		{
			return _score;
		}
	}

	void Awake()
	{
		if (_instance == null)
		{
			_instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			if (this != _instance)
			{
				Destroy(this.gameObject);
			}
		}
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
