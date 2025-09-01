using System;
using System.IO;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000051 RID: 81
	internal sealed class NormalizedSerializer : Serializer
	{
		// Token: 0x0600043D RID: 1085 RVA: 0x00010730 File Offset: 0x0000E930
		internal NormalizedSerializer(Type t) : base(t)
		{
			SqlUserDefinedTypeAttribute udtAttribute = SerializationHelperSql9.GetUdtAttribute(t);
			this._normalizer = new BinaryOrderedUdtNormalizer(t, true);
			this._isFixedSize = udtAttribute.IsFixedLength;
			this._maxSize = this._normalizer.Size;
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00010775 File Offset: 0x0000E975
		public override void Serialize(Stream s, object o)
		{
			this._normalizer.NormalizeTopObject(o, s);
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00010784 File Offset: 0x0000E984
		public override object Deserialize(Stream s)
		{
			return this._normalizer.DeNormalizeTopObject(this._type, s);
		}

		// Token: 0x04000536 RID: 1334
		private BinaryOrderedUdtNormalizer _normalizer;

		// Token: 0x04000537 RID: 1335
		private bool _isFixedSize;

		// Token: 0x04000538 RID: 1336
		private int _maxSize;
	}
}
