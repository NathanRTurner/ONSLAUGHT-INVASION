using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DRONE : MonoBehaviour
{
    public FOLLOW follow;
    [SerializeField] private Transform bullet;
    private bool coroutineAllowed = true;
    private bool coroutine2Allowed = true;
    private void Start()
    {
        follow = this.GetComponent<FOLLOW>();
    }
    private void Update()
    {
        GameObject target = follow.target;
        if (target != null)
        {

            Vector3 desPos = target.transform.position + target.transform.right * 1.5f;
            float offset = 0.1f;
            if ((desPos.x - offset < this.transform.position.x) && (this.transform.position.x < desPos.x + offset) && (desPos.y - offset < this.transform.position.y) && (this.transform.position.y < desPos.y + offset))
            {
                this.transform.position = target.transform.position + target.transform.right * 1.5f;
            }
            else if (coroutine2Allowed)
            {
                StartCoroutine(Move(desPos));
            }
        }
        else if (coroutine2Allowed)
        {
            StartCoroutine(Move(this.transform.position));
        }
        if (coroutineAllowed && follow.canShoot)
        {
            StartCoroutine(Shoot());
        }
        if (this.transform.position == this.transform.parent.position)
        {
            this.GetComponent<Animator>().SetFloat("FLYING", 0f);
        }
        else
        {
            this.GetComponent<Animator>().SetFloat("FLYING", 1f);
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
                Vector3 startPos = new Vector3(this.transform.position.x, this.transform.position.y, -0.25f);
                Vector3 targPos = new Vector3(target.transform.position.x, target.transform.position.y, -0.25f);
                Transform ibullet = Instantiate(bullet, startPos, Quaternion.identity);
                Vector3 shootDir = targPos - startPos;
                ibullet.GetComponent<BULLET>().Setup(shootDir, target);
                yield return new WaitForSeconds(0.3f);
            }
        }
        coroutineAllowed = true;
    }
    private IEnumerator Move(Vector3 targPos)
    {
        coroutine2Allowed = false;
        GameObject target = follow.target;
        float speed = 1f;
        if (target == null)
        {
            this.transform.GetChild(2).GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 1);
            speed = 3.5f;
            targPos = this.transform.parent.position;
            float DX = this.transform.parent.position[0];
            float DY = this.transform.parent.position[1];
            float X = this.transform.position[0];
            float Y = this.transform.position[1];
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
            this.transform.eulerAngles = new Vector3(AX, AY, A);
        }
        else
        {
            this.transform.GetChild(2).GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
        }
        
        this.transform.position = Vector3.MoveTowards(this.transform.position, targPos, 0.1f/speed);
        yield return new WaitForSeconds(0.01f);
        coroutine2Allowed = true;
    }
}
