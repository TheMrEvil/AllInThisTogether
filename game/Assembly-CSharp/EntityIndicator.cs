using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000195 RID: 405
public class EntityIndicator : Indicatable
{
	// Token: 0x1700013A RID: 314
	// (get) Token: 0x06001107 RID: 4359 RVA: 0x00069D85 File Offset: 0x00067F85
	public override Transform Root
	{
		get
		{
			if (this.entity != null && this.UseEntityCenter)
			{
				return this.entity.display.CenterOfMass;
			}
			return base.transform;
		}
	}

	// Token: 0x06001108 RID: 4360 RVA: 0x00069DB4 File Offset: 0x00067FB4
	private void Start()
	{
		this.entity = base.GetComponentInParent<EntityControl>();
		if (this.entity == null)
		{
			LockFollow component = base.GetComponent<LockFollow>();
			if (component != null)
			{
				this.entity = component.entityFollow;
			}
		}
		if (this.entity != null)
		{
			this.hadEntity = true;
		}
		WorldIndicators.Indicate(this);
		this.didInit = true;
	}

	// Token: 0x06001109 RID: 4361 RVA: 0x00069E19 File Offset: 0x00068019
	private void OnEnable()
	{
		if (this.didInit)
		{
			if (this.hadEntity)
			{
				base.StartCoroutine(this.CheckEntityDelayed());
			}
			WorldIndicators.Indicate(this);
		}
		this.overrideOff = false;
	}

	// Token: 0x0600110A RID: 4362 RVA: 0x00069E45 File Offset: 0x00068045
	private IEnumerator CheckEntityDelayed()
	{
		yield return true;
		LockFollow component = base.GetComponent<LockFollow>();
		if (component != null)
		{
			this.entity = component.entityFollow;
		}
		this.hadEntity = (this.entity != null);
		yield break;
	}

	// Token: 0x0600110B RID: 4363 RVA: 0x00069E54 File Offset: 0x00068054
	public void Deactivate()
	{
		this.overrideOff = true;
	}

	// Token: 0x0600110C RID: 4364 RVA: 0x00069E60 File Offset: 0x00068060
	public override bool ShouldIndicate()
	{
		if (!base.ShouldIndicate())
		{
			return false;
		}
		bool flag = (!this.hadEntity || (this.entity != null && !this.entity.IsDead)) && !this.overrideOff;
		if (this.HideIfLOS && this.entity != null)
		{
			flag &= !PlayerControl.CanSeeEntity(this.entity);
		}
		if (this.DistanceLimits.x > 0f || this.DistanceLimits.y < 250f)
		{
			float num = Vector3.Distance(base.transform.position, PlayerControl.myInstance.Movement.GetPosition());
			flag &= (num > this.DistanceLimits.x);
			flag &= (num < this.DistanceLimits.y);
		}
		return flag;
	}

	// Token: 0x0600110D RID: 4365 RVA: 0x00069F35 File Offset: 0x00068135
	private void OnDisable()
	{
		this.overrideOff = true;
		WorldIndicators.ReleaseIndicator(this);
	}

	// Token: 0x0600110E RID: 4366 RVA: 0x00069F44 File Offset: 0x00068144
	public EntityIndicator()
	{
	}

	// Token: 0x04000F67 RID: 3943
	private EntityControl entity;

	// Token: 0x04000F68 RID: 3944
	public bool UseEntityCenter;

	// Token: 0x04000F69 RID: 3945
	public bool HideIfLOS;

	// Token: 0x04000F6A RID: 3946
	public Vector2 DistanceLimits = new Vector2(0f, 250f);

	// Token: 0x04000F6B RID: 3947
	private bool overrideOff;

	// Token: 0x04000F6C RID: 3948
	private bool hadEntity;

	// Token: 0x04000F6D RID: 3949
	private bool didInit;

	// Token: 0x02000566 RID: 1382
	[CompilerGenerated]
	private sealed class <CheckEntityDelayed>d__11 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060024C1 RID: 9409 RVA: 0x000CF51B File Offset: 0x000CD71B
		[DebuggerHidden]
		public <CheckEntityDelayed>d__11(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060024C2 RID: 9410 RVA: 0x000CF52A File Offset: 0x000CD72A
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060024C3 RID: 9411 RVA: 0x000CF52C File Offset: 0x000CD72C
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			EntityIndicator entityIndicator = this;
			if (num == 0)
			{
				this.<>1__state = -1;
				this.<>2__current = true;
				this.<>1__state = 1;
				return true;
			}
			if (num != 1)
			{
				return false;
			}
			this.<>1__state = -1;
			LockFollow component = entityIndicator.GetComponent<LockFollow>();
			if (component != null)
			{
				entityIndicator.entity = component.entityFollow;
			}
			entityIndicator.hadEntity = (entityIndicator.entity != null);
			return false;
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x060024C4 RID: 9412 RVA: 0x000CF5A2 File Offset: 0x000CD7A2
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060024C5 RID: 9413 RVA: 0x000CF5AA File Offset: 0x000CD7AA
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x060024C6 RID: 9414 RVA: 0x000CF5B1 File Offset: 0x000CD7B1
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002706 RID: 9990
		private int <>1__state;

		// Token: 0x04002707 RID: 9991
		private object <>2__current;

		// Token: 0x04002708 RID: 9992
		public EntityIndicator <>4__this;
	}
}
