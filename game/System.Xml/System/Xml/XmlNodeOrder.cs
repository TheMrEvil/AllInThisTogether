using System;

namespace System.Xml
{
	/// <summary>Describes the document order of a node compared to a second node.</summary>
	// Token: 0x02000240 RID: 576
	public enum XmlNodeOrder
	{
		/// <summary>The current node of this navigator is before the current node of the supplied navigator.</summary>
		// Token: 0x040012EE RID: 4846
		Before,
		/// <summary>The current node of this navigator is after the current node of the supplied navigator.</summary>
		// Token: 0x040012EF RID: 4847
		After,
		/// <summary>The two navigators are positioned on the same node.</summary>
		// Token: 0x040012F0 RID: 4848
		Same,
		/// <summary>The node positions cannot be determined in document order, relative to each other. This could occur if the two nodes reside in different trees.</summary>
		// Token: 0x040012F1 RID: 4849
		Unknown
	}
}
