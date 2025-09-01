using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x0200027C RID: 636
	public class EventSpec : MemberSpec, IInterfaceMemberSpec
	{
		// Token: 0x06001F28 RID: 7976 RVA: 0x000999E6 File Offset: 0x00097BE6
		public EventSpec(TypeSpec declaringType, IMemberDefinition definition, TypeSpec eventType, Modifiers modifiers, MethodSpec add, MethodSpec remove) : base(MemberKind.Event, declaringType, definition, modifiers)
		{
			this.AccessorAdd = add;
			this.AccessorRemove = remove;
			this.MemberType = eventType;
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06001F29 RID: 7977 RVA: 0x00099A0A File Offset: 0x00097C0A
		// (set) Token: 0x06001F2A RID: 7978 RVA: 0x00099A12 File Offset: 0x00097C12
		public MethodSpec AccessorAdd
		{
			get
			{
				return this.add;
			}
			set
			{
				this.add = value;
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06001F2B RID: 7979 RVA: 0x00099A1B File Offset: 0x00097C1B
		// (set) Token: 0x06001F2C RID: 7980 RVA: 0x00099A23 File Offset: 0x00097C23
		public MethodSpec AccessorRemove
		{
			get
			{
				return this.remove;
			}
			set
			{
				this.remove = value;
			}
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06001F2D RID: 7981 RVA: 0x00099A2C File Offset: 0x00097C2C
		// (set) Token: 0x06001F2E RID: 7982 RVA: 0x00099A34 File Offset: 0x00097C34
		public FieldSpec BackingField
		{
			get
			{
				return this.backing_field;
			}
			set
			{
				this.backing_field = value;
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06001F2F RID: 7983 RVA: 0x00099A3D File Offset: 0x00097C3D
		// (set) Token: 0x06001F30 RID: 7984 RVA: 0x00099A45 File Offset: 0x00097C45
		public TypeSpec MemberType
		{
			[CompilerGenerated]
			get
			{
				return this.<MemberType>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<MemberType>k__BackingField = value;
			}
		}

		// Token: 0x06001F31 RID: 7985 RVA: 0x00099A50 File Offset: 0x00097C50
		public override MemberSpec InflateMember(TypeParameterInflator inflator)
		{
			EventSpec eventSpec = (EventSpec)base.InflateMember(inflator);
			eventSpec.MemberType = inflator.Inflate(this.MemberType);
			if (this.backing_field != null)
			{
				eventSpec.backing_field = (FieldSpec)this.backing_field.InflateMember(inflator);
			}
			return eventSpec;
		}

		// Token: 0x06001F32 RID: 7986 RVA: 0x00099A9D File Offset: 0x00097C9D
		public override List<MissingTypeSpecReference> ResolveMissingDependencies(MemberSpec caller)
		{
			return this.MemberType.ResolveMissingDependencies(this);
		}

		// Token: 0x04000B73 RID: 2931
		private MethodSpec add;

		// Token: 0x04000B74 RID: 2932
		private MethodSpec remove;

		// Token: 0x04000B75 RID: 2933
		private FieldSpec backing_field;

		// Token: 0x04000B76 RID: 2934
		[CompilerGenerated]
		private TypeSpec <MemberType>k__BackingField;
	}
}
