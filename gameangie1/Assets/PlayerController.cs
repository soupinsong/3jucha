using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Camera main; // 메인 카메라
    private Transform player; // 플레이어 Transform
    private Vector3 offset = new Vector3(0f, 5f, -10f); // 카메라와 플레이어 사이 거리
    private float sensitivity = 5f; // 마우스 감도
    private float smoothSpeed = 0.125f; // 카메라 부드러운 이동 속도
    private float minYAngle = -60f; // 카메라 최소 Y 축 각도
    private float maxYAngle = 20f; // 카메라 최대 Y 축 각도
    private float currentRotationX = 0f; // 현재 X 축 회전 값 (수직 회전)
    private float currentRotationY = 0f; // 현재 Y 축 회전 값 (수평 회전)
    
    private float speed = 5f; // 플레이어 이동 속도
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        main = Camera.main;
        player = GetComponent<Transform>();
        
        main.transform.position = player.position + (player.forward * -10) + offset;
        main.transform.LookAt(player.position);
    }

    
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            player.position += player.forward * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            player.position += -1* player.forward * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            player.position += -1*player.right * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            player.position += player.right * speed * Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        currentRotationY += Input.GetAxis("Mouse X") * sensitivity;
        currentRotationX -= Input.GetAxis("Mouse Y") * sensitivity;

        currentRotationX = Mathf.Clamp(currentRotationX, minYAngle, maxYAngle);
        Quaternion playerRotation = Quaternion.Euler(0f, currentRotationY, 0f);
        player.rotation = playerRotation;
        
        Quaternion rotation = Quaternion.Euler(currentRotationX, currentRotationY, 0f);
        Vector3 desiredPosition = player.position + rotation * offset;
        main.transform.position = Vector3.Lerp(main.transform.position, desiredPosition, smoothSpeed);
        
        main.transform.LookAt(player.position);
    }
}
