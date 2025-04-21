using UnityEngine;

/// <summary>
/// Класс, отвечающий за вращение прикреплённого 3D-объекта.
/// </summary>
/// <remarks>
/// Скрипт должен быть присоединён к GameObject в сцене Unity.
/// Вращение происходит каждый кадр вокруг осей X и Y.
/// </remarks>
public class Move : MonoBehaviour
{
    /// <summary>
    /// Метод Update, вызываемый Unity каждый кадр.
    /// </summary>
    /// <remarks>
    /// Выполняет вращение объекта с постоянной скоростью:
    /// 1 градус по оси X и 1 градус по оси Y за кадр.
    /// </remarks>
    void Update()
    {
        // Вращаем объект каждый кадр
        this.transform.Rotate(1f, 1f, 0f);
    }
}
