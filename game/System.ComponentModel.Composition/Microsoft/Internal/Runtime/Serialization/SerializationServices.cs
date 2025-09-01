using System;
using System.Runtime.Serialization;

namespace Microsoft.Internal.Runtime.Serialization
{
	// Token: 0x02000016 RID: 22
	internal static class SerializationServices
	{
		// Token: 0x060000D5 RID: 213 RVA: 0x00003899 File Offset: 0x00001A99
		public static T GetValue<T>(this SerializationInfo info, string name)
		{
			Assumes.NotNull<SerializationInfo, string>(info, name);
			return (T)((object)info.GetValue(name, typeof(T)));
		}
	}
}
