using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject[] itemsintstances;
    public Sprite defaultimage;
    public GameObject CellsHolder;
    public GameObject ItemsHolder;
    List<SpecificItem> realitems;
    List<Image> images;
    int maxstack = 3;
    public int maxcells = 3;
    int selected;
    Item[] items;
    private void Start()
    {
        items = new Item[maxcells];
        realitems = new List<SpecificItem>();
        images = new List<Image>();
        for (int i = 0; i < CellsHolder.transform.childCount; i++)
        {
            images.Add(CellsHolder.transform.GetChild(i).GetComponent<Image>());
        }
        for (int i = 0; i < ItemsHolder.transform.childCount; i++)
        {
            realitems.Add(ItemsHolder.transform.GetChild(i).GetComponent<SpecificItem>());
        }
    }
    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            selected--;
            if (selected < 0)
                selected = maxcells-1;
            UpdateSelector();
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            selected++;
            if (selected > maxcells - 1)
                selected = 0;
            UpdateSelector();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            int index = RemoveItem(selected).id - 1;
            if (index < 0)
                return;
            GameObject obj = Instantiate(itemsintstances[index], transform.parent.position, Quaternion.identity);
            obj.GetComponent<Rigidbody>().AddForce(transform.parent.forward*3f,ForceMode.Impulse);
        }
    }
    public bool AddItem(Item item)
    {
        if (item.Equals(default(Item)))
            return false;
        for (int i = 0; i < maxcells; i++)
        {
            if (items[i].Equals(default(Item)))
            {
                items[i] = item;
                UpdateSelector();
                UpdateImages();
                return true;
            }
        }
        return false;
    }
    void UpdateImages()
    {
        for (int i = 0; i < maxcells; i++)
        {
            images[i].sprite = defaultimage;
            if (items[i].image != null)
            {
                images[i].sprite = items[i].image;
            }
        }
    }
    void UpdateSelector()
    {
        for (int i = 0; i < images.Count; i++)
        {
            images[i].transform.GetChild(0).gameObject.SetActive(false);
        }
        images[selected].transform.GetChild(0).gameObject.SetActive(true);
        int selecteditemid = -1;
        if (!items[selected].Equals(default(Item)))
        {
            selecteditemid = items[selected].id;
        }
        for (int i = 0; i < realitems.Count; i++)
        {
            realitems[i].gameObject.SetActive(false);
            if (selecteditemid != -1)
            {
                if (realitems[i].item.id == selecteditemid)
                {
                    realitems[i].gameObject.SetActive(true);
                }
            }
        }
    }
    public Item RemoveItem(int index)
    {
        if (!items[index].Equals(default(Item)))
        {
            Item it = items[index];
            items[index] = default(Item);
            UpdateSelector();
            UpdateImages();
            return it;
        }
        return default(Item);
    }
}

[System.Serializable]
public struct Item
{
    public int id;
    public Sprite image;
}
