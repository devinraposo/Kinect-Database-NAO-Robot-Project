using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class SamePathScript : MonoBehaviour {
    public InputField field;
	public void LoadPath()
    {
        StringBuilder temp = new StringBuilder(Application.dataPath + "/Presets/");
        field.text = temp.ToString();
    }
}
