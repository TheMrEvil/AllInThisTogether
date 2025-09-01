using System;
using System.Security.Cryptography.Asn1;

namespace System.Security.Cryptography.Pkcs.Asn1
{
	// Token: 0x0200009E RID: 158
	internal struct AlgorithmIdentifierAsn
	{
		// Token: 0x06000510 RID: 1296 RVA: 0x00014A88 File Offset: 0x00012C88
		internal bool Equals(ref AlgorithmIdentifierAsn other)
		{
			if (this.Algorithm.Value != other.Algorithm.Value)
			{
				return false;
			}
			bool flag = AlgorithmIdentifierAsn.RepresentsNull(this.Parameters);
			bool flag2 = AlgorithmIdentifierAsn.RepresentsNull(other.Parameters);
			return flag == flag2 && (flag || this.Parameters.Value.Span.SequenceEqual(other.Parameters.Value.Span));
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00014B04 File Offset: 0x00012D04
		private unsafe static bool RepresentsNull(ReadOnlyMemory<byte>? parameters)
		{
			if (parameters == null)
			{
				return true;
			}
			ReadOnlySpan<byte> span = parameters.Value.Span;
			return span.Length == 2 && *span[0] == 5 && *span[1] == 0;
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00014B52 File Offset: 0x00012D52
		// Note: this type is marked as 'beforefieldinit'.
		static AlgorithmIdentifierAsn()
		{
			byte[] array = new byte[2];
			array[0] = 5;
			AlgorithmIdentifierAsn.ExplicitDerNull = array;
		}

		// Token: 0x040002DD RID: 733
		internal static readonly ReadOnlyMemory<byte> ExplicitDerNull;

		// Token: 0x040002DE RID: 734
		[ObjectIdentifier(PopulateFriendlyName = true)]
		public Oid Algorithm;

		// Token: 0x040002DF RID: 735
		[OptionalValue]
		[AnyValue]
		public ReadOnlyMemory<byte>? Parameters;
	}
}
