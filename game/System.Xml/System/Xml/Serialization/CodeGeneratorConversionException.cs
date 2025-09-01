using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000270 RID: 624
	internal class CodeGeneratorConversionException : Exception
	{
		// Token: 0x060017BA RID: 6074 RVA: 0x0008B49E File Offset: 0x0008969E
		public CodeGeneratorConversionException(Type sourceType, Type targetType, bool isAddress, string reason)
		{
			this.sourceType = sourceType;
			this.targetType = targetType;
			this.isAddress = isAddress;
			this.reason = reason;
		}

		// Token: 0x0400187F RID: 6271
		private Type sourceType;

		// Token: 0x04001880 RID: 6272
		private Type targetType;

		// Token: 0x04001881 RID: 6273
		private bool isAddress;

		// Token: 0x04001882 RID: 6274
		private string reason;
	}
}
