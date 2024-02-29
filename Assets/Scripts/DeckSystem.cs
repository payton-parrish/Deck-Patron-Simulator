using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;

public class DeckSystem : MonoBehaviour
{
    public GameObject cardGO;
    public Spell[] spells = new Spell[0];
    public GameObject[] deck = new GameObject[0];
    public GameObject[] hand = new GameObject[0];
    public GameObject[] trash = new GameObject[0];
    public GameObject view;
    public GameObject button;
    public void createDeck()
    {
        GameObject.Find("Canvas").transform.Find("Menu").gameObject.SetActive(false);
        GameObject.Find("Canvas").transform.Find("Scroller").gameObject.SetActive(false);
        Array.Resize<GameObject>(ref deck, spells.Length);
        for(int i = 0; i < spells.Length; i++)
        {
            deck[i] = Instantiate(cardGO,GameObject.Find("Deck").transform);
            deck[i].name = spells[i].spellName;
            deck[i].transform.Find("Front").transform.Find("Name Panel").transform.Find("Name").GetComponent<Text>().text = spells[i].spellName;
            deck[i].transform.Find("Front").transform.Find("Name Panel").transform.Find("Level").GetComponent<Text>().text = "" + spells[i].level;
            deck[i].transform.Find("Front").transform.Find("Description Panel").transform.Find("Description").GetComponent<Text>().text = spells[i].components + "\n\n";
            deck[i].transform.Find("Front").transform.Find("Description Panel").transform.Find("Description").GetComponent<Text>().text += spells[i].range + "\t" + spells[i].duration + "\n\n";
            deck[i].transform.Find("Front").transform.Find("Description Panel").transform.Find("Description").GetComponent<Text>().text += spells[i].description.Replace("@", "" + System.Environment.NewLine);
            string path = "";
            if(File.Exists("Assets/Resources/Images/" + spells[i].spellName + ".png"))
            {
                path += "Images/" + spells[i].spellName;
            }
            else
            {
                path += "Images/Question Symbol";
            }
            Texture2D image = Resources.Load<Texture2D>(path);
            deck[i].transform.Find("Front").transform.Find("Image Panel").transform.Find("Image").GetComponent<Image>().sprite = Sprite.Create(image,new Rect(0.0f, 0.0f, image.width, image.height), new Vector2(0.5f, 0.5f), 100.0f);
            switch(spells[i].castingTime)
            {
                case "1 Action":
                    deck[i].transform.Find("Front").transform.Find("Name Panel").GetComponent<Image>().color = new Color(1f/2,0,0,1f);
                    deck[i].transform.Find("Front").transform.Find("Image Panel").GetComponent<Image>().color = new Color(1f/2,0,0,1f);
                    deck[i].transform.Find("Front").transform.Find("Description Panel").GetComponent<Image>().color = new Color(1f*3/4,0,0,1f);
                    break;
                case "1 Bonus Action":
                    deck[i].transform.Find("Front").transform.Find("Name Panel").GetComponent<Image>().color = new Color(0,0,1f/2,1f);
                    deck[i].transform.Find("Front").transform.Find("Image Panel").GetComponent<Image>().color = new Color(0,0,1f/2,1f);
                    deck[i].transform.Find("Front").transform.Find("Description Panel").GetComponent<Image>().color = new Color(0,0,1f*3/4,1f);
                    break;
                case "1 Reaction":
                    deck[i].transform.Find("Front").transform.Find("Name Panel").GetComponent<Image>().color = new Color(0,1f/2,0,1f);
                    deck[i].transform.Find("Front").transform.Find("Image Panel").GetComponent<Image>().color = new Color(0,1f/2,0,1f);
                    deck[i].transform.Find("Front").transform.Find("Description Panel").GetComponent<Image>().color = new Color(0,1f*3/4,0,1f);
                    break;
                default:
                    deck[i].transform.Find("Front").transform.Find("Name Panel").GetComponent<Image>().color = new Color(0,0,0,1f);
                    deck[i].transform.Find("Front").transform.Find("Image Panel").GetComponent<Image>().color = new Color(0,0,0,1f);
                    deck[i].transform.Find("Front").transform.Find("Description Panel").GetComponent<Image>().color = new Color(0,0,0,1f);
                    break;
            }
            switch(spells[i].school)
            {
                case "Conjuration":
                    deck[i].transform.Find("Front").transform.Find("Internal Panel").GetComponent<Image>().color = new Color(1f*3/4,0.9f*3/4,0,0.4f);
                    break;
                case "Abjuration":
                    deck[i].transform.Find("Front").transform.Find("Internal Panel").GetComponent<Image>().color = new Color(0,0,1f*3/4,0.4f);
                    break;
                case "Evocation":
                    deck[i].transform.Find("Front").transform.Find("Internal Panel").GetComponent<Image>().color = new Color(1f*3/4,0,0,0.4f);
                    break;
                case "Necromancy":
                    deck[i].transform.Find("Front").transform.Find("Internal Panel").GetComponent<Image>().color = new Color(0,1f*3/4,0,0.4f);
                    break;
                case "Transmutation":
                    deck[i].transform.Find("Front").transform.Find("Internal Panel").GetComponent<Image>().color = new Color(1f*3/4,0.5f*3/4,0f,1f);
                    break;
                case "Illusion":
                    deck[i].transform.Find("Front").transform.Find("Internal Panel").GetComponent<Image>().color = new Color(1f*3/4,0,1f*3/4,0.4f);
                    break;
                case "Enchantment":
                    deck[i].transform.Find("Front").transform.Find("Internal Panel").GetComponent<Image>().color = new Color(1f*3/4,0.75f*3/4,0.80f*3/4,1f);
                    break;
                case "Divination":
                    deck[i].transform.Find("Front").transform.Find("Internal Panel").GetComponent<Image>().color = new Color(0.5f*3/4,0.5f*3/4,0.8f*3/4,0.4f);
                    break;
                default:
                    deck[i].transform.Find("Front").transform.Find("Internal Panel").GetComponent<Image>().color = new Color(0,0,0,0.4f);
                    break;
            }
            if(spells[i].ritual)
            {
                deck[i].transform.Find("Front").transform.Find("External Panel").GetComponent<Image>().color = new Color(1f*3/4,0.85f*3/4,0f,0.4f);
            }
        }
        Reshuffle();
    }

    public void eraseDeck()
    {
        GameObject.Find("Canvas").transform.Find("Menu").gameObject.SetActive(true);
        GameObject.Find("Canvas").transform.Find("Scroller").gameObject.SetActive(true);
        GameObject.Find("Canvas").transform.Find("X").gameObject.SetActive(false);
        for(int i = 0; i < deck.Length; i++)
        {
            Destroy(deck[i]);
        }
        for(int i = 0; i < hand.Length; i++)
        {
            Destroy(hand[i]);
        }
        for(int i = 0; i < trash.Length; i++)
        {
            Destroy(trash[i]);
        }
        Destroy(view);
        Array.Resize<GameObject>(ref deck, 0);
        Array.Resize<GameObject>(ref hand, 0);
        Array.Resize<GameObject>(ref trash, 0);
        Array.Resize<Spell>(ref spells, 0);
        view = null;
    }
    void reposition()
    {
        for(int i = 0; i < deck.Length; i++)
        {
            deck[i].transform.position = new Vector3(2,0,-(float)0.01*i);
            deck[i].transform.rotation = new Quaternion(0,180,0,0);
        }
        for(int i = 0; i < hand.Length; i++)
        {
            double pos = -1+2*(double)(i+1)/(hand.Length+1);
            hand[i].transform.position = new Vector3((float)pos,-1,-(float)0.01*i);
            hand[i].transform.rotation = new Quaternion(0,0,0,0);
        }
        for(int i = 0; i < trash.Length; i++)
        {
            trash[i].transform.position = new Vector3(-2,0,-(float)0.01*i);
            trash[i].transform.rotation = new Quaternion(0,0,0,0);
        }
        if(view != null)
        {
            view.transform.position = new Vector3(0,0,-0.1f);
            view.transform.rotation = new Quaternion(0,0,0,0);
        }
    }

    public void Reshuffle()
    {
        for (int i = 0; i < deck.Length; i++ )
        {
            GameObject temp = deck[i];
            int rand = UnityEngine.Random.Range(i, deck.Length);
            deck[i] = deck[rand];
            deck[rand] = temp;
        }
    }

    void Update()
    {
        reposition();
        if(GameObject.Find("Destroy Pile").transform.childCount > 0)
        {
            Destroy(GameObject.Find("Destroy Pile").transform.GetChild(0).gameObject);
        }
    }

    public void Draw(string d)
    {
        if(d.Equals("Deck") && deck.Length > 0)
        {
            if(view != null)
            {
                view.transform.localScale -= new Vector3(0.59f,0.86f,0.01f);
                Array.Resize<GameObject>(ref hand, hand.Length+1);
                hand[hand.Length-1] = view;
                hand[hand.Length-1].transform.SetParent(GameObject.Find("Hand").transform);
            }
            view = deck[deck.Length-1];
            view.transform.SetParent(GameObject.Find("View").transform);
            Array.Resize<GameObject>(ref deck, deck.Length-1);
            view.transform.localScale += new Vector3(0.59f,0.86f,0.01f);
        }
        else if(d.Equals("Trash") && trash.Length > 0)
        {
            if(view != null)
            {
                Array.Resize<GameObject>(ref hand, hand.Length+1);
                hand[hand.Length-1] = view;
                hand[hand.Length-1].transform.SetParent(GameObject.Find("Hand").transform);
            }
            view = trash[trash.Length-1];
            view.transform.SetParent(GameObject.Find("View").transform);
            Array.Resize<GameObject>(ref trash, trash.Length-1);
            view.transform.localScale += new Vector3(0.59f,0.86f,0.01f);
        }
    }

    public void IntoTrash(string d)
    {
        if(d.Equals("Deck") && deck.Length > 0)
        {
            Array.Resize<GameObject>(ref trash, trash.Length+1);
            trash[trash.Length-1] = deck[deck.Length-1];
            deck[deck.Length-1].transform.SetParent(GameObject.Find("Trash").transform);
            Array.Resize<GameObject>(ref deck, deck.Length-1);
        }
        else if(d.Equals("View") && view != null)
        {
            view.transform.localScale -= new Vector3(0.59f,0.86f,0.01f);
            Array.Resize<GameObject>(ref trash, trash.Length+1);
            trash[trash.Length-1] = view;
            view.transform.SetParent(GameObject.Find("Trash").transform);
            view = null;
        }
    }

    public void Select(string d)
    {
        if(d.Equals("Deck") && deck.Length > 0)
        {
            GameObject.Find("Canvas").transform.Find("Scroll View").gameObject.SetActive(true);
            GameObject list = GameObject.Find("Canvas/Scroll View/Viewport/Content").gameObject;
            for(int i = 0; i < deck.Length; i++)
            {
                GameObject j = Instantiate(button,list.transform);
                j.name = deck[i].name;
                j.GetComponent<Button>().onClick.AddListener(delegate {IntoView("Deck",j.transform.GetSiblingIndex()); });
                j.GetComponent<Button>().enabled = true;
                j.GetComponent<Image>().enabled = true;
                j.transform.Find("Text (TMP)").GetComponent<TMP_Text>().enabled = true;
                j.transform.Find("Text (TMP)").GetComponent<TMP_Text>().text = deck[i].name;
            }
        }
        else if(d.Equals("Trash") && trash.Length > 0)
        {
            GameObject.Find("Canvas").transform.Find("Scroll View").gameObject.SetActive(true);
            GameObject list = GameObject.Find("Canvas/Scroll View/Viewport/Content").gameObject;
            for(int i = 0; i < trash.Length; i++)
            {
                GameObject j = Instantiate(button,list.transform);
                j.name = trash[i].name;
                j.GetComponent<Button>().onClick.AddListener(delegate {IntoView("Trash",j.transform.GetSiblingIndex()); });
                j.GetComponent<Button>().enabled = true;
                j.GetComponent<Image>().enabled = true;
                j.transform.Find("Text (TMP)").GetComponent<TMP_Text>().enabled = true;
                j.transform.Find("Text (TMP)").GetComponent<TMP_Text>().text = deck[i].name;
            }
        }
    }

    public void IntoHand()
    {
        if(view != null)
        {
            view.transform.localScale -= new Vector3(0.59f,0.86f,0.01f);
            Array.Resize<GameObject>(ref hand, hand.Length+1);
            hand[hand.Length-1] = view;
            view.transform.SetParent(GameObject.Find("Hand").transform);
            view = null;
        }
    }

    public void IntoView(string d, int a)
    {
        if(d.Equals("View") && hand.Length > 0)
        {
            if(view != null)
            {
                view.transform.localScale -= new Vector3(0.59f,0.86f,0.01f);
                Array.Resize<GameObject>(ref hand, hand.Length+1);
                hand[hand.Length-1] = view;
                hand[hand.Length-1].transform.SetParent(GameObject.Find("Hand").transform);
            }
            view = hand[a];
            view.transform.SetParent(GameObject.Find("View").transform);
            for(int i = a+1; i < hand.Length; i++)
            {
                hand[i-1] = hand[i];
            }
            Array.Resize<GameObject>(ref hand, hand.Length-1);
            view.transform.localScale += new Vector3(0.59f,0.86f,0.01f);
        }
        if(d.Equals("Deck") && deck.Length > 0)
        {
            GameObject.Find("Canvas").transform.Find("Scroll View").gameObject.SetActive(false);
            if(view != null)
            {
                view.transform.localScale -= new Vector3(0.59f,0.86f,0.01f);
                Array.Resize<GameObject>(ref hand, hand.Length+1);
                hand[hand.Length-1] = view;
                hand[hand.Length-1].transform.SetParent(GameObject.Find("Hand").transform);
            }
            view = deck[a];
            view.transform.SetParent(GameObject.Find("View").transform);
            for(int i = a+1; i < deck.Length; i++)
            {
                deck[i-1] = deck[i];
            }
            Array.Resize<GameObject>(ref deck, deck.Length-1);
            view.transform.localScale += new Vector3(0.59f,0.86f,0.01f);
            GameObject list = GameObject.Find("Canvas").transform.Find("Scroll View").transform.Find("Viewport").transform.Find("Content").gameObject;
            while(list.transform.childCount > 0)
            {
                list.transform.GetChild(0).SetParent(GameObject.Find("Destroy Pile").transform);
            }
        }
        if(d.Equals("Trash") && trash.Length > 0)
        {
            GameObject.Find("Canvas").transform.Find("Scroll View").gameObject.SetActive(false);
            if(view != null)
            {
                view.transform.localScale -= new Vector3(0.59f,0.86f,0.01f);
                Array.Resize<GameObject>(ref hand, hand.Length+1);
                hand[hand.Length-1] = view;
                hand[hand.Length-1].transform.SetParent(GameObject.Find("Hand").transform);
            }
            view = trash[a];
            view.transform.SetParent(GameObject.Find("View").transform);
            for(int i = a+1; i < trash.Length; i++)
            {
                trash[i-1] = trash[i];
            }
            Array.Resize<GameObject>(ref trash, trash.Length-1);
            view.transform.localScale += new Vector3(0.59f,0.86f,0.01f);
        }
    }

    public void IntoDeck(string d)
    {
        if(d.Equals("Trash") && trash.Length > 0)
        {
            Array.Resize<GameObject>(ref deck, deck.Length+1);
            deck[deck.Length-1] = trash[trash.Length-1];
            trash[trash.Length-1].transform.SetParent(GameObject.Find("Deck").transform);
            Array.Resize<GameObject>(ref trash, trash.Length-1);
        }
        else if(d.Equals("View") && view != null)
        {
            view.transform.localScale -= new Vector3(0.59f,0.86f,0.01f);
            Array.Resize<GameObject>(ref deck, deck.Length+1);
            deck[deck.Length-1] = view;
            view.transform.SetParent(GameObject.Find("Deck").transform);
            view = null;
        }
    }

    public void Empty(string d)
    {
        if(d.Equals("View") && view != null)
        {
            IntoDeck("View");
        }
        else if(d.Equals("Trash") && trash.Length > 0)
        {
            while(trash.Length > 0)
            {
                IntoDeck("Trash");
            }
        }
        else if(d.Equals("Hand") && hand.Length > 0)
        {
            while(hand.Length > 0)
            {
                IntoDeck("Hand");
            }
        }
    }
}