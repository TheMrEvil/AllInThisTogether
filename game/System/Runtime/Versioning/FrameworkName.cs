using System;
using System.Text;

namespace System.Runtime.Versioning
{
	/// <summary>Represents the name of a version of the .NET Framework.</summary>
	// Token: 0x02000182 RID: 386
	[Serializable]
	public sealed class FrameworkName : IEquatable<FrameworkName>
	{
		/// <summary>Gets the identifier of this <see cref="T:System.Runtime.Versioning.FrameworkName" /> object.</summary>
		/// <returns>The identifier of this <see cref="T:System.Runtime.Versioning.FrameworkName" /> object.</returns>
		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000A58 RID: 2648 RVA: 0x0002D11C File Offset: 0x0002B31C
		public string Identifier
		{
			get
			{
				return this.m_identifier;
			}
		}

		/// <summary>Gets the version of this <see cref="T:System.Runtime.Versioning.FrameworkName" /> object.</summary>
		/// <returns>An object that contains version information about this <see cref="T:System.Runtime.Versioning.FrameworkName" /> object.</returns>
		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x0002D124 File Offset: 0x0002B324
		public Version Version
		{
			get
			{
				return this.m_version;
			}
		}

		/// <summary>Gets the profile name of this <see cref="T:System.Runtime.Versioning.FrameworkName" /> object.</summary>
		/// <returns>The profile name of this <see cref="T:System.Runtime.Versioning.FrameworkName" /> object.</returns>
		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000A5A RID: 2650 RVA: 0x0002D12C File Offset: 0x0002B32C
		public string Profile
		{
			get
			{
				return this.m_profile;
			}
		}

		/// <summary>Gets the full name of this <see cref="T:System.Runtime.Versioning.FrameworkName" /> object.</summary>
		/// <returns>The full name of this <see cref="T:System.Runtime.Versioning.FrameworkName" /> object.</returns>
		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x0002D134 File Offset: 0x0002B334
		public string FullName
		{
			get
			{
				if (this.m_fullName == null)
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append(this.Identifier);
					stringBuilder.Append(',');
					stringBuilder.Append("Version").Append('=');
					stringBuilder.Append('v');
					stringBuilder.Append(this.Version);
					if (!string.IsNullOrEmpty(this.Profile))
					{
						stringBuilder.Append(',');
						stringBuilder.Append("Profile").Append('=');
						stringBuilder.Append(this.Profile);
					}
					this.m_fullName = stringBuilder.ToString();
				}
				return this.m_fullName;
			}
		}

		/// <summary>Returns a value that indicates whether this <see cref="T:System.Runtime.Versioning.FrameworkName" /> instance represents the same .NET Framework version as a specified object.</summary>
		/// <param name="obj">The object to compare to the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if every component of the current <see cref="T:System.Runtime.Versioning.FrameworkName" /> object matches the corresponding component of <paramref name="obj" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A5C RID: 2652 RVA: 0x0002D1D9 File Offset: 0x0002B3D9
		public override bool Equals(object obj)
		{
			return this.Equals(obj as FrameworkName);
		}

		/// <summary>Returns a value that indicates whether this <see cref="T:System.Runtime.Versioning.FrameworkName" /> instance represents the same .NET Framework version as a specified <see cref="T:System.Runtime.Versioning.FrameworkName" /> instance.</summary>
		/// <param name="other">The object to compare to the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if every component of the current <see cref="T:System.Runtime.Versioning.FrameworkName" /> object matches the corresponding component of <paramref name="other" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A5D RID: 2653 RVA: 0x0002D1E7 File Offset: 0x0002B3E7
		public bool Equals(FrameworkName other)
		{
			return other != null && (this.Identifier == other.Identifier && this.Version == other.Version) && this.Profile == other.Profile;
		}

		/// <summary>Returns the hash code for the <see cref="T:System.Runtime.Versioning.FrameworkName" /> object.</summary>
		/// <returns>A 32-bit signed integer that represents the hash code of this instance.</returns>
		// Token: 0x06000A5E RID: 2654 RVA: 0x0002D227 File Offset: 0x0002B427
		public override int GetHashCode()
		{
			return this.Identifier.GetHashCode() ^ this.Version.GetHashCode() ^ this.Profile.GetHashCode();
		}

		/// <summary>Returns the string representation of this <see cref="T:System.Runtime.Versioning.FrameworkName" /> object.</summary>
		/// <returns>A string that represents this <see cref="T:System.Runtime.Versioning.FrameworkName" /> object.</returns>
		// Token: 0x06000A5F RID: 2655 RVA: 0x0002D24C File Offset: 0x0002B44C
		public override string ToString()
		{
			return this.FullName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Versioning.FrameworkName" /> class from a string and a <see cref="T:System.Version" /> object that identify a .NET Framework version.</summary>
		/// <param name="identifier">A string that identifies a .NET Framework version.</param>
		/// <param name="version">An object that contains .NET Framework version information.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="identifier" /> is <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identifier" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="version" /> is <see langword="null" />.</exception>
		// Token: 0x06000A60 RID: 2656 RVA: 0x0002D254 File Offset: 0x0002B454
		public FrameworkName(string identifier, Version version) : this(identifier, version, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Versioning.FrameworkName" /> class from a string, a <see cref="T:System.Version" /> object that identifies a .NET Framework version, and a profile name.</summary>
		/// <param name="identifier">A string that identifies a .NET Framework version.</param>
		/// <param name="version">An object that contains .NET Framework version information.</param>
		/// <param name="profile">A profile name.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="identifier" /> is <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identifier" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="version" /> is <see langword="null" />.</exception>
		// Token: 0x06000A61 RID: 2657 RVA: 0x0002D260 File Offset: 0x0002B460
		public FrameworkName(string identifier, Version version, string profile)
		{
			if (identifier == null)
			{
				throw new ArgumentNullException("identifier");
			}
			if (identifier.Trim().Length == 0)
			{
				throw new ArgumentException(SR.GetString("The parameter '{0}' cannot be an empty string.", new object[]
				{
					"identifier"
				}), "identifier");
			}
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			this.m_identifier = identifier.Trim();
			this.m_version = (Version)version.Clone();
			this.m_profile = ((profile == null) ? string.Empty : profile.Trim());
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Versioning.FrameworkName" /> class from a string that contains information about a version of the .NET Framework.</summary>
		/// <param name="frameworkName">A string that contains .NET Framework version information.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="frameworkName" /> is <see cref="F:System.String.Empty" />.  
		/// -or-  
		/// <paramref name="frameworkName" /> has fewer than two components or more than three components.  
		/// -or-  
		/// <paramref name="frameworkName" /> does not include a major and minor version number.  
		/// -or-  
		/// <paramref name="frameworkName" /> does not include a valid version number.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="frameworkName" /> is <see langword="null" />.</exception>
		// Token: 0x06000A62 RID: 2658 RVA: 0x0002D2F8 File Offset: 0x0002B4F8
		public FrameworkName(string frameworkName)
		{
			if (frameworkName == null)
			{
				throw new ArgumentNullException("frameworkName");
			}
			if (frameworkName.Length == 0)
			{
				throw new ArgumentException(SR.GetString("The parameter '{0}' cannot be an empty string.", new object[]
				{
					"frameworkName"
				}), "frameworkName");
			}
			string[] array = frameworkName.Split(',', StringSplitOptions.None);
			if (array.Length < 2 || array.Length > 3)
			{
				throw new ArgumentException(SR.GetString("FrameworkName cannot have less than two components or more than three components."), "frameworkName");
			}
			this.m_identifier = array[0].Trim();
			if (this.m_identifier.Length == 0)
			{
				throw new ArgumentException(SR.GetString("FrameworkName is invalid."), "frameworkName");
			}
			bool flag = false;
			this.m_profile = string.Empty;
			int i = 1;
			while (i < array.Length)
			{
				string[] array2 = array[i].Split('=', StringSplitOptions.None);
				if (array2.Length != 2)
				{
					throw new ArgumentException(SR.GetString("FrameworkName is invalid."), "frameworkName");
				}
				string text = array2[0].Trim();
				string text2 = array2[1].Trim();
				if (text.Equals("Version", StringComparison.OrdinalIgnoreCase))
				{
					flag = true;
					if (text2.Length > 0 && (text2[0] == 'v' || text2[0] == 'V'))
					{
						text2 = text2.Substring(1);
					}
					try
					{
						this.m_version = new Version(text2);
						goto IL_181;
					}
					catch (Exception innerException)
					{
						throw new ArgumentException(SR.GetString("FrameworkName version component is invalid."), "frameworkName", innerException);
					}
					goto IL_14B;
				}
				goto IL_14B;
				IL_181:
				i++;
				continue;
				IL_14B:
				if (!text.Equals("Profile", StringComparison.OrdinalIgnoreCase))
				{
					throw new ArgumentException(SR.GetString("FrameworkName is invalid."), "frameworkName");
				}
				if (!string.IsNullOrEmpty(text2))
				{
					this.m_profile = text2;
					goto IL_181;
				}
				goto IL_181;
			}
			if (!flag)
			{
				throw new ArgumentException(SR.GetString("FrameworkName version component is missing."), "frameworkName");
			}
		}

		/// <summary>Returns a value that indicates whether two <see cref="T:System.Runtime.Versioning.FrameworkName" /> objects represent the same .NET Framework version.</summary>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="left" /> and <paramref name="right" /> parameters represent the same .NET Framework version; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A63 RID: 2659 RVA: 0x0002D4BC File Offset: 0x0002B6BC
		public static bool operator ==(FrameworkName left, FrameworkName right)
		{
			if (left == null)
			{
				return right == null;
			}
			return left.Equals(right);
		}

		/// <summary>Returns a value that indicates whether two <see cref="T:System.Runtime.Versioning.FrameworkName" /> objects represent different .NET Framework versions.</summary>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="left" /> and <paramref name="right" /> parameters represent different .NET Framework versions; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A64 RID: 2660 RVA: 0x0002D4CD File Offset: 0x0002B6CD
		public static bool operator !=(FrameworkName left, FrameworkName right)
		{
			return !(left == right);
		}

		// Token: 0x040006DD RID: 1757
		private readonly string m_identifier;

		// Token: 0x040006DE RID: 1758
		private readonly Version m_version;

		// Token: 0x040006DF RID: 1759
		private readonly string m_profile;

		// Token: 0x040006E0 RID: 1760
		private string m_fullName;

		// Token: 0x040006E1 RID: 1761
		private const char c_componentSeparator = ',';

		// Token: 0x040006E2 RID: 1762
		private const char c_keyValueSeparator = '=';

		// Token: 0x040006E3 RID: 1763
		private const char c_versionValuePrefix = 'v';

		// Token: 0x040006E4 RID: 1764
		private const string c_versionKey = "Version";

		// Token: 0x040006E5 RID: 1765
		private const string c_profileKey = "Profile";
	}
}
