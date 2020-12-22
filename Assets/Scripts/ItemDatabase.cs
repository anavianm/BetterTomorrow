using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{

    public List<Item> items = new List<Item>();

    private void Awake() 
    {
        Debug.Log("building");
        BuildDatabase();
    }

    void Start()
    {
        // getRandomItem();
    }

    public Item getRandomItem()
    {
        Debug.Log("here");
        int numkeys = items.Count;
        System.Random randomInt = new System.Random();
        int randomObjectID = randomInt.Next(0, numkeys); //for ints

        return getItem(randomObjectID); 
    }

    public Item getItem(int id) 
    {
        Debug.Log(id);
        return items.Find(item => item.id == id);
    }

    public Item getItem(string itemName) 
    {
        return items.Find(item => item.title == itemName);
    }

    void BuildDatabase() {
        items = new List<Item>() {
            new Item(0, "Magnifying Glass", "Decreases Shot Cooldown", 
            new Dictionary<string, double>
            {
                {"Cooldown", 0.95}
            }),

            new Item(1, "Friendship bracelet", "Hurts anxiety monster's ability to slow you down by 5%", 
            new Dictionary<string, double>{
                {"Jump", 0.05}
            }),

            new Item(2, "Food", "Increases base HP by 5%", 
            new Dictionary<string, double>{
                {"HP", 0.05}
            }),

            new Item(3, "Water", "Heals 5% over time", 
            new Dictionary<string, double>{
                {"Heal", 0.05}
            }),

            new Item(4, "Medicine", "+50HP", 
            new Dictionary<string, double>{
                {"Health", 50}
            }),

            new Item(5, "Cigarettes", "Takes 50% of HP but gives more attack", 
            new Dictionary<string, double>{
                {"HP", 0.5},
                {"Attack", 0.05}
            }),

            new Item(6, "Inhaler", "Speed Increase", 
            new Dictionary<string, double>{
                {"Speed", 0.05}
            }),

        };
    }
    // Start is called before the first frame update
  

    // Update is called once per frame
    void Update()
    {
        
    }
}
