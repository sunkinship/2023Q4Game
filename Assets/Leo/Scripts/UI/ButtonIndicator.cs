using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonIndicator : MonoBehaviour
{
    private RectTransform thisRect;
    [SerializeField]
    private RectTransform indicator1, indicator2;
    [SerializeField]
    private float indicatorOffset;
    private Vector2 indicatorOffsetVector;

    private RectTransform currentButton;
    private float currentButtonHalfLength;

    private void Start()
    {
        thisRect = GetComponent<RectTransform>();
    }

    void Update()
    {
        SetCurrentButtonInfo();
        SetIndicatorOffset();
        SetIndicatorPosition();
    }

    private void SetCurrentButtonInfo()
    {
        currentButton = EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>();
        currentButtonHalfLength = currentButton.rect.width / 2;
        thisRect.transform.position = currentButton.transform.position;
    }

    private void SetIndicatorOffset()
    {
        indicatorOffsetVector = new Vector2(currentButtonHalfLength + indicatorOffset, 0);
    }

    private void SetIndicatorPosition()
    {
        Vector2 buttonPos = new Vector2(currentButton.anchoredPosition.x, 0);
        indicator1.anchoredPosition = indicatorOffsetVector;
        indicator2.anchoredPosition = Vector2.zero - indicatorOffsetVector;
    }
}