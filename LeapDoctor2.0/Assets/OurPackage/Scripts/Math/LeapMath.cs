namespace LeapMath
{
    using UnityEngine;

    public class LeapMath
    {
        public static Vector3 CalculateDeltaV(Vector3 old, Vector3 now)
        {
            return new Vector3(Mathf.Abs(now.x - old.x),
                               Mathf.Abs(now.y - old.y),
                               Mathf.Abs(now.z - old.z));
        }
        public static Quaternion CalculateDeltaQ(Quaternion old, Quaternion now)
        {
            return new Quaternion(Mathf.Abs(now.x - old.x),
                                  Mathf.Abs(now.y - old.y),
                                  Mathf.Abs(now.z - old.z),
                                  Mathf.Abs(now.w - old.w));
        }
    }
}
