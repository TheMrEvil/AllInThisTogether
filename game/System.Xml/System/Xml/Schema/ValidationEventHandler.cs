using System;

namespace System.Xml.Schema
{
	/// <summary>Represents the callback method that will handle XML schema validation events and the <see cref="T:System.Xml.Schema.ValidationEventArgs" />.</summary>
	/// <param name="sender">The source of the event. 
	///       Note    Determine the type of a sender before using it in your code. You cannot assume that the sender is an instance of a particular type. The sender is also not guaranteed to not  be null. Always surround your casts with failure handling logic.</param>
	/// <param name="e">The event data. </param>
	// Token: 0x0200057F RID: 1407
	// (Invoke) Token: 0x060038AD RID: 14509
	public delegate void ValidationEventHandler(object sender, ValidationEventArgs e);
}
