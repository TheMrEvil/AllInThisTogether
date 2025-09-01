using System;

namespace System.Xml
{
	/// <summary>Specifies how the <see cref="T:System.Xml.XmlTextReader" /> or <see cref="T:System.Xml.XmlValidatingReader" /> handle entities.</summary>
	// Token: 0x02000030 RID: 48
	public enum EntityHandling
	{
		/// <summary>Expands all entities and returns the expanded nodes.</summary>
		// Token: 0x040005E8 RID: 1512
		ExpandEntities = 1,
		/// <summary>Expands character entities and returns general entities as <see cref="F:System.Xml.XmlNodeType.EntityReference" /> nodes. </summary>
		// Token: 0x040005E9 RID: 1513
		ExpandCharEntities
	}
}
