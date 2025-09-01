using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x020002A5 RID: 677
public class CancelAbilityNode : EffectNode
{
	// Token: 0x0600198E RID: 6542 RVA: 0x0009F573 File Offset: 0x0009D773
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "End Ability",
			ShowInspectorView = false,
			MinInspectorSize = new Vector2(220f, 0f)
		};
	}

	// Token: 0x0600198F RID: 6543 RVA: 0x0009F5A4 File Offset: 0x0009D7A4
	internal override void Apply(EffectProperties properties)
	{
		if (!this.ShouldApply(properties, properties.SourceControl))
		{
			return;
		}
		PlayerControl playerControl = properties.SourceControl as PlayerControl;
		if (playerControl != null)
		{
			playerControl.actions.ForceCancel(base.AbilityRoot);
			return;
		}
		AIControl aicontrol = properties.SourceControl as AIControl;
		if (aicontrol != null)
		{
			aicontrol.StartCoroutine(this.EndFrame(aicontrol));
		}
	}

	// Token: 0x06001990 RID: 6544 RVA: 0x0009F5FF File Offset: 0x0009D7FF
	private IEnumerator EndFrame(AIControl ai)
	{
		yield return new WaitForEndOfFrame();
		ai.Net.AbilityReleased(base.AbilityRoot);
		yield break;
	}

	// Token: 0x06001991 RID: 6545 RVA: 0x0009F615 File Offset: 0x0009D815
	internal override bool ShouldApply(EffectProperties props, EntityControl applyTo)
	{
		return (props.IsLocal && !(applyTo is PlayerControl)) || applyTo == PlayerControl.myInstance;
	}

	// Token: 0x06001992 RID: 6546 RVA: 0x0009F639 File Offset: 0x0009D839
	public CancelAbilityNode()
	{
	}

	// Token: 0x02000643 RID: 1603
	[CompilerGenerated]
	private sealed class <EndFrame>d__2 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060027A9 RID: 10153 RVA: 0x000D6C3F File Offset: 0x000D4E3F
		[DebuggerHidden]
		public <EndFrame>d__2(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060027AA RID: 10154 RVA: 0x000D6C4E File Offset: 0x000D4E4E
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060027AB RID: 10155 RVA: 0x000D6C50 File Offset: 0x000D4E50
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			CancelAbilityNode cancelAbilityNode = this;
			if (num == 0)
			{
				this.<>1__state = -1;
				this.<>2__current = new WaitForEndOfFrame();
				this.<>1__state = 1;
				return true;
			}
			if (num != 1)
			{
				return false;
			}
			this.<>1__state = -1;
			ai.Net.AbilityReleased(cancelAbilityNode.AbilityRoot);
			return false;
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x060027AC RID: 10156 RVA: 0x000D6CAD File Offset: 0x000D4EAD
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060027AD RID: 10157 RVA: 0x000D6CB5 File Offset: 0x000D4EB5
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x060027AE RID: 10158 RVA: 0x000D6CBC File Offset: 0x000D4EBC
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002ABB RID: 10939
		private int <>1__state;

		// Token: 0x04002ABC RID: 10940
		private object <>2__current;

		// Token: 0x04002ABD RID: 10941
		public AIControl ai;

		// Token: 0x04002ABE RID: 10942
		public CancelAbilityNode <>4__this;
	}
}
