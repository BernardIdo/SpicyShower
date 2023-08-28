using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalAnimator : MonoBehaviour
{
    public Animator animator;
    public string movementParameterName;
    [HideInInspector]
    public bool isMoving;
    [HideInInspector]
    public bool facingLeft;

    private float _scale;
    
    // Start is called before the first frame update
    void Start()
    {
        _scale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool(movementParameterName, isMoving);
        var scaleXSign = facingLeft ? 1 : -1;
        transform.localScale = new Vector3(scaleXSign * _scale, _scale, _scale);
    }
}
