using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Food : MonoBehaviour
{
    public GameObject Score;
    public Vector2 SpeedRange = new Vector2(100, 200);

    // Start is called before the first frame update
    private float _speed;

    enum State
    {
        Drop,
        Eat,
        OnGround,
    }

    private State _state = State.Drop;

    void Start()
    {
        _speed = Random.Range(SpeedRange.x, SpeedRange.y);
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameLogic.Instance.GamePlaying) return;
        
        var position = ((RectTransform) transform).anchoredPosition;
        if (_state == State.Drop)
        {
            position.y -= _speed * Time.deltaTime;

            //check eat or destroy
            if (position.y < -500)
            {
                _state = State.OnGround;
                GameObject.Destroy(gameObject, 0.5f);
                //TODO:play destroy praticle
            }

            var playerPosition = PlayerController.Instance.Position;
            var delta = playerPosition - position;
            if (math.abs(delta.x) < 250 && math.abs(delta.y) < 200)
            {
                GameLogic.Instance.Score += 1;
                PlayerController.Instance.TryToEat();

                Debug.Log($"score1 total socre:{GameLogic.Instance.Score}");
                _state = State.Eat;
                GameObject.Destroy(gameObject, 0.25f);
                var scoreGameObject = GameObject.Instantiate(Score);
                scoreGameObject.transform.SetParent(GameLogic.Instance.Root);
                ((RectTransform) scoreGameObject.transform).anchoredPosition = position;
                GameObject.Destroy(scoreGameObject, 1);
            }
        }
        else if (_state == State.Eat)
        {
            position = Vector2.Lerp(position, PlayerController.Instance.Position, 10 * Time.deltaTime);
            ((RectTransform) transform).localScale *= 0.985f;
        }

        ((RectTransform) transform).anchoredPosition = position;
    }
}