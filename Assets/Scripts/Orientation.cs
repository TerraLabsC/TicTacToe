using UnityEngine;

public class Orientation : MonoBehaviour
{
    private Camera _camera;

    [SerializeField] private Canvas CanvasHorizontal;

    [SerializeField] private Canvas CanvasVertical;
    
    private void Start()
    {
        _camera = Camera.main;

        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (_camera == null) return;

        if (Screen.width > Screen.height)
        {
            _camera.orthographicSize = 5.5f;
            CanvasHorizontal.enabled = true;
            CanvasVertical.enabled = false;
        }
        else
        {
            _camera.orthographicSize = 10f;
            CanvasHorizontal.enabled = false;
            CanvasVertical.enabled = true;
        }
    }
}