using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class FOLLOW : MonoBehaviour
{
    public GameObject range;
    public GameObject target;
    public bool canShoot = false;
    public List<int> SORTED;

    private void OnMouseDown()
    {
        if (range != null)
        {
            range.SetActive(!range.activeSelf);
        }
    }
    void Update()
    {
        float X = this.transform.position[0];
        float Y = this.transform.position[1];
        var center = new Vector3(8.5f, 5, 0);
        var screen = new Vector3(15 / 2, 9 / 2, 8 / 2);
        Collider[] C = Physics.OverlapBox(center, screen);
        if (range != null)
        {
            center = new Vector3(X, Y, 0);
            C = Physics.OverlapSphere(center, (range.transform.localScale.x / 2)-0.1f);
        }
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
        SORTED = TOUCHING.OrderBy((int i) => i).ToList();
        //TOUCHING
        if (SORTED.Count > 0)
        {
            target = GameObject.Find(SORTED[0].ToString());
            float DX = target.transform.position[0];
            float DY = target.transform.position[1];
            float AX = this.transform.eulerAngles.x;
            float AY = this.transform.eulerAngles.y;
            float AZ = this.transform.eulerAngles.z;

            float TAN = (Mathf.Abs(X - DX)) / (Mathf.Abs(Y - DY + 0.00000001f));
            float A = Mathf.Rad2Deg * Mathf.Atan(TAN);
            if (A > 0)
            {
                A += 180;
            }
            if (DX < X)
            {
                A *= -1;
            }
            if (DY > Y)
            {
                A = -A + 180;
            }
            if (DX == X)
            {
                A += 180;
            }
            if (range != null && gameObject.tag != "TESLA" && gameObject.tag != "LASER CANNON")
            {
                this.transform.eulerAngles = new Vector3(AX, AY, A);
            }
            canShoot = true;
        }
        else
        {
            canShoot = false;
        }
    }

}
