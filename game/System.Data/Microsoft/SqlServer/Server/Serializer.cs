using System;
using System.IO;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000050 RID: 80
	internal abstract class Serializer
	{
		// Token: 0x0600043A RID: 1082
		public abstract object Deserialize(Stream s);

		// Token: 0x0600043B RID: 1083
		public abstract void Serialize(Stream s, object o);

		// Token: 0x0600043C RID: 1084 RVA: 0x0001071F File Offset: 0x0000E91F
		protected Serializer(Type t)
		{
			this._type = t;
		}

		// Token: 0x04000535 RID: 1333
		protected Type _type;
	}
}
