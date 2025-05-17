using UnityEngine;
using UnityEngine.UI;

public class YAxisRotator : MonoBehaviour
{
    [Header("Настройки")]
    [Tooltip("Целевой объект для вращения")]
    public Transform targetObject;

    [Tooltip("Скорость вращения (только для плавного вращения)")]
    public float rotationSpeed = 5f;

    [Header("Ссылки")]
    [Tooltip("Слайдер для управления вращением")]
    public Slider rotationSlider;

    [Tooltip("Включить плавное вращение")]
    public bool smoothRotation = true;

    private Quaternion targetRotation;

    private void Start()
    {
        // Инициализация слайдера
        if (rotationSlider != null)
        {
            rotationSlider.minValue = 0;
            rotationSlider.maxValue = 360;
            rotationSlider.value = targetObject.eulerAngles.y;
            rotationSlider.onValueChanged.AddListener(OnSliderChanged);
        }

        targetRotation = targetObject.rotation;
    }

    private void Update()
    {
        if (smoothRotation)
        {
            // Плавная интерполяция вращения
            targetObject.rotation = Quaternion.Lerp(
                targetObject.rotation,
                targetRotation,
                Time.deltaTime * rotationSpeed
            );
        }
    }

    private void OnSliderChanged(float value)
    {
        // Создаем новый вектор поворота
        Vector3 newRotation = new Vector3(
            targetObject.eulerAngles.x,
            value,
            targetObject.eulerAngles.z
        );

        if (smoothRotation)
        {
            targetRotation = Quaternion.Euler(newRotation);
        }
        else
        {
            targetObject.eulerAngles = newRotation;
        }
    }

    // Обновление значения слайдера при внешних изменениях
    public void UpdateSliderValue()
    {
        if (rotationSlider != null)
        {
            rotationSlider.value = targetObject.eulerAngles.y;
        }
    }
}