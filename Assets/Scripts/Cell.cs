using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IPointerClickHandler
{
    public bool IsRunning { get; set; }

    public CellState CellState { get; private set;}

    private CellState nextCellState;

    private List<Cell> neighbouringCells;

    public Cell()
    {
        neighbouringCells = new List<Cell>();
    }

    public void AddNeighbour(Cell cell)
    {
        neighbouringCells.Add(cell);
    }

    public void SetNextState()
    {
        int livingNeighbours = neighbouringCells.Count(cell => cell.CellState == CellState.ALIVE);

        if (CellState == CellState.ALIVE && (livingNeighbours == 2 || livingNeighbours == 3))
        {
            return;
        }

        if (CellState == CellState.DEAD && livingNeighbours == 3)
        {
            nextCellState = CellState.ALIVE;
            return;
        }

        nextCellState = CellState.DEAD;
    }

    public void UpdateState()
    {
        CellState = nextCellState;

        Color nextColor = CellState == CellState.ALIVE ? Color.green : Color.red;
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", nextColor);
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (IsRunning)
        {
            return;
        }

        if (CellState == CellState.DEAD)
        {
            CellState = CellState.ALIVE;
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
        }

        CellState = CellState.DEAD;
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);        
    }
}
