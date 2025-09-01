using System;
using System.Collections.Generic;
using System.IO;

namespace Mono.CSharp
{
	// Token: 0x020002D3 RID: 723
	public class PredefinedType
	{
		// Token: 0x0600226C RID: 8812 RVA: 0x000A9E25 File Offset: 0x000A8025
		public PredefinedType(ModuleContainer module, MemberKind kind, string ns, string name, int arity) : this(module, kind, ns, name)
		{
			this.arity = arity;
		}

		// Token: 0x0600226D RID: 8813 RVA: 0x000A9E3A File Offset: 0x000A803A
		public PredefinedType(ModuleContainer module, MemberKind kind, string ns, string name)
		{
			this.module = module;
			this.kind = kind;
			this.name = name;
			this.ns = ns;
		}

		// Token: 0x0600226E RID: 8814 RVA: 0x000A9E5F File Offset: 0x000A805F
		public PredefinedType(BuiltinTypeSpec type)
		{
			this.kind = type.Kind;
			this.name = type.Name;
			this.ns = type.Namespace;
			this.type = type;
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x0600226F RID: 8815 RVA: 0x000A9E92 File Offset: 0x000A8092
		public int Arity
		{
			get
			{
				return this.arity;
			}
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x06002270 RID: 8816 RVA: 0x000A9E9A File Offset: 0x000A809A
		public bool IsDefined
		{
			get
			{
				return this.type != null;
			}
		}

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06002271 RID: 8817 RVA: 0x000A9EA5 File Offset: 0x000A80A5
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06002272 RID: 8818 RVA: 0x000A9EAD File Offset: 0x000A80AD
		public string Namespace
		{
			get
			{
				return this.ns;
			}
		}

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06002273 RID: 8819 RVA: 0x000A9EB5 File Offset: 0x000A80B5
		public TypeSpec TypeSpec
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x06002274 RID: 8820 RVA: 0x000A9EC0 File Offset: 0x000A80C0
		public bool Define()
		{
			if (this.type != null)
			{
				return true;
			}
			if (!this.defined)
			{
				this.defined = true;
				this.type = PredefinedType.Resolve(this.module, this.kind, this.ns, this.name, this.arity, false, false);
			}
			return this.type != null;
		}

		// Token: 0x06002275 RID: 8821 RVA: 0x000A9F1A File Offset: 0x000A811A
		public string GetSignatureForError()
		{
			return this.ns + "." + this.name;
		}

		// Token: 0x06002276 RID: 8822 RVA: 0x000A9F34 File Offset: 0x000A8134
		public static TypeSpec Resolve(ModuleContainer module, MemberKind kind, string ns, string name, int arity, bool required, bool reportErrors)
		{
			Namespace @namespace = module.GlobalRootNamespace.GetNamespace(ns, required);
			IList<TypeSpec> list = null;
			if (@namespace != null)
			{
				list = @namespace.GetAllTypes(name);
			}
			if (list == null)
			{
				if (reportErrors)
				{
					module.Compiler.Report.Error(518, "The predefined type `{0}.{1}' is not defined or imported", ns, name);
				}
				return null;
			}
			TypeSpec typeSpec = null;
			foreach (TypeSpec typeSpec2 in list)
			{
				if ((typeSpec2.Kind == kind || (typeSpec2.Kind == MemberKind.Struct && kind == MemberKind.Void && typeSpec2.MemberDefinition is TypeContainer)) && typeSpec2.Arity == arity && ((typeSpec2.Modifiers & Modifiers.INTERNAL) == (Modifiers)0 || typeSpec2.MemberDefinition.IsInternalAsPublic(module.DeclaringAssembly)))
				{
					if (typeSpec != null)
					{
						TypeSpec ms = typeSpec;
						if (!typeSpec.MemberDefinition.IsImported && module.Compiler.BuiltinTypes.Object.MemberDefinition.DeclaringAssembly == typeSpec2.MemberDefinition.DeclaringAssembly)
						{
							typeSpec = typeSpec2;
						}
						string fileName;
						if (typeSpec.MemberDefinition is MemberCore)
						{
							fileName = ((MemberCore)typeSpec.MemberDefinition).Location.Name;
						}
						else
						{
							fileName = Path.GetFileName(((ImportedAssemblyDefinition)typeSpec.MemberDefinition.DeclaringAssembly).Location);
						}
						module.Compiler.Report.SymbolRelatedToPreviousError(ms);
						module.Compiler.Report.SymbolRelatedToPreviousError(typeSpec2);
						module.Compiler.Report.Warning(1685, 1, "The predefined type `{0}.{1}' is defined multiple times. Using definition from `{2}'", new string[]
						{
							ns,
							name,
							fileName
						});
						break;
					}
					typeSpec = typeSpec2;
				}
			}
			if (typeSpec == null && reportErrors)
			{
				TypeSpec typeSpec3 = list[0];
				if (typeSpec3.Kind == MemberKind.MissingType)
				{
					module.Compiler.Report.Error(518, "The predefined type `{0}.{1}' is defined in an assembly that is not referenced.", ns, name);
				}
				else
				{
					Location loc;
					if (typeSpec3.MemberDefinition is MemberCore)
					{
						loc = ((MemberCore)typeSpec3.MemberDefinition).Location;
					}
					else
					{
						loc = Location.Null;
						module.Compiler.Report.SymbolRelatedToPreviousError(typeSpec3);
					}
					module.Compiler.Report.Error(520, loc, "The predefined type `{0}.{1}' is not declared correctly", ns, name);
				}
			}
			return typeSpec;
		}

		// Token: 0x06002277 RID: 8823 RVA: 0x000AA1B0 File Offset: 0x000A83B0
		public TypeSpec Resolve()
		{
			if (this.type == null)
			{
				this.type = PredefinedType.Resolve(this.module, this.kind, this.ns, this.name, this.arity, false, true);
			}
			return this.type;
		}

		// Token: 0x04000D44 RID: 3396
		private readonly string name;

		// Token: 0x04000D45 RID: 3397
		private readonly string ns;

		// Token: 0x04000D46 RID: 3398
		private readonly int arity;

		// Token: 0x04000D47 RID: 3399
		private readonly MemberKind kind;

		// Token: 0x04000D48 RID: 3400
		protected readonly ModuleContainer module;

		// Token: 0x04000D49 RID: 3401
		protected TypeSpec type;

		// Token: 0x04000D4A RID: 3402
		private bool defined;
	}
}
