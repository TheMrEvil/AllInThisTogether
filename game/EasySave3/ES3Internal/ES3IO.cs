using System;
using System.IO;
using UnityEngine;

namespace ES3Internal
{
	// Token: 0x020000D5 RID: 213
	public static class ES3IO
	{
		// Token: 0x0600042C RID: 1068 RVA: 0x0001B180 File Offset: 0x00019380
		public static DateTime GetTimestamp(string filePath)
		{
			if (!ES3IO.FileExists(filePath))
			{
				return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			}
			return File.GetLastWriteTime(filePath).ToUniversalTime();
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0001B1B5 File Offset: 0x000193B5
		public static string GetExtension(string path)
		{
			return Path.GetExtension(path);
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0001B1BD File Offset: 0x000193BD
		public static void DeleteFile(string filePath)
		{
			if (ES3IO.FileExists(filePath))
			{
				File.Delete(filePath);
			}
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0001B1CD File Offset: 0x000193CD
		public static bool FileExists(string filePath)
		{
			return File.Exists(filePath);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0001B1D5 File Offset: 0x000193D5
		public static void MoveFile(string sourcePath, string destPath)
		{
			File.Move(sourcePath, destPath);
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0001B1DE File Offset: 0x000193DE
		public static void CopyFile(string sourcePath, string destPath)
		{
			File.Copy(sourcePath, destPath);
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0001B1E7 File Offset: 0x000193E7
		public static void MoveDirectory(string sourcePath, string destPath)
		{
			Directory.Move(sourcePath, destPath);
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0001B1F0 File Offset: 0x000193F0
		public static void CreateDirectory(string directoryPath)
		{
			Directory.CreateDirectory(directoryPath);
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0001B1F9 File Offset: 0x000193F9
		public static bool DirectoryExists(string directoryPath)
		{
			return Directory.Exists(directoryPath);
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0001B204 File Offset: 0x00019404
		public static string GetDirectoryPath(string path, char seperator = '/')
		{
			char value = ES3IO.UsesForwardSlash(path) ? '/' : '\\';
			int num = path.LastIndexOf(value);
			if (num == path.Length - 1)
			{
				return path;
			}
			if (num == path.Length - 1)
			{
				num = path.Substring(0, num).LastIndexOf(value);
			}
			if (num == -1)
			{
				ES3Debug.LogError("Path provided is not a directory path as it contains no slashes.", null, 0);
			}
			return path.Substring(0, num);
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0001B267 File Offset: 0x00019467
		public static bool UsesForwardSlash(string path)
		{
			return path.Contains("/");
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0001B279 File Offset: 0x00019479
		public static string CombinePathAndFilename(string directoryPath, string fileOrDirectoryName)
		{
			if (directoryPath[directoryPath.Length - 1] != '/' && directoryPath[directoryPath.Length - 1] != '\\')
			{
				directoryPath += "/";
			}
			return directoryPath + fileOrDirectoryName;
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0001B2B4 File Offset: 0x000194B4
		public static string[] GetDirectories(string path, bool getFullPaths = true)
		{
			string[] directories = Directory.GetDirectories(path);
			for (int i = 0; i < directories.Length; i++)
			{
				if (!getFullPaths)
				{
					directories[i] = Path.GetFileName(directories[i]);
				}
				directories[i].Replace("\\", "/");
			}
			return directories;
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0001B2F8 File Offset: 0x000194F8
		public static void DeleteDirectory(string directoryPath)
		{
			if (ES3IO.DirectoryExists(directoryPath))
			{
				Directory.Delete(directoryPath, true);
			}
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0001B30C File Offset: 0x0001950C
		public static string[] GetFiles(string path, bool getFullPaths = true)
		{
			string[] files = Directory.GetFiles((path.EndsWith("/") || path.EndsWith("\\")) ? path : ES3IO.GetDirectoryPath(path, '/'));
			if (!getFullPaths)
			{
				for (int i = 0; i < files.Length; i++)
				{
					files[i] = Path.GetFileName(files[i]);
				}
			}
			return files;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0001B361 File Offset: 0x00019561
		public static byte[] ReadAllBytes(string path)
		{
			return File.ReadAllBytes(path);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0001B369 File Offset: 0x00019569
		public static void WriteAllBytes(string path, byte[] bytes)
		{
			File.WriteAllBytes(path, bytes);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0001B374 File Offset: 0x00019574
		public static void CommitBackup(ES3Settings settings)
		{
			ES3Debug.Log("Committing backup for " + settings.path + " to storage location " + settings.location.ToString(), null, 0);
			string text = settings.FullPath + ".tmp";
			if (settings.location != ES3.Location.File)
			{
				if (settings.location == ES3.Location.PlayerPrefs)
				{
					PlayerPrefs.SetString(settings.FullPath, PlayerPrefs.GetString(text));
					PlayerPrefs.DeleteKey(text);
					PlayerPrefs.Save();
				}
				return;
			}
			string text2 = settings.FullPath + ".tmp.bak";
			if (ES3IO.FileExists(settings.FullPath))
			{
				ES3IO.DeleteFile(text2);
				ES3IO.CopyFile(settings.FullPath, text2);
				try
				{
					ES3IO.DeleteFile(settings.FullPath);
					ES3IO.MoveFile(text, settings.FullPath);
				}
				catch (Exception ex)
				{
					try
					{
						ES3IO.DeleteFile(settings.FullPath);
					}
					catch
					{
					}
					ES3IO.MoveFile(text2, settings.FullPath);
					throw ex;
				}
				ES3IO.DeleteFile(text2);
				return;
			}
			ES3IO.MoveFile(text, settings.FullPath);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0001B488 File Offset: 0x00019688
		// Note: this type is marked as 'beforefieldinit'.
		static ES3IO()
		{
		}

		// Token: 0x04000131 RID: 305
		internal static readonly string persistentDataPath = Application.persistentDataPath;

		// Token: 0x04000132 RID: 306
		internal static readonly string dataPath = Application.dataPath;

		// Token: 0x04000133 RID: 307
		internal const string backupFileSuffix = ".bac";

		// Token: 0x04000134 RID: 308
		internal const string temporaryFileSuffix = ".tmp";

		// Token: 0x02000108 RID: 264
		public enum ES3FileMode
		{
			// Token: 0x040001FF RID: 511
			Read,
			// Token: 0x04000200 RID: 512
			Write,
			// Token: 0x04000201 RID: 513
			Append
		}
	}
}
