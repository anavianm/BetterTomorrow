using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{

    public List<Item> items = new List<Item>();

    private void Awake() 
    {
        BuildDatabase();
    }

    public Item getRandomItem()
    {
        int numkeys = items.Count;
        System.Random randomInt = new System.Random();
        int randomObjectID = randomInt.Next(0, numkeys); //for ints

        return getItem(randomObjectID); 
    }

    public Item getItem(int id) 
    {
        return items.Find(item => item.id == id);
    }

     public Item getItem(string itemName) 
    {
        return items.Find(item => item.title == itemName);
    }

    void BuildDatabase() {
        items = new List<Item>() {
            new Item(0, "Magnifying Glass", "Increases Enemy Warning by 5%", 
            new Dictionary<string, double>
            {
                {"Warning", 0.05}
            }),

            new Item(0, "Friendship bracelet", "Hurts anxiety monster's ability to slow you down by 5%", 
            new Dictionary<string, double>{
                {"reverseSlow", 0.05}
            }),

            new Item(0, "Food", "Increases base HP by 5%", 
            new Dictionary<string, double>{
                {"HP", 0.05}
            }),

            new Item(0, "Water", "Heals 5% over time", 
            new Dictionary<string, double>{
                {"Heal", 0.05}
            }),

            new Item(0, "Friendship bracelet", "Hurts anxiety monster's ability to slow you down by 5%", 
            new Dictionary<string, double>{
                {"reverseSlow", 0.05}
            })
        };
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
