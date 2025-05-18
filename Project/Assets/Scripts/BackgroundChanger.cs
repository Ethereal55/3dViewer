using UnityEngine;
using UnityEngine.UI;

public class BackgroundChanger : MonoBehaviour
{
    public Image backgroundImage; // ������ �� ��������� Image ����
    public Sprite[] backgrounds;  // ������ ����������� ��� ����
    private int currentIndex = 0; // ������� ������ ����

    void Awake()
    {
        Debug.Log("������ ������� �� " + gameObject.name);
    }
    // ����� ��� ��������� ����
    public void ChangeBackground()
    {
        if (backgrounds.Length == 0) return;

        currentIndex = (currentIndex + 1) % backgrounds.Length;
        backgroundImage.sprite = backgrounds[currentIndex];
        Debug.Log("Clicked!");
    }
}