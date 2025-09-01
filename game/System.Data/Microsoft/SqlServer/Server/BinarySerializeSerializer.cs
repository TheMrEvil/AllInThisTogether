using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000052 RID: 82
	internal sealed class BinarySerializeSerializer : Serializer
	{
		// Token: 0x06000440 RID: 1088 RVA: 0x00010798 File Offset: 0x0000E998
		internal BinarySerializeSerializer(Type t) : base(t)
		{
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x000107A4 File Offset: 0x0000E9A4
		public override void Serialize(Stream s, object o)
		{
			BinaryWriter w = new BinaryWriter(s);
			((IBinarySerialize)o).Write(w);
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x000107C4 File Offset: 0x0000E9C4
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override object Deserialize(Stream s)
		{
			object obj = Activator.CreateInstance(this._type);
			BinaryReader r = new BinaryReader(s);
			((IBinarySerialize)obj).Read(r);
			return obj;
		}
	}
}
