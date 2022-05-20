using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DESTROY : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name != this.name && collision.gameObject.name != "DRONE")
        {
            Destroy(collision.gameObject);
        }
    }
}
