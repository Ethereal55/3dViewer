using UnityEngine;
using UnityEngine.UI;

public class ObjectSwitcher : MonoBehaviour
{
    public GameObject lighting_ui;
    public MonoBehaviour lighting_script;
    public MonoBehaviour moving_script;
    public Toggle toggle;

    private void Start()
    {
        if (toggle != null)
        {
            toggle.onValueChanged.AddListener(OnToggleChanged);
            toggle.isOn = false;
            OnToggleChanged(false);
        }
    }

    public void SwitchMode()
    {
        if (lighting_ui.activeInHierarchy)
        {
            lighting_ui.SetActive(false);
            moving_script.enabled = true;
        }
        else{
            lighting_ui.SetActive(true);
            moving_script.enabled = false;
        }
    }

    private void OnToggleChanged(bool isOn)
    {
        lighting_script.enabled = isOn;
    }
}