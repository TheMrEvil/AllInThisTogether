using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x020002A2 RID: 674
public class AbilitySelectorNode : AbilityNode
{
	// Token: 0x0600197A RID: 6522 RVA: 0x0009EE98 File Offset: 0x0009D098
	internal override AbilityState Run(EffectProperties props)
	{
		float value = this.GetValue(props);
		for (int i = this.Thresholds.Count - 1; i >= 0; i--)
		{
			float num = this.Range.x + this.Thresholds[i] * (this.Range.y - this.Range.x);
			if (value >= num)
			{
				return (this.Options[i] as AbilityNode).DoUpdate(props);
			}
		}
		return AbilityState.Finished;
	}

	// Token: 0x0600197B RID: 6523 RVA: 0x0009EF14 File Offset: 0x0009D114
	private float GetValue(EffectProperties props)
	{
		if (this.hasChecked && this.CheckBehaviour == AbilitySelectorNode.ThresholdBehavior.CheckOnce)
		{
			return this.savedValue;
		}
		this.hasChecked = true;
		this.savedValue = 0f;
		if (this.Threshold == AbilitySelectorNode.ThresholdType.EffectProp)
		{
			if (this.NeedsID())
			{
				this.savedValue = props.GetExtra(this.EffectProp, this.ThreshID, 0f);
			}
			else
			{
				this.savedValue = props.GetExtra(this.EffectProp, 0f);
			}
		}
		if (this.Threshold == AbilitySelectorNode.ThresholdType.EntityHealth)
		{
			this.savedValue = props.SourceControl.health.CurrentHealthProportion;
		}
		return this.savedValue;
	}

	// Token: 0x0600197C RID: 6524 RVA: 0x0009EFB6 File Offset: 0x0009D1B6
	private bool NeedsID()
	{
		return this.EffectProp == EProp.ExplicitStacks;
	}

	// Token: 0x0600197D RID: 6525 RVA: 0x0009EFC4 File Offset: 0x0009D1C4
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Threshold Selector",
			MinInspectorSize = new Vector2(260f, 0f)
		};
	}

	// Token: 0x0600197E RID: 6526 RVA: 0x0009EFEB File Offset: 0x0009D1EB
	public override void OnConnectionsChanged()
	{
		this.ValidateList(this.Thresholds);
	}

	// Token: 0x0600197F RID: 6527 RVA: 0x0009EFFC File Offset: 0x0009D1FC
	private bool ValidateList(List<float> list)
	{
		if (list == null || this.Options == null)
		{
			return true;
		}
		if (list.Count < this.Options.Count)
		{
			int num = this.Options.Count - list.Count;
			for (int i = 0; i < num; i++)
			{
				list.Add(0f);
			}
		}
		else if (list.Count > this.Options.Count)
		{
			int num2 = list.Count - this.Options.Count;
			for (int j = 0; j < num2; j++)
			{
				list.RemoveAt(list.Count - 1);
			}
		}
		list.Sort((float x, float y) => x.CompareTo(y));
		return true;
	}

	// Token: 0x06001980 RID: 6528 RVA: 0x0009F0BB File Offset: 0x0009D2BB
	public AbilitySelectorNode()
	{
	}

	// Token: 0x040019B6 RID: 6582
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(AbilityNode), true, "", PortLocation.Header)]
	public List<Node> Options = new List<Node>();

	// Token: 0x040019B7 RID: 6583
	public AbilitySelectorNode.ThresholdBehavior CheckBehaviour;

	// Token: 0x040019B8 RID: 6584
	private bool hasChecked;

	// Token: 0x040019B9 RID: 6585
	private float savedValue;

	// Token: 0x040019BA RID: 6586
	public AbilitySelectorNode.ThresholdType Threshold;

	// Token: 0x040019BB RID: 6587
	public EProp EffectProp;

	// Token: 0x040019BC RID: 6588
	public string ThreshID;

	// Token: 0x040019BD RID: 6589
	public Vector2 Range = Vector2.up;

	// Token: 0x040019BE RID: 6590
	[Space(10f)]
	[Range(0f, 1f)]
	public List<float> Thresholds;

	// Token: 0x02000640 RID: 1600
	public enum ThresholdType
	{
		// Token: 0x04002AB4 RID: 10932
		EffectProp,
		// Token: 0x04002AB5 RID: 10933
		EntityHealth
	}

	// Token: 0x02000641 RID: 1601
	public enum ThresholdBehavior
	{
		// Token: 0x04002AB7 RID: 10935
		CheckAlways,
		// Token: 0x04002AB8 RID: 10936
		CheckOnce
	}

	// Token: 0x02000642 RID: 1602
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		// Token: 0x060027A6 RID: 10150 RVA: 0x000D6C21 File Offset: 0x000D4E21
		// Note: this type is marked as 'beforefieldinit'.
		static <>c()
		{
		}

		// Token: 0x060027A7 RID: 10151 RVA: 0x000D6C2D File Offset: 0x000D4E2D
		public <>c()
		{
		}

		// Token: 0x060027A8 RID: 10152 RVA: 0x000D6C35 File Offset: 0x000D4E35
		internal int <ValidateList>b__16_0(float x, float y)
		{
			return x.CompareTo(y);
		}

		// Token: 0x04002AB9 RID: 10937
		public static readonly AbilitySelectorNode.<>c <>9 = new AbilitySelectorNode.<>c();

		// Token: 0x04002ABA RID: 10938
		public static Comparison<float> <>9__16_0;
	}
}
