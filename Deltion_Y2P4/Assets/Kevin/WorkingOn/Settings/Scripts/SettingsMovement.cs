using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMovement : MonoBehaviour {


	public Toggle teleport;
	public Toggle smooth;

	public void SwitchWalkingType()
	{
		if(VRPlayerMovementManager.instance.movementType == VRPlayerMovementManager.MovementType.Teleportation)
		{
			VRPlayerMovementManager.instance.movementType = VRPlayerMovementManager.MovementType.TouchpadWalking;
			teleport.isOn = false;
			smooth.isOn = true;
		}
		else
		{
			VRPlayerMovementManager.instance.movementType = VRPlayerMovementManager.MovementType.Teleportation;
			teleport.isOn = true;
			smooth.isOn = false;
		}
	}
}
