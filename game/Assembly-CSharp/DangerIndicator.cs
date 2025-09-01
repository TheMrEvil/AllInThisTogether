using System;
using UnityEngine;

// Token: 0x0200009B RID: 155
public class DangerIndicator : MonoBehaviour
{
	// Token: 0x06000759 RID: 1881 RVA: 0x00035098 File Offset: 0x00033298
	private void Awake()
	{
		this._propBlock = new MaterialPropertyBlock();
	}

	// Token: 0x0600075A RID: 1882 RVA: 0x000350A8 File Offset: 0x000332A8
	public void Setup(ActionEffect actionRef)
	{
		this.LookTarget = actionRef.transform;
		this.action = actionRef;
		AreaOfEffect areaOfEffect = this.action as AreaOfEffect;
		if (areaOfEffect != null)
		{
			this.Danger = areaOfEffect.Danger;
		}
		this.Setup();
	}

	// Token: 0x0600075B RID: 1883 RVA: 0x000350E9 File Offset: 0x000332E9
	public void Setup(EntityControl entityRef, Ability abilityRef)
	{
		this.LookTarget = entityRef.display.CenterOfMass;
		this.entity = entityRef;
		this.ability = abilityRef;
		this.Danger = this.ability.props.Danger;
		this.Setup();
	}

	// Token: 0x0600075C RID: 1884 RVA: 0x00035128 File Offset: 0x00033328
	private void Setup()
	{
		MeshRenderer display = this.Display;
		Material sharedMaterial;
		switch (this.Danger)
		{
		case DangerIndicator.DangerLevel.Low:
			sharedMaterial = this.LowMat;
			break;
		case DangerIndicator.DangerLevel.Medium:
			sharedMaterial = this.MedMat;
			break;
		case DangerIndicator.DangerLevel.High:
			sharedMaterial = this.HighMat;
			break;
		default:
			sharedMaterial = this.LowMat;
			break;
		}
		display.sharedMaterial = sharedMaterial;
		base.transform.localPosition = new Vector3(0f, UnityEngine.Random.Range(0.24f, 0.26f), 0f);
		this._propBlock.SetFloat(DangerIndicator.Opacity, 0f);
		this.Display.SetPropertyBlock(this._propBlock);
	}

	// Token: 0x0600075D RID: 1885 RVA: 0x000351D4 File Offset: 0x000333D4
	public void TickUpdate()
	{
		this.UpdateDirection();
		this.UpdateOpacity();
		if (this.action != null)
		{
			this.CheckActionCompletion();
		}
		else if (this.entity != null && this.ability != null)
		{
			this.CheckEntityCompletion();
		}
		else
		{
			this.IsDone = true;
		}
		if (this.IsDone && this.opacity <= 0.05f)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x0600075E RID: 1886 RVA: 0x00035248 File Offset: 0x00033448
	private void UpdateDirection()
	{
		if (this.LookTarget == null)
		{
			return;
		}
		Vector3 position = base.transform.position;
		Vector3 vector = this.LookTarget.position - position;
		vector = Vector3.ProjectOnPlane(vector, Vector3.up);
		base.transform.LookAt(position + vector, Vector3.up);
	}

	// Token: 0x0600075F RID: 1887 RVA: 0x000352A8 File Offset: 0x000334A8
	private void UpdateOpacity()
	{
		this.t += Time.deltaTime / Mathf.Max(this.PulseDuration, 0.01f);
		if (this.t >= 1f)
		{
			this.t -= 1f;
		}
		float b = this.PulseCurve.Evaluate(this.t);
		if (this.IsDone)
		{
			b = 0f;
		}
		this.opacity = Mathf.Lerp(this.opacity, b, Time.deltaTime * 8f);
		this._propBlock.SetFloat(DangerIndicator.Opacity, this.opacity);
		this.Display.SetPropertyBlock(this._propBlock);
	}

	// Token: 0x06000760 RID: 1888 RVA: 0x0003535C File Offset: 0x0003355C
	private void CheckActionCompletion()
	{
		if (this.IsDone)
		{
			return;
		}
		float num = Vector3.Distance(base.transform.position, this.action.transform.position);
		if (this.action.isFinished || num > DangerIndicatorManager.AoEDangerDistance(this.action as AreaOfEffect))
		{
			this.IsDone = true;
		}
	}

	// Token: 0x06000761 RID: 1889 RVA: 0x000353BA File Offset: 0x000335BA
	private void CheckEntityCompletion()
	{
		if (this.IsDone)
		{
			return;
		}
		if (!this.ability.IsActive(true) || !this.ability.IsRunning())
		{
			this.IsDone = true;
		}
	}

	// Token: 0x06000762 RID: 1890 RVA: 0x000353E7 File Offset: 0x000335E7
	public DangerIndicator()
	{
	}

	// Token: 0x06000763 RID: 1891 RVA: 0x000353EF File Offset: 0x000335EF
	// Note: this type is marked as 'beforefieldinit'.
	static DangerIndicator()
	{
	}

	// Token: 0x040005F7 RID: 1527
	public MeshRenderer Display;

	// Token: 0x040005F8 RID: 1528
	private MaterialPropertyBlock _propBlock;

	// Token: 0x040005F9 RID: 1529
	public Material LowMat;

	// Token: 0x040005FA RID: 1530
	public Material MedMat;

	// Token: 0x040005FB RID: 1531
	public Material HighMat;

	// Token: 0x040005FC RID: 1532
	public float PulseDuration;

	// Token: 0x040005FD RID: 1533
	public AnimationCurve PulseCurve;

	// Token: 0x040005FE RID: 1534
	private float opacity;

	// Token: 0x040005FF RID: 1535
	private float t;

	// Token: 0x04000600 RID: 1536
	private DangerIndicator.DangerLevel Danger;

	// Token: 0x04000601 RID: 1537
	private Transform LookTarget;

	// Token: 0x04000602 RID: 1538
	[NonSerialized]
	public ActionEffect action;

	// Token: 0x04000603 RID: 1539
	[NonSerialized]
	public EntityControl entity;

	// Token: 0x04000604 RID: 1540
	private Ability ability;

	// Token: 0x04000605 RID: 1541
	private bool IsDone;

	// Token: 0x04000606 RID: 1542
	private static readonly int Opacity = Shader.PropertyToID("_Opacity");

	// Token: 0x020004AE RID: 1198
	public enum DangerLevel
	{
		// Token: 0x04002400 RID: 9216
		None,
		// Token: 0x04002401 RID: 9217
		Low,
		// Token: 0x04002402 RID: 9218
		Medium,
		// Token: 0x04002403 RID: 9219
		High
	}
}
