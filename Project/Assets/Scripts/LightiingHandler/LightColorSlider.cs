using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class ColorLightController : MonoBehaviour
{
    [Header("Настройки")]
    public Light targetLight;
    public Slider hueSlider;
    [Range(0, 1)] public float saturation = 1f;
    [Range(0, 1)] public float value = 1f;

    private void Start()
    {
        InitializeSlider();
        UpdateColor();
    }

    void InitializeSlider()
    {
        if (hueSlider != null)
        {
            hueSlider.minValue = 0f;
            hueSlider.maxValue = 1f;
            hueSlider.onValueChanged.AddListener(UpdateColor);
        }
    }

    void UpdateColor(float _ = 0)
    {
        Color newColor = Color.HSVToRGB(
            hueSlider.value,
            saturation,
            value
        );

        if (targetLight != null)
        {
            targetLight.color = newColor;
        }
    }

    public void SetHue(float hue) => hueSlider.value = hue;
}
