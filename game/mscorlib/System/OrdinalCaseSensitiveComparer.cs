using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200018A RID: 394
	[Serializable]
	internal sealed class OrdinalCaseSensitiveComparer : OrdinalComparer, ISerializable
	{
		// Token: 0x06000FBE RID: 4030 RVA: 0x00041964 File Offset: 0x0003FB64
		public OrdinalCaseSensitiveComparer() : base(false)
		{
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x0004196D File Offset: 0x0003FB6D
		public override int Compare(string x, string y)
		{
			return string.CompareOrdinal(x, y);
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x00041976 File Offset: 0x0003FB76
		public override bool Equals(string x, string y)
		{
			return string.Equals(x, y);
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x0004197F File Offset: 0x0003FB7F
		public override int GetHashCode(string obj)
		{
			if (obj == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.obj);
			}
			return obj.GetHashCode();
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x00041990 File Offset: 0x0003FB90
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.SetType(typeof(OrdinalComparer));
			info.AddValue("_ignoreCase", false);
		}
	}
}
