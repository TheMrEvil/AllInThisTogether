using System;

namespace System.Xml.Linq
{
	/// <summary>Specifies the event type when an event is raised for an <see cref="T:System.Xml.Linq.XObject" />.</summary>
	// Token: 0x02000049 RID: 73
	public enum XObjectChange
	{
		/// <summary>An <see cref="T:System.Xml.Linq.XObject" /> has been or will be added to an <see cref="T:System.Xml.Linq.XContainer" />.</summary>
		// Token: 0x04000172 RID: 370
		Add,
		/// <summary>An <see cref="T:System.Xml.Linq.XObject" /> has been or will be removed from an <see cref="T:System.Xml.Linq.XContainer" />.</summary>
		// Token: 0x04000173 RID: 371
		Remove,
		/// <summary>An <see cref="T:System.Xml.Linq.XObject" /> has been or will be renamed.</summary>
		// Token: 0x04000174 RID: 372
		Name,
		/// <summary>The value of an <see cref="T:System.Xml.Linq.XObject" /> has been or will be changed. In addition, a change in the serialization of an empty element (either from an empty tag to start/end tag pair or vice versa) raises this event.</summary>
		// Token: 0x04000175 RID: 373
		Value
	}
}
