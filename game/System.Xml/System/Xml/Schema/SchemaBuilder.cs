using System;

namespace System.Xml.Schema
{
	// Token: 0x0200056D RID: 1389
	internal abstract class SchemaBuilder
	{
		// Token: 0x0600374A RID: 14154
		internal abstract bool ProcessElement(string prefix, string name, string ns);

		// Token: 0x0600374B RID: 14155
		internal abstract void ProcessAttribute(string prefix, string name, string ns, string value);

		// Token: 0x0600374C RID: 14156
		internal abstract bool IsContentParsed();

		// Token: 0x0600374D RID: 14157
		internal abstract void ProcessMarkup(XmlNode[] markup);

		// Token: 0x0600374E RID: 14158
		internal abstract void ProcessCData(string value);

		// Token: 0x0600374F RID: 14159
		internal abstract void StartChildren();

		// Token: 0x06003750 RID: 14160
		internal abstract void EndChildren();

		// Token: 0x06003751 RID: 14161 RVA: 0x0000216B File Offset: 0x0000036B
		protected SchemaBuilder()
		{
		}
	}
}
