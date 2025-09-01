using System;
using System.Runtime.CompilerServices;

namespace System.Security.Cryptography.Asn1
{
	// Token: 0x020000F3 RID: 243
	[AttributeUsage(AttributeTargets.Field)]
	internal sealed class DefaultValueAttribute : AsnEncodingRuleAttribute
	{
		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x00017359 File Offset: 0x00015559
		internal byte[] EncodedBytes
		{
			[CompilerGenerated]
			get
			{
				return this.<EncodedBytes>k__BackingField;
			}
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00017361 File Offset: 0x00015561
		public DefaultValueAttribute(params byte[] encodedValue)
		{
			this.EncodedBytes = encodedValue;
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x00017370 File Offset: 0x00015570
		public ReadOnlyMemory<byte> EncodedValue
		{
			get
			{
				return this.EncodedBytes;
			}
		}

		// Token: 0x040003BE RID: 958
		[CompilerGenerated]
		private readonly byte[] <EncodedBytes>k__BackingField;
	}
}
