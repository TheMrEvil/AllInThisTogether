using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x0200091A RID: 2330
	[StructLayout(LayoutKind.Sequential)]
	internal class ByRefType : SymbolType
	{
		// Token: 0x06004F57 RID: 20311 RVA: 0x000F99E5 File Offset: 0x000F7BE5
		internal ByRefType(Type elementType) : base(elementType)
		{
		}

		// Token: 0x06004F58 RID: 20312 RVA: 0x000F99EE File Offset: 0x000F7BEE
		internal override Type InternalResolve()
		{
			return this.m_baseType.InternalResolve().MakeByRefType();
		}

		// Token: 0x06004F59 RID: 20313 RVA: 0x000040F7 File Offset: 0x000022F7
		protected override bool IsByRefImpl()
		{
			return true;
		}

		// Token: 0x06004F5A RID: 20314 RVA: 0x000F9A00 File Offset: 0x000F7C00
		internal override string FormatName(string elementName)
		{
			if (elementName == null)
			{
				return null;
			}
			return elementName + "&";
		}

		// Token: 0x06004F5B RID: 20315 RVA: 0x000F9A12 File Offset: 0x000F7C12
		public override Type MakeArrayType()
		{
			throw new ArgumentException("Cannot create an array type of a byref type");
		}

		// Token: 0x06004F5C RID: 20316 RVA: 0x000F9A12 File Offset: 0x000F7C12
		public override Type MakeArrayType(int rank)
		{
			throw new ArgumentException("Cannot create an array type of a byref type");
		}

		// Token: 0x06004F5D RID: 20317 RVA: 0x000F9A1E File Offset: 0x000F7C1E
		public override Type MakeByRefType()
		{
			throw new ArgumentException("Cannot create a byref type of an already byref type");
		}

		// Token: 0x06004F5E RID: 20318 RVA: 0x000F9A2A File Offset: 0x000F7C2A
		public override Type MakePointerType()
		{
			throw new ArgumentException("Cannot create a pointer type of a byref type");
		}
	}
}
