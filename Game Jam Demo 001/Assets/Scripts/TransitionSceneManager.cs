using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionSceneManager : MonoBehaviour
{
    private float time;

    void Start()
    {
        time = 30f;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if(time <= 0){
            SceneManager.LoadScene("Main");
        }
    }
}
