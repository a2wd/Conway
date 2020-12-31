using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IPointerClickHandler
{
    public GameController GameController { get; set; }

    public CellState CurrentCellState { get; private set;}

    private CellState nextCellState;

    private List<Cell> neighbouringCells;

    public Cell()
    {
        CurrentCellState = CellState.DEAD;
        neighbouringCells = new List<Cell>();
    }

    public void AddNeighbour(Cell cell)
    {
        neighbouringCells.Add(cell);
    }

    public void SetNextState()
    {
        int livingNeighbours = neighbouringCells.Count(cell => cell.CurrentCellState == CellState.ALIVE);

        if (CurrentCellState == CellState.ALIVE && (livingNeighbours == 2 || livingNeighbours == 3))
        {
            nextCellState = CellState.ALIVE;
            return;
        }

        if (CurrentCellState == CellState.DEAD && livingNeighbours == 3)
        {
            nextCellState = CellState.ALIVE;
            return;
        }

        nextCellState = CellState.DEAD;
    }

    public void UpdateState()
    {
        CurrentCellState = nextCellState;

        Color nextColor = CurrentCellState == CellState.ALIVE ? CellColours.Alive : CellColours.Dead;
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", nextColor);
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (GameController.IsRunning)
        {
            GameController.IsRunning = false;
        }

        if (CurrentCellState == CellState.ALIVE)
        {
            CurrentCellState = CellState.DEAD;
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", CellColours.Dead);
        }
        else if (CurrentCellState == CellState.DEAD)
        {
            CurrentCellState = CellState.ALIVE;
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", CellColours.Alive);
        }
    }
}
