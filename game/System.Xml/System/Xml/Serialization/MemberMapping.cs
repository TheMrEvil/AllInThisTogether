using System;
using System.CodeDom.Compiler;
using System.Reflection;

namespace System.Xml.Serialization
{
	// Token: 0x02000291 RID: 657
	internal class MemberMapping : AccessorMapping
	{
		// Token: 0x060018C9 RID: 6345 RVA: 0x0008F4CC File Offset: 0x0008D6CC
		internal MemberMapping()
		{
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x0008F4DC File Offset: 0x0008D6DC
		private MemberMapping(MemberMapping mapping) : base(mapping)
		{
			this.name = mapping.name;
			this.checkShouldPersist = mapping.checkShouldPersist;
			this.checkSpecified = mapping.checkSpecified;
			this.isReturnValue = mapping.isReturnValue;
			this.readOnly = mapping.readOnly;
			this.sequenceId = mapping.sequenceId;
			this.memberInfo = mapping.memberInfo;
			this.checkSpecifiedMemberInfo = mapping.checkSpecifiedMemberInfo;
			this.checkShouldPersistMethodInfo = mapping.checkShouldPersistMethodInfo;
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x060018CB RID: 6347 RVA: 0x0008F563 File Offset: 0x0008D763
		// (set) Token: 0x060018CC RID: 6348 RVA: 0x0008F56B File Offset: 0x0008D76B
		internal bool CheckShouldPersist
		{
			get
			{
				return this.checkShouldPersist;
			}
			set
			{
				this.checkShouldPersist = value;
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x060018CD RID: 6349 RVA: 0x0008F574 File Offset: 0x0008D774
		// (set) Token: 0x060018CE RID: 6350 RVA: 0x0008F57C File Offset: 0x0008D77C
		internal SpecifiedAccessor CheckSpecified
		{
			get
			{
				return this.checkSpecified;
			}
			set
			{
				this.checkSpecified = value;
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x060018CF RID: 6351 RVA: 0x0008F585 File Offset: 0x0008D785
		// (set) Token: 0x060018D0 RID: 6352 RVA: 0x0008F59B File Offset: 0x0008D79B
		internal string Name
		{
			get
			{
				if (this.name != null)
				{
					return this.name;
				}
				return string.Empty;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x060018D1 RID: 6353 RVA: 0x0008F5A4 File Offset: 0x0008D7A4
		// (set) Token: 0x060018D2 RID: 6354 RVA: 0x0008F5AC File Offset: 0x0008D7AC
		internal MemberInfo MemberInfo
		{
			get
			{
				return this.memberInfo;
			}
			set
			{
				this.memberInfo = value;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x060018D3 RID: 6355 RVA: 0x0008F5B5 File Offset: 0x0008D7B5
		// (set) Token: 0x060018D4 RID: 6356 RVA: 0x0008F5BD File Offset: 0x0008D7BD
		internal MemberInfo CheckSpecifiedMemberInfo
		{
			get
			{
				return this.checkSpecifiedMemberInfo;
			}
			set
			{
				this.checkSpecifiedMemberInfo = value;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x060018D5 RID: 6357 RVA: 0x0008F5C6 File Offset: 0x0008D7C6
		// (set) Token: 0x060018D6 RID: 6358 RVA: 0x0008F5CE File Offset: 0x0008D7CE
		internal MethodInfo CheckShouldPersistMethodInfo
		{
			get
			{
				return this.checkShouldPersistMethodInfo;
			}
			set
			{
				this.checkShouldPersistMethodInfo = value;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x060018D7 RID: 6359 RVA: 0x0008F5D7 File Offset: 0x0008D7D7
		// (set) Token: 0x060018D8 RID: 6360 RVA: 0x0008F5DF File Offset: 0x0008D7DF
		internal bool IsReturnValue
		{
			get
			{
				return this.isReturnValue;
			}
			set
			{
				this.isReturnValue = value;
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x060018D9 RID: 6361 RVA: 0x0008F5E8 File Offset: 0x0008D7E8
		// (set) Token: 0x060018DA RID: 6362 RVA: 0x0008F5F0 File Offset: 0x0008D7F0
		internal bool ReadOnly
		{
			get
			{
				return this.readOnly;
			}
			set
			{
				this.readOnly = value;
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x060018DB RID: 6363 RVA: 0x0008F5F9 File Offset: 0x0008D7F9
		internal bool IsSequence
		{
			get
			{
				return this.sequenceId >= 0;
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x060018DC RID: 6364 RVA: 0x0008F607 File Offset: 0x0008D807
		// (set) Token: 0x060018DD RID: 6365 RVA: 0x0008F60F File Offset: 0x0008D80F
		internal int SequenceId
		{
			get
			{
				return this.sequenceId;
			}
			set
			{
				this.sequenceId = value;
			}
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x0008F618 File Offset: 0x0008D818
		private string GetNullableType(TypeDesc td)
		{
			if (td.IsMappedType || (!td.IsValueType && (base.Elements[0].IsSoap || td.ArrayElementTypeDesc == null)))
			{
				return td.FullName;
			}
			if (td.ArrayElementTypeDesc != null)
			{
				return this.GetNullableType(td.ArrayElementTypeDesc) + "[]";
			}
			return "System.Nullable`1[" + td.FullName + "]";
		}

		// Token: 0x060018DF RID: 6367 RVA: 0x0008F687 File Offset: 0x0008D887
		internal MemberMapping Clone()
		{
			return new MemberMapping(this);
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x0008F68F File Offset: 0x0008D88F
		internal string GetTypeName(CodeDomProvider codeProvider)
		{
			if (base.IsNeedNullable && codeProvider.Supports(GeneratorSupport.GenericTypeReference))
			{
				return this.GetNullableType(base.TypeDesc);
			}
			return base.TypeDesc.FullName;
		}

		// Token: 0x040018E7 RID: 6375
		private string name;

		// Token: 0x040018E8 RID: 6376
		private bool checkShouldPersist;

		// Token: 0x040018E9 RID: 6377
		private SpecifiedAccessor checkSpecified;

		// Token: 0x040018EA RID: 6378
		private bool isReturnValue;

		// Token: 0x040018EB RID: 6379
		private bool readOnly;

		// Token: 0x040018EC RID: 6380
		private int sequenceId = -1;

		// Token: 0x040018ED RID: 6381
		private MemberInfo memberInfo;

		// Token: 0x040018EE RID: 6382
		private MemberInfo checkSpecifiedMemberInfo;

		// Token: 0x040018EF RID: 6383
		private MethodInfo checkShouldPersistMethodInfo;
	}
}
