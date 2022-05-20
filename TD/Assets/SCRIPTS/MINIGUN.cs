using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MINIGUN : MonoBehaviour
{
    public FOLLOW follow;
    [SerializeField] private Transform bullet;
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
        for (var i = 0; i < 10; i++)
        {
            GameObject target = follow.target;
            if (target != null)
            {
                transform.GetChild(1).gameObject.GetComponent<Animator>().SetFloat("FIRING", 1f);
                Vector3 startPos = new Vector3(this.transform.position.x, this.transform.position.y, -0.25f);
                Vector3 targPos = new Vector3(target.transform.position.x, target.transform.position.y, -0.25f);
                Transform ibullet = Instantiate(bullet, startPos, Quaternion.identity);
                Vector3 shootDir = targPos - startPos;
                ibullet.GetComponent<BULLET>().Setup(shootDir, target);
                yield return new WaitForSeconds(0.1f);
            }
            transform.GetChild(1).gameObject.GetComponent<Animator>().SetFloat("FIRING", 0f);
        }
        coroutineAllowed = true;
    }
}
