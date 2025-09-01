using System;
using System.Configuration;
using Unity;

namespace System.Net.Configuration
{
	/// <summary>Represents the Windows authentication element in a configuration file. This class cannot be inherited.</summary>
	// Token: 0x02000878 RID: 2168
	public sealed class WindowsAuthenticationElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.WindowsAuthenticationElement" /> class.</summary>
		// Token: 0x060044B7 RID: 17591 RVA: 0x00013BCA File Offset: 0x00011DCA
		public WindowsAuthenticationElement()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Defines the default size of the Windows credential handle cache.</summary>
		/// <returns>The default size of the Windows credential handle cache.</returns>
		// Token: 0x17000F87 RID: 3975
		// (get) Token: 0x060044B8 RID: 17592 RVA: 0x000ED1B8 File Offset: 0x000EB3B8
		// (set) Token: 0x060044B9 RID: 17593 RVA: 0x00013BCA File Offset: 0x00011DCA
		public int DefaultCredentialsHandleCacheSize
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0;
			}
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}
	}
}
