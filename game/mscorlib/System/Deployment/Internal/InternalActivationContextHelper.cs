using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal
{
	/// <summary>Provides access to data from an <see cref="T:System.ActivationContext" /> object.</summary>
	// Token: 0x0200027D RID: 637
	[ComVisible(false)]
	public static class InternalActivationContextHelper
	{
		/// <summary>Gets the contents of the application manifest from an <see cref="T:System.ActivationContext" /> object.</summary>
		/// <param name="appInfo">The object containing the manifest.</param>
		/// <returns>The application manifest that is contained by the <see cref="T:System.ActivationContext" /> object.</returns>
		// Token: 0x06001D48 RID: 7496 RVA: 0x000479F8 File Offset: 0x00045BF8
		[MonoTODO]
		public static object GetActivationContextData(ActivationContext appInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the manifest of the last deployment component in an <see cref="T:System.ActivationContext" /> object.</summary>
		/// <param name="appInfo">The object containing the manifest.</param>
		/// <returns>The manifest of the last deployment component in the <see cref="T:System.ActivationContext" /> object.</returns>
		// Token: 0x06001D49 RID: 7497 RVA: 0x000479F8 File Offset: 0x00045BF8
		[MonoTODO]
		public static object GetApplicationComponentManifest(ActivationContext appInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a byte array containing the raw content of the application manifest.</summary>
		/// <param name="appInfo">The object to get bytes from.</param>
		/// <returns>An array containing the application manifest as raw data.</returns>
		// Token: 0x06001D4A RID: 7498 RVA: 0x000479F8 File Offset: 0x00045BF8
		[MonoTODO("2.0 SP1 member")]
		public static byte[] GetApplicationManifestBytes(ActivationContext appInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the manifest of the first deployment component in an <see cref="T:System.ActivationContext" /> object.</summary>
		/// <param name="appInfo">The object containing the manifest.</param>
		/// <returns>The manifest of the first deployment component in the <see cref="T:System.ActivationContext" /> object.</returns>
		// Token: 0x06001D4B RID: 7499 RVA: 0x000479F8 File Offset: 0x00045BF8
		[MonoTODO]
		public static object GetDeploymentComponentManifest(ActivationContext appInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a byte array containing the raw content of the deployment manifest.</summary>
		/// <param name="appInfo">The object to get bytes from.</param>
		/// <returns>An array containing the deployment manifest as raw data.</returns>
		// Token: 0x06001D4C RID: 7500 RVA: 0x000479F8 File Offset: 0x00045BF8
		[MonoTODO("2.0 SP1 member")]
		public static byte[] GetDeploymentManifestBytes(ActivationContext appInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a value indicating whether this is the first time this <see cref="T:System.ActivationContext" /> object has been run.</summary>
		/// <param name="appInfo">The object to examine.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.ActivationContext" /> indicates it is running for the first time; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001D4D RID: 7501 RVA: 0x000479F8 File Offset: 0x00045BF8
		[MonoTODO]
		public static bool IsFirstRun(ActivationContext appInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Informs an <see cref="T:System.ActivationContext" /> to get ready to be run.</summary>
		/// <param name="appInfo">The object to inform.</param>
		// Token: 0x06001D4E RID: 7502 RVA: 0x000479F8 File Offset: 0x00045BF8
		[MonoTODO]
		public static void PrepareForExecution(ActivationContext appInfo)
		{
			throw new NotImplementedException();
		}
	}
}
