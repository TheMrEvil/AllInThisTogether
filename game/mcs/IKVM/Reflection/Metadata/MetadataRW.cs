using System;

namespace IKVM.Reflection.Metadata
{
	// Token: 0x020000A9 RID: 169
	internal abstract class MetadataRW
	{
		// Token: 0x060008C4 RID: 2244 RVA: 0x0001E30C File Offset: 0x0001C50C
		protected MetadataRW(Module module, bool bigStrings, bool bigGuids, bool bigBlobs)
		{
			this.bigStrings = bigStrings;
			this.bigGuids = bigGuids;
			this.bigBlobs = bigBlobs;
			this.bigField = module.Field.IsBig;
			this.bigMethodDef = module.MethodDef.IsBig;
			this.bigParam = module.Param.IsBig;
			this.bigTypeDef = module.TypeDef.IsBig;
			this.bigProperty = module.Property.IsBig;
			this.bigEvent = module.Event.IsBig;
			this.bigGenericParam = module.GenericParam.IsBig;
			this.bigModuleRef = module.ModuleRef.IsBig;
			this.bigResolutionScope = MetadataRW.IsBig(2, new Table[]
			{
				module.ModuleTable,
				module.ModuleRef,
				module.AssemblyRef,
				module.TypeRef
			});
			this.bigTypeDefOrRef = MetadataRW.IsBig(2, new Table[]
			{
				module.TypeDef,
				module.TypeRef,
				module.TypeSpec
			});
			this.bigMemberRefParent = MetadataRW.IsBig(3, new Table[]
			{
				module.TypeDef,
				module.TypeRef,
				module.ModuleRef,
				module.MethodDef,
				module.TypeSpec
			});
			this.bigMethodDefOrRef = MetadataRW.IsBig(1, new Table[]
			{
				module.MethodDef,
				module.MemberRef
			});
			this.bigHasCustomAttribute = MetadataRW.IsBig(5, new Table[]
			{
				module.MethodDef,
				module.Field,
				module.TypeRef,
				module.TypeDef,
				module.Param,
				module.InterfaceImpl,
				module.MemberRef,
				module.ModuleTable,
				module.Property,
				module.Event,
				module.StandAloneSig,
				module.ModuleRef,
				module.TypeSpec,
				module.AssemblyTable,
				module.AssemblyRef,
				module.File,
				module.ExportedType,
				module.ManifestResource
			});
			this.bigCustomAttributeType = MetadataRW.IsBig(3, new Table[]
			{
				module.MethodDef,
				module.MemberRef
			});
			this.bigHasConstant = MetadataRW.IsBig(2, new Table[]
			{
				module.Field,
				module.Param,
				module.Property
			});
			this.bigHasSemantics = MetadataRW.IsBig(1, new Table[]
			{
				module.Event,
				module.Property
			});
			this.bigHasFieldMarshal = MetadataRW.IsBig(1, new Table[]
			{
				module.Field,
				module.Param
			});
			this.bigHasDeclSecurity = MetadataRW.IsBig(2, new Table[]
			{
				module.TypeDef,
				module.MethodDef,
				module.AssemblyTable
			});
			this.bigTypeOrMethodDef = MetadataRW.IsBig(1, new Table[]
			{
				module.TypeDef,
				module.MethodDef
			});
			this.bigMemberForwarded = MetadataRW.IsBig(1, new Table[]
			{
				module.Field,
				module.MethodDef
			});
			this.bigImplementation = MetadataRW.IsBig(2, new Table[]
			{
				module.File,
				module.AssemblyRef,
				module.ExportedType
			});
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0001E67C File Offset: 0x0001C87C
		private static bool IsBig(int bitsUsed, params Table[] tables)
		{
			int num = 1 << 16 - bitsUsed;
			for (int i = 0; i < tables.Length; i++)
			{
				if (tables[i].RowCount >= num)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040003B0 RID: 944
		internal readonly bool bigStrings;

		// Token: 0x040003B1 RID: 945
		internal readonly bool bigGuids;

		// Token: 0x040003B2 RID: 946
		internal readonly bool bigBlobs;

		// Token: 0x040003B3 RID: 947
		internal readonly bool bigResolutionScope;

		// Token: 0x040003B4 RID: 948
		internal readonly bool bigTypeDefOrRef;

		// Token: 0x040003B5 RID: 949
		internal readonly bool bigMemberRefParent;

		// Token: 0x040003B6 RID: 950
		internal readonly bool bigHasCustomAttribute;

		// Token: 0x040003B7 RID: 951
		internal readonly bool bigCustomAttributeType;

		// Token: 0x040003B8 RID: 952
		internal readonly bool bigMethodDefOrRef;

		// Token: 0x040003B9 RID: 953
		internal readonly bool bigHasConstant;

		// Token: 0x040003BA RID: 954
		internal readonly bool bigHasSemantics;

		// Token: 0x040003BB RID: 955
		internal readonly bool bigHasFieldMarshal;

		// Token: 0x040003BC RID: 956
		internal readonly bool bigHasDeclSecurity;

		// Token: 0x040003BD RID: 957
		internal readonly bool bigTypeOrMethodDef;

		// Token: 0x040003BE RID: 958
		internal readonly bool bigMemberForwarded;

		// Token: 0x040003BF RID: 959
		internal readonly bool bigImplementation;

		// Token: 0x040003C0 RID: 960
		internal readonly bool bigField;

		// Token: 0x040003C1 RID: 961
		internal readonly bool bigMethodDef;

		// Token: 0x040003C2 RID: 962
		internal readonly bool bigParam;

		// Token: 0x040003C3 RID: 963
		internal readonly bool bigTypeDef;

		// Token: 0x040003C4 RID: 964
		internal readonly bool bigProperty;

		// Token: 0x040003C5 RID: 965
		internal readonly bool bigEvent;

		// Token: 0x040003C6 RID: 966
		internal readonly bool bigGenericParam;

		// Token: 0x040003C7 RID: 967
		internal readonly bool bigModuleRef;
	}
}
