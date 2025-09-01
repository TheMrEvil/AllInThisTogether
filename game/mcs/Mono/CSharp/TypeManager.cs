using System;
using System.Collections.Generic;
using System.Text;

namespace Mono.CSharp
{
	// Token: 0x020002D6 RID: 726
	internal class TypeManager
	{
		// Token: 0x06002288 RID: 8840 RVA: 0x000AA564 File Offset: 0x000A8764
		public static string CSharpName(IList<TypeSpec> types)
		{
			if (types.Count == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < types.Count; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(types[i].GetSignatureForError());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002289 RID: 8841 RVA: 0x00067B6F File Offset: 0x00065D6F
		public static string GetFullNameSignature(MemberSpec mi)
		{
			return mi.GetSignatureForError();
		}

		// Token: 0x0600228A RID: 8842 RVA: 0x00067B6F File Offset: 0x00065D6F
		public static string CSharpSignature(MemberSpec mb)
		{
			return mb.GetSignatureForError();
		}

		// Token: 0x0600228B RID: 8843 RVA: 0x000AA5C0 File Offset: 0x000A87C0
		public static bool IsFamilyAccessible(TypeSpec type, TypeSpec parent)
		{
			if (type.Kind != MemberKind.TypeParameter || parent.Kind != MemberKind.TypeParameter)
			{
				while (!TypeManager.IsInstantiationOfSameGenericType(type, parent))
				{
					type = type.BaseType;
					if (type == null)
					{
						return false;
					}
				}
				return true;
			}
			if (type == parent)
			{
				return true;
			}
			throw new NotImplementedException("net");
		}

		// Token: 0x0600228C RID: 8844 RVA: 0x000AA60F File Offset: 0x000A880F
		public static bool IsNestedChildOf(TypeSpec type, ITypeDefinition parent)
		{
			if (type == null)
			{
				return false;
			}
			if (type.MemberDefinition == parent)
			{
				return false;
			}
			for (type = type.DeclaringType; type != null; type = type.DeclaringType)
			{
				if (type.MemberDefinition == parent)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600228D RID: 8845 RVA: 0x000AA642 File Offset: 0x000A8842
		public static TypeSpec GetElementType(TypeSpec t)
		{
			return ((ElementTypeSpec)t).Element;
		}

		// Token: 0x0600228E RID: 8846 RVA: 0x000AA64F File Offset: 0x000A884F
		public static bool HasElementType(TypeSpec t)
		{
			return t is ElementTypeSpec;
		}

		// Token: 0x0600228F RID: 8847 RVA: 0x000AA65C File Offset: 0x000A885C
		public static bool VerifyUnmanaged(ModuleContainer rc, TypeSpec t, Location loc)
		{
			if (t.IsUnmanaged)
			{
				return true;
			}
			while (t.IsPointer)
			{
				t = ((ElementTypeSpec)t).Element;
			}
			rc.Compiler.Report.SymbolRelatedToPreviousError(t);
			rc.Compiler.Report.Error(208, loc, "Cannot take the address of, get the size of, or declare a pointer to a managed type `{0}'", t.GetSignatureForError());
			return false;
		}

		// Token: 0x06002290 RID: 8848 RVA: 0x000AA6BB File Offset: 0x000A88BB
		public static bool IsGenericParameter(TypeSpec type)
		{
			return type.IsGenericParameter;
		}

		// Token: 0x06002291 RID: 8849 RVA: 0x000AA6C3 File Offset: 0x000A88C3
		public static bool IsGenericType(TypeSpec type)
		{
			return type.IsGeneric;
		}

		// Token: 0x06002292 RID: 8850 RVA: 0x000AA6CB File Offset: 0x000A88CB
		public static TypeSpec[] GetTypeArguments(TypeSpec t)
		{
			return t.TypeArguments;
		}

		// Token: 0x06002293 RID: 8851 RVA: 0x000AA6D3 File Offset: 0x000A88D3
		public static bool IsInstantiationOfSameGenericType(TypeSpec type, TypeSpec parent)
		{
			return type == parent || type.MemberDefinition == parent.MemberDefinition;
		}

		// Token: 0x06002294 RID: 8852 RVA: 0x00002CCC File Offset: 0x00000ECC
		public TypeManager()
		{
		}
	}
}
