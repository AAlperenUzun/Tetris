using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class MatrixGrid : MonoBehaviour
{
    Spawner spawner;
    public static int row=8;
    public static int column=18;
    public static Transform[,] grid = new Transform[row, column];
    private void Awake()
    {
        spawner=GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();
    }
    public Vector2 RoundVector(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }
    public bool IsInsideBorder(Vector2 pos)
    {
        return ((int)pos.x>=0&&(int)pos.x<row&&(int)pos.y>=0);
    } 
    public void DeleteRow(int y)
    {
        for(int x = 0; x < row; ++x)
        {
            GameObject.Destroy(grid[x,y].gameObject);
            grid[x, y] = null;
        }
    }
    public void DecreaseRow(int y)
    {
        for (int x=0;x<row;++x)
        {
            if (grid[x, y] != null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;
                grid[x, y - 1].position += new Vector3(0,-1,0);
            }
        }
    }
    public void DecreaseRowsAbove(int y)
    {
        for(int i=y;i<column; ++i)
        {
            DecreaseRow(i);
        }
    }public bool isRowFull(int y)
    {
        for (int x=0;x<row;++x)
        {
            if (grid[x, y] == null)
            {
                return false;
            }
        }
        return true;
    }
    public void DeleteWholeRows()
    {
        for (int y=0; y<column;++y)
        {
            if (isRowFull(y))
            {
                spawner.AddPoint();
                DeleteRow(y);
                DecreaseRowsAbove(y+1);
                --y;
            }
        }
    }
}
