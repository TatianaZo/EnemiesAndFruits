using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameManager gm;
    private float speed = 4.0f;
    private Vector2 target;
    private Vector2 position;
    private Camera cam;

    void Start()
    {
        target = new Vector2(0.0f, 0.0f);
        position = gameObject.transform.position;

        cam = Camera.main;
    }

    private void OnEnable()
    {
        gameObject.GetComponent<EdgeCollider2D>().enabled = false; //неуязвимость при перерождении на 3 сек
        Invoke("TurnOnCollisions", 3.0f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.3f);
        target = Vector3.zero;
    }

    void Update()
    {
        float step = speed * Time.deltaTime;

        // Движение игрока в точку клика
        transform.position = Vector2.MoveTowards(transform.position, target, step);
    }
    void OnGUI()
    {
        Event currentEvent = Event.current;
        Vector2 mousePos = new Vector2();
        Vector2 point = new Vector2();

        // Позиция тапа в мировом пространстве
        mousePos.x = currentEvent.mousePosition.x;
        mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;
        point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0.0f));

        if (Input.GetMouseButton(0))
        {
            // назначаем таргет в точку клика
            target = point;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Apple")
        {
            gm.AppleConsume();
            Debug.Log("омномном");
        }

        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("aaaaaaaaaaaauch");
            gameObject.SetActive(false);
            gm.PlayerDeath();
        }
    }

    private void TurnOnCollisions()
    {
        gameObject.GetComponent<EdgeCollider2D>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }
}
