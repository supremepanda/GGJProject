using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManage : MonoBehaviour
{
    [SerializeField] GameObject inGamePanel;
    private bool isPanelActive;
    private bool hintCanActive;
    private int hintIndexNumber;
    private string [] hints;
    [SerializeField] Text hintText;
    [SerializeField] Button hintButton;
    private void Start() {
        hints = new string[4];
        hints[0] = "Repair";
        hints[1] = "Rigth-down-left";
        hints[2] = "Fibonacci";
        hints[3] = "Color venn diagram for lights";
       
        hintCanActive = true;
        hintIndexNumber = 0;
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if(!inGamePanel.activeSelf){
                inGamePanel.SetActive(true);
                Time.timeScale = 0;
            }
            else{
                inGamePanel.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    

    public void ReturnButton(){
        inGamePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void MainMenuButton(){
        //Time.timeScale = 1;
        //SceneManager.LoadScene("MainMenu");
    }

    public void HintButton(){
        if(hintCanActive){
            hintText.text = hints[hintIndexNumber];
        }
    }
}
