using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Customizationapplyer : MonoBehaviour
{
    public enum type { vehicle, chracter };
    public type customization;
    public Characters[] Character;
    public int[] temp;
    public int character_index = 0;
    bool character = false;
    private void Awake()
    {
        for (int i = 0; i < Character.Length; i++)
        {
            Character[i].Character.SetActive(false);
        }
        for (int i = 0; i < Character.Length; i++)
        {
            if (PlayerPrefs.GetInt(customization + "IsCharacterPurchased" + i) == 1 && PlayerPrefs.GetInt(customization + "ActivePlayer") == i)
            {
                Character[i].Character.SetActive(true);
                character_index = i;
                //print(character_index + " Index");
                character = true;
                break;
            }
        }
        if (!character)
        {
            Character[0].Character.SetActive(true);
            character_index = 0;
        }
        Array.Resize(ref temp, PlayerPrefs.GetInt(customization + "TotalCategories"));
        for (int i = 0; i < temp.Length; i++)
        {
            //print("ItemId" + character_index.ToString() + " " + i.ToString());
            temp[i] = PlayerPrefs.GetInt(customization+"ItemId" + character_index.ToString() + " " + i.ToString() );
        }
        for (int i = 0; i < temp.Length; i++)
        {
            if (Character[character_index].Category[i].Item[temp[i]].Type == Items.Customize_Type.Texture)
            {
                for (int m = 0; m < Character[character_index].Category[i].Item[temp[i]].texture.Length; m++)
                {
                    Character[character_index].Category[i].Item[temp[i]].Texture_Change_object[m].mainTexture = Character[character_index].Category[i].Item[temp[i]].texture[m];
                }
            }

            else if (Character[character_index].Category[i].Item[temp[i]].Type==Items.Customize_Type.Object)
            {
                for (int j = 0; j < Character[character_index].Category[i].Item.Length; j++)
                {
                    
                    Character[character_index].Category[i].Item[j].Object.SetActive(false);

                    if (Character[character_index].Category[i].Item[j].is_colorizeable)
                    {
                        Character[character_index].Category[i].Item[j].Texturize_Material.mainTexture = Character[character_index].Category[i].Item[j].Colorize_Texture[PlayerPrefs.GetInt(PlayerPrefs.GetString("cutomisation") + "HairColor" + character_index.ToString() + j.ToString())];
                    }
                }
                
                Character[character_index].Category[i].Item[temp[i]].Object.SetActive(true);
                

            }
        }
    }
}
