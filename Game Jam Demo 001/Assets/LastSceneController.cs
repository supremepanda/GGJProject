using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LastSceneController : MonoBehaviour
{
    [SerializeField] GameObject imageObject;
    private Image image;
    [SerializeField] Sprite[] sprites;
    private float time;

    void Start()
    {
        time = 10f;
        Debug.Log("indeks = "+FPSController.instance.endingIndex);
        image = imageObject.GetComponent<Image>();
        image.sprite = sprites[FPSController.instance.endingIndex];
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
