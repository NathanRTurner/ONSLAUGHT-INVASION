using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MINE : MonoBehaviour
{
    private bool coroutineAllowed = true;
    private bool alive = true;
    private TARGET Tscript;
    void Update()
    {
        Collider[] C = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 4);
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
                if (alive)
                {
                    alive = false;
                    transform.GetChild(0).gameObject.SetActive(false);
                    transform.GetChild(1).gameObject.SetActive(true);
                    transform.GetChild(1).GetComponent<Animator>().SetFloat("EXPLODE", 1f);
                    GameObject TARGET = GameObject.Find(SORTED[i].ToString());
                    Tscript = TARGET.GetComponent<TARGET>();
                    Tscript.lives -= 2f;
                    if (coroutineAllowed)
                    {
                        StartCoroutine(Demolish());
                    }
                }
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
