using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

// Token: 0x02000134 RID: 308
public class ChatAPI : MonoBehaviour
{
	// Token: 0x06000E39 RID: 3641 RVA: 0x0005A494 File Offset: 0x00058694
	public void Post()
	{
		UnityWebRequest request = ChatAPI.CreateRequest();
		base.StartCoroutine(this.ListenForResponse(request, new Action<string>(this.TextUpdated)));
	}

	// Token: 0x06000E3A RID: 3642 RVA: 0x0005A4C1 File Offset: 0x000586C1
	private IEnumerator ListenForResponse(UnityWebRequest request, Action<string> OnTextUpdated)
	{
		this.curBuilder = "";
		request.SendWebRequest();
		while (!request.isDone)
		{
			string text = request.downloadHandler.text;
			if (!string.IsNullOrEmpty(text) && !(this.curBuilder == text))
			{
				this.curBuilder = text;
				if (OnTextUpdated != null)
				{
					OnTextUpdated(text);
				}
				UnityEngine.Debug.Log("Got Text Update");
				yield return true;
			}
		}
		if (request.error != null && request.error.Length > 2)
		{
			UnityEngine.Debug.LogError(request.error);
		}
		else
		{
			UnityEngine.Debug.Log("Server Response: " + request.downloadHandler.text);
		}
		yield break;
	}

	// Token: 0x06000E3B RID: 3643 RVA: 0x0005A4DE File Offset: 0x000586DE
	private void TextUpdated(string text)
	{
		this.textOut.text = text;
	}

	// Token: 0x06000E3C RID: 3644 RVA: 0x0005A4EC File Offset: 0x000586EC
	private static UnityWebRequest CreateRequest()
	{
		WWWForm wwwform = new WWWForm();
		wwwform.AddField("players", "Blueteak, Fletch");
		UnityWebRequest unityWebRequest = new UnityWebRequest("http://54.197.14.169:3000/generate-text", "POST");
		unityWebRequest.uploadHandler = new UploadHandlerRaw(wwwform.data);
		unityWebRequest.downloadHandler = new DownloadHandlerBuffer();
		unityWebRequest.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
		unityWebRequest.timeout = 0;
		return unityWebRequest;
	}

	// Token: 0x06000E3D RID: 3645 RVA: 0x0005A551 File Offset: 0x00058751
	public ChatAPI()
	{
	}

	// Token: 0x04000BA2 RID: 2978
	public const string POST_URL = "http://54.197.14.169:3000/generate-text";

	// Token: 0x04000BA3 RID: 2979
	public TextMeshProUGUI textOut;

	// Token: 0x04000BA4 RID: 2980
	private string curBuilder = "";

	// Token: 0x02000538 RID: 1336
	[CompilerGenerated]
	private sealed class <ListenForResponse>d__4 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002419 RID: 9241 RVA: 0x000CCFA3 File Offset: 0x000CB1A3
		[DebuggerHidden]
		public <ListenForResponse>d__4(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x0600241A RID: 9242 RVA: 0x000CCFB2 File Offset: 0x000CB1B2
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x0600241B RID: 9243 RVA: 0x000CCFB4 File Offset: 0x000CB1B4
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			ChatAPI chatAPI = this;
			if (num != 0)
			{
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
			}
			else
			{
				this.<>1__state = -1;
				chatAPI.curBuilder = "";
				request.SendWebRequest();
			}
			while (!request.isDone)
			{
				string text = request.downloadHandler.text;
				if (!string.IsNullOrEmpty(text) && !(chatAPI.curBuilder == text))
				{
					chatAPI.curBuilder = text;
					Action<string> action = OnTextUpdated;
					if (action != null)
					{
						action(text);
					}
					UnityEngine.Debug.Log("Got Text Update");
					this.<>2__current = true;
					this.<>1__state = 1;
					return true;
				}
			}
			if (request.error != null && request.error.Length > 2)
			{
				UnityEngine.Debug.LogError(request.error);
			}
			else
			{
				UnityEngine.Debug.Log("Server Response: " + request.downloadHandler.text);
			}
			return false;
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x0600241C RID: 9244 RVA: 0x000CD0C0 File Offset: 0x000CB2C0
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0600241D RID: 9245 RVA: 0x000CD0C8 File Offset: 0x000CB2C8
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x0600241E RID: 9246 RVA: 0x000CD0CF File Offset: 0x000CB2CF
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002654 RID: 9812
		private int <>1__state;

		// Token: 0x04002655 RID: 9813
		private object <>2__current;

		// Token: 0x04002656 RID: 9814
		public ChatAPI <>4__this;

		// Token: 0x04002657 RID: 9815
		public UnityWebRequest request;

		// Token: 0x04002658 RID: 9816
		public Action<string> OnTextUpdated;
	}
}
