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

            Vector3 dir = new Vector3(horizontal, 0, vertical).normalized;
            if (dir.magnitude >= .1f)
            {
                float Angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, Angle, ref turnSmoothVel, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0, angle, 0);

                Vector3 moveDir = Quaternion.Euler(0f, Angle, 0f) * Vector3.forward;
                rb.MovePosition(transform.position + (speed * Time.deltaTime * moveDir.normalized));
            }
        }
    }
}
