using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LASER : MonoBehaviour
{
    private bool coroutineAllowed = true;
    private GameObject oldTarget;
    private TARGET Tscript;
    public float count = 0;
    public FOLLOW follow;
    public GameObject TARGET;
    void Start()
    {
        oldTarget = TARGET;
    }
    void Update()
    {
        if (oldTarget != TARGET)
        {
            oldTarget = TARGET;
            count = 0;
        }

        follow = this.transform.parent.GetComponent<FOLLOW>();
        GameObject[] temp = GameObject.FindGameObjectsWithTag("ENEMY");
        Transform[] allEnemies = new Transform[temp.Length];
        Transform[] allInRange = new Transform[follow.SORTED.Count];
        for (var i = 0; i < temp.Length; i++)
        {
            allEnemies[i] = temp[i].transform;
        }
        for (var i = 0; i < follow.SORTED.Count; i++)
        {
            GameObject temp2 = GameObject.Find(follow.SORTED[i].ToString());
            if (temp2 != null)
            {
                allInRange[i] = temp2.transform;
            }
        }
        this.transform.localScale = new Vector3(1, 0, 1);
        if (allEnemies.Length > 0)
        {
            Transform TARGETtrans = GetClosestEnemy(allEnemies);
            if (allInRange.Contains(TARGETtrans))
            {
                Vector3 targetPos = TARGETtrans.position;
                Vector3 thisPos = transform.parent.position;
                targetPos.x = targetPos.x - thisPos.x;
                targetPos.y = targetPos.y - thisPos.y;
                float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
                transform.parent.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 270));
                TARGET = TARGETtrans.gameObject;
                this.transform.localScale = new Vector3(1, Vector3.Distance(TARGET.transform.position, this.transform.parent.position), 1);
                Tscript = TARGET.GetComponent<TARGET>();
                if (coroutineAllowed)
                {
                    StartCoroutine(Shoot());
                }
                Tscript.lives -= count;
            }
        }  
    }
    private IEnumerator Shoot()
    {
        coroutineAllowed = false;
        count += 0.003f;
        yield return new WaitForSeconds(0.1f);
        coroutineAllowed = true;
    }
    Transform GetClosestEnemy(Transform[] enemies)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.parent.position;
        foreach (Transform potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
        return bestTarget;
    }
}