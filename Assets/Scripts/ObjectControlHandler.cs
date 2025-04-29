using System;
using UnityEngine;

public class ObjectControlHandler : MonoBehaviour
{
    [Header("Object Selecting with RayCast Handling")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float rayLength;
    [SerializeField] private LayerMask interactableLayer;

    //Player Inventory Reference
    [SerializeField] private Inventory playerInventory;

    [Header("Player Throw Item Handling")]
    public float throwForce;
    public static event Action<IRetrievable> OnItemThrown;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        playerInventory = gameObject.GetComponent<Inventory>();
    }

    void Update()
    {
        HandleMouseInput();
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, rayLength, interactableLayer))
            {
                if (hit.collider.TryGetComponent<IPickable>(out var pickable))
                {
                    playerInventory.AddItem(pickable);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (playerInventory.HasItem())
            {
                IPickable item = playerInventory.RemoveItem();
                item.OnUse(transform);
                if (item is IRetrievable retrievable)
                {
                    OnItemThrown?.Invoke(retrievable);
                }
            }

        }
    }
}
