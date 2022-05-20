using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class ROTATE : MonoBehaviour
{
    public int dire = 1;
    private bool coroutineAllowed = true;
    private TARGET Tscript;
    //public float DZ = 0f;
    void Update()
    {
        Collider[] C = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2);
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
                var TARGET = GameObject.Find(SORTED[i].ToString());

                Vector3 tmp = TARGET.transform.position;
                if (this.transform.parent.eulerAngles.z == 90 || this.transform.parent.eulerAngles.z == 270)
                {
                    tmp.y = Mathf.Round(tmp.y);
                }
                else if (this.transform.parent.eulerAngles.z == 0 || this.transform.parent.eulerAngles.z == 180)
                {
                    tmp.x = Mathf.Round(tmp.x);
                }

                TARGET.transform.position = tmp;
                //TARGET.transform.eulerAngles = new Vector3(0, 0, this.transform.parent.eulerAngles.z + 270);
                float OLD = (int)TARGET.transform.eulerAngles.z;
                float BLOCK1 = (int)this.transform.parent.eulerAngles.z + 270;
                float BLOCK2 = (int)this.transform.parent.eulerAngles.z - 90;
                if (coroutineAllowed)
                {
                    if (OLD != BLOCK1 && OLD != BLOCK2)
                    {
                        StartCoroutine(RotateImage(TARGET, OLD));
                    }
                }
            }
        }
    }
    IEnumerator RotateImage(GameObject target,float old)
    {
        coroutineAllowed = false;
        Tscript = target.GetComponent<TARGET>();
        Tscript.on = 0;

        float dire = this.transform.localPosition.x * 4; 
        for (int i = 0; i <= 90; i++)
        {
            if (target != null)
            {
                target.transform.eulerAngles = new Vector3(0, 0, old + (i * dire));
                yield return new WaitForSeconds(0.001f);
            }
        }


        Tscript.on = 1;
        coroutineAllowed = true;
    }
}
