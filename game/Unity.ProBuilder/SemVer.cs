using System;
using System.Text;
using System.Text.RegularExpressions;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000050 RID: 80
	[Serializable]
	internal sealed class SemVer : IEquatable<SemVer>, IComparable<SemVer>, IComparable
	{
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0001C350 File Offset: 0x0001A550
		public int major
		{
			get
			{
				return this.m_Major;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000301 RID: 769 RVA: 0x0001C358 File Offset: 0x0001A558
		public int minor
		{
			get
			{
				return this.m_Minor;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0001C360 File Offset: 0x0001A560
		public int patch
		{
			get
			{
				return this.m_Patch;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000303 RID: 771 RVA: 0x0001C368 File Offset: 0x0001A568
		public int build
		{
			get
			{
				return this.m_Build;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000304 RID: 772 RVA: 0x0001C370 File Offset: 0x0001A570
		public string type
		{
			get
			{
				if (this.m_Type == null)
				{
					return "";
				}
				return this.m_Type;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000305 RID: 773 RVA: 0x0001C386 File Offset: 0x0001A586
		public string metadata
		{
			get
			{
				if (this.m_Metadata == null)
				{
					return "";
				}
				return this.m_Metadata;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000306 RID: 774 RVA: 0x0001C39C File Offset: 0x0001A59C
		public string date
		{
			get
			{
				if (this.m_Date == null)
				{
					return "";
				}
				return this.m_Date;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000307 RID: 775 RVA: 0x0001C3B2 File Offset: 0x0001A5B2
		public SemVer MajorMinorPatch
		{
			get
			{
				return new SemVer(this.major, this.minor, this.patch, -1, null, null, null);
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0001C3D0 File Offset: 0x0001A5D0
		public SemVer()
		{
			this.m_Major = 0;
			this.m_Minor = 0;
			this.m_Patch = 0;
			this.m_Build = -1;
			this.m_Type = null;
			this.m_Date = null;
			this.m_Metadata = null;
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0001C430 File Offset: 0x0001A630
		public SemVer(string formatted, string date = null)
		{
			this.m_Metadata = formatted;
			this.m_Date = date;
			SemVer semVer;
			if (SemVer.TryGetVersionInfo(formatted, out semVer))
			{
				this.m_Major = semVer.m_Major;
				this.m_Minor = semVer.m_Minor;
				this.m_Patch = semVer.m_Patch;
				this.m_Build = semVer.m_Build;
				this.m_Type = semVer.m_Type;
				this.m_Metadata = semVer.metadata;
			}
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0001C4C0 File Offset: 0x0001A6C0
		public SemVer(int major, int minor, int patch, int build = -1, string type = null, string date = null, string metadata = null)
		{
			this.m_Major = major;
			this.m_Minor = minor;
			this.m_Patch = patch;
			this.m_Build = build;
			this.m_Type = type;
			this.m_Metadata = metadata;
			this.m_Date = date;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0001C524 File Offset: 0x0001A724
		public bool IsValid()
		{
			return this.major != -1 && this.minor != -1 && this.patch != -1;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0001C546 File Offset: 0x0001A746
		public override bool Equals(object o)
		{
			return o is SemVer && this.Equals((SemVer)o);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0001C560 File Offset: 0x0001A760
		public override int GetHashCode()
		{
			int num = 13;
			if (this.IsValid())
			{
				num = num * 7 + this.major.GetHashCode();
				num = num * 7 + this.minor.GetHashCode();
				num = num * 7 + this.patch.GetHashCode();
				num = num * 7 + this.build.GetHashCode();
				return num * 7 + this.type.GetHashCode();
			}
			if (!string.IsNullOrEmpty(this.metadata))
			{
				return base.GetHashCode();
			}
			return this.metadata.GetHashCode();
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0001C5F8 File Offset: 0x0001A7F8
		public bool Equals(SemVer version)
		{
			if (version == null)
			{
				return false;
			}
			if (this.IsValid() != version.IsValid())
			{
				return false;
			}
			if (this.IsValid())
			{
				return this.major == version.major && this.minor == version.minor && this.patch == version.patch && this.type.Equals(version.type) && this.build.Equals(version.build);
			}
			return !string.IsNullOrEmpty(this.metadata) && !string.IsNullOrEmpty(version.metadata) && this.metadata.Equals(version.metadata);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0001C6A3 File Offset: 0x0001A8A3
		public int CompareTo(object obj)
		{
			return this.CompareTo(obj as SemVer);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0001C6B1 File Offset: 0x0001A8B1
		private static int WrapNoValue(int value)
		{
			if (value >= 0)
			{
				return value;
			}
			return int.MaxValue;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0001C6C0 File Offset: 0x0001A8C0
		public int CompareTo(SemVer version)
		{
			if (version == null)
			{
				return 1;
			}
			if (this.Equals(version))
			{
				return 0;
			}
			if (this.major > version.major)
			{
				return 1;
			}
			if (this.major < version.major)
			{
				return -1;
			}
			if (this.minor > version.minor)
			{
				return 1;
			}
			if (this.minor < version.minor)
			{
				return -1;
			}
			if (SemVer.WrapNoValue(this.patch) > SemVer.WrapNoValue(version.patch))
			{
				return 1;
			}
			if (SemVer.WrapNoValue(this.patch) < SemVer.WrapNoValue(version.patch))
			{
				return -1;
			}
			if (string.IsNullOrEmpty(this.type) && !string.IsNullOrEmpty(version.type))
			{
				return 1;
			}
			if (!string.IsNullOrEmpty(this.type) && string.IsNullOrEmpty(version.type))
			{
				return -1;
			}
			if (SemVer.WrapNoValue(this.build) > SemVer.WrapNoValue(version.build))
			{
				return 1;
			}
			if (SemVer.WrapNoValue(this.build) < SemVer.WrapNoValue(version.build))
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0001C7BE File Offset: 0x0001A9BE
		public static bool operator ==(SemVer left, SemVer right)
		{
			if (left == null)
			{
				return right == null;
			}
			return left.Equals(right);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0001C7CF File Offset: 0x0001A9CF
		public static bool operator !=(SemVer left, SemVer right)
		{
			return !(left == right);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0001C7DB File Offset: 0x0001A9DB
		public static bool operator <(SemVer left, SemVer right)
		{
			if (left == null)
			{
				return right != null;
			}
			return left.CompareTo(right) < 0;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0001C7EF File Offset: 0x0001A9EF
		public static bool operator >(SemVer left, SemVer right)
		{
			return left != null && left.CompareTo(right) > 0;
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0001C800 File Offset: 0x0001AA00
		public static bool operator <=(SemVer left, SemVer right)
		{
			return left == right || left < right;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0001C814 File Offset: 0x0001AA14
		public static bool operator >=(SemVer left, SemVer right)
		{
			return left == right || left > right;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0001C828 File Offset: 0x0001AA28
		public string ToString(string format)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			foreach (char c in format.ToCharArray())
			{
				if (flag)
				{
					stringBuilder.Append(c);
					flag = false;
				}
				else if (c == '\\')
				{
					flag = true;
				}
				else if (c == 'M')
				{
					stringBuilder.Append(this.major);
				}
				else if (c == 'm')
				{
					stringBuilder.Append(this.minor);
				}
				else if (c == 'p')
				{
					stringBuilder.Append(this.patch);
				}
				else if (c == 'b')
				{
					stringBuilder.Append(this.build);
				}
				else if (c == 'T' || c == 't')
				{
					stringBuilder.Append(this.type);
				}
				else if (c == 'd')
				{
					stringBuilder.Append(this.date);
				}
				else if (c == 'D')
				{
					stringBuilder.Append(this.metadata);
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0001C928 File Offset: 0x0001AB28
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.ToString("M.m.p"));
			if (!string.IsNullOrEmpty(this.type))
			{
				stringBuilder.Append("-");
				stringBuilder.Append(this.type);
				if (this.build > -1)
				{
					stringBuilder.Append(".");
					stringBuilder.Append(this.build.ToString());
				}
			}
			if (!string.IsNullOrEmpty(this.date))
			{
				stringBuilder.Append(" ");
				stringBuilder.Append(this.date);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0001C9CC File Offset: 0x0001ABCC
		public static bool TryGetVersionInfo(string input, out SemVer version)
		{
			version = new SemVer();
			bool result = false;
			try
			{
				Match match = Regex.Match(input, "^([0-9]+\\.[0-9]+\\.[0-9]+)");
				if (!match.Success)
				{
					return false;
				}
				string[] array = match.Value.Split('.', StringSplitOptions.None);
				int.TryParse(array[0], out version.m_Major);
				int.TryParse(array[1], out version.m_Minor);
				int.TryParse(array[2], out version.m_Patch);
				result = true;
				Match match2 = Regex.Match(input, "(?i)(?<=\\-)[a-z0-9\\-]+");
				if (match2.Success)
				{
					version.m_Type = match2.Value;
				}
				Match match3 = Regex.Match(input, "(?i)(?<=\\-[a-z0-9\\-]+\\.)[0-9]+");
				version.m_Build = (match3.Success ? SemVer.GetBuildNumber(match3.Value) : -1);
				Match match4 = Regex.Match(input, "(?<=\\+).+");
				if (match4.Success)
				{
					version.m_Metadata = match4.Value;
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0001CAC8 File Offset: 0x0001ACC8
		private static int GetBuildNumber(string input)
		{
			Match match = Regex.Match(input, "[0-9]+");
			int result = 0;
			if (match.Success && int.TryParse(match.Value, out result))
			{
				return result;
			}
			return -1;
		}

		// Token: 0x040001D5 RID: 469
		[SerializeField]
		private int m_Major = -1;

		// Token: 0x040001D6 RID: 470
		[SerializeField]
		private int m_Minor = -1;

		// Token: 0x040001D7 RID: 471
		[SerializeField]
		private int m_Patch = -1;

		// Token: 0x040001D8 RID: 472
		[SerializeField]
		private int m_Build = -1;

		// Token: 0x040001D9 RID: 473
		[SerializeField]
		private string m_Type;

		// Token: 0x040001DA RID: 474
		[SerializeField]
		private string m_Metadata;

		// Token: 0x040001DB RID: 475
		[SerializeField]
		private string m_Date;

		// Token: 0x040001DC RID: 476
		public const string DefaultStringFormat = "M.m.p-t.b";
	}
}
