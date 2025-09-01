using System;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x020002D4 RID: 724
	public class PredefinedMember<T> where T : MemberSpec
	{
		// Token: 0x06002278 RID: 8824 RVA: 0x000AA1EB File Offset: 0x000A83EB
		public PredefinedMember(ModuleContainer module, PredefinedType type, MemberFilter filter)
		{
			this.module = module;
			this.declaring_type_predefined = type;
			this.filter = filter;
		}

		// Token: 0x06002279 RID: 8825 RVA: 0x000AA208 File Offset: 0x000A8408
		public PredefinedMember(ModuleContainer module, TypeSpec type, MemberFilter filter)
		{
			this.module = module;
			this.declaring_type = type;
			this.filter = filter;
		}

		// Token: 0x0600227A RID: 8826 RVA: 0x000AA225 File Offset: 0x000A8425
		public PredefinedMember(ModuleContainer module, PredefinedType type, string name, params TypeSpec[] types) : this(module, type, MemberFilter.Method(name, 0, ParametersCompiled.CreateFullyResolved(types), null))
		{
		}

		// Token: 0x0600227B RID: 8827 RVA: 0x000AA240 File Offset: 0x000A8440
		public PredefinedMember(ModuleContainer module, PredefinedType type, string name, MemberKind kind, params PredefinedType[] types) : this(module, type, new MemberFilter(name, 0, kind, null, null))
		{
			this.filter_builder = delegate()
			{
				TypeSpec[] array = new TypeSpec[types.Length];
				for (int i = 0; i < array.Length; i++)
				{
					PredefinedType predefinedType = types[i];
					if (!predefinedType.Define())
					{
						return null;
					}
					array[i] = predefinedType.TypeSpec;
				}
				return array;
			};
		}

		// Token: 0x0600227C RID: 8828 RVA: 0x000AA280 File Offset: 0x000A8480
		public PredefinedMember(ModuleContainer module, PredefinedType type, string name, MemberKind kind, Func<TypeSpec[]> typesBuilder, TypeSpec returnType) : this(module, type, new MemberFilter(name, 0, kind, null, returnType))
		{
			this.filter_builder = typesBuilder;
		}

		// Token: 0x0600227D RID: 8829 RVA: 0x000AA29E File Offset: 0x000A849E
		public PredefinedMember(ModuleContainer module, BuiltinTypeSpec type, string name, params TypeSpec[] types) : this(module, type, MemberFilter.Method(name, 0, ParametersCompiled.CreateFullyResolved(types), null))
		{
		}

		// Token: 0x0600227E RID: 8830 RVA: 0x000AA2B8 File Offset: 0x000A84B8
		public T Get()
		{
			if (this.member != null)
			{
				return this.member;
			}
			if (this.declaring_type == null)
			{
				if (!this.declaring_type_predefined.Define())
				{
					return default(T);
				}
				this.declaring_type = this.declaring_type_predefined.TypeSpec;
			}
			if (this.filter_builder != null)
			{
				TypeSpec[] array = this.filter_builder();
				if (this.filter.Kind == MemberKind.Field)
				{
					this.filter = new MemberFilter(this.filter.Name, this.filter.Arity, this.filter.Kind, null, array[0]);
				}
				else
				{
					this.filter = new MemberFilter(this.filter.Name, this.filter.Arity, this.filter.Kind, ParametersCompiled.CreateFullyResolved(array), this.filter.MemberType);
				}
			}
			this.member = (MemberCache.FindMember(this.declaring_type, this.filter, BindingRestriction.DeclaredOnly) as T);
			if (this.member == null)
			{
				return default(T);
			}
			if (!this.member.IsAccessible(this.module))
			{
				return default(T);
			}
			return this.member;
		}

		// Token: 0x0600227F RID: 8831 RVA: 0x000AA400 File Offset: 0x000A8600
		public T Resolve(Location loc)
		{
			if (this.member != null)
			{
				return this.member;
			}
			if (this.Get() != null)
			{
				return this.member;
			}
			if (this.declaring_type == null && this.declaring_type_predefined.Resolve() == null)
			{
				return default(T);
			}
			if (this.filter_builder != null)
			{
				this.filter = new MemberFilter(this.filter.Name, this.filter.Arity, this.filter.Kind, ParametersCompiled.CreateFullyResolved(this.filter_builder()), this.filter.MemberType);
			}
			string text = null;
			if (this.filter.Parameters != null)
			{
				text = this.filter.Parameters.GetSignatureForError();
			}
			this.module.Compiler.Report.Error(656, loc, "The compiler required member `{0}.{1}{2}' could not be found or is inaccessible", new string[]
			{
				this.declaring_type.GetSignatureForError(),
				this.filter.Name,
				text
			});
			return default(T);
		}

		// Token: 0x04000D4B RID: 3403
		private readonly ModuleContainer module;

		// Token: 0x04000D4C RID: 3404
		private T member;

		// Token: 0x04000D4D RID: 3405
		private TypeSpec declaring_type;

		// Token: 0x04000D4E RID: 3406
		private readonly PredefinedType declaring_type_predefined;

		// Token: 0x04000D4F RID: 3407
		private MemberFilter filter;

		// Token: 0x04000D50 RID: 3408
		private readonly Func<TypeSpec[]> filter_builder;

		// Token: 0x02000402 RID: 1026
		[CompilerGenerated]
		private sealed class <>c__DisplayClass9_0
		{
			// Token: 0x06002837 RID: 10295 RVA: 0x00002CCC File Offset: 0x00000ECC
			public <>c__DisplayClass9_0()
			{
			}

			// Token: 0x06002838 RID: 10296 RVA: 0x000BEC74 File Offset: 0x000BCE74
			internal TypeSpec[] <.ctor>b__0()
			{
				TypeSpec[] array = new TypeSpec[this.types.Length];
				for (int i = 0; i < array.Length; i++)
				{
					PredefinedType predefinedType = this.types[i];
					if (!predefinedType.Define())
					{
						return null;
					}
					array[i] = predefinedType.TypeSpec;
				}
				return array;
			}

			// Token: 0x04001167 RID: 4455
			public PredefinedType[] types;
		}
	}
}
