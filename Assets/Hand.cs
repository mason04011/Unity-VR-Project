using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hand : MonoBehaviour
{
    [SerializeField] private InputActionReference controllerGrab;
    [SerializeField] private GameObject hand;
    [SerializeField] private Transform itemSlot;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Item item;
    [SerializeField] private GameObject itemToGrab;
    [SerializeField] private float grabRange = 3f;
    [SerializeField] private bool isHandVisible = true;
    [SerializeField] private bool isHoldingItem = false;
    [SerializeField] private Color lineCol = Color.white;
    [SerializeField] private RaycastHit hit;

    void Start()
    {
        lineRenderer = transform.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;
        lineRenderer.material.color = lineCol;
    }

    private void OnEnable()
    {
        controllerGrab.action.performed += Pickup;
        controllerGrab.action.canceled += Drop;
    }

    private void OnDisable()
    {
        controllerGrab.action.performed -= Pickup;
        controllerGrab.action.canceled -= Drop;
    }

    void Update()
    {
        HandVisible();
        
        lineRenderer.SetPosition(0, hand.transform.position);
        lineRenderer.SetPosition(1, transform.position + transform.forward * grabRange);
        lineRenderer.material.color = lineCol;
        
        Debug.DrawLine(transform.position, transform.position + transform.forward * grabRange, lineCol);
        
        if (Physics.Raycast(transform.position, transform.forward, out hit, grabRange))
        {
            if (hit.transform.gameObject.CompareTag("Item"))
            {
                itemToGrab = hit.transform.gameObject;
                lineCol = Color.blue;
            }
            else
            {   lineCol = Color.white;
                itemToGrab = null;
            }
        }
    }

    public void Pickup(InputAction.CallbackContext context)
    {
        Debug.Log("Hello");
        if (itemToGrab != null)
        {
            Rigidbody itemrb = itemToGrab.GetComponent<Rigidbody>();
            itemToGrab.transform.parent = itemSlot;
            itemToGrab.transform.localPosition = Vector3.zero;
            itemrb.useGravity = false;
            isHoldingItem = true;
            lineRenderer.enabled = false;
        }
    }
    
    public void Drop(InputAction.CallbackContext context)
    {
        Debug.Log("bye");
        if (isHoldingItem  && itemToGrab != null)
        {
            Rigidbody itemrb = itemToGrab.GetComponent<Rigidbody>();
            itemToGrab.transform.parent = null;
            itemrb.useGravity = true;
            isHoldingItem = false;
            itemToGrab = null;
            lineRenderer.enabled = true;
        }
    }

    public void HandVisible()
    {
        if (isHoldingItem)
        {
            isHandVisible = false;
            hand.SetActive(false);
        }
        else
        {
            isHandVisible = true;
            hand.SetActive(true);
        }
    }
}