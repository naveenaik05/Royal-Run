using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
   [SerializeField] Animator animator;

   [SerializeField] float moveSpeed = 5f;
   [SerializeField] float xClamp = 3f;
   [SerializeField] float zClamp = 3f;
   [SerializeField] float jumpHeight = 2f;
   [SerializeField] float jumpSpeed = 5f;

   Vector2 movement;
   Rigidbody rb;
   bool isJumping = false;
   const string groundString = "Ground";
   const string jumpString = "Jump";
   void Awake()
   {
      rb = GetComponent<Rigidbody>();
   }

   void FixedUpdate()
   {
      HandleMovement();
      // HandleGravity();
   }

   public void Move(InputAction.CallbackContext context)
   {
      movement = context.ReadValue<Vector2>();
   }

   public void Jump(InputAction.CallbackContext context)
   {
      if (context.performed && !isJumping)
      {
         StartCoroutine(JumpProcess());
      }
   }


   IEnumerator JumpProcess()
   {
      isJumping = true;

      animator.SetTrigger(jumpString);

      Vector3 startPos = transform.position;
      Vector3 peakPos = startPos + Vector3.up * jumpHeight;

      float elapsedTime = 0f;
      float duration = jumpHeight / jumpSpeed;


      while (elapsedTime < duration)
      {
         transform.position = Vector3.Lerp(startPos, peakPos, elapsedTime / duration);
         elapsedTime += Time.deltaTime;
         yield return null;
      }

      transform.position = peakPos;
      elapsedTime = 0f;

      while (elapsedTime < duration)
      {
         transform.position = Vector3.Lerp(peakPos, startPos, elapsedTime / duration);
         elapsedTime += Time.deltaTime;
         yield return null;
      }

      transform.position = startPos;
      isJumping = false;

   }



   void HandleMovement()
   {
      Vector3 currentPosition = rb.position;
      Vector3 moveDirection = new Vector3(movement.x, 0, movement.y);
      Vector3 newPosition = currentPosition + moveDirection * (moveSpeed * Time.fixedDeltaTime);

      newPosition.x = Mathf.Clamp(newPosition.x, -xClamp, xClamp);
      newPosition.z = Mathf.Clamp(newPosition.z, -zClamp, zClamp);

      rb.MovePosition(newPosition);
   }

}
