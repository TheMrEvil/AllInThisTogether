using System;

namespace Mono.CSharp
{
	// Token: 0x02000105 RID: 261
	public abstract class CompilerGeneratedContainer : ClassOrStruct
	{
		// Token: 0x06000D19 RID: 3353 RVA: 0x0002F69C File Offset: 0x0002D89C
		protected CompilerGeneratedContainer(TypeContainer parent, MemberName name, Modifiers mod) : this(parent, name, mod, MemberKind.Class)
		{
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x0002F6AC File Offset: 0x0002D8AC
		protected CompilerGeneratedContainer(TypeContainer parent, MemberName name, Modifiers mod, MemberKind kind) : base(parent, name, null, kind)
		{
			base.ModFlags = (mod | Modifiers.COMPILER_GENERATED | Modifiers.SEALED);
			this.spec = new TypeSpec(this.Kind, null, this, null, base.ModFlags);
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x0002F6E3 File Offset: 0x0002D8E3
		protected void CheckMembersDefined()
		{
			if (base.HasMembersDefined)
			{
				throw new InternalErrorException("Helper class already defined!");
			}
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x0002F6F8 File Offset: 0x0002D8F8
		protected override bool DoDefineMembers()
		{
			if (this.Kind == MemberKind.Class && !base.IsStatic && !base.PartialContainer.HasInstanceConstructor)
			{
				this.DefineDefaultConstructor(false);
			}
			return base.DoDefineMembers();
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x0002F72C File Offset: 0x0002D92C
		protected static MemberName MakeMemberName(MemberBase host, string name, int unique_id, TypeParameters tparams, Location loc)
		{
			string name2 = CompilerGeneratedContainer.MakeName((host == null) ? null : ((host is InterfaceMemberBase) ? ((InterfaceMemberBase)host).GetFullName(host.MemberName) : host.MemberName.Name), "c", name, unique_id);
			TypeParameters typeParameters = null;
			if (tparams != null)
			{
				typeParameters = new TypeParameters(tparams.Count);
				for (int i = 0; i < tparams.Count; i++)
				{
					typeParameters.Add(null);
				}
			}
			return new MemberName(name2, typeParameters, loc);
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x0002F7A4 File Offset: 0x0002D9A4
		public static string MakeName(string host, string typePrefix, string name, int id)
		{
			return string.Concat(new string[]
			{
				"<",
				host,
				">",
				typePrefix,
				"__",
				name,
				id.ToString("X")
			});
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x0002F7E4 File Offset: 0x0002D9E4
		protected override TypeSpec[] ResolveBaseTypes(out FullNamedExpression base_class)
		{
			this.base_type = this.Compiler.BuiltinTypes.Object;
			base_class = null;
			return null;
		}
	}
}
