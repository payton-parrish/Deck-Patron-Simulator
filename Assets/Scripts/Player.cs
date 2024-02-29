using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public struct Player
{
    public static int level { get; set; }
    public static int charismaMod { get; set; }
    public int slots { get; set; }
    public Transform playerBar { get; set; }

    public Player(int lvl,int cha)
    {
        level = 1;
        if(level < 1)
        {
            level = 1;
        }
        else if(level > 20)
        {
            level = 20;
        }
        charismaMod = cha;
        switch(level)
        {
            case int n when (n >= 2 && n < 11):
                slots = 2;
                break;
            case int n when (n >= 11 && n < 17):
                slots = 3;
                break;
            case int n when (n >= 17):
                slots = 4;
                break;
            default:
                slots = 1;
                break;
        }
        playerBar = GameObject.Find("Canvas").transform.Find("Menu").transform.Find("Player");
        playerBar.transform.Find("Player Level").transform.Find("Dropdown").GetComponent<TMP_Dropdown>().onValueChanged.AddListener(changeLevel);
        playerBar.transform.Find("Charisma").transform.Find("Increment").GetComponent<Button>().onClick.AddListener(Increment);
        playerBar.transform.Find("Charisma").transform.Find("Decrement").GetComponent<Button>().onClick.AddListener(Decrement);
        playerBar.transform.Find("Player Level").transform.Find("Dropdown").GetComponent<TMP_Dropdown>().value = level-1;
        playerBar.transform.Find("Charisma").GetComponent<Text>().text = "Charisma Modifier: " + charismaMod;
        playerBar.transform.Find("Spell Slots").GetComponent<Text>().text = "Spell Slots: " + slots;
    }

    void changeLevel(int l)
    {
        level = l;
        switch(level)
        {
            case int n when (n >= 2 && n < 11):
                slots = 2;
                break;
            case int n when (n >= 11 && n < 17):
                slots = 3;
                break;
            case int n when (n >= 17):
                slots = 4;
                break;
            default:
                slots = 1;
                break;
        }
        playerBar.transform.Find("Spell Slots").GetComponent<Text>().text = "Spell Slots: " + slots;
        /*if(level >= 11)
        {
            int slotLevel = 6;
            switch(level)
            {
                case int n when (n >= 11 && n < 13):
                    slotLevel = 6;
                    break;
                case int n when (n >= 13 && n < 15):
                    slotLevel = 7;
                    break;
                case int n when (n >= 15 && n < 17):
                    slotLevel = 8;
                    break;
                case int n when (n >= 17):
                    slotLevel = 9;
                    break;
                default:
                    slotLevel = 6;
                    break;
            }
            mysticArcanum(slotLevel);
        }*/
    }

    void Increment()
    {
        charismaMod++;
        playerBar.transform.Find("Charisma").GetComponent<Text>().text = "Charisma Modifier: " + charismaMod;
    }

    void Decrement()
    {
        charismaMod--;
        playerBar.transform.Find("Charisma").GetComponent<Text>().text = "Charisma Modifier: " + charismaMod;
    }

    /*void mysticArcanum(int slotLevel)
    {
        foreach (Card i in GameObject.Find("System").GetComponent<SpellSystem>().getList())
        {
            if(!i.learned)
            {
                if(i.spell.level <= slotLevel)
                {
                    i.forceSetCopies(1);
                }
                else
                {
                    i.forceSetCopies(0);
                }
            }
        }
    }*/
}
