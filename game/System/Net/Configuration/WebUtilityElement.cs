using System;
using System.Configuration;
using Unity;

namespace System.Net.Configuration
{
	/// <summary>Represents the WebUtility element in the configuration file.</summary>
	// Token: 0x02000877 RID: 2167
	public sealed class WebUtilityElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.WebUtilityElement" /> class.</summary>
		// Token: 0x060044B2 RID: 17586 RVA: 0x00013BCA File Offset: 0x00011DCA
		public WebUtilityElement()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the default Unicode decoding conformance behavior used for an <see cref="T:System.Net.WebUtility" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Net.Configuration.UnicodeDecodingConformance" />.  
		///  The default Unicode decoding behavior.</returns>
		// Token: 0x17000F85 RID: 3973
		// (get) Token: 0x060044B3 RID: 17587 RVA: 0x000ED180 File Offset: 0x000EB380
		// (set) Token: 0x060044B4 RID: 17588 RVA: 0x00013BCA File Offset: 0x00011DCA
		public UnicodeDecodingConformance UnicodeDecodingConformance
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return UnicodeDecodingConformance.Auto;
			}
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets the default Unicode encoding conformance behavior used for an <see cref="T:System.Net.WebUtility" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Net.Configuration.UnicodeEncodingConformance" />.  
		///  The default Unicode encoding behavior.</returns>
		// Token: 0x17000F86 RID: 3974
		// (get) Token: 0x060044B5 RID: 17589 RVA: 0x000ED19C File Offset: 0x000EB39C
		// (set) Token: 0x060044B6 RID: 17590 RVA: 0x00013BCA File Offset: 0x00011DCA
		public UnicodeEncodingConformance UnicodeEncodingConformance
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return UnicodeEncodingConformance.Auto;
			}
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}
	}
}
