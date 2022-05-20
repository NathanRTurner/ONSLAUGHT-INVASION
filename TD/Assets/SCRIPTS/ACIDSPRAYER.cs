using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ACIDSPRAYER : MonoBehaviour
{
    public FOLLOW follow;
    [SerializeField] public Transform acid;
    private bool coroutineAllowed = true;
    private ACID Ascript;
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
        GameObject target = follow.target;
        if (target != null)
        {
            Transform iacid = Instantiate(acid, new Vector3(this.transform.position.x, this.transform.position.y, -1f), Quaternion.identity);
            Ascript = iacid.GetComponent<ACID>();
            Ascript.target = target;
            yield return new WaitForSeconds(2f);
        }
        
        coroutineAllowed = true;
    }
}
