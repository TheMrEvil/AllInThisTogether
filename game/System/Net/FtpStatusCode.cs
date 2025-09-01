using System;

namespace System.Net
{
	/// <summary>Specifies the status codes returned for a File Transfer Protocol (FTP) operation.</summary>
	// Token: 0x020005CC RID: 1484
	public enum FtpStatusCode
	{
		/// <summary>Included for completeness, this value is never returned by servers.</summary>
		// Token: 0x04001A78 RID: 6776
		Undefined,
		/// <summary>Specifies that the response contains a restart marker reply. The text of the description that accompanies this status contains the user data stream marker and the server marker.</summary>
		// Token: 0x04001A79 RID: 6777
		RestartMarker = 110,
		/// <summary>Specifies that the service is not available now; try your request later.</summary>
		// Token: 0x04001A7A RID: 6778
		ServiceTemporarilyNotAvailable = 120,
		/// <summary>Specifies that the data connection is already open and the requested transfer is starting.</summary>
		// Token: 0x04001A7B RID: 6779
		DataAlreadyOpen = 125,
		/// <summary>Specifies that the server is opening the data connection.</summary>
		// Token: 0x04001A7C RID: 6780
		OpeningData = 150,
		/// <summary>Specifies that the command completed successfully.</summary>
		// Token: 0x04001A7D RID: 6781
		CommandOK = 200,
		/// <summary>Specifies that the command is not implemented by the server because it is not needed.</summary>
		// Token: 0x04001A7E RID: 6782
		CommandExtraneous = 202,
		/// <summary>Specifies the status of a directory.</summary>
		// Token: 0x04001A7F RID: 6783
		DirectoryStatus = 212,
		/// <summary>Specifies the status of a file.</summary>
		// Token: 0x04001A80 RID: 6784
		FileStatus,
		/// <summary>Specifies the system type name using the system names published in the Assigned Numbers document published by the Internet Assigned Numbers Authority.</summary>
		// Token: 0x04001A81 RID: 6785
		SystemType = 215,
		/// <summary>Specifies that the server is ready for a user login operation.</summary>
		// Token: 0x04001A82 RID: 6786
		SendUserCommand = 220,
		/// <summary>Specifies that the server is closing the control connection.</summary>
		// Token: 0x04001A83 RID: 6787
		ClosingControl,
		/// <summary>Specifies that the server is closing the data connection and that the requested file action was successful.</summary>
		// Token: 0x04001A84 RID: 6788
		ClosingData = 226,
		/// <summary>Specifies that the server is entering passive mode.</summary>
		// Token: 0x04001A85 RID: 6789
		EnteringPassive,
		/// <summary>Specifies that the user is logged in and can send commands.</summary>
		// Token: 0x04001A86 RID: 6790
		LoggedInProceed = 230,
		/// <summary>Specifies that the server accepts the authentication mechanism specified by the client, and the exchange of security data is complete.</summary>
		// Token: 0x04001A87 RID: 6791
		ServerWantsSecureSession = 234,
		/// <summary>Specifies that the requested file action completed successfully.</summary>
		// Token: 0x04001A88 RID: 6792
		FileActionOK = 250,
		/// <summary>Specifies that the requested path name was created.</summary>
		// Token: 0x04001A89 RID: 6793
		PathnameCreated = 257,
		/// <summary>Specifies that the server expects a password to be supplied.</summary>
		// Token: 0x04001A8A RID: 6794
		SendPasswordCommand = 331,
		/// <summary>Specifies that the server requires a login account to be supplied.</summary>
		// Token: 0x04001A8B RID: 6795
		NeedLoginAccount,
		/// <summary>Specifies that the requested file action requires additional information.</summary>
		// Token: 0x04001A8C RID: 6796
		FileCommandPending = 350,
		/// <summary>Specifies that the service is not available.</summary>
		// Token: 0x04001A8D RID: 6797
		ServiceNotAvailable = 421,
		/// <summary>Specifies that the data connection cannot be opened.</summary>
		// Token: 0x04001A8E RID: 6798
		CantOpenData = 425,
		/// <summary>Specifies that the connection has been closed.</summary>
		// Token: 0x04001A8F RID: 6799
		ConnectionClosed,
		/// <summary>Specifies that the requested action cannot be performed on the specified file because the file is not available or is being used.</summary>
		// Token: 0x04001A90 RID: 6800
		ActionNotTakenFileUnavailableOrBusy = 450,
		/// <summary>Specifies that an error occurred that prevented the request action from completing.</summary>
		// Token: 0x04001A91 RID: 6801
		ActionAbortedLocalProcessingError,
		/// <summary>Specifies that the requested action cannot be performed because there is not enough space on the server.</summary>
		// Token: 0x04001A92 RID: 6802
		ActionNotTakenInsufficientSpace,
		/// <summary>Specifies that the command has a syntax error or is not a command recognized by the server.</summary>
		// Token: 0x04001A93 RID: 6803
		CommandSyntaxError = 500,
		/// <summary>Specifies that one or more command arguments has a syntax error.</summary>
		// Token: 0x04001A94 RID: 6804
		ArgumentSyntaxError,
		/// <summary>Specifies that the command is not implemented by the FTP server.</summary>
		// Token: 0x04001A95 RID: 6805
		CommandNotImplemented,
		/// <summary>Specifies that the sequence of commands is not in the correct order.</summary>
		// Token: 0x04001A96 RID: 6806
		BadCommandSequence,
		/// <summary>Specifies that login information must be sent to the server.</summary>
		// Token: 0x04001A97 RID: 6807
		NotLoggedIn = 530,
		/// <summary>Specifies that a user account on the server is required.</summary>
		// Token: 0x04001A98 RID: 6808
		AccountNeeded = 532,
		/// <summary>Specifies that the requested action cannot be performed on the specified file because the file is not available.</summary>
		// Token: 0x04001A99 RID: 6809
		ActionNotTakenFileUnavailable = 550,
		/// <summary>Specifies that the requested action cannot be taken because the specified page type is unknown. Page types are described in RFC 959 Section 3.1.2.3</summary>
		// Token: 0x04001A9A RID: 6810
		ActionAbortedUnknownPageType,
		/// <summary>Specifies that the requested action cannot be performed.</summary>
		// Token: 0x04001A9B RID: 6811
		FileActionAborted,
		/// <summary>Specifies that the requested action cannot be performed on the specified file.</summary>
		// Token: 0x04001A9C RID: 6812
		ActionNotTakenFilenameNotAllowed
	}
}
