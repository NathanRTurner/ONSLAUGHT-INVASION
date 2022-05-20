using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TARGET : MonoBehaviour
{
    [SerializeField] public float lives = 1;
    public int on = 1;

    void Update()
    {
        transform.position += transform.right * Time.deltaTime * on * 1.5f;
        if (lives <= 0)
        {
            Destroy(gameObject);
        }
    }
}
