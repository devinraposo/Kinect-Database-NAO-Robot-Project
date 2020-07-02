using UnityEngine;
using System.Collections;

public class TriggerScript : MonoBehaviour {
    public bool trigger;
    CollisionScript parentScript;
    void Awake()
    {
        trigger = false;
        parentScript = transform.parent.gameObject.GetComponent<CollisionScript>();
    }
    void OnTriggerEnter(Collider col)
    {
        if(trigger)
        {
            parentScript.trigger = true;
            trigger = false;
        }
    }
}
