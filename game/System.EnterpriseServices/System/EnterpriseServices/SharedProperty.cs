using System;
using System.Runtime.InteropServices;
using Unity;

namespace System.EnterpriseServices
{
	/// <summary>Accesses a shared property. This class cannot be inherited.</summary>
	// Token: 0x0200004A RID: 74
	[ComVisible(false)]
	public sealed class SharedProperty
	{
		// Token: 0x0600013D RID: 317 RVA: 0x000025BD File Offset: 0x000007BD
		internal SharedProperty(ISharedProperty property)
		{
			this.property = property;
		}

		/// <summary>Gets or sets the value of the shared property.</summary>
		/// <returns>The value of the shared property.</returns>
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600013E RID: 318 RVA: 0x000025CC File Offset: 0x000007CC
		// (set) Token: 0x0600013F RID: 319 RVA: 0x000025D9 File Offset: 0x000007D9
		public object Value
		{
			get
			{
				return this.property.Value;
			}
			set
			{
				this.property.Value = value;
			}
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000024CD File Offset: 0x000006CD
		internal SharedProperty()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000086 RID: 134
		private ISharedProperty property;
	}
}
