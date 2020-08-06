using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TetrisBlock : MonoBehaviour
{
    private Vector2 startPosition, endPosition;
    public int deadLine=12;
    public Vector3 rotationPoint;
    float lastFall = 0f;
    MatrixGrid matrixGrid;
    void Awake(){
        matrixGrid = GameObject.FindGameObjectWithTag("Spawner").GetComponent<MatrixGrid>();
    }
    void Update(){
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startPosition = Input.GetTouch(0).position;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endPosition = Input.GetTouch(0).position;
       if(startPosition.x>endPosition.x && Mathf.Abs(startPosition.x-endPosition.x)> Mathf.Abs(startPosition.y-endPosition.y)) {
            transform.position += new Vector3(-1,0,0);
            if (IsValidGridPosition())
            {
                UpdateMatrixGrid();
            }
            else
            {
                transform.position += new Vector3(1,0,0);
            }
        }else if (startPosition.x < endPosition.x && Mathf.Abs(startPosition.x - endPosition.x) > Mathf.Abs(startPosition.y - endPosition.y))
        {
            transform.position += new Vector3(1, 0, 0);
            if (IsValidGridPosition())
            {
                UpdateMatrixGrid();
            }
            else
            {
                transform.position += new Vector3(-1, 0, 0);
            }
        }else if (startPosition.y < endPosition.y && Mathf.Abs(startPosition.x - endPosition.x) < Mathf.Abs(startPosition.y - endPosition.y))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint),new Vector3(0,0,1),90);
            if (IsValidGridPosition())
            {
                UpdateMatrixGrid();
            }
            else
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            }
        }
        else if((startPosition.y> endPosition.y) && Mathf.Abs(startPosition.x - endPosition.x) < Mathf.Abs(startPosition.y - endPosition.y))
            {
            transform.position += new Vector3(0,-1,0);
            if (IsValidGridPosition())
            {
                UpdateMatrixGrid();
            }
            else
            {
                transform.position += new Vector3(0, 1, 0);
                matrixGrid.DeleteWholeRows();
                    Spawner.ifDraw = true;
                    enabled = false;
            }
            }
        }
        if ( Time.time - lastFall >= 1)
        {
            transform.position += new Vector3(0, -1, 0);
            if (IsValidGridPosition())
            {
                UpdateMatrixGrid();
            }
            else
            {
                transform.position += new Vector3(0, 1, 0);
                matrixGrid.DeleteWholeRows();
                Spawner.ifDraw = true;
                enabled = false;
            }
            lastFall = Time.time;
        }
    }
    bool IsValidGridPosition()
    {
        foreach(Transform child in transform)
        {
            Vector2 v = matrixGrid.RoundVector(child.position);

            if (!matrixGrid.IsInsideBorder(v))
            {
                if (transform.position.y>13)
                {
                    Spawner.level = 0;
                    SceneManager.LoadScene(2);
                }
                return false;
            }
            if (MatrixGrid.grid[(int)v.x,(int)v.y]!=null&&MatrixGrid.grid[(int)v.x,(int)v.y].parent!=transform)
                {
                if (transform.position.y > deadLine)
                {
                    Spawner.level = 0;
                    SceneManager.LoadScene(2);
                }
                return false;
                }
        }
        return true;
    }
    void UpdateMatrixGrid()
    {
        for(int y = 0; y < MatrixGrid.column; ++y)
        {
            for(int x = 0; x < MatrixGrid.row; ++x)
            {
                if (MatrixGrid.grid[x, y] != null)
                {
                    if (MatrixGrid.grid[x, y].parent == transform)
                    {
                        MatrixGrid.grid[x, y] = null;
                    }
                }
            }
        }
        foreach (Transform child in transform)
        {
            Vector2 v = matrixGrid.RoundVector(child.position);
            MatrixGrid.grid[(int)v.x, (int)v.y] = child;
        }
    }
}
