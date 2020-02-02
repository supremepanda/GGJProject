using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManage : MonoBehaviour
{
    [SerializeField] GameObject inGamePanel;
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if(!inGamePanel.activeSelf){
                inGamePanel.SetActive(true);
                Time.timeScale = 0;
                FPSController.instance.paused=true;
                Cursor.lockState = CursorLockMode.None;
            }
            else{
                inGamePanel.SetActive(false);
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                FPSController.instance.paused=false;
            }
        }
    }

    public void ReturnButton(){
        inGamePanel.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        FPSController.instance.paused = false;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

 
}
