using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderCheck : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isPressed = false;
    private Slider slider;
    private const float DEFAULT_VALUE = 0.5f; // Define the default value as a constant

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        // Set initial value to 0.5
        slider.value = DEFAULT_VALUE;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPressed)
        {
            Debug.Log("Slider is being pressed.");
            // You can add code here to handle what happens when the slider is pressed
        }
        else
        {
            // When the slider is not pressed, smoothly lerp back to 0.5
            slider.value = Mathf.Lerp(slider.value, DEFAULT_VALUE, Time.deltaTime * 5f);
        }
    }

    // This method is called when the pointer is pressed down on the slider
    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }

    // This method is called when the pointer is released from the slider
    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
        slider.value = DEFAULT_VALUE; // Immediately set to 0.5
    }
}
