using System;
using System.Diagnostics;
using System.Reflection;
using System.Security;

namespace System.Xml.Xsl.IlGen
{
	// Token: 0x02000497 RID: 1175
	internal static class XmlILConstructors
	{
		// Token: 0x06002DD9 RID: 11737 RVA: 0x0010C4B1 File Offset: 0x0010A6B1
		private static ConstructorInfo GetConstructor(Type className)
		{
			return className.GetConstructor(new Type[0]);
		}

		// Token: 0x06002DDA RID: 11738 RVA: 0x0010C4BF File Offset: 0x0010A6BF
		private static ConstructorInfo GetConstructor(Type className, params Type[] args)
		{
			return className.GetConstructor(args);
		}

		// Token: 0x06002DDB RID: 11739 RVA: 0x0010C4C8 File Offset: 0x0010A6C8
		// Note: this type is marked as 'beforefieldinit'.
		static XmlILConstructors()
		{
		}

		// Token: 0x040023A5 RID: 9125
		public static readonly ConstructorInfo DecFromParts = XmlILConstructors.GetConstructor(typeof(decimal), new Type[]
		{
			typeof(int),
			typeof(int),
			typeof(int),
			typeof(bool),
			typeof(byte)
		});

		// Token: 0x040023A6 RID: 9126
		public static readonly ConstructorInfo DecFromInt32 = XmlILConstructors.GetConstructor(typeof(decimal), new Type[]
		{
			typeof(int)
		});

		// Token: 0x040023A7 RID: 9127
		public static readonly ConstructorInfo DecFromInt64 = XmlILConstructors.GetConstructor(typeof(decimal), new Type[]
		{
			typeof(long)
		});

		// Token: 0x040023A8 RID: 9128
		public static readonly ConstructorInfo Debuggable = XmlILConstructors.GetConstructor(typeof(DebuggableAttribute), new Type[]
		{
			typeof(DebuggableAttribute.DebuggingModes)
		});

		// Token: 0x040023A9 RID: 9129
		public static readonly ConstructorInfo NonUserCode = XmlILConstructors.GetConstructor(typeof(DebuggerNonUserCodeAttribute));

		// Token: 0x040023AA RID: 9130
		public static readonly ConstructorInfo QName = XmlILConstructors.GetConstructor(typeof(XmlQualifiedName), new Type[]
		{
			typeof(string),
			typeof(string)
		});

		// Token: 0x040023AB RID: 9131
		public static readonly ConstructorInfo StepThrough = XmlILConstructors.GetConstructor(typeof(DebuggerStepThroughAttribute));

		// Token: 0x040023AC RID: 9132
		public static readonly ConstructorInfo Transparent = XmlILConstructors.GetConstructor(typeof(SecurityTransparentAttribute));
	}
}
