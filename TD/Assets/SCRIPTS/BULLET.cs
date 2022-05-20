using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BULLET : MonoBehaviour
{
    private bool flying = true;
    private bool coroutineAllowed = true;
    public float projectileSpeed = 3;
    private Vector3 shootDir;
    private GameObject target;
    private TARGET Tscript;
    public static float GetAngle(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0)
        {
            n += 360;
        }
        return n;
    }
    public void Setup(Vector3 shootDir, GameObject target)
    {
        //this.GetComponent<Animator>().SetBool("FLYING", true);
        this.shootDir = shootDir;
        this.target = target;
        transform.eulerAngles = new Vector3(0, 0, GetAngle(shootDir) - 90);
        Destroy(gameObject, 5f);
    }
    void Update()
    {
        if (flying)
        {
            transform.position += shootDir * projectileSpeed * Time.deltaTime;
        }
        Collider[] C = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 8);
        List<int> TOUCHING = new List<int>();
        TOUCHING.Clear();
        if (C.Length > 0)
        {
            for (var i = 0; i < C.Length; i++)
            {
                //Debug.Log(C[i].name[0]);
                string N = C[i].name[0].ToString();
                if ((N == "0") || (N == "1") || (N == "2") || (N == "3") || (N == "4") || (N == "5") || (N == "6") || (N == "7") || (N == "8") || (N == "9"))
                {
                    TOUCHING.Add(int.Parse(C[i].name));
                    //Debug.Log(TOUCHING[0]);
                }
            }
        }
        List<int> SORTED = TOUCHING.OrderBy((int i) => i).ToList();
        if (SORTED.Count > 0)
        {
            //var TARGET = GameObject.Find(TOUCHING[0].ToString());
            for (var i = 0; i < SORTED.Count; i++)
            {
                if (flying)
                {
                    transform.GetChild(0).gameObject.SetActive(false);
                    //transform.GetChild(1).GetComponent<Animator>().SetFloat("EXPLODE", 1f);
                    GameObject TARGET = GameObject.Find(SORTED[i].ToString());
                    Tscript = TARGET.GetComponent<TARGET>();
                    Tscript.lives -= 0.2f;
                    if (coroutineAllowed)
                    {
                        StartCoroutine(Demolish());
                    }
                }
                flying = false;
            }
        }
    }
    private IEnumerator Demolish()
    {
        coroutineAllowed = false;
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
        coroutineAllowed = true;
    }
}