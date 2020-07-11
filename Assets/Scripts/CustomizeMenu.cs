using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizeMenu : MonoBehaviour
{
    //public Button exitButton;
    //public Button toMainMenuButton;

    public GameObject[] platformPrefabs = new GameObject[2];
    private GameObject[] platforms = new GameObject[2];
    private int currentPlatformId = 0;
    public int selectedPlatformId = 0;
    private int distanceBetweenPlatforms = 20;

    //CustomizeMenu;
    public Button Select;
    public Button LeftArrow;
    public Button RightArrow;

    public void PressArrowLeft()
    {
        currentPlatformId--;
        movePlatforms(1);
        UpdateMenu();
        //Перемещение след. платформы на середину, а пред. в данном случае вправо
    }

    public void PressArrowRight()
    {
        currentPlatformId++;
        movePlatforms(-1);
        UpdateMenu();
    }

    public void PressSelect()
    {
        selectedPlatformId = currentPlatformId;
        UpdateMenu();
        //Выбор и закремпление платформы за игроком
    }

    private void UpdateMenu()
    {
        if (currentPlatformId == selectedPlatformId)
            Select.enabled = false;
        else
            Select.enabled = true;

        if (currentPlatformId <= 0)
            LeftArrow.enabled = false;
        else
            LeftArrow.enabled = true;

        if (currentPlatformId >= 1)
            RightArrow.enabled = false;
        else
            RightArrow.enabled = true;
    }

    private void movePlatforms(int directory)
    {
        for (int i = 0; i < 2; i++)
        {
            platforms[i].transform.position += new Vector3(distanceBetweenPlatforms * directory, 0, 0);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentPlatformId = 0;
        LeftArrow.enabled = false;
        //Спавним платформы, чтобы они были видны в магазине
        for (int i = 0; i < 2; i++)
        {
            //platforms[i] = this.Instantiate(platformPrefabs[i], new Vector3(i * distanceBetweenPlatforms, 0, 0), new Quaternion(0, 0, 0, 0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
