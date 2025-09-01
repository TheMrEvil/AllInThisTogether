using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.IO.Enumeration
{
	// Token: 0x02000B7E RID: 2942
	internal static class FileSystemEnumerableFactory
	{
		// Token: 0x06006B1F RID: 27423 RVA: 0x0016E36C File Offset: 0x0016C56C
		internal static void NormalizeInputs(ref string directory, ref string expression, EnumerationOptions options)
		{
			if (Path.IsPathRooted(expression))
			{
				throw new ArgumentException("Second path fragment must not be a drive or UNC name.", "expression");
			}
			ReadOnlySpan<char> directoryName = Path.GetDirectoryName(expression.AsSpan());
			if (directoryName.Length != 0)
			{
				directory = Path.Join(directory, directoryName);
				expression = expression.Substring(directoryName.Length + 1);
			}
			MatchType matchType = options.MatchType;
			if (matchType == MatchType.Simple)
			{
				return;
			}
			if (matchType != MatchType.Win32)
			{
				throw new ArgumentOutOfRangeException("options");
			}
			if (string.IsNullOrEmpty(expression) || expression == "." || expression == "*.*")
			{
				expression = "*";
				return;
			}
			if (Path.DirectorySeparatorChar != '\\' && expression.IndexOfAny(FileSystemEnumerableFactory.s_unixEscapeChars) != -1)
			{
				expression = expression.Replace("\\", "\\\\");
				expression = expression.Replace("\"", "\\\"");
				expression = expression.Replace(">", "\\>");
				expression = expression.Replace("<", "\\<");
			}
			expression = FileSystemName.TranslateWin32Expression(expression);
		}

		// Token: 0x06006B20 RID: 27424 RVA: 0x0016E484 File Offset: 0x0016C684
		private static bool MatchesPattern(string expression, ReadOnlySpan<char> name, EnumerationOptions options)
		{
			bool ignoreCase = (options.MatchCasing == MatchCasing.PlatformDefault && !PathInternal.IsCaseSensitive) || options.MatchCasing == MatchCasing.CaseInsensitive;
			MatchType matchType = options.MatchType;
			if (matchType == MatchType.Simple)
			{
				return FileSystemName.MatchesSimpleExpression(expression, name, ignoreCase);
			}
			if (matchType != MatchType.Win32)
			{
				throw new ArgumentOutOfRangeException("options");
			}
			return FileSystemName.MatchesWin32Expression(expression, name, ignoreCase);
		}

		// Token: 0x06006B21 RID: 27425 RVA: 0x0016E4E4 File Offset: 0x0016C6E4
		internal static IEnumerable<string> UserFiles(string directory, string expression, EnumerationOptions options)
		{
			return new FileSystemEnumerable<string>(directory, delegate(ref FileSystemEntry entry)
			{
				return entry.ToSpecifiedFullPath();
			}, options)
			{
				ShouldIncludePredicate = delegate(ref FileSystemEntry entry)
				{
					return !entry.IsDirectory && FileSystemEnumerableFactory.MatchesPattern(expression, entry.FileName, options);
				}
			};
		}

		// Token: 0x06006B22 RID: 27426 RVA: 0x0016E544 File Offset: 0x0016C744
		internal static IEnumerable<string> UserDirectories(string directory, string expression, EnumerationOptions options)
		{
			return new FileSystemEnumerable<string>(directory, delegate(ref FileSystemEntry entry)
			{
				return entry.ToSpecifiedFullPath();
			}, options)
			{
				ShouldIncludePredicate = delegate(ref FileSystemEntry entry)
				{
					return entry.IsDirectory && FileSystemEnumerableFactory.MatchesPattern(expression, entry.FileName, options);
				}
			};
		}

		// Token: 0x06006B23 RID: 27427 RVA: 0x0016E5A4 File Offset: 0x0016C7A4
		internal static IEnumerable<string> UserEntries(string directory, string expression, EnumerationOptions options)
		{
			return new FileSystemEnumerable<string>(directory, delegate(ref FileSystemEntry entry)
			{
				return entry.ToSpecifiedFullPath();
			}, options)
			{
				ShouldIncludePredicate = delegate(ref FileSystemEntry entry)
				{
					return FileSystemEnumerableFactory.MatchesPattern(expression, entry.FileName, options);
				}
			};
		}

		// Token: 0x06006B24 RID: 27428 RVA: 0x0016E604 File Offset: 0x0016C804
		internal static IEnumerable<FileInfo> FileInfos(string directory, string expression, EnumerationOptions options)
		{
			return new FileSystemEnumerable<FileInfo>(directory, delegate(ref FileSystemEntry entry)
			{
				return (FileInfo)entry.ToFileSystemInfo();
			}, options)
			{
				ShouldIncludePredicate = delegate(ref FileSystemEntry entry)
				{
					return !entry.IsDirectory && FileSystemEnumerableFactory.MatchesPattern(expression, entry.FileName, options);
				}
			};
		}

		// Token: 0x06006B25 RID: 27429 RVA: 0x0016E664 File Offset: 0x0016C864
		internal static IEnumerable<DirectoryInfo> DirectoryInfos(string directory, string expression, EnumerationOptions options)
		{
			return new FileSystemEnumerable<DirectoryInfo>(directory, delegate(ref FileSystemEntry entry)
			{
				return (DirectoryInfo)entry.ToFileSystemInfo();
			}, options)
			{
				ShouldIncludePredicate = delegate(ref FileSystemEntry entry)
				{
					return entry.IsDirectory && FileSystemEnumerableFactory.MatchesPattern(expression, entry.FileName, options);
				}
			};
		}

		// Token: 0x06006B26 RID: 27430 RVA: 0x0016E6C4 File Offset: 0x0016C8C4
		internal static IEnumerable<FileSystemInfo> FileSystemInfos(string directory, string expression, EnumerationOptions options)
		{
			return new FileSystemEnumerable<FileSystemInfo>(directory, delegate(ref FileSystemEntry entry)
			{
				return entry.ToFileSystemInfo();
			}, options)
			{
				ShouldIncludePredicate = delegate(ref FileSystemEntry entry)
				{
					return FileSystemEnumerableFactory.MatchesPattern(expression, entry.FileName, options);
				}
			};
		}

		// Token: 0x06006B27 RID: 27431 RVA: 0x0016E722 File Offset: 0x0016C922
		// Note: this type is marked as 'beforefieldinit'.
		static FileSystemEnumerableFactory()
		{
		}

		// Token: 0x04003DB3 RID: 15795
		private static readonly char[] s_unixEscapeChars = new char[]
		{
			'\\',
			'"',
			'<',
			'>'
		};

		// Token: 0x02000B7F RID: 2943
		[CompilerGenerated]
		private sealed class <>c__DisplayClass3_0
		{
			// Token: 0x06006B28 RID: 27432 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c__DisplayClass3_0()
			{
			}

			// Token: 0x06006B29 RID: 27433 RVA: 0x0016E73A File Offset: 0x0016C93A
			internal bool <UserFiles>b__1(ref FileSystemEntry entry)
			{
				return !entry.IsDirectory && FileSystemEnumerableFactory.MatchesPattern(this.expression, entry.FileName, this.options);
			}

			// Token: 0x04003DB4 RID: 15796
			public string expression;

			// Token: 0x04003DB5 RID: 15797
			public EnumerationOptions options;
		}

		// Token: 0x02000B80 RID: 2944
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06006B2A RID: 27434 RVA: 0x0016E75D File Offset: 0x0016C95D
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06006B2B RID: 27435 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c()
			{
			}

			// Token: 0x06006B2C RID: 27436 RVA: 0x0016E769 File Offset: 0x0016C969
			internal string <UserFiles>b__3_0(ref FileSystemEntry entry)
			{
				return entry.ToSpecifiedFullPath();
			}

			// Token: 0x06006B2D RID: 27437 RVA: 0x0016E769 File Offset: 0x0016C969
			internal string <UserDirectories>b__4_0(ref FileSystemEntry entry)
			{
				return entry.ToSpecifiedFullPath();
			}

			// Token: 0x06006B2E RID: 27438 RVA: 0x0016E769 File Offset: 0x0016C969
			internal string <UserEntries>b__5_0(ref FileSystemEntry entry)
			{
				return entry.ToSpecifiedFullPath();
			}

			// Token: 0x06006B2F RID: 27439 RVA: 0x0016E771 File Offset: 0x0016C971
			internal FileInfo <FileInfos>b__6_0(ref FileSystemEntry entry)
			{
				return (FileInfo)entry.ToFileSystemInfo();
			}

			// Token: 0x06006B30 RID: 27440 RVA: 0x0016E77E File Offset: 0x0016C97E
			internal DirectoryInfo <DirectoryInfos>b__7_0(ref FileSystemEntry entry)
			{
				return (DirectoryInfo)entry.ToFileSystemInfo();
			}

			// Token: 0x06006B31 RID: 27441 RVA: 0x0016E78B File Offset: 0x0016C98B
			internal FileSystemInfo <FileSystemInfos>b__8_0(ref FileSystemEntry entry)
			{
				return entry.ToFileSystemInfo();
			}

			// Token: 0x04003DB6 RID: 15798
			public static readonly FileSystemEnumerableFactory.<>c <>9 = new FileSystemEnumerableFactory.<>c();

			// Token: 0x04003DB7 RID: 15799
			public static FileSystemEnumerable<string>.FindTransform <>9__3_0;

			// Token: 0x04003DB8 RID: 15800
			public static FileSystemEnumerable<string>.FindTransform <>9__4_0;

			// Token: 0x04003DB9 RID: 15801
			public static FileSystemEnumerable<string>.FindTransform <>9__5_0;

			// Token: 0x04003DBA RID: 15802
			public static FileSystemEnumerable<FileInfo>.FindTransform <>9__6_0;

			// Token: 0x04003DBB RID: 15803
			public static FileSystemEnumerable<DirectoryInfo>.FindTransform <>9__7_0;

			// Token: 0x04003DBC RID: 15804
			public static FileSystemEnumerable<FileSystemInfo>.FindTransform <>9__8_0;
		}

		// Token: 0x02000B81 RID: 2945
		[CompilerGenerated]
		private sealed class <>c__DisplayClass4_0
		{
			// Token: 0x06006B32 RID: 27442 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c__DisplayClass4_0()
			{
			}

			// Token: 0x06006B33 RID: 27443 RVA: 0x0016E793 File Offset: 0x0016C993
			internal bool <UserDirectories>b__1(ref FileSystemEntry entry)
			{
				return entry.IsDirectory && FileSystemEnumerableFactory.MatchesPattern(this.expression, entry.FileName, this.options);
			}

			// Token: 0x04003DBD RID: 15805
			public string expression;

			// Token: 0x04003DBE RID: 15806
			public EnumerationOptions options;
		}

		// Token: 0x02000B82 RID: 2946
		[CompilerGenerated]
		private sealed class <>c__DisplayClass5_0
		{
			// Token: 0x06006B34 RID: 27444 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c__DisplayClass5_0()
			{
			}

			// Token: 0x06006B35 RID: 27445 RVA: 0x0016E7B6 File Offset: 0x0016C9B6
			internal bool <UserEntries>b__1(ref FileSystemEntry entry)
			{
				return FileSystemEnumerableFactory.MatchesPattern(this.expression, entry.FileName, this.options);
			}

			// Token: 0x04003DBF RID: 15807
			public string expression;

			// Token: 0x04003DC0 RID: 15808
			public EnumerationOptions options;
		}

		// Token: 0x02000B83 RID: 2947
		[CompilerGenerated]
		private sealed class <>c__DisplayClass6_0
		{
			// Token: 0x06006B36 RID: 27446 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c__DisplayClass6_0()
			{
			}

			// Token: 0x06006B37 RID: 27447 RVA: 0x0016E7CF File Offset: 0x0016C9CF
			internal bool <FileInfos>b__1(ref FileSystemEntry entry)
			{
				return !entry.IsDirectory && FileSystemEnumerableFactory.MatchesPattern(this.expression, entry.FileName, this.options);
			}

			// Token: 0x04003DC1 RID: 15809
			public string expression;

			// Token: 0x04003DC2 RID: 15810
			public EnumerationOptions options;
		}

		// Token: 0x02000B84 RID: 2948
		[CompilerGenerated]
		private sealed class <>c__DisplayClass7_0
		{
			// Token: 0x06006B38 RID: 27448 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c__DisplayClass7_0()
			{
			}

			// Token: 0x06006B39 RID: 27449 RVA: 0x0016E7F2 File Offset: 0x0016C9F2
			internal bool <DirectoryInfos>b__1(ref FileSystemEntry entry)
			{
				return entry.IsDirectory && FileSystemEnumerableFactory.MatchesPattern(this.expression, entry.FileName, this.options);
			}

			// Token: 0x04003DC3 RID: 15811
			public string expression;

			// Token: 0x04003DC4 RID: 15812
			public EnumerationOptions options;
		}

		// Token: 0x02000B85 RID: 2949
		[CompilerGenerated]
		private sealed class <>c__DisplayClass8_0
		{
			// Token: 0x06006B3A RID: 27450 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c__DisplayClass8_0()
			{
			}

			// Token: 0x06006B3B RID: 27451 RVA: 0x0016E815 File Offset: 0x0016CA15
			internal bool <FileSystemInfos>b__1(ref FileSystemEntry entry)
			{
				return FileSystemEnumerableFactory.MatchesPattern(this.expression, entry.FileName, this.options);
			}

			// Token: 0x04003DC5 RID: 15813
			public string expression;

			// Token: 0x04003DC6 RID: 15814
			public EnumerationOptions options;
		}
	}
}
