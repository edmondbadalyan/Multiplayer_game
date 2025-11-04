using System;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private RectTransform _filledImage;
    private float _defaultWidth;
    private void Start()
    {
        _defaultWidth = _filledImage.sizeDelta.x;
    }

    public void UpdateHealth(float max, int current)
    {
        float percent = current / max;
        _filledImage.sizeDelta = new Vector2(_defaultWidth * percent, _filledImage.sizeDelta.y);
    }
}
