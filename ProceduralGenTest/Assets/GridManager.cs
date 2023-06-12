using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [SerializeField]
    GameObject cellPrefab;
    [SerializeField]
    Vector2Int gridSize;
    [SerializeField]
    List<Cell> cells = new List<Cell>();
    [SerializeField]
    List<Model> models = new List<Model>();

    [Header("Collapse")]
    public Vector2Int collapseCell;

    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        NumberCells();
        InitCells();
        CollapseCell(cells[12]);

    }
    private void Update()
    {
        if(Input.GetKey(KeyCode.E))
        {
            CollapseRandomCell();
        }if(Input.GetKey(KeyCode.R))
        {
            ResetCells();
        }
    }
    public void ResetCells()
    {
        InitCells();

    }
    public void CollapseCell(Cell cell)
    {
        Debug.Log("collapsing cell:" + cell.pos);
        Vector2Int pos = cell.pos;
        cell.Collapse();
        Model currentModel = GetModel(cell.currentModel);
        cell.SetImage(currentModel.sprite);
        //update neighbor cells

        //top cell
        if (GetCellAt(pos.x, pos.y + 1) != null)
        {
            GetCellAt(pos.x, pos.y + 1).UpdatePossibleModels(currentModel.topNeighbors);

        };

        //bottom cell
        if (GetCellAt(pos.x, pos.y - 1) != null)
        {
            GetCellAt(pos.x, pos.y - 1).UpdatePossibleModels(currentModel.bottomNeighbors);
        }


        //right cell
        if (GetCellAt(pos.x + 1, pos.y) != null)
        {
            GetCellAt(pos.x + 1, pos.y).UpdatePossibleModels(currentModel.rightNeighbors);

        };

        //left cell
        if (GetCellAt(pos.x - 1, pos.y) != null)
        {

            GetCellAt(pos.x - 1, pos.y).UpdatePossibleModels(currentModel.leftNeighbors);

        };


        UpdateGrid();

    }

    public void UpdateGrid()
    {
        foreach (var cell in cells)
        {
            //cell.SetEntropy(cell.GetPossibleModels().Count / 9);
            cell.GetComponentInChildren<ModelGrid>().HandleModelGrid();
            if (cell.GetPossibleModels().Count == 1 && !cell.IsCollapsed())
            {
                CollapseCell(cell);

            }
        }
    }

    public Model GetModel(int index)
    {
        if (index < 0) return null;

        foreach (Model model in models)
        {

            if (model.id == index)
            {
                return model;
            }

        }
        return null;
    }
    public void InitCells()
    {
        foreach (Cell cell in cells)
        {
            cell.SetImage(null);
            
            cell.InitPossibleModels(9);

        }
        UpdateGrid();

    }
    public void Propagate()
    {


    }

    public Cell GetCellAt(int x, int y)
    {
        foreach (Cell cell in cells)
        {

            if (cell.pos == new Vector2Int(x, y)) return cell;

        }


        return null;


    }

    public void NumberCells()
    {
        int k = 0;
        for (int i = 9; i >= 0; i--)
        {

            for (int j = 0; j < 10; j++)
            {

                cells[i * 10 + j].pos = new Vector2Int(j, k);

            }
            k++;
        }

    }
    public void CollapseRandomCell()
    {
        if(AllCellsCollapsed())
        {
            return;
        }
        List<Cell> minEntropyCells = GetCellsWithMinEntropy(cells);

       CollapseCell(RandomlyPickCell(minEntropyCells));
    }
    bool AllCellsCollapsed()
    {
        foreach(Cell cell in cells)
        {
            if (!cell.IsCollapsed())
            {
                return false;
            }
        }
        return true;
    }

     List<Cell> GetCellsWithMinEntropy(List<Cell> cellList)
    {
        float minEntropy = cellList.Where(cell => cell.GetEntropy() != 0).Min(cell => cell.GetEntropy());

        List<Cell> cellsWithMinEntropy = cellList.Where(cell => cell.GetEntropy() == minEntropy).ToList();

        return cellsWithMinEntropy;
    }

    Cell RandomlyPickCell(List<Cell> cellList)
    {
        int randomIndex = Random.Range(0, cellList.Count);
        Cell selectedCell = cellList[randomIndex];

        return selectedCell;
    }


}
