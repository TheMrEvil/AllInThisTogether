using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x020002AA RID: 682
public class ActionThresholdNode : EffectNode
{
	// Token: 0x060019AD RID: 6573 RVA: 0x0009FB70 File Offset: 0x0009DD70
	internal override void Apply(EffectProperties properties)
	{
		float num = 0f;
		if (this.Value != null)
		{
			NumberNode numberNode = this.Value as NumberNode;
			if (numberNode != null)
			{
				num = numberNode.Evaluate(properties);
				goto IL_59;
			}
		}
		if (this.NeedsID())
		{
			num = properties.GetExtra(this.Threshold, this.ThreshID, num);
		}
		else
		{
			num = properties.GetExtra(this.Threshold, num);
		}
		IL_59:
		for (int i = this.Thresholds.Count - 1; i >= 0; i--)
		{
			float num2 = this.Range.x + this.Thresholds[i] * (this.Range.y - this.Range.x);
			if (num >= num2)
			{
				(this.Options[i] as EffectNode).Invoke(properties);
				return;
			}
		}
	}

	// Token: 0x060019AE RID: 6574 RVA: 0x0009FC3B File Offset: 0x0009DE3B
	public override void TryCancel(EffectProperties props)
	{
		this.OnCancel(props);
		base.TryCancel(props);
	}

	// Token: 0x060019AF RID: 6575 RVA: 0x0009FC4C File Offset: 0x0009DE4C
	internal override void OnCancel(EffectProperties props)
	{
		float num = 0f;
		if (this.Value != null)
		{
			NumberNode numberNode = this.Value as NumberNode;
			if (numberNode != null)
			{
				num = numberNode.Evaluate(props);
				goto IL_59;
			}
		}
		if (this.NeedsID())
		{
			num = props.GetExtra(this.Threshold, this.ThreshID, num);
		}
		else
		{
			num = props.GetExtra(this.Threshold, num);
		}
		IL_59:
		for (int i = this.Thresholds.Count - 1; i >= 0; i--)
		{
			float num2 = this.Range.x + this.Thresholds[i] * (this.Range.y - this.Range.x);
			if (num >= num2)
			{
				(this.Options[i] as EffectNode).TryCancel(props);
				return;
			}
		}
	}

	// Token: 0x060019B0 RID: 6576 RVA: 0x0009FD17 File Offset: 0x0009DF17
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Threshold Selector",
			MinInspectorSize = new Vector2(260f, 0f)
		};
	}

	// Token: 0x060019B1 RID: 6577 RVA: 0x0009FD3E File Offset: 0x0009DF3E
	private bool NeedsID()
	{
		return !(this.Value == null) && this.Threshold == EProp.ExplicitStacks;
	}

	// Token: 0x060019B2 RID: 6578 RVA: 0x0009FD5C File Offset: 0x0009DF5C
	private bool ValidateList(List<float> list)
	{
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

	// Token: 0x060019B3 RID: 6579 RVA: 0x0009FE0E File Offset: 0x0009E00E
	public ActionThresholdNode()
	{
	}

	// Token: 0x040019D8 RID: 6616
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), true, "Input", PortLocation.Header)]
	public Node Value;

	// Token: 0x040019D9 RID: 6617
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(EffectNode), true, "Options", PortLocation.Default)]
	public List<Node> Options = new List<Node>();

	// Token: 0x040019DA RID: 6618
	public EProp Threshold;

	// Token: 0x040019DB RID: 6619
	public string ThreshID;

	// Token: 0x040019DC RID: 6620
	public Vector2 Range = Vector2.up;

	// Token: 0x040019DD RID: 6621
	[Space(10f)]
	[Range(0f, 1f)]
	public List<float> Thresholds = new List<float>();

	// Token: 0x02000644 RID: 1604
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		// Token: 0x060027AF RID: 10159 RVA: 0x000D6CC4 File Offset: 0x000D4EC4
		// Note: this type is marked as 'beforefieldinit'.
		static <>c()
		{
		}

		// Token: 0x060027B0 RID: 10160 RVA: 0x000D6CD0 File Offset: 0x000D4ED0
		public <>c()
		{
		}

		// Token: 0x060027B1 RID: 10161 RVA: 0x000D6CD8 File Offset: 0x000D4ED8
		internal int <ValidateList>b__11_0(float x, float y)
		{
			return x.CompareTo(y);
		}

		// Token: 0x04002ABF RID: 10943
		public static readonly ActionThresholdNode.<>c <>9 = new ActionThresholdNode.<>c();

		// Token: 0x04002AC0 RID: 10944
		public static Comparison<float> <>9__11_0;
	}
}
