using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonIndicator : MonoBehaviour
{
    [SerializeField]
    private GameObject indicator1, indicator2;
    [SerializeField]
    private float indicatorOffset;
    private Vector3 indicatorOffsetVector;

    private Transform currentButton;
    private float currentButtonHalfLength;

    void Update()
    {
        SetCurrentButtonInfo();
        SetIndicatorOffset();
        SetIndicatorPosition();
    }

    private void SetCurrentButtonInfo()
    {
        currentButton = EventSystem.current.currentSelectedGameObject.GetComponent<Transform>();
        currentButtonHalfLength = currentButton.GetComponent<RectTransform>().rect.width / 200;
    }

    private void SetIndicatorOffset()
    {
        indicatorOffsetVector = new Vector3(currentButtonHalfLength + indicatorOffset, 0, 0);
    }

    private void SetIndicatorPosition()
    {
        indicator1.transform.SetPositionAndRotation(currentButton.position + indicatorOffsetVector, Quaternion.identity);
        indicator2.transform.SetPositionAndRotation(currentButton.position - indicatorOffsetVector, Quaternion.identity);
    }
}