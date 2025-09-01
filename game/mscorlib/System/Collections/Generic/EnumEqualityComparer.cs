using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Collections.Generic
{
	// Token: 0x02000ACA RID: 2762
	[Serializable]
	internal class EnumEqualityComparer<T> : EqualityComparer<T>, ISerializable where T : struct
	{
		// Token: 0x060062AD RID: 25261 RVA: 0x0014A404 File Offset: 0x00148604
		public override bool Equals(T x, T y)
		{
			int num = JitHelpers.UnsafeEnumCast<T>(x);
			int num2 = JitHelpers.UnsafeEnumCast<T>(y);
			return num == num2;
		}

		// Token: 0x060062AE RID: 25262 RVA: 0x0014A424 File Offset: 0x00148624
		public override int GetHashCode(T obj)
		{
			return JitHelpers.UnsafeEnumCast<T>(obj).GetHashCode();
		}

		// Token: 0x060062AF RID: 25263 RVA: 0x0014A0A9 File Offset: 0x001482A9
		public EnumEqualityComparer()
		{
		}

		// Token: 0x060062B0 RID: 25264 RVA: 0x0014A0A9 File Offset: 0x001482A9
		protected EnumEqualityComparer(SerializationInfo information, StreamingContext context)
		{
		}

		// Token: 0x060062B1 RID: 25265 RVA: 0x0014A43F File Offset: 0x0014863F
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (Type.GetTypeCode(Enum.GetUnderlyingType(typeof(T))) != TypeCode.Int32)
			{
				info.SetType(typeof(ObjectEqualityComparer<T>));
			}
		}

		// Token: 0x060062B2 RID: 25266 RVA: 0x0014A469 File Offset: 0x00148669
		public override bool Equals(object obj)
		{
			return obj is EnumEqualityComparer<T>;
		}

		// Token: 0x060062B3 RID: 25267 RVA: 0x00149C82 File Offset: 0x00147E82
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}
	}
}
