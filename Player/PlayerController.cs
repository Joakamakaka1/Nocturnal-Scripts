using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : Singleton <PlayerController>, IShop
{
    [SerializeField] private float moveSpeed = 1f;
    public float damageMultiplier = 0f;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRender;
    private KnockBack knockback;

    private bool facingLeft = false;
    private CinemachineVirtualCamera virtualCamera;


    protected override void Awake()
    {
        base.Awake();

        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRender = GetComponent<SpriteRenderer>();
        knockback = GetComponent<KnockBack>();
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        DontDestroyOnLoad(gameObject);
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }
    public bool IsFacingLeft()
    {
        return facingLeft;
    }

    /*
    private void OnDisable()
    {
        playerControls.Disable();
    }
    */
    private void Update()
    {
        if (!PauseMenu.instance.isPaused)
        {
            PlayerInput();
        }
    }

    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }

    private void Move()
    {
        if (knockback.GettingKnockedBack || PlayerVida.Instance.isDead) { return; }

        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerScreenPoint.x)
        {
            mySpriteRender.flipX = true;
            facingLeft = true;
        }
        else
        {
            mySpriteRender.flipX = false;
            facingLeft = false;
        }
    }

    private void LateUpdate()
    {
        if (virtualCamera != null)
        {
            virtualCamera.Follow = transform;
        }
    }

    public void BoughtItem(Item.ItemType type)
    {
        switch (type)
        {
            case Item.ItemType.Armor_1:
                moveSpeed += 1f; 
                break;
            case Item.ItemType.Potion_1:
                PlayerVida.Instance.IncreaseMaxHealth();
                PlayerVida.Instance.HealPlayer();
                break;
            case Item.ItemType.Buff_1:
                damageMultiplier += 2f;
                break;
            case Item.ItemType.Buff_2:
                damageMultiplier += 3f;
                break;
            default:
                Debug.LogError("Tipo de objeto no reconocido: " + type);
                break;
        }
    }

    public bool TrySpendGold(int goldAmount)
    {
        if (EconomyManager.Instance.CurrentGold >= goldAmount)
        {   
            EconomyManager.Instance.UpdateCurrentGold(EconomyManager.Instance.CurrentGold - goldAmount);
            return true;
        }
        else
        {
            Debug.Log("No tienes suficiente dinero");
            return false;           
        }
    }
}

