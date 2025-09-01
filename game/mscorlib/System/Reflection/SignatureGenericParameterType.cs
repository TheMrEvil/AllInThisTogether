using System;

namespace System.Reflection
{
	// Token: 0x020008C5 RID: 2245
	internal abstract class SignatureGenericParameterType : SignatureType
	{
		// Token: 0x06004A41 RID: 19009 RVA: 0x000EF8AD File Offset: 0x000EDAAD
		protected SignatureGenericParameterType(int position)
		{
			this._position = position;
		}

		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x06004A42 RID: 19010 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public sealed override bool IsTypeDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x06004A43 RID: 19011 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public sealed override bool IsGenericTypeDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004A44 RID: 19012 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		protected sealed override bool HasElementTypeImpl()
		{
			return false;
		}

		// Token: 0x06004A45 RID: 19013 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		protected sealed override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x06004A46 RID: 19014 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		protected sealed override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x06004A47 RID: 19015 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public sealed override bool IsByRefLike
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004A48 RID: 19016 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		protected sealed override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x06004A49 RID: 19017 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public sealed override bool IsSZArray
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x06004A4A RID: 19018 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public sealed override bool IsVariableBoundArray
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x06004A4B RID: 19019 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public sealed override bool IsConstructedGenericType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x06004A4C RID: 19020 RVA: 0x000040F7 File Offset: 0x000022F7
		public sealed override bool IsGenericParameter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x06004A4D RID: 19021
		public abstract override bool IsGenericMethodParameter { get; }

		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x06004A4E RID: 19022 RVA: 0x000040F7 File Offset: 0x000022F7
		public sealed override bool ContainsGenericParameters
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x06004A4F RID: 19023 RVA: 0x0000AF5E File Offset: 0x0000915E
		internal sealed override SignatureType ElementType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004A50 RID: 19024 RVA: 0x000EF703 File Offset: 0x000ED903
		public sealed override int GetArrayRank()
		{
			throw new ArgumentException("Must be an array type.");
		}

		// Token: 0x06004A51 RID: 19025 RVA: 0x000EF8BC File Offset: 0x000EDABC
		public sealed override Type GetGenericTypeDefinition()
		{
			throw new InvalidOperationException("This operation is only valid on generic types.");
		}

		// Token: 0x06004A52 RID: 19026 RVA: 0x000EF2B2 File Offset: 0x000ED4B2
		public sealed override Type[] GetGenericArguments()
		{
			return Array.Empty<Type>();
		}

		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x06004A53 RID: 19027 RVA: 0x000EF2B2 File Offset: 0x000ED4B2
		public sealed override Type[] GenericTypeArguments
		{
			get
			{
				return Array.Empty<Type>();
			}
		}

		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x06004A54 RID: 19028 RVA: 0x000EF8C8 File Offset: 0x000EDAC8
		public sealed override int GenericParameterPosition
		{
			get
			{
				return this._position;
			}
		}

		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x06004A55 RID: 19029
		public abstract override string Name { get; }

		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x06004A56 RID: 19030 RVA: 0x0000AF5E File Offset: 0x0000915E
		public sealed override string Namespace
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004A57 RID: 19031 RVA: 0x00047BB4 File Offset: 0x00045DB4
		public sealed override string ToString()
		{
			return this.Name;
		}

		// Token: 0x04002F33 RID: 12083
		private readonly int _position;
	}
}
