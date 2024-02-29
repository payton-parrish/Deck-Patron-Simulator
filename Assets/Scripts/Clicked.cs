using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;

public class Clicked : MonoBehaviour
{
    bool active;

    void Start()
    {
        if(gameObject.name.Equals("Deck"))
        {
            gameObject.transform.Find("Buttons").transform.Find("Shuffle").GetComponent<Button>().onClick.AddListener(Shuffle);
            gameObject.transform.Find("Buttons").transform.Find("Draw").GetComponent<Button>().onClick.AddListener(delegate {Draw(gameObject.name); });
            gameObject.transform.Find("Buttons").transform.Find("Trash Card").GetComponent<Button>().onClick.AddListener(delegate {TrashCard(gameObject.name); });
            gameObject.transform.Find("Buttons").transform.Find("Select Card").GetComponent<Button>().onClick.AddListener(delegate {SelectCard(gameObject.name); });
        }
        else if(gameObject.name.Equals("View"))
        {
            gameObject.transform.Find("Buttons").transform.Find("Place in Hand").GetComponent<Button>().onClick.AddListener(PlaceInHand);
            gameObject.transform.Find("Buttons").transform.Find("Place in Deck").GetComponent<Button>().onClick.AddListener(delegate {PlaceInDeck(gameObject.name); });
            gameObject.transform.Find("Buttons").transform.Find("Place in Trash").GetComponent<Button>().onClick.AddListener(delegate {TrashCard(gameObject.name); });
            gameObject.transform.Find("Buttons").transform.Find("Description").GetComponent<Button>().onClick.AddListener(Description);
        }
        else if(gameObject.name.Equals("Trash"))
        {
            gameObject.transform.Find("Buttons").transform.Find("Draw").GetComponent<Button>().onClick.AddListener(delegate {Draw(gameObject.name); });
            gameObject.transform.Find("Buttons").transform.Find("Empty").GetComponent<Button>().onClick.AddListener(delegate {Empty(gameObject.name); });
            gameObject.transform.Find("Buttons").transform.Find("Select Card").GetComponent<Button>().onClick.AddListener(delegate {SelectCard(gameObject.name); });
        }
        GameObject.Find("Canvas").transform.Find("X").GetComponent<Button>().onClick.AddListener(Back);
        GameObject.Find("Canvas").transform.Find("X").SetSiblingIndex(0);
        GameObject.Find("Canvas").transform.Find("X").gameObject.SetActive(false);
    }
    void OnMouseDown()
    {
        if(gameObject.name.Equals("Deck") && active && gameObject.transform.childCount > 1)
        {
            GameObject.Find("Deck").transform.GetChild(0).gameObject.SetActive(true);
            GameObject.Find("View").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("Trash").transform.GetChild(0).gameObject.SetActive(false);
        }
        else if(gameObject.name.Equals("View") && active && gameObject.transform.childCount > 1)
        {
            GameObject.Find("Deck").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("View").transform.GetChild(0).gameObject.SetActive(true);
            GameObject.Find("Trash").transform.GetChild(0).gameObject.SetActive(false);
        }
        else if(gameObject.name.Equals("Trash") && active && gameObject.transform.childCount > 1)
        {
            GameObject.Find("Deck").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("View").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("Trash").transform.GetChild(0).gameObject.SetActive(true);
        }
        else if(gameObject.transform.parent != null)
        {
            if(gameObject.transform.parent.name.Equals("Hand") && active && gameObject.transform.childCount > 1)
            {
                GameObject.Find("System").GetComponent<DeckSystem>().IntoView("View",gameObject.transform.GetSiblingIndex());
            }
        }
    }

    void Update()
    {
        active = !GameObject.Find("Canvas").transform.Find("Menu").gameObject.activeSelf;
        if(active)
        {
            GameObject.Find("Canvas").transform.Find("X").gameObject.SetActive(true);
            GameObject.Find("Canvas").transform.Find("Image").gameObject.SetActive(false);
        }
        else
        {
            GameObject.Find("Canvas").transform.Find("X").gameObject.SetActive(false);
            GameObject.Find("Canvas").transform.Find("Scroll View").gameObject.SetActive(false);
            GameObject.Find("Deck").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("View").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("Trash").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("Canvas").transform.Find("Image").gameObject.SetActive(true);
        }
    }

    void Draw(string d)
    {
        GameObject.Find("System").GetComponent<DeckSystem>().Draw(d);
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    void Shuffle()
    {
        GameObject.Find("System").GetComponent<DeckSystem>().Reshuffle();
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
    void TrashCard(string d)
    {
        GameObject.Find("System").GetComponent<DeckSystem>().IntoTrash(d);
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    void SelectCard(string d)
    {
        GameObject.Find("System").GetComponent<DeckSystem>().Select(d);
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    void PlaceInHand()
    {
        GameObject.Find("System").GetComponent<DeckSystem>().IntoHand();
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    void PlaceInDeck(string d)
    {
        GameObject.Find("System").GetComponent<DeckSystem>().IntoDeck(d);
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    void Description()
    {
        GameObject description = GameObject.Find("Canvas").transform.Find("Info Screen").gameObject;
        Spell spell = GameObject.Find("System").GetComponent<DeckSystem>().spells[Array.FindIndex(GameObject.Find("System").GetComponent<DeckSystem>().spells, element => element.spellName == GameObject.Find("System").GetComponent<DeckSystem>().view.name)];
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
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        description.SetActive(true);
    }

    void Empty(string d)
    {
        GameObject.Find("System").GetComponent<DeckSystem>().Empty(d);
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
    void Back()
    {
        GameObject.Find("System").GetComponent<DeckSystem>().eraseDeck();
    }
}
