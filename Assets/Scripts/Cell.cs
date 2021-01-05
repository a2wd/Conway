using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameController GameController { get; set; }

    public CellState CurrentCellState { get; private set;}

    private CellState nextCellState;

    private List<Cell> neighbouringCells;

    private Behaviour halo;

    private Renderer objectRenderer;

    private void Awake()
    {
        CurrentCellState = CellState.DEAD;
        neighbouringCells = new List<Cell>();

        halo = (Behaviour) GetComponent("Halo");
        objectRenderer = gameObject.GetComponent<Renderer>();
    }

    private void UpdateHaloColour(Color color)
    {
        SerializedObject serializedHalo = new SerializedObject(halo);
        serializedHalo.FindProperty("m_Color").colorValue = color;
        serializedHalo.ApplyModifiedProperties();
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
        objectRenderer.material.SetColor("_Color", nextColor);
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (GameController.IsRunning)
        {
            GameController.IsRunning = false;
        }

        if (CurrentCellState == CellState.ALIVE)
        {
            UpdateHaloColour(CellColours.Dead);
            CurrentCellState = CellState.DEAD;
            objectRenderer.material.SetColor("_Color", CellColours.Dead);
        }
        else if (CurrentCellState == CellState.DEAD)
        {
            UpdateHaloColour(CellColours.Alive);
            CurrentCellState = CellState.ALIVE;
            objectRenderer.material.SetColor("_Color", CellColours.Alive);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        halo.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        halo.enabled = false;
    }
}
