using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Specifies the name of the COM+ application to be used for the install of the components in the assembly. This class cannot be inherited.</summary>
	// Token: 0x0200000F RID: 15
	[AttributeUsage(AttributeTargets.Assembly)]
	[ComVisible(false)]
	public sealed class ApplicationNameAttribute : Attribute, IConfigurationAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ApplicationNameAttribute" /> class, specifying the name of the COM+ application to be used for the install of the components.</summary>
		/// <param name="name">The name of the COM+ application.</param>
		// Token: 0x06000029 RID: 41 RVA: 0x00002153 File Offset: 0x00000353
		public ApplicationNameAttribute(string name)
		{
			this.name = name;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000020AA File Offset: 0x000002AA
		bool IConfigurationAttribute.AfterSaveChanges(Hashtable info)
		{
			return false;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		bool IConfigurationAttribute.Apply(Hashtable cache)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000020AD File Offset: 0x000002AD
		bool IConfigurationAttribute.IsValidTarget(string s)
		{
			return s == "Application";
		}

		/// <summary>Gets a value indicating the name of the COM+ application that contains the components in the assembly.</summary>
		/// <returns>The name of the COM+ application.</returns>
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002162 File Offset: 0x00000362
		public string Value
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04000039 RID: 57
		private string name;
	}
}
