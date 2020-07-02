//Authored by Devin Raposo
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Collections;
public class InstrumentScript : MonoBehaviour
{
    public Dropdown drop;
    public GameObject prefab;
    public List<ulong> timeList;
    public Stopwatch watch;
    public LoadPresetScript loadScript;
    public List<int> boxList;
    public List<GameObject> objList;
    void OnEnable()
    {
        timeList = new List<ulong>();
        watch = new Stopwatch();
        boxList = new List<int>();
        objList = new List<GameObject>();
    }
    public void MakeInstrument()
    {
        for (int i = 0; i < loadScript.soundList[drop.value].Count; ++i)
        {
            GameObject newObject = (GameObject)Instantiate(prefab, new Vector3(prefab.transform.position.x + (i * 3),
                prefab.transform.position.y, prefab.transform.position.z), Quaternion.identity, prefab.transform);
            //set it to an instance of a prefab, not the original
            //get rid of the children it carries over, disregarding the reset trigger so we don't delete it
            for (int j = 1; j < newObject.transform.childCount; ++j)
            {
                Destroy(newObject.transform.GetChild(j).gameObject);
            }
            objList.Add(newObject);
            //turn on its trigger and mesh
            newObject.transform.GetChild(0).gameObject.SetActive(true);
            MeshRenderer mesh = newObject.GetComponent<MeshRenderer>();
            mesh.enabled = true;
            newObject.name = loadScript.soundList[drop.value][i];
            StringBuilder temp = new StringBuilder("file://" + Application.dataPath.Substring(0, Application.dataPath.LastIndexOf("/"))
                + "/Kinect Module_Data/Presets/" + newObject.name);
            StartCoroutine(LoadAudio(temp.ToString(), newObject));
        }
    }
    IEnumerator<WWW> LoadAudio(string url, GameObject newObject)
    {
        WWW clip = new WWW(url);
        yield return clip;
        AudioSource audio = newObject.GetComponent<AudioSource>();
        audio.clip = clip.audioClip;
    }
    public void DeleteInstrument()
    {
        //start at 1 so we don't delete the reset trigger
        for (int i = 1; i <= loadScript.soundList[drop.value].Count; ++i)
        {
            Destroy(prefab.transform.GetChild(i).gameObject);
        }
    }
    public void StopStopwatch()
    {
        if (watch.IsRunning)
        {
            watch.Stop();
            timeList.Add((ulong)watch.ElapsedMilliseconds);
        }
    }
}
