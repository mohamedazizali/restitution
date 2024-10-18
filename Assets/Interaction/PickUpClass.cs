using UnityEngine;

public class PickUpClass : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private LayerMask pickUpLayerMask;
    [SerializeField] private QuestManager questManager;
    private ObjectGrabbable objectGrabbable;
    private bool inventoryReady = false;
    [SerializeField]
    private GameObject fireExtinguisher;

    private void Update()
    {
        if (fireExtinguisher != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                fireExtinguisher.GetComponent<FireExtinguisher>().SetPlayer(gameObject);
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                fireExtinguisher.GetComponent<FireExtinguisher>().Drop();
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (objectGrabbable == null)
            {
                // Not carrying an object, try to grab
                float pickUpDistance = 6f;
                if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickUpDistance, pickUpLayerMask))
                {
                    if (raycastHit.transform.TryGetComponent(out objectGrabbable))
                    {
                        //objectGrabbable.Grab(objectGrabPointTransform);

                        // Add the item to the inventory
                        if (objectGrabbable.TryGetComponent(out ItemPickup itemPickup))
                        {
                            if (itemPickup.item.itemName == "backpack") {
                                questManager.CompleteQuest1();
                                InventoryManager.Instance.Add(itemPickup.item);
                                Destroy(objectGrabbable.gameObject);
                                InventoryManager.Instance.ListItems();
                            }
                            else
                            { 
                            InventoryManager.Instance.Add(itemPickup.item);
                            Destroy(objectGrabbable.gameObject);
                            InventoryManager.Instance.ListItems();
                            }
                        }
                    }
                }
            }
            else
            {
                // Currently carrying something, drop
                objectGrabbable.Drop();
                objectGrabbable = null;
            }
            

        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FireExtinguisher"))
        {
            fireExtinguisher = other.gameObject;
        }
    }
}
