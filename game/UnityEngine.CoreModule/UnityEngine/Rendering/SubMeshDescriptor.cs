using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering
{
	// Token: 0x020003C0 RID: 960
	public struct SubMeshDescriptor
	{
		// Token: 0x06001F5A RID: 8026 RVA: 0x00033254 File Offset: 0x00031454
		public SubMeshDescriptor(int indexStart, int indexCount, MeshTopology topology = MeshTopology.Triangles)
		{
			this.indexStart = indexStart;
			this.indexCount = indexCount;
			this.topology = topology;
			this.bounds = default(Bounds);
			this.baseVertex = 0;
			this.firstVertex = 0;
			this.vertexCount = 0;
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06001F5B RID: 8027 RVA: 0x000332A2 File Offset: 0x000314A2
		// (set) Token: 0x06001F5C RID: 8028 RVA: 0x000332AA File Offset: 0x000314AA
		public Bounds bounds
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<bounds>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<bounds>k__BackingField = value;
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06001F5D RID: 8029 RVA: 0x000332B3 File Offset: 0x000314B3
		// (set) Token: 0x06001F5E RID: 8030 RVA: 0x000332BB File Offset: 0x000314BB
		public MeshTopology topology
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<topology>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<topology>k__BackingField = value;
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06001F5F RID: 8031 RVA: 0x000332C4 File Offset: 0x000314C4
		// (set) Token: 0x06001F60 RID: 8032 RVA: 0x000332CC File Offset: 0x000314CC
		public int indexStart
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<indexStart>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<indexStart>k__BackingField = value;
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06001F61 RID: 8033 RVA: 0x000332D5 File Offset: 0x000314D5
		// (set) Token: 0x06001F62 RID: 8034 RVA: 0x000332DD File Offset: 0x000314DD
		public int indexCount
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<indexCount>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<indexCount>k__BackingField = value;
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06001F63 RID: 8035 RVA: 0x000332E6 File Offset: 0x000314E6
		// (set) Token: 0x06001F64 RID: 8036 RVA: 0x000332EE File Offset: 0x000314EE
		public int baseVertex
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<baseVertex>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<baseVertex>k__BackingField = value;
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06001F65 RID: 8037 RVA: 0x000332F7 File Offset: 0x000314F7
		// (set) Token: 0x06001F66 RID: 8038 RVA: 0x000332FF File Offset: 0x000314FF
		public int firstVertex
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<firstVertex>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<firstVertex>k__BackingField = value;
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06001F67 RID: 8039 RVA: 0x00033308 File Offset: 0x00031508
		// (set) Token: 0x06001F68 RID: 8040 RVA: 0x00033310 File Offset: 0x00031510
		public int vertexCount
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<vertexCount>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<vertexCount>k__BackingField = value;
			}
		}

		// Token: 0x06001F69 RID: 8041 RVA: 0x0003331C File Offset: 0x0003151C
		public override string ToString()
		{
			return string.Format("(topo={0} indices={1},{2} vertices={3},{4} basevtx={5} bounds={6})", new object[]
			{
				this.topology,
				this.indexStart,
				this.indexCount,
				this.firstVertex,
				this.vertexCount,
				this.baseVertex,
				this.bounds
			});
		}

		// Token: 0x04000B66 RID: 2918
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Bounds <bounds>k__BackingField;

		// Token: 0x04000B67 RID: 2919
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private MeshTopology <topology>k__BackingField;

		// Token: 0x04000B68 RID: 2920
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int <indexStart>k__BackingField;

		// Token: 0x04000B69 RID: 2921
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <indexCount>k__BackingField;

		// Token: 0x04000B6A RID: 2922
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int <baseVertex>k__BackingField;

		// Token: 0x04000B6B RID: 2923
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <firstVertex>k__BackingField;

		// Token: 0x04000B6C RID: 2924
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int <vertexCount>k__BackingField;
	}
}
