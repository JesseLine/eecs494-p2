using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyFlashController : MonoBehaviour
{
    private Color originalColor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //originalColor = GetComponent<Renderer>().material.color;
        StartCoroutine(Flash());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Flash()
    {
        Debug.Log("Start flashing");
        while (true)
        {
            GetComponent<Renderer>().material.color = (originalColor * 10);
            yield return new WaitForSeconds(1f);
            GetComponent<Renderer>().material.color = originalColor;
            yield return new WaitForSeconds(1f);
        }
    }
}
