using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameLogic : MonoBehaviour
{
    public static GameLogic Instance;
    public GameObject EndMenu;
    public int Score;
    public int DropFoodCount;
    public Transform Root;
    public Sprite[] FoodSprites;
    public GameObject FoodPrefab;
    public Transform TimeProgress;

    public float GameTime = 60;

    private float _GameTimeLeft;
    public bool GamePlaying;

    private Vector2 SpawnRateRange = new Vector2(5, 1);
    public float SpawnRandomRate = 0.3f;

    private float _lastSpawnElapsedTime;
    private float _nextSpawnTime;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        Instance = this;
        _GameTimeLeft = GameTime;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        //play start animation
        yield return new WaitForSeconds(1);
        StartGame();
    }

    private void StartGame()
    {
        GamePlaying = true;
        Debug.Log($"start game");
        SpawnFood();
    }

    private void EndGame()
    {
        GamePlaying = false;
        GameObject.Instantiate(EndMenu);
    }

    // Update is called once per frame
    void Update()
    {
        var deltaTime = Time.deltaTime;
        if (GamePlaying)
        {
            _GameTimeLeft -= deltaTime;
            var rate = _GameTimeLeft / GameTime;
            TimeProgress.localScale = new Vector3(rate, 1, 1);
            PlayerController.Instance.Tick(deltaTime);
            if (_GameTimeLeft <= 0)
            {
                EndGame();
            }

            _TickSpawn(deltaTime);
        }
    }

    private void _TickSpawn(float deltaTime)
    {
        _lastSpawnElapsedTime += deltaTime;
        if (_lastSpawnElapsedTime > _nextSpawnTime)
        {
            SpawnFood();
            _lastSpawnElapsedTime = 0;
            var spawnProgress = (GameTime - _GameTimeLeft) / GameTime;
            var midSpawnTime = math.lerp(SpawnRateRange.x, SpawnRateRange.y, spawnProgress);
            _nextSpawnTime = midSpawnTime;
        }
    }

    private void SpawnFood()
    {
        var randomX = Random.Range(-0.5f * GameDefination.HorizontalRange + GameDefination.Border,
            0.5f * GameDefination.HorizontalRange - GameDefination.Border);
        var y = 600;
        var go = GameObject.Instantiate(FoodPrefab);
        go.transform.SetParent(Root);
        go.GetComponentInChildren<Image>().sprite = FoodSprites[Random.Range(0, FoodSprites.Length)];
        ((RectTransform) go.transform).anchoredPosition = new Vector2(randomX, y);
    }
}