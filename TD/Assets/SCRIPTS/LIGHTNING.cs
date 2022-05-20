using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LIGHTNING : MonoBehaviour
{
    private Vector3 shootDir;
    private GameObject target;
    private TARGET Tscript;
    private int count;
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
    public void Setup(Vector3 shootDir, GameObject target,int count)
    {
        this.count = count;
        this.shootDir = shootDir;
        this.target = target;
        transform.eulerAngles = new Vector3(0, 0, GetAngle(shootDir) - 90);
        Destroy(gameObject, 0.15f);
    }
    void Update()
    {
        Collider[] C = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2);
        List<int> TOUCHING = new List<int>();
        TOUCHING.Clear();
        if (C.Length > 0)
        {
            for (var i = 0; i < C.Length; i++)
            {
                string N = C[i].name[0].ToString();
                if ((N == "0") || (N == "1") || (N == "2") || (N == "3") || (N == "4") || (N == "5") || (N == "6") || (N == "7") || (N == "8") || (N == "9"))
                {
                    TOUCHING.Add(int.Parse(C[i].name));
                }
            }
        }
        List<int> SORTED = TOUCHING.OrderBy((int i) => i).ToList();
        if (SORTED.Count > 0)
        {
            for (var i = 0; i < SORTED.Count; i++)
            {
                GameObject TARGET = GameObject.Find(SORTED[i].ToString());
                
                Tscript = TARGET.GetComponent<TARGET>();
                Tscript.lives -= 0.05f/count;
                //Destroy(gameObject);
            }
        }
    }
}