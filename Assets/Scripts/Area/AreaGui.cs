using UnityEngine;

public class AreaGui : MonoBehaviour
{
    [Header("Настройки")]
    [Tooltip("Перетащите сюда объект guy")]
    public Transform guy;
   
    [Tooltip("Насколько опускать guy в портретном режиме")]
    public float portraitOffset = 1.0f;

    private Vector3 originalPosition;
    private string currentStatus = "Ожидаю...";

    void Start()
    {
        if (guy != null)
        {
            // Сохраняем начальную позицию (обычно это позиция для ландшафта)
            originalPosition = guy.position;
        }
    }

    void Update()
    {
        if (guy == null) return;

        // Проверяем ориентацию устройства
        switch (Input.deviceOrientation)
        {
            case DeviceOrientation.Portrait:
            case DeviceOrientation.PortraitUpsideDown:
                // Опускаем guy вниз
                guy.position = originalPosition - Vector3.up * portraitOffset;
                currentStatus = "Портретный режим (вниз)";
                break;

            case DeviceOrientation.LandscapeLeft:
            case DeviceOrientation.LandscapeRight:
                // Возвращаем guy обратно
                guy.position = originalPosition;
                currentStatus = "Ландшафтный режим (назад)";
                break;

            default:
                // На случай FaceUp, FaceUp или Unknown (например, в редакторе)
                // Можно оставить как есть или вернуть в оригинал
                break;
        }
    }

    // Отображение логической переменной на экране для тестов
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 300, 100));
        GUILayout.Label($"Статус: {currentStatus}");
        GUILayout.Label($"Позиция Y: {guy.position.y:F2}");
        GUILayout.Label($"Базовая Y: {originalPosition.y:F2}");
        GUILayout.EndArea();
    }
}
