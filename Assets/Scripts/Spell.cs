using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct Spell
{
    public int level { get; set; }
    public string spellName { get; set; }
    public string school { get; set; }
    public string castingTime { get; set; }
    public string range { get; set; }
    public string duration { get; set; }
    public string components { get; set; }
    public bool ritual { get; set; }
    public bool warlock { get; set; }
    public string description { get; set; }

    public Spell(int l, string sn, string s, string ct, string r, string d, string c, bool ri, bool w, string de)
    {
        level = l;
        spellName = sn;
        school = s;
        castingTime = ct;
        range = r;
        duration = d;
        components = c;
        ritual = ri;
        warlock = w;
        description = de;
    }

    public override string ToString()
   {
      return spellName;
   }
}
