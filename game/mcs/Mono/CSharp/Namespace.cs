using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x0200025B RID: 603
	public class Namespace
	{
		// Token: 0x06001DCE RID: 7630 RVA: 0x00091998 File Offset: 0x0008FB98
		public Namespace(Namespace parent, string name) : this()
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.parent = parent;
			string text = (parent != null) ? parent.fullname : null;
			if (text == null)
			{
				this.fullname = name;
			}
			else
			{
				this.fullname = text + "." + name;
			}
			while (parent.parent != null)
			{
				parent = parent.parent;
			}
			RootNamespace rootNamespace = parent as RootNamespace;
			if (rootNamespace == null)
			{
				throw new InternalErrorException("Root namespaces must be created using RootNamespace");
			}
			rootNamespace.RegisterNamespace(this);
		}

		// Token: 0x06001DCF RID: 7631 RVA: 0x00091A17 File Offset: 0x0008FC17
		protected Namespace()
		{
			this.namespaces = new Dictionary<string, Namespace>();
			this.cached_types = new Dictionary<string, TypeSpec>();
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06001DD0 RID: 7632 RVA: 0x00091A35 File Offset: 0x0008FC35
		public string Name
		{
			get
			{
				return this.fullname;
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06001DD1 RID: 7633 RVA: 0x00091A3D File Offset: 0x0008FC3D
		public Namespace Parent
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x06001DD2 RID: 7634 RVA: 0x00091A45 File Offset: 0x0008FC45
		public Namespace AddNamespace(MemberName name)
		{
			return ((name.Left == null) ? this : this.AddNamespace(name.Left)).TryAddNamespace(name.Basename);
		}

		// Token: 0x06001DD3 RID: 7635 RVA: 0x00091A6C File Offset: 0x0008FC6C
		private Namespace TryAddNamespace(string name)
		{
			Namespace @namespace;
			if (!this.namespaces.TryGetValue(name, out @namespace))
			{
				@namespace = new Namespace(this, name);
				this.namespaces.Add(name, @namespace);
			}
			return @namespace;
		}

		// Token: 0x06001DD4 RID: 7636 RVA: 0x00091A9F File Offset: 0x0008FC9F
		public bool TryGetNamespace(string name, out Namespace ns)
		{
			return this.namespaces.TryGetValue(name, out ns);
		}

		// Token: 0x06001DD5 RID: 7637 RVA: 0x00091AB0 File Offset: 0x0008FCB0
		public Namespace GetNamespace(string name, bool create)
		{
			int num = name.IndexOf('.');
			string text;
			if (num >= 0)
			{
				text = name.Substring(0, num);
			}
			else
			{
				text = name;
			}
			Namespace @namespace;
			if (!this.namespaces.TryGetValue(text, out @namespace))
			{
				if (!create)
				{
					return null;
				}
				@namespace = new Namespace(this, text);
				this.namespaces.Add(text, @namespace);
			}
			if (num >= 0)
			{
				@namespace = @namespace.GetNamespace(name.Substring(num + 1), create);
			}
			return @namespace;
		}

		// Token: 0x06001DD6 RID: 7638 RVA: 0x00091B18 File Offset: 0x0008FD18
		public IList<TypeSpec> GetAllTypes(string name)
		{
			IList<TypeSpec> result;
			if (this.types == null || !this.types.TryGetValue(name, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x06001DD7 RID: 7639 RVA: 0x00091A35 File Offset: 0x0008FC35
		public virtual string GetSignatureForError()
		{
			return this.fullname;
		}

		// Token: 0x06001DD8 RID: 7640 RVA: 0x00091B40 File Offset: 0x0008FD40
		public TypeSpec LookupType(IMemberContext ctx, string name, int arity, LookupMode mode, Location loc)
		{
			if (this.types == null)
			{
				return null;
			}
			TypeSpec typeSpec = null;
			if (arity == 0 && this.cached_types.TryGetValue(name, out typeSpec) && (typeSpec != null || mode != LookupMode.IgnoreAccessibility))
			{
				return typeSpec;
			}
			IList<TypeSpec> list;
			if (!this.types.TryGetValue(name, out list))
			{
				return null;
			}
			foreach (TypeSpec typeSpec2 in list)
			{
				if (typeSpec2.Arity == arity)
				{
					if (typeSpec == null)
					{
						if ((typeSpec2.Modifiers & Modifiers.INTERNAL) == (Modifiers)0 || typeSpec2.MemberDefinition.IsInternalAsPublic(ctx.Module.DeclaringAssembly) || mode == LookupMode.IgnoreAccessibility)
						{
							typeSpec = typeSpec2;
							continue;
						}
						continue;
					}
					else if (typeSpec.MemberDefinition.IsImported && typeSpec2.MemberDefinition.IsImported)
					{
						if (typeSpec2.Kind == MemberKind.MissingType)
						{
							continue;
						}
						if (typeSpec.Kind == MemberKind.MissingType)
						{
							typeSpec = typeSpec2;
							continue;
						}
						if (mode == LookupMode.Normal)
						{
							ctx.Module.Compiler.Report.SymbolRelatedToPreviousError(typeSpec);
							ctx.Module.Compiler.Report.SymbolRelatedToPreviousError(typeSpec2);
							ctx.Module.Compiler.Report.Error(433, loc, "The imported type `{0}' is defined multiple times", typeSpec2.GetSignatureForError());
							break;
						}
						break;
					}
					else
					{
						if (typeSpec2.Kind == MemberKind.MissingType)
						{
							continue;
						}
						if (typeSpec.MemberDefinition.IsImported)
						{
							typeSpec = typeSpec2;
						}
						if (((typeSpec.Modifiers & Modifiers.INTERNAL) != (Modifiers)0 && !typeSpec.MemberDefinition.IsInternalAsPublic(ctx.Module.DeclaringAssembly)) || mode != LookupMode.Normal)
						{
							continue;
						}
						if (typeSpec2.MemberDefinition.IsImported)
						{
							ctx.Module.Compiler.Report.SymbolRelatedToPreviousError(typeSpec);
							ctx.Module.Compiler.Report.SymbolRelatedToPreviousError(typeSpec2);
						}
						ctx.Module.Compiler.Report.Warning(436, 2, loc, "The type `{0}' conflicts with the imported type of same name'. Ignoring the imported type definition", typeSpec.GetSignatureForError());
					}
				}
				if (arity < 0)
				{
					if (typeSpec == null)
					{
						typeSpec = typeSpec2;
					}
					else if (Math.Abs(typeSpec2.Arity + arity) < Math.Abs(typeSpec.Arity + arity))
					{
						typeSpec = typeSpec2;
					}
				}
			}
			if (arity == 0 && mode == LookupMode.Normal)
			{
				this.cached_types.Add(name, typeSpec);
			}
			if (typeSpec != null)
			{
				List<MissingTypeSpecReference> missingDependencies = typeSpec.GetMissingDependencies();
				if (missingDependencies != null)
				{
					ImportedTypeDefinition.Error_MissingDependency(ctx, missingDependencies, loc);
				}
			}
			return typeSpec;
		}

		// Token: 0x06001DD9 RID: 7641 RVA: 0x00091DB4 File Offset: 0x0008FFB4
		public FullNamedExpression LookupTypeOrNamespace(IMemberContext ctx, string name, int arity, LookupMode mode, Location loc)
		{
			TypeSpec typeSpec = this.LookupType(ctx, name, arity, mode, loc);
			Namespace @namespace;
			if (arity == 0 && this.namespaces.TryGetValue(name, out @namespace))
			{
				if (typeSpec == null)
				{
					return new NamespaceExpression(@namespace, loc);
				}
				if (mode != LookupMode.Probing)
				{
					ctx.Module.Compiler.Report.Warning(437, 2, loc, "The type `{0}' conflicts with the imported namespace `{1}'. Using the definition found in the source file", typeSpec.GetSignatureForError(), @namespace.GetSignatureForError());
				}
				if (typeSpec.MemberDefinition.IsImported)
				{
					return new NamespaceExpression(@namespace, loc);
				}
			}
			if (typeSpec == null)
			{
				return null;
			}
			return new TypeExpression(typeSpec, loc);
		}

		// Token: 0x06001DDA RID: 7642 RVA: 0x00091E44 File Offset: 0x00090044
		public IEnumerable<string> CompletionGetTypesStartingWith(string prefix)
		{
			if (this.types == null)
			{
				return Enumerable.Empty<string>();
			}
			IEnumerable<string> enumerable = from item in this.types.Where(delegate(KeyValuePair<string, IList<TypeSpec>> item)
			{
				if (item.Key.StartsWith(prefix))
				{
					return item.Value.Any((TypeSpec l) => (l.Modifiers & Modifiers.PUBLIC) > (Modifiers)0);
				}
				return false;
			})
			select item.Key;
			if (this.namespaces != null)
			{
				enumerable = enumerable.Concat(from item in this.namespaces
				where item.Key.StartsWith(prefix)
				select item.Key);
			}
			return enumerable;
		}

		// Token: 0x06001DDB RID: 7643 RVA: 0x00091EF4 File Offset: 0x000900F4
		public List<MethodSpec> LookupExtensionMethod(IMemberContext invocationContext, string name, int arity)
		{
			if (this.extension_method_types == null)
			{
				return null;
			}
			List<MethodSpec> list = null;
			for (int i = 0; i < this.extension_method_types.Count; i++)
			{
				TypeSpec typeSpec = this.extension_method_types[i];
				if ((typeSpec.Modifiers & Modifiers.METHOD_EXTENSION) == (Modifiers)0)
				{
					if (this.extension_method_types.Count == 1)
					{
						this.extension_method_types = null;
						return list;
					}
					this.extension_method_types.RemoveAt(i--);
				}
				else
				{
					List<MethodSpec> list2 = typeSpec.MemberCache.FindExtensionMethods(invocationContext, name, arity);
					if (list2 != null)
					{
						if (list == null)
						{
							list = list2;
						}
						else
						{
							list.AddRange(list2);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06001DDC RID: 7644 RVA: 0x00091F88 File Offset: 0x00090188
		public void AddType(ModuleContainer module, TypeSpec ts)
		{
			if (this.types == null)
			{
				this.types = new Dictionary<string, IList<TypeSpec>>(64);
			}
			if (ts.IsClass && ts.Arity == 0 && (ts.MemberDefinition.IsImported ? ((ts.Modifiers & Modifiers.METHOD_EXTENSION) > (Modifiers)0) : (ts.IsStatic || ts.MemberDefinition.IsPartial)))
			{
				if (this.extension_method_types == null)
				{
					this.extension_method_types = new List<TypeSpec>();
				}
				this.extension_method_types.Add(ts);
			}
			string name = ts.Name;
			IList<TypeSpec> list;
			if (this.types.TryGetValue(name, out list))
			{
				if (list.Count == 1)
				{
					TypeSpec typeSpec = list[0];
					if (ts.Arity == typeSpec.Arity)
					{
						TypeSpec typeSpec2 = Namespace.IsImportedTypeOverride(module, ts, typeSpec);
						if (typeSpec2 == typeSpec)
						{
							return;
						}
						if (typeSpec2 != null)
						{
							list[0] = typeSpec2;
							return;
						}
					}
					list = new List<TypeSpec>();
					list.Add(typeSpec);
					this.types[name] = list;
				}
				else
				{
					for (int i = 0; i < list.Count; i++)
					{
						TypeSpec typeSpec = list[i];
						if (ts.Arity == typeSpec.Arity)
						{
							TypeSpec typeSpec2 = Namespace.IsImportedTypeOverride(module, ts, typeSpec);
							if (typeSpec2 == typeSpec)
							{
								return;
							}
							if (typeSpec2 != null)
							{
								list.RemoveAt(i);
								i--;
							}
						}
					}
				}
				list.Add(ts);
				return;
			}
			this.types.Add(name, new TypeSpec[]
			{
				ts
			});
		}

		// Token: 0x06001DDD RID: 7645 RVA: 0x000920E8 File Offset: 0x000902E8
		public static TypeSpec IsImportedTypeOverride(ModuleContainer module, TypeSpec ts, TypeSpec found)
		{
			bool flag = (ts.Modifiers & Modifiers.PUBLIC) != (Modifiers)0 || ts.MemberDefinition.IsInternalAsPublic(module.DeclaringAssembly);
			bool flag2 = (found.Modifiers & Modifiers.PUBLIC) != (Modifiers)0 || found.MemberDefinition.IsInternalAsPublic(module.DeclaringAssembly);
			if (flag && !flag2)
			{
				return ts;
			}
			if (!flag)
			{
				return found;
			}
			return null;
		}

		// Token: 0x06001DDE RID: 7646 RVA: 0x00092144 File Offset: 0x00090344
		public void RemoveContainer(TypeContainer tc)
		{
			IList<TypeSpec> list;
			if (this.types.TryGetValue(tc.MemberName.Name, out list))
			{
				int i = 0;
				while (i < list.Count)
				{
					if (tc.MemberName.Arity == list[i].Arity)
					{
						if (list.Count == 1)
						{
							this.types.Remove(tc.MemberName.Name);
							break;
						}
						list.RemoveAt(i);
						break;
					}
					else
					{
						i++;
					}
				}
			}
			this.cached_types.Remove(tc.MemberName.Basename);
		}

		// Token: 0x06001DDF RID: 7647 RVA: 0x000921D8 File Offset: 0x000903D8
		public void SetBuiltinType(BuiltinTypeSpec pts)
		{
			ICollection<TypeSpec> collection = this.types[pts.Name];
			this.cached_types.Remove(pts.Name);
			if (collection.Count == 1)
			{
				this.types[pts.Name][0] = pts;
				return;
			}
			throw new NotImplementedException();
		}

		// Token: 0x06001DE0 RID: 7648 RVA: 0x00092230 File Offset: 0x00090430
		public void VerifyClsCompliance()
		{
			if (this.types == null || this.cls_checked)
			{
				return;
			}
			this.cls_checked = true;
			Dictionary<string, List<TypeSpec>> dictionary = new Dictionary<string, List<TypeSpec>>(StringComparer.OrdinalIgnoreCase);
			foreach (IList<TypeSpec> list in this.types.Values)
			{
				foreach (TypeSpec typeSpec in list)
				{
					if ((typeSpec.Modifiers & Modifiers.PUBLIC) != (Modifiers)0 && typeSpec.IsCLSCompliant())
					{
						List<TypeSpec> list2;
						if (!dictionary.TryGetValue(typeSpec.Name, out list2))
						{
							list2 = new List<TypeSpec>();
							dictionary.Add(typeSpec.Name, list2);
						}
						list2.Add(typeSpec);
					}
				}
			}
			foreach (List<TypeSpec> list3 in dictionary.Values)
			{
				if (list3.Count >= 2)
				{
					bool flag = true;
					foreach (TypeSpec typeSpec2 in list3)
					{
						flag = (typeSpec2.Name == list3[0].Name);
						if (!flag)
						{
							break;
						}
					}
					if (!flag)
					{
						TypeContainer typeContainer = null;
						foreach (TypeSpec typeSpec3 in list3)
						{
							if (!typeSpec3.MemberDefinition.IsImported)
							{
								if (typeContainer != null)
								{
									typeContainer.Compiler.Report.SymbolRelatedToPreviousError(typeContainer);
								}
								typeContainer = (typeSpec3.MemberDefinition as TypeContainer);
							}
							else
							{
								typeContainer.Compiler.Report.SymbolRelatedToPreviousError(typeSpec3);
							}
						}
						typeContainer.Compiler.Report.Warning(3005, 1, typeContainer.Location, "Identifier `{0}' differing only in case is not CLS-compliant", typeContainer.GetSignatureForError());
					}
				}
			}
		}

		// Token: 0x04000B16 RID: 2838
		private readonly Namespace parent;

		// Token: 0x04000B17 RID: 2839
		private string fullname;

		// Token: 0x04000B18 RID: 2840
		protected Dictionary<string, Namespace> namespaces;

		// Token: 0x04000B19 RID: 2841
		protected Dictionary<string, IList<TypeSpec>> types;

		// Token: 0x04000B1A RID: 2842
		private List<TypeSpec> extension_method_types;

		// Token: 0x04000B1B RID: 2843
		private Dictionary<string, TypeSpec> cached_types;

		// Token: 0x04000B1C RID: 2844
		private bool cls_checked;

		// Token: 0x020003D2 RID: 978
		[CompilerGenerated]
		private sealed class <>c__DisplayClass21_0
		{
			// Token: 0x06002773 RID: 10099 RVA: 0x00002CCC File Offset: 0x00000ECC
			public <>c__DisplayClass21_0()
			{
			}

			// Token: 0x06002774 RID: 10100 RVA: 0x000BC35C File Offset: 0x000BA55C
			internal bool <CompletionGetTypesStartingWith>b__0(KeyValuePair<string, IList<TypeSpec>> item)
			{
				if (item.Key.StartsWith(this.prefix))
				{
					return item.Value.Any(new Func<TypeSpec, bool>(Namespace.<>c.<>9.<CompletionGetTypesStartingWith>b__21_1));
				}
				return false;
			}

			// Token: 0x06002775 RID: 10101 RVA: 0x000BC3AA File Offset: 0x000BA5AA
			internal bool <CompletionGetTypesStartingWith>b__3(KeyValuePair<string, Namespace> item)
			{
				return item.Key.StartsWith(this.prefix);
			}

			// Token: 0x040010EB RID: 4331
			public string prefix;
		}

		// Token: 0x020003D3 RID: 979
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06002776 RID: 10102 RVA: 0x000BC3BE File Offset: 0x000BA5BE
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06002777 RID: 10103 RVA: 0x00002CCC File Offset: 0x00000ECC
			public <>c()
			{
			}

			// Token: 0x06002778 RID: 10104 RVA: 0x000BC3CA File Offset: 0x000BA5CA
			internal bool <CompletionGetTypesStartingWith>b__21_1(TypeSpec l)
			{
				return (l.Modifiers & Modifiers.PUBLIC) > (Modifiers)0;
			}

			// Token: 0x06002779 RID: 10105 RVA: 0x000BC3D7 File Offset: 0x000BA5D7
			internal string <CompletionGetTypesStartingWith>b__21_2(KeyValuePair<string, IList<TypeSpec>> item)
			{
				return item.Key;
			}

			// Token: 0x0600277A RID: 10106 RVA: 0x000BC3E0 File Offset: 0x000BA5E0
			internal string <CompletionGetTypesStartingWith>b__21_4(KeyValuePair<string, Namespace> item)
			{
				return item.Key;
			}

			// Token: 0x040010EC RID: 4332
			public static readonly Namespace.<>c <>9 = new Namespace.<>c();

			// Token: 0x040010ED RID: 4333
			public static Func<TypeSpec, bool> <>9__21_1;

			// Token: 0x040010EE RID: 4334
			public static Func<KeyValuePair<string, IList<TypeSpec>>, string> <>9__21_2;

			// Token: 0x040010EF RID: 4335
			public static Func<KeyValuePair<string, Namespace>, string> <>9__21_4;
		}
	}
}
