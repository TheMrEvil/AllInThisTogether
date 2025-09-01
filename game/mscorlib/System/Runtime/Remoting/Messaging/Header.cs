using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Defines the out-of-band data for a call.</summary>
	// Token: 0x02000618 RID: 1560
	[ComVisible(true)]
	[Serializable]
	public class Header
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Messaging.Header" /> class with the given name and value.</summary>
		/// <param name="_Name">The name of the <see cref="T:System.Runtime.Remoting.Messaging.Header" />.</param>
		/// <param name="_Value">The object that contains the value for the <see cref="T:System.Runtime.Remoting.Messaging.Header" />.</param>
		// Token: 0x06003AEC RID: 15084 RVA: 0x000CE30D File Offset: 0x000CC50D
		public Header(string _Name, object _Value) : this(_Name, _Value, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Messaging.Header" /> class with the given name, value, and additional configuration information.</summary>
		/// <param name="_Name">The name of the <see cref="T:System.Runtime.Remoting.Messaging.Header" />.</param>
		/// <param name="_Value">The object that contains the value for the <see cref="T:System.Runtime.Remoting.Messaging.Header" />.</param>
		/// <param name="_MustUnderstand">Indicates whether the receiving end must understand the out-of-band data.</param>
		// Token: 0x06003AED RID: 15085 RVA: 0x000CE318 File Offset: 0x000CC518
		public Header(string _Name, object _Value, bool _MustUnderstand) : this(_Name, _Value, _MustUnderstand, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Messaging.Header" /> class.</summary>
		/// <param name="_Name">The name of the <see cref="T:System.Runtime.Remoting.Messaging.Header" />.</param>
		/// <param name="_Value">The object that contains the value of the <see cref="T:System.Runtime.Remoting.Messaging.Header" />.</param>
		/// <param name="_MustUnderstand">Indicates whether the receiving end must understand out-of-band data.</param>
		/// <param name="_HeaderNamespace">The <see cref="T:System.Runtime.Remoting.Messaging.Header" /> XML namespace.</param>
		// Token: 0x06003AEE RID: 15086 RVA: 0x000CE324 File Offset: 0x000CC524
		public Header(string _Name, object _Value, bool _MustUnderstand, string _HeaderNamespace)
		{
			this.Name = _Name;
			this.Value = _Value;
			this.MustUnderstand = _MustUnderstand;
			this.HeaderNamespace = _HeaderNamespace;
		}

		/// <summary>Indicates the XML namespace that the current <see cref="T:System.Runtime.Remoting.Messaging.Header" /> belongs to.</summary>
		// Token: 0x0400268E RID: 9870
		public string HeaderNamespace;

		/// <summary>Indicates whether the receiving end must understand the out-of-band data.</summary>
		// Token: 0x0400268F RID: 9871
		public bool MustUnderstand;

		/// <summary>Contains the name of the <see cref="T:System.Runtime.Remoting.Messaging.Header" />.</summary>
		// Token: 0x04002690 RID: 9872
		public string Name;

		/// <summary>Contains the value for the <see cref="T:System.Runtime.Remoting.Messaging.Header" />.</summary>
		// Token: 0x04002691 RID: 9873
		public object Value;
	}
}
