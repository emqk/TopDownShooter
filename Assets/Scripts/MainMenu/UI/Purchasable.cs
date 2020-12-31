using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public abstract class Purchasable : MonoBehaviour
{
    [SerializeField] protected TMP_Text title;
    [SerializeField] protected TMP_Text description;
    [SerializeField] protected Image thumbnail;
    [SerializeField] protected TMP_Text selectText;
    [SerializeField] protected Button selectButton;


    public abstract void Buy();

    public abstract void Select();

    public abstract void Refresh();
}
