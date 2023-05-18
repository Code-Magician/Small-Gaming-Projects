using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {
	public GameObject[] obstacles;
	public float mainSpeed = 10;
	public float maxSpeed = 10;
	public float acceleration = 10;
	private float distanceRan = 0f;

	private  bool isGameOver;
	[SerializeField] private JumpAndDuck Dinasour;

	

	void Start () {
		distanceRan = 0f;
	}
	
	void Update () {
		distanceRan += mainSpeed * Time.deltaTime / 4;
		if (mainSpeed < maxSpeed) {
			mainSpeed += acceleration;
		}
	}

	public float getDistance() {
		return distanceRan;
	}

	public void GameOver()
	{
		isGameOver = true;
	}

	public bool isOut()
	{
		return isGameOver;
	}

	public void onKeyPressed()
	{
		Dinasour.Moves();
	}
}
