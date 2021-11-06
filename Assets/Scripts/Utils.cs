using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Direction : byte
{
	NONE = 0, UP = 1, DOWN = 2, LEFT = 3, RIGHT = 4
}

public class CollisonSide
{

	public static Direction ColSide(BoxCollider2D current, BoxCollider2D other)
    {
        if(!current.IsTouching(other))
        {
            return Direction.NONE;
        }
        var thisMin = (Vector2)current.bounds.min;
        var thisMax = (Vector2)current.bounds.max;
        var otherMin = (Vector2)other.bounds.min;
        var otherMax = (Vector2)other.bounds.max;
        var otherMaxYDiff = Mathf.Abs(otherMax.y - thisMin.y);
        var otherMinYDiff = Mathf.Abs(otherMin.y - thisMax.y);
        var otherMaxXDiff = Mathf.Abs(otherMax.x - thisMin.x);
        var otherMinXDiff = Mathf.Abs(otherMin.x - thisMax.x);
        var min = Mathf.Min(otherMaxYDiff, Mathf.Min(otherMinYDiff, Mathf.Min(otherMaxXDiff, otherMinXDiff)));
		if (min == otherMaxYDiff)
		{
			return Direction.DOWN;
		}
		else if (min == otherMinYDiff)
		{
			return Direction.UP;
		}
		else if (min == otherMaxXDiff)
		{
			return Direction.LEFT;
		}
		else //otherMinXDiff
		{
			return Direction.RIGHT;
		}
	}
};

