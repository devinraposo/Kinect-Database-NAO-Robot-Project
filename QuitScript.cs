using UnityEngine;
using System.Collections;

public class QuitScript : MonoBehaviour {
	void Awake()
    {
        Application.targetFrameRate = 60;
    }
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
}
