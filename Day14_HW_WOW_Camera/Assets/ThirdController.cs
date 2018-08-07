﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdController : MonoBehaviour
{    
    float gravity = 9.81f;      //중력
    float runSpeed = 5.0f;      //달리는 속도
    float mouseSensitivity = 2.0f;  //카메라 마우스 감도
    public float jumpForce = 250f;
    public float rotateSpeed = 10f;

    Transform myTransform;
    Transform model;
    //CharacterController cc;
    //Animator ani;
    Vector3 mouseMove;
    Vector3 move;
    Vector3 moveDirection = Vector3.zero;
    Rigidbody rb;
    Transform cameraParentTransform;
    Transform cameraTransform;
    private bool grounded = false;

    // Use this for initialization

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        myTransform = transform;
        //cc = GetComponent<CharacterController>();
        model = transform.GetChild(0);
        //ani = model.GetComponent<Animator>();
        cameraTransform = Camera.main.transform;
        cameraParentTransform = cameraTransform.parent;

    }

    // Update is called once per frame

    void Update()
    {        
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1 + 0.1f))
            grounded = true;
        else
            grounded = false;

        if (Input.GetButtonDown("Jump") && grounded)
        {
            //rb.velocity = false;
            rb.AddForce(transform.up * jumpForce);
        }

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Turn(h, v);
        moveDirection = (new Vector3(h, 0, v)).normalized;
        moveDirection *= runSpeed;

        Balance();
        CameraDistanceCtrl();
    }

    void LateUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            cameraParentTransform.position = myTransform.position + Vector3.up * 1.4f; //캐릭터의 머리 높이쯤
            mouseMove += new Vector3(-Input.GetAxisRaw("Mouse Y") * mouseSensitivity, Input.GetAxisRaw("Mouse X") * mouseSensitivity, 0); //마우스의 움직임을 가감

            if (mouseMove.x < -40)  //높이제한
                mouseMove.x = -40;
            else if (30 < mouseMove.x)
                mouseMove.x = 30;

            cameraParentTransform.localEulerAngles = mouseMove;
        }
    }

    private void FixedUpdate()
    {
        Vector3 move = transform.TransformDirection(moveDirection) * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);
    }

    void Balance()
    {
        if (myTransform.eulerAngles.x != 0 || myTransform.eulerAngles.z != 0)   //모종의 이유로 기울어진다면 바로잡는다.
            myTransform.eulerAngles = new Vector3(0, myTransform.eulerAngles.y, 0);
    }

    void CameraDistanceCtrl()
    {

        Camera.main.transform.localPosition += new Vector3(0, 0, Input.GetAxisRaw("Mouse ScrollWheel") * 2.0f); //휠로 카메라의 거리를 조절한다.

        if (-1 < Camera.main.transform.localPosition.z)
            Camera.main.transform.localPosition = new Vector3(Camera.main.transform.localPosition.x, Camera.main.transform.localPosition.y, -1); //최대로 가까운 수치
        else if (Camera.main.transform.localPosition.z < -5)
            Camera.main.transform.localPosition = new Vector3(Camera.main.transform.localPosition.x, Camera.main.transform.localPosition.y, -5); //최대로 먼 수치

    }

    //void MoveCalc(float ratio)
    //{

    //    float tempMoveY = move.y;
    //    move.y = 0; //이동에는 xz값만 필요하므로 잠시 저장하고 빼둔다.
    //    Vector3 inputMoveXZ = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

    //    float inputMoveXZMgnitude = inputMoveXZ.sqrMagnitude; //sqrMagnitude연산을 두 번 할 필요 없도록 따로 저장
    //    inputMoveXZ = myTransform.TransformDirection(inputMoveXZ);

    //    if (inputMoveXZMgnitude <= 1)
    //        inputMoveXZ *= runSpeed;
    //    else
    //        inputMoveXZ = inputMoveXZ.normalized * runSpeed;

    //    //조작 중에만 카메라의 방향에 상대적으로 캐릭터가 움직이도록 한다.

    //    if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
    //    {
    //        Quaternion cameraRotation = cameraParentTransform.rotation;
    //        cameraRotation.x = cameraRotation.z = 0;    //y축만 필요하므로 나머지 값은 0으로 바꾼다.
    //        //자연스러움을 위해 Slerp로 회전시킨다.
    //        myTransform.rotation = Quaternion.Slerp(myTransform.rotation, cameraRotation, 10.0f * Time.deltaTime);
    //        if (move != Vector3.zero)//Quaternion.LookRotation는 (0,0,0)이 들어가면 경고를 내므로 예외처리 해준다.
    //        {

    //            Quaternion characterRotation = Quaternion.LookRotation(move);
    //            characterRotation.x = characterRotation.z = 0;
    //            model.rotation = Quaternion.Slerp(model.rotation, characterRotation, 10.0f * Time.deltaTime);

    //        }

    //        //관성을 위해 MoveTowards를 활용하여 서서히 이동하도록 한다.
    //        move = Vector3.MoveTowards(move, inputMoveXZ, ratio * runSpeed);
    //    }
    //    else
    //    {
    //        //조작이 없으면 서서히 멈춘다.
    //        move = Vector3.MoveTowards(move, Vector3.zero, (1 - inputMoveXZMgnitude) * runSpeed * ratio);
    //    }

    //    float speed = move.sqrMagnitude;    //현재 속도를 애니메이터에 세팅한다.
    //    //ani.SetFloat("Speed", speed);
    //    move.y = tempMoveY; //y값 복구

    //}

    //void GradientCheck()
    //{
    //    //ani.SetBool("isGrounded", true);
    //    if (Physics.Raycast(myTransform.position, Vector3.down, 0.2f))

    //    //경사로를 구분하기 위해 밑으로 레이를 쏘아 땅을 확인한다.

    //    //CharacterController는 밑으로 지속적으로 Move가 일어나야 땅을 체크하는데 -y값이 너무 낮으면 조금만 경사져도 공중에 떠버리고 너무 높으면 절벽에서 떨어질때 추락하듯 바로 떨어진다.

    //    //완벽하진 않지만 캡슐 모양의 CharacterController에서 절벽에 떨어지기 직전엔 중앙에서 밑으로 쏘아지는 레이에 아무것도 닿지 않으므로 그때만 -y값을 낮추면 경사로에도 잘 다니고

    //    //절벽에도 자연스럽게 천천히 떨어지는 효과를 줄 수 있다.
    //    {
    //        move.y = -5;
    //    }
    //    else
    //        move.y = -1;

    //}

    void Turn(float h, float v)
    {
        if (h == 0 && v == 0)
            return;
        Quaternion qt = Quaternion.LookRotation(moveDirection);
        rb.rotation = Quaternion.Slerp(rb.rotation, qt, rotateSpeed * Time.deltaTime);
        rb.MoveRotation(qt);

    }

}
