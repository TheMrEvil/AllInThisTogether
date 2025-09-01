using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Unity;

namespace System
{
	/// <summary>Identifies the activation context for the current application. This class cannot be inherited.</summary>
	// Token: 0x02000223 RID: 547
	[ComVisible(false)]
	[Serializable]
	public sealed class ActivationContext : IDisposable, ISerializable
	{
		// Token: 0x06001871 RID: 6257 RVA: 0x0005D854 File Offset: 0x0005BA54
		private ActivationContext(ApplicationIdentity identity)
		{
			this._appid = identity;
		}

		/// <summary>Enables an <see cref="T:System.ActivationContext" /> object to attempt to free resources and perform other cleanup operations before the <see cref="T:System.ActivationContext" /> is reclaimed by garbage collection.</summary>
		// Token: 0x06001872 RID: 6258 RVA: 0x0005D864 File Offset: 0x0005BA64
		~ActivationContext()
		{
			this.Dispose(false);
		}

		/// <summary>Gets the form, or store context, for the current application.</summary>
		/// <returns>One of the enumeration values.</returns>
		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06001873 RID: 6259 RVA: 0x0005D894 File Offset: 0x0005BA94
		public ActivationContext.ContextForm Form
		{
			get
			{
				return this._form;
			}
		}

		/// <summary>Gets the application identity for the current application.</summary>
		/// <returns>An <see cref="T:System.ApplicationIdentity" /> object that identifies the current application.</returns>
		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06001874 RID: 6260 RVA: 0x0005D89C File Offset: 0x0005BA9C
		public ApplicationIdentity Identity
		{
			get
			{
				return this._appid;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ActivationContext" /> class using the specified application identity.</summary>
		/// <param name="identity">An object that identifies an application.</param>
		/// <returns>An object with the specified application identity.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identity" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">No deployment or application identity is specified in <paramref name="identity" />.</exception>
		// Token: 0x06001875 RID: 6261 RVA: 0x0005D8A4 File Offset: 0x0005BAA4
		[MonoTODO("Missing validation")]
		public static ActivationContext CreatePartialActivationContext(ApplicationIdentity identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			return new ActivationContext(identity);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ActivationContext" /> class using the specified application identity and array of manifest paths.</summary>
		/// <param name="identity">An object that identifies an application.</param>
		/// <param name="manifestPaths">A string array of manifest paths for the application.</param>
		/// <returns>An object with the specified application identity and array of manifest paths.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identity" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="manifestPaths" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">No deployment or application identity is specified in <paramref name="identity" />.  
		///  -or-  
		///  <paramref name="identity" /> does not match the identity in the manifests.  
		///  -or-  
		///  <paramref name="identity" /> does not have the same number of components as the manifest paths.</exception>
		// Token: 0x06001876 RID: 6262 RVA: 0x0005D8BA File Offset: 0x0005BABA
		[MonoTODO("Missing validation")]
		public static ActivationContext CreatePartialActivationContext(ApplicationIdentity identity, string[] manifestPaths)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			if (manifestPaths == null)
			{
				throw new ArgumentNullException("manifestPaths");
			}
			return new ActivationContext(identity);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.ActivationContext" />.</summary>
		// Token: 0x06001877 RID: 6263 RVA: 0x0005D8DE File Offset: 0x0005BADE
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001878 RID: 6264 RVA: 0x0005D8ED File Offset: 0x0005BAED
		private void Dispose(bool disposing)
		{
			if (this._disposed)
			{
				this._disposed = true;
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="info">The object to populate with data.</param>
		/// <param name="context">The structure for this serialization.</param>
		// Token: 0x06001879 RID: 6265 RVA: 0x0005D900 File Offset: 0x0005BB00
		[MonoTODO("Missing serialization support")]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
		}

		// Token: 0x0600187A RID: 6266 RVA: 0x000173AD File Offset: 0x000155AD
		internal ActivationContext()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the ClickOnce application manifest for the current application.</summary>
		/// <returns>A byte array that contains the ClickOnce application manifest for the application that is associated with this <see cref="T:System.ActivationContext" />.</returns>
		// Token: 0x17000283 RID: 643
		// (get) Token: 0x0600187B RID: 6267 RVA: 0x00052955 File Offset: 0x00050B55
		public byte[] ApplicationManifestBytes
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the ClickOnce deployment manifest for the current application.</summary>
		/// <returns>A byte array that contains the ClickOnce deployment manifest for the application that is associated with this <see cref="T:System.ActivationContext" />.</returns>
		// Token: 0x17000284 RID: 644
		// (get) Token: 0x0600187C RID: 6268 RVA: 0x00052955 File Offset: 0x00050B55
		public byte[] DeploymentManifestBytes
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x040016B3 RID: 5811
		private ApplicationIdentity _appid;

		// Token: 0x040016B4 RID: 5812
		private ActivationContext.ContextForm _form;

		// Token: 0x040016B5 RID: 5813
		private bool _disposed;

		/// <summary>Indicates the context for a manifest-activated application.</summary>
		// Token: 0x02000224 RID: 548
		public enum ContextForm
		{
			/// <summary>The application is not in the ClickOnce store.</summary>
			// Token: 0x040016B7 RID: 5815
			Loose,
			/// <summary>The application is contained in the ClickOnce store.</summary>
			// Token: 0x040016B8 RID: 5816
			StoreBounded
		}
	}
}
