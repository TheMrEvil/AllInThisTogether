using System;
using UnityEngine;

// Token: 0x02000098 RID: 152
public class BookFollow : MonoBehaviour
{
	// Token: 0x170000A9 RID: 169
	// (get) Token: 0x06000742 RID: 1858 RVA: 0x00034666 File Offset: 0x00032866
	private Vector3 camScale
	{
		get
		{
			return this.scaleMult * Vector3.one * (this.baseScale / base.transform.parent.lossyScale.x);
		}
	}

	// Token: 0x06000743 RID: 1859 RVA: 0x0003469C File Offset: 0x0003289C
	private void Awake()
	{
		this.lastTargetPos = this.target.position;
		this.baseScale = base.transform.parent.lossyScale.x;
		this.scaleMult = base.transform.localScale.x;
		this.playerRef = base.GetComponentInParent<PlayerControl>();
	}

	// Token: 0x06000744 RID: 1860 RVA: 0x000346F8 File Offset: 0x000328F8
	public void SetCameraFollowDistance(Vector3 offset)
	{
		float aspect = PlayerControl.MyCamera.aspect;
		float num = 1.7777778f;
		float num2 = aspect / num;
		offset.x *= num2;
		this.cameraOpenTarget.localPosition = offset;
	}

	// Token: 0x06000745 RID: 1861 RVA: 0x00034730 File Offset: 0x00032930
	private void LateUpdate()
	{
		if (this.FollowCamera)
		{
			this.DoFollowCamera();
			return;
		}
		if (this.playerRef != null && this.playerRef.IsDead)
		{
			this.DoDeadFollow();
			return;
		}
		this.DoFollowNormal();
	}

	// Token: 0x06000746 RID: 1862 RVA: 0x0003476C File Offset: 0x0003296C
	private void DoFollowCamera()
	{
		if (this.cFT < 1f)
		{
			this.cFT += Time.deltaTime * this.LerpSpeed * 0.66f;
		}
		base.transform.position = Vector3.Lerp(this.target.position, this.cameraOpenTarget.position, this.cameraLerpCurve.Evaluate(this.cFT));
		base.transform.rotation = Quaternion.Lerp(base.transform.rotation, this.cameraOpenTarget.rotation, Time.deltaTime * this.LerpSpeed * 2f);
		base.transform.localScale = Vector3.Lerp(base.transform.localScale, this.camScale, Time.deltaTime * this.LerpSpeed * 4f);
	}

	// Token: 0x06000747 RID: 1863 RVA: 0x00034848 File Offset: 0x00032A48
	private void DoDeadFollow()
	{
		base.transform.position = Vector3.Lerp(base.transform.position, this.deadTarget.position, Time.deltaTime * this.LerpSpeed);
		base.transform.rotation = Quaternion.Lerp(base.transform.rotation, this.deadTarget.rotation, Time.deltaTime * this.LerpSpeed * 2f);
		base.transform.localScale = Vector3.Lerp(base.transform.localScale, Vector3.one * this.scaleMult, Time.deltaTime * this.LerpSpeed * 4f);
	}

	// Token: 0x06000748 RID: 1864 RVA: 0x000348FC File Offset: 0x00032AFC
	private void DoFollowNormal()
	{
		if (Time.timeScale == 0f)
		{
			return;
		}
		if (this.cFT > 0f)
		{
			this.cFT -= Time.deltaTime * this.LerpSpeed * 2f;
		}
		Vector3 vector = base.transform.parent.InverseTransformDirection(this.targetVel.normalized);
		this.wantPos = this.target.localPosition;
		if (this.targetVel.magnitude > 0f)
		{
			this.wantPos = this.target.localPosition - vector.normalized * this.Offset * this.VelOffsetCurve.Evaluate(this.targetVel.magnitude);
		}
		base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, this.wantPos, Time.deltaTime * this.LerpSpeed);
		base.transform.localRotation = Quaternion.Lerp(base.transform.localRotation, this.target.localRotation, Time.deltaTime * this.LerpSpeed);
		base.transform.localScale = Vector3.Lerp(base.transform.localScale, Vector3.one * this.scaleMult, Time.deltaTime * this.LerpSpeed * 4f);
		this.targetVel = (this.target.position - this.lastTargetPos) / Time.deltaTime;
		this.lastTargetPos = this.target.position;
	}

	// Token: 0x06000749 RID: 1865 RVA: 0x00034A97 File Offset: 0x00032C97
	public void BookParticleVortex(Vector3 pos)
	{
		this.VortexParticles.transform.position = pos;
		this.VortexParticles.Play();
	}

	// Token: 0x0600074A RID: 1866 RVA: 0x00034AB5 File Offset: 0x00032CB5
	public BookFollow()
	{
	}

	// Token: 0x040005D2 RID: 1490
	public float Offset = 0.5f;

	// Token: 0x040005D3 RID: 1491
	public AnimationCurve VelOffsetCurve;

	// Token: 0x040005D4 RID: 1492
	public float LerpSpeed = 6f;

	// Token: 0x040005D5 RID: 1493
	private PlayerControl playerRef;

	// Token: 0x040005D6 RID: 1494
	public Transform target;

	// Token: 0x040005D7 RID: 1495
	public Transform cameraOpenTarget;

	// Token: 0x040005D8 RID: 1496
	public Transform deadTarget;

	// Token: 0x040005D9 RID: 1497
	public AnimationCurve cameraLerpCurve;

	// Token: 0x040005DA RID: 1498
	public bool FollowCamera;

	// Token: 0x040005DB RID: 1499
	public ParticleSystem VortexParticles;

	// Token: 0x040005DC RID: 1500
	private Vector3 targetVel;

	// Token: 0x040005DD RID: 1501
	private Vector3 lastTargetPos;

	// Token: 0x040005DE RID: 1502
	private Vector3 wantPos;

	// Token: 0x040005DF RID: 1503
	private float cFT;

	// Token: 0x040005E0 RID: 1504
	private float baseScale = 1f;

	// Token: 0x040005E1 RID: 1505
	private float scaleMult = 1f;
}
