using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x0200006D RID: 109
public class AIPhysicsMovement : AIMovement
{
	// Token: 0x0600041D RID: 1053 RVA: 0x000208A6 File Offset: 0x0001EAA6
	public override void Awake()
	{
		base.Awake();
		this.rb = base.gameObject.GetOrAddComponent<Rigidbody>();
	}

	// Token: 0x0600041E RID: 1054 RVA: 0x000208BF File Offset: 0x0001EABF
	public override void Setup()
	{
		base.Setup();
		EntityHealth health = base.Control.health;
		health.OnDamageTaken = (Action<DamageInfo>)Delegate.Combine(health.OnDamageTaken, new Action<DamageInfo>(this.OnDamageTaken));
	}

	// Token: 0x0600041F RID: 1055 RVA: 0x000208F3 File Offset: 0x0001EAF3
	private IEnumerator Start()
	{
		yield return true;
		this.rb.isKinematic = false;
		Collider[] componentsInChildren = base.GetComponentsInChildren<Collider>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].gameObject.layer = 0;
		}
		yield break;
	}

	// Token: 0x06000420 RID: 1056 RVA: 0x00020904 File Offset: 0x0001EB04
	private void FixedUpdate()
	{
		if (!this.controller.IsMine)
		{
			float num = Vector3.Distance(this.rb.transform.position, this.wantPosition);
			this.rb.transform.position = Vector3.MoveTowards(this.rb.transform.position, this.wantPosition, this.wantVel.magnitude * 1f * Time.deltaTime);
			this.rb.transform.position = Vector3.Lerp(this.rb.transform.position, this.wantPosition, Time.deltaTime * ((num < 25f) ? 0.5f : 8f));
			this.rb.velocity = Vector3.Lerp(this.rb.velocity, this.wantVel, Time.deltaTime * 9f);
		}
	}

	// Token: 0x06000421 RID: 1057 RVA: 0x000209F0 File Offset: 0x0001EBF0
	private void OnDamageTaken(DamageInfo info)
	{
		float amount = info.Amount;
		Vector3 atPoint = info.AtPoint;
		if (Vector3.Distance(base.Control.Display.CenterOfMass.position, atPoint) < 0.5f)
		{
			return;
		}
		this.rb.AddExplosionForce(amount, atPoint, 5f);
	}

	// Token: 0x06000422 RID: 1058 RVA: 0x00020A40 File Offset: 0x0001EC40
	public override void AddForce(ApplyForceNode node, Vector3 dir, EffectProperties props)
	{
		float forceValue = node.GetForceValue(props);
		node.VerticalAdd * Vector3.up * forceValue;
		this.rb.AddForce(dir * forceValue);
	}

	// Token: 0x06000423 RID: 1059 RVA: 0x00020A7E File Offset: 0x0001EC7E
	public override void UpdateFromNetwork(Vector3 pos, Quaternion rot, Vector3 vel)
	{
		base.UpdateFromNetwork(pos, rot, vel);
		if (Vector3.Distance(this.GetPosition(), pos) < 5f)
		{
			return;
		}
		this.SetPositionImmediate(pos, base.transform.forward, true);
	}

	// Token: 0x06000424 RID: 1060 RVA: 0x00020AB0 File Offset: 0x0001ECB0
	public override void SetPositionImmediate(Vector3 point, Vector3 forward, bool clearMomentum = true)
	{
		base.transform.position = point;
		if (!this.controller.IsUsingActiveAbility() && forward.magnitude > 0f)
		{
			base.transform.forward = forward;
		}
	}

	// Token: 0x06000425 RID: 1061 RVA: 0x00020AE5 File Offset: 0x0001ECE5
	public AIPhysicsMovement()
	{
	}

	// Token: 0x040003A4 RID: 932
	private Rigidbody rb;

	// Token: 0x0200048E RID: 1166
	[CompilerGenerated]
	private sealed class <Start>d__3 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060021E2 RID: 8674 RVA: 0x000C44CA File Offset: 0x000C26CA
		[DebuggerHidden]
		public <Start>d__3(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060021E3 RID: 8675 RVA: 0x000C44D9 File Offset: 0x000C26D9
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060021E4 RID: 8676 RVA: 0x000C44DC File Offset: 0x000C26DC
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			AIPhysicsMovement aiphysicsMovement = this;
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
			aiphysicsMovement.rb.isKinematic = false;
			Collider[] componentsInChildren = aiphysicsMovement.GetComponentsInChildren<Collider>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].gameObject.layer = 0;
			}
			return false;
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060021E5 RID: 8677 RVA: 0x000C4553 File Offset: 0x000C2753
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060021E6 RID: 8678 RVA: 0x000C455B File Offset: 0x000C275B
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060021E7 RID: 8679 RVA: 0x000C4562 File Offset: 0x000C2762
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002322 RID: 8994
		private int <>1__state;

		// Token: 0x04002323 RID: 8995
		private object <>2__current;

		// Token: 0x04002324 RID: 8996
		public AIPhysicsMovement <>4__this;
	}
}
