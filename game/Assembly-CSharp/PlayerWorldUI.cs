using System;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000096 RID: 150
public class PlayerWorldUI : MonoBehaviour
{
	// Token: 0x170000A8 RID: 168
	// (get) Token: 0x06000737 RID: 1847 RVA: 0x00034268 File Offset: 0x00032468
	// (set) Token: 0x06000738 RID: 1848 RVA: 0x00034270 File Offset: 0x00032470
	public PlayerControl Player
	{
		[CompilerGenerated]
		get
		{
			return this.<Player>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			this.<Player>k__BackingField = value;
		}
	}

	// Token: 0x06000739 RID: 1849 RVA: 0x00034279 File Offset: 0x00032479
	private void Start()
	{
		this.Player = base.GetComponentInParent<PlayerControl>();
		WorldIndicators.Indicate(this);
		this.localPos = base.transform.localPosition;
		base.InvokeRepeating("UpdatePos", 0.5f, 0.5f);
	}

	// Token: 0x0600073A RID: 1850 RVA: 0x000342B4 File Offset: 0x000324B4
	private void UpdatePos()
	{
		if (this.Player.IsDead)
		{
			base.transform.position = this.Player.Health.Ghost.UIRoot.position;
			return;
		}
		base.transform.localPosition = this.localPos;
	}

	// Token: 0x0600073B RID: 1851 RVA: 0x00034308 File Offset: 0x00032508
	public bool ShouldShowName()
	{
		if (PlayerControl.MyCamera == null)
		{
			return false;
		}
		Vector3 toCamera = base.transform.position - PlayerControl.MyCamera.transform.position;
		return this.InAimArea(toCamera);
	}

	// Token: 0x0600073C RID: 1852 RVA: 0x0003434C File Offset: 0x0003254C
	private bool InAimArea(Vector3 toCamera)
	{
		Vector3 forward = PlayerControl.MyCamera.transform.forward;
		return Vector3.Dot(toCamera.normalized, forward) > this.DotDistanceCurve.Evaluate(toCamera.magnitude);
	}

	// Token: 0x0600073D RID: 1853 RVA: 0x0003438A File Offset: 0x0003258A
	private void OnDestroy()
	{
		WorldIndicators.ReleaseIndicator(this);
	}

	// Token: 0x0600073E RID: 1854 RVA: 0x00034392 File Offset: 0x00032592
	public PlayerWorldUI()
	{
	}

	// Token: 0x040005CA RID: 1482
	public AnimationCurve DotDistanceCurve;

	// Token: 0x040005CB RID: 1483
	[CompilerGenerated]
	private PlayerControl <Player>k__BackingField;

	// Token: 0x040005CC RID: 1484
	private Vector3 localPos;
}
