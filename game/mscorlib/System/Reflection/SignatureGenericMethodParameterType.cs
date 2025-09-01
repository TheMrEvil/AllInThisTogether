using System;

namespace System.Reflection
{
	// Token: 0x020008C4 RID: 2244
	internal sealed class SignatureGenericMethodParameterType : SignatureGenericParameterType
	{
		// Token: 0x06004A3D RID: 19005 RVA: 0x000EF87C File Offset: 0x000EDA7C
		internal SignatureGenericMethodParameterType(int position) : base(position)
		{
		}

		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x06004A3E RID: 19006 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public sealed override bool IsGenericTypeParameter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x06004A3F RID: 19007 RVA: 0x000040F7 File Offset: 0x000022F7
		public sealed override bool IsGenericMethodParameter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x06004A40 RID: 19008 RVA: 0x000EF888 File Offset: 0x000EDA88
		public sealed override string Name
		{
			get
			{
				return "!!" + this.GenericParameterPosition.ToString();
			}
		}
	}
}
