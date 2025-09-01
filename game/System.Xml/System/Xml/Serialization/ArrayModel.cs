using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000297 RID: 663
	internal class ArrayModel : TypeModel
	{
		// Token: 0x0600190C RID: 6412 RVA: 0x00090140 File Offset: 0x0008E340
		internal ArrayModel(Type type, TypeDesc typeDesc, ModelScope scope) : base(type, typeDesc, scope)
		{
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x0600190D RID: 6413 RVA: 0x0009014B File Offset: 0x0008E34B
		internal TypeModel Element
		{
			get
			{
				return base.ModelScope.GetTypeModel(TypeScope.GetArrayElementType(base.Type, null));
			}
		}
	}
}
