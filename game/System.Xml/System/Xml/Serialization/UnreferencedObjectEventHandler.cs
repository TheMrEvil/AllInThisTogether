using System;

namespace System.Xml.Serialization
{
	/// <summary>Represents the method that handles the <see cref="E:System.Xml.Serialization.XmlSerializer.UnreferencedObject" /> event of an <see cref="T:System.Xml.Serialization.XmlSerializer" />.</summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">An <see cref="T:System.Xml.Serialization.UnreferencedObjectEventArgs" /> that contains the event data. </param>
	// Token: 0x02000312 RID: 786
	// (Invoke) Token: 0x060020A6 RID: 8358
	public delegate void UnreferencedObjectEventHandler(object sender, UnreferencedObjectEventArgs e);
}
