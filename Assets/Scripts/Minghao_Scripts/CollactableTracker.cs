using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollactableTracker : MonoBehaviour
{
    public List<int> collection = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddItem(Collactable item)
    {
        collection.Add(item.ItemID);
    }
}
