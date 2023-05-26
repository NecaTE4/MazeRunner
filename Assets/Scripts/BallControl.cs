using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    public UnityEngine.UI.Button buton;
    public UnityEngine.UI.Text can, time, durum;
    float speed = 1.75f;
    float ZamanSayaci = 31;
    int CanSayaci = 5;
    bool oyundurum = true;
    bool oyunSonu = false;
    private Rigidbody rgbdy;
    // Start is called before the first frame update
    void Start()
    {
        can.text = CanSayaci + "";
        time.text = "Timer: " + ZamanSayaci;
        rgbdy = GetComponent<Rigidbody> (); 
    }

    // Update is called once per frame
    void Update()
    {
        if (oyundurum && !oyunSonu)
        {
            ZamanSayaci -= Time.deltaTime;
            time.text = "Timer: " + (int)ZamanSayaci;
        }
    
        if (ZamanSayaci <= 0)
        {
                oyundurum = false;
            durum.text = "GAME OVER";
            buton.gameObject.SetActive(true);
        }
        
    }
    private void FixedUpdate()
    {
        if (oyundurum && !oyunSonu) 
        {
            float yatay = Input.GetAxis("Horizontal");
            float dikey = Input.GetAxis("Vertical");
            Vector3 kuvvet = new Vector3(-dikey, 0, yatay);
            rgbdy.AddForce(kuvvet * speed);
        }
        else
        {
            rgbdy.velocity = Vector3.zero;
            rgbdy.angularVelocity = Vector3.zero;
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Equals("Finish"))
        {
            oyunSonu = true;
            durum.text = "WELL DONE";
            buton.gameObject.SetActive(true);
        }
        else if(!collision.gameObject.name.Equals("UpperPlane") && !collision.gameObject.name.Equals("LowerPlane"))
        {
            CanSayaci -= 1; // --------------BELLÝ BÝ SÜRE ÇARPMAYINCA EKSTRA CAN EKLE SONRA-------------------
            can.text = "" + CanSayaci;
            if(CanSayaci == 0)
            {
                oyundurum = false;
                durum.text = "GAME OVER";
                buton.gameObject.SetActive(true);
            }
        }
    }
 
}
