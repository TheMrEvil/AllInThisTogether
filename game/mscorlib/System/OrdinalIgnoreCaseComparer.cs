using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200018B RID: 395
	[Serializable]
	internal sealed class OrdinalIgnoreCaseComparer : OrdinalComparer, ISerializable
	{
		// Token: 0x06000FC3 RID: 4035 RVA: 0x000419AE File Offset: 0x0003FBAE
		public OrdinalIgnoreCaseComparer() : base(true)
		{
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x000419B7 File Offset: 0x0003FBB7
		public override int Compare(string x, string y)
		{
			return string.Compare(x, y, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x000419C1 File Offset: 0x0003FBC1
		public override bool Equals(string x, string y)
		{
			return string.Equals(x, y, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x000419CB File Offset: 0x0003FBCB
		public override int GetHashCode(string obj)
		{
			if (obj == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.obj);
			}
			return CompareInfo.GetIgnoreCaseHash(obj);
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x000419DC File Offset: 0x0003FBDC
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.SetType(typeof(OrdinalComparer));
			info.AddValue("_ignoreCase", true);
		}
	}
}
