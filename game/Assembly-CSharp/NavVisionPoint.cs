using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x02000100 RID: 256
[Serializable]
public class NavVisionPoint
{
	// Token: 0x06000C04 RID: 3076 RVA: 0x0004DFA5 File Offset: 0x0004C1A5
	public NavVisionPoint(Vector3 Position, LayerMask mask = default(LayerMask), float MaxLength = 120f)
	{
		this.overrideMask = mask;
		this.Position = Position;
		this.CalculateVision(MaxLength);
		this.InView = new Dictionary<EntityControl, bool>();
		this.Occupying = new Dictionary<EntityControl, bool>();
	}

	// Token: 0x06000C05 RID: 3077 RVA: 0x0004DFE4 File Offset: 0x0004C1E4
	private int CalculateVisRotation(int layerIndex)
	{
		int num = 10;
		int num2 = 3;
		int num3 = Mathf.Abs(layerIndex - num2) * 10;
		return num + num3;
	}

	// Token: 0x06000C06 RID: 3078 RVA: 0x0004E004 File Offset: 0x0004C204
	public void CalculateVision(float Max)
	{
		this.Distances = new List<List<float>>();
		RaycastHit hit = default(RaycastHit);
		LayerMask checkMask = this.overrideMask;
		if (FlyingNavmesh.instance != null)
		{
			checkMask = FlyingNavmesh.instance.checkMask;
		}
		this.ValidRadSqr = Max;
		for (int i = 0; i < 7; i++)
		{
			int num = this.CalculateVisRotation(i);
			int num2 = Mathf.FloorToInt(360f / (float)num);
			List<float> list = new List<float>();
			for (int j = 0; j < num2; j++)
			{
				Vector3 direction = this.GetDirection(i, j);
				float setupDistance = this.GetSetupDistance(direction, hit, checkMask);
				this.ValidRadSqr = Mathf.Min(setupDistance * setupDistance, this.ValidRadSqr);
				list.Add(setupDistance);
			}
			this.Distances.Add(list);
		}
		this.CalculateNavRange();
	}

	// Token: 0x06000C07 RID: 3079 RVA: 0x0004E0D0 File Offset: 0x0004C2D0
	public float GradientDistance(Vector3 dir, bool debug = false)
	{
		float num = (dir.y + 1f) / 2f;
		int num2 = this.DetermineLayerIndex(dir.y);
		int num3 = this.CalculateVisRotation(num2);
		Vector3 to = new Vector3(dir.x, 0f, dir.z);
		float num4 = Vector3.SignedAngle(Vector3.forward, to, Vector3.up);
		if (num4 < 0f)
		{
			num4 += 360f;
		}
		int num5 = Mathf.FloorToInt(360f / (float)num3);
		int num6 = Mathf.FloorToInt(num4 / (float)num3);
		float biasFactor = 0.1f;
		float num7 = this.Distances[num2][num6];
		float num8 = this.Distances[num2][(num6 + 1) % num5];
		float num9 = this.CustomLerp(num7, num8, num4 % (float)num3 / (float)num3, biasFactor);
		if (debug)
		{
			Debug.DrawRay(this.Position, this.GetDirection(num2, num6).normalized * num7, Color.blue);
			Debug.DrawRay(this.Position, this.GetDirection(num2, (num6 + 1) % num5).normalized * num8, Color.blue);
		}
		if (num2 - 1 >= 0)
		{
			int num10 = this.CalculateVisRotation(num2 - 1);
			int num11 = Mathf.FloorToInt(360f / (float)num10);
			int num12 = Mathf.FloorToInt(num4 / (float)num10);
			float num13 = this.Distances[num2 - 1][num12];
			float num14 = this.Distances[num2 - 1][(num12 + 1) % num11];
			float b = this.CustomLerp(num13, num14, num4 % (float)num10 / (float)num10, biasFactor);
			float num15 = this.CalculateAngularNormalizedVert(dir.y);
			float num16 = this.CalculateAngularNormalizedVert(this.GetDirection(num2, 0).y);
			float num17 = this.CalculateAngularNormalizedVert(this.GetDirection(num2 - 1, 0).y);
			float num18 = Mathf.InverseLerp(num16, num17, num15);
			if (debug)
			{
				Debug.DrawRay(this.Position, this.GetDirection(num2 - 1, num12).normalized * num13, Color.blue);
				Debug.DrawRay(this.Position, this.GetDirection(num2 - 1, (num12 + 1) % num11).normalized * num14, Color.blue);
				Debug.Log(string.Format("{0} -> {1} | {2} -> {3}", new object[]
				{
					num15,
					num16,
					num17,
					num18
				}));
			}
			return this.CustomLerp(num9, b, num18, 0.25f);
		}
		return num9;
	}

	// Token: 0x06000C08 RID: 3080 RVA: 0x0004E370 File Offset: 0x0004C570
	private float CustomLerp(float a, float b, float t, float biasFactor)
	{
		float result;
		if (a < b)
		{
			result = Mathf.Lerp(a, b, Mathf.Pow(t, 1f / biasFactor));
		}
		else
		{
			result = Mathf.Lerp(a, b, 1f - Mathf.Pow(1f - t, 1f / biasFactor));
		}
		return result;
	}

	// Token: 0x06000C09 RID: 3081 RVA: 0x0004E3BC File Offset: 0x0004C5BC
	private float CalculateAngularNormalizedVert(float yComponent)
	{
		float num = 0.33333334f;
		float num2 = (1f - yComponent) / num / 6f;
		return 1f - num2;
	}

	// Token: 0x06000C0A RID: 3082 RVA: 0x0004E3E8 File Offset: 0x0004C5E8
	private int DetermineLayerIndex(float vert)
	{
		float num = (vert + 1f) / 2f;
		return 6 - Mathf.FloorToInt(num * 6f);
	}

	// Token: 0x06000C0B RID: 3083 RVA: 0x0004E414 File Offset: 0x0004C614
	public void DrawLineGizmos()
	{
		if (this.Distances == null)
		{
			return;
		}
		Color green = Color.green;
		green.a = 0.3f;
		Gizmos.color = green;
		for (int i = 0; i < 7; i++)
		{
			if (this.Distances.Count < i)
			{
				return;
			}
			int num = Mathf.FloorToInt(360f / (float)this.CalculateVisRotation(i));
			for (int j = 0; j < num; j++)
			{
				if (this.Distances[i].Count < j)
				{
					return;
				}
				Vector3 direction = this.GetDirection(i, j);
				Gizmos.DrawLine(this.Position, this.Position + direction * this.Distances[i][j]);
			}
		}
	}

	// Token: 0x06000C0C RID: 3084 RVA: 0x0004E4D4 File Offset: 0x0004C6D4
	public Vector3 GetDirection(int vertIndex, int angleIndex)
	{
		float num = 0.33333334f;
		float num2 = 1f - num * (float)vertIndex;
		float angle = (float)(angleIndex * this.CalculateVisRotation(vertIndex));
		Vector3 point = Vector3.forward;
		point = Quaternion.AngleAxis(angle, Vector3.up) * point;
		point.y += num2;
		return point.normalized;
	}

	// Token: 0x06000C0D RID: 3085 RVA: 0x0004E528 File Offset: 0x0004C728
	public bool ValidDist(Vector3 targetPt, float fudge = 0f)
	{
		return (targetPt - this.Position).sqrMagnitude < this.ValidRadSqr + fudge;
	}

	// Token: 0x06000C0E RID: 3086 RVA: 0x0004E554 File Offset: 0x0004C754
	private float GetSetupDistance(Vector3 dir, RaycastHit hit, LayerMask mask)
	{
		float result = 120f;
		if (Physics.Raycast(this.Position, dir, out hit, 120f, mask))
		{
			result = hit.distance;
		}
		return result;
	}

	// Token: 0x06000C0F RID: 3087 RVA: 0x0004E58C File Offset: 0x0004C78C
	private void CalculateNavRange()
	{
		NavMeshHit navMeshHit;
		if (NavMesh.SamplePosition(this.Position, out navMeshHit, 3f, -1))
		{
			this.NavPosition = navMeshHit.position;
		}
		else
		{
			this.NavPosition = Vector3.one.INVALID();
		}
		if (!this.NavPosition.IsValid())
		{
			return;
		}
		float num = 10f;
		for (int i = 0; i < 7; i++)
		{
			int num2 = Mathf.FloorToInt(360f / (float)this.CalculateVisRotation(i));
			for (int j = 0; j < num2; j++)
			{
				Vector3 direction = this.GetDirection(i, j);
				if (NavMesh.SamplePosition(this.Position + direction * 10f, out navMeshHit, 10f, -1) && NavMesh.Raycast(this.Position, navMeshHit.position, out navMeshHit, -1) && navMeshHit.distance < num)
				{
					num = navMeshHit.distance;
				}
			}
		}
		this.ValidRadSqr = num * num;
	}

	// Token: 0x06000C10 RID: 3088 RVA: 0x0004E67C File Offset: 0x0004C87C
	public void AddConnection(NavVisionPoint node)
	{
		NavVisionPoint.NavConnection navConnection = new NavVisionPoint.NavConnection();
		navConnection.Node = node;
		navConnection.Cost = (node.NavPosition - this.NavPosition).sqrMagnitude;
		this.Connected.Add(navConnection);
	}

	// Token: 0x040009C0 RID: 2496
	public Vector3 Position;

	// Token: 0x040009C1 RID: 2497
	public Vector3 NavPosition;

	// Token: 0x040009C2 RID: 2498
	public float ValidRadSqr;

	// Token: 0x040009C3 RID: 2499
	public List<List<float>> Distances;

	// Token: 0x040009C4 RID: 2500
	private static int _DirsPerLayer;

	// Token: 0x040009C5 RID: 2501
	private static int _Layers;

	// Token: 0x040009C6 RID: 2502
	public const float _MAX_DISTANCE = 120f;

	// Token: 0x040009C7 RID: 2503
	private const int verticalLayers = 7;

	// Token: 0x040009C8 RID: 2504
	[NonSerialized]
	public Dictionary<EntityControl, bool> InView;

	// Token: 0x040009C9 RID: 2505
	[NonSerialized]
	public Dictionary<EntityControl, bool> Occupying;

	// Token: 0x040009CA RID: 2506
	public List<NavVisionPoint.NavConnection> Connected = new List<NavVisionPoint.NavConnection>();

	// Token: 0x040009CB RID: 2507
	private LayerMask overrideMask;

	// Token: 0x040009CC RID: 2508
	public float debugVertLerp;

	// Token: 0x040009CD RID: 2509
	public float debugAngLerp;

	// Token: 0x020004FA RID: 1274
	public class NavConnection
	{
		// Token: 0x06002368 RID: 9064 RVA: 0x000C9CE8 File Offset: 0x000C7EE8
		public NavConnection()
		{
		}

		// Token: 0x0400254E RID: 9550
		public NavVisionPoint Node;

		// Token: 0x0400254F RID: 9551
		public float Cost;
	}
}
