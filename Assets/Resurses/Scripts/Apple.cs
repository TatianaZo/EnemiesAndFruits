using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    public void Respawn()
    {
        gameObject.SetActive(false);
        transform.position = new Vector3(Random.Range(-2.2f, 2.2f), Random.Range(-5f, 5f), 0);//рандомная смена позиций
        gameObject.SetActive(true);
    }
}
