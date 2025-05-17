using System.Diagnostics;
using UnityEngine;

public class ObjectSwitcher : MonoBehaviour
{
    public GameObject lighting_ui;

    public void SwitchMode()
    {
        if (lighting_ui.activeInHierarchy)
        {
            lighting_ui.SetActive(false);
        }
        else{
            lighting_ui.SetActive(true);
        }
    }
}