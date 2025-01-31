using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class MoveTank : NetworkBehaviour
{
    private Slider movementSlider;
    private Slider rotationSlider;

    [SerializeField] private float maxMoveSpeed = 5f;
    [SerializeField] private float maxRotationSpeed = 180f;

    [SerializeField] private GameObject[] leftWheels;
    [SerializeField] private GameObject[] rightWheels;
    [SerializeField] private float wheelRotationSpeed = 360.0f;

    private Rigidbody rb;

    private void Start()
    {
        // Find sliders by their GameObject names
        movementSlider = GameObject.Find("MovSlider").GetComponent<Slider>();
        rotationSlider = GameObject.Find("RotSlider").GetComponent<Slider>();

        // Ensure sliders are found
        if (movementSlider == null)
            Debug.LogError("MovementSlider not found in the scene!");
        if (rotationSlider == null)
            Debug.LogError("RotationSlider not found in the scene!");

        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody not found on the Player GameObject!");
        }
    }

    private void Update()
    {
        // Allow only the local player to control the GameObject
        if (!IsOwner) return;

        if (movementSlider == null || rotationSlider == null) return;

        // Handle Tank Movement and Rotation
        float moveInput = GetMovementInput();
        float rotateInput = GetRotationInput();

        // Rotate the wheels for visual feedback
        RotateWheels(moveInput, rotateInput);
    }

    private void FixedUpdate()
    {
        // Movement and rotation logic in FixedUpdate for physics consistency
        float moveInput = GetMovementInput();
        float rotateInput = GetRotationInput();

        MoveTankObj(moveInput);
        RotateTank(rotateInput);
    }

    private float GetMovementInput()
    {
        float movementValue = movementSlider.value;

        if (movementValue < 0.5f)
        {
            return 1 - (movementValue * 2); // Forward
        }
        else if (movementValue > 0.5f)
        {
            return -(movementValue - 0.5f) * 2; // Backward
        }

        return 0; // Neutral
    }

    private float GetRotationInput()
    {
        float rotationValue = rotationSlider.value;

        if (rotationValue < 0.5f)
        {
            return -(1 - (rotationValue * 2)); // Left
        }
        else if (rotationValue > 0.5f)
        {
            return (rotationValue - 0.5f) * 2; // Right
        }

        return 0; // Neutral
    }

    private void MoveTankObj(float input)
    {
        Vector3 moveDirection = transform.forward * input * maxMoveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + moveDirection);
    }

    private void RotateTank(float input)
    {
        float rotateAmount = input * maxRotationSpeed * Time.fixedDeltaTime;
        Quaternion rotate = Quaternion.Euler(0, rotateAmount, 0);
        rb.MoveRotation(rb.rotation * rotate);
    }

    private void RotateWheels(float moveInput, float rotationInput)
    {
        float wheelRotation = moveInput * wheelRotationSpeed * Time.deltaTime;

        // Rotate left wheels
        foreach (GameObject wheel in leftWheels)
        {
            if (wheel != null)
            {
                wheel.transform.Rotate(wheelRotation - rotationInput * wheelRotationSpeed * Time.deltaTime, 0.0f, 0.0f);
            }
        }

        // Rotate right wheels
        foreach (GameObject wheel in rightWheels)
        {
            if (wheel != null)
            {
                wheel.transform.Rotate(wheelRotation + rotationInput * wheelRotationSpeed * Time.deltaTime, 0.0f, 0.0f);
            }
        }
    }
}