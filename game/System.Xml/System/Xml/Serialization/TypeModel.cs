using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000296 RID: 662
	internal abstract class TypeModel
	{
		// Token: 0x06001908 RID: 6408 RVA: 0x0009010B File Offset: 0x0008E30B
		protected TypeModel(Type type, TypeDesc typeDesc, ModelScope scope)
		{
			this.scope = scope;
			this.type = type;
			this.typeDesc = typeDesc;
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001909 RID: 6409 RVA: 0x00090128 File Offset: 0x0008E328
		internal Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x0600190A RID: 6410 RVA: 0x00090130 File Offset: 0x0008E330
		internal ModelScope ModelScope
		{
			get
			{
				return this.scope;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x0600190B RID: 6411 RVA: 0x00090138 File Offset: 0x0008E338
		internal TypeDesc TypeDesc
		{
			get
			{
				return this.typeDesc;
			}
		}

		// Token: 0x04001906 RID: 6406
		private TypeDesc typeDesc;

		// Token: 0x04001907 RID: 6407
		private Type type;

		// Token: 0x04001908 RID: 6408
		private ModelScope scope;
	}
}
