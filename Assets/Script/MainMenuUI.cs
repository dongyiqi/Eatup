using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public Button BtnStart; 
    // Start is called before the first frame update
    void Start()
    {
        BtnStart.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Game", LoadSceneMode.Single);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
