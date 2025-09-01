using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020000E2 RID: 226
public class PlayerTrigger : MonoBehaviour
{
	// Token: 0x06000A0A RID: 2570 RVA: 0x0004220A File Offset: 0x0004040A
	private void Awake()
	{
		this.col = base.GetComponent<Collider>();
	}

	// Token: 0x06000A0B RID: 2571 RVA: 0x00042218 File Offset: 0x00040418
	public void Reset()
	{
		this.didTrigger = false;
		if (this.col != null)
		{
			this.col.enabled = true;
		}
	}

	// Token: 0x06000A0C RID: 2572 RVA: 0x0004223B File Offset: 0x0004043B
	public void Deactivate()
	{
		this.col.enabled = false;
	}

	// Token: 0x06000A0D RID: 2573 RVA: 0x00042249 File Offset: 0x00040449
	private void OnEnable()
	{
		this.col.enabled = true;
	}

	// Token: 0x06000A0E RID: 2574 RVA: 0x00042258 File Offset: 0x00040458
	private void OnTriggerEnter(Collider other)
	{
		if (this.didTrigger)
		{
			return;
		}
		PlayerControl componentInParent = other.GetComponentInParent<PlayerControl>();
		if (componentInParent == null)
		{
			return;
		}
		if (this.LocalOnly && !componentInParent.IsMine)
		{
			return;
		}
		this.OnEntered(componentInParent);
	}

	// Token: 0x06000A0F RID: 2575 RVA: 0x00042297 File Offset: 0x00040497
	internal virtual void OnEntered(PlayerControl plr)
	{
		Action<PlayerTrigger, PlayerControl> onEnter = this.OnEnter;
		if (onEnter != null)
		{
			onEnter(this, plr);
		}
		this.Trigger();
	}

	// Token: 0x06000A10 RID: 2576 RVA: 0x000422B2 File Offset: 0x000404B2
	public virtual void Trigger()
	{
		if (this.didTrigger)
		{
			return;
		}
		UnityEvent enterEvents = this.EnterEvents;
		if (enterEvents != null)
		{
			enterEvents.Invoke();
		}
		this.col.enabled = false;
		this.didTrigger = true;
	}

	// Token: 0x06000A11 RID: 2577 RVA: 0x000422E1 File Offset: 0x000404E1
	public PlayerTrigger()
	{
	}

	// Token: 0x04000895 RID: 2197
	private Collider col;

	// Token: 0x04000896 RID: 2198
	public bool LocalOnly = true;

	// Token: 0x04000897 RID: 2199
	public Action<PlayerTrigger, PlayerControl> OnEnter;

	// Token: 0x04000898 RID: 2200
	public UnityEvent EnterEvents;

	// Token: 0x04000899 RID: 2201
	internal bool didTrigger;
}
