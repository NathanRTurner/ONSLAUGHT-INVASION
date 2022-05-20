using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ROCKETLAUNCHER  : MonoBehaviour
{
    public FOLLOW follow;
    [SerializeField] private Transform rocket;
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
        GameObject target = follow.target;
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
        Vector3 startPos = new Vector3(this.transform.position.x, this.transform.position.y, -0.5f);
        Vector3 targPos = new Vector3(target.transform.position.x, target.transform.position.y, -0.5f);
        Transform irocket = Instantiate(rocket, startPos, Quaternion.identity);
        Vector3 shootDir = targPos - startPos;
        irocket.GetComponent<ROCKET>().Setup(shootDir, target);
        yield return new WaitForSeconds(1f);
        coroutineAllowed = true;
    }
}
