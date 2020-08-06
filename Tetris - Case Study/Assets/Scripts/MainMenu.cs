using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text TotalPointText;
    private void Awake()
    {
        Spawner.totalPoint = (Spawner.level-1) * 10 + Spawner.currentPoint;
        TotalPointText.text = Spawner.totalPoint + "";
    }
    public void StartGame()
    {
        Spawner.currentPoint = 0;
        Spawner.level += 1;
        Spawner.goalPoint=Spawner.level*10;
        SceneManager.LoadScene(1);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

}
