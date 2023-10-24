using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class SpellSystem : MonoBehaviour
{
    public static Spell[] SpellList = new Spell[0];
    public static Card[] listOfCards = new Card[0];
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
                    SpellList[j].ritual = true;
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

        Player player = new Player(12,10);
        Array.Resize<Card>(ref listOfCards, SpellList.Length+1);
        for(int i = 0; i < SpellList.Length; i++)
        {
            listOfCards[i] = new Card(SpellList[i],false,0,300*(-1+i%3),100-50*(i/3), player);
        }
        Destroy(GameObject.Find("Display"));
    }
}
