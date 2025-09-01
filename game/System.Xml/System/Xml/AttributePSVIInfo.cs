using System;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x0200018B RID: 395
	internal class AttributePSVIInfo
	{
		// Token: 0x06000E40 RID: 3648 RVA: 0x0005C8AA File Offset: 0x0005AAAA
		internal AttributePSVIInfo()
		{
			this.attributeSchemaInfo = new XmlSchemaInfo();
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x0005C8BD File Offset: 0x0005AABD
		internal void Reset()
		{
			this.typedAttributeValue = null;
			this.localName = string.Empty;
			this.namespaceUri = string.Empty;
			this.attributeSchemaInfo.Clear();
		}

		// Token: 0x04000F32 RID: 3890
		internal string localName;

		// Token: 0x04000F33 RID: 3891
		internal string namespaceUri;

		// Token: 0x04000F34 RID: 3892
		internal object typedAttributeValue;

		// Token: 0x04000F35 RID: 3893
		internal XmlSchemaInfo attributeSchemaInfo;
	}
}
