using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemy : MonoBehaviour
{
    //public int Daño;
    
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("PJ"))
        {
            if (coll.GetComponent<PlayerController>().barra.value > 0)
            {
                coll.GetComponent<PlayerController>().animator.SetBool("damage0", true);
                //coll.GetComponent<PlayerController>().damage_ = true;

                //if (transform.position.x > coll.transform.position.x)
                //{
                //    coll.GetComponent<PlayerController>().empuje = -3;
                //    //coll.transform.rotation = Quaternion.Euler(0, 0, 0);
                //}
                //else
                //{
                //    coll.GetComponent<PlayerController>().empuje = 3;
                //    //coll.transform.rotation = Quaternion.Euler(0, 180, 0);
                //}
                //float restaSize = 0.3f;
                //coll.GetComponent<PlayerController>().barra.size = Mathf.Clamp(coll.GetComponent<PlayerController>().barra.size - restaSize, 0f, 1f);

            }
        }
    }
}
