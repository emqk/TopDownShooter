using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterData", menuName = "Characters/Create New Character")]
public class CharacterData : PurchaseData
{
    [Header("--------------------------------- [ Character INFO ] ---------------------------------")]
    [Header("Stats", order = 1)]
    [SerializeField] int maxHealth;
    [SerializeField] float movementSpeed;

    public int MaxHealth { get => maxHealth; }
    public float MovementSpeed { get => movementSpeed; }
}
