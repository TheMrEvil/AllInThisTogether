using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x02000295 RID: 661
	internal class ModelScope
	{
		// Token: 0x06001903 RID: 6403 RVA: 0x0008FFAD File Offset: 0x0008E1AD
		internal ModelScope(TypeScope typeScope)
		{
			this.typeScope = typeScope;
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001904 RID: 6404 RVA: 0x0008FFD2 File Offset: 0x0008E1D2
		internal TypeScope TypeScope
		{
			get
			{
				return this.typeScope;
			}
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x0008FFDA File Offset: 0x0008E1DA
		internal TypeModel GetTypeModel(Type type)
		{
			return this.GetTypeModel(type, true);
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x0008FFE4 File Offset: 0x0008E1E4
		internal TypeModel GetTypeModel(Type type, bool directReference)
		{
			TypeModel typeModel = (TypeModel)this.models[type];
			if (typeModel != null)
			{
				return typeModel;
			}
			TypeDesc typeDesc = this.typeScope.GetTypeDesc(type, null, directReference);
			switch (typeDesc.Kind)
			{
			case TypeKind.Root:
			case TypeKind.Struct:
			case TypeKind.Class:
				typeModel = new StructModel(type, typeDesc, this);
				break;
			case TypeKind.Primitive:
				typeModel = new PrimitiveModel(type, typeDesc, this);
				break;
			case TypeKind.Enum:
				typeModel = new EnumModel(type, typeDesc, this);
				break;
			case TypeKind.Array:
			case TypeKind.Collection:
			case TypeKind.Enumerable:
				typeModel = new ArrayModel(type, typeDesc, this);
				break;
			default:
				if (!typeDesc.IsSpecial)
				{
					throw new NotSupportedException(Res.GetString("The type {0} may not be serialized.", new object[]
					{
						type.FullName
					}));
				}
				typeModel = new SpecialModel(type, typeDesc, this);
				break;
			}
			this.models.Add(type, typeModel);
			return typeModel;
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x000900B0 File Offset: 0x0008E2B0
		internal ArrayModel GetArrayModel(Type type)
		{
			TypeModel typeModel = (TypeModel)this.arrayModels[type];
			if (typeModel == null)
			{
				typeModel = this.GetTypeModel(type);
				if (!(typeModel is ArrayModel))
				{
					TypeDesc arrayTypeDesc = this.typeScope.GetArrayTypeDesc(type);
					typeModel = new ArrayModel(type, arrayTypeDesc, this);
				}
				this.arrayModels.Add(type, typeModel);
			}
			return (ArrayModel)typeModel;
		}

		// Token: 0x04001903 RID: 6403
		private TypeScope typeScope;

		// Token: 0x04001904 RID: 6404
		private Hashtable models = new Hashtable();

		// Token: 0x04001905 RID: 6405
		private Hashtable arrayModels = new Hashtable();
	}
}
