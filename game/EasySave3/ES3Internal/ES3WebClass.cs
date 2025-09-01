using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

namespace ES3Internal
{
	// Token: 0x020000E6 RID: 230
	public class ES3WebClass
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x0001EA78 File Offset: 0x0001CC78
		public float uploadProgress
		{
			get
			{
				if (this._webRequest == null)
				{
					return 0f;
				}
				return this._webRequest.uploadProgress;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x0001EA93 File Offset: 0x0001CC93
		public float downloadProgress
		{
			get
			{
				if (this._webRequest == null)
				{
					return 0f;
				}
				return this._webRequest.downloadProgress;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x0001EAAE File Offset: 0x0001CCAE
		public bool isError
		{
			get
			{
				return !string.IsNullOrEmpty(this.error) || this.errorCode > 0L;
			}
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0001EAC9 File Offset: 0x0001CCC9
		public static bool IsNetworkError(UnityWebRequest www)
		{
			return www.result == UnityWebRequest.Result.ConnectionError;
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0001EAD4 File Offset: 0x0001CCD4
		protected ES3WebClass(string url, string apiKey)
		{
			this.url = url;
			this.apiKey = apiKey;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0001EAF5 File Offset: 0x0001CCF5
		public void AddPOSTField(string fieldName, string value)
		{
			this.formData.Add(new KeyValuePair<string, string>(fieldName, value));
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0001EB09 File Offset: 0x0001CD09
		protected string GetUser(string user, string password)
		{
			if (string.IsNullOrEmpty(user))
			{
				return "";
			}
			if (!string.IsNullOrEmpty(password))
			{
				user += password;
			}
			user = ES3Hash.SHA1Hash(user);
			return user;
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0001EB34 File Offset: 0x0001CD34
		protected WWWForm CreateWWWForm()
		{
			WWWForm wwwform = new WWWForm();
			foreach (KeyValuePair<string, string> keyValuePair in this.formData)
			{
				wwwform.AddField(keyValuePair.Key, keyValuePair.Value);
			}
			return wwwform;
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0001EB9C File Offset: 0x0001CD9C
		protected bool HandleError(UnityWebRequest webRequest, bool errorIfDataIsDownloaded)
		{
			if (ES3WebClass.IsNetworkError(webRequest))
			{
				this.errorCode = 1L;
				this.error = "Error: " + webRequest.error;
			}
			else if (webRequest.responseCode >= 400L)
			{
				this.errorCode = webRequest.responseCode;
				if (string.IsNullOrEmpty(webRequest.downloadHandler.text))
				{
					this.error = string.Format("Server returned {0} error with no message", webRequest.responseCode);
				}
				else
				{
					this.error = webRequest.downloadHandler.text;
				}
			}
			else
			{
				if (!errorIfDataIsDownloaded || webRequest.downloadedBytes <= 0UL)
				{
					return false;
				}
				this.errorCode = 2L;
				this.error = "Server error: '" + webRequest.downloadHandler.text + "'";
			}
			return true;
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0001EC6A File Offset: 0x0001CE6A
		protected IEnumerator SendWebRequest(UnityWebRequest webRequest)
		{
			this._webRequest = webRequest;
			yield return webRequest.SendWebRequest();
			yield break;
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0001EC80 File Offset: 0x0001CE80
		protected virtual void Reset()
		{
			this.error = null;
			this.errorCode = 0L;
			this.isDone = false;
		}

		// Token: 0x04000164 RID: 356
		protected string url;

		// Token: 0x04000165 RID: 357
		protected string apiKey;

		// Token: 0x04000166 RID: 358
		protected List<KeyValuePair<string, string>> formData = new List<KeyValuePair<string, string>>();

		// Token: 0x04000167 RID: 359
		protected UnityWebRequest _webRequest;

		// Token: 0x04000168 RID: 360
		public bool isDone;

		// Token: 0x04000169 RID: 361
		public string error;

		// Token: 0x0400016A RID: 362
		public long errorCode;

		// Token: 0x02000112 RID: 274
		[CompilerGenerated]
		private sealed class <SendWebRequest>d__19 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x060005CC RID: 1484 RVA: 0x000208C2 File Offset: 0x0001EAC2
			[DebuggerHidden]
			public <SendWebRequest>d__19(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x060005CD RID: 1485 RVA: 0x000208D1 File Offset: 0x0001EAD1
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060005CE RID: 1486 RVA: 0x000208D4 File Offset: 0x0001EAD4
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				ES3WebClass es3WebClass = this;
				if (num == 0)
				{
					this.<>1__state = -1;
					es3WebClass._webRequest = webRequest;
					this.<>2__current = webRequest.SendWebRequest();
					this.<>1__state = 1;
					return true;
				}
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				return false;
			}

			// Token: 0x17000036 RID: 54
			// (get) Token: 0x060005CF RID: 1487 RVA: 0x0002092D File Offset: 0x0001EB2D
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060005D0 RID: 1488 RVA: 0x00020935 File Offset: 0x0001EB35
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000037 RID: 55
			// (get) Token: 0x060005D1 RID: 1489 RVA: 0x0002093C File Offset: 0x0001EB3C
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000217 RID: 535
			private int <>1__state;

			// Token: 0x04000218 RID: 536
			private object <>2__current;

			// Token: 0x04000219 RID: 537
			public ES3WebClass <>4__this;

			// Token: 0x0400021A RID: 538
			public UnityWebRequest webRequest;
		}
	}
}
