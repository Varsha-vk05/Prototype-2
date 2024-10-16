using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShapeShiftTrigger : MonoBehaviour
{

    public LayerMask Player;

    public float shapeshiftRange = 5f;           // Distance within which you can shapeshift
    public LayerMask NPC;                   // Layer for NPCs
    public float shapeshiftDuration = 10f;       // Time in seconds for how long the player stays shapeshifted

    private GameObject targetNPC;                // The NPC the player will shapeshift into
    private bool isShapeshifted = false;         // Track if the player is shapeshifted
    private float shapeshiftTimer = 0f;          // Timer for shapeshifting

    // Variables to store original player data
    private Mesh originalMesh;
    private Material[] originalMaterials;
    private RuntimeAnimatorController originalAnimatorController;
    private Avatar originalAvatar;

    private SkinnedMeshRenderer playerMeshRenderer;
    private Animator playerAnimator;

    public Slider staminaBar;                 //UI slider to display stamina bar

    void Start()
    {
        // Store the player's original mesh, materials, and AnimatorController
        playerMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        playerAnimator = GetComponent<Animator>();

        originalMesh = playerMeshRenderer.sharedMesh;
        originalMaterials = playerMeshRenderer.materials;
        originalAnimatorController = playerAnimator.runtimeAnimatorController;
        originalAvatar = playerAnimator.avatar;

        //To initialize the stamina bar with the maximun value and the current value
        if (staminaBar != null)
        {
            staminaBar.maxValue = shapeshiftDuration;
            staminaBar.value = shapeshiftDuration;
        }
    }

    void Update()
    {
        // Detect NPC when not shapeshifted
        if (!isShapeshifted)
        {
            DetectNPC();
        }

        // Press 'E' to shapeshift into NPC
        if (Input.GetKeyDown(KeyCode.E) && !isShapeshifted && targetNPC != null)
        {
            Shapeshift(targetNPC);
        }

        // Handle shapeshift timer
        if (isShapeshifted)
        {
            gameObject.layer = 8; 
            shapeshiftTimer -= Time.deltaTime;

            if (staminaBar != null)
            {
                staminaBar.value = shapeshiftTimer;      //this will update the stamina bar as the timer will decrease
            }

            // Automatically revert when the timer runs out
            if (shapeshiftTimer <= 0)
            {
                RevertToOriginalForm();
                gameObject.layer = 7;
            }
        }
    }

    void DetectNPC()
    {
        Collider[] npcsInRange = Physics.OverlapSphere(transform.position, shapeshiftRange, NPC);
        if (npcsInRange.Length > 0)
        {
            targetNPC = npcsInRange[0].gameObject;
        }
    }

    public void Shapeshift(GameObject npc)
    {
        SkinnedMeshRenderer npcMeshRenderer = npc.GetComponentInChildren<SkinnedMeshRenderer>();

        if (npcMeshRenderer != null)
        {
            // Swap player mesh and materials with NPC's
            playerMeshRenderer.sharedMesh = npcMeshRenderer.sharedMesh;
            playerMeshRenderer.materials = npcMeshRenderer.materials;

            // Swap the animation controller and avatar to match NPC
            Animator npcAnimator = npc.GetComponent<Animator>();

            if (npcAnimator != null)
            {
                playerAnimator.runtimeAnimatorController = npcAnimator.runtimeAnimatorController;
                playerAnimator.avatar = npcAnimator.avatar;
            }

            // Set the timer for the shapeshift duration
            shapeshiftTimer = shapeshiftDuration;
            isShapeshifted = true;
        }
    }

    void RevertToOriginalForm()
    {
        // Restore original player mesh, materials, and AnimatorController
        playerMeshRenderer.sharedMesh = originalMesh;
        playerMeshRenderer.materials = originalMaterials;
        playerAnimator.runtimeAnimatorController = originalAnimatorController;
        playerAnimator.avatar = originalAvatar;

        // Reset shapeshift status
        isShapeshifted = false;
    }
}