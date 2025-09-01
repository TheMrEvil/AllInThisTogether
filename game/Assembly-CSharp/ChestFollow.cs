using System;
using UnityEngine;

// Token: 0x0200009A RID: 154
public class ChestFollow : MonoBehaviour
{
	// Token: 0x06000752 RID: 1874 RVA: 0x00034E3C File Offset: 0x0003303C
	private void Start()
	{
		this.down = Vector3.down;
		this.lastRot = base.transform.rotation;
		this.weightPoint = this.headFollow.position + this.down * this.torsoHeight;
	}

	// Token: 0x06000753 RID: 1875 RVA: 0x00034E8C File Offset: 0x0003308C
	private void Update()
	{
		if (this.updateType == ChestFollow.UpdateType.Update)
		{
			this.UpdateRotation();
		}
	}

	// Token: 0x06000754 RID: 1876 RVA: 0x00034E9C File Offset: 0x0003309C
	private void LateUpdate()
	{
		if (this.updateType == ChestFollow.UpdateType.LateUpdate)
		{
			this.UpdateRotation();
		}
	}

	// Token: 0x06000755 RID: 1877 RVA: 0x00034EAD File Offset: 0x000330AD
	private void FixedUpdate()
	{
		if (this.updateType == ChestFollow.UpdateType.FixedUpdate)
		{
			this.UpdateRotation();
		}
	}

	// Token: 0x06000756 RID: 1878 RVA: 0x00034EC0 File Offset: 0x000330C0
	private void UpdateRotation()
	{
		float num = Time.unscaledDeltaTime;
		if (this.updateType == ChestFollow.UpdateType.FixedUpdate)
		{
			num = Time.fixedDeltaTime;
		}
		Vector3 upwards = -this.headFollow.forward;
		Vector3 up = this.headFollow.up;
		if (this.onPlane)
		{
			upwards = Vector3.ProjectOnPlane(-this.headFollow.forward, Vector3.up);
			up = Vector3.up;
		}
		Quaternion quaternion = Quaternion.LookRotation(this.headFollow.position - this.weightPoint, upwards) * Quaternion.Euler(this.modelRotationFix);
		float num2 = Quaternion.Angle(base.transform.rotation, quaternion);
		if (num2 <= this.rotationAngleSwap)
		{
			base.transform.rotation = Quaternion.Lerp(base.transform.rotation, quaternion, this.rotationSpeed * num);
		}
		else
		{
			base.transform.rotation = Quaternion.RotateTowards(base.transform.rotation, quaternion, num2 - this.rotationAngleSwap);
		}
		this.lastRot = base.transform.rotation;
		Vector3 b = this.headFollow.position + up * -this.torsoHeight;
		this.weightPoint = Vector3.Lerp(this.weightPoint, b, this.weight * num);
	}

	// Token: 0x06000757 RID: 1879 RVA: 0x00035008 File Offset: 0x00033208
	public void ResetChestWeightTarget()
	{
		this.weightPoint = this.headFollow.position + this.down * this.torsoHeight;
	}

	// Token: 0x06000758 RID: 1880 RVA: 0x00035034 File Offset: 0x00033234
	public ChestFollow()
	{
	}

	// Token: 0x040005EB RID: 1515
	public float rotationAngleSwap = 15f;

	// Token: 0x040005EC RID: 1516
	public float rotationSpeed = 2f;

	// Token: 0x040005ED RID: 1517
	public float maxRotationSpeed = 720f;

	// Token: 0x040005EE RID: 1518
	public float weight = 4f;

	// Token: 0x040005EF RID: 1519
	public float torsoHeight = 0.3f;

	// Token: 0x040005F0 RID: 1520
	public bool onPlane;

	// Token: 0x040005F1 RID: 1521
	[Tooltip("For models that are facing the wrong diretion, like blender models. Set to 0,0,0 if not sure.")]
	public Vector3 modelRotationFix = new Vector3(90f, 0f, 0f);

	// Token: 0x040005F2 RID: 1522
	private Quaternion lastRot;

	// Token: 0x040005F3 RID: 1523
	private Vector3 weightPoint;

	// Token: 0x040005F4 RID: 1524
	public Transform headFollow;

	// Token: 0x040005F5 RID: 1525
	private Vector3 down;

	// Token: 0x040005F6 RID: 1526
	public ChestFollow.UpdateType updateType;

	// Token: 0x020004AD RID: 1197
	public enum UpdateType
	{
		// Token: 0x040023FC RID: 9212
		Update,
		// Token: 0x040023FD RID: 9213
		LateUpdate,
		// Token: 0x040023FE RID: 9214
		FixedUpdate
	}
}
