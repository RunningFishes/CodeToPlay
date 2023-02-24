using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GenerateStageButton : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonPrefab;
    [SerializeField]
    private GameObject canvas;

    float startX = -490;
    float startY = 200;

    float offsetX = 250;
    float offsetY = 150;


    void Start()
    {
        int stageReach = GameController.instance.GetStage();
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                GameObject button = Instantiate(buttonPrefab) as GameObject;
                button.transform.SetParent(canvas.transform);
                button.name = "Stage" + (i * 5 + j + 1).ToString();
                float posX = startX + offsetX * j;
                float posY = startY - offsetY * i;

                button.transform.localPosition = new Vector3(posX, posY, 0);
                button.transform.localScale = new Vector3(2, 2, 2);

                int stageNumber = i * 5 + j + 1;
                button.GetComponentInChildren<TextMeshProUGUI>().text = stageNumber.ToString();

                if (stageNumber > stageReach)
                {
                    button.GetComponent<Button>().interactable = false;
                }
                else
                {
                    button.GetComponent<Button>().interactable = true;
                }

                button.GetComponent<LoadPlayStage>().SetStageName(button.name);
                button.GetComponent<Button>().onClick.AddListener(() => button.GetComponent<LoadPlayStage>().LoadStage());
            }
        }
    }
}
