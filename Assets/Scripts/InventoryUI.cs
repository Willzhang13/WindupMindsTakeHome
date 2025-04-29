using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public List<Image> slotImages = new List<Image>();
    [SerializeField] private Inventory playerInventory;

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent<Image>(out var image))
            {
                slotImages.Add(image);
            }
        }
    }

    private void OnEnable()
    {
        if (playerInventory != null)
        {
            playerInventory.OnInventoryChanged += UpdateUI;
        }
    }

    private void OnDisable()
    {
        if (playerInventory != null)
            playerInventory.OnInventoryChanged -= UpdateUI;
    }

    public void UpdateUI(List<IPickable> items)
    {
        for (int i = 0; i < slotImages.Count; i++) 
        {
            if (i < items.Count && items[i]?.Icon != null)
            {
                slotImages[i].sprite = items[i].Icon;
                slotImages[i].color = Color.white;
            }
            else
            {
                slotImages[i].sprite = null;
                slotImages[i].color = new Color(1, 1, 1, 0.25f);
            }
        }
    }
}
