using System;
using System.Runtime.CompilerServices;
using Mono;

namespace System.Runtime.InteropServices
{
	/// <summary>Provides information about the .NET runtime installation.</summary>
	// Token: 0x02000737 RID: 1847
	public static class RuntimeInformation
	{
		// Token: 0x06004114 RID: 16660 RVA: 0x000E1A10 File Offset: 0x000DFC10
		static RuntimeInformation()
		{
			string runtimeArchitecture = RuntimeInformation.GetRuntimeArchitecture();
			string osname = RuntimeInformation.GetOSName();
			if (!(runtimeArchitecture == "arm"))
			{
				if (!(runtimeArchitecture == "armv8"))
				{
					if (!(runtimeArchitecture == "x86"))
					{
						if (!(runtimeArchitecture == "x86-64"))
						{
							if (!(runtimeArchitecture == "wasm"))
							{
							}
							RuntimeInformation._osArchitecture = (Environment.Is64BitOperatingSystem ? Architecture.X64 : Architecture.X86);
							RuntimeInformation._processArchitecture = (Environment.Is64BitProcess ? Architecture.X64 : Architecture.X86);
						}
						else
						{
							RuntimeInformation._osArchitecture = (Environment.Is64BitOperatingSystem ? Architecture.X64 : Architecture.X86);
							RuntimeInformation._processArchitecture = Architecture.X64;
						}
					}
					else
					{
						RuntimeInformation._osArchitecture = (Environment.Is64BitOperatingSystem ? Architecture.X64 : Architecture.X86);
						RuntimeInformation._processArchitecture = Architecture.X86;
					}
				}
				else
				{
					RuntimeInformation._osArchitecture = (Environment.Is64BitOperatingSystem ? Architecture.Arm64 : Architecture.Arm);
					RuntimeInformation._processArchitecture = Architecture.Arm64;
				}
			}
			else
			{
				RuntimeInformation._osArchitecture = (Environment.Is64BitOperatingSystem ? Architecture.Arm64 : Architecture.Arm);
				RuntimeInformation._processArchitecture = Architecture.Arm;
			}
			uint num = <PrivateImplementationDetails>.ComputeStringHash(osname);
			if (num <= 2784415053U)
			{
				if (num <= 758268069U)
				{
					if (num != 311744602U)
					{
						if (num == 758268069U)
						{
							if (osname == "aix")
							{
								RuntimeInformation._osPlatform = OSPlatform.Create("AIX");
								return;
							}
						}
					}
					else if (osname == "solaris")
					{
						RuntimeInformation._osPlatform = OSPlatform.Create("SOLARIS");
						return;
					}
				}
				else if (num != 1846719142U)
				{
					if (num != 1968959064U)
					{
						if (num == 2784415053U)
						{
							if (osname == "wasm")
							{
								RuntimeInformation._osPlatform = OSPlatform.Create("BROWSER");
								return;
							}
						}
					}
					else if (osname == "hpux")
					{
						RuntimeInformation._osPlatform = OSPlatform.Create("HPUX");
						return;
					}
				}
				else if (osname == "openbsd")
				{
					RuntimeInformation._osPlatform = OSPlatform.Create("OPENBSD");
					return;
				}
			}
			else if (num <= 3229321689U)
			{
				if (num != 2876596737U)
				{
					if (num != 3139461053U)
					{
						if (num == 3229321689U)
						{
							if (osname == "netbsd")
							{
								RuntimeInformation._osPlatform = OSPlatform.Create("NETBSD");
								return;
							}
						}
					}
					else if (osname == "osx")
					{
						RuntimeInformation._osPlatform = OSPlatform.OSX;
						return;
					}
				}
				else if (osname == "haiku")
				{
					RuntimeInformation._osPlatform = OSPlatform.Create("HAIKU");
					return;
				}
			}
			else if (num != 3583452906U)
			{
				if (num != 3971716381U)
				{
					if (num == 4059584116U)
					{
						if (osname == "freebsd")
						{
							RuntimeInformation._osPlatform = OSPlatform.Create("FREEBSD");
							return;
						}
					}
				}
				else if (osname == "linux")
				{
					RuntimeInformation._osPlatform = OSPlatform.Linux;
					return;
				}
			}
			else if (osname == "windows")
			{
				RuntimeInformation._osPlatform = OSPlatform.Windows;
				return;
			}
			RuntimeInformation._osPlatform = OSPlatform.Create("UNKNOWN");
		}

		// Token: 0x06004115 RID: 16661
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetRuntimeArchitecture();

		// Token: 0x06004116 RID: 16662
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetOSName();

		/// <summary>Returns a string that indicates the name of the .NET installation on which an app is running.</summary>
		/// <returns>The name of the .NET installation on which the app is running.</returns>
		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x06004117 RID: 16663 RVA: 0x000E1D23 File Offset: 0x000DFF23
		public static string FrameworkDescription
		{
			get
			{
				return "Mono " + Runtime.GetDisplayName();
			}
		}

		/// <summary>Indicates whether the current application is running on the specified platform.</summary>
		/// <param name="osPlatform">A platform.</param>
		/// <returns>
		///   <see langword="true" /> if the current app is running on the specified platform; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004118 RID: 16664 RVA: 0x000E1D34 File Offset: 0x000DFF34
		public static bool IsOSPlatform(OSPlatform osPlatform)
		{
			return RuntimeInformation._osPlatform == osPlatform;
		}

		/// <summary>Gets a string that describes the operating system on which the app is running.</summary>
		/// <returns>The description of the operating system on which the app is running.</returns>
		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x06004119 RID: 16665 RVA: 0x000E1D41 File Offset: 0x000DFF41
		public static string OSDescription
		{
			get
			{
				return Environment.OSVersion.VersionString;
			}
		}

		/// <summary>Gets the platform architecture on which the current app is running.</summary>
		/// <returns>The platform architecture on which the current app is running.</returns>
		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x0600411A RID: 16666 RVA: 0x000E1D4D File Offset: 0x000DFF4D
		public static Architecture OSArchitecture
		{
			get
			{
				return RuntimeInformation._osArchitecture;
			}
		}

		/// <summary>Gets the process architecture of the currently running app.</summary>
		/// <returns>The process architecture of the currently running app.</returns>
		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x0600411B RID: 16667 RVA: 0x000E1D54 File Offset: 0x000DFF54
		public static Architecture ProcessArchitecture
		{
			get
			{
				return RuntimeInformation._processArchitecture;
			}
		}

		// Token: 0x04002BA9 RID: 11177
		private static readonly Architecture _osArchitecture;

		// Token: 0x04002BAA RID: 11178
		private static readonly Architecture _processArchitecture;

		// Token: 0x04002BAB RID: 11179
		private static readonly OSPlatform _osPlatform;
	}
}
