using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public GameObject character;
    private Animator charAnim;

    void Start()
    {
        charAnim = character.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown("b"))
        {
            charAnim.Play("");
        }
    }
}
