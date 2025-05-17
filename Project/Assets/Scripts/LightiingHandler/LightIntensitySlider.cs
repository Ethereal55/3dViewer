using UnityEngine;
using UnityEngine.UI;

public class IntensityLightController : MonoBehaviour
{
    [Header("Настройки")]
    public Light targetLight;
    public Slider intensitySlider;
    [Min(0)] public float minIntensity = 0f;
    [Min(0.1f)] public float maxIntensity = 8f;

    private void Start()
    {
        InitializeSlider();
        UpdateIntensity();
    }

    void InitializeSlider()
    {
        if (intensitySlider != null)
        {
            intensitySlider.minValue = minIntensity;
            intensitySlider.maxValue = maxIntensity;
            intensitySlider.onValueChanged.AddListener(UpdateIntensity);

            // Инициализация текущим значением света
            if (targetLight != null)
                intensitySlider.value = targetLight.intensity;
        }
    }

    void UpdateIntensity(float _ = 0)
    {
        if (targetLight != null)
        {
            targetLight.intensity = intensitySlider.value;
        }
    }

    public void SetIntensity(float intensity) => intensitySlider.value = intensity;
}