using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePositionSortingOrder : MonoBehaviour
{
    [SerializeField] private bool runOnce;
    [SerializeField] private float posOffsetY;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void LateUpdate()
    {
        float positionMultiplier = 5f;
        spriteRenderer.sortingOrder = (int)(-(transform.position.y + posOffsetY) * positionMultiplier);
        if (runOnce)
            Destroy(this);
    }
}
