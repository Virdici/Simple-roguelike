﻿using System.Collections;
using UnityEngine;

public class GenerateNewLevel : MonoBehaviour
{
    public bool DidCollide = false;
    Generator generator;

    private void Awake()
    {
        generator = GameObject.FindObjectOfType<Generator>();
    }

    private void OnCollisionStay(Collision col)
    {
        //new generation on collision
        Debug.Log("fucking wot" + col.contacts[0].point);
        //StartCoroutine(Start());
        //generator.StartNewGeneration();
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        generator.StartNewGeneration();
    }
}
