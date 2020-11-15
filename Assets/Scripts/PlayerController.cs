using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    
    CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        float hor = Input.GetAxisRaw("Horizontal");   
        float ver = Input.GetAxisRaw("Vertical");
        MoveBy(new Vector3(hor, 0, ver).normalized);
    }

    void MoveBy(Vector3 moveVecNorm)
    {
        characterController.Move(moveVecNorm * movementSpeed * Time.deltaTime);
    }
}
