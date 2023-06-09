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
    }
    public void UpdatePossibleModels(List<int> neighborModel)
    {
        /*  Debug.Log("1"+neighborModel.Count);
          Debug.Log("2" + possibleModels.Count);
          Debug.Log("3" + possibleModels.Intersect(neighborModel).Count());
          Debug.Log("4" + neighborModel.Intersect(possibleModels).Count());*/


        possibleModels = possibleModels.Intersect(neighborModel).ToList();
        UpdateCell();


    }
    public void UpdateCell()
    {
        if (!collapsed)
            entropy = possibleModels.Count / 9f;
        //Debug.Log("Entropy updated "+entropy);

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
            return; // Nothing to delete, or only one element
        }

        int randomIndex = Random.Range(0, possibleModels.Count); // Get a random index
        Debug.Log("Collapsed cell in: " + pos);
        //Debug.Log(randomIndex);
        //Debug.Log(possibleModels.Count);
        int randomElement = possibleModels[randomIndex]; // Get the random element

        possibleModels.Clear(); // Clear the list

        possibleModels.Add(randomElement); // Add the random element back to the list
        currentModel = randomElement;
        collapsed = true;
        entropy = 0;
        // GetComponent<Image>().color = Color.black;
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
