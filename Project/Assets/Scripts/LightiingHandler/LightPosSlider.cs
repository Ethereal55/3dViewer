using UnityEngine;
using UnityEngine.UI;

public class YAxisRotator : MonoBehaviour
{
    [Header("���������")]
    [Tooltip("������� ������ ��� ��������")]
    public Transform targetObject;

    [Tooltip("�������� �������� (������ ��� �������� ��������)")]
    public float rotationSpeed = 5f;

    [Header("������")]
    [Tooltip("������� ��� ���������� ���������")]
    public Slider rotationSlider;

    [Tooltip("�������� ������� ��������")]
    public bool smoothRotation = true;

    private Quaternion targetRotation;

    private void Start()
    {
        // ������������� ��������
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
            // ������� ������������ ��������
            targetObject.rotation = Quaternion.Lerp(
                targetObject.rotation,
                targetRotation,
                Time.deltaTime * rotationSpeed
            );
        }
    }

    private void OnSliderChanged(float value)
    {
        // ������� ����� ������ ��������
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

    // ���������� �������� �������� ��� ������� ����������
    public void UpdateSliderValue()
    {
        if (rotationSlider != null)
        {
            rotationSlider.value = targetObject.eulerAngles.y;
        }
    }
}