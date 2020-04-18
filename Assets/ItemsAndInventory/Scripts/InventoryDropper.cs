using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InventoryDropper : MonoBehaviour
{
    [SerializeField] private Item Item;
    [SerializeField] private bool useInventorySystem;
    [Range(1, 17)]
    [SerializeField] private int howManyDrop = 1;
    [SerializeField] private float movement = 5;

    private HealthSystem healthSystem;
    private InventorySystem inventorySystem;
    private GameObject myPrefab;

    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        if (useInventorySystem)
            inventorySystem = GetComponent<InventorySystem>();

        myPrefab = Resources.Load<GameObject>("ItemGameObject");
    }

    // Start is called before the first frame update
    void Start()
    {
        healthSystem.OnDeathEvent.AddListener(OnDeath);
    }

    private void OnDestroy()
    {
        healthSystem.OnDeathEvent.RemoveListener(OnDeath);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDeath()
    {
        foreach (var _ in Enumerable.Range(0, howManyDrop))
        {
            if (useInventorySystem && inventorySystem != null)
            {
                var eq = inventorySystem.equipment.Where(n => n.Item != null).Count();
                var inv = inventorySystem.inventory.Where(n => n.Item != null).Count();
                if (eq + inv == 0) { return; }

                var i = Random.Range(0, eq + inv);

                if (i > eq)
                {
                    i -= eq;

                    Ref<Item> @ref = inventorySystem.inventory.Where(n => n.Item != null).ElementAt(i);
                    Item = @ref.Item;
                    @ref.Item = null;
                }
                else
                {
                    Ref<Item> @ref = inventorySystem.equipment.Where(n => n.Item != null).ElementAt(i);
                    Item = @ref.Item;
                    @ref.Item = null;
                }
            }

            if (Item != null)
            {
                GameObject ob = Instantiate(myPrefab, transform.position, Quaternion.identity);
                ob.GetComponent<ItemGameObject>().Item = Item;

                Vector2 velocity = new Vector2(Random.Range(.0f, 1.0f), Random.Range(.0f, 1.0f));
                velocity = movement * velocity.normalized;
                ob.GetComponent<Rigidbody2D>().AddForce(velocity, ForceMode2D.Impulse);
            }
        }
    }
}
