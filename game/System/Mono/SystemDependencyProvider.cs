using System;
using System.Runtime.CompilerServices;

namespace Mono
{
	// Token: 0x02000031 RID: 49
	internal class SystemDependencyProvider : ISystemDependencyProvider
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00002EAD File Offset: 0x000010AD
		public static SystemDependencyProvider Instance
		{
			get
			{
				SystemDependencyProvider.Initialize();
				return SystemDependencyProvider.instance;
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00002EBC File Offset: 0x000010BC
		internal static void Initialize()
		{
			object obj = SystemDependencyProvider.syncRoot;
			lock (obj)
			{
				if (SystemDependencyProvider.instance == null)
				{
					SystemDependencyProvider.instance = new SystemDependencyProvider();
				}
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00002F08 File Offset: 0x00001108
		ISystemCertificateProvider ISystemDependencyProvider.CertificateProvider
		{
			get
			{
				return this.CertificateProvider;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00002F10 File Offset: 0x00001110
		public SystemCertificateProvider CertificateProvider
		{
			[CompilerGenerated]
			get
			{
				return this.<CertificateProvider>k__BackingField;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00002F18 File Offset: 0x00001118
		public X509PalImpl X509Pal
		{
			get
			{
				return this.CertificateProvider.X509Pal;
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00002F25 File Offset: 0x00001125
		private SystemDependencyProvider()
		{
			this.CertificateProvider = new SystemCertificateProvider();
			DependencyInjector.Register(this);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00002F3E File Offset: 0x0000113E
		// Note: this type is marked as 'beforefieldinit'.
		static SystemDependencyProvider()
		{
		}

		// Token: 0x04000121 RID: 289
		private static SystemDependencyProvider instance;

		// Token: 0x04000122 RID: 290
		private static object syncRoot = new object();

		// Token: 0x04000123 RID: 291
		[CompilerGenerated]
		private readonly SystemCertificateProvider <CertificateProvider>k__BackingField;
	}
}
