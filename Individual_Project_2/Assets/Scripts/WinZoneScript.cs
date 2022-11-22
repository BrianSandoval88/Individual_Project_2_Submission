using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinZoneScript : MonoBehaviour
{
    public TMP_Text winText;

    public GameObject nextBtn;

    public void Start()
    {
        nextBtn.SetActive(false);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.GetComponent<Rigidbody>().CompareTag("Player"))
        {
            winText = GameObject.FindWithTag("WinText").GetComponent<TMP_Text>();
            winText.text = "You Win!!";

            nextBtn.SetActive(true);
        }
    }
}
