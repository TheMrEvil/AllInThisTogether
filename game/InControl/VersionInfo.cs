using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000072 RID: 114
	[Serializable]
	public struct VersionInfo : IComparable<VersionInfo>
	{
		// Token: 0x06000581 RID: 1409 RVA: 0x00013237 File Offset: 0x00011437
		public VersionInfo(int major, int minor, int patch, int build)
		{
			this.major = major;
			this.minor = minor;
			this.patch = patch;
			this.build = build;
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00013258 File Offset: 0x00011458
		public static VersionInfo InControlVersion()
		{
			return new VersionInfo
			{
				major = 1,
				minor = 8,
				patch = 9,
				build = 9376
			};
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00013294 File Offset: 0x00011494
		public static VersionInfo UnityVersion()
		{
			Match match = Regex.Match(Application.unityVersion, "^(\\d+)\\.(\\d+)\\.(\\d+)[a-zA-Z](\\d+)");
			return new VersionInfo
			{
				major = Convert.ToInt32(match.Groups[1].Value),
				minor = Convert.ToInt32(match.Groups[2].Value),
				patch = Convert.ToInt32(match.Groups[3].Value),
				build = Convert.ToInt32(match.Groups[4].Value)
			};
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x0001332E File Offset: 0x0001152E
		public static VersionInfo Min
		{
			get
			{
				return new VersionInfo(int.MinValue, int.MinValue, int.MinValue, int.MinValue);
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x00013349 File Offset: 0x00011549
		public static VersionInfo Max
		{
			get
			{
				return new VersionInfo(int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue);
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x00013364 File Offset: 0x00011564
		public VersionInfo Next
		{
			get
			{
				return new VersionInfo(this.major, this.minor, this.patch, this.build + 1);
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x00013385 File Offset: 0x00011585
		public int Build
		{
			get
			{
				return this.build;
			}
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00013390 File Offset: 0x00011590
		public int CompareTo(VersionInfo other)
		{
			if (this.major < other.major)
			{
				return -1;
			}
			if (this.major > other.major)
			{
				return 1;
			}
			if (this.minor < other.minor)
			{
				return -1;
			}
			if (this.minor > other.minor)
			{
				return 1;
			}
			if (this.patch < other.patch)
			{
				return -1;
			}
			if (this.patch > other.patch)
			{
				return 1;
			}
			if (this.build < other.build)
			{
				return -1;
			}
			if (this.build > other.build)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0001341E File Offset: 0x0001161E
		public static bool operator ==(VersionInfo a, VersionInfo b)
		{
			return a.CompareTo(b) == 0;
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0001342B File Offset: 0x0001162B
		public static bool operator !=(VersionInfo a, VersionInfo b)
		{
			return a.CompareTo(b) != 0;
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00013438 File Offset: 0x00011638
		public static bool operator <=(VersionInfo a, VersionInfo b)
		{
			return a.CompareTo(b) <= 0;
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00013448 File Offset: 0x00011648
		public static bool operator >=(VersionInfo a, VersionInfo b)
		{
			return a.CompareTo(b) >= 0;
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00013458 File Offset: 0x00011658
		public static bool operator <(VersionInfo a, VersionInfo b)
		{
			return a.CompareTo(b) < 0;
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00013465 File Offset: 0x00011665
		public static bool operator >(VersionInfo a, VersionInfo b)
		{
			return a.CompareTo(b) > 0;
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00013472 File Offset: 0x00011672
		public override bool Equals(object other)
		{
			return other is VersionInfo && this == (VersionInfo)other;
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0001348F File Offset: 0x0001168F
		public override int GetHashCode()
		{
			return this.major.GetHashCode() ^ this.minor.GetHashCode() ^ this.patch.GetHashCode() ^ this.build.GetHashCode();
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x000134C0 File Offset: 0x000116C0
		public override string ToString()
		{
			if (this.build == 0)
			{
				return string.Format("{0}.{1}.{2}", this.major, this.minor, this.patch);
			}
			return string.Format("{0}.{1}.{2} build {3}", new object[]
			{
				this.major,
				this.minor,
				this.patch,
				this.build
			});
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0001354C File Offset: 0x0001174C
		public string ToShortString()
		{
			if (this.build == 0)
			{
				return string.Format("{0}.{1}.{2}", this.major, this.minor, this.patch);
			}
			return string.Format("{0}.{1}.{2}b{3}", new object[]
			{
				this.major,
				this.minor,
				this.patch,
				this.build
			});
		}

		// Token: 0x0400041C RID: 1052
		[SerializeField]
		private int major;

		// Token: 0x0400041D RID: 1053
		[SerializeField]
		private int minor;

		// Token: 0x0400041E RID: 1054
		[SerializeField]
		private int patch;

		// Token: 0x0400041F RID: 1055
		[SerializeField]
		private int build;
	}
}
