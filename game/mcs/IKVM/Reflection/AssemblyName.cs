using System;
using System.Configuration.Assemblies;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using IKVM.Reflection.Reader;

namespace IKVM.Reflection
{
	// Token: 0x02000006 RID: 6
	public sealed class AssemblyName : ICloneable
	{
		// Token: 0x06000037 RID: 55 RVA: 0x0000230C File Offset: 0x0000050C
		public AssemblyName()
		{
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000231C File Offset: 0x0000051C
		public AssemblyName(string assemblyName)
		{
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			if (assemblyName == "")
			{
				throw new ArgumentException();
			}
			ParsedAssemblyName parsedAssemblyName;
			ParseAssemblyResult parseAssemblyResult = Fusion.ParseAssemblyName(assemblyName, out parsedAssemblyName);
			if (parseAssemblyResult == ParseAssemblyResult.GenericError || parseAssemblyResult == ParseAssemblyResult.DuplicateKey)
			{
				throw new FileLoadException();
			}
			if (!AssemblyName.ParseVersion(parsedAssemblyName.Version, parsedAssemblyName.Retargetable != null, out this.version))
			{
				throw new FileLoadException();
			}
			this.name = parsedAssemblyName.Name;
			if (parsedAssemblyName.Culture != null)
			{
				if (parsedAssemblyName.Culture.Equals("neutral", StringComparison.OrdinalIgnoreCase))
				{
					this.culture = "";
				}
				else
				{
					if (parsedAssemblyName.Culture == "")
					{
						throw new FileLoadException();
					}
					this.culture = new CultureInfo(parsedAssemblyName.Culture).Name;
				}
			}
			if (parsedAssemblyName.PublicKeyToken != null)
			{
				if (parsedAssemblyName.PublicKeyToken.Equals("null", StringComparison.OrdinalIgnoreCase))
				{
					this.publicKeyToken = Empty<byte>.Array;
				}
				else
				{
					if (parsedAssemblyName.PublicKeyToken.Length != 16)
					{
						throw new FileLoadException();
					}
					this.publicKeyToken = AssemblyName.ParseKey(parsedAssemblyName.PublicKeyToken);
				}
			}
			if (parsedAssemblyName.Retargetable != null)
			{
				if (parsedAssemblyName.Culture == null || parsedAssemblyName.PublicKeyToken == null || this.version == null)
				{
					throw new FileLoadException();
				}
				if (parsedAssemblyName.Retargetable.Value)
				{
					this.flags |= AssemblyNameFlags.Retargetable;
				}
			}
			this.ProcessorArchitecture = parsedAssemblyName.ProcessorArchitecture;
			if (parsedAssemblyName.WindowsRuntime)
			{
				this.ContentType = AssemblyContentType.WindowsRuntime;
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000024B0 File Offset: 0x000006B0
		private static byte[] ParseKey(string key)
		{
			if ((key.Length & 1) != 0)
			{
				throw new FileLoadException();
			}
			byte[] array = new byte[key.Length / 2];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (byte)(AssemblyName.ParseHexDigit(key[i * 2]) * 16 + AssemblyName.ParseHexDigit(key[i * 2 + 1]));
			}
			return array;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002510 File Offset: 0x00000710
		private static int ParseHexDigit(char digit)
		{
			if (digit >= '0' && digit <= '9')
			{
				return (int)(digit - '0');
			}
			digit |= ' ';
			if (digit >= 'a' && digit <= 'f')
			{
				return (int)('\n' + digit - 'a');
			}
			throw new FileLoadException();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000253F File Offset: 0x0000073F
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002547 File Offset: 0x00000747
		// (set) Token: 0x0600003D RID: 61 RVA: 0x0000254F File Offset: 0x0000074F
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002558 File Offset: 0x00000758
		// (set) Token: 0x0600003F RID: 63 RVA: 0x0000256F File Offset: 0x0000076F
		public CultureInfo CultureInfo
		{
			get
			{
				if (this.culture != null)
				{
					return new CultureInfo(this.culture);
				}
				return null;
			}
			set
			{
				this.culture = ((value == null) ? null : value.Name);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002583 File Offset: 0x00000783
		public string CultureName
		{
			get
			{
				return this.culture;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002583 File Offset: 0x00000783
		// (set) Token: 0x06000042 RID: 66 RVA: 0x0000258B File Offset: 0x0000078B
		internal string Culture
		{
			get
			{
				return this.culture;
			}
			set
			{
				this.culture = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002594 File Offset: 0x00000794
		// (set) Token: 0x06000044 RID: 68 RVA: 0x0000259C File Offset: 0x0000079C
		public Version Version
		{
			get
			{
				return this.version;
			}
			set
			{
				this.version = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000045 RID: 69 RVA: 0x000025A5 File Offset: 0x000007A5
		// (set) Token: 0x06000046 RID: 70 RVA: 0x000025AD File Offset: 0x000007AD
		public StrongNameKeyPair KeyPair
		{
			get
			{
				return this.keyPair;
			}
			set
			{
				this.keyPair = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000047 RID: 71 RVA: 0x000025B6 File Offset: 0x000007B6
		// (set) Token: 0x06000048 RID: 72 RVA: 0x000025BE File Offset: 0x000007BE
		public string CodeBase
		{
			get
			{
				return this.codeBase;
			}
			set
			{
				this.codeBase = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000049 RID: 73 RVA: 0x000025C7 File Offset: 0x000007C7
		public string EscapedCodeBase
		{
			get
			{
				return new AssemblyName
				{
					CodeBase = this.codeBase
				}.EscapedCodeBase;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600004A RID: 74 RVA: 0x000025DF File Offset: 0x000007DF
		// (set) Token: 0x0600004B RID: 75 RVA: 0x000025EC File Offset: 0x000007EC
		public ProcessorArchitecture ProcessorArchitecture
		{
			get
			{
				return (ProcessorArchitecture)((this.flags & (AssemblyNameFlags)112) >> 4);
			}
			set
			{
				if (value >= ProcessorArchitecture.None && value <= ProcessorArchitecture.Arm)
				{
					this.flags = ((this.flags & (AssemblyNameFlags)(-113)) | (AssemblyNameFlags)(value << 4));
				}
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002609 File Offset: 0x00000809
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00002617 File Offset: 0x00000817
		public AssemblyNameFlags Flags
		{
			get
			{
				return this.flags & (AssemblyNameFlags)(-3825);
			}
			set
			{
				this.flags = ((this.flags & (AssemblyNameFlags)3824) | (value & (AssemblyNameFlags)(-3825)));
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002633 File Offset: 0x00000833
		// (set) Token: 0x0600004F RID: 79 RVA: 0x0000263B File Offset: 0x0000083B
		public AssemblyVersionCompatibility VersionCompatibility
		{
			get
			{
				return this.versionCompatibility;
			}
			set
			{
				this.versionCompatibility = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002644 File Offset: 0x00000844
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00002655 File Offset: 0x00000855
		public AssemblyContentType ContentType
		{
			get
			{
				return (AssemblyContentType)((this.flags & (AssemblyNameFlags)3584) >> 9);
			}
			set
			{
				if (value >= AssemblyContentType.Default && value <= AssemblyContentType.WindowsRuntime)
				{
					this.flags = ((this.flags & (AssemblyNameFlags)(-3585)) | (AssemblyNameFlags)(value << 9));
				}
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002676 File Offset: 0x00000876
		public byte[] GetPublicKey()
		{
			return this.publicKey;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x0000267E File Offset: 0x0000087E
		public void SetPublicKey(byte[] publicKey)
		{
			this.publicKey = publicKey;
			this.flags = ((this.flags & ~AssemblyNameFlags.PublicKey) | ((publicKey == null) ? AssemblyNameFlags.None : AssemblyNameFlags.PublicKey));
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000269E File Offset: 0x0000089E
		public byte[] GetPublicKeyToken()
		{
			if (this.publicKeyToken == null && this.publicKey != null)
			{
				this.publicKeyToken = AssemblyName.ComputePublicKeyToken(this.publicKey);
			}
			return this.publicKeyToken;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000026C7 File Offset: 0x000008C7
		public void SetPublicKeyToken(byte[] publicKeyToken)
		{
			this.publicKeyToken = publicKeyToken;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000056 RID: 86 RVA: 0x000026D0 File Offset: 0x000008D0
		// (set) Token: 0x06000057 RID: 87 RVA: 0x000026D8 File Offset: 0x000008D8
		public AssemblyHashAlgorithm HashAlgorithm
		{
			get
			{
				return this.hashAlgorithm;
			}
			set
			{
				this.hashAlgorithm = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000058 RID: 88 RVA: 0x000026E1 File Offset: 0x000008E1
		public byte[] __Hash
		{
			get
			{
				return this.hash;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000059 RID: 89 RVA: 0x000026EC File Offset: 0x000008EC
		public string FullName
		{
			get
			{
				if (this.name == null)
				{
					return "";
				}
				ushort versionMajor = ushort.MaxValue;
				ushort versionMinor = ushort.MaxValue;
				ushort versionBuild = ushort.MaxValue;
				ushort versionRevision = ushort.MaxValue;
				if (this.version != null)
				{
					versionMajor = (ushort)this.version.Major;
					versionMinor = (ushort)this.version.Minor;
					versionBuild = (ushort)this.version.Build;
					versionRevision = (ushort)this.version.Revision;
				}
				byte[] array = this.publicKeyToken;
				if ((array == null || array.Length == 0) && this.publicKey != null)
				{
					array = AssemblyName.ComputePublicKeyToken(this.publicKey);
				}
				return AssemblyName.GetFullName(this.name, versionMajor, versionMinor, versionBuild, versionRevision, this.culture, array, (int)this.flags);
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000027A4 File Offset: 0x000009A4
		internal static string GetFullName(string name, ushort versionMajor, ushort versionMinor, ushort versionBuild, ushort versionRevision, string culture, byte[] publicKeyToken, int flags)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = name.StartsWith(" ") || name.EndsWith(" ") || name.IndexOf('\'') != -1;
			bool flag2 = name.IndexOf('"') != -1;
			if (flag2)
			{
				stringBuilder.Append('\'');
			}
			else if (flag)
			{
				stringBuilder.Append('"');
			}
			if (name.IndexOf(',') != -1 || name.IndexOf('\\') != -1 || name.IndexOf('=') != -1 || (flag2 && name.IndexOf('\'') != -1))
			{
				foreach (char c in name)
				{
					if (c == ',' || c == '\\' || c == '=' || (flag2 && c == '\''))
					{
						stringBuilder.Append('\\');
					}
					stringBuilder.Append(c);
				}
			}
			else
			{
				stringBuilder.Append(name);
			}
			if (flag2)
			{
				stringBuilder.Append('\'');
			}
			else if (flag)
			{
				stringBuilder.Append('"');
			}
			if (versionMajor != 65535)
			{
				stringBuilder.Append(", Version=").Append(versionMajor);
				if (versionMinor != 65535)
				{
					stringBuilder.Append('.').Append(versionMinor);
					if (versionBuild != 65535)
					{
						stringBuilder.Append('.').Append(versionBuild);
						if (versionRevision != 65535)
						{
							stringBuilder.Append('.').Append(versionRevision);
						}
					}
				}
			}
			if (culture != null)
			{
				stringBuilder.Append(", Culture=").Append((culture == "") ? "neutral" : culture);
			}
			if (publicKeyToken != null)
			{
				stringBuilder.Append(", PublicKeyToken=");
				if (publicKeyToken.Length == 0)
				{
					stringBuilder.Append("null");
				}
				else
				{
					AssemblyName.AppendPublicKey(stringBuilder, publicKeyToken);
				}
			}
			if ((flags & 256) != 0)
			{
				stringBuilder.Append(", Retargetable=Yes");
			}
			if ((flags & 3584) >> 9 == 1)
			{
				stringBuilder.Append(", ContentType=WindowsRuntime");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002994 File Offset: 0x00000B94
		internal static byte[] ComputePublicKeyToken(byte[] publicKey)
		{
			if (publicKey.Length == 0)
			{
				return publicKey;
			}
			byte[] array = new SHA1Managed().ComputeHash(publicKey);
			byte[] array2 = new byte[8];
			for (int i = 0; i < array2.Length; i++)
			{
				byte[] array3 = array2;
				int num = i;
				byte[] array4 = array;
				array3[num] = array4[array4.Length - 1 - i];
			}
			return array2;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000029D5 File Offset: 0x00000BD5
		internal static string ComputePublicKeyToken(string publicKey)
		{
			StringBuilder stringBuilder = new StringBuilder(16);
			AssemblyName.AppendPublicKey(stringBuilder, AssemblyName.ComputePublicKeyToken(AssemblyName.ParseKey(publicKey)));
			return stringBuilder.ToString();
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000029F4 File Offset: 0x00000BF4
		private static void AppendPublicKey(StringBuilder sb, byte[] publicKey)
		{
			for (int i = 0; i < publicKey.Length; i++)
			{
				sb.Append("0123456789abcdef"[publicKey[i] >> 4]);
				sb.Append("0123456789abcdef"[(int)(publicKey[i] & 15)]);
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002A3C File Offset: 0x00000C3C
		public override bool Equals(object obj)
		{
			AssemblyName assemblyName = obj as AssemblyName;
			return assemblyName != null && assemblyName.FullName == this.FullName;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002A66 File Offset: 0x00000C66
		public override int GetHashCode()
		{
			return this.FullName.GetHashCode();
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002A73 File Offset: 0x00000C73
		public object Clone()
		{
			AssemblyName assemblyName = (AssemblyName)base.MemberwiseClone();
			assemblyName.publicKey = AssemblyName.Copy(this.publicKey);
			assemblyName.publicKeyToken = AssemblyName.Copy(this.publicKeyToken);
			return assemblyName;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002AA2 File Offset: 0x00000CA2
		private static byte[] Copy(byte[] b)
		{
			if (b != null && b.Length != 0)
			{
				return (byte[])b.Clone();
			}
			return b;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002AB8 File Offset: 0x00000CB8
		public static bool ReferenceMatchesDefinition(AssemblyName reference, AssemblyName definition)
		{
			return AssemblyName.ReferenceMatchesDefinition(new AssemblyName(reference.FullName), new AssemblyName(definition.FullName));
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002AD8 File Offset: 0x00000CD8
		public static AssemblyName GetAssemblyName(string path)
		{
			AssemblyName result;
			try
			{
				path = Path.GetFullPath(path);
				using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
				{
					ModuleReader moduleReader = new ModuleReader(null, null, fileStream, path, false);
					if (moduleReader.Assembly == null)
					{
						throw new BadImageFormatException("Module does not contain a manifest");
					}
					result = moduleReader.Assembly.GetName();
				}
			}
			catch (IOException ex)
			{
				throw new FileNotFoundException(ex.Message, ex);
			}
			catch (UnauthorizedAccessException ex2)
			{
				throw new FileNotFoundException(ex2.Message, ex2);
			}
			return result;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002B70 File Offset: 0x00000D70
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00002B78 File Offset: 0x00000D78
		internal AssemblyNameFlags RawFlags
		{
			get
			{
				return this.flags;
			}
			set
			{
				this.flags = value;
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002B84 File Offset: 0x00000D84
		private static bool ParseVersion(string str, bool mustBeComplete, out Version version)
		{
			if (str == null)
			{
				version = null;
				return true;
			}
			string[] array = str.Split(new char[]
			{
				'.'
			});
			if (array.Length < 2 || array.Length > 4)
			{
				version = null;
				ushort num;
				return array.Length == 1 && ushort.TryParse(array[0], NumberStyles.Integer, null, out num);
			}
			if (array[0] == "" || array[1] == "")
			{
				version = null;
				return true;
			}
			ushort maxValue = ushort.MaxValue;
			ushort maxValue2 = ushort.MaxValue;
			ushort num2;
			ushort num3;
			if (ushort.TryParse(array[0], NumberStyles.Integer, null, out num2) && ushort.TryParse(array[1], NumberStyles.Integer, null, out num3) && (array.Length <= 2 || array[2] == "" || ushort.TryParse(array[2], NumberStyles.Integer, null, out maxValue)) && (array.Length <= 3 || array[3] == "" || (array[2] != "" && ushort.TryParse(array[3], NumberStyles.Integer, null, out maxValue2))))
			{
				if (mustBeComplete && (array.Length < 4 || array[2] == "" || array[3] == ""))
				{
					version = null;
				}
				else if (num2 == 65535 || num3 == 65535)
				{
					version = null;
				}
				else
				{
					version = new Version((int)num2, (int)num3, (int)maxValue, (int)maxValue2);
				}
				return true;
			}
			version = null;
			return false;
		}

		// Token: 0x04000023 RID: 35
		private string name;

		// Token: 0x04000024 RID: 36
		private string culture;

		// Token: 0x04000025 RID: 37
		private Version version;

		// Token: 0x04000026 RID: 38
		private byte[] publicKeyToken;

		// Token: 0x04000027 RID: 39
		private byte[] publicKey;

		// Token: 0x04000028 RID: 40
		private StrongNameKeyPair keyPair;

		// Token: 0x04000029 RID: 41
		private AssemblyNameFlags flags;

		// Token: 0x0400002A RID: 42
		private AssemblyHashAlgorithm hashAlgorithm;

		// Token: 0x0400002B RID: 43
		private AssemblyVersionCompatibility versionCompatibility = AssemblyVersionCompatibility.SameMachine;

		// Token: 0x0400002C RID: 44
		private string codeBase;

		// Token: 0x0400002D RID: 45
		internal byte[] hash;
	}
}
