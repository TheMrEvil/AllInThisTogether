using System;

namespace System.Xml
{
	/// <summary>Specifies the state of the <see cref="T:System.Xml.XmlWriter" />.</summary>
	// Token: 0x02000178 RID: 376
	public enum WriteState
	{
		/// <summary>Indicates that a Write method has not yet been called.</summary>
		// Token: 0x04000EA5 RID: 3749
		Start,
		/// <summary>Indicates that the prolog is being written.</summary>
		// Token: 0x04000EA6 RID: 3750
		Prolog,
		/// <summary>Indicates that an element start tag is being written.</summary>
		// Token: 0x04000EA7 RID: 3751
		Element,
		/// <summary>Indicates that an attribute value is being written.</summary>
		// Token: 0x04000EA8 RID: 3752
		Attribute,
		/// <summary>Indicates that element content is being written.</summary>
		// Token: 0x04000EA9 RID: 3753
		Content,
		/// <summary>Indicates that the <see cref="M:System.Xml.XmlWriter.Close" /> method has been called.</summary>
		// Token: 0x04000EAA RID: 3754
		Closed,
		/// <summary>An exception has been thrown, which has left the <see cref="T:System.Xml.XmlWriter" /> in an invalid state. You can call the <see cref="M:System.Xml.XmlWriter.Close" /> method to put the <see cref="T:System.Xml.XmlWriter" /> in the <see cref="F:System.Xml.WriteState.Closed" /> state. Any other <see cref="T:System.Xml.XmlWriter" /> method calls results in an <see cref="T:System.InvalidOperationException" />.</summary>
		// Token: 0x04000EAB RID: 3755
		Error
	}
}
