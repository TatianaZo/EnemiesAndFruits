using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool canMove = false;
    [SerializeField] private float speed;
    private Vector3 randomPoint;

    private void Start()
    {
        Invoke ("AllowMove", 3);
        StartCoroutine(Move());
    }

    private void Update()
    {
        MoveNextPoint();
    }

    private void MoveNextPoint()
    {
        if (canMove)
        {
            float step = speed * Time.deltaTime;
            Vector3 target = randomPoint;
            transform.position = Vector3.MoveTowards(transform.position, target, step);//движение врагов по рандомным координатам
        }
    }

    IEnumerator Move()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.3f, 2f));
            randomPoint = new Vector3(Random.Range(-2.2f, 2.2f), Random.Range(-5f, 5f), 0);// генерация рандомных координат
        }
    }

    private void AllowMove()//разрешить врагам движение через 3 сек. после начала игры
    {
        canMove = true;
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
    }
}