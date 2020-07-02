//Authored by Devin Raposo
using UnityEngine;
using UnityEngine.UI;

public class DropdownScript : MonoBehaviour
{
    public Dropdown dropdown;
    public Text label;
    void Awake()
    {
        dropdown.onValueChanged.AddListener(ChangeDropdownText);
    }
    private void ChangeDropdownText(int arg0)
    {
        label.text = dropdown.options[dropdown.value].text;
    }
}
