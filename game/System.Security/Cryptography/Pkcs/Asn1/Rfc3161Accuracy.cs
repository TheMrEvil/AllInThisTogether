using System;
using System.Security.Cryptography.Asn1;

namespace System.Security.Cryptography.Pkcs.Asn1
{
	// Token: 0x020000B8 RID: 184
	internal struct Rfc3161Accuracy
	{
		// Token: 0x0600051B RID: 1307 RVA: 0x00014BD4 File Offset: 0x00012DD4
		internal Rfc3161Accuracy(long accuracyInMicroseconds)
		{
			if (accuracyInMicroseconds < 0L)
			{
				throw new ArgumentOutOfRangeException("accuracyInMicroseconds");
			}
			long num2;
			long num3;
			long num = Math.DivRem(Math.DivRem(accuracyInMicroseconds, 1000L, out num2), 1000L, out num3);
			if (num != 0L)
			{
				this.Seconds = new int?(checked((int)num));
			}
			else
			{
				this.Seconds = null;
			}
			if (num3 != 0L)
			{
				this.Millis = new int?((int)num3);
			}
			else
			{
				this.Millis = null;
			}
			if (num2 != 0L)
			{
				this.Micros = new int?((int)num2);
				return;
			}
			this.Micros = null;
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x00014C66 File Offset: 0x00012E66
		internal long TotalMicros
		{
			get
			{
				return 1000000L * (long)this.Seconds.GetValueOrDefault() + 1000L * (long)this.Millis.GetValueOrDefault() + (long)this.Micros.GetValueOrDefault();
			}
		}

		// Token: 0x04000329 RID: 809
		[OptionalValue]
		internal int? Seconds;

		// Token: 0x0400032A RID: 810
		[OptionalValue]
		[ExpectedTag(0)]
		internal int? Millis;

		// Token: 0x0400032B RID: 811
		[ExpectedTag(1)]
		[OptionalValue]
		internal int? Micros;
	}
}
