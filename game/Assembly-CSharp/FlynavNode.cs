using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x020000FD RID: 253
[Serializable]
public class FlynavNode
{
	// Token: 0x1700010C RID: 268
	// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x0004DB27 File Offset: 0x0004BD27
	public Vector3 Position
	{
		get
		{
			return this.cube.Center;
		}
	}

	// Token: 0x1700010D RID: 269
	// (get) Token: 0x06000BF3 RID: 3059 RVA: 0x0004DB34 File Offset: 0x0004BD34
	public float Length
	{
		get
		{
			return this.cube.Length;
		}
	}

	// Token: 0x06000BF4 RID: 3060 RVA: 0x0004DB44 File Offset: 0x0004BD44
	public FlynavNode(FlyingNavmesh.Cube c, LayerMask mask, float terrainHeight)
	{
		this.cube = c;
		if (this.Position.y < terrainHeight + 20f)
		{
			this.VisionPoint = new NavVisionPoint(this.Position, mask, c.Length * c.Length);
		}
	}

	// Token: 0x06000BF5 RID: 3061 RVA: 0x0004DBA7 File Offset: 0x0004BDA7
	public Vector3 RandomPointInBounds()
	{
		return this.cube.RandomPointInBounds();
	}

	// Token: 0x06000BF6 RID: 3062 RVA: 0x0004DBB4 File Offset: 0x0004BDB4
	public bool IsNeightbor(FlynavNode node)
	{
		return this.neighbors.ContainsKey(node);
	}

	// Token: 0x06000BF7 RID: 3063 RVA: 0x0004DBC4 File Offset: 0x0004BDC4
	public void AddEdge(FlynavNode n)
	{
		this.neighbors.Add(n, true);
		float cost = Vector3.Distance(n.Position, this.Position);
		FlynavNode.Edge item = new FlynavNode.Edge(n, cost);
		this.Edges.Add(item);
	}

	// Token: 0x06000BF8 RID: 3064 RVA: 0x0004DC04 File Offset: 0x0004BE04
	public static implicit operator Vector3(FlynavNode c)
	{
		return c.Position;
	}

	// Token: 0x06000BF9 RID: 3065 RVA: 0x0004DC0C File Offset: 0x0004BE0C
	public override bool Equals(object obj)
	{
		return obj is FlynavNode && (obj as FlynavNode).Position.Equals(this.Position);
	}

	// Token: 0x06000BFA RID: 3066 RVA: 0x0004DC3C File Offset: 0x0004BE3C
	protected bool Equals(FlynavNode other)
	{
		return object.Equals(this.VisionPoint, other.VisionPoint);
	}

	// Token: 0x06000BFB RID: 3067 RVA: 0x0004DC4F File Offset: 0x0004BE4F
	public override int GetHashCode()
	{
		if (this.VisionPoint == null)
		{
			return 0;
		}
		return this.VisionPoint.GetHashCode();
	}

	// Token: 0x06000BFC RID: 3068 RVA: 0x0004DC68 File Offset: 0x0004BE68
	public void RemoveNeighbors()
	{
		foreach (FlynavNode.Edge edge in this.Edges)
		{
			edge.Node.Edges.RemoveAll((FlynavNode.Edge x) => x.Node == this);
		}
	}

	// Token: 0x06000BFD RID: 3069 RVA: 0x0004DCD0 File Offset: 0x0004BED0
	[CompilerGenerated]
	private bool <RemoveNeighbors>b__16_0(FlynavNode.Edge x)
	{
		return x.Node == this;
	}

	// Token: 0x040009B3 RID: 2483
	private FlyingNavmesh.Cube cube;

	// Token: 0x040009B4 RID: 2484
	[HideInInspector]
	public List<FlynavNode.Edge> Edges = new List<FlynavNode.Edge>();

	// Token: 0x040009B5 RID: 2485
	public NavVisionPoint VisionPoint;

	// Token: 0x040009B6 RID: 2486
	private Dictionary<FlynavNode, bool> neighbors = new Dictionary<FlynavNode, bool>();

	// Token: 0x020004F9 RID: 1273
	public class Edge
	{
		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06002366 RID: 9062 RVA: 0x000C9CC5 File Offset: 0x000C7EC5
		public Vector3 Position
		{
			get
			{
				return this.Node.Position;
			}
		}

		// Token: 0x06002367 RID: 9063 RVA: 0x000C9CD2 File Offset: 0x000C7ED2
		public Edge(FlynavNode node, float cost)
		{
			this.Node = node;
			this.cost = cost;
		}

		// Token: 0x0400254C RID: 9548
		public float cost;

		// Token: 0x0400254D RID: 9549
		[HideInInspector]
		public FlynavNode Node;
	}
}
