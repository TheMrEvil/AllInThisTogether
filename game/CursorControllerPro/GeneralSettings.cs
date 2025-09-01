using System;
using UnityEngine;

// Token: 0x02000003 RID: 3
[CreateAssetMenu(fileName = "GeneralSettings", menuName = "Data/Cursor Controller Settings", order = 336)]
public class GeneralSettings : ScriptableObject
{
	// Token: 0x06000004 RID: 4 RVA: 0x000020EA File Offset: 0x000002EA
	public GeneralSettings()
	{
	}

	// Token: 0x04000002 RID: 2
	[Tooltip("The cursor x-Axis movement speed according to Input Manager sensitivity")]
	[Range(1f, 50f)]
	public float horizontalSpeed;

	// Token: 0x04000003 RID: 3
	[Tooltip("The cursor y-Axis movement speed according to Input Manager sensitivity")]
	[Range(1f, 50f)]
	public float verticalSpeed;

	// Token: 0x04000004 RID: 4
	[Space]
	[Tooltip("The speed of the cursor when hovering over a button")]
	[Range(0.1f, 1f)]
	public float hoverMultiplier = 0.33f;

	// Token: 0x04000005 RID: 5
	[Tooltip("The size of the cursor")]
	[Range(0.5f, 1.5f)]
	public float cursorScale = 1f;

	// Token: 0x04000006 RID: 6
	[Tooltip("The value of joystick movement that is needed until the joystick registers input detection.")]
	[Range(0f, 1f)]
	public float deadZone = 0.1f;

	// Token: 0x04000007 RID: 7
	[Range(1f, 250f)]
	[Tooltip("The farthest distance that the cursor will interact with a Canvas (designed for World Space UI)")]
	public float maxDetectionDistance = 50f;

	// Token: 0x04000008 RID: 8
	[HideInInspector]
	public float tempHspeed;

	// Token: 0x04000009 RID: 9
	[HideInInspector]
	public float tempVspeed;

	// Token: 0x0400000A RID: 10
	[Tooltip("Similar to Depth Layer of a Camera. Setting the Z offset will allow you to control how close to the rendering camera the cursor is. This is helpful in stopping the cursor from rendering behind 3D objects in a scene.")]
	public float cursorZOffset = 500f;
}
