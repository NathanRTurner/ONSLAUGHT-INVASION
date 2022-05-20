using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TESLA : MonoBehaviour
{
    public FOLLOW follow;
    [SerializeField] private Transform lightning;
    private bool coroutineAllowed = true;
    private void Start()
    {
        follow = this.GetComponent<FOLLOW>();
    }
    private void Update()
    {
        if (coroutineAllowed && follow.canShoot)
        {
            StartCoroutine(Shoot());
        }
    }
    private IEnumerator Shoot()
    {
        coroutineAllowed = false;
        if (follow.SORTED.Count > 0)
        {
            for (var i = 0; i < follow.SORTED.Count; i++)
            { 
                GameObject target = GameObject.Find(follow.SORTED[i].ToString());
                if (target != null)
                {
                    Vector3 startPos = new Vector3(this.transform.position.x, this.transform.position.y, -0.4f);
                    Vector3 targPos = new Vector3(target.transform.position.x, target.transform.position.y, -0.4f);
                    Transform ilightning = Instantiate(lightning, startPos, Quaternion.identity);
                    Vector3 shootDir = targPos - startPos;
                    float dist = Vector3.Distance(targPos, startPos);
                    ilightning.transform.localScale = new Vector3(1, dist, 1);
                    ilightning.GetComponent<LIGHTNING>().Setup(shootDir, target, follow.SORTED.Count);
                    
                }
            }
            yield return new WaitForSeconds(0.75f);
        }
        coroutineAllowed = true;
    }
}
