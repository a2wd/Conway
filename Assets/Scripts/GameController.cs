using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Cell CellPrefab;

    public float UpdateInterval;

    public int Width;

    public int Height;

    private List<Cell> allCells = new List<Cell>();

    public bool IsRunning { get; set; }

    private float deltaTime = 0f;

    private void Start()
    {
        IsRunning = false;
        int xMax = Width;
        int yMax = Height;
        Cell[,] cells = new Cell[xMax,yMax];

        for (int x = 0; x < xMax; x++)
        {
            for (int y = 0; y < yMax; y++)
            {
                Cell cell = Instantiate<Cell>(CellPrefab, new Vector3(x, y, 0), Quaternion.identity);
                cell.gameObject.GetComponent<Renderer>().material.SetColor("_Color", CellColours.Dead);
                cell.GameController = this;
                cells[x, y] = cell;
                allCells.Add(cell);
            }
        }

        for (int x = 0; x < xMax; x++)
        {
            for (int y = 0; y < yMax; y++)
            {
                Cell cell = cells[x, y];
                cell.AddNeighbour(cells[(x + xMax - 1) % xMax, (y + yMax - 1) % yMax]);
                cell.AddNeighbour(cells[x, (y + yMax - 1) % yMax]);
                cell.AddNeighbour(cells[(x + 1) % xMax, (y + yMax - 1) % yMax]);
                cell.AddNeighbour(cells[(x + xMax - 1) % xMax, y]);
                cell.AddNeighbour(cells[(x + 1) % xMax, y]);
                cell.AddNeighbour(cells[(x + xMax - 1) % xMax, (y + 1) % yMax]);
                cell.AddNeighbour(cells[x, (y + 1) % yMax]);
                cell.AddNeighbour(cells[(x + 1) % xMax, (y + 1) % yMax]);

            }
        }        
    }

    private void Update()
    {
        if (Input.GetButton("Jump"))
        {
            IsRunning = true;
        }

        if (IsRunning)
        {
            deltaTime += Time.deltaTime;

            if (deltaTime > UpdateInterval)
            {
                deltaTime = 0f;
                allCells.ForEach(cell => cell.SetNextState());
                allCells.ForEach(cell => cell.UpdateState());
            }
        }
    }
}
