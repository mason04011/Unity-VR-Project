using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionReference movement,rotation;
    [SerializeField] private CharacterController controller;
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject leftHandSlot;
    [SerializeField] private GameObject rightHand;
    [SerializeField] private GameObject rightHandSlot;
    
    private bool groundedPlayer;

    private void OnEnable()
    {
        movement.action.performed += MovePlayer;
        rotation.action.performed += RotatePlayer;
    }

    private void OnDisable()
    {
        movement.action.performed -= MovePlayer;
        rotation.action.performed -= RotatePlayer;
    }

    void Update()
    {

    }

    void RotatePlayer(InputAction.CallbackContext context)
    {
        Vector2 rotationInput = context.ReadValue<Vector2>();
        float horizontalRotation = rotationInput.x;

        float snapAngle = 45f;
        float snappedRotation = Mathf.Round(horizontalRotation) * snapAngle;

        Vector3 currentRotation = transform.eulerAngles;
        currentRotation.y += snappedRotation;

        transform.rotation = Quaternion.Euler(currentRotation);
    }

    void MovePlayer(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();

        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        Vector3 movement = (forward * moveInput.y + right * moveInput.x) * playerSpeed * Time.deltaTime;

        controller.Move(movement);
    }



}
