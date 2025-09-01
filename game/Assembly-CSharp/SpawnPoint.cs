using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x020000CF RID: 207
public class SpawnPoint : MonoBehaviour
{
	// Token: 0x170000CF RID: 207
	// (get) Token: 0x06000985 RID: 2437 RVA: 0x0003FAEE File Offset: 0x0003DCEE
	public bool IsAISpawn
	{
		get
		{
			return this.SpawnType == SpawnType.AI_Flying || this.SpawnType == SpawnType.AI_Ground || this.SpawnType == SpawnType.AI_Turret;
		}
	}

	// Token: 0x170000D0 RID: 208
	// (get) Token: 0x06000986 RID: 2438 RVA: 0x0003FB0D File Offset: 0x0003DD0D
	public Vector3 point
	{
		get
		{
			return base.transform.position;
		}
	}

	// Token: 0x06000987 RID: 2439 RVA: 0x0003FB1A File Offset: 0x0003DD1A
	private void Awake()
	{
		SpawnPoint.AllSpawns.Add(this);
	}

	// Token: 0x06000988 RID: 2440 RVA: 0x0003FB27 File Offset: 0x0003DD27
	private IEnumerator Start()
	{
		yield return true;
		if (this.SpawnType != SpawnType.Player && AIVisionGraph.instance != null)
		{
			this.nearestVision = AIVisionGraph.instance.NearestNavPoint(base.transform.position);
		}
		yield break;
	}

	// Token: 0x06000989 RID: 2441 RVA: 0x0003FB36 File Offset: 0x0003DD36
	public void Spawn()
	{
		this.LastSpawnedTime = Time.realtimeSinceStartup;
	}

	// Token: 0x0600098A RID: 2442 RVA: 0x0003FB43 File Offset: 0x0003DD43
	private void OnDestroy()
	{
		SpawnPoint.AllSpawns.Remove(this);
	}

	// Token: 0x0600098B RID: 2443 RVA: 0x0003FB54 File Offset: 0x0003DD54
	private void OnDrawGizmos()
	{
		if (this.SpawnType != SpawnPoint.ShowSpawns && SpawnPoint.ShowSpawns != SpawnType.None)
		{
			return;
		}
		Color color = Color.clear;
		switch (this.SpawnType)
		{
		case SpawnType.Player:
			color = Color.green;
			break;
		case SpawnType.AI_Ground:
			color = Color.red;
			break;
		case SpawnType.AI_Flying:
			color = Color.yellow;
			break;
		case SpawnType.AI_Turret:
			color = Color.red;
			break;
		case SpawnType.Map_Goal:
			color = Color.white;
			break;
		case SpawnType.Map_Explore:
			color = Color.blue;
			break;
		case SpawnType.Map_Intersection:
			color = Color.grey;
			break;
		case SpawnType.Map_UnusedA:
			color = Color.yellow;
			break;
		}
		Gizmos.color = color;
		Gizmos.DrawSphere(base.transform.position, 0.5f);
	}

	// Token: 0x0600098C RID: 2444 RVA: 0x0003FC08 File Offset: 0x0003DE08
	public static Vector3 GetObjectiveLocation(EffectProperties props)
	{
		List<SpawnPoint> allSpawns = SpawnPoint.GetAllSpawns(SpawnType.Map_Goal, EnemyLevel.None);
		if (allSpawns.Count == 0)
		{
			Scene_Settings instance = Scene_Settings.instance;
			if (instance == null)
			{
				return Vector3.zero;
			}
			return instance.MapOrigin;
		}
		else
		{
			Vector3 sortLocation = (SpawnZone.CurZones != null && SpawnZone.CurZones.Count > 0) ? SpawnZone.CurZones[0].transform.position : Vector3.zero;
			allSpawns.Sort((SpawnPoint x, SpawnPoint y) => Vector3.Distance(sortLocation, x.point).CompareTo(Vector3.Distance(y.point, sortLocation)));
			if (SpawnZone.CurZones != null || props == null)
			{
				return allSpawns[0].point;
			}
			return allSpawns[props.RandomInt(0, allSpawns.Count)].point;
		}
	}

	// Token: 0x0600098D RID: 2445 RVA: 0x0003FCB8 File Offset: 0x0003DEB8
	public static List<SpawnPoint> GetAllSpawns(SpawnType spawnType, EnemyLevel level = EnemyLevel.None)
	{
		List<SpawnPoint> list = new List<SpawnPoint>();
		foreach (SpawnPoint spawnPoint in SpawnPoint.AllSpawns)
		{
			if (spawnPoint.SpawnType == spawnType && (!spawnPoint.IsAISpawn || spawnPoint.SpawnLevel == EnemyLevel.None || level == EnemyLevel.None || spawnPoint.SpawnLevel.HasFlag(level)))
			{
				list.Add(spawnPoint);
			}
		}
		return list;
	}

	// Token: 0x0600098E RID: 2446 RVA: 0x0003FD48 File Offset: 0x0003DF48
	public static SpawnPoint GetRandomSpawnPoint(SpawnType st, EffectProperties props = null)
	{
		List<SpawnPoint> list = new List<SpawnPoint>();
		foreach (SpawnPoint spawnPoint in SpawnPoint.AllSpawns)
		{
			if (spawnPoint.SpawnType == st)
			{
				list.Add(spawnPoint);
			}
		}
		if (list.Count == 0)
		{
			return null;
		}
		int index = UnityEngine.Random.Range(0, list.Count);
		if (props != null)
		{
			index = props.RandomInt(0, list.Count);
		}
		return list[index];
	}

	// Token: 0x0600098F RID: 2447 RVA: 0x0003FDD8 File Offset: 0x0003DFD8
	public static List<SpawnPoint> GetValidSpawns(SpawnType spawnType, EnemyLevel level = EnemyLevel.None)
	{
		List<SpawnPoint> list = new List<SpawnPoint>();
		foreach (SpawnPoint spawnPoint in SpawnPoint.GetAllSpawns(spawnType, level))
		{
			if (spawnPoint.ValidSpawn())
			{
				list.Add(spawnPoint);
			}
		}
		return list;
	}

	// Token: 0x06000990 RID: 2448 RVA: 0x0003FE3C File Offset: 0x0003E03C
	public bool ValidSpawn()
	{
		if (this.SpawnType == SpawnType.Player)
		{
			return true;
		}
		if (this.SpawnLevel.HasFlag(EnemyLevel.Boss))
		{
			return true;
		}
		if (this.nearestVision == null)
		{
			return true;
		}
		foreach (KeyValuePair<EntityControl, bool> keyValuePair in this.nearestVision.InView)
		{
			if (keyValuePair.Key != null && keyValuePair.Key is PlayerControl)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06000991 RID: 2449 RVA: 0x0003FEE0 File Offset: 0x0003E0E0
	public SpawnPoint()
	{
	}

	// Token: 0x06000992 RID: 2450 RVA: 0x0003FEE8 File Offset: 0x0003E0E8
	// Note: this type is marked as 'beforefieldinit'.
	static SpawnPoint()
	{
	}

	// Token: 0x040007E7 RID: 2023
	public SpawnType SpawnType;

	// Token: 0x040007E8 RID: 2024
	public EnemyLevel SpawnLevel;

	// Token: 0x040007E9 RID: 2025
	public static SpawnType ShowSpawns;

	// Token: 0x040007EA RID: 2026
	public static List<SpawnPoint> AllSpawns = new List<SpawnPoint>();

	// Token: 0x040007EB RID: 2027
	[NonSerialized]
	public float LastSpawnedTime;

	// Token: 0x040007EC RID: 2028
	private NavVisionPoint nearestVision;

	// Token: 0x020004C9 RID: 1225
	[CompilerGenerated]
	private sealed class <Start>d__11 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060022CA RID: 8906 RVA: 0x000C7A7D File Offset: 0x000C5C7D
		[DebuggerHidden]
		public <Start>d__11(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060022CB RID: 8907 RVA: 0x000C7A8C File Offset: 0x000C5C8C
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060022CC RID: 8908 RVA: 0x000C7A90 File Offset: 0x000C5C90
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			SpawnPoint spawnPoint = this;
			if (num == 0)
			{
				this.<>1__state = -1;
				this.<>2__current = true;
				this.<>1__state = 1;
				return true;
			}
			if (num != 1)
			{
				return false;
			}
			this.<>1__state = -1;
			if (spawnPoint.SpawnType != SpawnType.Player && AIVisionGraph.instance != null)
			{
				spawnPoint.nearestVision = AIVisionGraph.instance.NearestNavPoint(spawnPoint.transform.position);
			}
			return false;
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x060022CD RID: 8909 RVA: 0x000C7B08 File Offset: 0x000C5D08
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060022CE RID: 8910 RVA: 0x000C7B10 File Offset: 0x000C5D10
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x060022CF RID: 8911 RVA: 0x000C7B17 File Offset: 0x000C5D17
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002455 RID: 9301
		private int <>1__state;

		// Token: 0x04002456 RID: 9302
		private object <>2__current;

		// Token: 0x04002457 RID: 9303
		public SpawnPoint <>4__this;
	}

	// Token: 0x020004CA RID: 1226
	[CompilerGenerated]
	private sealed class <>c__DisplayClass15_0
	{
		// Token: 0x060022D0 RID: 8912 RVA: 0x000C7B1F File Offset: 0x000C5D1F
		public <>c__DisplayClass15_0()
		{
		}

		// Token: 0x060022D1 RID: 8913 RVA: 0x000C7B28 File Offset: 0x000C5D28
		internal int <GetObjectiveLocation>b__0(SpawnPoint x, SpawnPoint y)
		{
			return Vector3.Distance(this.sortLocation, x.point).CompareTo(Vector3.Distance(y.point, this.sortLocation));
		}

		// Token: 0x04002458 RID: 9304
		public Vector3 sortLocation;
	}
}
