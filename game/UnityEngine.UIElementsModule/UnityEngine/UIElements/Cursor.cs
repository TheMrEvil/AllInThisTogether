using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x02000016 RID: 22
	public struct Cursor : IEquatable<Cursor>
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000092 RID: 146 RVA: 0x000040A7 File Offset: 0x000022A7
		// (set) Token: 0x06000093 RID: 147 RVA: 0x000040AF File Offset: 0x000022AF
		public Texture2D texture
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<texture>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<texture>k__BackingField = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000094 RID: 148 RVA: 0x000040B8 File Offset: 0x000022B8
		// (set) Token: 0x06000095 RID: 149 RVA: 0x000040C0 File Offset: 0x000022C0
		public Vector2 hotspot
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<hotspot>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<hotspot>k__BackingField = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000096 RID: 150 RVA: 0x000040C9 File Offset: 0x000022C9
		// (set) Token: 0x06000097 RID: 151 RVA: 0x000040D1 File Offset: 0x000022D1
		internal int defaultCursorId
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<defaultCursorId>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<defaultCursorId>k__BackingField = value;
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000040DC File Offset: 0x000022DC
		public override bool Equals(object obj)
		{
			return obj is Cursor && this.Equals((Cursor)obj);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004108 File Offset: 0x00002308
		public bool Equals(Cursor other)
		{
			return EqualityComparer<Texture2D>.Default.Equals(this.texture, other.texture) && this.hotspot.Equals(other.hotspot) && this.defaultCursorId == other.defaultCursorId;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000415C File Offset: 0x0000235C
		public override int GetHashCode()
		{
			int num = 1500536833;
			num = num * -1521134295 + EqualityComparer<Texture2D>.Default.GetHashCode(this.texture);
			num = num * -1521134295 + EqualityComparer<Vector2>.Default.GetHashCode(this.hotspot);
			return num * -1521134295 + this.defaultCursorId.GetHashCode();
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000041C0 File Offset: 0x000023C0
		public static bool operator ==(Cursor style1, Cursor style2)
		{
			return style1.Equals(style2);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000041DC File Offset: 0x000023DC
		public static bool operator !=(Cursor style1, Cursor style2)
		{
			return !(style1 == style2);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000041F8 File Offset: 0x000023F8
		public override string ToString()
		{
			return string.Format("texture={0}, hotspot={1}", this.texture, this.hotspot);
		}

		// Token: 0x04000038 RID: 56
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Texture2D <texture>k__BackingField;

		// Token: 0x04000039 RID: 57
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Vector2 <hotspot>k__BackingField;

		// Token: 0x0400003A RID: 58
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int <defaultCursorId>k__BackingField;
	}
}
