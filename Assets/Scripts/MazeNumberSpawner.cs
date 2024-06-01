using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class MazeNumberSpawner : MonoBehaviour
{
    public TextMeshPro[] textBoxes; // Array of TextMeshPro text boxes in your scene

    private List<int> numbers = new List<int> { 6, 1, 9 };

    private void Start()
    {
        DisableRandomTextBoxes();
        SetTextBoxValues();
    }

    private void DisableRandomTextBoxes()
    {
        // Shuffle the array of text boxes
        ShuffleArray(textBoxes);

        // Disable the first 2 text boxes
        textBoxes[0].gameObject.SetActive(false);
        textBoxes[1].gameObject.SetActive(false);
    }

    private void SetTextBoxValues()
    {
        // Shuffle the list of numbers
        ShuffleList(numbers);

        // Set the text for each enabled text box
        int index = 0;
        for (int i = 0; i < textBoxes.Length; i++)
        {
            if (textBoxes[i].gameObject.activeSelf)
            {
                textBoxes[i].text = numbers[index].ToString();
                index++;
            }
        }
    }

    private void ShuffleArray<T>(T[] array)
    {
        // Implementation of the Fisher-Yates shuffle algorithm
        System.Random rng = new System.Random();
        int n = array.Length;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = array[k];
            array[k] = array[n];
            array[n] = value;
        }
    }

    private void ShuffleList<T>(List<T> list)
    {
        // Implementation of the Fisher-Yates shuffle algorithm
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}