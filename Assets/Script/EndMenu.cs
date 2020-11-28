using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    public Text TxtScore;
    public Text TxtDrop;
    public Button BtnPlayAgain;
    // Start is called before the first frame update
    void Start()
    {
        TxtScore.text = GameLogic.Instance.Score.ToString();
        TxtDrop.text =  GameLogic.Instance.DropFoodCount.ToString();
        BtnPlayAgain.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Game", LoadSceneMode.Single);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
