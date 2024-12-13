using System.Collections.Generic;
using UnityEngine;

public class CSVReader : MonoBehaviour
{
    public TextAsset csvFile; // Drag your CSV file into this field in the Inspector

    public List<WordPair> wordPairs = new List<WordPair>();

    void Start()
    {
        LoadCSV();
    }

    private void LoadCSV()
    {
        // Split the CSV file into rows
        string[] data = csvFile.text.Split(new char[] { '\n' });

        // Loop through the rows (start from 1 to skip the header)
        for (int i = 1; i < data.Length; i++)
        {
            // Split each row into columns
            string[] row = data[i].Split(',');

            if (row.Length == 2) // Ensure each row has two columns
            {
                WordPair pair = new WordPair(row[0], row[1]);
                wordPairs.Add(pair); // Store the word-translation pair
            }
        }

        Debug.Log($"Loaded {wordPairs.Count} word pairs!");
    }

    public WordPair GetWordPair(int index)
    {
        if (index >= 0 && index < wordPairs.Count)
            return wordPairs[index];
        return null;
    }

    public WordPair GetNextUnlearnedWord(int currentIndex)
    {
        // Loop through the list starting from the current index
        while (currentIndex < wordPairs.Count)
        {
            WordPair pair = wordPairs[currentIndex];
            currentIndex++; // Increment the index for the next call
            if (!pair.learned)
            {
                return pair; // Return the first unlearned word found
            }
        }
        
        return null; // All words are learned
    }

}

[System.Serializable]
public class WordPair
{
    public string word;
    public string translation;
    public int helpLevel; // Tracks the number of images (0–3)
    public bool learned; // Tracks if the word is fully learned

    public WordPair(string word, string translation)
    {
        this.word = word;
        this.translation = translation;
        this.helpLevel = 1; // 3 is maximum, starting at 1 help
        this.learned = false; // Not learned by default
    }
}

