using UnityEngine;

public static class Utils
{
    public static Vector3 GroundPosition(Vector3 position, float castLenght = 10f)
    {
        if (Physics.Raycast(position, Vector3.down, out RaycastHit hit, castLenght))
        {
            return new Vector3(position.x, hit.point.y, position.z);
        }
        return position;
    }
}
