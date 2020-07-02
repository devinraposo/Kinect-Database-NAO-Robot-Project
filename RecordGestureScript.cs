using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class RecordGestureScript : MonoBehaviour {
    public BodySourceView bodyView;
    void Awake()
    {
        //only make a new vectorlist if the wireframe is still active when we start recording
        if (bodyView._body != null)
        {
            bodyView.vectorList = new List<Vector3>[bodyView._body.transform.childCount];
            for (int i = 0; i < bodyView._body.transform.childCount; ++i)
            {
                bodyView.vectorList[i] = new List<Vector3>();
            }
        }
    }
	// Update is called once per frame, 60 frames per second
	void Update ()
    {
        if (bodyView._body != null)
        {
            for (int i = 0; i < bodyView._body.gameObject.transform.childCount; ++i)
            {
                bodyView.vectorList[i].Add(bodyView._body.transform.GetChild(i).transform.position);
            }
        }
	}
}
