using UnityEngine;

public class PickUpObjects : MonoBehaviour
{
    Renderer pickupRenderer;
    [SerializeField] Material outlineGlow;
    [SerializeField] Shader outlineGlowShader;
    public bool isPickedUp;
    Material[] matarials;

    private void Start()
    {
        pickupRenderer = GetComponent<Renderer>();
        matarials = pickupRenderer.materials;
    }

    private void Update()
    {
        if (isPickedUp)
        {
           // Material[] matarials = pickupRenderer.materials;

            matarials[1] = outlineGlow;
            matarials[1].shader = outlineGlowShader;

            pickupRenderer.materials = matarials;
        }
        else
        {

            //Material[] matarials = pickupRenderer.materials;

            matarials[1] = matarials[0];
            matarials[1].shader = matarials[0].shader;

            pickupRenderer.materials = matarials;
        }
    }

}
