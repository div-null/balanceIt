using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public GameObject[] platformPrefab = new GameObject[2];

    /*public GameObject title;
    public Button playButton;
    public Button customizeMenuButton;
    public Button exitButton;
    public Button toMainMenuButton;
    */
    /*
    public void PressPlay()
    {
        title.SetActive(false);
        toMainMenuButton.gameObject.SetActive(true);
        customizeMenuButton.gameObject.SetActive(false);
        playButton.gameObject.SetActive(false);
        ScoreText.gameObject.SetActive(true);
        StartCoroutine(GameScore());
        //Запускаем игру
    }

    public void OpenMenu()
    {
        title.SetActive(true);
        toMainMenuButton.gameObject.SetActive(false);
        customizeMenuButton.gameObject.SetActive(true);
        playButton.gameObject.SetActive(true);
        ScoreText.gameObject.SetActive(false);
        //Открываем меню, значит скрываем элементы игры
    }

    public void OpenCustomizeMenu()
    {
        title.SetActive(false);
        toMainMenuButton.gameObject.SetActive(true);
        customizeMenuButton.gameObject.SetActive(false);
        playButton.gameObject.SetActive(false);
        ScoreText.gameObject.SetActive(false);
        //Запускаем меню кастомизации
    }

    public void PressArrowLeft()
    {
        //Перемещение след. платформы на середину, а пред. в данном случае вправо
    }

    public void PressArrowRight()
    {

    }

    public void PressSelect()
    {
        //Выбор и закремпление платформы за игроком
    }
    */

    // Start is called before the first frame update
    void Start()
    {
        //OpenMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
