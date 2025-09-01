using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x0200031D RID: 797
public class PositionSort_Distance : PositionFilterNode
{
	// Token: 0x06001B87 RID: 7047 RVA: 0x000A99C0 File Offset: 0x000A7BC0
	public override void FilterPoints(ref List<PotentialNavPoint> points, AIControl control, Vector3 origin)
	{
		origin = (this.FromEntity ? control.movement.GetPosition() : origin);
		points.Sort(delegate(PotentialNavPoint x, PotentialNavPoint y)
		{
			this.d1 = (x.pos - origin).sqrMagnitude;
			this.d2 = (y.pos - origin).sqrMagnitude;
			if (this.NearestFirst)
			{
				return this.d1.CompareTo(this.d2);
			}
			return this.d2.CompareTo(this.d1);
		});
	}

	// Token: 0x06001B88 RID: 7048 RVA: 0x000A9A15 File Offset: 0x000A7C15
	public override bool PointIsValid(AIControl asker, PotentialNavPoint point, Vector3 origin)
	{
		return true;
	}

	// Token: 0x06001B89 RID: 7049 RVA: 0x000A9A18 File Offset: 0x000A7C18
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = base.GetInspectorProps();
		inspectorProps.Title = "Sort: Distance";
		return inspectorProps;
	}

	// Token: 0x06001B8A RID: 7050 RVA: 0x000A9A2B File Offset: 0x000A7C2B
	public PositionSort_Distance()
	{
	}

	// Token: 0x04001BED RID: 7149
	public bool NearestFirst = true;

	// Token: 0x04001BEE RID: 7150
	public bool FromEntity;

	// Token: 0x04001BEF RID: 7151
	private float d1;

	// Token: 0x04001BF0 RID: 7152
	private float d2;

	// Token: 0x02000659 RID: 1625
	[CompilerGenerated]
	private sealed class <>c__DisplayClass4_0
	{
		// Token: 0x060027BA RID: 10170 RVA: 0x000D6E28 File Offset: 0x000D5028
		public <>c__DisplayClass4_0()
		{
		}

		// Token: 0x060027BB RID: 10171 RVA: 0x000D6E30 File Offset: 0x000D5030
		internal int <FilterPoints>b__0(PotentialNavPoint x, PotentialNavPoint y)
		{
			this.<>4__this.d1 = (x.pos - this.origin).sqrMagnitude;
			this.<>4__this.d2 = (y.pos - this.origin).sqrMagnitude;
			if (this.<>4__this.NearestFirst)
			{
				return this.<>4__this.d1.CompareTo(this.<>4__this.d2);
			}
			return this.<>4__this.d2.CompareTo(this.<>4__this.d1);
		}

		// Token: 0x04002B1B RID: 11035
		public PositionSort_Distance <>4__this;

		// Token: 0x04002B1C RID: 11036
		public Vector3 origin;
	}
}
