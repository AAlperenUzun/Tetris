using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    String listString="";
    public static bool ifDraw = true;
    public static bool notNull = false;
    public static int[] Blocknums;
    int collideCount = 0;
    public static bool changeColor = false;
    public int SquarePoint;
    public static int totalPoint;
    public static int level;
    public static int goalPoint;
    public static int currentPoint;
    public Text cText;
    public Text goalText;
    public Text levelText;
    public Text warning;
    public float stwarningTime=1f;
    public float warningTime;
    [SerializeField]
    public GameObject[] tetrisObjects;
    void Awake()
    {
        totalPoint = level * 10 + currentPoint;
        cText.text = currentPoint + "";
        goalText.text = goalPoint + "";
        levelText.text =level + "";
        Blocknums = new int[6];
        for (int i = 0; i < Blocknums.Length; i++)
        {
            Blocknums[i] = 0;
        }
    }
    public void DrawB()
    {
        if (Spawner.ifDraw)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && notNull)
            {
                changeColor = true;
                warning.text = "";
                for (int i = 0; i < Blocknums.Length; i++)
                {
                    collideCount += Blocknums[i];
                }
                    if (2<collideCount&&collideCount<=4)
                    {
                    notNull = false;
                    Spawner.ifDraw = false;
                    SpawnDraw(Blocknums);
                    collideCount = 0;
                }
                else{
                    warning.text = "Draw appropiate tetris block!";
                    notNull = false;
                    collideCount = 0;
                    for (int i = 0; i < Blocknums.Length; i++)
                    {
                        Blocknums[i] = 0;
                    }
                }
            }
        }
    }
    public void SpawnDraw(int[] blockList)
    {
        listString = "";
        int index = UnityEngine.Random.Range(0,tetrisObjects.Length);
        Vector3 randomPoint = transform.position;
        randomPoint.x = UnityEngine.Random.Range(1, MatrixGrid.row-1);
        
            for (int i=0;i<Blocknums.Length;i++)
            {
            listString += Blocknums[i]+"";
            }
        if (listString=="111000"||listString == "000111")
        {
            Instantiate(tetrisObjects[0], randomPoint, Quaternion.identity);
        }else if (listString == "100111" || listString == "111001")
        {
            Instantiate(tetrisObjects[1], randomPoint, Quaternion.identity);
        }
        else if (listString == "001111" || listString == "111100")
        {
            Instantiate(tetrisObjects[2], randomPoint, Quaternion.identity);
        }
        else if (listString == "011110")
        {
            Instantiate(tetrisObjects[3], randomPoint, Quaternion.identity);
        }
        else if (listString == "110110" || listString == "011011")
        {
            Instantiate(tetrisObjects[4], randomPoint, Quaternion.identity);
        }
        else if (listString == "010111" || listString == "111010")
        {
            Instantiate(tetrisObjects[5], randomPoint, Quaternion.identity);
        }
        else if (listString == "110011")
        {
            Instantiate(tetrisObjects[6], randomPoint, Quaternion.identity);
        }
        listString = "";
        for (int i = 0; i < Blocknums.Length; i++)
        {
            Blocknums[i] = 0;
        }
    }
    public void AddPoint()
    {
        currentPoint += 10*SquarePoint;
        totalPoint = level * 10 + currentPoint;
        
        if (currentPoint >= goalPoint)
        {
            SceneManager.LoadScene(3);
        }
        cText.text = currentPoint + "";
    }
}
