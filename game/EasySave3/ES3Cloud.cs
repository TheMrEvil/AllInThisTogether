using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using ES3Internal;
using UnityEngine;
using UnityEngine.Networking;

// Token: 0x02000015 RID: 21
public class ES3Cloud : ES3WebClass
{
	// Token: 0x0600015B RID: 347 RVA: 0x00005EF8 File Offset: 0x000040F8
	public ES3Cloud(string url, string apiKey) : base(url, apiKey)
	{
	}

	// Token: 0x0600015C RID: 348 RVA: 0x00005F15 File Offset: 0x00004115
	public ES3Cloud(string url, string apiKey, int timeout) : base(url, apiKey)
	{
		this.timeout = timeout;
	}

	// Token: 0x1700000E RID: 14
	// (get) Token: 0x0600015D RID: 349 RVA: 0x00005F39 File Offset: 0x00004139
	public byte[] data
	{
		get
		{
			return this._data;
		}
	}

	// Token: 0x1700000F RID: 15
	// (get) Token: 0x0600015E RID: 350 RVA: 0x00005F41 File Offset: 0x00004141
	public string text
	{
		get
		{
			if (this.data == null)
			{
				return null;
			}
			return this.encoding.GetString(this.data);
		}
	}

	// Token: 0x17000010 RID: 16
	// (get) Token: 0x0600015F RID: 351 RVA: 0x00005F5E File Offset: 0x0000415E
	public string[] filenames
	{
		get
		{
			if (this.data == null || this.data.Length == 0)
			{
				return new string[0];
			}
			return this.text.Split(new char[]
			{
				';'
			}, StringSplitOptions.RemoveEmptyEntries);
		}
	}

	// Token: 0x17000011 RID: 17
	// (get) Token: 0x06000160 RID: 352 RVA: 0x00005F90 File Offset: 0x00004190
	public DateTime timestamp
	{
		get
		{
			if (this.data == null || this.data.Length == 0)
			{
				return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			}
			double value;
			if (!double.TryParse(this.text, out value))
			{
				throw new FormatException("Could not convert downloaded data to a timestamp. Data downloaded was: " + this.text);
			}
			return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(value);
		}
	}

	// Token: 0x06000161 RID: 353 RVA: 0x00005FFF File Offset: 0x000041FF
	public IEnumerator Sync()
	{
		return this.Sync(new ES3Settings(null, null), "", "");
	}

	// Token: 0x06000162 RID: 354 RVA: 0x00006018 File Offset: 0x00004218
	public IEnumerator Sync(string filePath)
	{
		return this.Sync(new ES3Settings(filePath, null), "", "");
	}

	// Token: 0x06000163 RID: 355 RVA: 0x00006031 File Offset: 0x00004231
	public IEnumerator Sync(string filePath, string user)
	{
		return this.Sync(new ES3Settings(filePath, null), user, "");
	}

	// Token: 0x06000164 RID: 356 RVA: 0x00006046 File Offset: 0x00004246
	public IEnumerator Sync(string filePath, string user, string password)
	{
		return this.Sync(new ES3Settings(filePath, null), user, password);
	}

	// Token: 0x06000165 RID: 357 RVA: 0x00006057 File Offset: 0x00004257
	public IEnumerator Sync(string filePath, ES3Settings settings)
	{
		return this.Sync(new ES3Settings(filePath, settings), "", "");
	}

	// Token: 0x06000166 RID: 358 RVA: 0x00006070 File Offset: 0x00004270
	public IEnumerator Sync(string filePath, string user, ES3Settings settings)
	{
		return this.Sync(new ES3Settings(filePath, settings), user, "");
	}

	// Token: 0x06000167 RID: 359 RVA: 0x00006085 File Offset: 0x00004285
	public IEnumerator Sync(string filePath, string user, string password, ES3Settings settings)
	{
		return this.Sync(new ES3Settings(filePath, settings), user, password);
	}

	// Token: 0x06000168 RID: 360 RVA: 0x00006097 File Offset: 0x00004297
	private IEnumerator Sync(ES3Settings settings, string user, string password)
	{
		this.Reset();
		yield return this.DownloadFile(settings, user, password, this.GetFileTimestamp(settings));
		if (this.errorCode == 3L)
		{
			this.Reset();
			if (ES3.FileExists(settings))
			{
				yield return this.UploadFile(settings, user, password);
			}
		}
		this.isDone = true;
		yield break;
	}

	// Token: 0x06000169 RID: 361 RVA: 0x000060BB File Offset: 0x000042BB
	public IEnumerator UploadFile()
	{
		return this.UploadFile(new ES3Settings(null, null), "", "");
	}

	// Token: 0x0600016A RID: 362 RVA: 0x000060D4 File Offset: 0x000042D4
	public IEnumerator UploadFile(string filePath)
	{
		return this.UploadFile(new ES3Settings(filePath, null), "", "");
	}

	// Token: 0x0600016B RID: 363 RVA: 0x000060ED File Offset: 0x000042ED
	public IEnumerator UploadFile(string filePath, string user)
	{
		return this.UploadFile(new ES3Settings(filePath, null), user, "");
	}

	// Token: 0x0600016C RID: 364 RVA: 0x00006102 File Offset: 0x00004302
	public IEnumerator UploadFile(string filePath, string user, string password)
	{
		return this.UploadFile(new ES3Settings(filePath, null), user, password);
	}

	// Token: 0x0600016D RID: 365 RVA: 0x00006113 File Offset: 0x00004313
	public IEnumerator UploadFile(string filePath, ES3Settings settings)
	{
		return this.UploadFile(new ES3Settings(filePath, settings), "", "");
	}

	// Token: 0x0600016E RID: 366 RVA: 0x0000612C File Offset: 0x0000432C
	public IEnumerator UploadFile(string filePath, string user, ES3Settings settings)
	{
		return this.UploadFile(new ES3Settings(filePath, settings), user, "");
	}

	// Token: 0x0600016F RID: 367 RVA: 0x00006141 File Offset: 0x00004341
	public IEnumerator UploadFile(string filePath, string user, string password, ES3Settings settings)
	{
		return this.UploadFile(new ES3Settings(filePath, settings), user, password);
	}

	// Token: 0x06000170 RID: 368 RVA: 0x00006153 File Offset: 0x00004353
	public IEnumerator UploadFile(ES3File es3File)
	{
		return this.UploadFile(es3File.GetBytes(null), es3File.settings, "", "", this.DateTimeToUnixTimestamp(DateTime.Now));
	}

	// Token: 0x06000171 RID: 369 RVA: 0x0000617D File Offset: 0x0000437D
	public IEnumerator UploadFile(ES3File es3File, string user)
	{
		return this.UploadFile(es3File.GetBytes(null), es3File.settings, user, "", this.DateTimeToUnixTimestamp(DateTime.Now));
	}

	// Token: 0x06000172 RID: 370 RVA: 0x000061A3 File Offset: 0x000043A3
	public IEnumerator UploadFile(ES3File es3File, string user, string password)
	{
		return this.UploadFile(es3File.GetBytes(null), es3File.settings, user, password, this.DateTimeToUnixTimestamp(DateTime.Now));
	}

	// Token: 0x06000173 RID: 371 RVA: 0x000061C5 File Offset: 0x000043C5
	public IEnumerator UploadFile(ES3Settings settings, string user, string password)
	{
		return this.UploadFile(ES3.LoadRawBytes(settings), settings, user, password);
	}

	// Token: 0x06000174 RID: 372 RVA: 0x000061D6 File Offset: 0x000043D6
	public IEnumerator UploadFile(byte[] bytes, ES3Settings settings, string user, string password)
	{
		return this.UploadFile(bytes, settings, user, password, this.DateTimeToUnixTimestamp(ES3.GetTimestamp(settings)));
	}

	// Token: 0x06000175 RID: 373 RVA: 0x000061EF File Offset: 0x000043EF
	private IEnumerator UploadFile(byte[] bytes, ES3Settings settings, string user, string password, long fileTimestamp)
	{
		this.Reset();
		WWWForm wwwform = base.CreateWWWForm();
		wwwform.AddField("apiKey", this.apiKey);
		wwwform.AddField("putFile", settings.path);
		wwwform.AddField("timestamp", fileTimestamp.ToString());
		wwwform.AddField("user", base.GetUser(user, password));
		wwwform.AddBinaryData("data", bytes, "data.dat", "multipart/form-data");
		using (UnityWebRequest webRequest = UnityWebRequest.Post(this.url, wwwform))
		{
			webRequest.timeout = this.timeout;
			yield return base.SendWebRequest(webRequest);
			base.HandleError(webRequest, true);
		}
		UnityWebRequest webRequest = null;
		this.isDone = true;
		yield break;
		yield break;
	}

	// Token: 0x06000176 RID: 374 RVA: 0x00006223 File Offset: 0x00004423
	public IEnumerator DownloadFile()
	{
		return this.DownloadFile(new ES3Settings(null, null), "", "", 0L);
	}

	// Token: 0x06000177 RID: 375 RVA: 0x0000623E File Offset: 0x0000443E
	public IEnumerator DownloadFile(string filePath)
	{
		return this.DownloadFile(new ES3Settings(filePath, null), "", "", 0L);
	}

	// Token: 0x06000178 RID: 376 RVA: 0x00006259 File Offset: 0x00004459
	public IEnumerator DownloadFile(string filePath, string user)
	{
		return this.DownloadFile(new ES3Settings(filePath, null), user, "", 0L);
	}

	// Token: 0x06000179 RID: 377 RVA: 0x00006270 File Offset: 0x00004470
	public IEnumerator DownloadFile(string filePath, string user, string password)
	{
		return this.DownloadFile(new ES3Settings(filePath, null), user, password, 0L);
	}

	// Token: 0x0600017A RID: 378 RVA: 0x00006283 File Offset: 0x00004483
	public IEnumerator DownloadFile(string filePath, ES3Settings settings)
	{
		return this.DownloadFile(new ES3Settings(filePath, settings), "", "", 0L);
	}

	// Token: 0x0600017B RID: 379 RVA: 0x0000629E File Offset: 0x0000449E
	public IEnumerator DownloadFile(string filePath, string user, ES3Settings settings)
	{
		return this.DownloadFile(new ES3Settings(filePath, settings), user, "", 0L);
	}

	// Token: 0x0600017C RID: 380 RVA: 0x000062B5 File Offset: 0x000044B5
	public IEnumerator DownloadFile(string filePath, string user, string password, ES3Settings settings)
	{
		return this.DownloadFile(new ES3Settings(filePath, settings), user, password, 0L);
	}

	// Token: 0x0600017D RID: 381 RVA: 0x000062C9 File Offset: 0x000044C9
	public IEnumerator DownloadFile(ES3File es3File)
	{
		return this.DownloadFile(es3File, "", "", 0L);
	}

	// Token: 0x0600017E RID: 382 RVA: 0x000062DE File Offset: 0x000044DE
	public IEnumerator DownloadFile(ES3File es3File, string user)
	{
		return this.DownloadFile(es3File, user, "", 0L);
	}

	// Token: 0x0600017F RID: 383 RVA: 0x000062EF File Offset: 0x000044EF
	public IEnumerator DownloadFile(ES3File es3File, string user, string password)
	{
		return this.DownloadFile(es3File, user, password, 0L);
	}

	// Token: 0x06000180 RID: 384 RVA: 0x000062FC File Offset: 0x000044FC
	private IEnumerator DownloadFile(ES3File es3File, string user, string password, long timestamp)
	{
		this.Reset();
		WWWForm wwwform = base.CreateWWWForm();
		wwwform.AddField("apiKey", this.apiKey);
		wwwform.AddField("getFile", es3File.settings.path);
		wwwform.AddField("user", base.GetUser(user, password));
		if (timestamp > 0L)
		{
			wwwform.AddField("timestamp", timestamp.ToString());
		}
		using (UnityWebRequest webRequest = UnityWebRequest.Post(this.url, wwwform))
		{
			webRequest.timeout = this.timeout;
			yield return base.SendWebRequest(webRequest);
			if (!base.HandleError(webRequest, false))
			{
				if (webRequest.downloadedBytes > 0UL)
				{
					es3File.Clear();
					es3File.SaveRaw(webRequest.downloadHandler.data, null);
				}
				else
				{
					this.error = string.Format("File {0} was not found on the server.", es3File.settings.path);
					this.errorCode = 3L;
				}
			}
		}
		UnityWebRequest webRequest = null;
		this.isDone = true;
		yield break;
		yield break;
	}

	// Token: 0x06000181 RID: 385 RVA: 0x00006328 File Offset: 0x00004528
	private IEnumerator DownloadFile(ES3Settings settings, string user, string password, long timestamp)
	{
		this.Reset();
		WWWForm wwwform = base.CreateWWWForm();
		wwwform.AddField("apiKey", this.apiKey);
		wwwform.AddField("getFile", settings.path);
		wwwform.AddField("user", base.GetUser(user, password));
		if (timestamp > 0L)
		{
			wwwform.AddField("timestamp", timestamp.ToString());
		}
		using (UnityWebRequest webRequest = UnityWebRequest.Post(this.url, wwwform))
		{
			webRequest.timeout = this.timeout;
			yield return base.SendWebRequest(webRequest);
			if (!base.HandleError(webRequest, false))
			{
				if (webRequest.downloadedBytes > 0UL)
				{
					ES3.SaveRaw(webRequest.downloadHandler.data, settings);
				}
				else
				{
					this.error = string.Format("File {0} was not found on the server.", settings.path);
					this.errorCode = 3L;
				}
			}
		}
		UnityWebRequest webRequest = null;
		this.isDone = true;
		yield break;
		yield break;
	}

	// Token: 0x06000182 RID: 386 RVA: 0x00006354 File Offset: 0x00004554
	public IEnumerator DeleteFile()
	{
		return this.DeleteFile(new ES3Settings(null, null), "", "");
	}

	// Token: 0x06000183 RID: 387 RVA: 0x0000636D File Offset: 0x0000456D
	public IEnumerator DeleteFile(string filePath)
	{
		return this.DeleteFile(new ES3Settings(filePath, null), "", "");
	}

	// Token: 0x06000184 RID: 388 RVA: 0x00006386 File Offset: 0x00004586
	public IEnumerator DeleteFile(string filePath, string user)
	{
		return this.DeleteFile(new ES3Settings(filePath, null), user, "");
	}

	// Token: 0x06000185 RID: 389 RVA: 0x0000639B File Offset: 0x0000459B
	public IEnumerator DeleteFile(string filePath, string user, string password)
	{
		return this.DeleteFile(new ES3Settings(filePath, null), user, password);
	}

	// Token: 0x06000186 RID: 390 RVA: 0x000063AC File Offset: 0x000045AC
	public IEnumerator DeleteFile(string filePath, ES3Settings settings)
	{
		return this.DeleteFile(new ES3Settings(filePath, settings), "", "");
	}

	// Token: 0x06000187 RID: 391 RVA: 0x000063C5 File Offset: 0x000045C5
	public IEnumerator DeleteFile(string filePath, string user, ES3Settings settings)
	{
		return this.DeleteFile(new ES3Settings(filePath, settings), user, "");
	}

	// Token: 0x06000188 RID: 392 RVA: 0x000063DA File Offset: 0x000045DA
	public IEnumerator DeleteFile(string filePath, string user, string password, ES3Settings settings)
	{
		return this.DeleteFile(new ES3Settings(filePath, settings), user, password);
	}

	// Token: 0x06000189 RID: 393 RVA: 0x000063EC File Offset: 0x000045EC
	private IEnumerator DeleteFile(ES3Settings settings, string user, string password)
	{
		this.Reset();
		WWWForm wwwform = base.CreateWWWForm();
		wwwform.AddField("apiKey", this.apiKey);
		wwwform.AddField("deleteFile", settings.path);
		wwwform.AddField("user", base.GetUser(user, password));
		using (UnityWebRequest webRequest = UnityWebRequest.Post(this.url, wwwform))
		{
			webRequest.timeout = this.timeout;
			yield return base.SendWebRequest(webRequest);
			base.HandleError(webRequest, true);
		}
		UnityWebRequest webRequest = null;
		this.isDone = true;
		yield break;
		yield break;
	}

	// Token: 0x0600018A RID: 394 RVA: 0x00006410 File Offset: 0x00004610
	public IEnumerator RenameFile(string filePath, string newFilePath)
	{
		return this.RenameFile(new ES3Settings(filePath, null), new ES3Settings(newFilePath, null), "", "");
	}

	// Token: 0x0600018B RID: 395 RVA: 0x00006430 File Offset: 0x00004630
	public IEnumerator RenameFile(string filePath, string newFilePath, string user)
	{
		return this.RenameFile(new ES3Settings(filePath, null), new ES3Settings(newFilePath, null), user, "");
	}

	// Token: 0x0600018C RID: 396 RVA: 0x0000644C File Offset: 0x0000464C
	public IEnumerator RenameFile(string filePath, string newFilePath, string user, string password)
	{
		return this.RenameFile(new ES3Settings(filePath, null), new ES3Settings(newFilePath, null), user, password);
	}

	// Token: 0x0600018D RID: 397 RVA: 0x00006465 File Offset: 0x00004665
	public IEnumerator RenameFile(string filePath, string newFilePath, ES3Settings settings)
	{
		return this.RenameFile(new ES3Settings(filePath, settings), new ES3Settings(newFilePath, settings), "", "");
	}

	// Token: 0x0600018E RID: 398 RVA: 0x00006485 File Offset: 0x00004685
	public IEnumerator RenameFile(string filePath, string newFilePath, string user, ES3Settings settings)
	{
		return this.RenameFile(new ES3Settings(filePath, settings), new ES3Settings(newFilePath, settings), user, "");
	}

	// Token: 0x0600018F RID: 399 RVA: 0x000064A3 File Offset: 0x000046A3
	public IEnumerator RenameFile(string filePath, string newFilePath, string user, string password, ES3Settings settings)
	{
		return this.RenameFile(new ES3Settings(filePath, settings), new ES3Settings(newFilePath, settings), user, password);
	}

	// Token: 0x06000190 RID: 400 RVA: 0x000064BE File Offset: 0x000046BE
	private IEnumerator RenameFile(ES3Settings settings, ES3Settings newSettings, string user, string password)
	{
		this.Reset();
		WWWForm wwwform = base.CreateWWWForm();
		wwwform.AddField("apiKey", this.apiKey);
		wwwform.AddField("renameFile", settings.path);
		wwwform.AddField("newFilename", newSettings.path);
		wwwform.AddField("user", base.GetUser(user, password));
		using (UnityWebRequest webRequest = UnityWebRequest.Post(this.url, wwwform))
		{
			webRequest.timeout = this.timeout;
			yield return base.SendWebRequest(webRequest);
			base.HandleError(webRequest, true);
		}
		UnityWebRequest webRequest = null;
		this.isDone = true;
		yield break;
		yield break;
	}

	// Token: 0x06000191 RID: 401 RVA: 0x000064EA File Offset: 0x000046EA
	public IEnumerator DownloadFilenames(string user = "", string password = "")
	{
		this.Reset();
		WWWForm wwwform = base.CreateWWWForm();
		wwwform.AddField("apiKey", this.apiKey);
		wwwform.AddField("getFilenames", "");
		wwwform.AddField("user", base.GetUser(user, password));
		using (UnityWebRequest webRequest = UnityWebRequest.Post(this.url, wwwform))
		{
			webRequest.timeout = this.timeout;
			yield return base.SendWebRequest(webRequest);
			if (!base.HandleError(webRequest, false))
			{
				this._data = webRequest.downloadHandler.data;
			}
		}
		UnityWebRequest webRequest = null;
		this.isDone = true;
		yield break;
		yield break;
	}

	// Token: 0x06000192 RID: 402 RVA: 0x00006507 File Offset: 0x00004707
	public IEnumerator SearchFilenames(string searchPattern, string user = "", string password = "")
	{
		this.Reset();
		WWWForm wwwform = base.CreateWWWForm();
		wwwform.AddField("apiKey", this.apiKey);
		wwwform.AddField("getFilenames", "");
		wwwform.AddField("user", base.GetUser(user, password));
		if (!string.IsNullOrEmpty(searchPattern))
		{
			wwwform.AddField("pattern", searchPattern);
		}
		using (UnityWebRequest webRequest = UnityWebRequest.Post(this.url, wwwform))
		{
			webRequest.timeout = this.timeout;
			yield return base.SendWebRequest(webRequest);
			if (!base.HandleError(webRequest, false))
			{
				this._data = webRequest.downloadHandler.data;
			}
		}
		UnityWebRequest webRequest = null;
		this.isDone = true;
		yield break;
		yield break;
	}

	// Token: 0x06000193 RID: 403 RVA: 0x0000652B File Offset: 0x0000472B
	public IEnumerator DownloadTimestamp()
	{
		return this.DownloadTimestamp(new ES3Settings(null, null), "", "");
	}

	// Token: 0x06000194 RID: 404 RVA: 0x00006544 File Offset: 0x00004744
	public IEnumerator DownloadTimestamp(string filePath)
	{
		return this.DownloadTimestamp(new ES3Settings(filePath, null), "", "");
	}

	// Token: 0x06000195 RID: 405 RVA: 0x0000655D File Offset: 0x0000475D
	public IEnumerator DownloadTimestamp(string filePath, string user)
	{
		return this.DownloadTimestamp(new ES3Settings(filePath, null), user, "");
	}

	// Token: 0x06000196 RID: 406 RVA: 0x00006572 File Offset: 0x00004772
	public IEnumerator DownloadTimestamp(string filePath, string user, string password)
	{
		return this.DownloadTimestamp(new ES3Settings(filePath, null), user, password);
	}

	// Token: 0x06000197 RID: 407 RVA: 0x00006583 File Offset: 0x00004783
	public IEnumerator DownloadTimestamp(string filePath, ES3Settings settings)
	{
		return this.DownloadTimestamp(new ES3Settings(filePath, settings), "", "");
	}

	// Token: 0x06000198 RID: 408 RVA: 0x0000659C File Offset: 0x0000479C
	public IEnumerator DownloadTimestamp(string filePath, string user, ES3Settings settings)
	{
		return this.DownloadTimestamp(new ES3Settings(filePath, settings), user, "");
	}

	// Token: 0x06000199 RID: 409 RVA: 0x000065B1 File Offset: 0x000047B1
	public IEnumerator DownloadTimestamp(string filePath, string user, string password, ES3Settings settings)
	{
		return this.DownloadTimestamp(new ES3Settings(filePath, settings), user, password);
	}

	// Token: 0x0600019A RID: 410 RVA: 0x000065C3 File Offset: 0x000047C3
	private IEnumerator DownloadTimestamp(ES3Settings settings, string user, string password)
	{
		this.Reset();
		WWWForm wwwform = base.CreateWWWForm();
		wwwform.AddField("apiKey", this.apiKey);
		wwwform.AddField("getTimestamp", settings.path);
		wwwform.AddField("user", base.GetUser(user, password));
		using (UnityWebRequest webRequest = UnityWebRequest.Post(this.url, wwwform))
		{
			webRequest.timeout = this.timeout;
			yield return base.SendWebRequest(webRequest);
			if (!base.HandleError(webRequest, false))
			{
				this._data = webRequest.downloadHandler.data;
			}
		}
		UnityWebRequest webRequest = null;
		this.isDone = true;
		yield break;
		yield break;
	}

	// Token: 0x0600019B RID: 411 RVA: 0x000065E8 File Offset: 0x000047E8
	private long DateTimeToUnixTimestamp(DateTime dt)
	{
		return Convert.ToInt64((dt.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds);
	}

	// Token: 0x0600019C RID: 412 RVA: 0x0000661F File Offset: 0x0000481F
	private long GetFileTimestamp(ES3Settings settings)
	{
		return this.DateTimeToUnixTimestamp(ES3.GetTimestamp(settings));
	}

	// Token: 0x0600019D RID: 413 RVA: 0x0000662D File Offset: 0x0000482D
	protected override void Reset()
	{
		this._data = null;
		base.Reset();
	}

	// Token: 0x04000052 RID: 82
	private int timeout = 20;

	// Token: 0x04000053 RID: 83
	public Encoding encoding = Encoding.UTF8;

	// Token: 0x04000054 RID: 84
	private byte[] _data;

	// Token: 0x020000FB RID: 251
	[CompilerGenerated]
	private sealed class <Sync>d__20 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06000560 RID: 1376 RVA: 0x0001F5DF File Offset: 0x0001D7DF
		[DebuggerHidden]
		public <Sync>d__20(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0001F5EE File Offset: 0x0001D7EE
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0001F5F0 File Offset: 0x0001D7F0
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			ES3Cloud es3Cloud = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				es3Cloud.Reset();
				this.<>2__current = es3Cloud.DownloadFile(settings, user, password, es3Cloud.GetFileTimestamp(settings));
				this.<>1__state = 1;
				return true;
			case 1:
				this.<>1__state = -1;
				if (es3Cloud.errorCode == 3L)
				{
					es3Cloud.Reset();
					if (ES3.FileExists(settings))
					{
						this.<>2__current = es3Cloud.UploadFile(settings, user, password);
						this.<>1__state = 2;
						return true;
					}
				}
				break;
			case 2:
				this.<>1__state = -1;
				break;
			default:
				return false;
			}
			es3Cloud.isDone = true;
			return false;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x0001F6B9 File Offset: 0x0001D8B9
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0001F6C1 File Offset: 0x0001D8C1
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x0001F6C8 File Offset: 0x0001D8C8
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040001B3 RID: 435
		private int <>1__state;

		// Token: 0x040001B4 RID: 436
		private object <>2__current;

		// Token: 0x040001B5 RID: 437
		public ES3Cloud <>4__this;

		// Token: 0x040001B6 RID: 438
		public ES3Settings settings;

		// Token: 0x040001B7 RID: 439
		public string user;

		// Token: 0x040001B8 RID: 440
		public string password;
	}

	// Token: 0x020000FC RID: 252
	[CompilerGenerated]
	private sealed class <UploadFile>d__33 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06000566 RID: 1382 RVA: 0x0001F6D0 File Offset: 0x0001D8D0
		[DebuggerHidden]
		public <UploadFile>d__33(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0001F6E0 File Offset: 0x0001D8E0
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			int num = this.<>1__state;
			if (num == -3 || num == 1)
			{
				try
				{
				}
				finally
				{
					this.<>m__Finally1();
				}
			}
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0001F718 File Offset: 0x0001D918
		bool IEnumerator.MoveNext()
		{
			bool result;
			try
			{
				int num = this.<>1__state;
				ES3Cloud es3Cloud = this;
				if (num != 0)
				{
					if (num != 1)
					{
						result = false;
					}
					else
					{
						this.<>1__state = -3;
						es3Cloud.HandleError(webRequest, true);
						this.<>m__Finally1();
						webRequest = null;
						es3Cloud.isDone = true;
						result = false;
					}
				}
				else
				{
					this.<>1__state = -1;
					es3Cloud.Reset();
					WWWForm wwwform = es3Cloud.CreateWWWForm();
					wwwform.AddField("apiKey", es3Cloud.apiKey);
					wwwform.AddField("putFile", settings.path);
					wwwform.AddField("timestamp", fileTimestamp.ToString());
					wwwform.AddField("user", es3Cloud.GetUser(user, password));
					wwwform.AddBinaryData("data", bytes, "data.dat", "multipart/form-data");
					webRequest = UnityWebRequest.Post(es3Cloud.url, wwwform);
					this.<>1__state = -3;
					webRequest.timeout = es3Cloud.timeout;
					this.<>2__current = es3Cloud.SendWebRequest(webRequest);
					this.<>1__state = 1;
					result = true;
				}
			}
			catch
			{
				this.System.IDisposable.Dispose();
				throw;
			}
			return result;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0001F868 File Offset: 0x0001DA68
		private void <>m__Finally1()
		{
			this.<>1__state = -1;
			if (webRequest != null)
			{
				((IDisposable)webRequest).Dispose();
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x0001F884 File Offset: 0x0001DA84
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0001F88C File Offset: 0x0001DA8C
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x0001F893 File Offset: 0x0001DA93
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040001B9 RID: 441
		private int <>1__state;

		// Token: 0x040001BA RID: 442
		private object <>2__current;

		// Token: 0x040001BB RID: 443
		public ES3Cloud <>4__this;

		// Token: 0x040001BC RID: 444
		public ES3Settings settings;

		// Token: 0x040001BD RID: 445
		public long fileTimestamp;

		// Token: 0x040001BE RID: 446
		public string user;

		// Token: 0x040001BF RID: 447
		public string password;

		// Token: 0x040001C0 RID: 448
		public byte[] bytes;

		// Token: 0x040001C1 RID: 449
		private UnityWebRequest <webRequest>5__2;
	}

	// Token: 0x020000FD RID: 253
	[CompilerGenerated]
	private sealed class <DownloadFile>d__44 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x0600056D RID: 1389 RVA: 0x0001F89B File Offset: 0x0001DA9B
		[DebuggerHidden]
		public <DownloadFile>d__44(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0001F8AC File Offset: 0x0001DAAC
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			int num = this.<>1__state;
			if (num == -3 || num == 1)
			{
				try
				{
				}
				finally
				{
					this.<>m__Finally1();
				}
			}
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0001F8E4 File Offset: 0x0001DAE4
		bool IEnumerator.MoveNext()
		{
			bool result;
			try
			{
				int num = this.<>1__state;
				ES3Cloud es3Cloud = this;
				if (num != 0)
				{
					if (num != 1)
					{
						result = false;
					}
					else
					{
						this.<>1__state = -3;
						if (!es3Cloud.HandleError(webRequest, false))
						{
							if (webRequest.downloadedBytes > 0UL)
							{
								es3File.Clear();
								es3File.SaveRaw(webRequest.downloadHandler.data, null);
							}
							else
							{
								es3Cloud.error = string.Format("File {0} was not found on the server.", es3File.settings.path);
								es3Cloud.errorCode = 3L;
							}
						}
						this.<>m__Finally1();
						webRequest = null;
						es3Cloud.isDone = true;
						result = false;
					}
				}
				else
				{
					this.<>1__state = -1;
					es3Cloud.Reset();
					WWWForm wwwform = es3Cloud.CreateWWWForm();
					wwwform.AddField("apiKey", es3Cloud.apiKey);
					wwwform.AddField("getFile", es3File.settings.path);
					wwwform.AddField("user", es3Cloud.GetUser(user, password));
					if (timestamp > 0L)
					{
						wwwform.AddField("timestamp", timestamp.ToString());
					}
					webRequest = UnityWebRequest.Post(es3Cloud.url, wwwform);
					this.<>1__state = -3;
					webRequest.timeout = es3Cloud.timeout;
					this.<>2__current = es3Cloud.SendWebRequest(webRequest);
					this.<>1__state = 1;
					result = true;
				}
			}
			catch
			{
				this.System.IDisposable.Dispose();
				throw;
			}
			return result;
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0001FA8C File Offset: 0x0001DC8C
		private void <>m__Finally1()
		{
			this.<>1__state = -1;
			if (webRequest != null)
			{
				((IDisposable)webRequest).Dispose();
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x0001FAA8 File Offset: 0x0001DCA8
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0001FAB0 File Offset: 0x0001DCB0
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x0001FAB7 File Offset: 0x0001DCB7
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040001C2 RID: 450
		private int <>1__state;

		// Token: 0x040001C3 RID: 451
		private object <>2__current;

		// Token: 0x040001C4 RID: 452
		public ES3Cloud <>4__this;

		// Token: 0x040001C5 RID: 453
		public ES3File es3File;

		// Token: 0x040001C6 RID: 454
		public string user;

		// Token: 0x040001C7 RID: 455
		public string password;

		// Token: 0x040001C8 RID: 456
		public long timestamp;

		// Token: 0x040001C9 RID: 457
		private UnityWebRequest <webRequest>5__2;
	}

	// Token: 0x020000FE RID: 254
	[CompilerGenerated]
	private sealed class <DownloadFile>d__45 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06000574 RID: 1396 RVA: 0x0001FABF File Offset: 0x0001DCBF
		[DebuggerHidden]
		public <DownloadFile>d__45(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0001FAD0 File Offset: 0x0001DCD0
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			int num = this.<>1__state;
			if (num == -3 || num == 1)
			{
				try
				{
				}
				finally
				{
					this.<>m__Finally1();
				}
			}
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0001FB08 File Offset: 0x0001DD08
		bool IEnumerator.MoveNext()
		{
			bool result;
			try
			{
				int num = this.<>1__state;
				ES3Cloud es3Cloud = this;
				if (num != 0)
				{
					if (num != 1)
					{
						result = false;
					}
					else
					{
						this.<>1__state = -3;
						if (!es3Cloud.HandleError(webRequest, false))
						{
							if (webRequest.downloadedBytes > 0UL)
							{
								ES3.SaveRaw(webRequest.downloadHandler.data, settings);
							}
							else
							{
								es3Cloud.error = string.Format("File {0} was not found on the server.", settings.path);
								es3Cloud.errorCode = 3L;
							}
						}
						this.<>m__Finally1();
						webRequest = null;
						es3Cloud.isDone = true;
						result = false;
					}
				}
				else
				{
					this.<>1__state = -1;
					es3Cloud.Reset();
					WWWForm wwwform = es3Cloud.CreateWWWForm();
					wwwform.AddField("apiKey", es3Cloud.apiKey);
					wwwform.AddField("getFile", settings.path);
					wwwform.AddField("user", es3Cloud.GetUser(user, password));
					if (timestamp > 0L)
					{
						wwwform.AddField("timestamp", timestamp.ToString());
					}
					webRequest = UnityWebRequest.Post(es3Cloud.url, wwwform);
					this.<>1__state = -3;
					webRequest.timeout = es3Cloud.timeout;
					this.<>2__current = es3Cloud.SendWebRequest(webRequest);
					this.<>1__state = 1;
					result = true;
				}
			}
			catch
			{
				this.System.IDisposable.Dispose();
				throw;
			}
			return result;
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0001FC9C File Offset: 0x0001DE9C
		private void <>m__Finally1()
		{
			this.<>1__state = -1;
			if (webRequest != null)
			{
				((IDisposable)webRequest).Dispose();
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x0001FCB8 File Offset: 0x0001DEB8
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0001FCC0 File Offset: 0x0001DEC0
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x0001FCC7 File Offset: 0x0001DEC7
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040001CA RID: 458
		private int <>1__state;

		// Token: 0x040001CB RID: 459
		private object <>2__current;

		// Token: 0x040001CC RID: 460
		public ES3Cloud <>4__this;

		// Token: 0x040001CD RID: 461
		public ES3Settings settings;

		// Token: 0x040001CE RID: 462
		public string user;

		// Token: 0x040001CF RID: 463
		public string password;

		// Token: 0x040001D0 RID: 464
		public long timestamp;

		// Token: 0x040001D1 RID: 465
		private UnityWebRequest <webRequest>5__2;
	}

	// Token: 0x020000FF RID: 255
	[CompilerGenerated]
	private sealed class <DeleteFile>d__53 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x0600057B RID: 1403 RVA: 0x0001FCCF File Offset: 0x0001DECF
		[DebuggerHidden]
		public <DeleteFile>d__53(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0001FCE0 File Offset: 0x0001DEE0
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			int num = this.<>1__state;
			if (num == -3 || num == 1)
			{
				try
				{
				}
				finally
				{
					this.<>m__Finally1();
				}
			}
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0001FD18 File Offset: 0x0001DF18
		bool IEnumerator.MoveNext()
		{
			bool result;
			try
			{
				int num = this.<>1__state;
				ES3Cloud es3Cloud = this;
				if (num != 0)
				{
					if (num != 1)
					{
						result = false;
					}
					else
					{
						this.<>1__state = -3;
						es3Cloud.HandleError(webRequest, true);
						this.<>m__Finally1();
						webRequest = null;
						es3Cloud.isDone = true;
						result = false;
					}
				}
				else
				{
					this.<>1__state = -1;
					es3Cloud.Reset();
					WWWForm wwwform = es3Cloud.CreateWWWForm();
					wwwform.AddField("apiKey", es3Cloud.apiKey);
					wwwform.AddField("deleteFile", settings.path);
					wwwform.AddField("user", es3Cloud.GetUser(user, password));
					webRequest = UnityWebRequest.Post(es3Cloud.url, wwwform);
					this.<>1__state = -3;
					webRequest.timeout = es3Cloud.timeout;
					this.<>2__current = es3Cloud.SendWebRequest(webRequest);
					this.<>1__state = 1;
					result = true;
				}
			}
			catch
			{
				this.System.IDisposable.Dispose();
				throw;
			}
			return result;
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0001FE2C File Offset: 0x0001E02C
		private void <>m__Finally1()
		{
			this.<>1__state = -1;
			if (webRequest != null)
			{
				((IDisposable)webRequest).Dispose();
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x0001FE48 File Offset: 0x0001E048
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0001FE50 File Offset: 0x0001E050
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x0001FE57 File Offset: 0x0001E057
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040001D2 RID: 466
		private int <>1__state;

		// Token: 0x040001D3 RID: 467
		private object <>2__current;

		// Token: 0x040001D4 RID: 468
		public ES3Cloud <>4__this;

		// Token: 0x040001D5 RID: 469
		public ES3Settings settings;

		// Token: 0x040001D6 RID: 470
		public string user;

		// Token: 0x040001D7 RID: 471
		public string password;

		// Token: 0x040001D8 RID: 472
		private UnityWebRequest <webRequest>5__2;
	}

	// Token: 0x02000100 RID: 256
	[CompilerGenerated]
	private sealed class <RenameFile>d__60 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06000582 RID: 1410 RVA: 0x0001FE5F File Offset: 0x0001E05F
		[DebuggerHidden]
		public <RenameFile>d__60(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0001FE70 File Offset: 0x0001E070
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			int num = this.<>1__state;
			if (num == -3 || num == 1)
			{
				try
				{
				}
				finally
				{
					this.<>m__Finally1();
				}
			}
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0001FEA8 File Offset: 0x0001E0A8
		bool IEnumerator.MoveNext()
		{
			bool result;
			try
			{
				int num = this.<>1__state;
				ES3Cloud es3Cloud = this;
				if (num != 0)
				{
					if (num != 1)
					{
						result = false;
					}
					else
					{
						this.<>1__state = -3;
						es3Cloud.HandleError(webRequest, true);
						this.<>m__Finally1();
						webRequest = null;
						es3Cloud.isDone = true;
						result = false;
					}
				}
				else
				{
					this.<>1__state = -1;
					es3Cloud.Reset();
					WWWForm wwwform = es3Cloud.CreateWWWForm();
					wwwform.AddField("apiKey", es3Cloud.apiKey);
					wwwform.AddField("renameFile", settings.path);
					wwwform.AddField("newFilename", newSettings.path);
					wwwform.AddField("user", es3Cloud.GetUser(user, password));
					webRequest = UnityWebRequest.Post(es3Cloud.url, wwwform);
					this.<>1__state = -3;
					webRequest.timeout = es3Cloud.timeout;
					this.<>2__current = es3Cloud.SendWebRequest(webRequest);
					this.<>1__state = 1;
					result = true;
				}
			}
			catch
			{
				this.System.IDisposable.Dispose();
				throw;
			}
			return result;
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0001FFDC File Offset: 0x0001E1DC
		private void <>m__Finally1()
		{
			this.<>1__state = -1;
			if (webRequest != null)
			{
				((IDisposable)webRequest).Dispose();
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x0001FFF8 File Offset: 0x0001E1F8
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00020000 File Offset: 0x0001E200
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x00020007 File Offset: 0x0001E207
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040001D9 RID: 473
		private int <>1__state;

		// Token: 0x040001DA RID: 474
		private object <>2__current;

		// Token: 0x040001DB RID: 475
		public ES3Cloud <>4__this;

		// Token: 0x040001DC RID: 476
		public ES3Settings settings;

		// Token: 0x040001DD RID: 477
		public ES3Settings newSettings;

		// Token: 0x040001DE RID: 478
		public string user;

		// Token: 0x040001DF RID: 479
		public string password;

		// Token: 0x040001E0 RID: 480
		private UnityWebRequest <webRequest>5__2;
	}

	// Token: 0x02000101 RID: 257
	[CompilerGenerated]
	private sealed class <DownloadFilenames>d__61 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06000589 RID: 1417 RVA: 0x0002000F File Offset: 0x0001E20F
		[DebuggerHidden]
		public <DownloadFilenames>d__61(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00020020 File Offset: 0x0001E220
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			int num = this.<>1__state;
			if (num == -3 || num == 1)
			{
				try
				{
				}
				finally
				{
					this.<>m__Finally1();
				}
			}
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00020058 File Offset: 0x0001E258
		bool IEnumerator.MoveNext()
		{
			bool result;
			try
			{
				int num = this.<>1__state;
				ES3Cloud es3Cloud = this;
				if (num != 0)
				{
					if (num != 1)
					{
						result = false;
					}
					else
					{
						this.<>1__state = -3;
						if (!es3Cloud.HandleError(webRequest, false))
						{
							es3Cloud._data = webRequest.downloadHandler.data;
						}
						this.<>m__Finally1();
						webRequest = null;
						es3Cloud.isDone = true;
						result = false;
					}
				}
				else
				{
					this.<>1__state = -1;
					es3Cloud.Reset();
					WWWForm wwwform = es3Cloud.CreateWWWForm();
					wwwform.AddField("apiKey", es3Cloud.apiKey);
					wwwform.AddField("getFilenames", "");
					wwwform.AddField("user", es3Cloud.GetUser(user, password));
					webRequest = UnityWebRequest.Post(es3Cloud.url, wwwform);
					this.<>1__state = -3;
					webRequest.timeout = es3Cloud.timeout;
					this.<>2__current = es3Cloud.SendWebRequest(webRequest);
					this.<>1__state = 1;
					result = true;
				}
			}
			catch
			{
				this.System.IDisposable.Dispose();
				throw;
			}
			return result;
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0002017C File Offset: 0x0001E37C
		private void <>m__Finally1()
		{
			this.<>1__state = -1;
			if (webRequest != null)
			{
				((IDisposable)webRequest).Dispose();
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x00020198 File Offset: 0x0001E398
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x000201A0 File Offset: 0x0001E3A0
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x000201A7 File Offset: 0x0001E3A7
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040001E1 RID: 481
		private int <>1__state;

		// Token: 0x040001E2 RID: 482
		private object <>2__current;

		// Token: 0x040001E3 RID: 483
		public ES3Cloud <>4__this;

		// Token: 0x040001E4 RID: 484
		public string user;

		// Token: 0x040001E5 RID: 485
		public string password;

		// Token: 0x040001E6 RID: 486
		private UnityWebRequest <webRequest>5__2;
	}

	// Token: 0x02000102 RID: 258
	[CompilerGenerated]
	private sealed class <SearchFilenames>d__62 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06000590 RID: 1424 RVA: 0x000201AF File Offset: 0x0001E3AF
		[DebuggerHidden]
		public <SearchFilenames>d__62(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x000201C0 File Offset: 0x0001E3C0
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			int num = this.<>1__state;
			if (num == -3 || num == 1)
			{
				try
				{
				}
				finally
				{
					this.<>m__Finally1();
				}
			}
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x000201F8 File Offset: 0x0001E3F8
		bool IEnumerator.MoveNext()
		{
			bool result;
			try
			{
				int num = this.<>1__state;
				ES3Cloud es3Cloud = this;
				if (num != 0)
				{
					if (num != 1)
					{
						result = false;
					}
					else
					{
						this.<>1__state = -3;
						if (!es3Cloud.HandleError(webRequest, false))
						{
							es3Cloud._data = webRequest.downloadHandler.data;
						}
						this.<>m__Finally1();
						webRequest = null;
						es3Cloud.isDone = true;
						result = false;
					}
				}
				else
				{
					this.<>1__state = -1;
					es3Cloud.Reset();
					WWWForm wwwform = es3Cloud.CreateWWWForm();
					wwwform.AddField("apiKey", es3Cloud.apiKey);
					wwwform.AddField("getFilenames", "");
					wwwform.AddField("user", es3Cloud.GetUser(user, password));
					if (!string.IsNullOrEmpty(searchPattern))
					{
						wwwform.AddField("pattern", searchPattern);
					}
					webRequest = UnityWebRequest.Post(es3Cloud.url, wwwform);
					this.<>1__state = -3;
					webRequest.timeout = es3Cloud.timeout;
					this.<>2__current = es3Cloud.SendWebRequest(webRequest);
					this.<>1__state = 1;
					result = true;
				}
			}
			catch
			{
				this.System.IDisposable.Dispose();
				throw;
			}
			return result;
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00020348 File Offset: 0x0001E548
		private void <>m__Finally1()
		{
			this.<>1__state = -1;
			if (webRequest != null)
			{
				((IDisposable)webRequest).Dispose();
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x00020364 File Offset: 0x0001E564
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0002036C File Offset: 0x0001E56C
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x00020373 File Offset: 0x0001E573
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040001E7 RID: 487
		private int <>1__state;

		// Token: 0x040001E8 RID: 488
		private object <>2__current;

		// Token: 0x040001E9 RID: 489
		public ES3Cloud <>4__this;

		// Token: 0x040001EA RID: 490
		public string user;

		// Token: 0x040001EB RID: 491
		public string password;

		// Token: 0x040001EC RID: 492
		public string searchPattern;

		// Token: 0x040001ED RID: 493
		private UnityWebRequest <webRequest>5__2;
	}

	// Token: 0x02000103 RID: 259
	[CompilerGenerated]
	private sealed class <DownloadTimestamp>d__70 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06000597 RID: 1431 RVA: 0x0002037B File Offset: 0x0001E57B
		[DebuggerHidden]
		public <DownloadTimestamp>d__70(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0002038C File Offset: 0x0001E58C
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			int num = this.<>1__state;
			if (num == -3 || num == 1)
			{
				try
				{
				}
				finally
				{
					this.<>m__Finally1();
				}
			}
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x000203C4 File Offset: 0x0001E5C4
		bool IEnumerator.MoveNext()
		{
			bool result;
			try
			{
				int num = this.<>1__state;
				ES3Cloud es3Cloud = this;
				if (num != 0)
				{
					if (num != 1)
					{
						result = false;
					}
					else
					{
						this.<>1__state = -3;
						if (!es3Cloud.HandleError(webRequest, false))
						{
							es3Cloud._data = webRequest.downloadHandler.data;
						}
						this.<>m__Finally1();
						webRequest = null;
						es3Cloud.isDone = true;
						result = false;
					}
				}
				else
				{
					this.<>1__state = -1;
					es3Cloud.Reset();
					WWWForm wwwform = es3Cloud.CreateWWWForm();
					wwwform.AddField("apiKey", es3Cloud.apiKey);
					wwwform.AddField("getTimestamp", settings.path);
					wwwform.AddField("user", es3Cloud.GetUser(user, password));
					webRequest = UnityWebRequest.Post(es3Cloud.url, wwwform);
					this.<>1__state = -3;
					webRequest.timeout = es3Cloud.timeout;
					this.<>2__current = es3Cloud.SendWebRequest(webRequest);
					this.<>1__state = 1;
					result = true;
				}
			}
			catch
			{
				this.System.IDisposable.Dispose();
				throw;
			}
			return result;
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x000204FC File Offset: 0x0001E6FC
		private void <>m__Finally1()
		{
			this.<>1__state = -1;
			if (webRequest != null)
			{
				((IDisposable)webRequest).Dispose();
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x00020518 File Offset: 0x0001E718
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00020520 File Offset: 0x0001E720
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x00020527 File Offset: 0x0001E727
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040001EE RID: 494
		private int <>1__state;

		// Token: 0x040001EF RID: 495
		private object <>2__current;

		// Token: 0x040001F0 RID: 496
		public ES3Cloud <>4__this;

		// Token: 0x040001F1 RID: 497
		public ES3Settings settings;

		// Token: 0x040001F2 RID: 498
		public string user;

		// Token: 0x040001F3 RID: 499
		public string password;

		// Token: 0x040001F4 RID: 500
		private UnityWebRequest <webRequest>5__2;
	}
}
