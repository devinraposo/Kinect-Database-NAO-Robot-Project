//Authored by Devin Raposo
using UnityEngine;
using System.Collections;

public class CollisionScript : MonoBehaviour {
    AudioSource sfx;
    public bool trigger;
    TriggerScript childScript;
    InstrumentScript instrScript;
    void Awake()
    {
        trigger = true;
        sfx = GetComponent<AudioSource>();
        childScript = this.transform.Find("Trigger").GetComponent<TriggerScript>();
        instrScript = GameObject.Find("Instrument").GetComponent<InstrumentScript>();
    }
    void OnTriggerEnter(Collider col)
    {
        if(trigger)
        {
            childScript.trigger = true;
            trigger = false;
            sfx.Play();
            int index = 0;
            for(int i = 0; i < instrScript.objList.Count; ++i)
            {
                if(gameObject.name.Equals(instrScript.objList[i].name))
                {
                    index = i;
                    break;
                }
            }
            instrScript.boxList.Add(index);
            if(!instrScript.watch.IsRunning) instrScript.watch.Start();
            else
            {
                instrScript.watch.Stop();
                instrScript.timeList.Add((ulong)instrScript.watch.ElapsedMilliseconds);
            }
        }
    }
}
