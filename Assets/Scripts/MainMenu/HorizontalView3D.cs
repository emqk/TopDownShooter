using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalView3D : MonoBehaviour
{
    [SerializeField] Transform contentParent;
    [SerializeField] float spacing;

    List<GameObject> currentContentObjects = new List<GameObject>();

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
    }
}
