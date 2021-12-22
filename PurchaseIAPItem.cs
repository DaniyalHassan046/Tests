using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using SIS;
using System;

public class PurchaseIAPItem : MonoBehaviour
{
    public string productId;

    [SerializeField]
    Image imgIcon;
    [SerializeField]
    Text txtPrice;

    Button btn;
    IAPObject obj;

    public UnityEvent AlreadyPurchased, OnPurchaseSuccussful;

    void Start()
    {
        obj = IAPManager.GetIAPObject(productId);
        if (imgIcon && obj.icon)
        {
            imgIcon.sprite = obj.icon;
        }
        if (txtPrice)
        {
            txtPrice.text = obj.realPrice;
        }

        btn = GetComponent<Button>();

        if (btn)
        {
            btn.onClick.AddListener(BuyProduct);
        }

        if (DBManager.isPurchased(productId))
        {
            AlreadyPurchased.Invoke();
            gameObject.SetActive(false);
        }
    }

    void OnEnable()
    {
        IAPManager.purchaseSucceededEvent += PurchaseSuccessful;
    }

    void OnDisable()
    {
        IAPManager.purchaseSucceededEvent -= PurchaseSuccessful;
    }

    //make buy money function
    public void BuyProduct()
    {
        IAPManager.PurchaseProduct(productId);
    }

    private void PurchaseSuccessful(string productId)
    {
        Debug.Log(productId);
        Debug.Log(this.productId);

        if (productId.Contains(this.productId))
        {
            OnPurchaseSuccussful.Invoke();
        }
    }

}
