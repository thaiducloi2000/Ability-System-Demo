using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    [Title("Camera Setting", titleAlignment: TitleAlignments.Centered)]
    [SerializeField] private float cameraMoveSpeed = 1.0f;
    [SerializeField] private float cameraRotationY= 40f;
    [SerializeField] private LayerMask groundLayer;

    private Vector3 mousePoint;

    //Setting Component
    //private InputHandler handler;

    private Camera mainCamera;

    private PlayerMovementController _player;

    public void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        //handler = GetComponent<InputHandler>();
        mainCamera = Camera.main;
        mousePoint = Vector3.zero;
    }

    private void Start()
    {
        //Load Data From GameManager ?
        //handler.AssignBuildCallBack(UseAbility);
        //handler.AssignSelectCallBack(abilitiesManager.SelectAbility);
    }

    private void Update()
    {
        //Move(handler.MoveDirection);
        //MouseToWorldSpace(handler.MousePosition);
        //CheckState();
        FollowPlayer();
        //transform.position = Vector3.Lerp(transform.position, currentPosition, 1f);
    }

    //private void MouseToWorldSpace(Vector2 input)
    //{
    //    Ray ray = mainCamera.ScreenPointToRay(input);
    //    RaycastHit hit;
    //    if (Physics.Raycast(ray,out hit, maxDistance: float.MaxValue,layerMask: groundLayer))
    //    {
    //        mousePoint = hit.point;
    //    }
    //}

    private void UseAbility()
    {
        //abilitiesManager.UseAbility(mousePoint, null);
        //if (buildManager.state == BuildState.PLACEING)
        //{
        //    buildManager.PlacingPosition(mousePoint);
        //}
    }

    private void FollowPlayer()
    {
        if(Vector3.Distance(transform.position,_player.transform.position) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, _player.transform.position, 0.5f);
        }
    }

    public void AssignPlayer(PlayerMovementController player)
    {
        _player = player;
    } 

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 30f);
    }
#endif

}
