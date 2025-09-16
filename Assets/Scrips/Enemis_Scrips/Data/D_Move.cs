using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "D_Move", menuName = "Data/Enemies/D_Move")]
public class D_Move : ScriptableObject
{
    public float moveSpeed = 2f;
    public bool isGrounded;
}
