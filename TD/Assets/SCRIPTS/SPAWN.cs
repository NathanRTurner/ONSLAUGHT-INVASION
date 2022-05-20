using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPAWN : MonoBehaviour
{
    private int amount;
    public GameObject[] order;
    public TARGET Pscript;
    private bool coroutineAllowed = true;
    float random;
    void Start()
    {

        if (coroutineAllowed)
        {
            StartCoroutine(Do());
        }
    }
    private IEnumerator Do()
    {
        coroutineAllowed = false;
        for (var i = 0; i < order.Length; i++)
        {
            Pscript = order[i].GetComponent<TARGET>();
            float speed = 1.5f;//Pscript.speed;
            random = 2;//* (0.9f + (1 / Random.Range(1f, 10f)));
            //Debug.Log(i.ToString() + "  " + random);
            var Circle = Instantiate(order[i], this.transform.position, Quaternion.identity);
            Circle.gameObject.name = i.ToString();
            Circle.transform.eulerAngles = this.transform.eulerAngles;
            yield return new WaitForSeconds(random/speed);
        }
        coroutineAllowed = true;
        
    }
}
