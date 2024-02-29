using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoScreenSettings : MonoBehaviour
{
    Button X = null;
    // Start is called before the first frame update
    void Start()
    {
        X = gameObject.transform.Find("X").GetComponent<Button>();
        X.onClick.AddListener(exit);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void exit()
    {
        gameObject.SetActive(false);
    }
}
