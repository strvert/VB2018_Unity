using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawn : MonoBehaviour {
	private GameObject spawn_point;
	private GameObject prefab;
	public void SpawnBall(Vector3 vector)
	{
		spawn_point = GameObject.FindWithTag("SpawnPoint");
		prefab = (GameObject) Resources.Load("Prefabs/Ball");
        GameObject ball = Instantiate(prefab, spawn_point.transform.position, Quaternion.identity);
		ball.GetComponent<Rigidbody>().AddForce(vector);
	}
}
