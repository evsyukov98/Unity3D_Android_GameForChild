﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager_3 : MonoBehaviour
{
    public List<ItemSlot_3> itemSlots;

    public Transform WinText;

    private void Update()
    {
        CatchAnimal();
    }

    private void Start()
    {
        SlotFill(itemSlots);
    }

    
    void SlotFill(List<ItemSlot_3> slots)
    {
        int randCorrectType = Random.Range(0, (int)ItemSlot_3.prefabType.MaxSize);

        int randWrongType = Random.Range(0, (int)ItemSlot_3.prefabType.MaxSize);

        int randSlot = Random.Range(0, slots.Count);

        while (randCorrectType == randWrongType)
        {
            randWrongType= Random.Range(0,(int)ItemSlot_3.prefabType.MaxSize);
        }

        for (int i = 0; i < slots.Count; i++)
        {
            if (randSlot == i)
            {
                slots[i].CreatePrefab((ItemSlot_3.prefabType)randWrongType,i);

                slots[i].currentPrefab.tag = "WrongAnimal";
                continue;
            }

            slots[i].CreatePrefab((ItemSlot_3.prefabType) randCorrectType, i);
        }
    }

    void CatchAnimal()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(ray, Vector3.back);

            if (hit.collider != null && hit.transform.tag == "WrongAnimal")
            {
                StartCoroutine(WinHandler());

            }
        }
    }

    IEnumerator WinHandler()
    {
        WinText.GetComponent<Text>().text = "Молодец ";

        yield return new WaitForSeconds(1);

        WinText.GetComponent<Text>().text = "";

        SceneManager.LoadScene(4);

        //SlotFill(itemSlots);

    }
}
