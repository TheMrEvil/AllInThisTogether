using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000066 RID: 102
	public sealed class WingedEdge : IEquatable<WingedEdge>
	{
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x00024060 File Offset: 0x00022260
		// (set) Token: 0x0600040D RID: 1037 RVA: 0x00024068 File Offset: 0x00022268
		public EdgeLookup edge
		{
			[CompilerGenerated]
			get
			{
				return this.<edge>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<edge>k__BackingField = value;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x00024071 File Offset: 0x00022271
		// (set) Token: 0x0600040F RID: 1039 RVA: 0x00024079 File Offset: 0x00022279
		public Face face
		{
			[CompilerGenerated]
			get
			{
				return this.<face>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<face>k__BackingField = value;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x00024082 File Offset: 0x00022282
		// (set) Token: 0x06000411 RID: 1041 RVA: 0x0002408A File Offset: 0x0002228A
		public WingedEdge next
		{
			[CompilerGenerated]
			get
			{
				return this.<next>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<next>k__BackingField = value;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x00024093 File Offset: 0x00022293
		// (set) Token: 0x06000413 RID: 1043 RVA: 0x0002409B File Offset: 0x0002229B
		public WingedEdge previous
		{
			[CompilerGenerated]
			get
			{
				return this.<previous>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<previous>k__BackingField = value;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x000240A4 File Offset: 0x000222A4
		// (set) Token: 0x06000415 RID: 1045 RVA: 0x000240AC File Offset: 0x000222AC
		public WingedEdge opposite
		{
			[CompilerGenerated]
			get
			{
				return this.<opposite>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<opposite>k__BackingField = value;
			}
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x000240B5 File Offset: 0x000222B5
		private WingedEdge()
		{
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x000240C0 File Offset: 0x000222C0
		public bool Equals(WingedEdge other)
		{
			return other != null && this.edge.local.Equals(other.edge.local);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x000240F8 File Offset: 0x000222F8
		public override bool Equals(object obj)
		{
			WingedEdge wingedEdge = obj as WingedEdge;
			return (wingedEdge != null && this.Equals(wingedEdge)) || (obj is Edge && this.edge.local.Equals((Edge)obj));
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00024144 File Offset: 0x00022344
		public override int GetHashCode()
		{
			return this.edge.local.GetHashCode();
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00024170 File Offset: 0x00022370
		public int Count()
		{
			WingedEdge wingedEdge = this;
			int num = 0;
			do
			{
				num++;
				wingedEdge = wingedEdge.next;
			}
			while (wingedEdge != null && wingedEdge != this);
			return num;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00024194 File Offset: 0x00022394
		public override string ToString()
		{
			return string.Format("Common: {0}\nLocal: {1}\nOpposite: {2}\nFace: {3}", new object[]
			{
				this.edge.common.ToString(),
				this.edge.local.ToString(),
				(this.opposite == null) ? "null" : this.opposite.edge.ToString(),
				this.face.ToString()
			});
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00024228 File Offset: 0x00022428
		internal static int[] MakeQuad(WingedEdge left, WingedEdge right)
		{
			if (left.Count() != 3 || right.Count() != 3)
			{
				return null;
			}
			EdgeLookup[] array = new EdgeLookup[]
			{
				left.edge,
				left.next.edge,
				left.next.next.edge,
				right.edge,
				right.next.edge,
				right.next.next.edge
			};
			int[] array2 = new int[6];
			int num = 0;
			for (int i = 0; i < 3; i++)
			{
				for (int j = 3; j < 6; j++)
				{
					if (array[i].Equals(array[j]))
					{
						num++;
						array2[i] = 1;
						array2[j] = 1;
						break;
					}
				}
			}
			if (num != 1)
			{
				return null;
			}
			int num2 = 0;
			EdgeLookup[] array3 = new EdgeLookup[4];
			for (int k = 0; k < 6; k++)
			{
				if (array2[k] < 1)
				{
					array3[num2++] = array[k];
				}
			}
			int[] array4 = new int[]
			{
				array3[0].local.a,
				array3[0].local.b,
				-1,
				-1
			};
			int b = array3[0].common.b;
			int num3 = -1;
			if (array3[1].common.a == b)
			{
				array4[2] = array3[1].local.b;
				num3 = array3[1].common.b;
			}
			else if (array3[2].common.a == b)
			{
				array4[2] = array3[2].local.b;
				num3 = array3[2].common.b;
			}
			else if (array3[3].common.a == b)
			{
				array4[2] = array3[3].local.b;
				num3 = array3[3].common.b;
			}
			if (array3[1].common.a == num3)
			{
				array4[3] = array3[1].local.b;
			}
			else if (array3[2].common.a == num3)
			{
				array4[3] = array3[2].local.b;
			}
			else if (array3[3].common.a == num3)
			{
				array4[3] = array3[3].local.b;
			}
			if (array4[2] == -1 || array4[3] == -1)
			{
				return null;
			}
			return array4;
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x000244FC File Offset: 0x000226FC
		public WingedEdge GetAdjacentEdgeWithCommonIndex(int common)
		{
			if (this.next.edge.common.Contains(common))
			{
				return this.next;
			}
			if (this.previous.edge.common.Contains(common))
			{
				return this.previous;
			}
			return null;
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00024554 File Offset: 0x00022754
		public static List<Edge> SortEdgesByAdjacency(Face face)
		{
			if (face == null || face.edgesInternal == null)
			{
				throw new ArgumentNullException("face");
			}
			List<Edge> list = new List<Edge>(face.edgesInternal);
			WingedEdge.SortEdgesByAdjacency(list);
			return list;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00024580 File Offset: 0x00022780
		public static void SortEdgesByAdjacency(List<Edge> edges)
		{
			if (edges == null)
			{
				throw new ArgumentNullException("edges");
			}
			for (int i = 1; i < edges.Count; i++)
			{
				int b = edges[i - 1].b;
				for (int j = i + 1; j < edges.Count; j++)
				{
					if (edges[j].a == b || edges[j].b == b)
					{
						Edge value = edges[j];
						edges[j] = edges[i];
						edges[i] = value;
					}
				}
			}
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0002460C File Offset: 0x0002280C
		public static Dictionary<int, List<WingedEdge>> GetSpokes(List<WingedEdge> wings)
		{
			if (wings == null)
			{
				throw new ArgumentNullException("wings");
			}
			Dictionary<int, List<WingedEdge>> dictionary = new Dictionary<int, List<WingedEdge>>();
			List<WingedEdge> list = null;
			for (int i = 0; i < wings.Count; i++)
			{
				if (dictionary.TryGetValue(wings[i].edge.common.a, out list))
				{
					list.Add(wings[i]);
				}
				else
				{
					dictionary.Add(wings[i].edge.common.a, new List<WingedEdge>
					{
						wings[i]
					});
				}
				if (dictionary.TryGetValue(wings[i].edge.common.b, out list))
				{
					list.Add(wings[i]);
				}
				else
				{
					dictionary.Add(wings[i].edge.common.b, new List<WingedEdge>
					{
						wings[i]
					});
				}
			}
			return dictionary;
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00024710 File Offset: 0x00022910
		internal static List<int> SortCommonIndexesByAdjacency(List<WingedEdge> wings, HashSet<int> common)
		{
			List<Edge> list = (from x in wings
			where common.Contains(x.edge.common.a) && common.Contains(x.edge.common.b)
			select x into y
			select y.edge.common).ToList<Edge>();
			if (list.Count != common.Count)
			{
				return null;
			}
			WingedEdge.SortEdgesByAdjacency(list);
			return list.ConvertAll<int>((Edge x) => x.a);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x000247A6 File Offset: 0x000229A6
		public static List<WingedEdge> GetWingedEdges(ProBuilderMesh mesh, bool oneWingPerFace = false)
		{
			if (mesh == null)
			{
				throw new ArgumentNullException("mesh");
			}
			return WingedEdge.GetWingedEdges(mesh, mesh.facesInternal, oneWingPerFace);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x000247CC File Offset: 0x000229CC
		public static List<WingedEdge> GetWingedEdges(ProBuilderMesh mesh, IEnumerable<Face> faces, bool oneWingPerFace = false)
		{
			if (mesh == null)
			{
				throw new ArgumentNullException("mesh");
			}
			Dictionary<int, int> sharedVertexLookup = mesh.sharedVertexLookup;
			List<WingedEdge> list = new List<WingedEdge>();
			WingedEdge.k_OppositeEdgeDictionary.Clear();
			foreach (Face face in faces)
			{
				List<Edge> list2 = WingedEdge.SortEdgesByAdjacency(face);
				int count = list2.Count;
				WingedEdge wingedEdge = null;
				WingedEdge wingedEdge2 = null;
				for (int i = 0; i < count; i++)
				{
					Edge edge = list2[i];
					WingedEdge wingedEdge3 = new WingedEdge();
					wingedEdge3.edge = new EdgeLookup(sharedVertexLookup[edge.a], sharedVertexLookup[edge.b], edge.a, edge.b);
					wingedEdge3.face = face;
					if (i < 1)
					{
						wingedEdge = wingedEdge3;
					}
					if (i > 0)
					{
						wingedEdge3.previous = wingedEdge2;
						wingedEdge2.next = wingedEdge3;
					}
					if (i == count - 1)
					{
						wingedEdge3.next = wingedEdge;
						wingedEdge.previous = wingedEdge3;
					}
					wingedEdge2 = wingedEdge3;
					WingedEdge wingedEdge4;
					if (WingedEdge.k_OppositeEdgeDictionary.TryGetValue(wingedEdge3.edge.common, out wingedEdge4))
					{
						wingedEdge4.opposite = wingedEdge3;
						wingedEdge3.opposite = wingedEdge4;
					}
					else
					{
						wingedEdge3.opposite = null;
						WingedEdge.k_OppositeEdgeDictionary.Add(wingedEdge3.edge.common, wingedEdge3);
					}
					if (!oneWingPerFace || i < 1)
					{
						list.Add(wingedEdge3);
					}
				}
			}
			return list;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0002496C File Offset: 0x00022B6C
		// Note: this type is marked as 'beforefieldinit'.
		static WingedEdge()
		{
		}

		// Token: 0x04000226 RID: 550
		private static readonly Dictionary<Edge, WingedEdge> k_OppositeEdgeDictionary = new Dictionary<Edge, WingedEdge>();

		// Token: 0x04000227 RID: 551
		[CompilerGenerated]
		private EdgeLookup <edge>k__BackingField;

		// Token: 0x04000228 RID: 552
		[CompilerGenerated]
		private Face <face>k__BackingField;

		// Token: 0x04000229 RID: 553
		[CompilerGenerated]
		private WingedEdge <next>k__BackingField;

		// Token: 0x0400022A RID: 554
		[CompilerGenerated]
		private WingedEdge <previous>k__BackingField;

		// Token: 0x0400022B RID: 555
		[CompilerGenerated]
		private WingedEdge <opposite>k__BackingField;

		// Token: 0x020000A9 RID: 169
		[CompilerGenerated]
		private sealed class <>c__DisplayClass32_0
		{
			// Token: 0x06000564 RID: 1380 RVA: 0x00035F40 File Offset: 0x00034140
			public <>c__DisplayClass32_0()
			{
			}

			// Token: 0x06000565 RID: 1381 RVA: 0x00035F48 File Offset: 0x00034148
			internal bool <SortCommonIndexesByAdjacency>b__0(WingedEdge x)
			{
				return this.common.Contains(x.edge.common.a) && this.common.Contains(x.edge.common.b);
			}

			// Token: 0x040002C4 RID: 708
			public HashSet<int> common;
		}

		// Token: 0x020000AA RID: 170
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000566 RID: 1382 RVA: 0x00035F95 File Offset: 0x00034195
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000567 RID: 1383 RVA: 0x00035FA1 File Offset: 0x000341A1
			public <>c()
			{
			}

			// Token: 0x06000568 RID: 1384 RVA: 0x00035FAC File Offset: 0x000341AC
			internal Edge <SortCommonIndexesByAdjacency>b__32_1(WingedEdge y)
			{
				return y.edge.common;
			}

			// Token: 0x06000569 RID: 1385 RVA: 0x00035FC7 File Offset: 0x000341C7
			internal int <SortCommonIndexesByAdjacency>b__32_2(Edge x)
			{
				return x.a;
			}

			// Token: 0x040002C5 RID: 709
			public static readonly WingedEdge.<>c <>9 = new WingedEdge.<>c();

			// Token: 0x040002C6 RID: 710
			public static Func<WingedEdge, Edge> <>9__32_1;

			// Token: 0x040002C7 RID: 711
			public static Converter<Edge, int> <>9__32_2;
		}
	}
}
