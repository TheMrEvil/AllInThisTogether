using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200006A RID: 106
public class AINavPath
{
	// Token: 0x1700004C RID: 76
	// (get) Token: 0x060003FE RID: 1022 RVA: 0x0002005C File Offset: 0x0001E25C
	public Vector3 Destination
	{
		get
		{
			return this.destination.Position;
		}
	}

	// Token: 0x060003FF RID: 1023 RVA: 0x00020069 File Offset: 0x0001E269
	public static AINavPath GeneratePath(AIMovement mover, Vector3 target)
	{
		return AINavPath.GeneratePath(mover, new List<Vector3>
		{
			target
		});
	}

	// Token: 0x06000400 RID: 1024 RVA: 0x0002007D File Offset: 0x0001E27D
	public static AINavPath GeneratePath(AIMovement mover, List<Vector3> waypoints)
	{
		new AINavPath().mover = mover;
		return null;
	}

	// Token: 0x06000401 RID: 1025 RVA: 0x0002008B File Offset: 0x0001E28B
	public void Clear()
	{
		while (this.waypoints.Count > 0)
		{
			AIVisionGraph.ReleaseTravel(this.waypoints.Dequeue().Node);
		}
	}

	// Token: 0x06000402 RID: 1026 RVA: 0x000200B2 File Offset: 0x0001E2B2
	public AINavPath()
	{
	}

	// Token: 0x0400039C RID: 924
	private AIMovement mover;

	// Token: 0x0400039D RID: 925
	private int curIndex;

	// Token: 0x0400039E RID: 926
	private Queue<AINavPath.Waypoint> waypoints = new Queue<AINavPath.Waypoint>();

	// Token: 0x0400039F RID: 927
	private AINavPath.Waypoint destination;

	// Token: 0x0200048C RID: 1164
	internal class Waypoint
	{
		// Token: 0x060021DD RID: 8669 RVA: 0x000C4453 File Offset: 0x000C2653
		public Waypoint(NavVisionPoint navPt, Vector3 forward, Vector3 offset)
		{
			this.Node = navPt;
			this.Position = navPt.NavPosition + offset;
		}

		// Token: 0x060021DE RID: 8670 RVA: 0x000C4474 File Offset: 0x000C2674
		public Waypoint(Vector3 point)
		{
			this.Position = point;
		}

		// Token: 0x0400231C RID: 8988
		public Vector3 Position;

		// Token: 0x0400231D RID: 8989
		public NavVisionPoint Node;
	}
}
