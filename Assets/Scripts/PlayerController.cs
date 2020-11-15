using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] Weapon equipedWeapon;
    [SerializeField] Transform body;
    [SerializeField] float movementSpeed;

    [Header("Touch input")]
    [SerializeField] Joystick movementJoystick;
    [SerializeField] Joystick rotationJoystick;
    
    CharacterController characterController;
    Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);


    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        ControlTouchInput();
        //ControlKeyboardMouseInput();
    }

    void ControlKeyboardMouseInput()
    {
        //Movement
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        MoveBy(new Vector3(hor, 0, ver).normalized);

        //Look
        RotateToScreenPoint(Input.mousePosition);
    }

    void ControlTouchInput()
    {
        //Movement
        if (movementJoystick.IsFingerOnMe())
        {
            Vector2 moveVec = movementJoystick.GetResult();
            MoveBy(new Vector3(moveVec.x, 0, moveVec.y));
        }

        //Look
        if (rotationJoystick.IsFingerOnMe())
        {
            Vector2 lookVec = rotationJoystick.GetResult();
            Quaternion lookRot = Quaternion.LookRotation(new Vector3(lookVec.x, 0, lookVec.y));
            body.rotation = lookRot;

            //Shooting
            if (equipedWeapon)
            {
                equipedWeapon.UpdateMe();
            }
        }
    }

    void MoveBy(Vector3 moveVecNorm)
    {
        characterController.Move(moveVecNorm * movementSpeed * Time.deltaTime);
    }

    void RotateToScreenPoint(Vector2 screenPoint)
    {
        Vector2 dirToCursor = (screenPoint - screenCenter).normalized;
        Quaternion lookRot = Quaternion.LookRotation(new Vector3(dirToCursor.x, 0, dirToCursor.y));
        body.rotation = lookRot;
    }
}
