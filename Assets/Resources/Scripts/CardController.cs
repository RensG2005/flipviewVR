using UnityEngine;
using UnityEngine.InputSystem;

public class FlashcardController : MonoBehaviour
{
    [SerializeField] private InputActionProperty touchpadInput; // Reference to the touchpad action
    [SerializeField] private float deadzone = 0.96f; // Sensitivity for the touchpad
    bool isTriggerPressed = false;

    public FlipCard flipCardScript;

    void OnEnable()
    {
        touchpadInput.action.Enable();
    }

    void OnDisable()
    {
        touchpadInput.action.Disable();
    }

    void Update()
    {
        Vector2 touchpadValue = touchpadInput.action.ReadValue<Vector2>();


        if (!isTriggerPressed && touchpadValue.x > deadzone)
        {
           flipCardScript.UpdateWordState (true);
           isTriggerPressed = true;
        }
        else if (!isTriggerPressed && touchpadValue.x < -deadzone)
        {
            flipCardScript.UpdateWordState (false);
            isTriggerPressed = true;
        }
        else if (touchpadValue.x < deadzone && touchpadValue.x > -deadzone)
        {
            isTriggerPressed = false;
        }

        // Detect if the "P" key is pressed
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            //NextWord(); // Call the NextWord() function from FlipCard
            flipCardScript.UpdateWordState (true);
        }

        // Detect if the "O" key is pressed
        if (Keyboard.current.oKey.wasPressedThisFrame)
        {
            //NextWord(); // Call the NextWord() function from FlipCard
            flipCardScript.UpdateWordState (false);
        }
    }

    void HandleCorrectAnswer()
    {
        Debug.Log("Correct Answer!");
        NextWord();
        // Call the function to show next flashcard or handle correct response
    }

    void HandleIncorrectAnswer()
    {
        Debug.Log("Incorrect Answer!");
        NextWord();
        // Call the function to handle incorrect response
    }

    // Call the NextWord function from the FlipCard script
    private void NextWord()
    {
        if (flipCardScript != null)
        {
            flipCardScript.NextWord(); // Calls the NextWord method from FlipCard
            Debug.Log("Next Word Triggered");
        }
        else
        {
            Debug.LogError("FlipCard script reference is not assigned.");
        }
    }
}
