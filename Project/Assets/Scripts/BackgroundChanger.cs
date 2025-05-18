using UnityEngine;
using UnityEngine.UI;

public class BackgroundChanger : MonoBehaviour
{
    public Image backgroundImage; // Ссылка на компонент Image фона
    public Sprite[] backgrounds;  // Массив изображений для фона
    private int currentIndex = 0; // Текущий индекс фона

    void Awake()
    {
        Debug.Log("Скрипт активен на " + gameObject.name);
    }
    // Метод для изменения фона
    public void ChangeBackground()
    {
        if (backgrounds.Length == 0) return;

        currentIndex = (currentIndex + 1) % backgrounds.Length;
        backgroundImage.sprite = backgrounds[currentIndex];
        Debug.Log("Clicked!");
    }
}