using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Security.Cryptography.Asn1
{
	// Token: 0x02000100 RID: 256
	internal class SetOfValueComparer : IComparer<ReadOnlyMemory<byte>>
	{
		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x00017C3D File Offset: 0x00015E3D
		internal static SetOfValueComparer Instance
		{
			[CompilerGenerated]
			get
			{
				return SetOfValueComparer.<Instance>k__BackingField;
			}
		} = new SetOfValueComparer();

		// Token: 0x060005EE RID: 1518 RVA: 0x00017C44 File Offset: 0x00015E44
		public unsafe int Compare(ReadOnlyMemory<byte> x, ReadOnlyMemory<byte> y)
		{
			ReadOnlySpan<byte> span = x.Span;
			ReadOnlySpan<byte> span2 = y.Span;
			int num = Math.Min(x.Length, y.Length);
			int num3;
			for (int i = 0; i < num; i++)
			{
				int num2 = (int)(*span[i]);
				byte b = *span2[i];
				num3 = num2 - (int)b;
				if (num3 != 0)
				{
					return num3;
				}
			}
			num3 = x.Length - y.Length;
			if (num3 != 0)
			{
				return num3;
			}
			return 0;
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00002145 File Offset: 0x00000345
		public SetOfValueComparer()
		{
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00017CBA File Offset: 0x00015EBA
		// Note: this type is marked as 'beforefieldinit'.
		static SetOfValueComparer()
		{
		}

		// Token: 0x0400040D RID: 1037
		[CompilerGenerated]
		private static readonly SetOfValueComparer <Instance>k__BackingField;
	}
}
