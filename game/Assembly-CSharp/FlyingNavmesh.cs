using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x020000FC RID: 252
public class FlyingNavmesh : MonoBehaviour
{
	// Token: 0x06000BE4 RID: 3044 RVA: 0x0004D149 File Offset: 0x0004B349
	private void Awake()
	{
		FlyingNavmesh.instance = this;
		FlyingNavmesh.IsGenerated = false;
		this.Generate(UnityEngine.Random.Range(0, 1000));
		MapManager.SceneChanged = (Action)Delegate.Combine(MapManager.SceneChanged, new Action(delegate()
		{
			this.Generate(UnityEngine.Random.Range(0, 1000));
		}));
	}

	// Token: 0x06000BE5 RID: 3045 RVA: 0x0004D188 File Offset: 0x0004B388
	private void Update()
	{
		if (this.Navmesh == null || this.Navmesh.Count == 0 || EntityControl.AllEntities == null)
		{
			return;
		}
		if (!FlyingNavmesh.HasFlyingAI())
		{
			return;
		}
		int num = 50;
		if (this.checkIndex >= this.Navmesh.Count)
		{
			this.checkIndex = 0;
		}
		for (int i = this.checkIndex; i < Mathf.Min(this.checkIndex + num, this.Navmesh.Count); i++)
		{
			FlynavNode flynavNode = this.Navmesh[i];
			if (flynavNode.VisionPoint != null)
			{
				flynavNode.VisionPoint.InView.Clear();
				foreach (EntityControl entityControl in EntityControl.AllEntities)
				{
					Vector3 vector = entityControl.display.CenterOfMass.position - flynavNode.Position;
					if (vector.magnitude <= 120f && vector.magnitude < flynavNode.VisionPoint.GradientDistance(vector.normalized, false))
					{
						flynavNode.VisionPoint.InView.Add(entityControl, true);
					}
				}
			}
		}
		this.checkIndex += num;
	}

	// Token: 0x06000BE6 RID: 3046 RVA: 0x0004D2D8 File Offset: 0x0004B4D8
	public static bool HasFlyingAI()
	{
		if (AIManager.instance == null || AIControl.AllAI == null)
		{
			return false;
		}
		bool result = false;
		using (List<AIControl>.Enumerator enumerator = AIControl.AllAI.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.movement is AIFlyingMovement)
				{
					result = true;
					break;
				}
			}
		}
		return result;
	}

	// Token: 0x06000BE7 RID: 3047 RVA: 0x0004D34C File Offset: 0x0004B54C
	public void ClearPoints()
	{
		if (this.Navmesh == null)
		{
			this.Navmesh = new List<FlynavNode>();
		}
		this.Navmesh.Clear();
		this.checkedPts.Clear();
	}

	// Token: 0x06000BE8 RID: 3048 RVA: 0x0004D378 File Offset: 0x0004B578
	public void Generate(int MeshSeed)
	{
		this.ClearPoints();
		this.config = UnityEngine.Object.FindObjectOfType<FlyingConfig>();
		if (this.config == null)
		{
			if (!MapManager.InLobbyScene)
			{
				UnityEngine.Debug.LogError("No Flying Config found, aborting generation");
			}
			return;
		}
		if (this.genRoutine != null)
		{
			base.StopCoroutine(this.genRoutine);
		}
		this.genRoutine = base.StartCoroutine(this.GenerateTree());
	}

	// Token: 0x06000BE9 RID: 3049 RVA: 0x0004D3DC File Offset: 0x0004B5DC
	private IEnumerator GenerateTree()
	{
		this.IsGenerating = true;
		Vector3 gridSize = this.config.gridSize;
		int cellSize = this.config.cellSize;
		int pointsToCheck = (int)(gridSize.x / (float)cellSize * (gridSize.y / (float)cellSize) * (gridSize.z / (float)cellSize));
		int numPoints = 0;
		int num;
		for (int x = -(int)(gridSize.x / 2f); x < (int)(gridSize.x / 2f); x += cellSize)
		{
			for (int y = 6; y < (int)gridSize.y; y += cellSize)
			{
				for (int z = -(int)(gridSize.z / 2f); z < (int)(gridSize.z / 2f); z += cellSize)
				{
					Vector3 corner = new Vector3((float)x, (float)y, (float)z) + this.config.Offset;
					this.GeneratePoints(new FlyingNavmesh.Cube[]
					{
						new FlyingNavmesh.Cube(corner, (float)cellSize)
					});
					num = numPoints;
					numPoints = num + 1;
					this.GenerationProgress = 0.75f * ((float)numPoints / (float)pointsToCheck);
					yield return true;
				}
			}
		}
		yield return true;
		int skipCount = 20;
		int skip = 0;
		for (int x = this.Navmesh.Count - 1; x >= 0; x = num - 1)
		{
			num = skip;
			skip = num + 1;
			if (skip >= skipCount)
			{
				skip = 0;
				yield return true;
			}
			this.AddNeihbors(this.Navmesh[x]);
			if (this.Navmesh[x].Edges.Count == 0)
			{
				this.Navmesh[x].RemoveNeighbors();
				this.Navmesh.RemoveAt(x);
			}
			this.GenerationProgress = 0.75f + 0.25f * ((float)(this.Navmesh.Count - x) / (float)this.Navmesh.Count);
			num = x;
		}
		this.IsGenerating = false;
		if (Application.isPlaying)
		{
			FlyingNavmesh.IsGenerated = true;
		}
		yield break;
	}

	// Token: 0x06000BEA RID: 3050 RVA: 0x0004D3EC File Offset: 0x0004B5EC
	public void GeneratePoints(FlyingNavmesh.Cube[] Cubes)
	{
		if (this.config == null)
		{
			return;
		}
		float minSize = this.config.minSize;
		Terrain terrainTest = this.config.TerrainTest;
		if (Cubes.Length == 0 || Cubes[0].Length < minSize)
		{
			return;
		}
		foreach (FlyingNavmesh.Cube cube in Cubes)
		{
			if (!this.checkedPts.ContainsKey(cube.Corner) || this.checkedPts[cube.Corner] > cube.Length)
			{
				if (this.checkedPts.ContainsKey(cube.Corner))
				{
					this.checkedPts[cube.Corner] = cube.Length;
				}
				else
				{
					this.checkedPts.Add(cube.Corner, cube.Length);
				}
				if (Physics.OverlapBox(cube.Center, Vector3.one * cube.Length / 2f, Quaternion.identity, this.checkMask).Length != 0)
				{
					this.GeneratePoints(cube.Subdivide());
				}
				else if (cube.Corner.y >= 0.1f)
				{
					float num = (terrainTest != null) ? terrainTest.SampleHeight(cube.Corner) : 0f;
					if (num <= cube.Corner.y)
					{
						FlynavNode item = new FlynavNode(cube, this.checkMask, num);
						this.Navmesh.Add(item);
					}
				}
			}
		}
	}

	// Token: 0x06000BEB RID: 3051 RVA: 0x0004D570 File Offset: 0x0004B770
	private Vector3[] GetCorners(Vector3 center, float Radius)
	{
		Radius /= 2f;
		return new Vector3[]
		{
			center + Vector3.up * Radius + Vector3.right * Radius + Vector3.forward * Radius,
			center + Vector3.up * Radius + Vector3.right * Radius - Vector3.forward * Radius,
			center + Vector3.up * Radius - Vector3.right * Radius + Vector3.forward * Radius,
			center + Vector3.up * Radius - Vector3.right * Radius - Vector3.forward * Radius,
			center - Vector3.up * Radius + Vector3.right * Radius + Vector3.forward * Radius,
			center - Vector3.up * Radius + Vector3.right * Radius - Vector3.forward * Radius,
			center - Vector3.up * Radius - Vector3.right * Radius + Vector3.forward * Radius,
			center - Vector3.up * Radius - Vector3.right * Radius - Vector3.forward * Radius
		};
	}

	// Token: 0x06000BEC RID: 3052 RVA: 0x0004D74C File Offset: 0x0004B94C
	private void AddNeihbors(FlynavNode cell)
	{
		foreach (FlynavNode flynavNode in this.Navmesh)
		{
			if (flynavNode != cell && !flynavNode.IsNeightbor(cell) && (cell.Position - flynavNode.Position).sqrMagnitude * 0.95f <= cell.Length * cell.Length + flynavNode.Length * flynavNode.Length)
			{
				flynavNode.AddEdge(cell);
				cell.AddEdge(flynavNode);
			}
		}
	}

	// Token: 0x06000BED RID: 3053 RVA: 0x0004D7F4 File Offset: 0x0004B9F4
	public FlynavNode NearestNode(Vector3 point)
	{
		FlynavNode result = null;
		float num = float.MaxValue;
		foreach (FlynavNode flynavNode in this.Navmesh)
		{
			float num2 = Vector3.Distance(flynavNode, point);
			if (num2 < num)
			{
				num = num2;
				result = flynavNode;
			}
		}
		return result;
	}

	// Token: 0x06000BEE RID: 3054 RVA: 0x0004D864 File Offset: 0x0004BA64
	private void OnDrawGizmos()
	{
		if (this.InDebug(FlyingNavmesh.DebugViewMode.Off) || this.config == null)
		{
			return;
		}
		int cellSize = this.config.cellSize;
		Vector3 gridSize = this.config.gridSize;
		if (this.Navmesh != null && this.Navmesh.Count > 0)
		{
			foreach (FlynavNode flynavNode in this.Navmesh)
			{
				if (this.InDebug(FlyingNavmesh.DebugViewMode.Cubes))
				{
					Gizmos.color = new Color(0f, 0f, 1f, flynavNode.Length / (float)cellSize);
					Gizmos.DrawWireCube(flynavNode.Position, Vector3.one * flynavNode.Length);
				}
				if (this.InDebug(FlyingNavmesh.DebugViewMode.Links))
				{
					Gizmos.color = new Color(0f, 0f, 0f, 0.2f);
					foreach (FlynavNode.Edge edge in flynavNode.Edges)
					{
						Gizmos.DrawLine(flynavNode.Position, edge.Position);
					}
				}
				if (this.InDebug(FlyingNavmesh.DebugViewMode.VisionRange) && flynavNode.VisionPoint != null)
				{
					Gizmos.color = new Color(0f, 0f, 1f, 0.05f);
					Gizmos.DrawSphere(flynavNode.Position, Mathf.Sqrt(flynavNode.VisionPoint.ValidRadSqr));
				}
				if (this.InDebug(FlyingNavmesh.DebugViewMode.VisionLines) && flynavNode.VisionPoint != null)
				{
					flynavNode.VisionPoint.DrawLineGizmos();
				}
				if (this.InDebug(FlyingNavmesh.DebugViewMode.PlayerVisible) && flynavNode.VisionPoint != null && flynavNode.VisionPoint.InView != null && PlayerControl.myInstance != null && flynavNode.VisionPoint.InView.ContainsKey(PlayerControl.myInstance))
				{
					Gizmos.color = new Color(0f, 0f, 1f, 0.8f);
					Gizmos.DrawWireCube(flynavNode.Position, Vector3.one * flynavNode.Length);
				}
			}
			Gizmos.color = new Color(0.25f, 0.25f, 0.25f, 0.1f);
			Gizmos.DrawCube(Vector3.up * (gridSize.y / 2f), gridSize);
		}
	}

	// Token: 0x06000BEF RID: 3055 RVA: 0x0004DAF4 File Offset: 0x0004BCF4
	private bool InDebug(FlyingNavmesh.DebugViewMode modeMask)
	{
		return (this.DebugMode & modeMask) > (FlyingNavmesh.DebugViewMode)0;
	}

	// Token: 0x06000BF0 RID: 3056 RVA: 0x0004DB01 File Offset: 0x0004BD01
	public FlyingNavmesh()
	{
	}

	// Token: 0x06000BF1 RID: 3057 RVA: 0x0004DB14 File Offset: 0x0004BD14
	[CompilerGenerated]
	private void <Awake>b__9_0()
	{
		this.Generate(UnityEngine.Random.Range(0, 1000));
	}

	// Token: 0x040009A8 RID: 2472
	public static FlyingNavmesh instance;

	// Token: 0x040009A9 RID: 2473
	[NonSerialized]
	public List<FlynavNode> Navmesh;

	// Token: 0x040009AA RID: 2474
	public LayerMask checkMask;

	// Token: 0x040009AB RID: 2475
	private bool IsGenerating;

	// Token: 0x040009AC RID: 2476
	public static bool IsGenerated;

	// Token: 0x040009AD RID: 2477
	public float GenerationProgress;

	// Token: 0x040009AE RID: 2478
	public FlyingNavmesh.DebugViewMode DebugMode;

	// Token: 0x040009AF RID: 2479
	private Coroutine genRoutine;

	// Token: 0x040009B0 RID: 2480
	private FlyingConfig config;

	// Token: 0x040009B1 RID: 2481
	private int checkIndex;

	// Token: 0x040009B2 RID: 2482
	private Dictionary<Vector3, float> checkedPts = new Dictionary<Vector3, float>();

	// Token: 0x020004F6 RID: 1270
	[Serializable]
	public class Cube
	{
		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x0600235B RID: 9051 RVA: 0x000C974A File Offset: 0x000C794A
		// (set) Token: 0x0600235C RID: 9052 RVA: 0x000C9752 File Offset: 0x000C7952
		public Vector3 Center
		{
			[CompilerGenerated]
			get
			{
				return this.<Center>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Center>k__BackingField = value;
			}
		}

		// Token: 0x0600235D RID: 9053 RVA: 0x000C975B File Offset: 0x000C795B
		public Cube(Vector3 corner, float length)
		{
			this.Corner = corner;
			this.Length = length;
			this.Center = this.Corner + Vector3.one * length / 2f;
		}

		// Token: 0x0600235E RID: 9054 RVA: 0x000C9798 File Offset: 0x000C7998
		public Vector3 RandomPointInBounds()
		{
			float x = UnityEngine.Random.Range(-1f, 1f);
			float y = UnityEngine.Random.Range(-1f, 1f);
			float z = UnityEngine.Random.Range(-1f, 1f);
			return this.Center + new Vector3(x, y, z) * this.Length;
		}

		// Token: 0x0600235F RID: 9055 RVA: 0x000C97F4 File Offset: 0x000C79F4
		public FlyingNavmesh.Cube[] Subdivide()
		{
			FlyingNavmesh.Cube[] array = new FlyingNavmesh.Cube[8];
			float num = this.Length / 2f;
			array[0] = new FlyingNavmesh.Cube(this.Corner, num);
			array[1] = new FlyingNavmesh.Cube(this.Corner + Vector3.right * num, num);
			array[2] = new FlyingNavmesh.Cube(this.Corner + Vector3.forward * num, num);
			array[3] = new FlyingNavmesh.Cube(this.Corner + Vector3.right * num + Vector3.forward * num, num);
			array[4] = new FlyingNavmesh.Cube(this.Corner + Vector3.up * num, num);
			array[5] = new FlyingNavmesh.Cube(this.Corner + Vector3.up * num + Vector3.right * num, num);
			array[6] = new FlyingNavmesh.Cube(this.Corner + Vector3.up * num + Vector3.forward * num, num);
			array[7] = new FlyingNavmesh.Cube(this.Corner + Vector3.up * num + Vector3.right * num + Vector3.forward * num, num);
			return array;
		}

		// Token: 0x04002536 RID: 9526
		public Vector3 Corner;

		// Token: 0x04002537 RID: 9527
		public float Length;

		// Token: 0x04002538 RID: 9528
		[CompilerGenerated]
		private Vector3 <Center>k__BackingField;
	}

	// Token: 0x020004F7 RID: 1271
	public enum DebugViewMode
	{
		// Token: 0x0400253A RID: 9530
		Off = 1,
		// Token: 0x0400253B RID: 9531
		Cubes,
		// Token: 0x0400253C RID: 9532
		Links = 4,
		// Token: 0x0400253D RID: 9533
		VisionRange = 8,
		// Token: 0x0400253E RID: 9534
		VisionLines = 16,
		// Token: 0x0400253F RID: 9535
		PlayerVisible = 32
	}

	// Token: 0x020004F8 RID: 1272
	[CompilerGenerated]
	private sealed class <GenerateTree>d__15 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002360 RID: 9056 RVA: 0x000C994C File Offset: 0x000C7B4C
		[DebuggerHidden]
		public <GenerateTree>d__15(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002361 RID: 9057 RVA: 0x000C995B File Offset: 0x000C7B5B
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002362 RID: 9058 RVA: 0x000C9960 File Offset: 0x000C7B60
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			FlyingNavmesh flyingNavmesh = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				flyingNavmesh.IsGenerating = true;
				gridSize = flyingNavmesh.config.gridSize;
				cellSize = flyingNavmesh.config.cellSize;
				pointsToCheck = (int)(gridSize.x / (float)cellSize * (gridSize.y / (float)cellSize) * (gridSize.z / (float)cellSize));
				numPoints = 0;
				x = -(int)(gridSize.x / 2f);
				goto IL_1E2;
			case 1:
				this.<>1__state = -1;
				z += cellSize;
				break;
			case 2:
				this.<>1__state = -1;
				skipCount = 20;
				skip = 0;
				x = flyingNavmesh.Navmesh.Count - 1;
				goto IL_320;
			case 3:
				this.<>1__state = -1;
				goto IL_283;
			default:
				return false;
			}
			IL_188:
			int num2;
			if (z < (int)(gridSize.z / 2f))
			{
				Vector3 corner = new Vector3((float)x, (float)y, (float)z) + flyingNavmesh.config.Offset;
				flyingNavmesh.GeneratePoints(new FlyingNavmesh.Cube[]
				{
					new FlyingNavmesh.Cube(corner, (float)cellSize)
				});
				num2 = numPoints;
				numPoints = num2 + 1;
				flyingNavmesh.GenerationProgress = 0.75f * ((float)numPoints / (float)pointsToCheck);
				this.<>2__current = true;
				this.<>1__state = 1;
				return true;
			}
			y += cellSize;
			IL_1B8:
			if (y < (int)gridSize.y)
			{
				z = -(int)(gridSize.z / 2f);
				goto IL_188;
			}
			x += cellSize;
			IL_1E2:
			if (x >= (int)(gridSize.x / 2f))
			{
				this.<>2__current = true;
				this.<>1__state = 2;
				return true;
			}
			y = 6;
			goto IL_1B8;
			IL_283:
			flyingNavmesh.AddNeihbors(flyingNavmesh.Navmesh[x]);
			if (flyingNavmesh.Navmesh[x].Edges.Count == 0)
			{
				flyingNavmesh.Navmesh[x].RemoveNeighbors();
				flyingNavmesh.Navmesh.RemoveAt(x);
			}
			flyingNavmesh.GenerationProgress = 0.75f + 0.25f * ((float)(flyingNavmesh.Navmesh.Count - x) / (float)flyingNavmesh.Navmesh.Count);
			num2 = x;
			x = num2 - 1;
			IL_320:
			if (x < 0)
			{
				flyingNavmesh.IsGenerating = false;
				if (Application.isPlaying)
				{
					FlyingNavmesh.IsGenerated = true;
				}
				return false;
			}
			num2 = skip;
			skip = num2 + 1;
			if (skip >= skipCount)
			{
				skip = 0;
				this.<>2__current = true;
				this.<>1__state = 3;
				return true;
			}
			goto IL_283;
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06002363 RID: 9059 RVA: 0x000C9CAE File Offset: 0x000C7EAE
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x000C9CB6 File Offset: 0x000C7EB6
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06002365 RID: 9061 RVA: 0x000C9CBD File Offset: 0x000C7EBD
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002540 RID: 9536
		private int <>1__state;

		// Token: 0x04002541 RID: 9537
		private object <>2__current;

		// Token: 0x04002542 RID: 9538
		public FlyingNavmesh <>4__this;

		// Token: 0x04002543 RID: 9539
		private Vector3 <gridSize>5__2;

		// Token: 0x04002544 RID: 9540
		private int <cellSize>5__3;

		// Token: 0x04002545 RID: 9541
		private int <pointsToCheck>5__4;

		// Token: 0x04002546 RID: 9542
		private int <numPoints>5__5;

		// Token: 0x04002547 RID: 9543
		private int <skipCount>5__6;

		// Token: 0x04002548 RID: 9544
		private int <skip>5__7;

		// Token: 0x04002549 RID: 9545
		private int <x>5__8;

		// Token: 0x0400254A RID: 9546
		private int <y>5__9;

		// Token: 0x0400254B RID: 9547
		private int <z>5__10;
	}
}
