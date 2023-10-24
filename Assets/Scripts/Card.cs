using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public struct Card
{
    public Spell spell { get; set; }
    public static bool learned { get; set; }
    public static int copies { get; set; }
    public GameObject display { get; set; }
    public float xPos { get; set; }
    public float yPos { get; set; }
    public Player player { get; set; }
    public Card(Spell s, bool l, int c, float x, float y, Player p)
    {
        spell = s;
        learned = l;
        copies = c;
        xPos = x;
        yPos = y;
        player = p;
        display = GameObject.Instantiate(GameObject.Find("Display"),GameObject.Find("Canvas").transform);
        display.transform.Translate(xPos,yPos,0);
        display.transform.Find("Toggle").transform.Find("Name").GetComponent<Text>().text = spell.spellName;
        display.name = spell.spellName;
        display.transform.Find("Increment").GetComponent<Button>().onClick.AddListener(Increment);
        display.transform.Find("Decrement").GetComponent<Button>().onClick.AddListener(Decrement);
        display.transform.Find("Toggle").GetComponent<Toggle>().isOn = learned;
        display.transform.Find("Toggle").GetComponent<Toggle>().onValueChanged.AddListener(TF);
    }

    void Increment()
    {
        copies++;
        display.transform.Find("Toggle").transform.Find("Copies").GetComponent<Text>().text = "" + copies;
    }

    void Decrement()
    {
        if(!learned && copies > 0)
        {
            copies--;
            display.transform.Find("Toggle").transform.Find("Copies").GetComponent<Text>().text = "" + copies;
        }
        else if(learned && copies > player.slots)
        {
            copies--;
            display.transform.Find("Toggle").transform.Find("Copies").GetComponent<Text>().text = "" + copies;
        }
    }

    void TF(bool t)
    {
        learned = t;
        if(!learned)
        {
            copies = 0;
        }
        else
        {
            copies = player.slots;
        }
        display.transform.Find("Toggle").transform.Find("Copies").GetComponent<Text>().text = "" + copies;

    }
}