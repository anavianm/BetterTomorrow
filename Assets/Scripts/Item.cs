using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int id;
    public string title;
    public string description;
    public Dictionary<string, double> stats = new Dictionary<string, double>();
    

    public Item(int id, string title, string description, Dictionary<string,double> stats) {
        this.id = id;
        this.title = title;
        this.description = description;
        this.stats = stats;
    }
    public Item(Item item) {
        this.id = id;
        this.title = title;
        this.description = description;
        this.stats = stats;
    }
}
