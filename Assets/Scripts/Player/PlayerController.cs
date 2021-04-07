using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform body;
    [SerializeField] float constantHeight = 1;
    float movementSpeed;

    [Header("Touch input")]
    Joystick movementJoystick;
    Joystick rotationJoystick;
    
    CharacterController characterController;
    Player controlledPlayer;
    Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        controlledPlayer = GetComponent<Player>();

        movementJoystick = UIManager.instance.MovementJoystick;
        rotationJoystick = UIManager.instance.RotationJoystick;
    }

    void Update()
    {
        ControlTouchInput();
        //ControlKeyboardMouseInput();

        SetPositionToDefaultHeight();
    }

    void SetPositionToDefaultHeight()
    {
        transform.position = new Vector3(transform.position.x, constantHeight, transform.position.z);
    }

    public void SetMovementSpeed(float newSpeed)
    {
        movementSpeed = newSpeed;
    }

    void ControlKeyboardMouseInput()
    {
        //Movement
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        MoveBy(new Vector3(hor, 0, ver).normalized);

        //Look
        RotateToScreenPoint(Input.mousePosition);

        //Shooting
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if(controlledPlayer.WeaponShoot())
                controlledPlayer.OnWeaponShoot();
        }
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
            RotateToNormalizedVector(lookVec);

            //Shooting
            if(controlledPlayer.WeaponShoot())
                controlledPlayer.OnWeaponShoot();
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

    void RotateToNormalizedVector(Vector2 vec)
    {
        Quaternion lookRot = Quaternion.LookRotation(new Vector3(vec.x, 0, vec.y));
        body.rotation = lookRot;
    }
}
