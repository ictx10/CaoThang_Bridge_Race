using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private float speed = 5f;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsState(GameState.Gameplay) && Input.GetMouseButton(0))
        {
            Vector3 nextPoint = JoystickController.direct * speed * Time.deltaTime + transform.position;
            if (CanMove(nextPoint))
            {
                transform.position = CheckGround(nextPoint);
            }
            if (JoystickController.direct != Vector3.zero)
            {
                skin.forward = JoystickController.direct;
            }

            ChangeAnim("run");
        }
        if (Input.GetMouseButtonUp(0)) 
        { 
            ChangeAnim("idle");
        }
    }

   
}
