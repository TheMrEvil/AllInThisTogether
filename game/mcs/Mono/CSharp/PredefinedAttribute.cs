using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x02000125 RID: 293
	public class PredefinedAttribute : PredefinedType
	{
		// Token: 0x06000E72 RID: 3698 RVA: 0x000372C1 File Offset: 0x000354C1
		public PredefinedAttribute(ModuleContainer module, string ns, string name) : base(module, MemberKind.Class, ns, name)
		{
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000E73 RID: 3699 RVA: 0x000372D1 File Offset: 0x000354D1
		public MethodSpec Constructor
		{
			get
			{
				return this.ctor;
			}
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x000372D9 File Offset: 0x000354D9
		public static bool operator ==(TypeSpec type, PredefinedAttribute pa)
		{
			return type == pa.type && pa.type != null;
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x000372EF File Offset: 0x000354EF
		public static bool operator !=(TypeSpec type, PredefinedAttribute pa)
		{
			return type != pa.type;
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x000372FD File Offset: 0x000354FD
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x0000225C File Offset: 0x0000045C
		public override bool Equals(object obj)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x00037305 File Offset: 0x00035505
		public void EmitAttribute(ConstructorBuilder builder)
		{
			if (this.ResolveBuilder())
			{
				builder.SetCustomAttribute(this.GetCtorMetaInfo(), AttributeEncoder.Empty);
			}
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x00037320 File Offset: 0x00035520
		public void EmitAttribute(MethodBuilder builder)
		{
			if (this.ResolveBuilder())
			{
				builder.SetCustomAttribute(this.GetCtorMetaInfo(), AttributeEncoder.Empty);
			}
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x0003733B File Offset: 0x0003553B
		public void EmitAttribute(PropertyBuilder builder)
		{
			if (this.ResolveBuilder())
			{
				builder.SetCustomAttribute(this.GetCtorMetaInfo(), AttributeEncoder.Empty);
			}
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x00037356 File Offset: 0x00035556
		public void EmitAttribute(FieldBuilder builder)
		{
			if (this.ResolveBuilder())
			{
				builder.SetCustomAttribute(this.GetCtorMetaInfo(), AttributeEncoder.Empty);
			}
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x00037371 File Offset: 0x00035571
		public void EmitAttribute(TypeBuilder builder)
		{
			if (this.ResolveBuilder())
			{
				builder.SetCustomAttribute(this.GetCtorMetaInfo(), AttributeEncoder.Empty);
			}
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x0003738C File Offset: 0x0003558C
		public void EmitAttribute(AssemblyBuilder builder)
		{
			if (this.ResolveBuilder())
			{
				builder.SetCustomAttribute(this.GetCtorMetaInfo(), AttributeEncoder.Empty);
			}
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x000373A7 File Offset: 0x000355A7
		public void EmitAttribute(ModuleBuilder builder)
		{
			if (this.ResolveBuilder())
			{
				builder.SetCustomAttribute(this.GetCtorMetaInfo(), AttributeEncoder.Empty);
			}
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x000373C2 File Offset: 0x000355C2
		public void EmitAttribute(ParameterBuilder builder)
		{
			if (this.ResolveBuilder())
			{
				builder.SetCustomAttribute(this.GetCtorMetaInfo(), AttributeEncoder.Empty);
			}
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x000373DD File Offset: 0x000355DD
		private ConstructorInfo GetCtorMetaInfo()
		{
			return (ConstructorInfo)this.ctor.GetMetaInfo();
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x000373EF File Offset: 0x000355EF
		public bool ResolveBuilder()
		{
			if (this.ctor != null)
			{
				return true;
			}
			if (!base.Define())
			{
				return false;
			}
			this.ctor = (MethodSpec)MemberCache.FindMember(this.type, MemberFilter.Constructor(ParametersCompiled.EmptyReadOnlyParameters), BindingRestriction.DeclaredOnly);
			return this.ctor != null;
		}

		// Token: 0x040006C9 RID: 1737
		protected MethodSpec ctor;
	}
}
