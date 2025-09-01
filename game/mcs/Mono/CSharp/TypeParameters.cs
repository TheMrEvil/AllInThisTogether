using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;

namespace Mono.CSharp
{
	// Token: 0x02000227 RID: 551
	public class TypeParameters
	{
		// Token: 0x06001C00 RID: 7168 RVA: 0x00087830 File Offset: 0x00085A30
		public TypeParameters()
		{
			this.names = new List<TypeParameter>();
		}

		// Token: 0x06001C01 RID: 7169 RVA: 0x00087843 File Offset: 0x00085A43
		public TypeParameters(int count)
		{
			this.names = new List<TypeParameter>(count);
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06001C02 RID: 7170 RVA: 0x00087857 File Offset: 0x00085A57
		public int Count
		{
			get
			{
				return this.names.Count;
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06001C03 RID: 7171 RVA: 0x00087864 File Offset: 0x00085A64
		public TypeParameterSpec[] Types
		{
			get
			{
				return this.types;
			}
		}

		// Token: 0x06001C04 RID: 7172 RVA: 0x0008786C File Offset: 0x00085A6C
		public void Add(TypeParameter tparam)
		{
			this.names.Add(tparam);
		}

		// Token: 0x06001C05 RID: 7173 RVA: 0x0008787A File Offset: 0x00085A7A
		public void Add(TypeParameters tparams)
		{
			this.names.AddRange(tparams.names);
		}

		// Token: 0x06001C06 RID: 7174 RVA: 0x00087890 File Offset: 0x00085A90
		public void Create(TypeSpec declaringType, int parentOffset, TypeContainer parent)
		{
			this.types = new TypeParameterSpec[this.Count];
			for (int i = 0; i < this.types.Length; i++)
			{
				TypeParameter typeParameter = this.names[i];
				typeParameter.Create(declaringType, parent);
				this.types[i] = typeParameter.Type;
				this.types[i].DeclaredPosition = i + parentOffset;
				if (typeParameter.Variance != Variance.None && (declaringType == null || (declaringType.Kind != MemberKind.Interface && declaringType.Kind != MemberKind.Delegate)))
				{
					parent.Compiler.Report.Error(1960, typeParameter.Location, "Variant type parameters can only be used with interfaces and delegates");
				}
			}
		}

		// Token: 0x06001C07 RID: 7175 RVA: 0x0008793C File Offset: 0x00085B3C
		public void Define(GenericTypeParameterBuilder[] builders)
		{
			for (int i = 0; i < this.types.Length; i++)
			{
				this.names[i].Define(builders[this.types[i].DeclaredPosition]);
			}
		}

		// Token: 0x17000667 RID: 1639
		public TypeParameter this[int index]
		{
			get
			{
				return this.names[index];
			}
			set
			{
				this.names[index] = value;
			}
		}

		// Token: 0x06001C0A RID: 7178 RVA: 0x0008799C File Offset: 0x00085B9C
		public TypeParameter Find(string name)
		{
			foreach (TypeParameter typeParameter in this.names)
			{
				if (typeParameter.Name == name)
				{
					return typeParameter;
				}
			}
			return null;
		}

		// Token: 0x06001C0B RID: 7179 RVA: 0x00087A00 File Offset: 0x00085C00
		public string[] GetAllNames()
		{
			return (from l in this.names
			select l.Name).ToArray<string>();
		}

		// Token: 0x06001C0C RID: 7180 RVA: 0x00087A34 File Offset: 0x00085C34
		public string GetSignatureForError()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.Count; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(',');
				}
				TypeParameter typeParameter = this.names[i];
				if (typeParameter != null)
				{
					stringBuilder.Append(typeParameter.GetSignatureForError());
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001C0D RID: 7181 RVA: 0x00087A88 File Offset: 0x00085C88
		public void CheckPartialConstraints(Method part)
		{
			TypeParameters currentTypeParameters = part.CurrentTypeParameters;
			int i = 0;
			while (i < this.Count)
			{
				TypeParameter typeParameter = this.names[i];
				TypeParameter typeParameter2 = currentTypeParameters[i];
				if (typeParameter.Constraints == null)
				{
					if (typeParameter2.Constraints != null)
					{
						goto IL_50;
					}
				}
				else if (typeParameter2.Constraints == null || !typeParameter.Type.HasSameConstraintsDefinition(typeParameter2.Type))
				{
					goto IL_50;
				}
				IL_A8:
				i++;
				continue;
				IL_50:
				part.Compiler.Report.SymbolRelatedToPreviousError(this[i].CurrentMemberDefinition.Location, "");
				part.Compiler.Report.Error(761, part.Location, "Partial method declarations of `{0}' have inconsistent constraints for type parameter `{1}'", part.GetSignatureForError(), currentTypeParameters[i].GetSignatureForError());
				goto IL_A8;
			}
		}

		// Token: 0x06001C0E RID: 7182 RVA: 0x00087B50 File Offset: 0x00085D50
		public void UpdateConstraints(TypeDefinition part)
		{
			TypeParameters typeParameters = part.MemberName.TypeParameters;
			for (int i = 0; i < this.Count; i++)
			{
				TypeParameter typeParameter = this.names[i];
				if (!typeParameter.AddPartialConstraints(part, typeParameters[i]))
				{
					part.Compiler.Report.SymbolRelatedToPreviousError(this[i].CurrentMemberDefinition);
					part.Compiler.Report.Error(265, part.Location, "Partial declarations of `{0}' have inconsistent constraints for type parameter `{1}'", part.GetSignatureForError(), typeParameter.GetSignatureForError());
				}
			}
		}

		// Token: 0x06001C0F RID: 7183 RVA: 0x00087BE0 File Offset: 0x00085DE0
		public void VerifyClsCompliance()
		{
			foreach (TypeParameter typeParameter in this.names)
			{
				typeParameter.VerifyClsCompliance();
			}
		}

		// Token: 0x04000A5E RID: 2654
		private List<TypeParameter> names;

		// Token: 0x04000A5F RID: 2655
		private TypeParameterSpec[] types;

		// Token: 0x020003C3 RID: 963
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600274D RID: 10061 RVA: 0x000BBF05 File Offset: 0x000BA105
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600274E RID: 10062 RVA: 0x00002CCC File Offset: 0x00000ECC
			public <>c()
			{
			}

			// Token: 0x0600274F RID: 10063 RVA: 0x000BBF11 File Offset: 0x000BA111
			internal string <GetAllNames>b__16_0(TypeParameter l)
			{
				return l.Name;
			}

			// Token: 0x040010B3 RID: 4275
			public static readonly TypeParameters.<>c <>9 = new TypeParameters.<>c();

			// Token: 0x040010B4 RID: 4276
			public static Func<TypeParameter, string> <>9__16_0;
		}
	}
}
