using System;
using UnityEngine;

// Token: 0x020000A0 RID: 160
public class VelocityFollow : MonoBehaviour
{
	// Token: 0x0600077F RID: 1919 RVA: 0x00036219 File Offset: 0x00034419
	private void Awake()
	{
		this.offset = base.transform.localPosition;
		this.lastPos = this.VelocityReference.position;
		this.baseRotation = base.transform.localRotation;
	}

	// Token: 0x06000780 RID: 1920 RVA: 0x0003624E File Offset: 0x0003444E
	private void LateUpdate()
	{
		if (this.UpdateType == ChestFollow.UpdateType.LateUpdate)
		{
			this.UpdatePosition(Time.deltaTime);
		}
	}

	// Token: 0x06000781 RID: 1921 RVA: 0x00036264 File Offset: 0x00034464
	private void Update()
	{
		if (this.UpdateType == ChestFollow.UpdateType.Update)
		{
			this.UpdatePosition(Time.deltaTime);
		}
	}

	// Token: 0x06000782 RID: 1922 RVA: 0x00036279 File Offset: 0x00034479
	private void FixedUpdate()
	{
		if (this.UpdateType == ChestFollow.UpdateType.FixedUpdate)
		{
			this.UpdatePosition(Time.fixedDeltaTime);
		}
	}

	// Token: 0x06000783 RID: 1923 RVA: 0x00036290 File Offset: 0x00034490
	private void UpdatePosition(float delta)
	{
		Vector3 position = this.VelocityReference.position;
		Vector3 direction = (position - this.lastPos) / delta;
		direction.y *= this.VerticalWeight;
		direction = direction.normalized * Mathf.Clamp(direction.magnitude, 0f, this.maxOffset);
		Vector3 b = this.offset - base.transform.parent.InverseTransformDirection(direction);
		base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, b, this.DelaySpeed * delta);
		if (this.keepRotation)
		{
			base.transform.localRotation = Quaternion.Lerp(base.transform.localRotation, this.baseRotation, delta * this.keepRotationSpeed);
		}
		this.lastPos = position;
	}

	// Token: 0x06000784 RID: 1924 RVA: 0x0003636A File Offset: 0x0003456A
	public VelocityFollow()
	{
	}

	// Token: 0x04000634 RID: 1588
	[Range(0.1f, 8f)]
	public float DelaySpeed = 1f;

	// Token: 0x04000635 RID: 1589
	[Range(0f, 1f)]
	public float VerticalWeight;

	// Token: 0x04000636 RID: 1590
	public ChestFollow.UpdateType UpdateType;

	// Token: 0x04000637 RID: 1591
	public Transform VelocityReference;

	// Token: 0x04000638 RID: 1592
	public float maxOffset = 1f;

	// Token: 0x04000639 RID: 1593
	public bool keepRotation = true;

	// Token: 0x0400063A RID: 1594
	public float keepRotationSpeed = 3f;

	// Token: 0x0400063B RID: 1595
	private Vector3 offset;

	// Token: 0x0400063C RID: 1596
	private Quaternion baseRotation;

	// Token: 0x0400063D RID: 1597
	private Vector3 lastPos;
}
