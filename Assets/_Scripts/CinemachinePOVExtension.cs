using Cinemachine;
using UnityEngine;


public class CinemachinePOVExtension : CinemachineExtension
{
    private InputManager inpuManager;
    private Vector3 startingRotation;

    bool gravityReversed;

    [SerializeField] private float clampAngle = 80f;

    [SerializeField] private float rotationAngle =0;

    [SerializeField] private float horizontalSpeed = 20f;
    [SerializeField] private float verticalSpeed = 10f;

    [SerializeField] float coefficient;
    float normalGravityF=0f;
    float reverseGravityF=180f;

    [Header("Za testiranje")]
    public bool eventActivated;
    public float timer;
    public float rawRotation;
    public float currentRotationZ;

    protected override void OnEnable()
    {
        Events.OnGravityReverse += GravityReverseEvent;
        base.OnEnable();
    }

    private void OnDisable()
    {
        Events.OnGravityReverse += GravityReverseEvent;
    }

    public void Start()
    {
        inpuManager = InputManager.Instance;
        if (startingRotation == null) { startingRotation = transform.localRotation.eulerAngles; }
        currentRotationZ = normalGravityF;
        base.Awake();
    }
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (vcam.Follow)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {


                Vector2 deltaInput = inpuManager.GetMouseDelta();
                startingRotation.x += deltaInput.x * Time.deltaTime * verticalSpeed;
                startingRotation.y += deltaInput.y * Time.deltaTime * horizontalSpeed;

                startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);

                //state.RawOrientation = Quaternion.Euler(startingRotation.y, -startingRotation.x, 0f);
                //state.RawOrientation = Quaternion.Euler(startingRotation.y, -startingRotation.x, normalGravityF);

                if (eventActivated)
                {
                    //if (timer >= 1) return;
                    timer += Time.deltaTime;
                    currentRotationZ = Mathf.Lerp(normalGravityF, reverseGravityF,timer);
                    


                    state.RawOrientation = Quaternion.Euler(startingRotation.y, -startingRotation.x, currentRotationZ);
                    rawRotation = currentRotationZ;
                }
                else if (currentRotationZ != normalGravityF )
                {
                    if (timer >= 1) return;
                    timer += Time.deltaTime;
                     currentRotationZ= Mathf.Lerp(reverseGravityF, normalGravityF, timer);
                  
                    state.RawOrientation = Quaternion.Euler(-startingRotation.y, startingRotation.x, currentRotationZ);
                    rawRotation = currentRotationZ;
                }
                else 
                {
                    state.RawOrientation = Quaternion.Euler(-startingRotation.y, startingRotation.x, currentRotationZ);
                }

            }
        }
    }



    private void GravityReverseEvent(Vector3 helper = default)
    {
        timer = 0;
        eventActivated = !eventActivated;
        

    }
}
