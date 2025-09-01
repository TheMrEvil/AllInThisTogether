using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200060C RID: 1548
	internal class CADObjRef
	{
		// Token: 0x06003A92 RID: 14994 RVA: 0x000CD195 File Offset: 0x000CB395
		public CADObjRef(ObjRef o, int sourceDomain)
		{
			this.objref = o;
			this.TypeInfo = o.SerializeType();
			this.SourceDomain = sourceDomain;
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06003A93 RID: 14995 RVA: 0x000CD1B7 File Offset: 0x000CB3B7
		public string TypeName
		{
			get
			{
				return this.objref.TypeInfo.TypeName;
			}
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06003A94 RID: 14996 RVA: 0x000CD1C9 File Offset: 0x000CB3C9
		public string URI
		{
			get
			{
				return this.objref.URI;
			}
		}

		// Token: 0x04002670 RID: 9840
		internal ObjRef objref;

		// Token: 0x04002671 RID: 9841
		internal int SourceDomain;

		// Token: 0x04002672 RID: 9842
		internal byte[] TypeInfo;
	}
}
