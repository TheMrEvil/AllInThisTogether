using System;

namespace System.Xml.Schema
{
	// Token: 0x020004EA RID: 1258
	internal class ChameleonKey
	{
		// Token: 0x060033C3 RID: 13251 RVA: 0x001266EB File Offset: 0x001248EB
		public ChameleonKey(string ns, XmlSchema originalSchema)
		{
			this.targetNS = ns;
			this.chameleonLocation = originalSchema.BaseUri;
			if (this.chameleonLocation.OriginalString.Length == 0)
			{
				this.originalSchema = originalSchema;
			}
		}

		// Token: 0x060033C4 RID: 13252 RVA: 0x00126720 File Offset: 0x00124920
		public override int GetHashCode()
		{
			if (this.hashCode == 0)
			{
				this.hashCode = this.targetNS.GetHashCode() + this.chameleonLocation.GetHashCode() + ((this.originalSchema == null) ? 0 : this.originalSchema.GetHashCode());
			}
			return this.hashCode;
		}

		// Token: 0x060033C5 RID: 13253 RVA: 0x00126770 File Offset: 0x00124970
		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			ChameleonKey chameleonKey = obj as ChameleonKey;
			return chameleonKey != null && (this.targetNS.Equals(chameleonKey.targetNS) && this.chameleonLocation.Equals(chameleonKey.chameleonLocation)) && this.originalSchema == chameleonKey.originalSchema;
		}

		// Token: 0x0400269F RID: 9887
		internal string targetNS;

		// Token: 0x040026A0 RID: 9888
		internal Uri chameleonLocation;

		// Token: 0x040026A1 RID: 9889
		internal XmlSchema originalSchema;

		// Token: 0x040026A2 RID: 9890
		private int hashCode;
	}
}
