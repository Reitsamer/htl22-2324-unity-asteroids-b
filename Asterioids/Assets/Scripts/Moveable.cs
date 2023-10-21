using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour
{
    protected float Left => Camera.main.ViewportToWorldPoint(Vector3.zero).x;
    protected float Right => Camera.main.ViewportToWorldPoint(Vector3.right).x;
    protected float Top => Camera.main.ViewportToWorldPoint(Vector3.up).y;
    protected float Bottom => Camera.main.ViewportToWorldPoint(Vector3.zero).y;

}
