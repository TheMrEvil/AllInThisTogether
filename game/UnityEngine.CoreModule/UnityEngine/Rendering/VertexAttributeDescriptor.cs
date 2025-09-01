using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x020003C1 RID: 961
	[UsedByNativeCode]
	public struct VertexAttributeDescriptor : IEquatable<VertexAttributeDescriptor>
	{
		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06001F6A RID: 8042 RVA: 0x000333A0 File Offset: 0x000315A0
		// (set) Token: 0x06001F6B RID: 8043 RVA: 0x000333A8 File Offset: 0x000315A8
		public VertexAttribute attribute
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<attribute>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<attribute>k__BackingField = value;
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06001F6C RID: 8044 RVA: 0x000333B1 File Offset: 0x000315B1
		// (set) Token: 0x06001F6D RID: 8045 RVA: 0x000333B9 File Offset: 0x000315B9
		public VertexAttributeFormat format
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<format>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<format>k__BackingField = value;
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06001F6E RID: 8046 RVA: 0x000333C2 File Offset: 0x000315C2
		// (set) Token: 0x06001F6F RID: 8047 RVA: 0x000333CA File Offset: 0x000315CA
		public int dimension
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<dimension>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<dimension>k__BackingField = value;
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001F70 RID: 8048 RVA: 0x000333D3 File Offset: 0x000315D3
		// (set) Token: 0x06001F71 RID: 8049 RVA: 0x000333DB File Offset: 0x000315DB
		public int stream
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<stream>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<stream>k__BackingField = value;
			}
		}

		// Token: 0x06001F72 RID: 8050 RVA: 0x000333E4 File Offset: 0x000315E4
		public VertexAttributeDescriptor(VertexAttribute attribute = VertexAttribute.Position, VertexAttributeFormat format = VertexAttributeFormat.Float32, int dimension = 3, int stream = 0)
		{
			this.attribute = attribute;
			this.format = format;
			this.dimension = dimension;
			this.stream = stream;
		}

		// Token: 0x06001F73 RID: 8051 RVA: 0x00033408 File Offset: 0x00031608
		public override string ToString()
		{
			return string.Format("(attr={0} fmt={1} dim={2} stream={3})", new object[]
			{
				this.attribute,
				this.format,
				this.dimension,
				this.stream
			});
		}

		// Token: 0x06001F74 RID: 8052 RVA: 0x00033464 File Offset: 0x00031664
		public override int GetHashCode()
		{
			int num = 17;
			num = (int)(num * 23 + this.attribute);
			num = (int)(num * 23 + this.format);
			num = num * 23 + this.dimension;
			return num * 23 + this.stream;
		}

		// Token: 0x06001F75 RID: 8053 RVA: 0x000334AC File Offset: 0x000316AC
		public override bool Equals(object other)
		{
			bool flag = !(other is VertexAttributeDescriptor);
			return !flag && this.Equals((VertexAttributeDescriptor)other);
		}

		// Token: 0x06001F76 RID: 8054 RVA: 0x000334E0 File Offset: 0x000316E0
		public bool Equals(VertexAttributeDescriptor other)
		{
			return this.attribute == other.attribute && this.format == other.format && this.dimension == other.dimension && this.stream == other.stream;
		}

		// Token: 0x06001F77 RID: 8055 RVA: 0x00033534 File Offset: 0x00031734
		public static bool operator ==(VertexAttributeDescriptor lhs, VertexAttributeDescriptor rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x06001F78 RID: 8056 RVA: 0x00033550 File Offset: 0x00031750
		public static bool operator !=(VertexAttributeDescriptor lhs, VertexAttributeDescriptor rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x04000B6D RID: 2925
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private VertexAttribute <attribute>k__BackingField;

		// Token: 0x04000B6E RID: 2926
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private VertexAttributeFormat <format>k__BackingField;

		// Token: 0x04000B6F RID: 2927
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int <dimension>k__BackingField;

		// Token: 0x04000B70 RID: 2928
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <stream>k__BackingField;
	}
}
