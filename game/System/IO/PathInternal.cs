using System;
using System.Runtime.CompilerServices;

namespace System.IO
{
	// Token: 0x020004F5 RID: 1269
	internal static class PathInternal
	{
		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x0600297C RID: 10620 RVA: 0x0008EB39 File Offset: 0x0008CD39
		internal static StringComparison StringComparison
		{
			get
			{
				if (!PathInternal.s_isCaseSensitive)
				{
					return StringComparison.OrdinalIgnoreCase;
				}
				return StringComparison.Ordinal;
			}
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x0600297D RID: 10621 RVA: 0x0008EB45 File Offset: 0x0008CD45
		internal static bool IsCaseSensitive
		{
			get
			{
				return PathInternal.s_isCaseSensitive;
			}
		}

		// Token: 0x0600297E RID: 10622 RVA: 0x0008EB4C File Offset: 0x0008CD4C
		private static bool GetIsCaseSensitive()
		{
			bool result;
			try
			{
				string text = Path.Combine(Path.GetTempPath(), "CASESENSITIVETEST" + Guid.NewGuid().ToString("N"));
				using (new FileStream(text, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.None, 4096, FileOptions.DeleteOnClose))
				{
					result = !File.Exists(text.ToLowerInvariant());
				}
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600297F RID: 10623 RVA: 0x0006E763 File Offset: 0x0006C963
		internal static bool IsValidDriveChar(char value)
		{
			return (value >= 'A' && value <= 'Z') || (value >= 'a' && value <= 'z');
		}

		// Token: 0x06002980 RID: 10624 RVA: 0x0008EBD4 File Offset: 0x0008CDD4
		private static bool EndsWithPeriodOrSpace(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				return false;
			}
			char c = path[path.Length - 1];
			return c == ' ' || c == '.';
		}

		// Token: 0x06002981 RID: 10625 RVA: 0x0008EC06 File Offset: 0x0008CE06
		internal static string EnsureExtendedPrefixIfNeeded(string path)
		{
			if (path != null && (path.Length >= 260 || PathInternal.EndsWithPeriodOrSpace(path)))
			{
				return PathInternal.EnsureExtendedPrefix(path);
			}
			return path;
		}

		// Token: 0x06002982 RID: 10626 RVA: 0x0008EC28 File Offset: 0x0008CE28
		internal static string EnsureExtendedPrefix(string path)
		{
			if (PathInternal.IsPartiallyQualified(path) || PathInternal.IsDevice(path))
			{
				return path;
			}
			if (path.StartsWith("\\\\", StringComparison.OrdinalIgnoreCase))
			{
				return path.Insert(2, "?\\UNC\\");
			}
			return "\\\\?\\" + path;
		}

		// Token: 0x06002983 RID: 10627 RVA: 0x0008EC64 File Offset: 0x0008CE64
		internal static bool IsDevice(string path)
		{
			return PathInternal.IsExtended(path) || (path.Length >= 4 && PathInternal.IsDirectorySeparator(path[0]) && PathInternal.IsDirectorySeparator(path[1]) && (path[2] == '.' || path[2] == '?') && PathInternal.IsDirectorySeparator(path[3]));
		}

		// Token: 0x06002984 RID: 10628 RVA: 0x0008ECC4 File Offset: 0x0008CEC4
		internal static bool IsExtended(string path)
		{
			return path.Length >= 4 && path[0] == '\\' && (path[1] == '\\' || path[1] == '?') && path[2] == '?' && path[3] == '\\';
		}

		// Token: 0x06002985 RID: 10629 RVA: 0x0008ED14 File Offset: 0x0008CF14
		internal unsafe static int GetRootLength(ReadOnlySpan<char> path)
		{
			int i = 0;
			int num = 2;
			int num2 = 2;
			bool flag = path.StartsWith("\\\\?\\");
			bool flag2 = path.StartsWith("\\\\?\\UNC\\");
			if (flag)
			{
				if (flag2)
				{
					num2 = "\\\\?\\UNC\\".Length;
				}
				else
				{
					num += "\\\\?\\".Length;
				}
			}
			if ((!flag || flag2) && path.Length > 0 && PathInternal.IsDirectorySeparator((char)(*path[0])))
			{
				i = 1;
				if (flag2 || (path.Length > 1 && PathInternal.IsDirectorySeparator((char)(*path[1]))))
				{
					i = num2;
					int num3 = 2;
					while (i < path.Length)
					{
						if (PathInternal.IsDirectorySeparator((char)(*path[i])) && --num3 <= 0)
						{
							break;
						}
						i++;
					}
				}
			}
			else if (path.Length >= num && *path[num - 1] == (ushort)Path.VolumeSeparatorChar)
			{
				i = num;
				if (path.Length >= num + 1 && PathInternal.IsDirectorySeparator((char)(*path[num])))
				{
					i++;
				}
			}
			return i;
		}

		// Token: 0x06002986 RID: 10630 RVA: 0x0008EE18 File Offset: 0x0008D018
		internal static bool IsPartiallyQualified(string path)
		{
			if (path.Length < 2)
			{
				return true;
			}
			if (PathInternal.IsDirectorySeparator(path[0]))
			{
				return path[1] != '?' && !PathInternal.IsDirectorySeparator(path[1]);
			}
			return path.Length < 3 || path[1] != Path.VolumeSeparatorChar || !PathInternal.IsDirectorySeparator(path[2]) || !PathInternal.IsValidDriveChar(path[0]);
		}

		// Token: 0x06002987 RID: 10631 RVA: 0x0008EE91 File Offset: 0x0008D091
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool IsDirectorySeparator(char c)
		{
			return c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar;
		}

		// Token: 0x06002988 RID: 10632 RVA: 0x0008EEA5 File Offset: 0x0008D0A5
		// Note: this type is marked as 'beforefieldinit'.
		static PathInternal()
		{
		}

		// Token: 0x040015EE RID: 5614
		private static readonly bool s_isCaseSensitive = PathInternal.GetIsCaseSensitive();

		// Token: 0x040015EF RID: 5615
		internal const string ExtendedDevicePathPrefix = "\\\\?\\";

		// Token: 0x040015F0 RID: 5616
		internal const string UncPathPrefix = "\\\\";

		// Token: 0x040015F1 RID: 5617
		internal const string UncDevicePrefixToInsert = "?\\UNC\\";

		// Token: 0x040015F2 RID: 5618
		internal const string UncExtendedPathPrefix = "\\\\?\\UNC\\";

		// Token: 0x040015F3 RID: 5619
		internal const string DevicePathPrefix = "\\\\.\\";

		// Token: 0x040015F4 RID: 5620
		internal const int MaxShortPath = 260;

		// Token: 0x040015F5 RID: 5621
		internal const int DevicePrefixLength = 4;
	}
}
