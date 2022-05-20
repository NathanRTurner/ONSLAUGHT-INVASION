using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLACE : MonoBehaviour
{
    public GameObject TOWER;
    public GameObject B;
    private bool coroutine2Allowed = true;
    void Start()
    {
        TOWER.transform.position = this.transform.position;
    }
    private void Update()
    {
        if (coroutine2Allowed)
        {
            StartCoroutine(Move(B.transform.position));
        }
    }
    private IEnumerator Move(Vector3 targPos)
    {
        coroutine2Allowed = false;
        float speed = 1f;
        TOWER.transform.position = Vector3.MoveTowards(TOWER.transform.position, targPos, 0.1f / speed);
        yield return new WaitForSeconds(0.01f);
        coroutine2Allowed = true;
    }
}
