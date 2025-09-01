using System;
using UnityEngine;

// Token: 0x0200009F RID: 159
public class SpringFollow : MonoBehaviour
{
	// Token: 0x0600077C RID: 1916 RVA: 0x00036079 File Offset: 0x00034279
	private void Awake()
	{
		this.lastTargetPos = this.target.position;
	}

	// Token: 0x0600077D RID: 1917 RVA: 0x0003608C File Offset: 0x0003428C
	private void LateUpdate()
	{
		if (Time.timeScale == 0f)
		{
			return;
		}
		Vector3 vector = base.transform.parent.InverseTransformDirection(this.targetVel.normalized);
		this.wantPos = this.target.localPosition;
		if (this.targetVel.magnitude > 0f)
		{
			this.wantPos = this.target.localPosition - vector.normalized * this.Offset * this.VelOffsetCurve.Evaluate(this.targetVel.magnitude);
		}
		base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, this.wantPos, Time.deltaTime * this.LerpSpeed);
		this.targetVel = (this.target.position - this.lastTargetPos) / Time.deltaTime;
		this.lastTargetPos = this.target.position;
		Quaternion rotation = this.target.rotation;
		Quaternion quaternion = Quaternion.Lerp(this.lastTargetRot, rotation, Time.deltaTime * this.RotLerpSpeed);
		float num = Quaternion.Angle(quaternion, rotation);
		if (num > this.MaximumAngleOffset)
		{
			quaternion = Quaternion.RotateTowards(quaternion, rotation, num - this.MaximumAngleOffset);
		}
		base.transform.rotation = quaternion;
		this.lastTargetRot = quaternion;
	}

	// Token: 0x0600077E RID: 1918 RVA: 0x000361E5 File Offset: 0x000343E5
	public SpringFollow()
	{
	}

	// Token: 0x0400062A RID: 1578
	public float Offset = 0.5f;

	// Token: 0x0400062B RID: 1579
	public AnimationCurve VelOffsetCurve;

	// Token: 0x0400062C RID: 1580
	public float LerpSpeed = 6f;

	// Token: 0x0400062D RID: 1581
	public float MaximumAngleOffset = 15f;

	// Token: 0x0400062E RID: 1582
	public float RotLerpSpeed = 6f;

	// Token: 0x0400062F RID: 1583
	public Transform target;

	// Token: 0x04000630 RID: 1584
	private Vector3 targetVel;

	// Token: 0x04000631 RID: 1585
	private Vector3 lastTargetPos;

	// Token: 0x04000632 RID: 1586
	private Vector3 wantPos;

	// Token: 0x04000633 RID: 1587
	private Quaternion lastTargetRot;
}
