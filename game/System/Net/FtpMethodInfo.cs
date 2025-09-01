using System;
using System.Globalization;

namespace System.Net
{
	// Token: 0x0200058D RID: 1421
	internal class FtpMethodInfo
	{
		// Token: 0x06002E05 RID: 11781 RVA: 0x0009EAC8 File Offset: 0x0009CCC8
		internal FtpMethodInfo(string method, FtpOperation operation, FtpMethodFlags flags, string httpCommand)
		{
			this.Method = method;
			this.Operation = operation;
			this.Flags = flags;
			this.HttpCommand = httpCommand;
		}

		// Token: 0x06002E06 RID: 11782 RVA: 0x0009EAED File Offset: 0x0009CCED
		internal bool HasFlag(FtpMethodFlags flags)
		{
			return (this.Flags & flags) > FtpMethodFlags.None;
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06002E07 RID: 11783 RVA: 0x0009EAFA File Offset: 0x0009CCFA
		internal bool IsCommandOnly
		{
			get
			{
				return (this.Flags & (FtpMethodFlags.IsDownload | FtpMethodFlags.IsUpload)) == FtpMethodFlags.None;
			}
		}

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06002E08 RID: 11784 RVA: 0x0009EB07 File Offset: 0x0009CD07
		internal bool IsUpload
		{
			get
			{
				return (this.Flags & FtpMethodFlags.IsUpload) > FtpMethodFlags.None;
			}
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06002E09 RID: 11785 RVA: 0x0009EB14 File Offset: 0x0009CD14
		internal bool IsDownload
		{
			get
			{
				return (this.Flags & FtpMethodFlags.IsDownload) > FtpMethodFlags.None;
			}
		}

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x06002E0A RID: 11786 RVA: 0x0009EB21 File Offset: 0x0009CD21
		internal bool ShouldParseForResponseUri
		{
			get
			{
				return (this.Flags & FtpMethodFlags.ShouldParseForResponseUri) > FtpMethodFlags.None;
			}
		}

		// Token: 0x06002E0B RID: 11787 RVA: 0x0009EB30 File Offset: 0x0009CD30
		internal static FtpMethodInfo GetMethodInfo(string method)
		{
			method = method.ToUpper(CultureInfo.InvariantCulture);
			foreach (FtpMethodInfo ftpMethodInfo in FtpMethodInfo.s_knownMethodInfo)
			{
				if (method == ftpMethodInfo.Method)
				{
					return ftpMethodInfo;
				}
			}
			throw new ArgumentException("This method is not supported.", "method");
		}

		// Token: 0x06002E0C RID: 11788 RVA: 0x0009EB84 File Offset: 0x0009CD84
		// Note: this type is marked as 'beforefieldinit'.
		static FtpMethodInfo()
		{
		}

		// Token: 0x04001963 RID: 6499
		internal string Method;

		// Token: 0x04001964 RID: 6500
		internal FtpOperation Operation;

		// Token: 0x04001965 RID: 6501
		internal FtpMethodFlags Flags;

		// Token: 0x04001966 RID: 6502
		internal string HttpCommand;

		// Token: 0x04001967 RID: 6503
		private static readonly FtpMethodInfo[] s_knownMethodInfo = new FtpMethodInfo[]
		{
			new FtpMethodInfo("RETR", FtpOperation.DownloadFile, FtpMethodFlags.IsDownload | FtpMethodFlags.TakesParameter | FtpMethodFlags.HasHttpCommand, "GET"),
			new FtpMethodInfo("NLST", FtpOperation.ListDirectory, FtpMethodFlags.IsDownload | FtpMethodFlags.MayTakeParameter | FtpMethodFlags.HasHttpCommand | FtpMethodFlags.MustChangeWorkingDirectoryToPath, "GET"),
			new FtpMethodInfo("LIST", FtpOperation.ListDirectoryDetails, FtpMethodFlags.IsDownload | FtpMethodFlags.MayTakeParameter | FtpMethodFlags.HasHttpCommand | FtpMethodFlags.MustChangeWorkingDirectoryToPath, "GET"),
			new FtpMethodInfo("STOR", FtpOperation.UploadFile, FtpMethodFlags.IsUpload | FtpMethodFlags.TakesParameter, null),
			new FtpMethodInfo("STOU", FtpOperation.UploadFileUnique, FtpMethodFlags.IsUpload | FtpMethodFlags.DoesNotTakeParameter | FtpMethodFlags.ShouldParseForResponseUri | FtpMethodFlags.MustChangeWorkingDirectoryToPath, null),
			new FtpMethodInfo("APPE", FtpOperation.AppendFile, FtpMethodFlags.IsUpload | FtpMethodFlags.TakesParameter, null),
			new FtpMethodInfo("DELE", FtpOperation.DeleteFile, FtpMethodFlags.TakesParameter, null),
			new FtpMethodInfo("MDTM", FtpOperation.GetDateTimestamp, FtpMethodFlags.TakesParameter, null),
			new FtpMethodInfo("SIZE", FtpOperation.GetFileSize, FtpMethodFlags.TakesParameter, null),
			new FtpMethodInfo("RENAME", FtpOperation.Rename, FtpMethodFlags.TakesParameter, null),
			new FtpMethodInfo("MKD", FtpOperation.MakeDirectory, FtpMethodFlags.TakesParameter | FtpMethodFlags.ParameterIsDirectory, null),
			new FtpMethodInfo("RMD", FtpOperation.RemoveDirectory, FtpMethodFlags.TakesParameter | FtpMethodFlags.ParameterIsDirectory, null),
			new FtpMethodInfo("PWD", FtpOperation.PrintWorkingDirectory, FtpMethodFlags.DoesNotTakeParameter, null)
		};
	}
}
