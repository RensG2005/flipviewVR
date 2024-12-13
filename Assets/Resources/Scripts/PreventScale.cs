using UnityEngine;

public class PreventScale : MonoBehaviour
    {
    private Vector3 initialScale;

    void Start()
    {
        // Save the initial scale of the object
        initialScale = transform.localScale;
    }

    void Update()
    {
        // Continuously lock the scale
        transform.localScale = initialScale;
    }
}
