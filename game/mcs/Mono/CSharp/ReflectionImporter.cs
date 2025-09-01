using System;
using System.Collections;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x0200027F RID: 639
	public sealed class ReflectionImporter : MetadataImporter
	{
		// Token: 0x06001F46 RID: 8006 RVA: 0x00099EA5 File Offset: 0x000980A5
		public ReflectionImporter(ModuleContainer module, BuiltinTypes builtin) : base(module)
		{
			this.Initialize(builtin);
		}

		// Token: 0x06001F47 RID: 8007 RVA: 0x0000AF70 File Offset: 0x00009170
		public override void AddCompiledType(TypeBuilder builder, TypeSpec spec)
		{
		}

		// Token: 0x06001F48 RID: 8008 RVA: 0x00099EB5 File Offset: 0x000980B5
		protected override MemberKind DetermineKindFromBaseType(Type baseType)
		{
			if (baseType == typeof(ValueType))
			{
				return MemberKind.Struct;
			}
			if (baseType == typeof(Enum))
			{
				return MemberKind.Enum;
			}
			if (baseType == typeof(MulticastDelegate))
			{
				return MemberKind.Delegate;
			}
			return MemberKind.Class;
		}

		// Token: 0x06001F49 RID: 8009 RVA: 0x00099EF8 File Offset: 0x000980F8
		protected override bool HasVolatileModifier(Type[] modifiers)
		{
			for (int i = 0; i < modifiers.Length; i++)
			{
				if (modifiers[i] == typeof(IsVolatile))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001F4A RID: 8010 RVA: 0x00099F28 File Offset: 0x00098128
		public void ImportAssembly(Assembly assembly, RootNamespace targetNamespace)
		{
			base.GetAssemblyDefinition(assembly);
			Type[] types;
			try
			{
				types = assembly.GetTypes();
			}
			catch (ReflectionTypeLoadException ex)
			{
				types = ex.Types;
			}
			base.ImportTypes(types, targetNamespace, true);
		}

		// Token: 0x06001F4B RID: 8011 RVA: 0x00099F68 File Offset: 0x00098168
		public ImportedModuleDefinition ImportModule(Module module, RootNamespace targetNamespace)
		{
			ImportedModuleDefinition importedModuleDefinition = new ImportedModuleDefinition(module);
			importedModuleDefinition.ReadAttributes();
			Type[] types;
			try
			{
				types = module.GetTypes();
			}
			catch (ReflectionTypeLoadException ex)
			{
				types = ex.Types;
			}
			base.ImportTypes(types, targetNamespace, false);
			return importedModuleDefinition;
		}

		// Token: 0x06001F4C RID: 8012 RVA: 0x00099FB0 File Offset: 0x000981B0
		private void Initialize(BuiltinTypes builtin)
		{
			this.compiled_types.Add(typeof(object), builtin.Object);
			this.compiled_types.Add(typeof(ValueType), builtin.ValueType);
			this.compiled_types.Add(typeof(Attribute), builtin.Attribute);
			this.compiled_types.Add(typeof(int), builtin.Int);
			this.compiled_types.Add(typeof(long), builtin.Long);
			this.compiled_types.Add(typeof(uint), builtin.UInt);
			this.compiled_types.Add(typeof(ulong), builtin.ULong);
			this.compiled_types.Add(typeof(byte), builtin.Byte);
			this.compiled_types.Add(typeof(sbyte), builtin.SByte);
			this.compiled_types.Add(typeof(short), builtin.Short);
			this.compiled_types.Add(typeof(ushort), builtin.UShort);
			this.compiled_types.Add(typeof(IEnumerator), builtin.IEnumerator);
			this.compiled_types.Add(typeof(IEnumerable), builtin.IEnumerable);
			this.compiled_types.Add(typeof(IDisposable), builtin.IDisposable);
			this.compiled_types.Add(typeof(char), builtin.Char);
			this.compiled_types.Add(typeof(string), builtin.String);
			this.compiled_types.Add(typeof(float), builtin.Float);
			this.compiled_types.Add(typeof(double), builtin.Double);
			this.compiled_types.Add(typeof(decimal), builtin.Decimal);
			this.compiled_types.Add(typeof(bool), builtin.Bool);
			this.compiled_types.Add(typeof(IntPtr), builtin.IntPtr);
			this.compiled_types.Add(typeof(UIntPtr), builtin.UIntPtr);
			this.compiled_types.Add(typeof(MulticastDelegate), builtin.MulticastDelegate);
			this.compiled_types.Add(typeof(Delegate), builtin.Delegate);
			this.compiled_types.Add(typeof(Enum), builtin.Enum);
			this.compiled_types.Add(typeof(Array), builtin.Array);
			this.compiled_types.Add(typeof(void), builtin.Void);
			this.compiled_types.Add(typeof(Type), builtin.Type);
			this.compiled_types.Add(typeof(Exception), builtin.Exception);
			this.compiled_types.Add(typeof(RuntimeFieldHandle), builtin.RuntimeFieldHandle);
			this.compiled_types.Add(typeof(RuntimeTypeHandle), builtin.RuntimeTypeHandle);
		}
	}
}
