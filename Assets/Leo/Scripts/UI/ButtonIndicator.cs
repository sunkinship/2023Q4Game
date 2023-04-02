using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonIndicator : MonoBehaviour
{
    private Transform currentButton;
    [SerializeField]
    private Vector3 indicatorOffset;

    void Update()
    {
        SetCurrentButton();
        SetIndicatorPosition();
    }

    private void SetCurrentButton() => currentButton = EventSystem.current.currentSelectedGameObject.GetComponent<Transform>();

    private void SetIndicatorPosition() => transform.SetPositionAndRotation(currentButton.position + indicatorOffset, transform.rotation);
}