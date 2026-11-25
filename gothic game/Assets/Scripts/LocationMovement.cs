using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LocationMovement : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Location Navigation")]
    private GameObject currentLocation;

    [Tooltip("Drag the parent location folder you want to travel to")]
    [SerializeField] private GameObject targetLocation;

    [Header("Optional Lock State")]
    [SerializeField] private bool isLocked = false;
    [SerializeField] private string lockedMessage = "The door is locked.";

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private void Awake()
    {
        currentLocation = FindCurrentLocation();

        if (currentLocation == null)
        {
            Debug.LogError($"[{name}] Could not find any parent object starting with 'Location'!", this);
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    private GameObject FindCurrentLocation()
    {
        Transform current = transform.parent;

        while (current != null)
        {
            if (current.name.StartsWith("Location"))
            {
                return current.gameObject;
            }

            current = current.parent;
        }
        Debug.Log("Curr is " + current);
        return null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isLocked)
        {
            Debug.Log(lockedMessage); // Replace with your Inspection_Popup call later
            return;
        }

        MoveToTargetLocation();
    }

    public void MoveToTargetLocation()
    {
        if (currentLocation != null)
        {
            currentLocation.SetActive(false);
        }

        if (targetLocation != null)
        {
            targetLocation.SetActive(true);
        }
    }

    // Call this from your clue/key logic later to unlock a passage (e.g., Staircase Rear Door)
    public void UnlockDoor()
    {
        isLocked = false;
    }

    // Subtle visual feedback when hovering the mouse over the clickable box
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(originalColor.r * 1.2f, originalColor.g * 1.2f, originalColor.b * 1.2f, originalColor.a);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
        }
    }
}
