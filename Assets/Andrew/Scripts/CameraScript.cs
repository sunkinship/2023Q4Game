﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour { 
    public GameObject player;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void LateUpdate() {
        transform.position = player.transform.position + offset;
    }
}

