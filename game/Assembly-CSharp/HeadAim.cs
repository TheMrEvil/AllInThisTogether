using System;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x0200009D RID: 157
public class HeadAim : MonoBehaviour
{
	// Token: 0x0600076D RID: 1901 RVA: 0x0003575F File Offset: 0x0003395F
	private void Awake()
	{
		this.Control = base.GetComponentInParent<PlayerControl>();
		PlayerActions actions = this.Control.actions;
		actions.abilityActivated = (Action<int, Vector3, Vector3, EffectProperties>)Delegate.Combine(actions.abilityActivated, new Action<int, Vector3, Vector3, EffectProperties>(delegate(int <p0>, Vector3 <p1>, Vector3 <p2>, EffectProperties <p3>)
		{
			this.AbilityActivated();
		}));
	}

	// Token: 0x0600076E RID: 1902 RVA: 0x0003579C File Offset: 0x0003399C
	private void LateUpdate()
	{
		this.InertiaRotate();
		if (!this.localControl)
		{
			base.transform.rotation = Quaternion.Lerp(base.transform.rotation, this.baseRot, Time.deltaTime * 9f);
			this.fullRotate.localRotation = Quaternion.Lerp(this.fullRotate.localRotation, this.fullRot, Time.deltaTime * 9f);
			return;
		}
		if (!this.ShouldRotate())
		{
			return;
		}
		Vector3 forward = this.cameraRef.transform.forward;
		Vector3 vector = Vector3.ProjectOnPlane(this.cameraRef.transform.forward, Vector3.up);
		if (vector == Vector3.zero)
		{
			return;
		}
		Quaternion quaternion = Quaternion.LookRotation(vector, Vector3.up);
		Quaternion quaternion2 = Quaternion.LookRotation(forward, Vector3.up);
		if (this.Control.Display.PlayerAnims.speed >= 0.95f && this.controlSnapDelay <= 0f)
		{
			base.transform.rotation = quaternion;
			this.fullRotate.rotation = quaternion2;
		}
		else
		{
			base.transform.rotation = Quaternion.Lerp(base.transform.rotation, quaternion, Time.deltaTime * 12f);
			this.fullRotate.rotation = Quaternion.Lerp(this.fullRotate.rotation, quaternion2, Time.deltaTime * 12f);
			this.controlSnapDelay -= Time.deltaTime;
		}
		Vector3 localEulerAngles = this.fullRotate.localEulerAngles;
		if (localEulerAngles.x > 180f)
		{
			localEulerAngles.x -= 360f;
		}
		localEulerAngles.x = Mathf.Clamp(localEulerAngles.x, -this.MaxVertAngle, this.MaxVertAngle);
		this.fullRotate.localEulerAngles = localEulerAngles;
		this.baseRot = base.transform.rotation;
		this.fullRot = this.fullRotate.localRotation;
	}

	// Token: 0x0600076F RID: 1903 RVA: 0x00035990 File Offset: 0x00033B90
	public void SetForwardExplicit(Vector3 forward)
	{
		if (forward == Vector3.zero)
		{
			return;
		}
		Quaternion rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(forward, Vector3.up), Vector3.up);
		Quaternion rotation2 = Quaternion.LookRotation(forward, Vector3.up);
		base.transform.rotation = rotation;
		this.fullRotate.rotation = rotation2;
	}

	// Token: 0x06000770 RID: 1904 RVA: 0x000359E5 File Offset: 0x00033BE5
	public void RequestForceRotate()
	{
		this.ForceRotateFrame = true;
	}

	// Token: 0x06000771 RID: 1905 RVA: 0x000359F0 File Offset: 0x00033BF0
	private void InertiaRotate()
	{
		this.planarVel = this.Control.Movement.GetPlanarVelocity();
		this.accel = (this.planarVel - this.lastVel) / Time.deltaTime;
		this.lastVel = this.planarVel;
		Vector3 rhs = base.transform.InverseTransformDirection(this.planarVel.normalized);
		Vector3 rhs2 = base.transform.InverseTransformDirection(this.accel);
		Vector3 normalized = Vector3.Cross(Vector3.up, rhs).normalized;
		float num = rhs.magnitude * this.BaseLeanScalingFactor;
		Vector3 normalized2 = Vector3.Cross(Vector3.up, rhs2).normalized;
		float value = rhs2.magnitude * this.InertiaScalingFactor;
		float angle = num + Mathf.Clamp(value, -this.MaxInertiaRot, this.MaxInertiaRot);
		Vector3 normalized3 = (normalized + normalized2).normalized;
		Quaternion b = Quaternion.AngleAxis(angle, normalized3);
		if (Time.timeScale > 0f)
		{
			this.InertiaRot.localRotation = Quaternion.Lerp(this.InertiaRot.localRotation, b, Time.deltaTime * 2f);
		}
	}

	// Token: 0x06000772 RID: 1906 RVA: 0x00035B18 File Offset: 0x00033D18
	private bool ShouldRotate()
	{
		if (this.ForceRotateFrame)
		{
			this.ForceRotateFrame = false;
			return true;
		}
		if (this.Control.Display.DisplayMoveSpeed == 0f)
		{
			return false;
		}
		if (!this.Control.Display.CanRotateDisplay())
		{
			return false;
		}
		if (GameplayManager.CurState == GameState.InWave)
		{
			return true;
		}
		if (this.Control.Movement.input.movementAxis.magnitude > 0f)
		{
			return true;
		}
		if (this.Control.IsUsingActiveAbility())
		{
			return true;
		}
		if (this.abilityUsedDelay > 0f)
		{
			this.abilityUsedDelay -= Time.deltaTime;
			return true;
		}
		this.controlSnapDelay = 0.5f;
		return false;
	}

	// Token: 0x06000773 RID: 1907 RVA: 0x00035BCC File Offset: 0x00033DCC
	private void AbilityActivated()
	{
		this.abilityUsedDelay = 0.5f;
	}

	// Token: 0x06000774 RID: 1908 RVA: 0x00035BD9 File Offset: 0x00033DD9
	public HeadAim()
	{
	}

	// Token: 0x06000775 RID: 1909 RVA: 0x00035C02 File Offset: 0x00033E02
	[CompilerGenerated]
	private void <Awake>b__14_0(int <p0>, Vector3 <p1>, Vector3 <p2>, EffectProperties <p3>)
	{
		this.AbilityActivated();
	}

	// Token: 0x0400060A RID: 1546
	public bool localControl;

	// Token: 0x0400060B RID: 1547
	public Camera cameraRef;

	// Token: 0x0400060C RID: 1548
	public Transform fullRotate;

	// Token: 0x0400060D RID: 1549
	[Header("Inertia Rotation")]
	public Transform InertiaRot;

	// Token: 0x0400060E RID: 1550
	public float BaseLeanScalingFactor = 1f;

	// Token: 0x0400060F RID: 1551
	public float InertiaScalingFactor = 1f;

	// Token: 0x04000610 RID: 1552
	public float MaxInertiaRot = 30f;

	// Token: 0x04000611 RID: 1553
	public float MaxVertAngle;

	// Token: 0x04000612 RID: 1554
	public Quaternion baseRot;

	// Token: 0x04000613 RID: 1555
	public Quaternion fullRot;

	// Token: 0x04000614 RID: 1556
	private PlayerControl Control;

	// Token: 0x04000615 RID: 1557
	private float controlSnapDelay;

	// Token: 0x04000616 RID: 1558
	private float abilityUsedDelay;

	// Token: 0x04000617 RID: 1559
	private bool ForceRotateFrame;

	// Token: 0x04000618 RID: 1560
	private Vector3 planarVel;

	// Token: 0x04000619 RID: 1561
	private Vector3 lastVel;

	// Token: 0x0400061A RID: 1562
	private Vector3 accel;
}
