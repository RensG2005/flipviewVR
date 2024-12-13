using UnityEngine;

public class DynamicImageUpdater : MonoBehaviour
{
    public string imageName; // The name of the image to load dynamically
    public string imageFolder = "Images"; // Folder in Resources where images are stored
    private Renderer objectRenderer;

    void Start()
    {
        // Get the Renderer component of the object
        objectRenderer = GetComponent<Renderer>();

        if (objectRenderer == null)
        {
            Debug.LogError("Renderer not found! Attach this script to a GameObject with a Renderer.");
        }
    }

    public void UpdateImage(string newImageName)
    {
        if (objectRenderer != null)
        {
            // Set the new image name
            imageName = newImageName;

            // Load the texture from the Resources folder
            Texture texture = Resources.Load<Texture>($"{imageFolder}/{imageName}");
  

            if (texture != null)
            {
                // Update the material's texture
                objectRenderer.material.mainTexture = texture;
                Debug.Log($"Image updated to: {imageName}");
            }
            else
            {
                Debug.LogWarning($"Image '{imageName}' not found in Resources/{imageFolder}!");
            }
        }
    }
}
