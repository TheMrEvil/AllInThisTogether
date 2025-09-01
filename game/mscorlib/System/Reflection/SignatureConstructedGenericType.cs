using System;
using System.Text;

namespace System.Reflection
{
	// Token: 0x020008C3 RID: 2243
	internal sealed class SignatureConstructedGenericType : SignatureType
	{
		// Token: 0x06004A25 RID: 18981 RVA: 0x000EF718 File Offset: 0x000ED918
		internal SignatureConstructedGenericType(Type genericTypeDefinition, Type[] typeArguments)
		{
			if (genericTypeDefinition == null)
			{
				throw new ArgumentNullException("genericTypeDefinition");
			}
			if (typeArguments == null)
			{
				throw new ArgumentNullException("typeArguments");
			}
			typeArguments = (Type[])typeArguments.Clone();
			for (int i = 0; i < typeArguments.Length; i++)
			{
				if (typeArguments[i] == null)
				{
					throw new ArgumentNullException("typeArguments");
				}
			}
			this._genericTypeDefinition = genericTypeDefinition;
			this._genericTypeArguments = typeArguments;
		}

		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x06004A26 RID: 18982 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public sealed override bool IsTypeDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x06004A27 RID: 18983 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public sealed override bool IsGenericTypeDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004A28 RID: 18984 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		protected sealed override bool HasElementTypeImpl()
		{
			return false;
		}

		// Token: 0x06004A29 RID: 18985 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		protected sealed override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x06004A2A RID: 18986 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		protected sealed override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x06004A2B RID: 18987 RVA: 0x000EF78C File Offset: 0x000ED98C
		public sealed override bool IsByRefLike
		{
			get
			{
				return this._genericTypeDefinition.IsByRefLike;
			}
		}

		// Token: 0x06004A2C RID: 18988 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		protected sealed override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x06004A2D RID: 18989 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public sealed override bool IsSZArray
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x06004A2E RID: 18990 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public sealed override bool IsVariableBoundArray
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x06004A2F RID: 18991 RVA: 0x000040F7 File Offset: 0x000022F7
		public sealed override bool IsConstructedGenericType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x06004A30 RID: 18992 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public sealed override bool IsGenericParameter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x06004A31 RID: 18993 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public sealed override bool IsGenericTypeParameter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x06004A32 RID: 18994 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public sealed override bool IsGenericMethodParameter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x06004A33 RID: 18995 RVA: 0x000EF79C File Offset: 0x000ED99C
		public sealed override bool ContainsGenericParameters
		{
			get
			{
				for (int i = 0; i < this._genericTypeArguments.Length; i++)
				{
					if (this._genericTypeArguments[i].ContainsGenericParameters)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x06004A34 RID: 18996 RVA: 0x0000AF5E File Offset: 0x0000915E
		internal sealed override SignatureType ElementType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004A35 RID: 18997 RVA: 0x000EF703 File Offset: 0x000ED903
		public sealed override int GetArrayRank()
		{
			throw new ArgumentException("Must be an array type.");
		}

		// Token: 0x06004A36 RID: 18998 RVA: 0x000EF7CE File Offset: 0x000ED9CE
		public sealed override Type GetGenericTypeDefinition()
		{
			return this._genericTypeDefinition;
		}

		// Token: 0x06004A37 RID: 18999 RVA: 0x000EF7D6 File Offset: 0x000ED9D6
		public sealed override Type[] GetGenericArguments()
		{
			return this.GenericTypeArguments;
		}

		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x06004A38 RID: 19000 RVA: 0x000EF7DE File Offset: 0x000ED9DE
		public sealed override Type[] GenericTypeArguments
		{
			get
			{
				return (Type[])this._genericTypeArguments.Clone();
			}
		}

		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x06004A39 RID: 19001 RVA: 0x000472BC File Offset: 0x000454BC
		public sealed override int GenericParameterPosition
		{
			get
			{
				throw new InvalidOperationException("Method may only be called on a Type for which Type.IsGenericParameter is true.");
			}
		}

		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x06004A3A RID: 19002 RVA: 0x000EF7F0 File Offset: 0x000ED9F0
		public sealed override string Name
		{
			get
			{
				return this._genericTypeDefinition.Name;
			}
		}

		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x06004A3B RID: 19003 RVA: 0x000EF7FD File Offset: 0x000ED9FD
		public sealed override string Namespace
		{
			get
			{
				return this._genericTypeDefinition.Namespace;
			}
		}

		// Token: 0x06004A3C RID: 19004 RVA: 0x000EF80C File Offset: 0x000EDA0C
		public sealed override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this._genericTypeDefinition.ToString());
			stringBuilder.Append('[');
			for (int i = 0; i < this._genericTypeArguments.Length; i++)
			{
				if (i != 0)
				{
					stringBuilder.Append(',');
				}
				stringBuilder.Append(this._genericTypeArguments[i].ToString());
			}
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}

		// Token: 0x04002F31 RID: 12081
		private readonly Type _genericTypeDefinition;

		// Token: 0x04002F32 RID: 12082
		private readonly Type[] _genericTypeArguments;
	}
}
