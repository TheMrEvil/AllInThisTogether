using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
	// Token: 0x02000ACC RID: 2764
	[Serializable]
	internal sealed class ShortEnumEqualityComparer<T> : EnumEqualityComparer<T>, ISerializable where T : struct
	{
		// Token: 0x060062B7 RID: 25271 RVA: 0x0014A474 File Offset: 0x00148674
		public ShortEnumEqualityComparer()
		{
		}

		// Token: 0x060062B8 RID: 25272 RVA: 0x0014A474 File Offset: 0x00148674
		public ShortEnumEqualityComparer(SerializationInfo information, StreamingContext context)
		{
		}

		// Token: 0x060062B9 RID: 25273 RVA: 0x0014A498 File Offset: 0x00148698
		public override int GetHashCode(T obj)
		{
			return ((short)JitHelpers.UnsafeEnumCast<T>(obj)).GetHashCode();
		}
	}
}
