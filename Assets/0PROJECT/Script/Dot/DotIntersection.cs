using UnityEngine;

/// <summary>   
/// Class that controls the intersection of given points
/// </summary>

public class DotIntersection
{
    //Method for intersection check between given points
    public static bool AreLinesIntersecting(out Vector3 intersection, Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4)
    {
        // Initialize intersection point
        intersection = Vector3.zero;

        float offsetValue = 0.1f;

        // Add a small offset to points to prevent precision issues
        p1 += offsetValue * (p2 - p1).normalized;
        p2 += offsetValue * (p1 - p2).normalized;
        p3 += offsetValue * (p4 - p3).normalized;
        p4 += offsetValue * (p3 - p4).normalized;

        // Calculate directional vectors
        Vector3 dir1 = p2 - p1;
        Vector3 dir2 = p4 - p3;

        // Calculate coefficients for line equations
        float a1 = -dir1.y;
        float b1 = dir1.x;
        float d1 = -(a1 * p1.x + b1 * p1.y);

        float a2 = -dir2.y;
        float b2 = dir2.x;
        float d2 = -(a2 * p3.x + b2 * p3.y);

        // Calculate start and end points of line segment intersections
        float seg1_line2_start = a2 * p1.x + b2 * p1.y + d2;
        float seg1_line2_end = a2 * p2.x + b2 * p2.y + d2;

        float seg2_line1_start = a1 * p3.x + b1 * p3.y + d1;
        float seg2_line1_end = a1 * p4.x + b1 * p4.y + d1;

        // Check if line segments intersect
        if ((seg1_line2_start < 0 && seg1_line2_end > 0) || (seg1_line2_start > 0 && seg1_line2_end < 0))
        {
            if ((seg2_line1_start < 0 && seg2_line1_end > 0) || (seg2_line1_start > 0 && seg2_line1_end < 0))
            {
                // Calculate intersection point
                float u = seg1_line2_start / (seg1_line2_start - seg1_line2_end);
                intersection = p1 + u * dir1;
                return true;
            }
        }
        return false;
    }


}
