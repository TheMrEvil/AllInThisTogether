using System;

namespace Mono.CSharp
{
	// Token: 0x02000246 RID: 582
	public struct MemberFilter : IEquatable<MemberSpec>
	{
		// Token: 0x06001CD1 RID: 7377 RVA: 0x0008B0A1 File Offset: 0x000892A1
		public MemberFilter(MethodSpec m)
		{
			this.Name = m.Name;
			this.Kind = MemberKind.Method;
			this.Parameters = m.Parameters;
			this.MemberType = m.ReturnType;
			this.Arity = m.Arity;
		}

		// Token: 0x06001CD2 RID: 7378 RVA: 0x0008B0DA File Offset: 0x000892DA
		public MemberFilter(string name, int arity, MemberKind kind, AParametersCollection param, TypeSpec type)
		{
			this.Name = name;
			this.Kind = kind;
			this.Parameters = param;
			this.MemberType = type;
			this.Arity = arity;
		}

		// Token: 0x06001CD3 RID: 7379 RVA: 0x0008B101 File Offset: 0x00089301
		public static MemberFilter Constructor(AParametersCollection param)
		{
			return new MemberFilter(Mono.CSharp.Constructor.ConstructorName, 0, MemberKind.Constructor, param, null);
		}

		// Token: 0x06001CD4 RID: 7380 RVA: 0x0008B111 File Offset: 0x00089311
		public static MemberFilter Property(string name, TypeSpec type)
		{
			return new MemberFilter(name, 0, MemberKind.Property, null, type);
		}

		// Token: 0x06001CD5 RID: 7381 RVA: 0x0008B11E File Offset: 0x0008931E
		public static MemberFilter Field(string name, TypeSpec type)
		{
			return new MemberFilter(name, 0, MemberKind.Field, null, type);
		}

		// Token: 0x06001CD6 RID: 7382 RVA: 0x0008B12A File Offset: 0x0008932A
		public static MemberFilter Method(string name, int arity, AParametersCollection param, TypeSpec type)
		{
			return new MemberFilter(name, arity, MemberKind.Method, param, type);
		}

		// Token: 0x06001CD7 RID: 7383 RVA: 0x0008B138 File Offset: 0x00089338
		public bool Equals(MemberSpec other)
		{
			if ((other.Kind & this.Kind & MemberKind.MaskType) == (MemberKind)0)
			{
				return false;
			}
			if (this.Arity >= 0 && this.Arity != other.Arity)
			{
				return false;
			}
			if (this.Parameters != null)
			{
				if (!(other is IParametersMember))
				{
					return false;
				}
				AParametersCollection parameters = ((IParametersMember)other).Parameters;
				if (!TypeSpecComparer.Override.IsEqual(this.Parameters, parameters))
				{
					return false;
				}
			}
			if (this.MemberType != null)
			{
				if (!(other is IInterfaceMemberSpec))
				{
					return false;
				}
				if (!TypeSpecComparer.Override.IsEqual(((IInterfaceMemberSpec)other).MemberType, this.MemberType))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000ABD RID: 2749
		public readonly string Name;

		// Token: 0x04000ABE RID: 2750
		public readonly MemberKind Kind;

		// Token: 0x04000ABF RID: 2751
		public readonly AParametersCollection Parameters;

		// Token: 0x04000AC0 RID: 2752
		public readonly TypeSpec MemberType;

		// Token: 0x04000AC1 RID: 2753
		public readonly int Arity;
	}
}
