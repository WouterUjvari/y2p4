using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMovement : MonoBehaviour {

	public void SwitchWalkingType()
	{
		if(VRPlayerMovementManager.instance.movementType == VRPlayerMovementManager.MovementType.Teleportation)
		{
			VRPlayerMovementManager.instance.movementType = VRPlayerMovementManager.MovementType.TouchpadWalking;
		}
		else
		{
			VRPlayerMovementManager.instance.movementType = VRPlayerMovementManager.MovementType.Teleportation;
		}
	}
}
