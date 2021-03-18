using System.Collections.Generic;
using UnityEngine;

public class HorizontalView3D : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] Transform content;
    [SerializeField] float spacing;

    [Header("Movement")]
    [SerializeField] bool canMove = true;
    [SerializeField] float moveSpeed = 1;
    [SerializeField] Vector3 startMoveOffset = Vector3.zero;
    float currentMoveOffsetX = 0;

    [Header("Rotation")]
    [SerializeField] bool canRotate = true;
    [SerializeField] float rotateSpeed = 10;
    [SerializeField] float startRotation = 0;
    float currentRotation = 0;

    List<GameObject> currentContentObjects = new List<GameObject>();


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
        currentMoveOffsetX += touchDeltaNormalized.x * moveSpeed;

        content.transform.localPosition = new Vector3(currentMoveOffsetX, 0, 0);
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
}
