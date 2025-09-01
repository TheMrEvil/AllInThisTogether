using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x0200026D RID: 621
public class LogicFilter_SortNumber : LogicFilterNode
{
	// Token: 0x060018CE RID: 6350 RVA: 0x0009AFC4 File Offset: 0x000991C4
	public override void Filter(ref List<EffectProperties> propList, EffectProperties props)
	{
		if (!(this.Num == null))
		{
			Node num2 = this.Num;
			NumberNode num = num2 as NumberNode;
			if (num != null)
			{
				propList.Sort(delegate(EffectProperties x, EffectProperties y)
				{
					float value = num.Evaluate(x);
					float value2 = num.Evaluate(y);
					if (this.Ascending)
					{
						return value.CompareTo(value2);
					}
					return value2.CompareTo(value);
				});
				return;
			}
		}
	}

	// Token: 0x060018CF RID: 6351 RVA: 0x0009B01B File Offset: 0x0009921B
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = base.GetInspectorProps();
		inspectorProps.Title = "Sort: Number";
		return inspectorProps;
	}

	// Token: 0x060018D0 RID: 6352 RVA: 0x0009B02E File Offset: 0x0009922E
	public LogicFilter_SortNumber()
	{
	}

	// Token: 0x040018AF RID: 6319
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), false, "Number", PortLocation.Default)]
	public Node Num;

	// Token: 0x040018B0 RID: 6320
	[Tooltip("Lowest value first")]
	public bool Ascending;

	// Token: 0x0200062C RID: 1580
	[CompilerGenerated]
	private sealed class <>c__DisplayClass2_0
	{
		// Token: 0x0600279B RID: 10139 RVA: 0x000D6881 File Offset: 0x000D4A81
		public <>c__DisplayClass2_0()
		{
		}

		// Token: 0x0600279C RID: 10140 RVA: 0x000D688C File Offset: 0x000D4A8C
		internal int <Filter>b__0(EffectProperties x, EffectProperties y)
		{
			float value = this.num.Evaluate(x);
			float value2 = this.num.Evaluate(y);
			if (this.<>4__this.Ascending)
			{
				return value.CompareTo(value2);
			}
			return value2.CompareTo(value);
		}

		// Token: 0x04002A24 RID: 10788
		public NumberNode num;

		// Token: 0x04002A25 RID: 10789
		public LogicFilter_SortNumber <>4__this;
	}
}
