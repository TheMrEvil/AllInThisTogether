using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x0200026C RID: 620
public class LogicFilter_SortDistance : LogicFilterNode
{
	// Token: 0x060018CA RID: 6346 RVA: 0x0009AEF0 File Offset: 0x000990F0
	public override void Filter(ref List<EffectProperties> propList, EffectProperties props)
	{
		LogicFilter_SortDistance.<>c__DisplayClass3_0 CS$<>8__locals1 = new LogicFilter_SortDistance.<>c__DisplayClass3_0();
		CS$<>8__locals1.<>4__this = this;
		LogicFilter_SortDistance.<>c__DisplayClass3_0 CS$<>8__locals2 = CS$<>8__locals1;
		Location loc;
		if (this.Loc != null)
		{
			LocationNode locationNode = this.Loc as LocationNode;
			if (locationNode != null)
			{
				loc = locationNode.GetLocation(null);
				goto IL_35;
			}
		}
		loc = null;
		IL_35:
		CS$<>8__locals2.loc1 = loc;
		LogicFilter_SortDistance.<>c__DisplayClass3_0 CS$<>8__locals3 = CS$<>8__locals1;
		Location loc2;
		if (this.Loc2 != null)
		{
			LocationNode locationNode2 = this.Loc2 as LocationNode;
			if (locationNode2 != null)
			{
				loc2 = locationNode2.GetLocation(null);
				goto IL_62;
			}
		}
		loc2 = null;
		IL_62:
		CS$<>8__locals3.loc2 = loc2;
		propList.Sort(delegate(EffectProperties x, EffectProperties y)
		{
			Location loc3 = CS$<>8__locals1.loc1;
			Vector3 b = (loc3 != null) ? loc3.GetPosition(x) : x.StartLoc.GetPosition(x);
			Location loc4 = CS$<>8__locals1.loc2;
			Vector3 a = (loc4 != null) ? loc4.GetPosition(x) : x.OutLoc.GetPosition(x);
			Location loc5 = CS$<>8__locals1.loc1;
			Vector3 b2 = (loc5 != null) ? loc5.GetPosition(y) : y.StartLoc.GetPosition(y);
			Location loc6 = CS$<>8__locals1.loc2;
			Vector3 a2 = (loc6 != null) ? loc6.GetPosition(y) : y.OutLoc.GetPosition(y);
			float sqrMagnitude = (a - b).sqrMagnitude;
			float sqrMagnitude2 = (a2 - b2).sqrMagnitude;
			if (CS$<>8__locals1.<>4__this.Ascending)
			{
				return sqrMagnitude.CompareTo(sqrMagnitude2);
			}
			return sqrMagnitude2.CompareTo(sqrMagnitude);
		});
	}

	// Token: 0x060018CB RID: 6347 RVA: 0x0009AF78 File Offset: 0x00099178
	private float GetSqrDistance(EffectProperties props, Vector3 v1, Vector3 v2)
	{
		return (v2 - v1).sqrMagnitude;
	}

	// Token: 0x060018CC RID: 6348 RVA: 0x0009AF94 File Offset: 0x00099194
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = base.GetInspectorProps();
		inspectorProps.Title = "Sort: Distance";
		inspectorProps.MaxInspectorSize = new Vector2(150f, 0f);
		return inspectorProps;
	}

	// Token: 0x060018CD RID: 6349 RVA: 0x0009AFBC File Offset: 0x000991BC
	public LogicFilter_SortDistance()
	{
	}

	// Token: 0x040018AC RID: 6316
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(LocationNode), false, "Origin Override", PortLocation.Default)]
	public Node Loc;

	// Token: 0x040018AD RID: 6317
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(LocationNode), false, "Output Override", PortLocation.Default)]
	public Node Loc2;

	// Token: 0x040018AE RID: 6318
	[Tooltip("Nearest value first if on")]
	public bool Ascending;

	// Token: 0x0200062B RID: 1579
	[CompilerGenerated]
	private sealed class <>c__DisplayClass3_0
	{
		// Token: 0x06002799 RID: 10137 RVA: 0x000D67AE File Offset: 0x000D49AE
		public <>c__DisplayClass3_0()
		{
		}

		// Token: 0x0600279A RID: 10138 RVA: 0x000D67B8 File Offset: 0x000D49B8
		internal int <Filter>b__0(EffectProperties x, EffectProperties y)
		{
			Location location = this.loc1;
			Vector3 b = (location != null) ? location.GetPosition(x) : x.StartLoc.GetPosition(x);
			Location location2 = this.loc2;
			Vector3 a = (location2 != null) ? location2.GetPosition(x) : x.OutLoc.GetPosition(x);
			Location location3 = this.loc1;
			Vector3 b2 = (location3 != null) ? location3.GetPosition(y) : y.StartLoc.GetPosition(y);
			Location location4 = this.loc2;
			Vector3 a2 = (location4 != null) ? location4.GetPosition(y) : y.OutLoc.GetPosition(y);
			float sqrMagnitude = (a - b).sqrMagnitude;
			float sqrMagnitude2 = (a2 - b2).sqrMagnitude;
			if (this.<>4__this.Ascending)
			{
				return sqrMagnitude.CompareTo(sqrMagnitude2);
			}
			return sqrMagnitude2.CompareTo(sqrMagnitude);
		}

		// Token: 0x04002A21 RID: 10785
		public Location loc1;

		// Token: 0x04002A22 RID: 10786
		public Location loc2;

		// Token: 0x04002A23 RID: 10787
		public LogicFilter_SortDistance <>4__this;
	}
}
