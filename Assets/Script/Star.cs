using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Star : MonoBehaviour
{
    public Vector2 SpeedRange = new Vector2(100, 200);

    // Start is called before the first frame update
    private float _speed;

    void Start()
    {
        _speed = Random.Range(SpeedRange.x, SpeedRange.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameLogic.Instance.GamePlaying) return;

        var position = ((RectTransform) transform).anchoredPosition;
        position.y -= _speed * Time.deltaTime;

        //check eat or destroy
        if (position.y < -500)
        {
            GameObject.Destroy(gameObject, 0.5f);
            //TODO:play destroy praticle
        }

        var playerPosition = PlayerController.Instance.Position;
        var delta = playerPosition - position;
        if (math.abs(delta.x) < 250 && math.abs(delta.y) < 200)
        {
            GameObject.Destroy(gameObject);
            //finish all food
            foreach (var food in Food.FoodList)
            {
                food.EatImmediate();
            }
        }


        ((RectTransform) transform).anchoredPosition = position;
    }
}