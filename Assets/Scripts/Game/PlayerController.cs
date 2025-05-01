using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
public partial class PlayerController : MonoBehaviour
{
    [SerializeField] private AudioData audioData;
    [SerializeField] private AudioSettings audioSettings;
    [SerializeField] private ChannelPlayer sfxPlayer;

    [Header("Player Movement Properties")]
    [SerializeField] private float speed;
    [SerializeField] Vector2 movementInput;

    [Header("Jump Properties")]
    [SerializeField] private float jumoForce;
    public bool canJump;

    [Header("Raycast Properties")]
    [SerializeField] private Transform _origin;
    [SerializeField] private Vector3 _direction = Vector3.down;
    [SerializeField] private float _distance;
    [SerializeField] private LayerMask _layerInteraction;

    [Header("DrawRay Properties")]
    [SerializeField] private Color OnCollisionRay = Color.green;
    [SerializeField] private Color OnNotCollisionRay = Color.white;

    private Rigidbody _rigidbody;
    private bool isMoving;
    private float stepTimer;
    [SerializeField] private float stepDelay = 0.4f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Debug.Log(audioSettings);
    }
    private void Update()
    {

        Vector3 direction = new Vector3(movementInput.x, 0f, movementInput.y);
        transform.Translate(direction *  speed * Time.deltaTime);

        isMoving = direction.magnitude > 0.1f;

        if (isMoving && canJump)
        {
            stepTimer += Time.deltaTime;
            if (stepTimer >= stepDelay)
            {
                PlayFootStep();
                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = 0f; 
        }
        RaycastHit hit;
        if (Physics.Raycast(_origin.position, _direction, out hit, _distance, _layerInteraction))
        {
            Debug.DrawRay(_origin.position, _direction * _distance, OnCollisionRay);
            canJump = true;
        }
        else
        {
            Debug.DrawRay(_origin.position, _direction * _distance, OnNotCollisionRay);
            canJump = false;
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.performed && canJump)
        {
            _rigidbody.AddForce(Vector3.up * jumoForce, ForceMode.Impulse);
        }
    }
    private void PlayFootStep()
    {
        if (sfxPlayer != null && audioData != null)
        {
            sfxPlayer.PlayClip(audioData.AudioClip, false);
        }
    }
}
