using System;
using UnityEngine;

// Token: 0x02000182 RID: 386
public class UINavReceiver : MonoBehaviour
{
	// Token: 0x0600104B RID: 4171 RVA: 0x00066750 File Offset: 0x00064950
	public bool ProcessMovement(bool left, bool right, bool up, bool down, bool isDigital)
	{
		if (!left && !right && !up && !down)
		{
			return false;
		}
		if (!isDigital && Time.realtimeSinceStartupAsDouble - this.lastInteractTime < 0.30000001192092896)
		{
			return false;
		}
		this.lastInteractTime = Time.realtimeSinceStartupAsDouble;
		if (left)
		{
			Action onLeft = this.OnLeft;
			if (onLeft != null)
			{
				onLeft();
			}
		}
		if (right)
		{
			Action onRight = this.OnRight;
			if (onRight != null)
			{
				onRight();
			}
		}
		if (up)
		{
			Action onUp = this.OnUp;
			if (onUp != null)
			{
				onUp();
			}
		}
		if (down)
		{
			Action onDown = this.OnDown;
			if (onDown != null)
			{
				onDown();
			}
		}
		return true;
	}

	// Token: 0x0600104C RID: 4172 RVA: 0x000667E6 File Offset: 0x000649E6
	public void SecondaryInput()
	{
		Action onSecondaryInput = this.OnSecondaryInput;
		if (onSecondaryInput == null)
		{
			return;
		}
		onSecondaryInput();
	}

	// Token: 0x0600104D RID: 4173 RVA: 0x000667F8 File Offset: 0x000649F8
	public UINavReceiver()
	{
	}

	// Token: 0x04000E7C RID: 3708
	public Action OnUp;

	// Token: 0x04000E7D RID: 3709
	public Action OnDown;

	// Token: 0x04000E7E RID: 3710
	public Action OnLeft;

	// Token: 0x04000E7F RID: 3711
	public Action OnRight;

	// Token: 0x04000E80 RID: 3712
	public Action OnSecondaryInput;

	// Token: 0x04000E81 RID: 3713
	private double lastInteractTime;
}
