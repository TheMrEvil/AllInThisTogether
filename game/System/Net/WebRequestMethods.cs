using System;

namespace System.Net
{
	/// <summary>Container class for <see cref="T:System.Net.WebRequestMethods.Ftp" />, <see cref="T:System.Net.WebRequestMethods.File" />, and <see cref="T:System.Net.WebRequestMethods.Http" /> classes. This class cannot be inherited</summary>
	// Token: 0x02000618 RID: 1560
	public static class WebRequestMethods
	{
		/// <summary>Represents the types of FTP protocol methods that can be used with an FTP request. This class cannot be inherited.</summary>
		// Token: 0x02000619 RID: 1561
		public static class Ftp
		{
			/// <summary>Represents the FTP RETR protocol method that is used to download a file from an FTP server.</summary>
			// Token: 0x04001CB2 RID: 7346
			public const string DownloadFile = "RETR";

			/// <summary>Represents the FTP NLIST protocol method that gets a short listing of the files on an FTP server.</summary>
			// Token: 0x04001CB3 RID: 7347
			public const string ListDirectory = "NLST";

			/// <summary>Represents the FTP STOR protocol method that uploads a file to an FTP server.</summary>
			// Token: 0x04001CB4 RID: 7348
			public const string UploadFile = "STOR";

			/// <summary>Represents the FTP DELE protocol method that is used to delete a file on an FTP server.</summary>
			// Token: 0x04001CB5 RID: 7349
			public const string DeleteFile = "DELE";

			/// <summary>Represents the FTP APPE protocol method that is used to append a file to an existing file on an FTP server.</summary>
			// Token: 0x04001CB6 RID: 7350
			public const string AppendFile = "APPE";

			/// <summary>Represents the FTP SIZE protocol method that is used to retrieve the size of a file on an FTP server.</summary>
			// Token: 0x04001CB7 RID: 7351
			public const string GetFileSize = "SIZE";

			/// <summary>Represents the FTP STOU protocol that uploads a file with a unique name to an FTP server.</summary>
			// Token: 0x04001CB8 RID: 7352
			public const string UploadFileWithUniqueName = "STOU";

			/// <summary>Represents the FTP MKD protocol method creates a directory on an FTP server.</summary>
			// Token: 0x04001CB9 RID: 7353
			public const string MakeDirectory = "MKD";

			/// <summary>Represents the FTP RMD protocol method that removes a directory.</summary>
			// Token: 0x04001CBA RID: 7354
			public const string RemoveDirectory = "RMD";

			/// <summary>Represents the FTP LIST protocol method that gets a detailed listing of the files on an FTP server.</summary>
			// Token: 0x04001CBB RID: 7355
			public const string ListDirectoryDetails = "LIST";

			/// <summary>Represents the FTP MDTM protocol method that is used to retrieve the date-time stamp from a file on an FTP server.</summary>
			// Token: 0x04001CBC RID: 7356
			public const string GetDateTimestamp = "MDTM";

			/// <summary>Represents the FTP PWD protocol method that prints the name of the current working directory.</summary>
			// Token: 0x04001CBD RID: 7357
			public const string PrintWorkingDirectory = "PWD";

			/// <summary>Represents the FTP RENAME protocol method that renames a directory.</summary>
			// Token: 0x04001CBE RID: 7358
			public const string Rename = "RENAME";
		}

		/// <summary>Represents the types of HTTP protocol methods that can be used with an HTTP request.</summary>
		// Token: 0x0200061A RID: 1562
		public static class Http
		{
			/// <summary>Represents an HTTP GET protocol method.</summary>
			// Token: 0x04001CBF RID: 7359
			public const string Get = "GET";

			/// <summary>Represents the HTTP CONNECT protocol method that is used with a proxy that can dynamically switch to tunneling, as in the case of SSL tunneling.</summary>
			// Token: 0x04001CC0 RID: 7360
			public const string Connect = "CONNECT";

			/// <summary>Represents an HTTP HEAD protocol method. The HEAD method is identical to GET except that the server only returns message-headers in the response, without a message-body.</summary>
			// Token: 0x04001CC1 RID: 7361
			public const string Head = "HEAD";

			/// <summary>Represents an HTTP PUT protocol method that is used to replace an entity identified by a URI.</summary>
			// Token: 0x04001CC2 RID: 7362
			public const string Put = "PUT";

			/// <summary>Represents an HTTP POST protocol method that is used to post a new entity as an addition to a URI.</summary>
			// Token: 0x04001CC3 RID: 7363
			public const string Post = "POST";

			/// <summary>Represents an HTTP MKCOL request that creates a new collection (such as a collection of pages) at the location specified by the request-Uniform Resource Identifier (URI).</summary>
			// Token: 0x04001CC4 RID: 7364
			public const string MkCol = "MKCOL";
		}

		/// <summary>Represents the types of file protocol methods that can be used with a FILE request. This class cannot be inherited.</summary>
		// Token: 0x0200061B RID: 1563
		public static class File
		{
			/// <summary>Represents the FILE GET protocol method that is used to retrieve a file from a specified location.</summary>
			// Token: 0x04001CC5 RID: 7365
			public const string DownloadFile = "GET";

			/// <summary>Represents the FILE PUT protocol method that is used to copy a file to a specified location.</summary>
			// Token: 0x04001CC6 RID: 7366
			public const string UploadFile = "PUT";
		}
	}
}
