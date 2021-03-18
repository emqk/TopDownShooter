using System.Collections.Generic;
using UnityEngine;

public class HorizontalView3D : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] Transform contentParent;
    [SerializeField] float spacing;

    [Header("Rotation")]
    [SerializeField] bool rotate = true;
    [SerializeField] float rotateSpeed = 10;
    [SerializeField] float startRotation = 0;
    float currentRotation = 0;

    List<GameObject> currentContentObjects = new List<GameObject>();


    private void Update()
    {
        if (rotate)
        {
            RotateContent();
        }
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
            GameObject instance = Instantiate(contentObjects[i], contentParent);
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
        currentRotation = startRotation;
    }
}
