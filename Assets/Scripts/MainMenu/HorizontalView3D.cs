using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HorizontalView3D : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] Transform content;
    [SerializeField] float spacing;

    [Header("Movement")]
    [SerializeField] bool canMove = true;
    [SerializeField] float rawMoveSpeed = 1;
    [SerializeField] float smoothMoveSpeed = 5;
    [SerializeField] float focusClosestOnDistance = 0.5f;
    [SerializeField] Vector3 startMoveOffset = Vector3.zero;
    float currentMoveOffsetX = 0;

    [Header("Rotation")]
    [SerializeField] bool canRotate = true;
    [SerializeField] float rotateSpeed = 10;
    [SerializeField] float startRotation = 0;
    float currentRotation = 0;

    [Header("Other")]
    [SerializeField] UnityEvent onSelectedChange;

    float minContentSize = 0;
    float maxContentSize = 0;

    List<GameObject> currentContentObjects = new List<GameObject>();
    int selectedIndex = 0;

    public void MoveByElements(int amount)
    {
        selectedIndex = Mathf.Clamp(selectedIndex + amount, 0, currentContentObjects.Count - 1);
        currentMoveOffsetX = -RoundToClosestElement(selectedIndex * spacing);
        OnSelectedChange();
    }

    private void Update()
    {
        if (canMove)
        {
            MoveContent();
        }
        if (canRotate)
        {
            RotateContent();
        }
    }

    void MoveContent()
    {
        Vector2 touchDeltaNormalized = GetTouchDeltaScreenNormalized();
        currentMoveOffsetX += touchDeltaNormalized.x * rawMoveSpeed;
        currentMoveOffsetX = GetClampToContent(currentMoveOffsetX);

        float currentTargetDifferenceX = Mathf.Abs(content.transform.localPosition.x - currentMoveOffsetX);
        if (Input.touchCount == 0 && currentTargetDifferenceX <= focusClosestOnDistance)
        {
            currentMoveOffsetX = RoundToClosestElement(currentMoveOffsetX);
        }

        content.transform.localPosition = Vector3.Lerp(content.transform.localPosition, new Vector3(currentMoveOffsetX, 0, 0), smoothMoveSpeed * Time.deltaTime);

        //Set currently selected object and invoke event when newSelectedObject != oldSelectedObject
        int newSelectedObjectIndex = GetClosestElementIndex();
        if (selectedIndex != newSelectedObjectIndex)
        {
            selectedIndex = newSelectedObjectIndex;
            OnSelectedChange();
        }
    }


    void OnSelectedChange()
    {
        onSelectedChange.Invoke();
    }

    float GetClampToContent(float locationX)
    {
        return Mathf.Clamp(locationX, -maxContentSize, minContentSize);
    }

    float RoundToClosestElement(float locationX)
    {
        return Mathf.RoundToInt(locationX / spacing) * spacing;
    }

    void RotateContent()
    {
        currentRotation += Time.deltaTime * rotateSpeed;
        currentRotation %= 360;

        foreach (GameObject current in currentContentObjects)
        {
            current.transform.localRotation = Quaternion.Euler(0, currentRotation, 0);
        }
    }

    public void SetContentObjects(GameObject[] contentObjects)
    {
        ClearContent();

        for (int i = 0; i < contentObjects.Length; i++)
        {
            GameObject instance = Instantiate(contentObjects[i], content);
            instance.transform.localPosition = new Vector3(spacing * i, 0, 0);
            currentContentObjects.Add(instance);
        }

        minContentSize = 0;
        maxContentSize = spacing * (currentContentObjects.Count - 1);

        //Invoke after new objects spawned, because selected object data will probably change after setting new content
        onSelectedChange.Invoke();
    }

    void ClearContent()
    {
        foreach (GameObject obj in currentContentObjects)
        {
            Destroy(obj);
        }

        currentContentObjects.Clear();

        //Set default rotation
        currentRotation = startRotation;

        //Set default move and transform location
        currentMoveOffsetX = startMoveOffset.x;
        content.transform.localPosition = startMoveOffset;
    }

    Vector2 GetTouchDeltaScreenNormalized()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 dragDistanceUnscaled = touch.deltaPosition;
            float dragDistanceScaledX = dragDistanceUnscaled.x / Screen.width;
            float dragDistanceScaledY = dragDistanceUnscaled.y / Screen.height;
            return new Vector2(dragDistanceScaledX, dragDistanceScaledY);
        }

        return Vector2.zero;
    }

    public int GetSelectedIndex()
    {
        return selectedIndex;
    }

    int GetClosestElementIndex()
    {
        if (currentContentObjects.Count < 1)
            return -1;
        
        float closestX = Mathf.Abs(RoundToClosestElement(currentMoveOffsetX));
        int index = Mathf.RoundToInt(closestX / spacing);
        return index;
    }
}
