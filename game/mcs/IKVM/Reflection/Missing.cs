using System;
using System.Runtime.Serialization;

namespace IKVM.Reflection
{
	// Token: 0x02000073 RID: 115
	[Serializable]
	public sealed class Missing : ISerializable
	{
		// Token: 0x06000680 RID: 1664 RVA: 0x00002CCC File Offset: 0x00000ECC
		private Missing()
		{
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x00013A27 File Offset: 0x00011C27
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.SetType(typeof(Missing.SingletonSerializationHelper));
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x00013A39 File Offset: 0x00011C39
		// Note: this type is marked as 'beforefieldinit'.
		static Missing()
		{
		}

		// Token: 0x04000275 RID: 629
		public static readonly Missing Value = new Missing();

		// Token: 0x02000338 RID: 824
		[Serializable]
		private sealed class SingletonSerializationHelper : IObjectReference
		{
			// Token: 0x060025D7 RID: 9687 RVA: 0x000B46E2 File Offset: 0x000B28E2
			public object GetRealObject(StreamingContext context)
			{
				return Missing.Value;
			}

			// Token: 0x060025D8 RID: 9688 RVA: 0x00002CCC File Offset: 0x00000ECC
			public SingletonSerializationHelper()
			{
			}
		}
	}
}
