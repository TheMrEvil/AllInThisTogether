using System;
using System.Collections;
using System.Reflection;

namespace System.Xml.Serialization
{
	// Token: 0x0200029E RID: 670
	internal class EnumModel : TypeModel
	{
		// Token: 0x06001928 RID: 6440 RVA: 0x00090140 File Offset: 0x0008E340
		internal EnumModel(Type type, TypeDesc typeDesc, ModelScope scope) : base(type, typeDesc, scope)
		{
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06001929 RID: 6441 RVA: 0x000906D4 File Offset: 0x0008E8D4
		internal ConstantModel[] Constants
		{
			get
			{
				if (this.constants == null)
				{
					ArrayList arrayList = new ArrayList();
					foreach (FieldInfo fieldInfo in base.Type.GetFields())
					{
						ConstantModel constantModel = this.GetConstantModel(fieldInfo);
						if (constantModel != null)
						{
							arrayList.Add(constantModel);
						}
					}
					this.constants = (ConstantModel[])arrayList.ToArray(typeof(ConstantModel));
				}
				return this.constants;
			}
		}

		// Token: 0x0600192A RID: 6442 RVA: 0x00090744 File Offset: 0x0008E944
		private ConstantModel GetConstantModel(FieldInfo fieldInfo)
		{
			if (fieldInfo.IsSpecialName)
			{
				return null;
			}
			return new ConstantModel(fieldInfo, ((IConvertible)fieldInfo.GetValue(null)).ToInt64(null));
		}

		// Token: 0x04001919 RID: 6425
		private ConstantModel[] constants;
	}
}
