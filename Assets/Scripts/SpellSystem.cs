using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;

public class SpellSystem : MonoBehaviour
{
    public Spell[] SpellList = new Spell[0];
    public Card[] listOfCards = new Card[0];
    public Font thisFont = null;
    public GameObject thisSlider = null;
    public GameObject thisButton = null;
    void Start()
    {
        string[] lines = File.ReadAllLines("All Spells List.txt");
        char previous = ' ';
        int level = 0;
        for(int i = 0; i < lines.Length; i++)
        {
            Array.Resize<Spell>(ref SpellList, SpellList.Length+1);
            string[] separate = lines[i].Split("\t");
            if(previous>separate[0][0])
            {
                level++;
            }
            SpellList[i] = new Spell(level,separate[0],separate[1],separate[2],separate[3],separate[4],separate[5],false,false,separate[6]);
            previous = separate[0][0];
        }

        lines = File.ReadAllLines("Extra Spells List.txt");
        for(int i = 0; i < lines.Length; i++)
        {
            Array.Resize<Spell>(ref SpellList, SpellList.Length+1);
            string[] separate = lines[i].Split("\t");
            if(previous>separate[0][0])
            {
                level++;
            }
            SpellList[i] = new Spell(Convert.ToInt32(separate[0]),separate[1],separate[2],separate[3],separate[4],separate[5], separate[6], Convert.ToBoolean(separate[7]),Convert.ToBoolean(separate[8]),separate[9]);
            previous = separate[0][0];
        }

        lines = File.ReadAllLines("Warlock Spells List.txt");
        for(int i = 0; i < lines.Length; i++)
        {
            string[] separate = lines[i].Split("\t");
            for(int j = 0; j < SpellList.Length; j++)
            {
                if(SpellList[j].spellName.Equals(separate[0]))
                {
                    SpellList[j].warlock = true;
                    break;
                }
            }
        }

        lines = File.ReadAllLines("Ritual Spells List.txt");
        for(int i = 0; i < lines.Length; i++)
        {
            string[] separate = lines[i].Split("\t");
            for(int j = 0; j < SpellList.Length; j++)
            {
                if(SpellList[j].spellName.Equals(separate[0]))
                {
                    SpellList[j].ritual = true;
                    break;
                }
            }
        }

        Player player = new Player(1,10);
        Array.Resize<Card>(ref listOfCards, SpellList.Length);
        GameObject levelTitle = new GameObject();
        levelTitle.transform.parent = GameObject.Find("Canvas").transform.Find("Menu").transform;
        levelTitle.transform.SetPositionAndRotation(new Vector3(150,325,0),new Quaternion(0,0,0,0));
        levelTitle.name = "Cantrip";
        levelTitle.AddComponent<Text>().text = levelTitle.name + ":";
        levelTitle.GetComponent<Text>().font = thisFont;
        levelTitle.GetComponent<Text>().fontSize = 42;
        levelTitle.GetComponent<Text>().fontStyle = FontStyle.Bold;
        levelTitle.GetComponent<Text>().horizontalOverflow = HorizontalWrapMode.Overflow;
        level = 0;
        int cols = 0;
        int rows = 0;
        for(int i = 0; i < SpellList.Length; i++)
        {
            if(level != SpellList[i].level)
            {
                cols = 0;
                if(rows%3 != 0)
                {
                    rows += 3;
                    rows -= rows%3;
                }
                rows += 3;
                level = SpellList[i].level;
                levelTitle = new GameObject();
                levelTitle.transform.parent = GameObject.Find("Canvas").transform.Find("Menu").transform;
                levelTitle.transform.SetPositionAndRotation(new Vector3(150,325,0),new Quaternion(0,0,0,0));
                levelTitle.transform.Translate(0,-50*(rows/3),0);
                levelTitle.name = "Level " + level;
                levelTitle.AddComponent<Text>().text = levelTitle.name + ":";
                levelTitle.GetComponent<Text>().font = thisFont;
                levelTitle.GetComponent<Text>().fontSize = 42;
                levelTitle.GetComponent<Text>().fontStyle = FontStyle.Bold;
                levelTitle.GetComponent<Text>().horizontalOverflow = HorizontalWrapMode.Overflow;
            }
            listOfCards[i] = new Card(SpellList[i],false,0,300*(-1+cols%3),50-50*(rows/3), player, i);
            cols++;
            rows++;
        }
        Destroy(GameObject.Find("Display"));

        GameObject scroller = thisSlider;
        scroller.transform.SetParent(GameObject.Find("Canvas").transform);
        scroller.name = "Scroller";
        scroller.GetComponent<Slider>().value = 0;
        scroller.GetComponent<Slider>().minValue = 0;
        scroller.GetComponent<Slider>().maxValue = 50*(rows/3)-260;
        scroller.GetComponent<Slider>().onValueChanged.AddListener(delegate {Scroll(); });
        scroller.transform.SetSiblingIndex(0);
        GameObject.Find("Canvas").transform.Find("Image").SetSiblingIndex(0);
        
        GameObject create = thisButton;
        thisButton.GetComponent<Button>().onClick.AddListener(delegate {createDeck(listOfCards); });
    }

    public Card[] getList()
    {
        return listOfCards;
    }

    void Scroll()
    {
        GameObject.Find("Canvas").transform.Find("Menu").transform.localPosition = new Vector3(0,thisSlider.GetComponent<Slider>().value,0);
    }

    void createDeck(Card[] cards)
    {
        int x = 0;
        Spell[] deck = GameObject.Find("System").GetComponent<DeckSystem>().spells;
        for (int i = 0; i < listOfCards.Length; i++)
        {
            for(int j = 0; j < listOfCards[i].copies; j++)
            {
                Array.Resize<Spell>(ref GameObject.Find("System").GetComponent<DeckSystem>().spells, GameObject.Find("System").GetComponent<DeckSystem>().spells.Length+1);
                GameObject.Find("System").GetComponent<DeckSystem>().spells[x++] = listOfCards[i].spell;
            }
        }
        GameObject.Find("System").GetComponent<DeckSystem>().createDeck();
    }
}
