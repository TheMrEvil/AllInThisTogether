using System;
using IKVM.Reflection.Reader;

namespace IKVM.Reflection
{
	// Token: 0x0200004D RID: 77
	internal abstract class NonPEModule : Module
	{
		// Token: 0x060003A1 RID: 929 RVA: 0x0000AF72 File Offset: 0x00009172
		protected NonPEModule(Universe universe) : base(universe)
		{
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000AF7B File Offset: 0x0000917B
		protected virtual Exception InvalidOperationException()
		{
			return new InvalidOperationException();
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000AF82 File Offset: 0x00009182
		protected virtual Exception NotSupportedException()
		{
			return new NotSupportedException();
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000AF89 File Offset: 0x00009189
		protected virtual Exception ArgumentOutOfRangeException()
		{
			return new ArgumentOutOfRangeException();
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000AF90 File Offset: 0x00009190
		internal sealed override Type GetModuleType()
		{
			throw this.InvalidOperationException();
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000AF90 File Offset: 0x00009190
		internal sealed override ByteReader GetBlob(int blobIndex)
		{
			throw this.InvalidOperationException();
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000AF98 File Offset: 0x00009198
		public sealed override AssemblyName[] __GetReferencedAssemblies()
		{
			throw this.NotSupportedException();
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000AF98 File Offset: 0x00009198
		public sealed override string[] __GetReferencedModules()
		{
			throw this.NotSupportedException();
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000AF98 File Offset: 0x00009198
		public override Type[] __GetReferencedTypes()
		{
			throw this.NotSupportedException();
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000AF98 File Offset: 0x00009198
		public override Type[] __GetExportedTypes()
		{
			throw this.NotSupportedException();
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000AF98 File Offset: 0x00009198
		protected sealed override long GetImageBaseImpl()
		{
			throw this.NotSupportedException();
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000AF98 File Offset: 0x00009198
		protected sealed override long GetStackReserveImpl()
		{
			throw this.NotSupportedException();
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000AF98 File Offset: 0x00009198
		protected sealed override int GetFileAlignmentImpl()
		{
			throw this.NotSupportedException();
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000AF98 File Offset: 0x00009198
		protected override DllCharacteristics GetDllCharacteristicsImpl()
		{
			throw this.NotSupportedException();
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000AFA0 File Offset: 0x000091A0
		internal sealed override Type ResolveType(int metadataToken, IGenericContext context)
		{
			throw this.ArgumentOutOfRangeException();
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000AFA0 File Offset: 0x000091A0
		public sealed override MethodBase ResolveMethod(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			throw this.ArgumentOutOfRangeException();
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000AFA0 File Offset: 0x000091A0
		public sealed override FieldInfo ResolveField(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			throw this.ArgumentOutOfRangeException();
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000AFA0 File Offset: 0x000091A0
		public sealed override MemberInfo ResolveMember(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			throw this.ArgumentOutOfRangeException();
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000AFA0 File Offset: 0x000091A0
		public sealed override string ResolveString(int metadataToken)
		{
			throw this.ArgumentOutOfRangeException();
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000AFA0 File Offset: 0x000091A0
		public sealed override Type[] __ResolveOptionalParameterTypes(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments, out CustomModifiers[] customModifiers)
		{
			throw this.ArgumentOutOfRangeException();
		}
	}
}
