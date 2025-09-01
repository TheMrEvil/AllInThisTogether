using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Photon.Pun;
using UnityEngine;

// Token: 0x020000D2 RID: 210
public class SpawnZone : MonoBehaviour
{
	// Token: 0x170000D1 RID: 209
	// (get) Token: 0x06000994 RID: 2452 RVA: 0x0003FF1E File Offset: 0x0003E11E
	private static Vector3 SORT_FROM
	{
		get
		{
			return new Vector3(0.12467f, -1.3591827f, 0.5181f);
		}
	}

	// Token: 0x170000D2 RID: 210
	// (get) Token: 0x06000995 RID: 2453 RVA: 0x0003FF34 File Offset: 0x0003E134
	public static bool HasSpawnZone
	{
		get
		{
			return SpawnZone.CurZones.Count > 0;
		}
	}

	// Token: 0x170000D3 RID: 211
	// (get) Token: 0x06000996 RID: 2454 RVA: 0x0003FF43 File Offset: 0x0003E143
	public Vector3 Point
	{
		get
		{
			return base.transform.position;
		}
	}

	// Token: 0x06000997 RID: 2455 RVA: 0x0003FF50 File Offset: 0x0003E150
	private void Awake()
	{
		SpawnZone.AllZones.Add(this);
		SpawnZone.AllZones.Sort((SpawnZone x, SpawnZone y) => Vector3.Distance(x.transform.position, SpawnZone.SORT_FROM).CompareTo(Vector3.Distance(y.transform.position, SpawnZone.SORT_FROM)));
	}

	// Token: 0x06000998 RID: 2456 RVA: 0x0003FF86 File Offset: 0x0003E186
	private void OnDestroy()
	{
		SpawnZone.AllZones.Remove(this);
		if (this.zone != null)
		{
			GreyscaleAreas.RemoveZone(this.zone);
		}
	}

	// Token: 0x06000999 RID: 2457 RVA: 0x0003FFA8 File Offset: 0x0003E1A8
	public static void SetZoneFromPosition(List<Vector3> points)
	{
		if (PhotonNetwork.IsMasterClient)
		{
			return;
		}
		SpawnZone.EndCurrentGreyscale();
		SpawnZone.CurZones.Clear();
		foreach (Vector3 b in points)
		{
			foreach (SpawnZone spawnZone in SpawnZone.AllZones)
			{
				if (Vector3.Distance(spawnZone.transform.position, b) < 0.25f)
				{
					SpawnZone.CurZones.Add(spawnZone);
					break;
				}
			}
		}
	}

	// Token: 0x0600099A RID: 2458 RVA: 0x00040068 File Offset: 0x0003E268
	public static void EndCurrentGreyscale()
	{
		if (SpawnZone.CurZones == null || SpawnZone.CurZones.Count == 0)
		{
			return;
		}
		foreach (SpawnZone spawnZone in SpawnZone.CurZones)
		{
			spawnZone.EndGreyscale();
		}
	}

	// Token: 0x0600099B RID: 2459 RVA: 0x000400CC File Offset: 0x0003E2CC
	public static void NextZone()
	{
		if (SpawnZone.AllZones.Count == 0)
		{
			UnityEngine.Debug.LogError("No Spawns Found on Map!");
			return;
		}
		SpawnZone.EndCurrentGreyscale();
		List<SpawnZone> list = SpawnZone.AllZones.Clone<SpawnZone>();
		foreach (SpawnZone item in SpawnZone.CurZones)
		{
			list.Remove(item);
		}
		if (SpawnZone.RequiredSubGroup > 0)
		{
			for (int i = list.Count - 1; i >= 0; i--)
			{
				if (list[i].SubGroup != SpawnZone.RequiredSubGroup)
				{
					list.RemoveAt(i);
				}
			}
		}
		if (list.Count == 0)
		{
			return;
		}
		if (GoalManager.HasObjective && GoalManager.StartedObjective && !GoalManager.ObjectiveCompleted)
		{
			list.Sort((SpawnZone x, SpawnZone y) => Vector3.Distance(x.Point, GoalManager.ObjectiveLocation).CompareTo(Vector3.Distance(y.Point, GoalManager.ObjectiveLocation)));
			for (int j = list.Count - 1; j > 2; j--)
			{
				list.RemoveAt(j);
			}
		}
		int num = (PlayerControl.PlayerCount > 3) ? 2 : 1;
		SpawnZone.CurZones.Clear();
		int num2 = 0;
		while (num2 < num && list.Count != 0)
		{
			SpawnZone item2 = list[UnityEngine.Random.Range(0, list.Count)];
			list.Remove(item2);
			SpawnZone.CurZones.Add(item2);
			num2++;
		}
	}

	// Token: 0x0600099C RID: 2460 RVA: 0x0004023C File Offset: 0x0003E43C
	public static List<SpawnPoint> GetCurrentValidSpawns(SpawnType spawnType, EnemyLevel level = EnemyLevel.None)
	{
		if (SpawnZone.CurZones.Count == 0)
		{
			return null;
		}
		List<SpawnPoint> list = new List<SpawnPoint>();
		foreach (SpawnZone spawnZone in SpawnZone.CurZones)
		{
			list.AddRange(spawnZone.GetValidSpawns(spawnType, level));
		}
		return list;
	}

	// Token: 0x0600099D RID: 2461 RVA: 0x000402AC File Offset: 0x0003E4AC
	private List<SpawnPoint> GetValidSpawns(SpawnType spawnType, EnemyLevel level)
	{
		List<SpawnPoint> list = new List<SpawnPoint>();
		foreach (SpawnPoint spawnPoint in this.Spawns)
		{
			if (!(spawnPoint == null) && spawnPoint.SpawnType == spawnType && (!spawnPoint.IsAISpawn || spawnPoint.SpawnLevel == EnemyLevel.None || level == EnemyLevel.None || spawnPoint.SpawnLevel.HasFlag(level)) && Time.realtimeSinceStartup - spawnPoint.LastSpawnedTime >= 1.5f)
			{
				list.Add(spawnPoint);
			}
		}
		return list;
	}

	// Token: 0x0600099E RID: 2462 RVA: 0x00040358 File Offset: 0x0003E558
	public void AutoCollectSpawns()
	{
		this.Spawns.Clear();
		foreach (SpawnPoint spawnPoint in UnityEngine.Object.FindObjectsOfType<SpawnPoint>())
		{
			if (spawnPoint.IsAISpawn && spawnPoint.SpawnType != SpawnType.AI_Flying)
			{
				float num = Vector3.Distance(spawnPoint.point, base.transform.position);
				float num2 = Mathf.Abs(spawnPoint.point.y - base.transform.position.y);
				if (num <= this.Distance && num2 <= this.Height)
				{
					this.Spawns.Add(spawnPoint);
				}
			}
		}
	}

	// Token: 0x0600099F RID: 2463 RVA: 0x000403EE File Offset: 0x0003E5EE
	private void OnDrawGizmos()
	{
	}

	// Token: 0x060009A0 RID: 2464 RVA: 0x000403F0 File Offset: 0x0003E5F0
	public void StartGreyscale()
	{
		base.StopAllCoroutines();
		base.StartCoroutine("GreyscaleIn");
	}

	// Token: 0x060009A1 RID: 2465 RVA: 0x00040404 File Offset: 0x0003E604
	public void EndGreyscale()
	{
		if (this.zone == null || this.isFadingOut)
		{
			return;
		}
		this.isFadingOut = true;
		base.StopAllCoroutines();
		base.StartCoroutine("GreyScaleOut");
	}

	// Token: 0x060009A2 RID: 2466 RVA: 0x00040430 File Offset: 0x0003E630
	private IEnumerator GreyscaleIn()
	{
		if (this.zone == null)
		{
			this.zone = GreyscaleAreas.AddZone(base.transform.position, 0f);
		}
		if (this.zoneBorder == null)
		{
			this.zoneBorder = UnityEngine.Object.Instantiate<GameObject>(AIManager.instance.DB.SpawnRingDisplay, base.transform.position, Quaternion.identity).GetComponent<AuraController>();
		}
		this.zoneBorder.transform.position = base.transform.position;
		this.zoneBorder.transform.localScale = Vector3.one * 0.5f;
		this.zoneBorder.Opacity = 1f;
		this.zone.Update(base.transform.position, 0f);
		yield return new WaitForSeconds(0.66f);
		float t = 0f;
		while (t < 0.98f)
		{
			t = Mathf.Lerp(t, 1f, Time.deltaTime * 0.5f);
			this.zone.Update(base.transform.position, Mathf.Lerp(0f, this.Distance, t));
			this.zoneBorder.transform.localScale = Vector3.one * t * this.Distance;
			GreyscaleAreas.UpdateZones(0f);
			yield return true;
		}
		yield break;
	}

	// Token: 0x060009A3 RID: 2467 RVA: 0x0004043F File Offset: 0x0003E63F
	private IEnumerator GreyScaleOut()
	{
		if (this.zone == null)
		{
			yield break;
		}
		float collapseTime = 3f;
		float t = 1f;
		yield return new WaitForSeconds(0.5f);
		while (t > 0f)
		{
			t -= Time.deltaTime / collapseTime;
			this.zone.Update(base.transform.position, Mathf.Lerp(0f, this.Distance, t));
			GreyscaleAreas.UpdateZones(0f);
			if (this.zoneBorder != null && this.zoneBorder.Opacity > 0f)
			{
				this.zoneBorder.Opacity -= Time.deltaTime;
			}
			yield return true;
		}
		GreyscaleAreas.RemoveZone(this.zone);
		this.zone = null;
		this.isFadingOut = false;
		if (this.zoneBorder != null)
		{
			UnityEngine.Object.Destroy(this.zoneBorder.gameObject);
		}
		yield break;
	}

	// Token: 0x060009A4 RID: 2468 RVA: 0x0004044E File Offset: 0x0003E64E
	public SpawnZone()
	{
	}

	// Token: 0x060009A5 RID: 2469 RVA: 0x00040477 File Offset: 0x0003E677
	// Note: this type is marked as 'beforefieldinit'.
	static SpawnZone()
	{
	}

	// Token: 0x040007FA RID: 2042
	public List<SpawnPoint> Spawns = new List<SpawnPoint>();

	// Token: 0x040007FB RID: 2043
	public static List<SpawnZone> AllZones = new List<SpawnZone>();

	// Token: 0x040007FC RID: 2044
	public static List<SpawnZone> CurZones = new List<SpawnZone>();

	// Token: 0x040007FD RID: 2045
	[Range(5f, 90f)]
	public float Distance = 35f;

	// Token: 0x040007FE RID: 2046
	public float Height = 30f;

	// Token: 0x040007FF RID: 2047
	public static int RequiredSubGroup = 0;

	// Token: 0x04000800 RID: 2048
	public int SubGroup;

	// Token: 0x04000801 RID: 2049
	public static bool DebugShowAll;

	// Token: 0x04000802 RID: 2050
	private GreyscaleAreas.GSZone zone;

	// Token: 0x04000803 RID: 2051
	private bool isFadingOut;

	// Token: 0x04000804 RID: 2052
	private AuraController zoneBorder;

	// Token: 0x020004CB RID: 1227
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		// Token: 0x060022D2 RID: 8914 RVA: 0x000C7B5F File Offset: 0x000C5D5F
		// Note: this type is marked as 'beforefieldinit'.
		static <>c()
		{
		}

		// Token: 0x060022D3 RID: 8915 RVA: 0x000C7B6B File Offset: 0x000C5D6B
		public <>c()
		{
		}

		// Token: 0x060022D4 RID: 8916 RVA: 0x000C7B74 File Offset: 0x000C5D74
		internal int <Awake>b__14_0(SpawnZone x, SpawnZone y)
		{
			return Vector3.Distance(x.transform.position, SpawnZone.SORT_FROM).CompareTo(Vector3.Distance(y.transform.position, SpawnZone.SORT_FROM));
		}

		// Token: 0x060022D5 RID: 8917 RVA: 0x000C7BB4 File Offset: 0x000C5DB4
		internal int <NextZone>b__18_0(SpawnZone x, SpawnZone y)
		{
			return Vector3.Distance(x.Point, GoalManager.ObjectiveLocation).CompareTo(Vector3.Distance(y.Point, GoalManager.ObjectiveLocation));
		}

		// Token: 0x04002459 RID: 9305
		public static readonly SpawnZone.<>c <>9 = new SpawnZone.<>c();

		// Token: 0x0400245A RID: 9306
		public static Comparison<SpawnZone> <>9__14_0;

		// Token: 0x0400245B RID: 9307
		public static Comparison<SpawnZone> <>9__18_0;
	}

	// Token: 0x020004CC RID: 1228
	[CompilerGenerated]
	private sealed class <GreyscaleIn>d__28 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060022D6 RID: 8918 RVA: 0x000C7BE9 File Offset: 0x000C5DE9
		[DebuggerHidden]
		public <GreyscaleIn>d__28(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060022D7 RID: 8919 RVA: 0x000C7BF8 File Offset: 0x000C5DF8
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060022D8 RID: 8920 RVA: 0x000C7BFC File Offset: 0x000C5DFC
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			SpawnZone spawnZone = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				if (spawnZone.zone == null)
				{
					spawnZone.zone = GreyscaleAreas.AddZone(spawnZone.transform.position, 0f);
				}
				if (spawnZone.zoneBorder == null)
				{
					spawnZone.zoneBorder = UnityEngine.Object.Instantiate<GameObject>(AIManager.instance.DB.SpawnRingDisplay, spawnZone.transform.position, Quaternion.identity).GetComponent<AuraController>();
				}
				spawnZone.zoneBorder.transform.position = spawnZone.transform.position;
				spawnZone.zoneBorder.transform.localScale = Vector3.one * 0.5f;
				spawnZone.zoneBorder.Opacity = 1f;
				spawnZone.zone.Update(spawnZone.transform.position, 0f);
				this.<>2__current = new WaitForSeconds(0.66f);
				this.<>1__state = 1;
				return true;
			case 1:
				this.<>1__state = -1;
				t = 0f;
				break;
			case 2:
				this.<>1__state = -1;
				break;
			default:
				return false;
			}
			if (t >= 0.98f)
			{
				return false;
			}
			t = Mathf.Lerp(t, 1f, Time.deltaTime * 0.5f);
			spawnZone.zone.Update(spawnZone.transform.position, Mathf.Lerp(0f, spawnZone.Distance, t));
			spawnZone.zoneBorder.transform.localScale = Vector3.one * t * spawnZone.Distance;
			GreyscaleAreas.UpdateZones(0f);
			this.<>2__current = true;
			this.<>1__state = 2;
			return true;
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x060022D9 RID: 8921 RVA: 0x000C7DD6 File Offset: 0x000C5FD6
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060022DA RID: 8922 RVA: 0x000C7DDE File Offset: 0x000C5FDE
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x060022DB RID: 8923 RVA: 0x000C7DE5 File Offset: 0x000C5FE5
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0400245C RID: 9308
		private int <>1__state;

		// Token: 0x0400245D RID: 9309
		private object <>2__current;

		// Token: 0x0400245E RID: 9310
		public SpawnZone <>4__this;

		// Token: 0x0400245F RID: 9311
		private float <t>5__2;
	}

	// Token: 0x020004CD RID: 1229
	[CompilerGenerated]
	private sealed class <GreyScaleOut>d__29 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060022DC RID: 8924 RVA: 0x000C7DED File Offset: 0x000C5FED
		[DebuggerHidden]
		public <GreyScaleOut>d__29(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060022DD RID: 8925 RVA: 0x000C7DFC File Offset: 0x000C5FFC
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060022DE RID: 8926 RVA: 0x000C7E00 File Offset: 0x000C6000
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			SpawnZone spawnZone = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				if (spawnZone.zone == null)
				{
					return false;
				}
				collapseTime = 3f;
				t = 1f;
				this.<>2__current = new WaitForSeconds(0.5f);
				this.<>1__state = 1;
				return true;
			case 1:
				this.<>1__state = -1;
				break;
			case 2:
				this.<>1__state = -1;
				break;
			default:
				return false;
			}
			if (t <= 0f)
			{
				GreyscaleAreas.RemoveZone(spawnZone.zone);
				spawnZone.zone = null;
				spawnZone.isFadingOut = false;
				if (spawnZone.zoneBorder != null)
				{
					UnityEngine.Object.Destroy(spawnZone.zoneBorder.gameObject);
				}
				return false;
			}
			t -= Time.deltaTime / collapseTime;
			spawnZone.zone.Update(spawnZone.transform.position, Mathf.Lerp(0f, spawnZone.Distance, t));
			GreyscaleAreas.UpdateZones(0f);
			if (spawnZone.zoneBorder != null && spawnZone.zoneBorder.Opacity > 0f)
			{
				spawnZone.zoneBorder.Opacity -= Time.deltaTime;
			}
			this.<>2__current = true;
			this.<>1__state = 2;
			return true;
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x060022DF RID: 8927 RVA: 0x000C7F65 File Offset: 0x000C6165
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060022E0 RID: 8928 RVA: 0x000C7F6D File Offset: 0x000C616D
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x060022E1 RID: 8929 RVA: 0x000C7F74 File Offset: 0x000C6174
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002460 RID: 9312
		private int <>1__state;

		// Token: 0x04002461 RID: 9313
		private object <>2__current;

		// Token: 0x04002462 RID: 9314
		public SpawnZone <>4__this;

		// Token: 0x04002463 RID: 9315
		private float <collapseTime>5__2;

		// Token: 0x04002464 RID: 9316
		private float <t>5__3;
	}
}
