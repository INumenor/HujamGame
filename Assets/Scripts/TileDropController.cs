using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileDropController : MonoBehaviour
{
    private MainInventory inventory;
    public GameObject item;
    Image tex = null;
    bool isActive = false;
    private string TileDropName;
    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<MainInventory>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if(inventory.slots[i].name == this.gameObject.name)
                {
                    tex = inventory.slots[i].GetComponentInChildren<Transform>().Find("Tex").GetComponent<Image>();
                    if (this.gameObject.name == "stone")
                    {
                        CharacterStatus.StoneNumbers += 1;
                        tex.GetComponentInChildren<Transform>().Find("Amount").GetComponent<Text>().text = CharacterStatus.StoneNumbers.ToString();
                    }
                    else if (this.gameObject.name == "gravel_stone")
                    {
                        CharacterStatus.MineNumbers += 1;
                        tex.GetComponentInChildren<Transform>().Find("Amount").GetComponent<Text>().text = CharacterStatus.MineNumbers.ToString();
                    }
                    else if (this.gameObject.name == "dirt")
                    {
                        CharacterStatus.DirtNumbers += 1;
                        tex.GetComponentInChildren<Transform>().Find("Amount").GetComponent<Text>().text = CharacterStatus.DirtNumbers.ToString();
                    }
                    else if (this.gameObject.name == "dirt_grass")
                    {
                        CharacterStatus.GrassNumbers += 1;
                        tex.GetComponentInChildren<Transform>().Find("Amount").GetComponent<Text>().text = CharacterStatus.GrassNumbers.ToString();
                    }
                    else if (this.gameObject.name == "slime_idle1_13")
                    {
                        CharacterStatus.MicStoneNumbers += 1;
                        tex.GetComponentInChildren<Transform>().Find("Amount").GetComponent<Text>().text = CharacterStatus.MicStoneNumbers.ToString();
                    }
                    else if (this.gameObject.name == "slime_idle1_3")
                    {
                        CharacterStatus.MicMineNumbers += 1;
                        tex.GetComponentInChildren<Transform>().Find("Amount").GetComponent<Text>().text = CharacterStatus.MicMineNumbers.ToString();
                    }
                    else if (this.gameObject.name == "slime_idle1_11")
                    {
                        CharacterStatus.MicDirtNumbers += 1;
                        tex.GetComponentInChildren<Transform>().Find("Amount").GetComponent<Text>().text = CharacterStatus.MicDirtNumbers.ToString();
                    }
                    break;
                }
                else
                {
                    if (inventory.isFull[i] == false)
                    {
                        tex = inventory.slots[i].GetComponentInChildren<Transform>().Find("Tex").GetComponent<Image>();
                        inventory.isFull[i] = true;
                        tex.sprite = this.gameObject.GetComponent<SpriteRenderer>().sprite;
                        inventory.slots[i].name = this.gameObject.name;
                        break;
                    }
                }
            }
            
            Destroy(this.gameObject);
        }
    }
}
