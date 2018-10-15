using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrrigerVolume : MonoBehaviour
{
	public GameContoller controller;

	public int camera_mode = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Ball"))
		{
			Debug.Log(other.transform.tag);
			controller.SetCamMode(camera_mode);
		}
	}

}
