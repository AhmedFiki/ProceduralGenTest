using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{

    [SerializeField]
    List<int> possibleModels = new List<int>();

    public int currentModel = -1;

    [SerializeField]
    bool collapsed = false;

    [SerializeField]
    float entropy = 1;

    public Vector2Int pos;

    public ModelGrid modelGrid;

    private void Start()
    {

    }

    public void InitPossibleModels(int x)
    {
        for (int i = 1; i <= x; i++)
        {

            possibleModels.Add(i);
        }

        List<int> numbers = new List<int>();

        // Add numbers from 1 to 9 to the list
        for (int i = 1; i <= 9; i++)
        {
            numbers.Add(i);
        }
        collapsed = false;
        UpdatePossibleModels(numbers);
    }
    public void UpdatePossibleModels(List<int> neighborModel)
    {



        possibleModels = possibleModels.Intersect(neighborModel).ToList();
        UpdateCell();


    }
    public void UpdateCell()
    {
        if (!collapsed)
            entropy = possibleModels.Count / 9f;

    }
    public float GetEntropy()
    {
        return entropy;
    }
    public void SetEntropy(float x)
    {
        entropy = x;
    }
    public void Collapse()
    {
        if (possibleModels.Count < 1)
        {
            return; 
        }

        int randomIndex = Random.Range(0, possibleModels.Count); 
        Debug.Log("Collapsed cell in: " + pos);

        int randomElement = possibleModels[randomIndex]; 

        possibleModels.Clear();

        possibleModels.Add(randomElement);
        currentModel = randomElement;
        collapsed = true;
        entropy = 0;
    }
    public void SetImage(Sprite sprite)
    {
        GetComponent<Image>().sprite = sprite;
    }
    public List<int> GetPossibleModels()
    {
        return possibleModels;
    }
    public bool IsCollapsed()
    {
        return collapsed;
    }
}
