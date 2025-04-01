using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private Image quantityContainer;
    [SerializeField] private TextMeshProUGUI itemQuantityTMP;

    public int Index { get; set; }

    // This will update the sprite from the default to the
    // item's icon, and the item's quantity values from default.
    public void UpdateSlot(InventoryItem item)
    {
        itemIcon.sprite = item.Icon;
        itemQuantityTMP.text = item.Quantity.ToString();
    }

    // This will toggle whether or not the slot
    // should show item and quantity images
    public void ShowSlotInformation(bool value)
    {
        itemIcon.gameObject.SetActive(value);
        quantityContainer.gameObject.SetActive(value);
    }
}
