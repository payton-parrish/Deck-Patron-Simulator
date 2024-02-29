using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;

public struct Card
{
    public Spell spell { get; set; }
    public bool learned { get; set; }
    public int copies { get; set; }
    public GameObject display { get; set; }
    public GameObject description { get; set; }
    public float xPos { get; set; }
    public float yPos { get; set; }
    public Player player { get; set; }
    public int arrayPosition { get; set; }
    public Card(Spell s, bool l, int c, float x, float y, Player p, int i)
    {
        spell = s;
        learned = l;
        copies = c;
        xPos = x;
        yPos = y;
        player = p;
        arrayPosition = i;
        description = GameObject.Find("Canvas").transform.Find("Info Screen").gameObject;
        display = GameObject.Instantiate(GameObject.Find("Display"),GameObject.Find("Canvas").transform.Find("Menu").transform);
        display.transform.Translate(xPos,yPos,0);
        display.transform.Find("Toggle").transform.Find("Name").GetComponent<Text>().text = spell.spellName;
        display.transform.Find("Toggle").transform.Find("Copies").GetComponent<Text>().text = "" + copies;
        display.name = spell.spellName;
        display.transform.Find("Increment").GetComponent<Button>().onClick.AddListener(Increment);
        display.transform.Find("Decrement").GetComponent<Button>().onClick.AddListener(Decrement);
        display.transform.Find("ShowDescription").GetComponent<Button>().onClick.AddListener(ShowDescription);
        display.transform.Find("Toggle").GetComponent<Toggle>().isOn = learned;
        display.transform.Find("Toggle").GetComponent<Toggle>().onValueChanged.AddListener(TF);
    }

    void Increment()
    {
        copies = GameObject.Find("System").GetComponent<SpellSystem>().listOfCards[arrayPosition].copies + 1;
        updateList();
    }

    void Decrement()
    {
        if((!learned && GameObject.Find("System").GetComponent<SpellSystem>().listOfCards[arrayPosition].copies > 0) || (learned && GameObject.Find("System").GetComponent<SpellSystem>().listOfCards[arrayPosition].copies > player.slots))
        {
            copies = GameObject.Find("System").GetComponent<SpellSystem>().listOfCards[arrayPosition].copies - 1;
            updateList();
        }
    }

    void TF(bool t)
    {
        learned = t;
        if(!this.learned)
        {
            if(display.transform)
            copies = 0;
        }
        else
        {
            copies = player.slots;
        }
        display.transform.Find("Toggle").transform.Find("Copies").GetComponent<Text>().text = "" + copies;
        updateList();
    }

    void updateList()
    {
        GameObject.Find("System").GetComponent<SpellSystem>().listOfCards[arrayPosition] = this;
        display.transform.Find("Toggle").transform.Find("Copies").GetComponent<Text>().text = "" + GameObject.Find("System").GetComponent<SpellSystem>().listOfCards[arrayPosition].copies;
    }

    public void ShowDescription()
    {
        description.SetActive(true);
        description.transform.Find("Name Panel").transform.Find("Name").GetComponent<Text>().text = spell.spellName;
        description.transform.Find("Name Panel").transform.Find("Level").GetComponent<Text>().text = "" + spell.level;
        if(spell.level == 0)
        {
            description.transform.Find("Name Panel").transform.Find("Level").GetComponent<Text>().text = "Cantrip";
        }
        description.transform.Find("Description Panel").transform.Find("Description").GetComponent<Text>().text = "";
        description.transform.Find("Description Panel").transform.Find("Description").GetComponent<Text>().text += spell.components + "\n\n";
        description.transform.Find("Description Panel").transform.Find("Description").GetComponent<Text>().text += "School: " + spell.school + "\n";
        description.transform.Find("Description Panel").transform.Find("Description").GetComponent<Text>().text += "Casting Time: " + spell.castingTime;
        if(spell.ritual)
        {
            description.transform.Find("Description Panel").transform.Find("Description").GetComponent<Text>().text += " (R)";
        }
        description.transform.Find("Description Panel").transform.Find("Description").GetComponent<Text>().text += "\nRange: " + spell.range + "\n";
        description.transform.Find("Description Panel").transform.Find("Description").GetComponent<Text>().text += "Duration: " + spell.duration + "\n\n";
        description.transform.Find("Description Panel").transform.Find("Description").GetComponent<Text>().text += spell.description.Replace("@", "" + System.Environment.NewLine) + "\n";
        string path = "";
        if(File.Exists("Assets/Resources/Images/" + spell.spellName + ".png"))
        {
            path += "Images/" + spell.spellName;
        }
        else
        {
            path += "Images/Question Symbol";
        }
        Texture2D image = Resources.Load<Texture2D>(path);
        description.transform.Find("Image Panel").transform.Find("Image").GetComponent<Image>().sprite = Sprite.Create(image,new Rect(0.0f, 0.0f, image.width, image.height), new Vector2(0.5f, 0.5f), 100.0f);
    }
}