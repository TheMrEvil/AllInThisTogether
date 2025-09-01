using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
	// Token: 0x02000ACB RID: 2763
	[Serializable]
	internal sealed class SByteEnumEqualityComparer<T> : EnumEqualityComparer<T>, ISerializable where T : struct
	{
		// Token: 0x060062B4 RID: 25268 RVA: 0x0014A474 File Offset: 0x00148674
		public SByteEnumEqualityComparer()
		{
		}

		// Token: 0x060062B5 RID: 25269 RVA: 0x0014A474 File Offset: 0x00148674
		public SByteEnumEqualityComparer(SerializationInfo information, StreamingContext context)
		{
		}

		// Token: 0x060062B6 RID: 25270 RVA: 0x0014A47C File Offset: 0x0014867C
		public override int GetHashCode(T obj)
		{
			return ((sbyte)JitHelpers.UnsafeEnumCast<T>(obj)).GetHashCode();
		}
	}
}
