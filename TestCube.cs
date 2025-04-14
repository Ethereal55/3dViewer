using UnityEngine;

public class Move : MonoBehaviour
{
    void Update()
    {
        // Вращаем объект каждый кадр
        this.transform.Rotate(1f, 1f, 0f);
    }
}
