using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class SpaceChange : MonoBehaviour
{
    Rigidbody objecRigidbody;
    Transform startingScale;
    public bool grow;

    [SerializeField] Vector3 pickUpSize;

    void OnEnable()
    {
        Events.OnSpaceChange += ChangeSizeHelper;
    }

    void OnDisable()
    {
        Events.OnSpaceChange -= ChangeSizeHelper;
    }

    private void Start()
    {
        startingScale = transform;
        objecRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CanPickup();
    }

    void ChangeSizeHelper(Vector3 scalehelper, bool buttonPressed)
    {

        if (startingScale.localScale.magnitude > scalehelper.magnitude)
        {
            grow = false;
        }
        else if (startingScale.localScale.magnitude < scalehelper.magnitude)
        {
            grow = true;
        }
        else if (startingScale.localScale.magnitude == scalehelper.magnitude)
        {
            return;
        }

        StartCoroutine(ChangeSize(scalehelper, buttonPressed));
    }

    IEnumerator ChangeSize(Vector3 scale, bool buttonPressed)
    {

        if (transform.localScale != scale && transform.localScale.x >= 0)
        {
            if (grow)
            {
                if (transform.localScale.x >= scale.x && transform.localScale.x <= 0)
                {
                    StopCoroutine("ChangeSize");
                }

                yield return new WaitForSeconds(0.2f);
                transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);


                StopCoroutine("ChangeSize");
            }

            else if (!grow)
            {
                if (transform.localScale.x <= scale.x && transform.localScale.x <= 0)
                {
                    StopCoroutine("ChangeSize");
                }

                yield return new WaitForSeconds(0.2f);
                transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);

                StopCoroutine("ChangeSize");

            }

        }
        StopCoroutine("ChangeSize");
    }

    void CanPickup()
    {
        if (transform.localScale.magnitude <= pickUpSize.magnitude)
        {
            gameObject.tag = "Objective";
        }
    }
}

