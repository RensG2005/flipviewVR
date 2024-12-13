using UnityEngine;

public class imageRenderer : MonoBehaviour
{
    private string currentWord;
    private WordPair _pair;

    public WordPair Pair
    {
        get => _pair;
        set
        {
            _pair = value;
            if (_pair != null && _pair.word != currentWord)
            {
                currentWord = _pair.word;
                UpdateImage();
            }
        }
    }
    private void UpdateImage()
        {
            // Load the texture from Resources using the pair's word
            Texture2D texture = Resources.Load<Texture2D>(currentWord);
            Debug.Log(currentWord);
            Debug.Log(texture);

            if (texture != null)
            {
                Renderer quadRenderer = GetComponent<Renderer>();
                quadRenderer.material.mainTexture = texture;
            }
            else
            {
                Debug.LogError("Texture not found for word: " + currentWord);
            }
    }
}