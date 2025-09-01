using System;
using System.Collections.Generic;
using UnityEngine;

namespace Onager.FXMesh
{
	// Token: 0x02000165 RID: 357
	[CreateAssetMenu(menuName = "Data/FX Mesh Preset", order = 337)]
	public class FXMeshSettings : ScriptableObject
	{
		// Token: 0x06000DB3 RID: 3507 RVA: 0x0005CA50 File Offset: 0x0005AC50
		public Mesh GetMesh(bool forceRebuild = false)
		{
			Mesh mesh;
			if (FXMeshSettings.GeneratedMeshes.ContainsKey(this))
			{
				mesh = FXMeshSettings.GeneratedMeshes[this];
				if (!forceRebuild && !this.dirty && mesh != null)
				{
					return mesh;
				}
			}
			else
			{
				mesh = new Mesh();
				FXMeshSettings.GeneratedMeshes.Add(this, mesh);
			}
			if (mesh == null)
			{
				mesh = new Mesh();
			}
			mesh.name = base.name;
			mesh.Clear();
			FXMeshSettings.vertexLUT.Clear();
			FXMeshSettings.vertices.Clear();
			FXMeshSettings.triangles.Clear();
			FXMeshSettings.colors.Clear();
			FXMeshSettings.uv0.Clear();
			FXMeshSettings.uv1.Clear();
			FXMeshSettings.uv2.Clear();
			FXMeshSettings.uv3.Clear();
			for (int i = 0; i < this.rings; i++)
			{
				for (int j = 0; j < this.loops; j++)
				{
					this.AddQuad(i, j);
				}
			}
			mesh.SetVertices(FXMeshSettings.vertices);
			mesh.SetTriangles(FXMeshSettings.triangles, 0);
			if (this.Color.Enabled)
			{
				mesh.SetColors(FXMeshSettings.colors);
			}
			if (this.UV0.Enabled)
			{
				mesh.SetUVs(0, FXMeshSettings.uv0);
			}
			if (this.UV1.Enabled)
			{
				mesh.SetUVs(1, FXMeshSettings.uv1);
			}
			if (this.UV2.Enabled)
			{
				mesh.SetUVs(2, FXMeshSettings.uv2);
			}
			if (this.UV3.Enabled)
			{
				mesh.SetUVs(3, FXMeshSettings.uv3);
			}
			if (this.computeNormals)
			{
				mesh.RecalculateNormals();
			}
			mesh.UploadMeshData(false);
			this.dirty = false;
			return mesh;
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x0005CBEC File Offset: 0x0005ADEC
		private void InsertUniqueVertex(FXMeshSettings.PolarCoords coords)
		{
			if (FXMeshSettings.vertexLUT.ContainsKey(coords))
			{
				return;
			}
			float f = this.GetAngle(coords) * 0.017453292f;
			Vector3 a = new Vector3(Mathf.Cos(f), 0f, Mathf.Sin(f));
			Vector3 a2 = a * this.startRadius + a * this.GetLength((float)coords.ring);
			Vector3 b = Vector3.up * this.GetHeight((a2.magnitude - this.startRadius) / this.endRadius);
			FXMeshSettings.vertices.Add(a2 + b);
			FXMeshSettings.vertexLUT.Add(coords, FXMeshSettings.vertices.Count - 1);
			this.GetColor(this.Color, coords, FXMeshSettings.colors);
			this.GetUV(this.UV0, coords, FXMeshSettings.uv0);
			this.GetUV(this.UV1, coords, FXMeshSettings.uv1);
			this.GetUV(this.UV2, coords, FXMeshSettings.uv2);
			this.GetUV(this.UV3, coords, FXMeshSettings.uv3);
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x0005CCFC File Offset: 0x0005AEFC
		private float GetLength(float ring)
		{
			float time = ring / (float)this.rings;
			float num = this.endRadius;
			return this.ringProfile.Evaluate(time) * (num / 2f);
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x0005CD2E File Offset: 0x0005AF2E
		private float GetHeight(float progress)
		{
			return this.heightProfile.Evaluate(progress) * this.height;
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x0005CD44 File Offset: 0x0005AF44
		private float GetAngle(FXMeshSettings.PolarCoords coords)
		{
			float time = (float)coords.ring / (float)this.rings;
			float num = this.twistProfile.Evaluate(time) * this.twist;
			float num2 = this.maxAngle / (float)this.loops;
			return num2 * (float)coords.loop % (this.maxAngle + num2) + num;
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x0005CD98 File Offset: 0x0005AF98
		private void GetUV(FXMeshSettings.ChannelInfo info, FXMeshSettings.PolarCoords coords, List<Vector4> target)
		{
			if (info.Mode == FXMeshSettings.ChannelMode.None)
			{
				return;
			}
			float num = (float)coords.ring / (float)this.rings;
			float num2 = (float)coords.loop / (float)this.loops;
			if (info.Mode == FXMeshSettings.ChannelMode.Gradient)
			{
				float time = info.Gradient_Horizontal ? num2 : num;
				target.Add(info.Gradient.Evaluate(time));
			}
			if (info.Mode == FXMeshSettings.ChannelMode.Curves)
			{
				target.Add(new Vector4
				{
					x = info.R.Evaluate(info.R_Horizontal ? num2 : num),
					y = info.G.Evaluate(info.G_Horizontal ? num2 : num),
					z = info.B.Evaluate(info.B_Horizontal ? num2 : num),
					w = info.A.Evaluate(info.A_Horizontal ? num2 : num)
				});
			}
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x0005CE90 File Offset: 0x0005B090
		private void GetColor(FXMeshSettings.ChannelInfo info, FXMeshSettings.PolarCoords coords, List<Color> target)
		{
			if (info.Mode == FXMeshSettings.ChannelMode.None)
			{
				return;
			}
			float num = (float)coords.ring / (float)this.rings;
			float num2 = (float)coords.loop / (float)this.loops;
			if (info.Mode == FXMeshSettings.ChannelMode.Gradient)
			{
				float time = info.Gradient_Horizontal ? num2 : num;
				target.Add(info.Gradient.Evaluate(time));
			}
			if (info.Mode == FXMeshSettings.ChannelMode.Curves)
			{
				target.Add(new Color
				{
					r = info.R.Evaluate(info.R_Horizontal ? num2 : num),
					g = info.G.Evaluate(info.G_Horizontal ? num2 : num),
					b = info.B.Evaluate(info.B_Horizontal ? num2 : num),
					a = info.A.Evaluate(info.A_Horizontal ? num2 : num)
				});
			}
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x0005CF81 File Offset: 0x0005B181
		private int GetNextLoop(int currentLoop)
		{
			return currentLoop + 1;
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x0005CF88 File Offset: 0x0005B188
		private void AddQuad(int ring, int loop)
		{
			int nextLoop = this.GetNextLoop(loop);
			int ring2 = ring + 1;
			FXMeshSettings.PolarCoords polarCoords = new FXMeshSettings.PolarCoords(ring2, loop);
			FXMeshSettings.PolarCoords polarCoords2 = new FXMeshSettings.PolarCoords(ring, loop);
			FXMeshSettings.PolarCoords polarCoords3 = new FXMeshSettings.PolarCoords(ring, nextLoop);
			FXMeshSettings.PolarCoords polarCoords4 = new FXMeshSettings.PolarCoords(ring2, nextLoop);
			this.InsertUniqueVertex(polarCoords);
			this.InsertUniqueVertex(polarCoords2);
			this.InsertUniqueVertex(polarCoords3);
			this.InsertUniqueVertex(polarCoords4);
			FXMeshSettings.triangles.Add(FXMeshSettings.vertexLUT[polarCoords]);
			FXMeshSettings.triangles.Add(FXMeshSettings.vertexLUT[polarCoords2]);
			FXMeshSettings.triangles.Add(FXMeshSettings.vertexLUT[polarCoords3]);
			FXMeshSettings.triangles.Add(FXMeshSettings.vertexLUT[polarCoords]);
			FXMeshSettings.triangles.Add(FXMeshSettings.vertexLUT[polarCoords3]);
			FXMeshSettings.triangles.Add(FXMeshSettings.vertexLUT[polarCoords4]);
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x0005D064 File Offset: 0x0005B264
		private void OnValidate()
		{
			this.dirty = true;
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x0005D070 File Offset: 0x0005B270
		public FXMeshSettings()
		{
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x0005D104 File Offset: 0x0005B304
		// Note: this type is marked as 'beforefieldinit'.
		static FXMeshSettings()
		{
		}

		// Token: 0x04000BA8 RID: 2984
		[Header("Radius")]
		[Min(0f)]
		public float startRadius;

		// Token: 0x04000BA9 RID: 2985
		[Min(0.001f)]
		public float endRadius = 10f;

		// Token: 0x04000BAA RID: 2986
		[Range(0.001f, 360f)]
		public float maxAngle = 360f;

		// Token: 0x04000BAB RID: 2987
		[Header("Topology")]
		[Range(3f, 128f)]
		public int loops = 8;

		// Token: 0x04000BAC RID: 2988
		[Range(2f, 128f)]
		public int rings = 3;

		// Token: 0x04000BAD RID: 2989
		public AnimationCurve ringProfile = AnimationCurve.Linear(0f, 0f, 1f, 1f);

		// Token: 0x04000BAE RID: 2990
		[Header("Height")]
		public float height;

		// Token: 0x04000BAF RID: 2991
		public AnimationCurve heightProfile = AnimationCurve.Linear(0f, 0f, 1f, 1f);

		// Token: 0x04000BB0 RID: 2992
		[Header("Normals")]
		public bool computeNormals;

		// Token: 0x04000BB1 RID: 2993
		[Header("Twist")]
		public float twist;

		// Token: 0x04000BB2 RID: 2994
		public AnimationCurve twistProfile = AnimationCurve.Linear(0f, 0f, 0f, 0f);

		// Token: 0x04000BB3 RID: 2995
		public FXMeshSettings.ChannelInfo Color;

		// Token: 0x04000BB4 RID: 2996
		public FXMeshSettings.ChannelInfo UV0;

		// Token: 0x04000BB5 RID: 2997
		public FXMeshSettings.ChannelInfo UV1;

		// Token: 0x04000BB6 RID: 2998
		public FXMeshSettings.ChannelInfo UV2;

		// Token: 0x04000BB7 RID: 2999
		public FXMeshSettings.ChannelInfo UV3;

		// Token: 0x04000BB8 RID: 3000
		public static Dictionary<FXMeshSettings, Mesh> GeneratedMeshes = new Dictionary<FXMeshSettings, Mesh>();

		// Token: 0x04000BB9 RID: 3001
		private static Dictionary<FXMeshSettings.PolarCoords, int> vertexLUT = new Dictionary<FXMeshSettings.PolarCoords, int>();

		// Token: 0x04000BBA RID: 3002
		private static List<Vector3> vertices = new List<Vector3>();

		// Token: 0x04000BBB RID: 3003
		private static List<Color> colors = new List<Color>();

		// Token: 0x04000BBC RID: 3004
		private static List<Vector4> uv0 = new List<Vector4>();

		// Token: 0x04000BBD RID: 3005
		private static List<Vector4> uv1 = new List<Vector4>();

		// Token: 0x04000BBE RID: 3006
		private static List<Vector4> uv2 = new List<Vector4>();

		// Token: 0x04000BBF RID: 3007
		private static List<Vector4> uv3 = new List<Vector4>();

		// Token: 0x04000BC0 RID: 3008
		private static List<int> triangles = new List<int>();

		// Token: 0x04000BC1 RID: 3009
		private bool dirty;

		// Token: 0x0200023F RID: 575
		public enum ChannelMode
		{
			// Token: 0x040010D9 RID: 4313
			None,
			// Token: 0x040010DA RID: 4314
			Gradient,
			// Token: 0x040010DB RID: 4315
			Curves
		}

		// Token: 0x02000240 RID: 576
		[Serializable]
		public struct ChannelInfo
		{
			// Token: 0x17000265 RID: 613
			// (get) Token: 0x060011D2 RID: 4562 RVA: 0x0006E6DE File Offset: 0x0006C8DE
			public bool Enabled
			{
				get
				{
					return this.Mode > FXMeshSettings.ChannelMode.None;
				}
			}

			// Token: 0x040010DC RID: 4316
			public FXMeshSettings.ChannelMode Mode;

			// Token: 0x040010DD RID: 4317
			public Gradient Gradient;

			// Token: 0x040010DE RID: 4318
			public bool Gradient_Horizontal;

			// Token: 0x040010DF RID: 4319
			public AnimationCurve R;

			// Token: 0x040010E0 RID: 4320
			public bool R_Horizontal;

			// Token: 0x040010E1 RID: 4321
			public AnimationCurve G;

			// Token: 0x040010E2 RID: 4322
			public bool G_Horizontal;

			// Token: 0x040010E3 RID: 4323
			public AnimationCurve B;

			// Token: 0x040010E4 RID: 4324
			public bool B_Horizontal;

			// Token: 0x040010E5 RID: 4325
			public AnimationCurve A;

			// Token: 0x040010E6 RID: 4326
			public bool A_Horizontal;
		}

		// Token: 0x02000241 RID: 577
		private struct PolarCoords
		{
			// Token: 0x060011D3 RID: 4563 RVA: 0x0006E6E9 File Offset: 0x0006C8E9
			public PolarCoords(int ring, int theta)
			{
				this.ring = ring;
				this.loop = theta;
			}

			// Token: 0x060011D4 RID: 4564 RVA: 0x0006E6F9 File Offset: 0x0006C8F9
			public override string ToString()
			{
				return string.Format("{0};{1}", this.ring, this.loop);
			}

			// Token: 0x040010E7 RID: 4327
			public int ring;

			// Token: 0x040010E8 RID: 4328
			public int loop;
		}
	}
}
