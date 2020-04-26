﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController> {

    public int xPos, yPos;

    private void Start() {
        xPos = 0; // Floor number
        yPos = 1; // Room number

        summonPlayer();
    }

    public void summonPlayer() {
        transform.position = GameController.Instance.roomPosition[xPos, yPos].position;
    }

    private void Update() {
        // Esto se puede mejorar, molaria sacar la llamada a MovePosition y hacerla común
        if (Input.GetKeyDown(KeyCode.W) && xPos < GameController.Instance.numFloors - 1 && yPos == 1) {
            xPos++;
            movePosition();
        } else if (Input.GetKeyDown(KeyCode.S) && xPos > 0 && yPos == 1) {
            xPos--;
            movePosition();
        } else if (Input.GetKeyDown(KeyCode.D) && yPos < GameController.Instance.numRooms - 1) {
            yPos++;
            movePosition();
        } else if (Input.GetKeyDown(KeyCode.A) && yPos > 0) {
            yPos--;
            movePosition();
        }

    }

    private void movePosition() {
        this.transform.position = GameController.Instance.roomPosition[xPos, yPos].position;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        // Cambiar por las zonas de las salas
        if (collision.CompareTag("Item")) {
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Heart")){
            Debug.Log("Te quito vida wey");
            PlayerStats.Instance.performDamage(collision.gameObject.GetComponent<ItemDrop>().damage);
            Destroy(collision.gameObject);
        }
    }
}
