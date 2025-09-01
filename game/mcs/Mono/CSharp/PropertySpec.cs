using System;
using System.Collections.Generic;
using System.Reflection;

namespace Mono.CSharp
{
	// Token: 0x02000276 RID: 630
	public class PropertySpec : MemberSpec, IInterfaceMemberSpec
	{
		// Token: 0x06001EDA RID: 7898 RVA: 0x00098409 File Offset: 0x00096609
		public PropertySpec(MemberKind kind, TypeSpec declaringType, IMemberDefinition definition, TypeSpec memberType, PropertyInfo info, Modifiers modifiers) : base(kind, declaringType, definition, modifiers)
		{
			this.info = info;
			this.memberType = memberType;
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06001EDB RID: 7899 RVA: 0x00098426 File Offset: 0x00096626
		// (set) Token: 0x06001EDC RID: 7900 RVA: 0x0009842E File Offset: 0x0009662E
		public MethodSpec Get
		{
			get
			{
				return this.get;
			}
			set
			{
				this.get = value;
				this.get.IsAccessor = true;
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06001EDD RID: 7901 RVA: 0x00098443 File Offset: 0x00096643
		// (set) Token: 0x06001EDE RID: 7902 RVA: 0x0009844B File Offset: 0x0009664B
		public MethodSpec Set
		{
			get
			{
				return this.set;
			}
			set
			{
				this.set = value;
				this.set.IsAccessor = true;
			}
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06001EDF RID: 7903 RVA: 0x00098460 File Offset: 0x00096660
		public bool HasDifferentAccessibility
		{
			get
			{
				return this.HasGet && this.HasSet && (this.Get.Modifiers & Modifiers.AccessibilityMask) != (this.Set.Modifiers & Modifiers.AccessibilityMask);
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06001EE0 RID: 7904 RVA: 0x00098495 File Offset: 0x00096695
		public bool HasGet
		{
			get
			{
				return this.Get != null;
			}
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06001EE1 RID: 7905 RVA: 0x000984A0 File Offset: 0x000966A0
		public bool HasSet
		{
			get
			{
				return this.Set != null;
			}
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06001EE2 RID: 7906 RVA: 0x000984AB File Offset: 0x000966AB
		public PropertyInfo MetaInfo
		{
			get
			{
				if ((this.state & MemberSpec.StateFlags.PendingMetaInflate) != (MemberSpec.StateFlags)0)
				{
					throw new NotSupportedException();
				}
				return this.info;
			}
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06001EE3 RID: 7907 RVA: 0x000984C7 File Offset: 0x000966C7
		public TypeSpec MemberType
		{
			get
			{
				return this.memberType;
			}
		}

		// Token: 0x06001EE4 RID: 7908 RVA: 0x000984CF File Offset: 0x000966CF
		public override MemberSpec InflateMember(TypeParameterInflator inflator)
		{
			PropertySpec propertySpec = (PropertySpec)base.InflateMember(inflator);
			propertySpec.memberType = inflator.Inflate(this.memberType);
			return propertySpec;
		}

		// Token: 0x06001EE5 RID: 7909 RVA: 0x000984F0 File Offset: 0x000966F0
		public override List<MissingTypeSpecReference> ResolveMissingDependencies(MemberSpec caller)
		{
			return this.memberType.ResolveMissingDependencies(this);
		}

		// Token: 0x04000B5D RID: 2909
		private PropertyInfo info;

		// Token: 0x04000B5E RID: 2910
		private TypeSpec memberType;

		// Token: 0x04000B5F RID: 2911
		private MethodSpec set;

		// Token: 0x04000B60 RID: 2912
		private MethodSpec get;
	}
}
