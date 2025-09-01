using System;
using System.Globalization;
using System.Text;

namespace System
{
	/// <summary>Represents the version number of an assembly, operating system, or the common language runtime. This class cannot be inherited.</summary>
	// Token: 0x020001B6 RID: 438
	[Serializable]
	public sealed class Version : ICloneable, IComparable, IComparable<Version>, IEquatable<Version>, ISpanFormattable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Version" /> class with the specified major, minor, build, and revision numbers.</summary>
		/// <param name="major">The major version number.</param>
		/// <param name="minor">The minor version number.</param>
		/// <param name="build">The build number.</param>
		/// <param name="revision">The revision number.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="major" />, <paramref name="minor" />, <paramref name="build" />, or <paramref name="revision" /> is less than zero.</exception>
		// Token: 0x06001300 RID: 4864 RVA: 0x0004CEBC File Offset: 0x0004B0BC
		public Version(int major, int minor, int build, int revision)
		{
			if (major < 0)
			{
				throw new ArgumentOutOfRangeException("major", "Version's parameters must be greater than or equal to zero.");
			}
			if (minor < 0)
			{
				throw new ArgumentOutOfRangeException("minor", "Version's parameters must be greater than or equal to zero.");
			}
			if (build < 0)
			{
				throw new ArgumentOutOfRangeException("build", "Version's parameters must be greater than or equal to zero.");
			}
			if (revision < 0)
			{
				throw new ArgumentOutOfRangeException("revision", "Version's parameters must be greater than or equal to zero.");
			}
			this._Major = major;
			this._Minor = minor;
			this._Build = build;
			this._Revision = revision;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Version" /> class using the specified major, minor, and build values.</summary>
		/// <param name="major">The major version number.</param>
		/// <param name="minor">The minor version number.</param>
		/// <param name="build">The build number.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="major" />, <paramref name="minor" />, or <paramref name="build" /> is less than zero.</exception>
		// Token: 0x06001301 RID: 4865 RVA: 0x0004CF4C File Offset: 0x0004B14C
		public Version(int major, int minor, int build)
		{
			if (major < 0)
			{
				throw new ArgumentOutOfRangeException("major", "Version's parameters must be greater than or equal to zero.");
			}
			if (minor < 0)
			{
				throw new ArgumentOutOfRangeException("minor", "Version's parameters must be greater than or equal to zero.");
			}
			if (build < 0)
			{
				throw new ArgumentOutOfRangeException("build", "Version's parameters must be greater than or equal to zero.");
			}
			this._Major = major;
			this._Minor = minor;
			this._Build = build;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Version" /> class using the specified major and minor values.</summary>
		/// <param name="major">The major version number.</param>
		/// <param name="minor">The minor version number.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="major" /> or <paramref name="minor" /> is less than zero.</exception>
		// Token: 0x06001302 RID: 4866 RVA: 0x0004CFC0 File Offset: 0x0004B1C0
		public Version(int major, int minor)
		{
			if (major < 0)
			{
				throw new ArgumentOutOfRangeException("major", "Version's parameters must be greater than or equal to zero.");
			}
			if (minor < 0)
			{
				throw new ArgumentOutOfRangeException("minor", "Version's parameters must be greater than or equal to zero.");
			}
			this._Major = major;
			this._Minor = minor;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Version" /> class using the specified string.</summary>
		/// <param name="version">A string containing the major, minor, build, and revision numbers, where each number is delimited with a period character ('.').</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="version" /> has fewer than two components or more than four components.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="version" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">A major, minor, build, or revision component is less than zero.</exception>
		/// <exception cref="T:System.FormatException">At least one component of <paramref name="version" /> does not parse to an integer.</exception>
		/// <exception cref="T:System.OverflowException">At least one component of <paramref name="version" /> represents a number greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06001303 RID: 4867 RVA: 0x0004D018 File Offset: 0x0004B218
		public Version(string version)
		{
			Version version2 = Version.Parse(version);
			this._Major = version2.Major;
			this._Minor = version2.Minor;
			this._Build = version2.Build;
			this._Revision = version2.Revision;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Version" /> class.</summary>
		// Token: 0x06001304 RID: 4868 RVA: 0x0004D070 File Offset: 0x0004B270
		public Version()
		{
			this._Major = 0;
			this._Minor = 0;
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x0004D094 File Offset: 0x0004B294
		private Version(Version version)
		{
			this._Major = version._Major;
			this._Minor = version._Minor;
			this._Build = version._Build;
			this._Revision = version._Revision;
		}

		/// <summary>Returns a new <see cref="T:System.Version" /> object whose value is the same as the current <see cref="T:System.Version" /> object.</summary>
		/// <returns>A new <see cref="T:System.Object" /> whose values are a copy of the current <see cref="T:System.Version" /> object.</returns>
		// Token: 0x06001306 RID: 4870 RVA: 0x0004D0E5 File Offset: 0x0004B2E5
		public object Clone()
		{
			return new Version(this);
		}

		/// <summary>Gets the value of the major component of the version number for the current <see cref="T:System.Version" /> object.</summary>
		/// <returns>The major version number.</returns>
		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06001307 RID: 4871 RVA: 0x0004D0ED File Offset: 0x0004B2ED
		public int Major
		{
			get
			{
				return this._Major;
			}
		}

		/// <summary>Gets the value of the minor component of the version number for the current <see cref="T:System.Version" /> object.</summary>
		/// <returns>The minor version number.</returns>
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06001308 RID: 4872 RVA: 0x0004D0F5 File Offset: 0x0004B2F5
		public int Minor
		{
			get
			{
				return this._Minor;
			}
		}

		/// <summary>Gets the value of the build component of the version number for the current <see cref="T:System.Version" /> object.</summary>
		/// <returns>The build number, or -1 if the build number is undefined.</returns>
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06001309 RID: 4873 RVA: 0x0004D0FD File Offset: 0x0004B2FD
		public int Build
		{
			get
			{
				return this._Build;
			}
		}

		/// <summary>Gets the value of the revision component of the version number for the current <see cref="T:System.Version" /> object.</summary>
		/// <returns>The revision number, or -1 if the revision number is undefined.</returns>
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x0600130A RID: 4874 RVA: 0x0004D105 File Offset: 0x0004B305
		public int Revision
		{
			get
			{
				return this._Revision;
			}
		}

		/// <summary>Gets the high 16 bits of the revision number.</summary>
		/// <returns>A 16-bit signed integer.</returns>
		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x0600130B RID: 4875 RVA: 0x0004D10D File Offset: 0x0004B30D
		public short MajorRevision
		{
			get
			{
				return (short)(this._Revision >> 16);
			}
		}

		/// <summary>Gets the low 16 bits of the revision number.</summary>
		/// <returns>A 16-bit signed integer.</returns>
		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x0600130C RID: 4876 RVA: 0x0004D119 File Offset: 0x0004B319
		public short MinorRevision
		{
			get
			{
				return (short)(this._Revision & 65535);
			}
		}

		/// <summary>Compares the current <see cref="T:System.Version" /> object to a specified object and returns an indication of their relative values.</summary>
		/// <param name="version">An object to compare, or <see langword="null" />.</param>
		/// <returns>A signed integer that indicates the relative values of the two objects, as shown in the following table.  
		///   Return value  
		///
		///   Meaning  
		///
		///   Less than zero  
		///
		///   The current <see cref="T:System.Version" /> object is a version before <paramref name="version" />.  
		///
		///   Zero  
		///
		///   The current <see cref="T:System.Version" /> object is the same version as <paramref name="version" />.  
		///
		///   Greater than zero  
		///
		///   The current <see cref="T:System.Version" /> object is a version subsequent to <paramref name="version" />.  
		///
		///  -or-  
		///
		///  <paramref name="version" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="version" /> is not of type <see cref="T:System.Version" />.</exception>
		// Token: 0x0600130D RID: 4877 RVA: 0x0004D128 File Offset: 0x0004B328
		public int CompareTo(object version)
		{
			if (version == null)
			{
				return 1;
			}
			Version version2 = version as Version;
			if (version2 == null)
			{
				throw new ArgumentException("Object must be of type Version.");
			}
			return this.CompareTo(version2);
		}

		/// <summary>Compares the current <see cref="T:System.Version" /> object to a specified <see cref="T:System.Version" /> object and returns an indication of their relative values.</summary>
		/// <param name="value">A <see cref="T:System.Version" /> object to compare to the current <see cref="T:System.Version" /> object, or <see langword="null" />.</param>
		/// <returns>A signed integer that indicates the relative values of the two objects, as shown in the following table.  
		///   Return value  
		///
		///   Meaning  
		///
		///   Less than zero  
		///
		///   The current <see cref="T:System.Version" /> object is a version before <paramref name="value" />.  
		///
		///   Zero  
		///
		///   The current <see cref="T:System.Version" /> object is the same version as <paramref name="value" />.  
		///
		///   Greater than zero  
		///
		///   The current <see cref="T:System.Version" /> object is a version subsequent to <paramref name="value" />.  
		///
		///  -or-  
		///
		///  <paramref name="value" /> is <see langword="null" />.</returns>
		// Token: 0x0600130E RID: 4878 RVA: 0x0004D15C File Offset: 0x0004B35C
		public int CompareTo(Version value)
		{
			if (value == this)
			{
				return 0;
			}
			if (value == null)
			{
				return 1;
			}
			if (this._Major == value._Major)
			{
				if (this._Minor == value._Minor)
				{
					if (this._Build == value._Build)
					{
						if (this._Revision == value._Revision)
						{
							return 0;
						}
						if (this._Revision <= value._Revision)
						{
							return -1;
						}
						return 1;
					}
					else
					{
						if (this._Build <= value._Build)
						{
							return -1;
						}
						return 1;
					}
				}
				else
				{
					if (this._Minor <= value._Minor)
					{
						return -1;
					}
					return 1;
				}
			}
			else
			{
				if (this._Major <= value._Major)
				{
					return -1;
				}
				return 1;
			}
		}

		/// <summary>Returns a value indicating whether the current <see cref="T:System.Version" /> object is equal to a specified object.</summary>
		/// <param name="obj">An object to compare with the current <see cref="T:System.Version" /> object, or <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if the current <see cref="T:System.Version" /> object and <paramref name="obj" /> are both <see cref="T:System.Version" /> objects, and every component of the current <see cref="T:System.Version" /> object matches the corresponding component of <paramref name="obj" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600130F RID: 4879 RVA: 0x0004D1FB File Offset: 0x0004B3FB
		public override bool Equals(object obj)
		{
			return this.Equals(obj as Version);
		}

		/// <summary>Returns a value indicating whether the current <see cref="T:System.Version" /> object and a specified <see cref="T:System.Version" /> object represent the same value.</summary>
		/// <param name="obj">A <see cref="T:System.Version" /> object to compare to the current <see cref="T:System.Version" /> object, or <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if every component of the current <see cref="T:System.Version" /> object matches the corresponding component of the <paramref name="obj" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001310 RID: 4880 RVA: 0x0004D20C File Offset: 0x0004B40C
		public bool Equals(Version obj)
		{
			return obj == this || (obj != null && this._Major == obj._Major && this._Minor == obj._Minor && this._Build == obj._Build && this._Revision == obj._Revision);
		}

		/// <summary>Returns a hash code for the current <see cref="T:System.Version" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001311 RID: 4881 RVA: 0x0004D25C File Offset: 0x0004B45C
		public override int GetHashCode()
		{
			return 0 | (this._Major & 15) << 28 | (this._Minor & 255) << 20 | (this._Build & 255) << 12 | (this._Revision & 4095);
		}

		/// <summary>Converts the value of the current <see cref="T:System.Version" /> object to its equivalent <see cref="T:System.String" /> representation.</summary>
		/// <returns>The <see cref="T:System.String" /> representation of the values of the major, minor, build, and revision components of the current <see cref="T:System.Version" /> object, as depicted in the following format. Each component is separated by a period character ('.'). Square brackets ('[' and ']') indicate a component that will not appear in the return value if the component is not defined:  
		///  major.minor[.build[.revision]]  
		///  For example, if you create a <see cref="T:System.Version" /> object using the constructor Version(1,1), the returned string is "1.1". If you create a <see cref="T:System.Version" /> object using the constructor Version(1,3,4,2), the returned string is "1.3.4.2".</returns>
		// Token: 0x06001312 RID: 4882 RVA: 0x0004D299 File Offset: 0x0004B499
		public override string ToString()
		{
			return this.ToString(this.DefaultFormatFieldCount);
		}

		/// <summary>Converts the value of the current <see cref="T:System.Version" /> object to its equivalent <see cref="T:System.String" /> representation. A specified count indicates the number of components to return.</summary>
		/// <param name="fieldCount">The number of components to return. The <paramref name="fieldCount" /> ranges from 0 to 4.</param>
		/// <returns>The <see cref="T:System.String" /> representation of the values of the major, minor, build, and revision components of the current <see cref="T:System.Version" /> object, each separated by a period character ('.'). The <paramref name="fieldCount" /> parameter determines how many components are returned.  
		///   fieldCount  
		///
		///   Return Value  
		///
		///   0  
		///
		///   An empty string ("").  
		///
		///   1  
		///
		///   major  
		///
		///   2  
		///
		///   major.minor  
		///
		///   3  
		///
		///   major.minor.build  
		///
		///   4  
		///
		///   major.minor.build.revision  
		///
		///
		///
		///  For example, if you create <see cref="T:System.Version" /> object using the constructor Version(1,3,5), ToString(2) returns "1.3" and ToString(4) throws an exception.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fieldCount" /> is less than 0, or more than 4.  
		/// -or-  
		/// <paramref name="fieldCount" /> is more than the number of components defined in the current <see cref="T:System.Version" /> object.</exception>
		// Token: 0x06001313 RID: 4883 RVA: 0x0004D2A7 File Offset: 0x0004B4A7
		public string ToString(int fieldCount)
		{
			if (fieldCount == 0)
			{
				return string.Empty;
			}
			if (fieldCount != 1)
			{
				return StringBuilderCache.GetStringAndRelease(this.ToCachedStringBuilder(fieldCount));
			}
			return this._Major.ToString();
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x0004D2CE File Offset: 0x0004B4CE
		public bool TryFormat(Span<char> destination, out int charsWritten)
		{
			return this.TryFormat(destination, this.DefaultFormatFieldCount, out charsWritten);
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x0004D2E0 File Offset: 0x0004B4E0
		public bool TryFormat(Span<char> destination, int fieldCount, out int charsWritten)
		{
			if (fieldCount == 0)
			{
				charsWritten = 0;
				return true;
			}
			if (fieldCount == 1)
			{
				return this._Major.TryFormat(destination, out charsWritten, default(ReadOnlySpan<char>), null);
			}
			StringBuilder stringBuilder = this.ToCachedStringBuilder(fieldCount);
			if (stringBuilder.Length <= destination.Length)
			{
				stringBuilder.CopyTo(0, destination, stringBuilder.Length);
				StringBuilderCache.Release(stringBuilder);
				charsWritten = stringBuilder.Length;
				return true;
			}
			StringBuilderCache.Release(stringBuilder);
			charsWritten = 0;
			return false;
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x0004D350 File Offset: 0x0004B550
		bool ISpanFormattable.TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider provider)
		{
			return this.TryFormat(destination, out charsWritten);
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06001317 RID: 4887 RVA: 0x0004D35A File Offset: 0x0004B55A
		private int DefaultFormatFieldCount
		{
			get
			{
				if (this._Build == -1)
				{
					return 2;
				}
				if (this._Revision != -1)
				{
					return 4;
				}
				return 3;
			}
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x0004D374 File Offset: 0x0004B574
		private StringBuilder ToCachedStringBuilder(int fieldCount)
		{
			if (fieldCount == 2)
			{
				StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
				stringBuilder.Append(this._Major);
				stringBuilder.Append('.');
				stringBuilder.Append(this._Minor);
				return stringBuilder;
			}
			if (this._Build == -1)
			{
				throw new ArgumentException(SR.Format("Argument must be between {0} and {1}.", "0", "2"), "fieldCount");
			}
			if (fieldCount == 3)
			{
				StringBuilder stringBuilder2 = StringBuilderCache.Acquire(16);
				stringBuilder2.Append(this._Major);
				stringBuilder2.Append('.');
				stringBuilder2.Append(this._Minor);
				stringBuilder2.Append('.');
				stringBuilder2.Append(this._Build);
				return stringBuilder2;
			}
			if (this._Revision == -1)
			{
				throw new ArgumentException(SR.Format("Argument must be between {0} and {1}.", "0", "3"), "fieldCount");
			}
			if (fieldCount == 4)
			{
				StringBuilder stringBuilder3 = StringBuilderCache.Acquire(16);
				stringBuilder3.Append(this._Major);
				stringBuilder3.Append('.');
				stringBuilder3.Append(this._Minor);
				stringBuilder3.Append('.');
				stringBuilder3.Append(this._Build);
				stringBuilder3.Append('.');
				stringBuilder3.Append(this._Revision);
				return stringBuilder3;
			}
			throw new ArgumentException(SR.Format("Argument must be between {0} and {1}.", "0", "4"), "fieldCount");
		}

		/// <summary>Converts the string representation of a version number to an equivalent <see cref="T:System.Version" /> object.</summary>
		/// <param name="input">A string that contains a version number to convert.</param>
		/// <returns>An object that is equivalent to the version number specified in the <paramref name="input" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="input" /> has fewer than two or more than four version components.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">At least one component in <paramref name="input" /> is less than zero.</exception>
		/// <exception cref="T:System.FormatException">At least one component in <paramref name="input" /> is not an integer.</exception>
		/// <exception cref="T:System.OverflowException">At least one component in <paramref name="input" /> represents a number that is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06001319 RID: 4889 RVA: 0x0004D4BE File Offset: 0x0004B6BE
		public static Version Parse(string input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return Version.ParseVersion(input.AsSpan(), true);
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x0004D4DA File Offset: 0x0004B6DA
		public static Version Parse(ReadOnlySpan<char> input)
		{
			return Version.ParseVersion(input, true);
		}

		/// <summary>Tries to convert the string representation of a version number to an equivalent <see cref="T:System.Version" /> object, and returns a value that indicates whether the conversion succeeded.</summary>
		/// <param name="input">A string that contains a version number to convert.</param>
		/// <param name="result">When this method returns, contains the <see cref="T:System.Version" /> equivalent of the number that is contained in <paramref name="input" />, if the conversion succeeded. If <paramref name="input" /> is <see langword="null" />, <see cref="F:System.String.Empty" />, or if the conversion fails, <paramref name="result" /> is <see langword="null" /> when the method returns.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="input" /> parameter was converted successfully; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600131B RID: 4891 RVA: 0x0004D4E4 File Offset: 0x0004B6E4
		public static bool TryParse(string input, out Version result)
		{
			if (input == null)
			{
				result = null;
				return false;
			}
			Version v;
			result = (v = Version.ParseVersion(input.AsSpan(), false));
			return v != null;
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x0004D510 File Offset: 0x0004B710
		public static bool TryParse(ReadOnlySpan<char> input, out Version result)
		{
			Version v;
			result = (v = Version.ParseVersion(input, false));
			return v != null;
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x0004D530 File Offset: 0x0004B730
		private static Version ParseVersion(ReadOnlySpan<char> input, bool throwOnFailure)
		{
			int num = input.IndexOf('.');
			if (num < 0)
			{
				if (throwOnFailure)
				{
					throw new ArgumentException("Version string portion was too short or too long.", "input");
				}
				return null;
			}
			else
			{
				int num2 = -1;
				int num3 = input.Slice(num + 1).IndexOf('.');
				if (num3 != -1)
				{
					num3 += num + 1;
					num2 = input.Slice(num3 + 1).IndexOf('.');
					if (num2 != -1)
					{
						num2 += num3 + 1;
						if (input.Slice(num2 + 1).IndexOf('.') != -1)
						{
							if (throwOnFailure)
							{
								throw new ArgumentException("Version string portion was too short or too long.", "input");
							}
							return null;
						}
					}
				}
				int major;
				if (!Version.TryParseComponent(input.Slice(0, num), "input", throwOnFailure, out major))
				{
					return null;
				}
				if (num3 != -1)
				{
					int minor;
					if (!Version.TryParseComponent(input.Slice(num + 1, num3 - num - 1), "input", throwOnFailure, out minor))
					{
						return null;
					}
					if (num2 != -1)
					{
						int build;
						int revision;
						if (!Version.TryParseComponent(input.Slice(num3 + 1, num2 - num3 - 1), "build", throwOnFailure, out build) || !Version.TryParseComponent(input.Slice(num2 + 1), "revision", throwOnFailure, out revision))
						{
							return null;
						}
						return new Version(major, minor, build, revision);
					}
					else
					{
						int build;
						if (!Version.TryParseComponent(input.Slice(num3 + 1), "build", throwOnFailure, out build))
						{
							return null;
						}
						return new Version(major, minor, build);
					}
				}
				else
				{
					int minor;
					if (!Version.TryParseComponent(input.Slice(num + 1), "input", throwOnFailure, out minor))
					{
						return null;
					}
					return new Version(major, minor);
				}
			}
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x0004D698 File Offset: 0x0004B898
		private static bool TryParseComponent(ReadOnlySpan<char> component, string componentName, bool throwOnFailure, out int parsedComponent)
		{
			if (!throwOnFailure)
			{
				return int.TryParse(component, NumberStyles.Integer, CultureInfo.InvariantCulture, out parsedComponent) && parsedComponent >= 0;
			}
			if ((parsedComponent = int.Parse(component, NumberStyles.Integer, CultureInfo.InvariantCulture)) < 0)
			{
				throw new ArgumentOutOfRangeException(componentName, "Version's parameters must be greater than or equal to zero.");
			}
			return true;
		}

		/// <summary>Determines whether two specified <see cref="T:System.Version" /> objects are equal.</summary>
		/// <param name="v1">The first <see cref="T:System.Version" /> object.</param>
		/// <param name="v2">The second <see cref="T:System.Version" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="v1" /> equals <paramref name="v2" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600131F RID: 4895 RVA: 0x0004D6E3 File Offset: 0x0004B8E3
		public static bool operator ==(Version v1, Version v2)
		{
			if (v1 == null)
			{
				return v2 == null;
			}
			return v1.Equals(v2);
		}

		/// <summary>Determines whether two specified <see cref="T:System.Version" /> objects are not equal.</summary>
		/// <param name="v1">The first <see cref="T:System.Version" /> object.</param>
		/// <param name="v2">The second <see cref="T:System.Version" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="v1" /> does not equal <paramref name="v2" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001320 RID: 4896 RVA: 0x0004D6F4 File Offset: 0x0004B8F4
		public static bool operator !=(Version v1, Version v2)
		{
			return !(v1 == v2);
		}

		/// <summary>Determines whether the first specified <see cref="T:System.Version" /> object is less than the second specified <see cref="T:System.Version" /> object.</summary>
		/// <param name="v1">The first <see cref="T:System.Version" /> object.</param>
		/// <param name="v2">The second <see cref="T:System.Version" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="v1" /> is less than <paramref name="v2" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="v1" /> is <see langword="null" />.</exception>
		// Token: 0x06001321 RID: 4897 RVA: 0x0004D700 File Offset: 0x0004B900
		public static bool operator <(Version v1, Version v2)
		{
			if (v1 == null)
			{
				throw new ArgumentNullException("v1");
			}
			return v1.CompareTo(v2) < 0;
		}

		/// <summary>Determines whether the first specified <see cref="T:System.Version" /> object is less than or equal to the second <see cref="T:System.Version" /> object.</summary>
		/// <param name="v1">The first <see cref="T:System.Version" /> object.</param>
		/// <param name="v2">The second <see cref="T:System.Version" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="v1" /> is less than or equal to <paramref name="v2" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="v1" /> is <see langword="null" />.</exception>
		// Token: 0x06001322 RID: 4898 RVA: 0x0004D71A File Offset: 0x0004B91A
		public static bool operator <=(Version v1, Version v2)
		{
			if (v1 == null)
			{
				throw new ArgumentNullException("v1");
			}
			return v1.CompareTo(v2) <= 0;
		}

		/// <summary>Determines whether the first specified <see cref="T:System.Version" /> object is greater than the second specified <see cref="T:System.Version" /> object.</summary>
		/// <param name="v1">The first <see cref="T:System.Version" /> object.</param>
		/// <param name="v2">The second <see cref="T:System.Version" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="v1" /> is greater than <paramref name="v2" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001323 RID: 4899 RVA: 0x0004D737 File Offset: 0x0004B937
		public static bool operator >(Version v1, Version v2)
		{
			return v2 < v1;
		}

		/// <summary>Determines whether the first specified <see cref="T:System.Version" /> object is greater than or equal to the second specified <see cref="T:System.Version" /> object.</summary>
		/// <param name="v1">The first <see cref="T:System.Version" /> object.</param>
		/// <param name="v2">The second <see cref="T:System.Version" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="v1" /> is greater than or equal to <paramref name="v2" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001324 RID: 4900 RVA: 0x0004D740 File Offset: 0x0004B940
		public static bool operator >=(Version v1, Version v2)
		{
			return v2 <= v1;
		}

		// Token: 0x0400138A RID: 5002
		private readonly int _Major;

		// Token: 0x0400138B RID: 5003
		private readonly int _Minor;

		// Token: 0x0400138C RID: 5004
		private readonly int _Build = -1;

		// Token: 0x0400138D RID: 5005
		private readonly int _Revision = -1;
	}
}
