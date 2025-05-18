using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class ColorLightController : MonoBehaviour
{
    [Header("Настройки")]
    public Light targetLight;
    public Slider hueSlider;
    private float saturation = 1f;
    private float value = 1f;

    private void Start()
    {
        InitializeSlider();
        UpdateColor();
    }

    private void OnEnable()
    {
        saturation = 1f;
        UpdateColor();
    }

    private void OnDisable()
    {
        saturation = 0f;
        UpdateColor();
    }

    private void InitializeSlider()
    {
        if (hueSlider != null)
        {
            hueSlider.minValue = 0f;
            hueSlider.maxValue = 1f;
            hueSlider.onValueChanged.AddListener(UpdateColor);
        }
    }

    private void UpdateColor(float _ = 0)
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
}
