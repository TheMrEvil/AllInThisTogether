using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Photon.Pun;
using UnityEngine;

// Token: 0x02000070 RID: 112
public class AITrivialControl : AIControl
{
	// Token: 0x0600042D RID: 1069 RVA: 0x00020D90 File Offset: 0x0001EF90
	public override void Setup()
	{
		if (this.didSetup)
		{
			return;
		}
		this.net.Setup();
		this.display.Setup();
		this.movement.Setup();
		this.health.Setup();
		this.audio.Setup();
		base.InitialTimers();
		if (this.behaviourTree != null)
		{
			base.ChangeBrain(this.behaviourTree);
		}
		EntityHealth health = this.health;
		health.OnDamageTaken = (Action<DamageInfo>)Delegate.Combine(health.OnDamageTaken, new Action<DamageInfo>(base.OnDamaged));
		EntityHealth health2 = this.health;
		health2.OnDie = (Action<DamageInfo>)Delegate.Combine(health2.OnDie, new Action<DamageInfo>(this.OnDeath));
		base.DoAISpawnEvents();
		this.didSetup = true;
	}

	// Token: 0x0600042E RID: 1070 RVA: 0x00020E58 File Offset: 0x0001F058
	public override void Update()
	{
		if (base.IsDead)
		{
			return;
		}
		base.UpdateStatuses();
		base.UpdateTimers();
	}

	// Token: 0x0600042F RID: 1071 RVA: 0x00020E6F File Offset: 0x0001F06F
	private void TryBrainEvents(EventTrigger trigger)
	{
		if (this.runtimeTree == null)
		{
			return;
		}
		if (PhotonNetwork.InRoom && !this.net.view.IsMine)
		{
			return;
		}
		this.runtimeTree.TriggerEvents(trigger, this);
	}

	// Token: 0x06000430 RID: 1072 RVA: 0x00020EA8 File Offset: 0x0001F0A8
	public override float GetPassiveMod(Passive.EntityValue passive, float startVal)
	{
		return startVal;
	}

	// Token: 0x06000431 RID: 1073 RVA: 0x00020EAB File Offset: 0x0001F0AB
	public override float GetPassiveMod(EntityPassive passive, float startVal)
	{
		return startVal;
	}

	// Token: 0x06000432 RID: 1074 RVA: 0x00020EAE File Offset: 0x0001F0AE
	public override float GetPassiveMod(EntityPassive passive, EffectProperties props, float startVal)
	{
		return startVal;
	}

	// Token: 0x06000433 RID: 1075 RVA: 0x00020EB1 File Offset: 0x0001F0B1
	public override float ModifyDamageTaken(EffectProperties props, float DamageAmount)
	{
		return DamageAmount;
	}

	// Token: 0x06000434 RID: 1076 RVA: 0x00020EB4 File Offset: 0x0001F0B4
	public override void TriggerSnippets(EventTrigger trigger, EffectProperties props = null, float chanceMult = 1f)
	{
		this.TryBrainEvents(trigger);
	}

	// Token: 0x06000435 RID: 1077 RVA: 0x00020EC0 File Offset: 0x0001F0C0
	public override EffectProperties DamageDone(DamageInfo info)
	{
		if (this.PetOwnerID != -1)
		{
			EntityControl entity = EntityControl.GetEntity(this.PetOwnerID);
			if (entity != null && entity == PlayerControl.myInstance)
			{
				HitMarker.Show(info.DamageType, (int)info.TotalAmount, info.AtPoint, info.Depth);
				DamageInfo damageInfo = info.Copy();
				damageInfo.SnippetChance = 0f;
				PlayerControl.myInstance.DamageDone(damageInfo);
			}
		}
		return null;
	}

	// Token: 0x06000436 RID: 1078 RVA: 0x00020F35 File Offset: 0x0001F135
	private void OnDeath(DamageInfo dmg)
	{
		if (this.DeathRoutine != null)
		{
			base.StopCoroutine(this.DeathRoutine);
		}
		this.DeathRoutine = base.StartCoroutine("DeathSequence");
	}

	// Token: 0x06000437 RID: 1079 RVA: 0x00020F5C File Offset: 0x0001F15C
	private IEnumerator DeathSequence()
	{
		float t = 0f;
		while (t < 1.5f)
		{
			t += Time.deltaTime;
			yield return true;
		}
		if (!base.IsDead)
		{
			base.StopCoroutine(this.DeathRoutine);
		}
		t = 0f;
		while (t < 1.5f)
		{
			t += Time.deltaTime;
			yield return true;
		}
		this.net.Destroy();
		yield break;
	}

	// Token: 0x06000438 RID: 1080 RVA: 0x00020F6B File Offset: 0x0001F16B
	public AITrivialControl()
	{
	}

	// Token: 0x040003A6 RID: 934
	private Coroutine DeathRoutine;

	// Token: 0x0200048F RID: 1167
	[CompilerGenerated]
	private sealed class <DeathSequence>d__11 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060021E8 RID: 8680 RVA: 0x000C456A File Offset: 0x000C276A
		[DebuggerHidden]
		public <DeathSequence>d__11(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060021E9 RID: 8681 RVA: 0x000C4579 File Offset: 0x000C2779
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060021EA RID: 8682 RVA: 0x000C457C File Offset: 0x000C277C
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			AITrivialControl aitrivialControl = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				t = 0f;
				break;
			case 1:
				this.<>1__state = -1;
				break;
			case 2:
				this.<>1__state = -1;
				goto IL_C0;
			default:
				return false;
			}
			if (t < 1.5f)
			{
				t += Time.deltaTime;
				this.<>2__current = true;
				this.<>1__state = 1;
				return true;
			}
			if (!aitrivialControl.IsDead)
			{
				aitrivialControl.StopCoroutine(aitrivialControl.DeathRoutine);
			}
			t = 0f;
			IL_C0:
			if (t >= 1.5f)
			{
				aitrivialControl.net.Destroy();
				return false;
			}
			t += Time.deltaTime;
			this.<>2__current = true;
			this.<>1__state = 2;
			return true;
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060021EB RID: 8683 RVA: 0x000C4662 File Offset: 0x000C2862
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060021EC RID: 8684 RVA: 0x000C466A File Offset: 0x000C286A
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060021ED RID: 8685 RVA: 0x000C4671 File Offset: 0x000C2871
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002325 RID: 8997
		private int <>1__state;

		// Token: 0x04002326 RID: 8998
		private object <>2__current;

		// Token: 0x04002327 RID: 8999
		public AITrivialControl <>4__this;

		// Token: 0x04002328 RID: 9000
		private float <t>5__2;
	}
}
