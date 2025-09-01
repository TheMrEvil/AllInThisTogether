using System;

namespace System.Reflection
{
	// Token: 0x020008C7 RID: 2247
	internal sealed class SignaturePointerType : SignatureHasElementType
	{
		// Token: 0x06004A71 RID: 19057 RVA: 0x000EF6FA File Offset: 0x000ED8FA
		internal SignaturePointerType(SignatureType elementType) : base(elementType)
		{
		}

		// Token: 0x06004A72 RID: 19058 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		protected sealed override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x06004A73 RID: 19059 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		protected sealed override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x06004A74 RID: 19060 RVA: 0x000040F7 File Offset: 0x000022F7
		protected sealed override bool IsPointerImpl()
		{
			return true;
		}

		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x06004A75 RID: 19061 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public sealed override bool IsSZArray
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x06004A76 RID: 19062 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public sealed override bool IsVariableBoundArray
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004A77 RID: 19063 RVA: 0x000EF703 File Offset: 0x000ED903
		public sealed override int GetArrayRank()
		{
			throw new ArgumentException("Must be an array type.");
		}

		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x06004A78 RID: 19064 RVA: 0x000EF931 File Offset: 0x000EDB31
		protected sealed override string Suffix
		{
			get
			{
				return "*";
			}
		}
	}
}
