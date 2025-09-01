using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000F9 RID: 249
[ExecuteInEditMode]
public class AIVisionGraph : MonoBehaviour
{
	// Token: 0x06000BCC RID: 3020 RVA: 0x0004C77B File Offset: 0x0004A97B
	private void Awake()
	{
		AIVisionGraph.instance = this;
		AIVisionGraph.ownedPts = new Dictionary<NavVisionPoint, int>();
		AIVisionGraph.travellingThrough = new Dictionary<NavVisionPoint, int>();
		MapManager.SceneChanged = (Action)Delegate.Combine(MapManager.SceneChanged, new Action(this.LoadPoints));
	}

	// Token: 0x06000BCD RID: 3021 RVA: 0x0004C7B7 File Offset: 0x0004A9B7
	private void Start()
	{
		this.LoadPoints();
	}

	// Token: 0x06000BCE RID: 3022 RVA: 0x0004C7C0 File Offset: 0x0004A9C0
	public void LoadPoints()
	{
		VisionSpawns visionSpawns = UnityEngine.Object.FindObjectOfType<VisionSpawns>();
		Transform transform = (visionSpawns != null) ? visionSpawns.transform : null;
		if (transform == null || transform.childCount == 0)
		{
			return;
		}
		this.ClearGraph();
		foreach (object obj in transform)
		{
			Transform transform2 = (Transform)obj;
			this.AddVisionPoint(transform2.position);
			if (Application.isPlaying)
			{
				UnityEngine.Object.Destroy(transform2.gameObject);
			}
		}
		this.CalculateLinks();
	}

	// Token: 0x06000BCF RID: 3023 RVA: 0x0004C85C File Offset: 0x0004AA5C
	private void ClearGraph()
	{
		this.points.Clear();
		AIVisionGraph.ownedPts.Clear();
		AIVisionGraph.travellingThrough.Clear();
	}

	// Token: 0x06000BD0 RID: 3024 RVA: 0x0004C880 File Offset: 0x0004AA80
	private void CalculateLinks()
	{
		foreach (NavVisionPoint navVisionPoint in this.points)
		{
			foreach (NavVisionPoint navVisionPoint2 in this.points)
			{
				if ((navVisionPoint.Position - navVisionPoint2.Position).sqrMagnitude < navVisionPoint.ValidRadSqr + navVisionPoint2.ValidRadSqr + (navVisionPoint.ValidRadSqr + navVisionPoint2.ValidRadSqr))
				{
					navVisionPoint.AddConnection(navVisionPoint2);
				}
			}
		}
	}

	// Token: 0x06000BD1 RID: 3025 RVA: 0x0004C948 File Offset: 0x0004AB48
	public static void AddOwnership(NavVisionPoint pt)
	{
		if (AIVisionGraph.ownedPts.ContainsKey(pt))
		{
			Dictionary<NavVisionPoint, int> dictionary = AIVisionGraph.ownedPts;
			int num = dictionary[pt];
			dictionary[pt] = num + 1;
			return;
		}
		AIVisionGraph.ownedPts.Add(pt, 1);
	}

	// Token: 0x06000BD2 RID: 3026 RVA: 0x0004C988 File Offset: 0x0004AB88
	public static void ReleaseOwnership(NavVisionPoint pt)
	{
		if (AIVisionGraph.ownedPts.ContainsKey(pt) && AIVisionGraph.ownedPts[pt] > 0)
		{
			Dictionary<NavVisionPoint, int> dictionary = AIVisionGraph.ownedPts;
			int num = dictionary[pt];
			dictionary[pt] = num - 1;
		}
	}

	// Token: 0x06000BD3 RID: 3027 RVA: 0x0004C9C8 File Offset: 0x0004ABC8
	public static void AddTravel(NavVisionPoint pt)
	{
		if (AIVisionGraph.travellingThrough.ContainsKey(pt))
		{
			Dictionary<NavVisionPoint, int> dictionary = AIVisionGraph.travellingThrough;
			int num = dictionary[pt];
			dictionary[pt] = num + 1;
			return;
		}
		AIVisionGraph.travellingThrough.Add(pt, 1);
	}

	// Token: 0x06000BD4 RID: 3028 RVA: 0x0004CA08 File Offset: 0x0004AC08
	public static void ReleaseTravel(NavVisionPoint pt)
	{
		if (AIVisionGraph.travellingThrough.ContainsKey(pt) && AIVisionGraph.travellingThrough[pt] > 0)
		{
			Dictionary<NavVisionPoint, int> dictionary = AIVisionGraph.travellingThrough;
			int num = dictionary[pt];
			dictionary[pt] = num - 1;
		}
	}

	// Token: 0x06000BD5 RID: 3029 RVA: 0x0004CA48 File Offset: 0x0004AC48
	public static bool CanTarget(NavVisionPoint pt)
	{
		return !AIVisionGraph.ownedPts.ContainsKey(pt) || (float)AIVisionGraph.ownedPts[pt] < AIVisionGraph.instance.MaxAIPerPoint.Evaluate((float)AIManager.AliveEnemies);
	}

	// Token: 0x06000BD6 RID: 3030 RVA: 0x0004CA7C File Offset: 0x0004AC7C
	private void Update()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		if (this.points == null || this.points.Count == 0 || EntityControl.AllEntities == null)
		{
			return;
		}
		int num = 10;
		if (this.checkIndex >= this.points.Count)
		{
			this.checkIndex = 0;
		}
		for (int i = this.checkIndex; i < Mathf.Min(this.checkIndex + num, this.points.Count); i++)
		{
			NavVisionPoint navVisionPoint = this.points[i];
			navVisionPoint.InView.Clear();
			foreach (EntityControl entityControl in EntityControl.AllEntities)
			{
				if (!(entityControl is AITrivialControl))
				{
					Vector3 vector = entityControl.display.CenterOfMass.position - navVisionPoint.Position;
					float magnitude = vector.magnitude;
					if (magnitude <= 120f)
					{
						if (magnitude < navVisionPoint.GradientDistance(vector.normalized, false))
						{
							navVisionPoint.InView.Add(entityControl, true);
						}
						if (vector.sqrMagnitude < Mathf.Max(navVisionPoint.ValidRadSqr, 12f))
						{
							navVisionPoint.Occupying.TryAdd(entityControl, true);
						}
						else
						{
							navVisionPoint.Occupying.Remove(entityControl);
						}
					}
				}
			}
		}
		this.checkIndex += num;
	}

	// Token: 0x06000BD7 RID: 3031 RVA: 0x0004CBF8 File Offset: 0x0004ADF8
	public NavVisionPoint GetOccupiedPoint(EntityControl entity)
	{
		foreach (NavVisionPoint navVisionPoint in this.points)
		{
			if (navVisionPoint.Occupying.ContainsKey(entity))
			{
				return navVisionPoint;
			}
		}
		return null;
	}

	// Token: 0x06000BD8 RID: 3032 RVA: 0x0004CC5C File Offset: 0x0004AE5C
	private void AddVisionPoint(Vector3 position)
	{
		NavVisionPoint item = new NavVisionPoint(position, this.mask, 120f);
		this.points.Add(item);
	}

	// Token: 0x06000BD9 RID: 3033 RVA: 0x0004CC88 File Offset: 0x0004AE88
	public List<NavVisionPoint> CalculateGroundPath(Vector3 StartPt, Vector3 TargetPt)
	{
		NavVisionPoint start = this.NearestNavPoint(StartPt);
		NavVisionPoint goal = this.NearestNavPoint(TargetPt);
		return this.CalculateGroundPath(start, goal);
	}

	// Token: 0x06000BDA RID: 3034 RVA: 0x0004CCB0 File Offset: 0x0004AEB0
	public List<NavVisionPoint> CalculateGroundPath(NavVisionPoint start, NavVisionPoint goal)
	{
		if (start == null || goal == null)
		{
			return new List<NavVisionPoint>();
		}
		new List<NavVisionPoint>();
		Dictionary<NavVisionPoint, NavVisionPoint> dictionary = new Dictionary<NavVisionPoint, NavVisionPoint>();
		Dictionary<NavVisionPoint, float> dictionary2 = new Dictionary<NavVisionPoint, float>();
		PriorityQueue<NavVisionPoint> priorityQueue = new PriorityQueue<NavVisionPoint>();
		priorityQueue.Enqueue(start, 0f);
		dictionary.Add(start, start);
		dictionary2.Add(start, 0f);
		NavVisionPoint navVisionPoint;
		while (priorityQueue.Count > 0)
		{
			navVisionPoint = priorityQueue.Dequeue();
			if (navVisionPoint == goal)
			{
				break;
			}
			foreach (NavVisionPoint.NavConnection navConnection in navVisionPoint.Connected)
			{
				NavVisionPoint node = navConnection.Node;
				float num = navConnection.Cost;
				if (AIVisionGraph.ownedPts.ContainsKey(node) || AIVisionGraph.travellingThrough.ContainsKey(node))
				{
					num += navConnection.Cost * 2f;
				}
				float num2 = dictionary2[navVisionPoint] + num;
				if (!dictionary2.ContainsKey(node) || num2 < dictionary2[node])
				{
					if (dictionary2.ContainsKey(node))
					{
						dictionary2.Remove(node);
						dictionary.Remove(node);
					}
					dictionary2.Add(node, num2);
					dictionary.Add(node, navVisionPoint);
					if (node == null || goal == null)
					{
						string str = "Nbr: ";
						NavVisionPoint navVisionPoint2 = node;
						Debug.LogError(str + ((navVisionPoint2 != null) ? navVisionPoint2.ToString() : null) + " | Goal: " + ((goal != null) ? goal.ToString() : null));
					}
					float priority = num2 + (node.NavPosition - goal.NavPosition).sqrMagnitude;
					priorityQueue.Enqueue(node, priority);
				}
			}
		}
		List<NavVisionPoint> list = new List<NavVisionPoint>();
		navVisionPoint = goal;
		while (!navVisionPoint.Equals(start))
		{
			if (!dictionary.ContainsKey(navVisionPoint))
			{
				return new List<NavVisionPoint>();
			}
			list.Add(navVisionPoint);
			navVisionPoint = dictionary[navVisionPoint];
		}
		list.Add(start);
		list.Reverse();
		return list;
	}

	// Token: 0x06000BDB RID: 3035 RVA: 0x0004CEAC File Offset: 0x0004B0AC
	public NavVisionPoint NearestNavPoint(Vector3 pt)
	{
		float num = float.MaxValue;
		NavVisionPoint result = null;
		foreach (NavVisionPoint navVisionPoint in this.points)
		{
			float sqrMagnitude = (navVisionPoint.NavPosition - pt).sqrMagnitude;
			if (sqrMagnitude < num)
			{
				num = sqrMagnitude;
				result = navVisionPoint;
			}
		}
		return result;
	}

	// Token: 0x06000BDC RID: 3036 RVA: 0x0004CF24 File Offset: 0x0004B124
	public AIVisionGraph()
	{
	}

	// Token: 0x06000BDD RID: 3037 RVA: 0x0004CF37 File Offset: 0x0004B137
	// Note: this type is marked as 'beforefieldinit'.
	static AIVisionGraph()
	{
	}

	// Token: 0x0400099C RID: 2460
	public static AIVisionGraph instance;

	// Token: 0x0400099D RID: 2461
	[NonSerialized]
	public List<NavVisionPoint> points = new List<NavVisionPoint>();

	// Token: 0x0400099E RID: 2462
	public LayerMask mask;

	// Token: 0x0400099F RID: 2463
	public AIVisionGraph.VisionDebugMode DebugMode;

	// Token: 0x040009A0 RID: 2464
	public bool SpawnMode;

	// Token: 0x040009A1 RID: 2465
	public AnimationCurve MaxAIPerPoint;

	// Token: 0x040009A2 RID: 2466
	public static Dictionary<NavVisionPoint, int> ownedPts = new Dictionary<NavVisionPoint, int>();

	// Token: 0x040009A3 RID: 2467
	public static Dictionary<NavVisionPoint, int> travellingThrough = new Dictionary<NavVisionPoint, int>();

	// Token: 0x040009A4 RID: 2468
	private int checkIndex;

	// Token: 0x020004F5 RID: 1269
	public enum VisionDebugMode
	{
		// Token: 0x0400252F RID: 9519
		Off,
		// Token: 0x04002530 RID: 9520
		Players_Visible,
		// Token: 0x04002531 RID: 9521
		Owned,
		// Token: 0x04002532 RID: 9522
		TravelingThrough,
		// Token: 0x04002533 RID: 9523
		Connections,
		// Token: 0x04002534 RID: 9524
		InvalidConnections,
		// Token: 0x04002535 RID: 9525
		All
	}
}
