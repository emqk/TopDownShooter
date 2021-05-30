using UnityEngine;

/// <summary>
/// Base class for creating reference proxy.
/// To use it properly, juse inherit from this type with T type and add CreateAssetMenu to be able to create an asset from Unity
/// </summary>
/// <typeparam name="T">Reference type</typeparam>
public abstract class ReferenceProxy<T> : ScriptableObject
{
    [SerializeField] T reference;

    public T GetReference()
    {
        return reference;
    }
}
