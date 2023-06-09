using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Model", menuName = "Model")]
public class Model : ScriptableObject
{
    public int id;
    public List<int> topNeighbors = new List<int>();
    public List<int> bottomNeighbors = new List<int>();
    public List<int> rightNeighbors = new List<int>();
    public List<int> leftNeighbors = new List<int>();

    public Sprite sprite;

}
