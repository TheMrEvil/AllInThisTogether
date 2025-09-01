using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x020000E3 RID: 227
public class PositionalInteraction : MonoBehaviour
{
	// Token: 0x170000DA RID: 218
	// (get) Token: 0x06000A12 RID: 2578 RVA: 0x000422F0 File Offset: 0x000404F0
	private float interactionDistance
	{
		get
		{
			return this.InteractDistance * (this.MultiplyScale ? base.transform.localScale.x : 1f);
		}
	}

	// Token: 0x170000DB RID: 219
	// (get) Token: 0x06000A13 RID: 2579 RVA: 0x00042318 File Offset: 0x00040518
	public bool IsInteracting
	{
		get
		{
			return PlayerControl.myInstance != null && this.PlayerInside && PlayerInput.myInstance.interactDownPressed && this.HoldTimer < this.InteractTime;
		}
	}

	// Token: 0x06000A14 RID: 2580 RVA: 0x0004234B File Offset: 0x0004054B
	internal virtual void Awake()
	{
		if (PositionalInteraction.Interactives == null || (PositionalInteraction.Interactives.Count > 0 && PositionalInteraction.Interactives[0] == null))
		{
			PositionalInteraction.Interactives = new List<PositionalInteraction>();
		}
	}

	// Token: 0x06000A15 RID: 2581 RVA: 0x0004237E File Offset: 0x0004057E
	private void Start()
	{
		PositionalInteraction.Interactives.Add(this);
	}

	// Token: 0x06000A16 RID: 2582 RVA: 0x0004238C File Offset: 0x0004058C
	internal virtual void Update()
	{
		if (PlayerControl.myInstance == null)
		{
			return;
		}
		if (PositionalInteraction.master == null)
		{
			PositionalInteraction.master = this;
		}
		if (PositionalInteraction.master == this)
		{
			this.CheckNearest();
		}
		if (this.PlayerInside && (PlayerInput.myInstance.interactDownPressed || (this.InteractTime > 0f && PlayerInput.myInstance.interactPressed)))
		{
			this.HoldTimer += Time.deltaTime;
			if (this.HoldTimer >= this.InteractTime)
			{
				this.TryInteract();
				return;
			}
		}
		else
		{
			this.HoldTimer = 0f;
		}
	}

	// Token: 0x06000A17 RID: 2583 RVA: 0x0004242C File Offset: 0x0004062C
	private void CheckNearest()
	{
		Vector3 plrPos = PlayerControl.myInstance.display.CenterOfMass.position;
		PositionalInteraction.Interactives.Sort((PositionalInteraction x, PositionalInteraction y) => Vector3.SqrMagnitude(plrPos - x.transform.position).CompareTo(Vector3.SqrMagnitude(plrPos - y.transform.position)));
		bool flag = false;
		foreach (PositionalInteraction positionalInteraction in PositionalInteraction.Interactives)
		{
			if (flag)
			{
				if (positionalInteraction.PlayerInside)
				{
					positionalInteraction.PlayerInside = false;
					positionalInteraction.OnExit();
				}
			}
			else
			{
				positionalInteraction.CheckInside();
				if (positionalInteraction.PlayerInside)
				{
					flag = true;
				}
			}
		}
	}

	// Token: 0x06000A18 RID: 2584 RVA: 0x000424DC File Offset: 0x000406DC
	internal virtual bool CanInteract()
	{
		return true;
	}

	// Token: 0x06000A19 RID: 2585 RVA: 0x000424E0 File Offset: 0x000406E0
	internal bool CheckInside()
	{
		if (PlayerControl.myInstance == null)
		{
			return false;
		}
		bool flag = Vector3.Distance(PlayerControl.myInstance.display.CenterOfMass.position, base.transform.position) <= this.interactionDistance;
		if (this.PlayerInside && !flag)
		{
			this.OnExit();
		}
		if (!this.PlayerInside && flag && this.CanInteract())
		{
			this.OnEnter();
		}
		return flag;
	}

	// Token: 0x06000A1A RID: 2586 RVA: 0x00042559 File Offset: 0x00040759
	private void TryInteract()
	{
		this.HoldTimer = 0f;
		if (!this.PlayerInside || (this.HasInteracted && this.OneShot))
		{
			return;
		}
		this.OnInteract();
	}

	// Token: 0x06000A1B RID: 2587 RVA: 0x00042585 File Offset: 0x00040785
	internal virtual void OnEnter()
	{
		this.HasInteracted = false;
		this.PlayerInside = true;
	}

	// Token: 0x06000A1C RID: 2588 RVA: 0x00042595 File Offset: 0x00040795
	internal virtual void OnInteract()
	{
		this.HasInteracted = true;
	}

	// Token: 0x06000A1D RID: 2589 RVA: 0x0004259E File Offset: 0x0004079E
	internal virtual void OnExit()
	{
		this.HasInteracted = false;
		this.PlayerInside = false;
	}

	// Token: 0x06000A1E RID: 2590 RVA: 0x000425AE File Offset: 0x000407AE
	public virtual void OnDrawGizmos()
	{
	}

	// Token: 0x06000A1F RID: 2591 RVA: 0x000425B0 File Offset: 0x000407B0
	private void OnDestroy()
	{
		PositionalInteraction.Interactives.Remove(this);
		if (this.PlayerInside)
		{
			this.OnExit();
		}
	}

	// Token: 0x06000A20 RID: 2592 RVA: 0x000425CC File Offset: 0x000407CC
	public PositionalInteraction()
	{
	}

	// Token: 0x06000A21 RID: 2593 RVA: 0x000425DF File Offset: 0x000407DF
	// Note: this type is marked as 'beforefieldinit'.
	static PositionalInteraction()
	{
	}

	// Token: 0x0400089A RID: 2202
	public float InteractDistance = 10f;

	// Token: 0x0400089B RID: 2203
	public bool MultiplyScale;

	// Token: 0x0400089C RID: 2204
	public bool OneShot;

	// Token: 0x0400089D RID: 2205
	public float InteractTime;

	// Token: 0x0400089E RID: 2206
	public bool PlayerInside;

	// Token: 0x0400089F RID: 2207
	public float HoldTimer;

	// Token: 0x040008A0 RID: 2208
	public bool HasInteracted;

	// Token: 0x040008A1 RID: 2209
	private static List<PositionalInteraction> Interactives = new List<PositionalInteraction>();

	// Token: 0x040008A2 RID: 2210
	private static PositionalInteraction master = null;

	// Token: 0x020004D5 RID: 1237
	[CompilerGenerated]
	private sealed class <>c__DisplayClass16_0
	{
		// Token: 0x06002302 RID: 8962 RVA: 0x000C83F5 File Offset: 0x000C65F5
		public <>c__DisplayClass16_0()
		{
		}

		// Token: 0x06002303 RID: 8963 RVA: 0x000C8400 File Offset: 0x000C6600
		internal int <CheckNearest>b__0(PositionalInteraction x, PositionalInteraction y)
		{
			return Vector3.SqrMagnitude(this.plrPos - x.transform.position).CompareTo(Vector3.SqrMagnitude(this.plrPos - y.transform.position));
		}

		// Token: 0x0400247F RID: 9343
		public Vector3 plrPos;
	}
}
