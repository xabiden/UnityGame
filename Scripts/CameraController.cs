using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField]
    private float speed = 2.0F;

    [SerializeField]

    private Transform target;

    private void Awake()
    {
        if (!target) target = FindObjectOfType<Character>().transform;
    }

    private void Update()
    {
        if (!target) target = FindObjectOfType<Monster>().transform; //если мёртв игрок камера фокусируется на другого юнита
        Vector3 position = target.position;
        position.z = - 10.0F;
        transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);
    }
}
