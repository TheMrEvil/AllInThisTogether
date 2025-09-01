using System;
using System.Reflection;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x0200043C RID: 1084
	internal sealed class EarlyBoundInfo
	{
		// Token: 0x06002AEC RID: 10988 RVA: 0x00102EA1 File Offset: 0x001010A1
		public EarlyBoundInfo(string namespaceUri, Type ebType)
		{
			this.namespaceUri = namespaceUri;
			this.constrInfo = ebType.GetConstructor(Type.EmptyTypes);
		}

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x06002AED RID: 10989 RVA: 0x00102EC1 File Offset: 0x001010C1
		public string NamespaceUri
		{
			get
			{
				return this.namespaceUri;
			}
		}

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x06002AEE RID: 10990 RVA: 0x00102EC9 File Offset: 0x001010C9
		public Type EarlyBoundType
		{
			get
			{
				return this.constrInfo.DeclaringType;
			}
		}

		// Token: 0x06002AEF RID: 10991 RVA: 0x00102ED6 File Offset: 0x001010D6
		public object CreateObject()
		{
			return this.constrInfo.Invoke(new object[0]);
		}

		// Token: 0x06002AF0 RID: 10992 RVA: 0x00102EEC File Offset: 0x001010EC
		public override bool Equals(object obj)
		{
			EarlyBoundInfo earlyBoundInfo = obj as EarlyBoundInfo;
			return earlyBoundInfo != null && this.namespaceUri == earlyBoundInfo.namespaceUri && this.constrInfo == earlyBoundInfo.constrInfo;
		}

		// Token: 0x06002AF1 RID: 10993 RVA: 0x00102F2B File Offset: 0x0010112B
		public override int GetHashCode()
		{
			return this.namespaceUri.GetHashCode();
		}

		// Token: 0x040021D5 RID: 8661
		private string namespaceUri;

		// Token: 0x040021D6 RID: 8662
		private ConstructorInfo constrInfo;
	}
}
