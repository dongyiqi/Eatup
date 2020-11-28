using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public float Speed;
    public float Height;
    public Vector2 Position;
    public Sprite[] EatSprites;
    private RectTransform _transform;
    private Image _image;

    private void Awake()
    {
        Instance = this;
        _transform = transform as RectTransform;
        _image = GetComponent<Image>();
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public void Tick(float deltaTime)
    {
        Position = _transform.anchoredPosition;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) ||
            (Input.GetMouseButton(0) && Input.mousePosition.x < 0.5f * Screen.width))
        {
            Position.x -= Time.deltaTime * Speed;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) ||
            (Input.GetMouseButton(0) && Input.mousePosition.x > 0.5f * Screen.width))
        {
            Position.x += Time.deltaTime * Speed;
        }

        //keep position in range
        Position.x = math.clamp(Position.x, -0.5f * GameDefination.HorizontalRange + GameDefination.Border,
            0.5f * GameDefination.HorizontalRange - GameDefination.Border);
        _transform.anchoredPosition = Position;
    }

    private bool _eating;

    public void TryToEat()
    {
        if (!_eating)
        {
            StartCoroutine(_Eat());
        }
    }

    private IEnumerator _Eat()
    {
        _eating = true;
        _image.sprite = EatSprites[1];
        yield return new WaitForSeconds(0.25f);
        _image.sprite = EatSprites[0];

        _eating = false;
    }
}