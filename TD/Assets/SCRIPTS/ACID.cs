using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ACID : MonoBehaviour
{
    public GameObject target;
    private bool coroutineAllowed = true;
    private bool coroutine2Allowed = true;
    private TARGET Tscript;
    private void Start()
    {
        Destroy(gameObject, 5f);
    }
    private void Update()
    {
        if (target != null)
        {
            Vector3 desPos = new Vector3(target.transform.position.x, target.transform.position.y,-1);
            float offset = 0.1f;
            if ((desPos.x - offset < this.transform.position.x) && (this.transform.position.x < desPos.x + offset) && (desPos.y - offset < this.transform.position.y) && (this.transform.position.y < desPos.y + offset))
            {
                this.transform.position = desPos;
                if (coroutine2Allowed)
                {
                    StartCoroutine(Shoot());
                }
            }
            else if (coroutineAllowed)
            {
                StartCoroutine(Move(desPos));
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private IEnumerator Shoot()
    {
        coroutine2Allowed = false;
        Tscript = target.GetComponent<TARGET>();
        Tscript.lives -= 0.1f;
        yield return new WaitForSeconds(0.1f);
        coroutine2Allowed = true;
    }
    private IEnumerator Move(Vector3 targPos)
    {
        coroutineAllowed = false;
        this.transform.position = Vector3.MoveTowards(this.transform.position, targPos, 0.1f);
        yield return new WaitForSeconds(0.001f);
        coroutineAllowed = true;
    }
}
