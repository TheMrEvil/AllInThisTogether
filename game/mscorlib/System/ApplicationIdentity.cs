using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>Provides the ability to uniquely identify a manifest-activated application. This class cannot be inherited.</summary>
	// Token: 0x02000228 RID: 552
	[ComVisible(false)]
	[Serializable]
	public sealed class ApplicationIdentity : ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ApplicationIdentity" /> class.</summary>
		/// <param name="applicationIdentityFullName">The full name of the application.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="applicationIdentityFullName" /> is <see langword="null" />.</exception>
		// Token: 0x060018C7 RID: 6343 RVA: 0x0005DFFB File Offset: 0x0005C1FB
		public ApplicationIdentity(string applicationIdentityFullName)
		{
			if (applicationIdentityFullName == null)
			{
				throw new ArgumentNullException("applicationIdentityFullName");
			}
			if (applicationIdentityFullName.IndexOf(", Culture=") == -1)
			{
				this._fullName = applicationIdentityFullName + ", Culture=neutral";
				return;
			}
			this._fullName = applicationIdentityFullName;
		}

		/// <summary>Gets the location of the deployment manifest as a URL.</summary>
		/// <returns>The URL of the deployment manifest.</returns>
		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x060018C8 RID: 6344 RVA: 0x0005E038 File Offset: 0x0005C238
		public string CodeBase
		{
			get
			{
				return this._codeBase;
			}
		}

		/// <summary>Gets the full name of the application.</summary>
		/// <returns>The full name of the application, also known as the display name.</returns>
		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x060018C9 RID: 6345 RVA: 0x0005E040 File Offset: 0x0005C240
		public string FullName
		{
			get
			{
				return this._fullName;
			}
		}

		/// <summary>Returns the full name of the manifest-activated application.</summary>
		/// <returns>The full name of the manifest-activated application.</returns>
		// Token: 0x060018CA RID: 6346 RVA: 0x0005E040 File Offset: 0x0005C240
		public override string ToString()
		{
			return this._fullName;
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data needed to serialize the target object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" />) structure for the serialization.</param>
		// Token: 0x060018CB RID: 6347 RVA: 0x0005D900 File Offset: 0x0005BB00
		[MonoTODO("Missing serialization")]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
		}

		// Token: 0x040016D5 RID: 5845
		private string _fullName;

		// Token: 0x040016D6 RID: 5846
		private string _codeBase;
	}
}
