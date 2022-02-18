using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class FurnitureDataManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static FurnitureDataManager Instance;
    public FurnitureData FurnitureDataInstance;
    public int SelectedCategory;
    public int SelectedProduct;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        
    }

}


[Serializable]
public class FurnitureData
{
    public CategoryClass[] catagories;
}

[Serializable]
public class CategoryClass
{
    public string categoryName;
    public Sprite category_images;
    public ProductClass[] products;
}

[Serializable]
public class ProductClass
{
    public string product_name;
    public Sprite product_images;
    public GameObject product_model;
}


/*
 * Kichen Pack 1 Includes: Category 2 
Deep fryer. Product 1
* 6 burner stove. Product 2
* Prep table. Product 3
* Extractor hood. Product 4
*
* 
 * Kichen Pack 2 Includes: Category 1 
* Griddle Product 1
* Char grill Product 2
* Convection oven Product 3*/



