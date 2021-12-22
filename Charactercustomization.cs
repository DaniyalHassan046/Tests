using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;
using SIS;

[System.Serializable]
public class Items
{
    public enum Customize_Type { Object, Texture, Color }; 
    public Customize_Type Type;
    public string Name;
   // [Tooltip("This variable contains gameobject of Butoon. e.g: cap button, glasses button, shoes button etc")]
    //public GameObject item_button;
    [Tooltip("Size of (sprite and texture) or (sprite and Object(variable)) must same. This variable contains 2D sprite images for buttons e.g: shoes type1,shoes type2,shoes type3 etc.")]
    public Sprite sprites;
    [Tooltip("This variable contains the part of character/gameobject whose texture you want to change.")]
    [ShowIf("@Type == Customize_Type.Texture")]
    public Material[] Texture_Change_object;
    [Tooltip("This variable contains Textures for characterpart(variable name ) to change.")]
    [ShowIf("@Type == Customize_Type.Texture")]
    public Texture[] texture;
    [Tooltip("This variable contains gameobject to switch between different types e.g: shoes1(gameobject),shoes2(gameobject),shoes3(gameobject)etc")]
    [ShowIf("@Type == Customize_Type.Object")]
    public GameObject Object;
    [ShowIf("@Type == Customize_Type.Object")]
    public bool is_colorizeable = false;
    [ShowIf("@Type == Customize_Type.Object && is_colorizeable")]
    public GameObject Texture_buttons_panel;
    [ShowIf("@Type == Customize_Type.Object && is_colorizeable")]
    public Material Texturize_Material;
    [ShowIf("@Type == Customize_Type.Object && is_colorizeable")]
    public Texture[] Colorize_Texture;
    [HideInInspector]
    public GameObject inner_button;
    public int Coin_price = 20;
    public int Cash_price = 2;
    [ShowIf("@Type == Customize_Type.Color")]
    public bool IsColor=false;
    [ShowIf("@Type == Customize_Type.Color && IsColor")]
    public Color Col;
};
[System.Serializable]
public class Categories 
{
    public string Name;
    public GameObject MainButton;
    public Sprite image;
    public string purchase_id="Nothing";
    public bool purchased_from_iap = false;
    public GameObject CameraTransformPosition;
    public bool iscolorize;
    [ShowIf("iscolorize")]
    public Material Obj; 
    public Items[] Item;
};
[System.Serializable]
public class Characters
{
    public GameObject Character;
    public bool isunlock;
    public string id;
    public int Price;
    public GameObject SitPoint;
    public Categories[] Category;
};

public class Charactercustomization : MonoBehaviour
{
    public enum type { vehicle, chracter };
    public type customization;
    public GameObject Tutorial;
    public AudioSource Click;
    public Button playmainmanu;
    public Button next;
    public Button previos;
    public Button select;
    public Button start;
    public Button back;
    public Button cart;
    public Button cash_buy;
    public Button coins_buy;
    public Button back2;
    public Button btn_customization;
    public GameObject modepnl;
    public GameObject chracterscript;
    public GameObject vehiclescript;
    public Transform campos;
    public Transform carcampos;
    public GameObject customizationpnl, mainmenupnl;
    public GameObject Particle_Spawn_pos;
    public GameObject Menu_to_Customization_pos;
    public GameObject Cutomization_to_back_pos;
    string name = "";
    string inner_name = "";
    public string Inapp_everything_key = "unlock_all_accessories";
    public GameObject ModePanel;
    public GameObject Instantiate_inner_btn;
    public GameObject Instantiate_cart_btn;
    public GameObject Instantiate_catagory_btn;

    public GameObject InnerButtonsPanel;
    public GameObject CartButtonsPanel;
    public GameObject catagorypanel;
    public GameObject WarningPanel;
    public GameObject successpanel;
    public GameObject MainCamera;
    public GameObject Character_Panel;
    public GameObject Customize_Panel;
    public GameObject Customize_Button;
    public Animator player_animator;
    public Text totalprice_coins;
    public Text totalprice_cash;
    public Text Our_coins;
    public Text Our_cash;
    public Text PanelText;
    public Text Price_Panel_Val;
    public Text Character_Buy_Text;
    public Text Character_Coins_val;
    public Text Warning_message;
    public Text Confirm_message;
    public GameObject Buy_button;
    public GameObject Lock;
    public static Charactercustomization ins;
    public Transform pos;
    public Characters[] Character;
    int Character_Index = 0;
    int Category_Index = 0;
    int Item_Index = 0;
    GameObject texture_btn_panel_temp;
    public int[] Selected_Item_Index;
    int[] Coin_price_array;
    public int[] Cash_price_array;
    public int[] Store_Item_Index;
    int[] temp2;
    int temp3 = 1;
    int counter = 0;
    int haircolor = 0;
    private void Awake()
    {
        ins = this;
        Debug.Log(customization + "THIS IS ");
    }

    public void btnstart()
    {
        if ((int)customization == 0)
        {
            // modepnl.SetActive(true);
            // customizationpnl.SetActive(false);
            chracterscript.SetActive(true);
            //  btn_customization.GetComponent<Image>().sprite = chracter_cust;
            Character[Character_Index].Character.SetActive(false);
            MainCamera.transform.position = campos.transform.position;
            MainCamera.transform.rotation = campos.transform.rotation;
            Catagory_destroy();
            this.gameObject.SetActive(false);

        }
        else if ((int)customization == 1)
        {
            modepnl.SetActive(true);
            customizationpnl.SetActive(false);
            Catagory_destroy();
        }
    }
    public void btnback()
    {
        if ((int)customization == 0)
        {
            customizationpnl.SetActive(false);
            Character[Character_Index].Character.SetActive(false);
            mainmenupnl.SetActive(true);
            this.gameObject.SetActive(false);
            Debug.Log("back 0 running");

            MainCamera.transform.position = carcampos.transform.position;
            MainCamera.transform.rotation = carcampos.transform.rotation;
        }
        else if ((int)customization == 1)
        {
            customizationpnl.SetActive(false);
            mainmenupnl.SetActive(true);
            vehiclescript.SetActive(true);
            // btn_customization.GetComponent<Image>().sprite = vehicle_cust;
            Character[Character_Index].Character.SetActive(false);
            this.gameObject.SetActive(false);
            chracterscript.SetActive(false);
            MainCamera.transform.position = carcampos.transform.position;
            MainCamera.transform.rotation = carcampos.transform.rotation;
            Debug.Log("back 1 running");
        }

    }
    public void btncutomizeation()
    {

        if (Character_Buy_Text.text == "Select")
        {
            print("asdasd");
            //Character_Panel.SetActive(false);
            //Customize_Panel.SetActive(true);
            InnerButtonsPanel.transform.parent.parent.gameObject.SetActive(true);
           // ModePanel.SetActive(true);
            PlayerPrefs.SetInt(customization.ToString() + "ActivePlayer", Character_Index);
            destroy_buttons();
            destroy();
            Catagory_destroy();
            Category_Index = 0;
            catagorybtn_insttiatoor();
            button_instantiator();
        }
        else if (Character_Buy_Text.text == "Unlock")
        {
            if (DBManager.GetFunds("coins") - Character[Character_Index].Price >= 0)
            {
                Price_Panel_Val.text = "select";
                Character_Coins_val.text = (DBManager.GetFunds("coins") - Character[Character_Index].Price).ToString();
                DBManager.SetFunds("coins", int.Parse(Character_Coins_val.text.ToString()));
                PlayerPrefs.SetInt(customization.ToString() + "IsCharacterPurchased" + Character_Index, 1);
                Character[Character_Index].isunlock = true;
                //print("Active player: "+PlayerPrefs.GetInt("ActivePlayer"));
                Lock_Unlock();
            }
            else if (DBManager.GetFunds("coins") - Character[Character_Index].Price < 0)
            {
                WarningPanel.SetActive(true);
            }
        }

    }
    public void changeposagain()
    {
        back2btn();
    }
    public void back2btn()
    {

        if ((int)customization == 0)
        {
            Scene_Call(carcampos);
        }
        else if ((int)customization == 1)
        {
            Scene_Call(campos);
        }
    }
    private void OnEnable()
    {
        next.GetComponent<Button>().onClick.AddListener(Next);
        previos.GetComponent<Button>().onClick.AddListener(Previous);
        select.GetComponent<Button>().onClick.AddListener(Character_Buy);
        back2.GetComponent<Button>().onClick.AddListener(back2btn);
        select.GetComponent<Button>().onClick.AddListener(Tuto);
        start.GetComponent<Button>().onClick.AddListener(Character_Buy);
        back.GetComponent<Button>().onClick.AddListener(btnback);
        cart.GetComponent<Button>().onClick.AddListener(Ok);
        cash_buy.GetComponent<Button>().onClick.AddListener(Cash_buy_button);
        coins_buy.GetComponent<Button>().onClick.AddListener(Coins_buy_button);
        btn_customization.GetComponent<Button>().onClick.AddListener(btncutomizeation);
        back2btn();
        Store_Item_Index = new int[Character[Character_Index].Category.Length];
        for (int i = 0; i < Store_Item_Index.Length; i++)
        {
            Store_Item_Index[i] = -1;
        }
        if (PlayerPrefs.GetInt(customization.ToString() + "FirstTimeCustomization") != 0)
        {
            Onstart();
        }
    }
    private void OnDisable()
    {
        destroy();
        destroy_buttons();
        Character[Character_Index].Character.SetActive(true);
        next.GetComponent<Button>().onClick.RemoveListener(Next);
        back.GetComponent<Button>().onClick.RemoveListener(btnback);
        previos.GetComponent<Button>().onClick.RemoveListener(Previous);
        select.GetComponent<Button>().onClick.RemoveListener(Character_Buy);
        select.GetComponent<Button>().onClick.RemoveListener(Tuto);
        start.GetComponent<Button>().onClick.RemoveListener(Character_Buy);
        cart.GetComponent<Button>().onClick.RemoveListener(Ok);
        cash_buy.GetComponent<Button>().onClick.RemoveListener(Cash_buy_button);
        coins_buy.GetComponent<Button>().onClick.RemoveListener(Coins_buy_button);
        back2.GetComponent<Button>().onClick.RemoveListener(back2btn);
        btn_customization.GetComponent<Button>().onClick.RemoveListener(btncutomizeation);
        movetostart();
    }
    void movetostart()
    {
        MainCamera.transform.DOMove(pos.position, 1.5f);
        MainCamera.transform.DORotate(pos.rotation.eulerAngles, 1.5f);
        
    }
    void Start()
    {
        Array.Resize(ref Selected_Item_Index, Character[Character_Index].Category.Length);
        Array.Resize(ref Coin_price_array, Character[Character_Index].Category.Length);
        Array.Resize(ref Cash_price_array, Character[Character_Index].Category.Length);
        //Array.Resize(ref temp, Category.Length);
        Array.Resize(ref temp2, Character[Character_Index].Category.Length);
        for (int i = 0; i < Selected_Item_Index.Length; i++)
        {
            Selected_Item_Index[i] = -1;
            Coin_price_array[i] = -1;
            Cash_price_array[i] = -1;
        }
        Onstart();
        WarningPanel.SetActive(false);
        PlayerPrefs.SetString((int)customization + "cutomisation", customization.ToString());
    }
    public void Onstart()
    {
        if (DBManager.isPurchased(Character[Character_Index].id))
        {
            Character[Character_Index].isunlock = true;
        }
        Spawn();
        MainCamera.GetComponent<Camera>().fieldOfView = 70;

        Our_coins.text = DBManager.GetFunds("coins").ToString();
        Our_cash.text = DBManager.GetFunds("cash").ToString();
        Character_Panel.SetActive(true);
        Customize_Panel.SetActive(false);
        for (int i = 0; i < Character.Length; i++)
        {
            for (int j = 0; j < Character[i].Category.Length; j++)
            {
                if (DBManager.isPurchased(Inapp_everything_key))
                {
                    DBManager.SetPurchase(Character[i].Category[j].purchase_id, 1);
                }
                if (DBManager.isPurchased(Character[i].Category[j].purchase_id) || DBManager.isPurchased(Inapp_everything_key))
                {
                    //print("Purchased " + Character[i].Category[j].purchase_id);
                    Character[i].Category[j].purchased_from_iap = true;
                }
            }

        }
        PanelText.text = "";
        PanelText.text = Character[Character_Index].Category[Category_Index].Name;
        if (PlayerPrefs.GetInt(customization.ToString() + "FirstTimeCustomization") == 0)
        {
            PlayerPrefs.SetInt(customization.ToString() + "ActivePlayer", 0);

            PlayerPrefs.SetInt(customization.ToString() + "IsCharacterPurchased" + 0, 1);
            On_Start_character();
            temp3 = 1;
            for (int i = 0; i < Character[Character_Index].Category.Length; i++)
            {
                if (Character[Character_Index].Category[i].Item[0].Type == Items.Customize_Type.Color)
                {
                    for (int l = 0; l < Character.Length; l++)
                    {
                        Character[Character_Index].Category[i].Obj.color = Character[Character_Index].Category[i].Item[0].Col;
                    }
                }
                else if (Character[Character_Index].Category[i].Item[0].Type == Items.Customize_Type.Texture)
                {
                    for (int l = 0; l < Character[Character_Index].Category[i].Item[0].texture.Length; l++)
                    {
                        Character[Character_Index].Category[i].Item[0].Texture_Change_object[l].mainTexture = Character[Character_Index].Category[i].Item[0].texture[l];
                    }
                }
                else if (Character[Character_Index].Category[i].Item[0].Type == Items.Customize_Type.Object)
                {
                    for (int j = 0; j < Character[Character_Index].Category[i].Item.Length; j++)
                    {
                        Character[Character_Index].Category[i].Item[j].Object.SetActive(false);
                    }

                    Character[Character_Index].Category[i].Item[0].Object.SetActive(true);

                }
                for (int j = 0; j < Character[Character_Index].Category[i].Item.Length; j++)
                {
                    PlayerPrefs.SetInt(customization.ToString() + Character[Character_Index].Category[i].Item[j].Name, 0);
                    PlayerPrefs.SetInt(customization.ToString() + "ItemId" + Character_Index.ToString() + " " + i.ToString(), 0);
                    // if (Character[Character_Index].Category[i].Item[j].is_colorizeable)
                    // {
                    //     PlayerPrefs.SetInt(customization.ToString() + "HairColor" + Character_Index.ToString() + j.ToString(), 0);
                    //     Character[Character_Index].Category[i].Item[j].Texturize_Material.mainTexture = Character[Character_Index].Category[i].Item[j].Colorize_Texture[0];

                    // }
                }
            }
            PlayerPrefs.SetInt(customization.ToString() + "FirstTimeCustomization", 1);
        }
        else if (PlayerPrefs.GetInt(customization.ToString() + "FirstTimeCustomization") != 0)
        {
            On_Start_character();
            temp3 = 1;
            Debug.Log(Store_Item_Index);

            for (int i = 0; i < Store_Item_Index.Length; i++)
            {
                Store_Item_Index[i] = PlayerPrefs.GetInt(customization.ToString() + "ItemId" + Character_Index.ToString() + " " + i.ToString());
            }
            for (int i = 0; i < Store_Item_Index.Length; i++)
            {
                if (Store_Item_Index[i] != -1)
                {
                    if (Character[Character_Index].Category[i].Item[Store_Item_Index[i]].Type == Items.Customize_Type.Color)
                    {
                        Character[Character_Index].Category[i].Obj.color = Character[Character_Index].Category[i].Item[Store_Item_Index[i]].Col;
                    }
                    else if (Character[Character_Index].Category[i].Item[Store_Item_Index[i]].Type == Items.Customize_Type.Texture)
                    {

                        for (int m = 0; m < Character[Character_Index].Category[i].Item[Store_Item_Index[i]].texture.Length; m++)
                        {
                            Character[Character_Index].Category[i].Item[Store_Item_Index[i]].Texture_Change_object[m].mainTexture = Character[Character_Index].Category[i].Item[Store_Item_Index[i]].texture[m];
                        }

                    }
                    else if (Character[Character_Index].Category[i].Item[Store_Item_Index[i]].Type == Items.Customize_Type.Object)
                    {
                        for (int j = 0; j < Character[Character_Index].Category[i].Item.Length; j++)
                        {

                            Character[Character_Index].Category[i].Item[j].Object.SetActive(false);

                            // if (Character[Character_Index].Category[i].Item[j].is_colorizeable)
                            // {
                            //     Character[Character_Index].Category[i].Item[j].Texturize_Material.mainTexture = Character[Character_Index].Category[i].Item[j].Colorize_Texture[PlayerPrefs.GetInt(customization.ToString() + "HairColor" + Character_Index.ToString() + j.ToString())];
                            // }
                        }
                        Character[Character_Index].Category[i].Item[Store_Item_Index[i]].Object.SetActive(true);


                    }
                }
            }
        }
        PlayerPrefs.SetInt(customization.ToString() + "TotalCategories", Character[Character_Index].Category.Length);
        Character[Character_Index].Character.GetComponent<Animator>();
    }
    public void Change_Through_index(int val)
    {
        Character[Character_Index].Character.SetActive(false);
        Character_Index = val;
        Character[Character_Index].Character.SetActive(true);
        Lock_Unlock();
        Onstart();
        Cart_Empty();
        Spawn();
        //player_animator = Character[Character_Index].Character.GetComponent<Animator>();
    }
    public void Main_panel_buttons()
    {
        destroy_buttons();
        name = EventSystem.current.currentSelectedGameObject.name;
        main_index_setter();
        button_instantiator();
    }
    public void main_index_setter()
    {
        for (int i = 0; i < Character[Character_Index].Category.Length; i++)
        {
            Character[Character_Index].Category[i].MainButton.GetComponent<Image>().color = Color.yellow;
            if (name == Character[Character_Index].Category[i].MainButton.name)
            {
                Character[Character_Index].Category[i].MainButton.GetComponent<Image>().color = Color.red;
                Category_Index = i;

            }
        }
    }
    public void index_setter()
    {
        for (int i = 0; i < Character[Character_Index].Category[Category_Index].Item.Length; i++)
        {
            Character[Character_Index].Category[Category_Index].Item[i].inner_button.transform.GetChild(2).gameObject.SetActive(false);
            if (inner_name == Character[Character_Index].Category[Category_Index].Item[i].inner_button.name)
            {
                Item_Index = i;
                Character[Character_Index].Category[Category_Index].Item[i].inner_button.transform.GetChild(2).gameObject.SetActive(true);
                //print("Inner idex: " + Item_Index);
            }
        }
    }
    public void Scene_Call(Transform a)
    {
        Character[Character_Index].Character.SetActive(false);
        MainCamera.transform.DOMove(a.transform.position, 1.5f);
        MainCamera.transform.DORotate(a.transform.rotation.eulerAngles, 1.5f);
        Onstart();
        //SceneManager.LoadScene("MainMenu");
    }
    public void catagorybtn_insttiatoor()
    {
        for (int i = 0; i < Character[Character_Index].Category.Length; i++)
        {

            GameObject tempcatagory = Instantiate(Instantiate_catagory_btn, catagorypanel.transform);
            tempcatagory.name = Character[Character_Index].Category[i].Name;
            tempcatagory.transform.GetChild(0).GetComponent<Image>().sprite = Character[Character_Index].Category[i].image;
            tempcatagory.GetComponent<Button>().onClick.AddListener(Main_panel_buttons);
            Character[Character_Index].Category[i].MainButton = tempcatagory;
        }
    }
    public void button_instantiator()
    {
        MainCamera.transform.DOMove(Character[Character_Index].Category[Category_Index].CameraTransformPosition.transform.position, 1.5f);
        MainCamera.transform.DORotate(Character[Character_Index].Category[Category_Index].CameraTransformPosition.transform.rotation.eulerAngles, 1.5f);
        PanelText.text = Character[Character_Index].Category[Category_Index].MainButton.name;
        // print(Item[index].Item[index].textures.Length);
        for (int i = 0; i < Character[Character_Index].Category[Category_Index].Item.Length; i++)
        {
            InnerButtonsPanel.SetActive(true);

            GameObject tempGameobject = Instantiate(Instantiate_inner_btn, InnerButtonsPanel.transform);
            tempGameobject.name = i.ToString();
            if (Character[Character_Index].Category[Category_Index].purchased_from_iap)
            {
                Character[Character_Index].Category[Category_Index].Item[i].Coin_price = 0;
            }
            tempGameobject.transform.GetChild(0).GetComponent<Text>().text = Character[Character_Index].Category[Category_Index].Item[i].Coin_price.ToString();
            tempGameobject.transform.GetChild(1).GetComponent<Text>().text = Character[Character_Index].Category[Category_Index].Item[i].Cash_price.ToString();
            tempGameobject.GetComponent<Image>().sprite = Character[Character_Index].Category[Category_Index].Item[i].sprites;
            if (Character[Character_Index].Category[Category_Index].Item[i].Type== Items.Customize_Type.Color)
            {
                tempGameobject.GetComponent<Image>().color = Character[Character_Index].Category[Category_Index].Item[i].Col;
                //tempGameobject.GetComponent<Image>().color;
            }
            tempGameobject.GetComponent<Button>().onClick.AddListener(CutomButtonClick);
            tempGameobject.transform.GetChild(2).gameObject.SetActive(false);
            Character[Character_Index].Category[Category_Index].Item[i].Name = Character[Character_Index].Category[Category_Index].Name + " " + i;
            if (Character[Character_Index].Category[Category_Index].Item[i].Coin_price == 0 || temp3 == 0 || Character[Character_Index].Category[Category_Index].purchased_from_iap || PlayerPrefs.GetInt(customization.ToString() + Character_Index.ToString() + Character[Character_Index].Category[Category_Index].Item[i].Name) == 1)
            {
                tempGameobject.transform.GetChild(0).GetComponent<Text>().text = "Open";
                tempGameobject.transform.GetChild(1).GetComponent<Text>().text = "Open";
                Character[Character_Index].Category[Category_Index].Item[i].Coin_price = 0;
                Character[Character_Index].Category[Category_Index].Item[i].Cash_price = 0;
            }
            Character[Character_Index].Category[Category_Index].Item[i].inner_button = tempGameobject;
        }

    }

    public void value_checker()
    {
        for (int i = 0; i < Selected_Item_Index.Length; i++)
        {
            if (Selected_Item_Index[i] != -1)
            {
                counter += 1;
            }
        }
    }
    public void BuyThroughCustomize()
    {
        
    }
    public void Ok()
    {
        Our_coins.text = DBManager.GetFunds("coins").ToString();
        destroy();
        //value_checker();
        totalprice_coins.text = "Free";
        totalprice_cash.text = "Free";
        for (int i = 0; i < Character[Character_Index].Category.Length; i++)
        {
            if (Selected_Item_Index[i] != -1)
            {
                GameObject temp = Instantiate(Instantiate_cart_btn, CartButtonsPanel.transform);
                temp.name = i.ToString();
                temp.transform.GetChild(0).GetComponent<Image>().sprite = Character[Character_Index].Category[i].Item[Selected_Item_Index[i]].sprites;
                temp.GetComponent<Button>().onClick.AddListener(CartButton);
                if (Character[Character_Index].Category[i].Item[Selected_Item_Index[i]].Coin_price == 0 || temp3 == 0 || Character[Character_Index].Category[i].purchased_from_iap || PlayerPrefs.GetInt(customization.ToString() + Character_Index + Character[Character_Index].Character.name + Character[Character_Index].Category[i].Item[Selected_Item_Index[i]].Name) == 1)
                {
                    temp.transform.GetChild(2).gameObject.GetComponent<Text>().text = "Free";
                    temp.transform.GetChild(3).gameObject.GetComponent<Text>().text = "Free";
                }
                else if (temp3 == 1)
                {
                    temp.transform.GetChild(2).gameObject.GetComponent<Text>().text = Coin_price_array[i].ToString();
                    temp.transform.GetChild(3).gameObject.GetComponent<Text>().text = Cash_price_array[i].ToString();
                    //temp.transform.GetChild(2).gameObject.GetComponent<Text>().text = price_array[i].ToString();
                }
            }
        }
        if (temp3 != 0)
        {
            totalprice_coins.text = "0";
            totalprice_cash.text = "0";
            for (int i = 0; i < Coin_price_array.Length; i++)
            {
                if (Coin_price_array[i] != -1 && Cash_price_array[i] != -1)
                {
                    totalprice_coins.text = (int.Parse(totalprice_coins.text) + Coin_price_array[i]).ToString();
                    totalprice_cash.text = (int.Parse(totalprice_cash.text) + Cash_price_array[i]).ToString();
                }
            }
        }
    }
    public void ChangeColor(Color col)
    {
        Character[Character_Index].Category[Category_Index].Obj.color=col;        
    }
    public void CartButton()
    {
        Click.Play();
        if (EventSystem.current.currentSelectedGameObject.transform.GetChild(1).gameObject.activeInHierarchy)
        {
            EventSystem.current.currentSelectedGameObject.transform.GetChild(1).gameObject.SetActive(false);
            if (temp3 != 0)
            {
                totalprice_coins.text = (int.Parse(totalprice_coins.text) - Coin_price_array[int.Parse(EventSystem.current.currentSelectedGameObject.name)]).ToString();
                totalprice_cash.text = (int.Parse(totalprice_cash.text) - Cash_price_array[int.Parse(EventSystem.current.currentSelectedGameObject.name)]).ToString();
            }
            Selected_Item_Index[int.Parse(EventSystem.current.currentSelectedGameObject.name)] = -1;
        }
        else
        {
            EventSystem.current.currentSelectedGameObject.transform.GetChild(1).gameObject.SetActive(true);
            if (temp3 != 0)
            {
                totalprice_coins.text = (int.Parse(totalprice_coins.text) + Coin_price_array[int.Parse(EventSystem.current.currentSelectedGameObject.name)]).ToString();
                totalprice_cash.text = (int.Parse(totalprice_cash.text) + Cash_price_array[int.Parse(EventSystem.current.currentSelectedGameObject.name)]).ToString();
            }
            Selected_Item_Index[int.Parse(EventSystem.current.currentSelectedGameObject.name)] = temp2[int.Parse(EventSystem.current.currentSelectedGameObject.name)];
        }
    }
    public void destroy_buttons()
    {
        for (int i = 0; i < InnerButtonsPanel.transform.childCount; i++)
        {
            Destroy(InnerButtonsPanel.transform.GetChild(i).gameObject);
        }
    }
    public void destroy()
    {
        for (int i = 0; i < CartButtonsPanel.transform.childCount; i++)
        {
            Destroy(CartButtonsPanel.transform.GetChild(i).gameObject);
        }
    }
    public void Catagory_destroy()
    {
        for (int i = 0; i < catagorypanel.transform.childCount; i++)
        {
            Destroy(catagorypanel.transform.GetChild(i).gameObject);
        }
    }
    void Buy_through_coins()
    {
        if (temp3 == 0)
        {
            totalprice_coins.text = "Free";
            success();
        }
        else if ((int.Parse(Our_coins.text) - int.Parse(totalprice_coins.text)) >= 0)
        {
            Our_coins.text = (int.Parse(Our_coins.text) - int.Parse(totalprice_coins.text)).ToString();
            DBManager.SetFunds("coins", int.Parse(Our_coins.text));
            success();
            Onstart();
            WarningPanel.SetActive(false);
        }
        else
        {
            WarningPanel.SetActive(true);
            Warning_message.text = "You don't have enough Coins to buy these items";
        }
    }
    public void Coins_buy_button()
    {
        Confirm_message.text = "You really want to buy these item by Coins?";
        Buy_button.GetComponent<Button>().onClick.AddListener(Buy_through_coins);
    }
    public void Cash_buy_button()
    {
        Confirm_message.text = "You really want to buy these item by Cash?";
        Buy_button.GetComponent<Button>().onClick.AddListener(Buy_through_cash);
    }
    void Buy_through_cash()
    {
        if (temp3 == 0)
        {
            totalprice_cash.text = "Free";
            success();
        }
        else if ((int.Parse(Our_cash.text) - int.Parse(totalprice_cash.text)) >= 0)
        {
            Our_cash.text = (int.Parse(Our_cash.text) - int.Parse(totalprice_cash.text)).ToString();
            DBManager.SetFunds("cash", int.Parse(Our_cash.text));
            success();
            Onstart();
            WarningPanel.SetActive(false);
        }
        else
        {
            WarningPanel.SetActive(true);
            Warning_message.text = "You don't have enough Cash to buy these items";
        }
    }
    public void Emotes()
    {
        player_animator.Play(UnityEngine.Random.Range(0,12).ToString());
    }
    public void success()
    {
        successpanel.SetActive(true);
        Back_to_Character_Shop();
        for (int i = 0; i < Selected_Item_Index.Length; i++)
        {
            if (Selected_Item_Index[i] != -1)
            {
                //PlayerPrefs.SetInt(Character[Character_Index].Category[Category_Index].Item[Selected_Item_Index[i]].Name, 1);
                PlayerPrefs.SetInt(customization.ToString() + Character_Index.ToString() + Character[Character_Index].Category[i].Item[Selected_Item_Index[i]].Name, 1);
                Debug.Log(customization.ToString() + "ItemId" + Character_Index.ToString() + " " + i.ToString());
                PlayerPrefs.SetInt(customization.ToString() + "ItemId" + Character_Index.ToString() + " " + i.ToString(), Selected_Item_Index[i]);
                //print("ItemId" + Character_Index.ToString() + " " + i.ToString() + "   " + Selected_Item_Index[i]);
                PlayerPrefs.SetInt(customization.ToString() + "HairColor" + Character_Index.ToString() + Item_Index.ToString(), haircolor);
            }
            Selected_Item_Index[i] = -1;
            Coin_price_array[i] = 0;
            Cash_price_array[i] = 0;
        }
    }
    private void CutomButtonClick()
    {
        Debug.Log(EventSystem.current.currentSelectedGameObject.name);
        inner_name = EventSystem.current.currentSelectedGameObject.name;
        index_setter();
        Click.Play();
        if (Character[Character_Index].Category[Category_Index].Item[Item_Index].IsColor)
        {
            Character[Character_Index].Category[Category_Index].Obj.color = Character[Character_Index].Category[Category_Index].Item[Item_Index].Col;
        }
        else if (Character[Character_Index].Category[Category_Index].Item[Item_Index].Type == Items.Customize_Type.Texture)
        {
            for (int m = 0; m < Character[Character_Index].Category[Category_Index].Item[Item_Index].texture.Length; m++)
            {
                Character[Character_Index].Category[Category_Index].Item[Item_Index].Texture_Change_object[m].mainTexture = Character[Character_Index].Category[Category_Index].Item[Item_Index].texture[m];
            }
        }
        else if (Character[Character_Index].Category[Category_Index].Item[Item_Index].Type == Items.Customize_Type.Object)
        {
            for (int i = 0; i < Character[Character_Index].Category[Category_Index].Item.Length; i++)
            {

                Character[Character_Index].Category[Category_Index].Item[i].Object.SetActive(false);

            }
            Character[Character_Index].Category[Category_Index].Item[Item_Index].Object.SetActive(true);
        }
        else
        {
            Debug.Log("You missed something in inspector panel, please check it and try again");
        }
        Selected_Item_Index[Category_Index] = Item_Index;
        temp2[Category_Index] = Item_Index;
        Coin_price_array[Category_Index] = Character[Character_Index].Category[Category_Index].Item[Item_Index].Coin_price;
        Cash_price_array[Category_Index] = Character[Character_Index].Category[Category_Index].Item[Item_Index].Cash_price;
        Debug.Log("Button was pressed");
        //print("Id" + index + ":" + inner_index+ "PriceofId" + index + ":" + Category[index].Item[inner_index].price); 
    }
    public void Character_Buy()
    {
        if ((int)customization == 0)
        {

            if (Character_Buy_Text.text == "Select")
            {
               PlayerPrefs.SetInt(customization.ToString() + "ActivePlayer", Character_Index);
                destroy_buttons();
                destroy();
                Catagory_destroy();
                modepnl.SetActive(true);
                customizationpnl.SetActive(false);
                chracterscript.SetActive(false);
                Character[Character_Index].Character.SetActive(true);

            }
            else if (Character_Buy_Text.text == "Unlock")
            {
                if (DBManager.GetFunds("coins") - Character[Character_Index].Price >= 0)
                {
                    Price_Panel_Val.text = "select";
                    Character_Coins_val.text = (DBManager.GetFunds("coins") - Character[Character_Index].Price).ToString();
                    DBManager.SetFunds("coins", int.Parse(Character_Coins_val.text.ToString()));
                    PlayerPrefs.SetInt(customization.ToString() + "IsCharacterPurchased" + Character_Index, 1);
                    Character[Character_Index].isunlock = true;
                    //print("Active player: "+PlayerPrefs.GetInt("ActivePlayer"));
                    Lock_Unlock();
                }
                else if (DBManager.GetFunds("coins") - Character[Character_Index].Price < 0)
                {
                    WarningPanel.SetActive(true);
                }
            }

        }

        else if ((int)customization == 1)
        {
            
            if (Character_Buy_Text.text == "Select")
            {
                PlayerPrefs.SetInt(customization.ToString() + "ActivePlayer", Character_Index);
                destroy_buttons();
                destroy();
                Catagory_destroy();
                modepnl.SetActive(true);
                customizationpnl.SetActive(false);
                chracterscript.SetActive(false);
                Character[Character_Index].Character.SetActive(false);

            }
            else if (Character_Buy_Text.text == "Unlock")
            {
                if (DBManager.GetFunds("coins") - Character[Character_Index].Price >= 0)
                {
                    Price_Panel_Val.text = "select";
                    Character_Coins_val.text = (DBManager.GetFunds("coins") - Character[Character_Index].Price).ToString();
                    DBManager.SetFunds("coins", int.Parse(Character_Coins_val.text.ToString()));
                    PlayerPrefs.SetInt(customization.ToString() + "IsCharacterPurchased" + Character_Index, 1);
                    Character[Character_Index].isunlock = true;
                    //print("Active player: "+PlayerPrefs.GetInt("ActivePlayer"));
                    Lock_Unlock();
                }
                else if (DBManager.GetFunds("coins") - Character[Character_Index].Price < 0)
                {
                    WarningPanel.SetActive(true);
                }

            }


        }
    }
    void Cart_Empty()
    {
        for (int i = 0; i < Selected_Item_Index.Length; i++)
        {
            Selected_Item_Index[i] = -1;
        }
    }

    public void Next()
    {
        Character_Index++;
        if (Character_Index > Character.Length - 1)
        {
            Character_Index = 0;
        }
        Character[Character_Index].Character.SetActive(true);
        Invoke("particle_on", 1f);
        if (Character_Index == 0)
        {
            Character[Character.Length - 1].Character.SetActive(false);
        }
        else
        {
            Character[Character_Index - 1].Character.SetActive(false);
        }
        Lock_Unlock();
        Onstart();
        Cart_Empty();
        Spawn();
        player_animator = Character[Character_Index].Character.GetComponent<Animator>();
        //if (PlayerPrefs.GetInt("Shader"+index) == 2)
        //{
        //    CustomShader();
        //}
        //else
        //{
        //    DiffuseShader();
        //}
        //yield return new WaitForSeconds(0f);


    }
    void Spawn()
    {
    }
    public void Previous()
    {
        Character_Index--;
        if (Character_Index < 0)
        {
            Character_Index = Character.Length - 1;
        }
        Character[Character_Index].Character.SetActive(true);
        Invoke("particle_on", 1f);
        if (Character_Index == Character.Length - 1)
        {
            Character[0].Character.SetActive(false);
        }
        else
        {
            Character[Character_Index + 1].Character.SetActive(false);
        }
        Lock_Unlock();
        Onstart();
        Cart_Empty();
        Spawn();
        player_animator = Character[Character_Index].Character.GetComponent<Animator>();

        //yield return new WaitForSeconds(0f);
    }
    private void Lock_Unlock()
    {
        if (Character[Character_Index].isunlock)
        {
            Lock.SetActive(false);
            Price_Panel_Val.text = "Free";
            Character_Buy_Text.text = "Select";
            btn_customization.gameObject.SetActive(true);
            //coins.text
        }
        else if (Character[Character_Index].isunlock != true)
        {
            Lock.SetActive(true);
            Price_Panel_Val.text = Character[Character_Index].Price.ToString();
            Character_Buy_Text.text = "Unlock";
            btn_customization.gameObject.SetActive(false);

        }
    }
    public void Back_to_Character_Shop()
    {
        //MainCamera.transform.DOMove(Menu_to_Customization_pos.transform.position, 1.5f);
        Onstart();
        destroy_buttons();
        destroy();
        Catagory_destroy();

    }
    void On_Start_character()
    {
        Character_Coins_val.text = DBManager.GetFunds("coins").ToString();
        for (int i = 0; i < Character.Length; i++)
        {
            if(PlayerPrefs.GetInt(customization.ToString() + "IsCharacterPurchased" + i)==1)
            {
                Character[i].isunlock = true;
            }
            if (i == 1)
            {
                Character[Character_Index].Character.SetActive(true);
                Lock_Unlock();
            }

        }
    }
    public void Tuto()
    {
        if (PlayerPrefs.GetInt(customization.ToString() + "FirstTimeCustomization") == 0)
        {
            Tutorial.SetActive(true);
            PlayerPrefs.SetInt(customization.ToString() + "FirstTimeCustomization", 1);
        }
    }
    public void Texturize_Object_button(int val)
    {
        val -= 1;
        haircolor = val;
        // if (Character[Character_Index].Category[Category_Index].Item[Item_Index].is_colorizeable)
        // {
        //     Character[Character_Index].Category[Category_Index].Item[Item_Index].Texturize_Material.mainTexture = Character[Character_Index].Category[Category_Index].Item[Item_Index].Colorize_Texture[val];
        // }
    }
    //public void modeback()
    //{
    //    MainCamera.transform.position = pos.transform.position;
    //    MainCamera.transform.rotation = pos.transform.rotation;
    //    customizationpnl.SetActive(false);
    //    Character_Panel.SetActive(true);
    //}
}
