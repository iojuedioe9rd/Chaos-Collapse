using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {

        public float speed = 6f;

        public Rigidbody rb;
        public Transform cam;

        public float turnSmoothTime = .1f;
        float turnSmoothVel;

        public Transform jumpPos;

        bool Jumping;
        public float maxJumpTime = 10;
        public float minJumpTime = 1;
        public float jump = 1;
        public float maxJump = 100;
        public float minJump = 1;
        float jumpTime;

        float t = 0;

        // Start is called before the first frame update
        void Start()
        {
            print("hi");
        }

        // Update is called once per frame
        void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 dir = new Vector3(horizontal, vertical).normalized;

            jumpTime = Mathf.Lerp(minJumpTime, maxJumpTime, t);
            t += Time.deltaTime;
            print( Mathf.Lerp(minJump, maxJump, jumpTime));
            

            if (dir.magnitude >= .1f)
            {
                float Angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, Angle, ref turnSmoothVel, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0, angle, 0);

                
                Vector3 moveDir = Quaternion.Euler(0f, Angle, 0f) * Vector3.forward;
                if (Input.GetKeyDown(KeyCode.Space) && isJumping())
                {
                    Vector3 v = new Vector3(0, 1000, 0);
                    rb.AddForce(v * Time.deltaTime);

                    moveDir.y = v.y;
                }
                rb.MovePosition(transform.position + (speed * Time.deltaTime * (moveDir.normalized)));
            }
        }

        bool isJumping()
        {
            if (Physics.Raycast(jumpPos.position, Vector3.down, out RaycastHit hit, 5f))
            {
                return true;
            }

            return false;
        }
    }
}
