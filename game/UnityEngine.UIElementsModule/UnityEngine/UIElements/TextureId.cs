using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x02000266 RID: 614
	internal struct TextureId
	{
		// Token: 0x060012AD RID: 4781 RVA: 0x0004AD08 File Offset: 0x00048F08
		public TextureId(int index)
		{
			this.m_Index = index + 1;
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x060012AE RID: 4782 RVA: 0x0004AD14 File Offset: 0x00048F14
		public int index
		{
			get
			{
				return this.m_Index - 1;
			}
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x0004AD20 File Offset: 0x00048F20
		public float ConvertToGpu()
		{
			return (float)this.index;
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x0004AD3C File Offset: 0x00048F3C
		public override bool Equals(object obj)
		{
			bool flag = !(obj is TextureId);
			return !flag && (TextureId)obj == this;
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x0004AD74 File Offset: 0x00048F74
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(TextureId other)
		{
			return this.m_Index == other.m_Index;
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x0004AD94 File Offset: 0x00048F94
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return this.m_Index.GetHashCode();
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x0004ADB4 File Offset: 0x00048FB4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(TextureId left, TextureId right)
		{
			return left.m_Index == right.m_Index;
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x0004ADD4 File Offset: 0x00048FD4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(TextureId left, TextureId right)
		{
			return !(left == right);
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x0004ADF0 File Offset: 0x00048FF0
		// Note: this type is marked as 'beforefieldinit'.
		static TextureId()
		{
		}

		// Token: 0x040008AD RID: 2221
		private readonly int m_Index;

		// Token: 0x040008AE RID: 2222
		public static readonly TextureId invalid = new TextureId(-1);
	}
}
