using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Collections.Generic
{
	// Token: 0x02000ACD RID: 2765
	[Serializable]
	internal sealed class LongEnumEqualityComparer<T> : EqualityComparer<T>, ISerializable where T : struct
	{
		// Token: 0x060062BA RID: 25274 RVA: 0x0014A4B4 File Offset: 0x001486B4
		public override bool Equals(T x, T y)
		{
			long num = JitHelpers.UnsafeEnumCastLong<T>(x);
			long num2 = JitHelpers.UnsafeEnumCastLong<T>(y);
			return num == num2;
		}

		// Token: 0x060062BB RID: 25275 RVA: 0x0014A4D4 File Offset: 0x001486D4
		public override int GetHashCode(T obj)
		{
			return JitHelpers.UnsafeEnumCastLong<T>(obj).GetHashCode();
		}

		// Token: 0x060062BC RID: 25276 RVA: 0x0014A4EF File Offset: 0x001486EF
		public override bool Equals(object obj)
		{
			return obj is LongEnumEqualityComparer<T>;
		}

		// Token: 0x060062BD RID: 25277 RVA: 0x00149C82 File Offset: 0x00147E82
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}

		// Token: 0x060062BE RID: 25278 RVA: 0x0014A0A9 File Offset: 0x001482A9
		public LongEnumEqualityComparer()
		{
		}

		// Token: 0x060062BF RID: 25279 RVA: 0x0014A0A9 File Offset: 0x001482A9
		public LongEnumEqualityComparer(SerializationInfo information, StreamingContext context)
		{
		}

		// Token: 0x060062C0 RID: 25280 RVA: 0x0014A4FA File Offset: 0x001486FA
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.SetType(typeof(ObjectEqualityComparer<T>));
		}
	}
}
