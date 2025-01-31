using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : NetworkBehaviour
{
    private Slider movementSlider;
    private Slider rotationSlider;

    [SerializeField] private float maxMoveSpeed = 5f;
    [SerializeField] private float maxRotationSpeed = 180f;

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
    }

    private void Update()
    {
        // Allow only the local player to control the GameObject
        if (!IsOwner) return;

        if (movementSlider == null || rotationSlider == null) return;

        // Handle Movement
        float movementValue = movementSlider.value;
        float moveSpeed = 0f;

        if (movementValue < 0.5f)
        {
            moveSpeed = maxMoveSpeed * (1 - (movementValue * 2));
        }
        else if (movementValue > 0.5f)
        {
            moveSpeed = -maxMoveSpeed * ((movementValue - 0.5f) * 2);
        }

        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        // Handle Rotation
        float rotationValue = rotationSlider.value;
        float rotationSpeed = 0f;

        if (rotationValue < 0.5f)
        {
            rotationSpeed = -maxRotationSpeed * (1 - (rotationValue * 2));
        }
        else if (rotationValue > 0.5f)
        {
            rotationSpeed = maxRotationSpeed * ((rotationValue - 0.5f) * 2);
        }

        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
