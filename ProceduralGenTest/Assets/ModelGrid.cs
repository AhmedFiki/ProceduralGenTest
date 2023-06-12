using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelGrid : MonoBehaviour
{
    Cell cell;
    public GameObject[] tiles;

    private void Start()
    {
        cell = GetComponentInParent<Cell>(); DeActivateTiles();

    }
    private void Update()
    {
       
    }
    public void HandleModelGrid()
    {
        if (cell.GetPossibleModels().Count > 1)
            UpdateGrid();
        else
            DeActivateTiles();
    }
    public void UpdateGrid()
    {
        DeActivateTiles();
        List<int> possibleModels = cell.GetPossibleModels();

        for(int i = 0; i < possibleModels.Count; i++)
        {
           tiles[ possibleModels[i]-1].SetActive( true );
        }
    }
    public void DeActivateTiles()
    {
        foreach(GameObject go in tiles)
        {
            go.SetActive( false );
        }
    }

}
