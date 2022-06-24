using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMechanics : MonoBehaviour
{
    public float moveSpeed;
    
    private Vector3 _moveVector;
    private float _gravityForce;
    private float _gravityConst = 9.81f;

    private CharacterController _charController;
    private Animator _animController;

    void Start()
    {
        _charController = GetComponent<CharacterController>();
        _animController = GetComponent<Animator>();
    }

    void Update()
    {
        CharacterMove();
        CharacterGravity();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CharacterHarvest();
        }
    }

    private void CharacterMove()
    {
        _moveVector = Vector3.zero;
        _moveVector.x = Input.GetAxis("Horizontal") * moveSpeed;
        _moveVector.z = Input.GetAxis("Vertical") * moveSpeed;


        //анимация движения
        if (_moveVector.x != 0 || _moveVector.z != 0)
            _animController.SetBool("Move", true);
        else   
            _animController.SetBool("Move", false);

        

        if (Vector3.Angle(Vector3.forward, _moveVector) > 1f ||
            Vector3.Angle(Vector3.forward, _moveVector) == 0f)
        {
            Vector3 direct = Vector3.RotateTowards(transform.forward, _moveVector, moveSpeed, 0f);
            transform.rotation = Quaternion.LookRotation(direct);
        }
        
        _moveVector.y = _gravityForce;

        _charController.Move(_moveVector * Time.deltaTime);
        
    }

    private void CharacterGravity()
    {
        if (!_charController.isGrounded)
            _gravityForce -= _gravityConst * Time.deltaTime;
        else
            _gravityForce = -1f;
    }


    private void CharacterHarvest()
    {
        _animController.SetTrigger("Harvest");
    }
}
