using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000250 RID: 592
public class GreyscaleAreas
{
	// Token: 0x060017E8 RID: 6120 RVA: 0x00095950 File Offset: 0x00093B50
	public static void Init()
	{
		GreyscaleAreas.Initialized = true;
		GreyscaleAreas.Zones = new List<GreyscaleAreas.GSZone>();
		Shader.SetGlobalVectorArray(GreyscaleAreas.GsVector, new Vector4[16]);
	}

	// Token: 0x060017E9 RID: 6121 RVA: 0x00095974 File Offset: 0x00093B74
	public static GreyscaleAreas.GSZone AddZone(Vector3 pos, float radius)
	{
		if (GreyscaleAreas.Zones == null)
		{
			GreyscaleAreas.Zones = new List<GreyscaleAreas.GSZone>();
		}
		GreyscaleAreas.GSZone gszone = new GreyscaleAreas.GSZone();
		gszone.Update(pos, radius);
		if (GreyscaleAreas.Zones.Count >= 16)
		{
			GreyscaleAreas.Zones.Sort((GreyscaleAreas.GSZone x, GreyscaleAreas.GSZone y) => x.lastUpdateTime.CompareTo(y.lastUpdateTime));
			GreyscaleAreas.Zones.RemoveAt(0);
		}
		GreyscaleAreas.Zones.Add(gszone);
		GreyscaleAreas.UpdateZones(0f);
		return gszone;
	}

	// Token: 0x060017EA RID: 6122 RVA: 0x000959F8 File Offset: 0x00093BF8
	public static void RemoveZone(GreyscaleAreas.GSZone zone)
	{
		if (GreyscaleAreas.Zones == null)
		{
			return;
		}
		GreyscaleAreas.Zones.Remove(zone);
		GreyscaleAreas.UpdateZones(0f);
	}

	// Token: 0x060017EB RID: 6123 RVA: 0x00095A18 File Offset: 0x00093C18
	public static void ClearZones()
	{
		if (GreyscaleAreas.Zones == null)
		{
			return;
		}
		GreyscaleAreas.Zones.Clear();
		Shader.SetGlobalFloat(GreyscaleAreas.GsRadMin, 0f);
		Shader.SetGlobalFloat(GreyscaleAreas.GsRadMax, 0f);
		GreyscaleAreas.UpdateZones(0f);
	}

	// Token: 0x060017EC RID: 6124 RVA: 0x00095A54 File Offset: 0x00093C54
	public static void UpdateZones(float innerRadius = 0f)
	{
		if (!GreyscaleAreas.Initialized || GreyscaleAreas.Zones == null)
		{
			GreyscaleAreas.Init();
		}
		Vector4[] array = new Vector4[GreyscaleAreas.Zones.Count];
		for (int i = 0; i < GreyscaleAreas.Zones.Count; i++)
		{
			array[i] = GreyscaleAreas.Zones[i].GetVector();
		}
		Shader.SetGlobalInt(GreyscaleAreas.GsCount, GreyscaleAreas.Zones.Count);
		if (array.Length == 0)
		{
			Shader.SetGlobalFloat(GreyscaleAreas.GsRadMin, 0f);
			Shader.SetGlobalFloat(GreyscaleAreas.GsRadMax, 0f);
		}
		if (array.Length == 1)
		{
			Vector4 vector = GreyscaleAreas.Zones[0].GetVector();
			Shader.SetGlobalVector(GreyscaleAreas.GsSinglePt, vector);
			Shader.SetGlobalFloat(GreyscaleAreas.GsRadMin, innerRadius);
			Shader.SetGlobalFloat(GreyscaleAreas.GsRadMax, vector.w);
			return;
		}
		if (array.Length != 0)
		{
			Shader.SetGlobalVectorArray(GreyscaleAreas.GsVector, array);
		}
	}

	// Token: 0x060017ED RID: 6125 RVA: 0x00095B33 File Offset: 0x00093D33
	public GreyscaleAreas()
	{
	}

	// Token: 0x060017EE RID: 6126 RVA: 0x00095B3C File Offset: 0x00093D3C
	// Note: this type is marked as 'beforefieldinit'.
	static GreyscaleAreas()
	{
	}

	// Token: 0x040017B2 RID: 6066
	private const int MAX_ZONES = 16;

	// Token: 0x040017B3 RID: 6067
	public static List<GreyscaleAreas.GSZone> Zones;

	// Token: 0x040017B4 RID: 6068
	private static readonly int GsCount = Shader.PropertyToID("_GS_Count");

	// Token: 0x040017B5 RID: 6069
	private static readonly int GsVector = Shader.PropertyToID("_GS_Vectors");

	// Token: 0x040017B6 RID: 6070
	private static readonly int GsSinglePt = Shader.PropertyToID("_GS_SinglePt");

	// Token: 0x040017B7 RID: 6071
	private static readonly int GsRadMin = Shader.PropertyToID("_GS_RadMin");

	// Token: 0x040017B8 RID: 6072
	private static readonly int GsRadMax = Shader.PropertyToID("_GS_RadMax");

	// Token: 0x040017B9 RID: 6073
	public static bool Initialized;

	// Token: 0x02000613 RID: 1555
	public class GSZone
	{
		// Token: 0x06002725 RID: 10021 RVA: 0x000D50A9 File Offset: 0x000D32A9
		public void Update(Vector3 p, float r)
		{
			this.pos = p;
			this.radius = r;
			this.lastUpdateTime = Time.realtimeSinceStartup;
		}

		// Token: 0x06002726 RID: 10022 RVA: 0x000D50C4 File Offset: 0x000D32C4
		public Vector4 GetVector()
		{
			return new Vector4(this.pos.x, this.pos.y, this.pos.z, this.radius);
		}

		// Token: 0x06002727 RID: 10023 RVA: 0x000D50F2 File Offset: 0x000D32F2
		public GSZone()
		{
		}

		// Token: 0x040029B6 RID: 10678
		private Vector3 pos;

		// Token: 0x040029B7 RID: 10679
		private float radius;

		// Token: 0x040029B8 RID: 10680
		public float lastUpdateTime = Time.realtimeSinceStartup;
	}

	// Token: 0x02000614 RID: 1556
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		// Token: 0x06002728 RID: 10024 RVA: 0x000D5105 File Offset: 0x000D3305
		// Note: this type is marked as 'beforefieldinit'.
		static <>c()
		{
		}

		// Token: 0x06002729 RID: 10025 RVA: 0x000D5111 File Offset: 0x000D3311
		public <>c()
		{
		}

		// Token: 0x0600272A RID: 10026 RVA: 0x000D5119 File Offset: 0x000D3319
		internal int <AddZone>b__9_0(GreyscaleAreas.GSZone x, GreyscaleAreas.GSZone y)
		{
			return x.lastUpdateTime.CompareTo(y.lastUpdateTime);
		}

		// Token: 0x040029B9 RID: 10681
		public static readonly GreyscaleAreas.<>c <>9 = new GreyscaleAreas.<>c();

		// Token: 0x040029BA RID: 10682
		public static Comparison<GreyscaleAreas.GSZone> <>9__9_0;
	}
}
