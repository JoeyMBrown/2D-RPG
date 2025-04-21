using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : Singleton<CraftingManager>
{
    [Header("Config")]
    [SerializeField] private RecipeCard recipeCardPrefab;
    [SerializeField] private Transform recipeContainer;
    [SerializeField] private GameObject craftMaterialsPanel;

    [Header("Recipe Info")]
    [SerializeField] private TextMeshProUGUI recipeName;
    [SerializeField] private Image item1Icon;
    [SerializeField] private TextMeshProUGUI item1Name;
    [SerializeField] private TextMeshProUGUI item1Amount;
    
    [SerializeField] private Image item2Icon;
    [SerializeField] private TextMeshProUGUI item2Name;
    [SerializeField] private TextMeshProUGUI item2Amount;
    [SerializeField] private Button craftButton;

    [Header("Final Item")]
    [SerializeField] private Image finalItemIcon;
    [SerializeField] private TextMeshProUGUI finalItemName;
    [SerializeField] private TextMeshProUGUI finalItemDescription;

    [Header("Recipes")]
    [SerializeField] private RecipeList recipes;
    public Recipe RecipeSelected { get; set; }

    private void Start()
    {
        LoadRecipes();
    }

    private void LoadRecipes()
    {
        for (int i = 0; i < recipes.Recipes.Length; i ++)
        {
            RecipeCard card = Instantiate(recipeCardPrefab, recipeContainer);
            card.InitRecipeCard(recipes.Recipes[i]);
        }
    }

    // TODO: Fix me - This is a poor implementation.
    public void CraftItem()
    {
        for (int i = 0; i < RecipeSelected.Item1Amount; i++)
        {
            Inventory.Instance.ConsumeItem(RecipeSelected.Item1.ID);
        }

        for (int i = 0; i < RecipeSelected.Item2Amount; i++)
        {
            Inventory.Instance.ConsumeItem(RecipeSelected.Item2.ID);
        }

        Inventory.Instance.AddItem(RecipeSelected.FinalItem, RecipeSelected.FinalItemAmount);
        ShowRecipe(RecipeSelected);
    }

    public void ShowRecipe(Recipe recipe)
    {
        if (craftMaterialsPanel.activeSelf == false)
        {
            craftMaterialsPanel.SetActive(true);
        }

        RecipeSelected = recipe;

        recipeName.text = recipe.Name;
        item1Icon.sprite = recipe.Item1.Icon;
        item1Name.text = recipe.Item1.Name;

        item2Icon.sprite = recipe.Item2.Icon;
        item2Name.text = recipe.Item2.Name;

        item1Amount.text = $"{Inventory.Instance.GetItemCurrentQuantityInInventory(recipe.Item1.ID)}/{recipe.Item1Amount}";
        item2Amount.text = $"{Inventory.Instance.GetItemCurrentQuantityInInventory(recipe.Item2.ID)}/{recipe.Item2Amount}";

        finalItemIcon.sprite = recipe.FinalItem.Icon;
        finalItemName.text = recipe.FinalItem.Name;
        finalItemDescription.text = recipe.FinalItem.Description;

        craftButton.interactable = CanCraftItem(recipe);
    }

    private bool CanCraftItem(Recipe recipe)
    {
        int item1Stock = Inventory.Instance.GetItemCurrentQuantityInInventory(recipe.Item1.ID);
        int item2Stock = Inventory.Instance.GetItemCurrentQuantityInInventory (recipe.Item2.ID);

        return item1Stock >= recipe.Item1Amount && item2Stock >= recipe.Item2Amount;
    }
}
