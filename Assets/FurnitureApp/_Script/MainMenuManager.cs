using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject MainMenuScreen;
    public GameObject CatagoryScreen;
    public GameObject ProductScreen;
    public Text CategoryName;

    public FurnitureData FurnitureDataInstance;

    public GameObject CategoryElementParent;
    public GameObject CategoryElement;

    public GameObject ProductElementParent;
    public GameObject ProductElement;

    public static bool DoShowHomeScreen = true;
    void Start()
    {
        if(!DoShowHomeScreen)
        {
            OnClickCategory(FurnitureDataManager.Instance.SelectedCategory.ToString());
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickLetsGoButton()
    {
        ProductScreen.SetActive(false);
        MainMenuScreen.SetActive(false);
        CatagoryScreen.SetActive(true);

        foreach(Transform child in CategoryElementParent.transform)
        {
            if(child.name!= "CatagoryElement")
            {
                Destroy(child.gameObject);
            }
        }
        for(int i = 0; i < FurnitureDataInstance.catagories.Length;i++)
        {
            GameObject CategoryElementTmp = Instantiate(CategoryElement, CategoryElementParent.transform);
            CategoryElementTmp.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = FurnitureDataInstance.catagories[i].categoryName;
            CategoryElementTmp.transform.GetChild(1).GetComponent<Image>().sprite = FurnitureDataInstance.catagories[i].category_images;
            string category_number = i.ToString(); 
            CategoryElementTmp.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { OnClickCategory(category_number); });
            CategoryElementTmp.SetActive(true);
        }
    }


    public void OnClickBackFromCategoryButton()
    {
        MainMenuScreen.SetActive(true);
        CatagoryScreen.SetActive(false);
    }

    public void OnClickCategory(string Cat_ID)
    {
        CatagoryScreen.SetActive(false);
        ProductScreen.SetActive(true);
        FurnitureDataManager.Instance.SelectedCategory = int.Parse(Cat_ID);
        CategoryName.text = FurnitureDataInstance.catagories[FurnitureDataManager.Instance.SelectedCategory].categoryName;

        foreach (Transform child in ProductElementParent.transform)
        {
            if (child.name != "ProductElement")
            {
                Destroy(child.gameObject);
            }
        }
        for (int i = 0; i < FurnitureDataInstance.catagories[FurnitureDataManager.Instance.SelectedCategory].products.Length; i++)
        {
            GameObject ProductElementTmp = Instantiate(ProductElement, ProductElementParent.transform);
            ProductElementTmp.transform.GetChild(0).GetChild(0).GetComponent<Text>().text =
                FurnitureDataInstance.catagories[FurnitureDataManager.Instance.SelectedCategory].products[i].product_name;
            ProductElementTmp.transform.GetChild(1).GetComponent<Image>().sprite =
                FurnitureDataInstance.catagories[FurnitureDataManager.Instance.SelectedCategory].products[i].product_images;
            string product_number = i.ToString();
            ProductElementTmp.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(delegate { OnClickProduct(product_number); });
            ProductElementTmp.SetActive(true);
        }

    }

    public void OnClickBackFromProductButton()
    {
        OnClickLetsGoButton();
    }

    public void OnClickProduct(string product_ID)
    {
        FurnitureDataManager.Instance.SelectedProduct = int.Parse(product_ID);
        print("Selected Product : " + FurnitureDataManager.Instance.SelectedProduct);
        SceneManager.LoadScene("MainAppScene");
    }
}


