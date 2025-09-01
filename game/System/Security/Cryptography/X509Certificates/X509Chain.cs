using System;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents a chain-building engine for <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> certificates.</summary>
	// Token: 0x020002D9 RID: 729
	public class X509Chain : IDisposable
	{
		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x060016F0 RID: 5872 RVA: 0x0005B298 File Offset: 0x00059498
		internal X509ChainImpl Impl
		{
			get
			{
				X509Helper2.ThrowIfContextInvalid(this.impl);
				return this.impl;
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x060016F1 RID: 5873 RVA: 0x0005B2AB File Offset: 0x000594AB
		internal bool IsValid
		{
			get
			{
				return X509Helper2.IsValid(this.impl);
			}
		}

		// Token: 0x060016F2 RID: 5874 RVA: 0x0005B2B8 File Offset: 0x000594B8
		internal void ThrowIfContextInvalid()
		{
			X509Helper2.ThrowIfContextInvalid(this.impl);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> class.</summary>
		// Token: 0x060016F3 RID: 5875 RVA: 0x0005B2C5 File Offset: 0x000594C5
		public X509Chain() : this(false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> class specifying a value that indicates whether the machine context should be used.</summary>
		/// <param name="useMachineContext">
		///   <see langword="true" /> to use the machine context; <see langword="false" /> to use the current user context.</param>
		// Token: 0x060016F4 RID: 5876 RVA: 0x0005B2CE File Offset: 0x000594CE
		public X509Chain(bool useMachineContext)
		{
			this.impl = X509Helper2.CreateChainImpl(useMachineContext);
		}

		// Token: 0x060016F5 RID: 5877 RVA: 0x0005B2E2 File Offset: 0x000594E2
		internal X509Chain(X509ChainImpl impl)
		{
			X509Helper2.ThrowIfContextInvalid(impl);
			this.impl = impl;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> class using an <see cref="T:System.IntPtr" /> handle to an X.509 chain.</summary>
		/// <param name="chainContext">An <see cref="T:System.IntPtr" /> handle to an X.509 chain.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="chainContext" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="chainContext" /> parameter points to an invalid context.</exception>
		// Token: 0x060016F6 RID: 5878 RVA: 0x0005B2F7 File Offset: 0x000594F7
		[MonoTODO("Mono's X509Chain is fully managed. All handles are invalid.")]
		public X509Chain(IntPtr chainContext)
		{
			throw new NotSupportedException();
		}

		/// <summary>Gets a handle to an X.509 chain.</summary>
		/// <returns>An <see cref="T:System.IntPtr" /> handle to an X.509 chain.</returns>
		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x060016F7 RID: 5879 RVA: 0x0005B304 File Offset: 0x00059504
		[MonoTODO("Mono's X509Chain is fully managed. Always returns IntPtr.Zero.")]
		public IntPtr ChainContext
		{
			get
			{
				if (this.impl != null && this.impl.IsValid)
				{
					return this.impl.Handle;
				}
				return IntPtr.Zero;
			}
		}

		/// <summary>Gets a collection of <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElement" /> objects.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" /> object.</returns>
		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x060016F8 RID: 5880 RVA: 0x0005B32C File Offset: 0x0005952C
		public X509ChainElementCollection ChainElements
		{
			get
			{
				return this.Impl.ChainElements;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainPolicy" /> to use when building an X.509 certificate chain.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainPolicy" /> object associated with this X.509 chain.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value being set for this property is <see langword="null" />.</exception>
		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x060016F9 RID: 5881 RVA: 0x0005B339 File Offset: 0x00059539
		// (set) Token: 0x060016FA RID: 5882 RVA: 0x0005B346 File Offset: 0x00059546
		public X509ChainPolicy ChainPolicy
		{
			get
			{
				return this.Impl.ChainPolicy;
			}
			set
			{
				this.Impl.ChainPolicy = value;
			}
		}

		/// <summary>Gets the status of each element in an <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> object.</summary>
		/// <returns>An array of <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainStatus" /> objects.</returns>
		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x060016FB RID: 5883 RVA: 0x0005B354 File Offset: 0x00059554
		public X509ChainStatus[] ChainStatus
		{
			get
			{
				return this.Impl.ChainStatus;
			}
		}

		/// <summary>Gets a safe handle for this <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> instance.</summary>
		/// <returns>The safe handle for this <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> instance.</returns>
		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x060016FC RID: 5884 RVA: 0x0000829A File Offset: 0x0000649A
		public SafeX509ChainHandle SafeHandle
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Builds an X.509 chain using the policy specified in <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainPolicy" />.</summary>
		/// <param name="certificate">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if the X.509 certificate is valid; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="certificate" /> is not a valid certificate or is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="certificate" /> is unreadable.</exception>
		// Token: 0x060016FD RID: 5885 RVA: 0x0005B361 File Offset: 0x00059561
		[MonoTODO("Not totally RFC3280 compliant, but neither is MS implementation...")]
		public bool Build(X509Certificate2 certificate)
		{
			return this.Impl.Build(certificate);
		}

		/// <summary>Clears the current <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> object.</summary>
		// Token: 0x060016FE RID: 5886 RVA: 0x0005B36F File Offset: 0x0005956F
		public void Reset()
		{
			this.Impl.Reset();
		}

		/// <summary>Creates an <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> object after querying for the mapping defined in the CryptoConfig file, and maps the chain to that mapping.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> object.</returns>
		// Token: 0x060016FF RID: 5887 RVA: 0x0005B37C File Offset: 0x0005957C
		public static X509Chain Create()
		{
			return (X509Chain)CryptoConfig.CreateFromName("X509Chain");
		}

		/// <summary>Releases all of the resources used by this <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" />.</summary>
		// Token: 0x06001700 RID: 5888 RVA: 0x0005B38D File Offset: 0x0005958D
		[SecuritySafeCritical]
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by this <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" />, and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06001701 RID: 5889 RVA: 0x0005B39C File Offset: 0x0005959C
		protected virtual void Dispose(bool disposing)
		{
			if (this.impl != null)
			{
				this.impl.Dispose();
				this.impl = null;
			}
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x0005B3B8 File Offset: 0x000595B8
		~X509Chain()
		{
			this.Dispose(false);
		}

		// Token: 0x04000D02 RID: 3330
		private X509ChainImpl impl;
	}
}
