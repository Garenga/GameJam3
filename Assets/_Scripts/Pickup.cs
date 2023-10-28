using UnityEngine;

public enum AbilityType
{
    Gravity,Space,Time
}

public class Pickup : MonoBehaviour
{
    [SerializeField] Transform holdArea;
    private GameObject heldObject;
    public GameObject heldAbiliy;
    public Ability abilityToUse;
    private Rigidbody heldObjectRigidbody;

    [SerializeField]private float pickupRange = 5f;
    [SerializeField]private float pickupForce = 150f;

    InputManager inputManager;

    public AbilityType ability;
    private void Start()
    {
        inputManager = InputManager.Instance;
    }

    private void Update()
    {
        //if (inputManager.PlayerUseAbility(ability))
        //{
        //    heldAbiliy.GetComponent<Ability>().ActivateAbility(true);
        //    print(inputManager.PlayerUseAbility(ability));
        //}

        if (Input.GetMouseButtonDown(1))
        {
            if (heldAbiliy == null) return;
            heldAbiliy.GetComponent<Ability>().ActivateAbility(true);
        }
        if (Input.GetMouseButtonUp(1))
        {
            if (ability != AbilityType.Time) return;
            heldAbiliy.GetComponent<Ability>().ActivateAbility(false);
        }

        if (inputManager.PlayerPickUp())
        {
            //Debug.Log("PressedLeftButton");
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange))
            {
                PickupObject(hit.transform.gameObject);
               
            }
        }
        else
        {
            DropObject();
        }


        if (heldObject != null)
        {
            MoveObject();
        }

    }

    void PickupObject(GameObject pickObject)
    {
        if (pickObject.gameObject.CompareTag("Objective") && heldObject==null)
        {
            heldObjectRigidbody = pickObject.GetComponent<Rigidbody>();
            heldObjectRigidbody.gameObject.GetComponent<PickUpObjects>().isPickedUp = true;
            heldObjectRigidbody.useGravity = false;
            heldObjectRigidbody.drag = 10;
            heldObjectRigidbody.constraints = RigidbodyConstraints.FreezeRotation;

            heldObjectRigidbody.transform.parent = holdArea;
            heldObject = pickObject;
        }

        if (pickObject.gameObject.CompareTag("Ability"))
        {
            heldAbiliy = pickObject;
            abilityToUse = heldAbiliy.GetComponent<Ability>();
            pickObject.SetActive(false);

            CheckAbility();
        }
    }

    void MoveObject()
    {
        if (Vector3.Distance(heldObject.transform.position, holdArea.position) > 0.1f)
        {
            Vector3 moveDirection = (holdArea.position - heldObject.transform.position);
            heldObjectRigidbody.AddForce(moveDirection * pickupForce);
        }
    }
    void DropObject()
    {
        if (heldObjectRigidbody == null) return;

        heldObjectRigidbody.useGravity = true;
        heldObjectRigidbody.drag = 1;
        heldObjectRigidbody.constraints = RigidbodyConstraints.None;
        heldObjectRigidbody.gameObject.GetComponent<PickUpObjects>().isPickedUp = false;

        heldObjectRigidbody.transform.parent = null;
        heldObject = null;

    }

    void CheckAbility()
    {
        if (heldAbiliy.GetComponent<UseTime>())
        {
            ability = AbilityType.Time;
        }
        else if (heldAbiliy.GetComponent<UseSpace>())
        {
            ability = AbilityType.Space;
        }
        else if (heldAbiliy.GetComponent<UseGravity>())
        {
            ability = AbilityType.Gravity;
        }
    }
}
