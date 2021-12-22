using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS;

[System.Serializable]
public class ItemToPuchase
{
    public string Item_id;
    public GameObject[] Ad_btns;
    public GameObject Offer;
}

public class PurchaseIAPItems : MonoBehaviour
{
    public ItemToPuchase[] Products;
    public bool showOfferOnStart;
    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < Products.Length; i++)
        {
            if (DBManager.isPurchased(Products[i].Item_id))
            {
                ManageBtns(i, false);
            }
            else
            {
                ManageBtns(i, true);
            }
        }

        if (showOfferOnStart)
        {
            for (int i = 0; i < Products.Length; i++)
            {
                if (!DBManager.isPurchased(Products[i].Item_id))
                {
                    ShowRandomOffer();
                    break;
                }
            }
        }
    }


    void OnEnable()
    {

        IAPManager.purchaseSucceededEvent += PurchaseSuccessful;
        IAPManager.purchaseFailedEvent += PurchaseFailed;

    }

    void OnDisable()
    {

        IAPManager.purchaseSucceededEvent -= PurchaseSuccessful;
        IAPManager.purchaseFailedEvent -= PurchaseFailed;
    }

    private void PurchaseSuccessful(string error)
    {
        for (int i = 0; i < Products.Length; i++)
        {
            if (DBManager.isPurchased(Products[i].Item_id))
            {
                ManageBtns(i, false);
            }
            else
            {
                ManageBtns(i, true);
            }
        }

        if (Products[c_offer].Offer && Products[c_offer].Offer.activeSelf)
        {
            Products[c_offer].Offer.SetActive(false);
        }
    }
    private void PurchaseFailed(string error)
    {

    }
    public void PuchaseNow(int Val)
    {
        IAPManager.PurchaseProduct(Products[Val].Item_id);
    }

    public void ManageBtns(int No, bool Active)
    {
        for (int j = 0; j < Products[No].Ad_btns.Length; j++)
        {
            if (Products[No].Ad_btns[j] && (Products[No].Ad_btns[j].activeSelf != Active))
                Products[No].Ad_btns[j].SetActive(Active);
        }
    }

    int count = 0;
    int c_offer = 0;
    public void ShowRandomOffer()
    {
        c_offer = Random.Range(0, Products.Length);

        if (c_offer == PlayerPrefs.GetInt("LastOffer", -1) || DBManager.isPurchased(Products[c_offer].Item_id))
        {
            if (count >= 5)
            {
                return;
            }
            count++;
            ShowRandomOffer();
            return;
        }

        if (Products[c_offer].Offer)
        {
            PlayerPrefs.SetInt("LastOffer", c_offer);
            Products[c_offer].Offer.SetActive(true);
        }
    }
    public void RemoveAds()
    {
        
    }
    public void Everything()
    {
        IAPManager.PurchaseProduct("everything");
        if (DBManager.isPurchased("everything"))
        {
            DBManager.SetPurchase("unlock_all_vehicles", 1);
            DBManager.SetPurchase("no_ads", 1);
            DBManager.SetPurchase("unlock_all_levels", 1);
            DBManager.SetPurchase("unlock_all_accessories", 1);
        }
    }
}
