using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loopable : MonoBehaviour
{
    public static float Left
    => Camera.main.ViewportToWorldPoint(Vector3.zero).x;
    public static float Right
        => Camera.main.ViewportToWorldPoint(Vector3.right).x;
    public static float Top
        => Camera.main.ViewportToWorldPoint(Vector3.up).y;
    public static float Bottom
        => Camera.main.ViewportToWorldPoint(Vector3.zero).y;

    protected bool CorrectPosition(Renderer renderer)
    {
        Vector3 size = renderer.bounds.size / 2;

        var pos = transform.position;

        if (pos.x < Left - size.x - 0.1)
            pos.x = Right + size.x;
        if (pos.x > Right + size.x + 0.1)
            pos.x = Left - size.x;

        if (pos.y > Top + size.y + 0.1)
            pos.y = Bottom - size.y;
        if (pos.y < Bottom - size.y - 0.1)
            pos.y = Top + size.y;

        bool res = (transform.position != pos);
        transform.position = pos;
        return res;
    }

    protected void CheckOutOfBoundsDeath(Renderer renderer)
    {
        Vector3 size = renderer.bounds.size / 2;
        var pos = transform.position;

        if (pos.x < Left - size.x - 0.1)
            Destroy(gameObject);
        if (pos.x > Right + size.x + 0.1)
            Destroy(gameObject);

        if (pos.y > Top + size.y + 0.1)
            Destroy(gameObject);
        if (pos.y < Bottom - size.y - 0.1)
            Destroy(gameObject);
    }

}
