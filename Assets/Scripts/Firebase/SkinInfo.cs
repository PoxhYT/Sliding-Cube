using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkinInfo
{
    public string skinname;
    public int price;
    public bool bought;

    public SkinInfo(string skinname, int price, bool bought)
    {
        this.skinname = skinname;
        this.price = price;
        this.bought = bought;
    }
}
