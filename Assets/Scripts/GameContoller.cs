using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

public class GameContoller : MonoBehaviour 
{
	private enum GameStatus
	{
		StartMenu,
		Playing1,
		Finish1,
		Playing2,
		Finish2
	}
	
	public Vector3 vec;
	public GameObject CameraPoint1;
	public string VelocityDataPath;
	public GameObject StartMenu_UI;
	public GameObject PinCanvas_UI;
	
	private BallSpawn ballspawn;
	private string TimeStamp;
	private int camera_mode;
	private GameStatus state;
	private GameObject ball;
	
	// PinState true=stand, false=knock down
	private bool[] PinStates;
	public GameObject[] PinObjects;
	public GameObject[] PinCanvas;
	
	// Use this for initialization
	void Start ()
	{
		ballspawn = gameObject.AddComponent<BallSpawn>();
		TimeStamp = File.GetLastWriteTime(VelocityDataPath).ToString();
		camera_mode = 0;
		PinStates = new [] 
		{
			true, true, true, true, true, true, true, true, true, true
		};
	}
	
	// Update is called once per frame
	void Update()
	{
		if (state == GameStatus.StartMenu)
		{
			// UI setting
			if (PinCanvas_UI.activeSelf)
			{
				PinCanvas_UI.SetActive(false);
			}

			if (!StartMenu_UI.activeSelf)
			{
				StartMenu_UI.SetActive(true);
			}
			
			// Key
            if (Input.GetKeyDown(KeyCode.Space))
            {
	            state = GameStatus.Playing1;
            }
		}
		else if (state == GameStatus.Playing1 || state == GameStatus.Playing2)
		{
			// UI setting
			if (StartMenu_UI.activeSelf)
			{
				StartMenu_UI.SetActive(false);
			}

			if (!PinCanvas_UI.activeSelf)
			{
				PinCanvas_UI.SetActive(true);
			}
			
			// Key debug
            if (Input.GetKeyDown(KeyCode.S))
            {
                ball = GameObject.FindGameObjectWithTag("Ball");
                if (ball == null)
                {
                    ballspawn.SpawnBall(vec);
                }
            }

			// Read velocity file
            if (TimeStamp != File.GetLastWriteTime(VelocityDataPath).ToString())
            {
                TimeStamp = File.GetLastWriteTime(VelocityDataPath).ToString();
                StreamReader sr = new StreamReader(VelocityDataPath, Encoding.Default);
                Debug.Log(sr.ReadToEnd());
                sr.Close();
	            
                ball = GameObject.FindGameObjectWithTag("Ball");
                if (ball == null)
                {
                    ballspawn.SpawnBall(vec);
                }
            }

            for (int i = 0; i < 10; i++)
            {
                float x = PinObjects[i].transform.rotation.x;
                float z = PinObjects[i].transform.rotation.z;
                if (x > 0.45 || -0.45 > x || z > 0.45 || -0.45 > z)
                {
                    PinStates[i] = false;
                    PinCanvas[i].GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f);
                }
            }

			// Set camera position
            Vector3 pos;
            switch (camera_mode)
            {
                case 0:
                    Debug.Log("camera mode 0");
                    pos = CameraPoint1.transform.position;
                    transform.position = pos;
                    break;
                case 1:
                    Debug.Log("camera mode 1");
                    ball = GameObject.FindGameObjectWithTag("Ball");
                    pos = new Vector3(ball.transform.position.x + 1, 1.0f, 0.0f);
                    transform.position = pos;
                    break;
                case 2:
                    Debug.Log("camera mode 2");
                    break;
                case 3:
                    Debug.Log("camera mode 3");
                    break;
            }
        }
    }

	// Set camera mode
	public void SetCamMode(int mode)
	{
		camera_mode = mode;
	}
}
