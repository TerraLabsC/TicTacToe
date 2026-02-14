using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class ColorTween : MonoBehaviour
{
    [Header("Настройки")]

    [SerializeField] private Graphic _graphic;
    [SerializeField] private Color _targetColor = Color.red;
    [SerializeField] private float _duration = 0.5f;

    private Color _originalColor;

    private void Awake()
    {
        if (_graphic == null)
            _graphic = GetComponent<Graphic>();

        _originalColor = _graphic.color;
    }

    public void ChangeColor()
    {
        _graphic.DOColor(_targetColor, _duration);
    }

    public void ResetColor()
    {
        _graphic.DOColor(_originalColor, _duration);
    }
}
