using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellGrid : MonoBehaviour {

    public int x = 5;
    public int z = 5;

    [SerializeField]
    public Cell[] cells;

    private void Awake()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            cells[i].cellID = i;
        }
    }

    public Cell getTargetCell(Cell currentCell, Direction facingDirection)
    {
        Cell targetCell;

        switch (facingDirection)
        {
            case Direction.UP:
                if (currentCell.cellID > 19)
                {
                    return null;
                }
                else
                {
                    return targetCell = cells[currentCell.cellID + 5];
                }

            case Direction.DOWN:
                if (currentCell.cellID < 5)
                {
                    return null;
                }
                else
                {
                    return targetCell = cells[currentCell.cellID - 5];
                }


            case Direction.LEFT:
                if (currentCell.cellID % 5 == 0)
                {
                    return null;
                }
                else
                {
                    return targetCell = cells[currentCell.cellID - 1];
                }

            case Direction.RIGHT:
                if (currentCell.cellID % 5 == 4)
                {
                    return null;
                }
                else
                {
                    return targetCell = cells[currentCell.cellID + 1];
                }
        }
        return null;
    }
}
