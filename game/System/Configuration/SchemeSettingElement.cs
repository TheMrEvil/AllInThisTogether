using System;
using Unity;

namespace System.Configuration
{
	/// <summary>Represents an element in a <see cref="T:System.Configuration.SchemeSettingElementCollection" /> class.</summary>
	// Token: 0x0200087A RID: 2170
	public sealed class SchemeSettingElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SchemeSettingElement" /> class.</summary>
		// Token: 0x060044BF RID: 17599 RVA: 0x00013BCA File Offset: 0x00011DCA
		public SchemeSettingElement()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the value of the GenericUriParserOptions entry from a <see cref="T:System.Configuration.SchemeSettingElement" /> instance.</summary>
		/// <returns>The value of GenericUriParserOptions entry.</returns>
		// Token: 0x17000F89 RID: 3977
		// (get) Token: 0x060044C0 RID: 17600 RVA: 0x000ED1F0 File Offset: 0x000EB3F0
		public GenericUriParserOptions GenericUriParserOptions
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return GenericUriParserOptions.Default;
			}
		}

		/// <summary>Gets the value of the Name entry from a <see cref="T:System.Configuration.SchemeSettingElement" /> instance.</summary>
		/// <returns>The protocol used by this schema setting.</returns>
		// Token: 0x17000F8A RID: 3978
		// (get) Token: 0x060044C1 RID: 17601 RVA: 0x00032884 File Offset: 0x00030A84
		public string Name
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
