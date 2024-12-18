using UnityEngine;
using TMPro;


public class FlipCard : MonoBehaviour
{
    public TextMeshPro frontText; // Text on the front of the card
    public TextMeshPro backText;  // Text on the back of the card

    public WordPair pair;
    public DynamicImageUpdater imageUpdater1;
    public DynamicImageUpdater imageUpdater2;
    public DynamicImageUpdater imageUpdater3;

    private Vector3 initialScale;

    private CSVReader csvReader;  // Reference to the CSVReader
    private int currentIndex = 0; // Current word index

    void Start()
    {
        // Find the CSVReader in the scene
        csvReader = FindObjectOfType<CSVReader>();

        if (csvReader != null)
        {
            UpdateCard(); // Set the initial word on the card
        }
        else
        {
            Debug.LogError("CSVReader not found in the scene!");
        }
    }

    void Update() {
        initialScale = transform.localScale;
    }

    public void NextWord()
    {
        // Increment the index and loop back to 0 if needed
        currentIndex = (currentIndex + 1) % csvReader.wordPairs.Count;


        UpdateCard();

        string word1 = pair.word + "1";
        string word2 = pair.word + "2";
        string word3 = pair.word + "3";

        imageUpdater1.UpdateImage(word1); // Dynamically update to "exampleImage"
        imageUpdater2.UpdateImage(word2); // Dynamically update to "exampleImage"
        imageUpdater3.UpdateImage(word3); // Dynamically update to "exampleImage"

    }

    private void UpdateCard()
    {
        // Use GetNextUnlearnedWord to find the next unlearned word
        //currentIndex = (currentIndex + 1) % csvReader.wordPairs.Count;
        pair = csvReader.GetNextUnlearnedWord(ref currentIndex);

        if (pair != null)
        {
            Debug.Log($"Loaded {pair.word} with help level {pair.helpLevel}");
            frontText.text = pair.word;
            backText.text = pair.translation;

            string word1 = pair.word + "1";
            string word2 = pair.word + "2";
            string word3 = pair.word + "3";

            imageUpdater1.UpdateImage(word1); 
            imageUpdater2.UpdateImage(word2); 
            imageUpdater3.UpdateImage(word3); 

            int helpLevel = pair.helpLevel;

            imageUpdater1.gameObject.SetActive(helpLevel >= 2);
            imageUpdater2.gameObject.SetActive(helpLevel >= 1);
            imageUpdater3.gameObject.SetActive(helpLevel >= 3);
        }
        else
        {
            Debug.Log("All words are learned! Program complete.");
            //EndProgram(); // Implement your program completion logic here
        }
    }

    public void UpdateWordState(bool isCorrect)
    {
        pair = csvReader.GetWordPair(currentIndex);

        if (isCorrect)
        {
            if (pair.helpLevel == 0)
            {
                pair.learned = true; // Mark as learned if no help is needed
            }
            pair.helpLevel = Mathf.Max(0, pair.helpLevel - 1);
        }
        else
        {
            pair.helpLevel = Mathf.Min(3, pair.helpLevel + 1); // More help, max 3
        }
        UpdateCard();
    }


}
