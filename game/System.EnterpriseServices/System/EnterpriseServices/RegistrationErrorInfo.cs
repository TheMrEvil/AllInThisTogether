using System;
using Unity;

namespace System.EnterpriseServices
{
	/// <summary>Retrieves extended error information about methods related to multiple COM+ objects. This also includes methods that install, import, and export COM+ applications and components. This class cannot be inherited.</summary>
	// Token: 0x0200003B RID: 59
	[Serializable]
	public sealed class RegistrationErrorInfo
	{
		// Token: 0x060000BE RID: 190 RVA: 0x00002480 File Offset: 0x00000680
		[MonoTODO]
		internal RegistrationErrorInfo(string name, string majorRef, string minorRef, int errorCode)
		{
			this.name = name;
			this.majorRef = majorRef;
			this.minorRef = minorRef;
			this.errorCode = errorCode;
		}

		/// <summary>Gets the error code for the object or file.</summary>
		/// <returns>The error code for the object or file.</returns>
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000BF RID: 191 RVA: 0x000024A5 File Offset: 0x000006A5
		public int ErrorCode
		{
			get
			{
				return this.errorCode;
			}
		}

		/// <summary>Gets the description of the <see cref="P:System.EnterpriseServices.RegistrationErrorInfo.ErrorCode" />.</summary>
		/// <returns>The description of the <see cref="P:System.EnterpriseServices.RegistrationErrorInfo.ErrorCode" />.</returns>
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x000024AD File Offset: 0x000006AD
		public string ErrorString
		{
			get
			{
				return this.errorString;
			}
		}

		/// <summary>Gets the key value for the object that caused the error, if applicable.</summary>
		/// <returns>The key value for the object that caused the error, if applicable.</returns>
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x000024B5 File Offset: 0x000006B5
		public string MajorRef
		{
			get
			{
				return this.majorRef;
			}
		}

		/// <summary>Gets a precise specification of the item that caused the error, such as a property name.</summary>
		/// <returns>A precise specification of the item, such as a property name, that caused the error. If multiple errors occurred, or this does not apply, <see cref="P:System.EnterpriseServices.RegistrationErrorInfo.MinorRef" /> returns the string "&lt;Invalid&gt;".</returns>
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x000024BD File Offset: 0x000006BD
		public string MinorRef
		{
			get
			{
				return this.minorRef;
			}
		}

		/// <summary>Gets the name of the object or file that caused the error.</summary>
		/// <returns>The name of the object or file that caused the error.</returns>
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x000024C5 File Offset: 0x000006C5
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000024CD File Offset: 0x000006CD
		internal RegistrationErrorInfo()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400007D RID: 125
		private int errorCode;

		// Token: 0x0400007E RID: 126
		private string errorString;

		// Token: 0x0400007F RID: 127
		private string majorRef;

		// Token: 0x04000080 RID: 128
		private string minorRef;

		// Token: 0x04000081 RID: 129
		private string name;
	}
}
