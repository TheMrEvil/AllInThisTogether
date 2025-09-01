using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Specifies the application ID (as a GUID) for this assembly. This class cannot be inherited.</summary>
	// Token: 0x0200000E RID: 14
	[AttributeUsage(AttributeTargets.Assembly)]
	[ComVisible(false)]
	public sealed class ApplicationIDAttribute : Attribute, IConfigurationAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ApplicationIDAttribute" /> class specifying the GUID representing the application ID for the COM+ application.</summary>
		/// <param name="guid">The GUID associated with the COM+ application.</param>
		// Token: 0x06000024 RID: 36 RVA: 0x00002137 File Offset: 0x00000337
		public ApplicationIDAttribute(string guid)
		{
			this.guid = new Guid(guid);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000020AA File Offset: 0x000002AA
		bool IConfigurationAttribute.AfterSaveChanges(Hashtable info)
		{
			return false;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000020AA File Offset: 0x000002AA
		bool IConfigurationAttribute.Apply(Hashtable cache)
		{
			return false;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000020AD File Offset: 0x000002AD
		bool IConfigurationAttribute.IsValidTarget(string s)
		{
			return s == "Application";
		}

		/// <summary>Gets the GUID of the COM+ application.</summary>
		/// <returns>The GUID representing the COM+ application.</returns>
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000028 RID: 40 RVA: 0x0000214B File Offset: 0x0000034B
		public Guid Value
		{
			get
			{
				return this.guid;
			}
		}

		// Token: 0x04000038 RID: 56
		private Guid guid;
	}
}
