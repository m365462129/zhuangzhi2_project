using UnityEngine;
using System.Collections;
//using HedgehogTeam.EasyTouch;

public class ThirdPersonCamera : MonoBehaviour
{
    public static ThirdPersonCamera Instance;
    public float distanceAway;			// distance from the back of the craft
	public float distanceUp;			// distance above the craft
	public float smooth;				// how smooth the camera movement is
	
	private GameObject hovercraft;		// to store the hovercraft
	private Vector3 targetPosition;		// the position the camera is trying to be in
	
	public Transform follow;

    public bool IsLookAt = true;

    private void Awake()
    {
        Instance = this;
    }

    public void SetFollowTarget(Transform _tras)
    {
        //follow = _tras;
    }

    public void SetTargetPos(Vector3 v3Pos)
    {
        targetPosition = v3Pos;
    }

    void LateUpdate ()
	{
        //if (follow == null) return;
        //targetPosition = follow.position + Vector3.up * distanceUp - follow.forward * distanceAway;
        //transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);
        //if (IsLookAt)
        //{
        //    transform.LookAt(follow);
        //}

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);
    }
}
