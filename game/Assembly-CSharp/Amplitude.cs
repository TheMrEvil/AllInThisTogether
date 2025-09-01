using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

// Token: 0x02000103 RID: 259
public class Amplitude : MonoBehaviour
{
	// Token: 0x06000C18 RID: 3096 RVA: 0x0004E718 File Offset: 0x0004C918
	private void Awake()
	{
		Amplitude.instance = this;
		if (this.InitializeOnAwake && this.APIKey.Length > 8)
		{
			Amplitude.Initialize(Application.version);
		}
	}

	// Token: 0x06000C19 RID: 3097 RVA: 0x0004E740 File Offset: 0x0004C940
	private void OnEnable()
	{
		base.InvokeRepeating("CheckEventQueue", 0.25f, 1f);
	}

	// Token: 0x06000C1A RID: 3098 RVA: 0x0004E758 File Offset: 0x0004C958
	public static void Initialize(string AppVersion)
	{
		if (Amplitude.instance == null)
		{
			UnityEngine.Debug.LogError("AMP: No instance of Amplitude found, make sure it's enabled in your scene hierarchy!");
			return;
		}
		if (Amplitude.WasInitialized)
		{
			return;
		}
		if (PlayerPrefs.HasKey("amp_deviceID"))
		{
			Amplitude.instance.device_id = PlayerPrefs.GetString("amp_deviceID");
			if (Amplitude.instance.DebugMessages)
			{
				UnityEngine.Debug.Log("AMP: Found existing Device ID [" + Amplitude.instance.device_id + "]");
			}
		}
		else
		{
			Amplitude.instance.device_id = Guid.NewGuid().ToString();
			PlayerPrefs.SetString("amp_deviceID", Amplitude.instance.device_id);
			PlayerPrefs.Save();
			if (Amplitude.instance.DebugMessages)
			{
				UnityEngine.Debug.Log("AMP: No Device ID found, creating new: [" + Amplitude.instance.device_id + "]");
			}
		}
		if (Amplitude.instance.user_id == null)
		{
			Amplitude.instance.user_id = "";
		}
		Amplitude.instance.app_version = AppVersion + Amplitude.instance.gameObject.GetComponent<NetworkManager>().versionAdd;
		Amplitude.instance.sessionid = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
		Amplitude.instance.platform = "windows";
		if (Amplitude.instance.RecordIP)
		{
			Amplitude.instance.ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList.First((IPAddress f) => f.AddressFamily == AddressFamily.InterNetwork).ToString();
		}
		Amplitude.WasInitialized = true;
		Amplitude.SendEvent(new AmplitudeEvent("launch"));
	}

	// Token: 0x06000C1B RID: 3099 RVA: 0x0004E8FC File Offset: 0x0004CAFC
	public static void SetUserID(string userID)
	{
		if (Amplitude.instance != null)
		{
			Amplitude.instance.user_id = userID;
		}
	}

	// Token: 0x06000C1C RID: 3100 RVA: 0x0004E916 File Offset: 0x0004CB16
	public static void SendEvent(AmplitudeEvent analyticsEvent)
	{
		Amplitude.instance.eventQueue.Enqueue(analyticsEvent);
	}

	// Token: 0x06000C1D RID: 3101 RVA: 0x0004E928 File Offset: 0x0004CB28
	private void CheckEventQueue()
	{
		if (this.eventQueue.Count == 0)
		{
			return;
		}
		this.timeSinceLastSend++;
		if (this.timeSinceLastSend >= Amplitude.instance.MaxSecondsPerBatch || this.eventQueue.Count >= this.MaxEventsPerBatch)
		{
			List<AmplitudeEvent> list = new List<AmplitudeEvent>();
			int num = Mathf.Min(this.MaxEventsPerBatch, this.eventQueue.Count);
			for (int i = 0; i < num; i++)
			{
				list.Add(this.eventQueue.Dequeue());
			}
			this.SendEvents(list);
			this.timeSinceLastSend = 0;
		}
	}

	// Token: 0x06000C1E RID: 3102 RVA: 0x0004E9C0 File Offset: 0x0004CBC0
	private void SendEvents(List<AmplitudeEvent> events)
	{
		Amplitude.<SendEvents>d__23 <SendEvents>d__;
		<SendEvents>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
		<SendEvents>d__.<>4__this = this;
		<SendEvents>d__.events = events;
		<SendEvents>d__.<>1__state = -1;
		<SendEvents>d__.<>t__builder.Start<Amplitude.<SendEvents>d__23>(ref <SendEvents>d__);
	}

	// Token: 0x06000C1F RID: 3103 RVA: 0x0004EA00 File Offset: 0x0004CC00
	private string JSONFromEvents(List<AmplitudeEvent> events)
	{
		string text = "";
		foreach (AmplitudeEvent amplitudeEvent in events)
		{
			text = text + amplitudeEvent.ToJSON() + ",";
		}
		text = text.Substring(0, text.Length - 1);
		return string.Concat(new string[]
		{
			"{\"api_key\":\"",
			this.APIKey,
			"\", \"events\": [",
			text,
			"]}"
		});
	}

	// Token: 0x06000C20 RID: 3104 RVA: 0x0004EAA0 File Offset: 0x0004CCA0
	private void OnDisable()
	{
		base.CancelInvoke();
	}

	// Token: 0x06000C21 RID: 3105 RVA: 0x0004EAA8 File Offset: 0x0004CCA8
	public Amplitude()
	{
	}

	// Token: 0x040009D1 RID: 2513
	public const string URL_BASE = "https://api.amplitude.com/2/httpapi";

	// Token: 0x040009D2 RID: 2514
	[Tooltip("Amplitude Web API Key")]
	public string APIKey;

	// Token: 0x040009D3 RID: 2515
	[Tooltip("Max events allowed per web request")]
	[Range(0f, 10f)]
	public int MaxEventsPerBatch = 5;

	// Token: 0x040009D4 RID: 2516
	[Tooltip("Max seconds to wait before sending event, even if the batch is not full")]
	public int MaxSecondsPerBatch = 10;

	// Token: 0x040009D5 RID: 2517
	[Tooltip("Record IP Address for events")]
	public bool RecordIP;

	// Token: 0x040009D6 RID: 2518
	[Tooltip("Initialize Analytics data on startup")]
	public bool InitializeOnAwake = true;

	// Token: 0x040009D7 RID: 2519
	[Tooltip("Send Debug.Log Info messages, does not disable error messages")]
	public bool DebugMessages = true;

	// Token: 0x040009D8 RID: 2520
	public static Amplitude instance;

	// Token: 0x040009D9 RID: 2521
	public static bool WasInitialized;

	// Token: 0x040009DA RID: 2522
	private Queue<AmplitudeEvent> eventQueue = new Queue<AmplitudeEvent>();

	// Token: 0x040009DB RID: 2523
	[NonSerialized]
	public string user_id = "";

	// Token: 0x040009DC RID: 2524
	[NonSerialized]
	public string device_id = "";

	// Token: 0x040009DD RID: 2525
	[NonSerialized]
	public string app_version = "0";

	// Token: 0x040009DE RID: 2526
	[NonSerialized]
	public string platform = "";

	// Token: 0x040009DF RID: 2527
	[NonSerialized]
	public long sessionid = -1L;

	// Token: 0x040009E0 RID: 2528
	[NonSerialized]
	public string ip = "";

	// Token: 0x040009E1 RID: 2529
	private int timeSinceLastSend;

	// Token: 0x020004FB RID: 1275
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		// Token: 0x06002369 RID: 9065 RVA: 0x000C9CF0 File Offset: 0x000C7EF0
		// Note: this type is marked as 'beforefieldinit'.
		static <>c()
		{
		}

		// Token: 0x0600236A RID: 9066 RVA: 0x000C9CFC File Offset: 0x000C7EFC
		public <>c()
		{
		}

		// Token: 0x0600236B RID: 9067 RVA: 0x000C9D04 File Offset: 0x000C7F04
		internal bool <Initialize>b__19_0(IPAddress f)
		{
			return f.AddressFamily == AddressFamily.InterNetwork;
		}

		// Token: 0x04002550 RID: 9552
		public static readonly Amplitude.<>c <>9 = new Amplitude.<>c();

		// Token: 0x04002551 RID: 9553
		public static Func<IPAddress, bool> <>9__19_0;
	}

	// Token: 0x020004FC RID: 1276
	[CompilerGenerated]
	[StructLayout(LayoutKind.Auto)]
	private struct <SendEvents>d__23 : IAsyncStateMachine
	{
		// Token: 0x0600236C RID: 9068 RVA: 0x000C9D10 File Offset: 0x000C7F10
		void IAsyncStateMachine.MoveNext()
		{
			int num = this.<>1__state;
			Amplitude amplitude = this.<>4__this;
			try
			{
				if (num != 0)
				{
					string text = amplitude.JSONFromEvents(this.events);
					if (amplitude.DebugMessages)
					{
						UnityEngine.Debug.Log("AMP: Sending JSON:\n" + text);
					}
					this.<www>5__2 = UnityWebRequest.Put("https://api.amplitude.com/2/httpapi", text);
				}
				try
				{
					UnityWebRequestAwaiter unityWebRequestAwaiter;
					if (num != 0)
					{
						this.<www>5__2.method = "POST";
						this.<www>5__2.SetRequestHeader("Content-Type", "application/json");
						this.<www>5__2.SetRequestHeader("Accept", "application/json");
						unityWebRequestAwaiter = this.<www>5__2.SendWebRequest().GetAwaiter();
						if (!unityWebRequestAwaiter.IsCompleted)
						{
							num = (this.<>1__state = 0);
							this.<>u__1 = unityWebRequestAwaiter;
							this.<>t__builder.AwaitOnCompleted<UnityWebRequestAwaiter, Amplitude.<SendEvents>d__23>(ref unityWebRequestAwaiter, ref this);
							return;
						}
					}
					else
					{
						unityWebRequestAwaiter = (UnityWebRequestAwaiter)this.<>u__1;
						this.<>u__1 = null;
						num = (this.<>1__state = -1);
					}
					unityWebRequestAwaiter.GetResult();
					if (this.<www>5__2.result != UnityWebRequest.Result.Success)
					{
						UnityEngine.Debug.LogError("AMP: " + this.<www>5__2.error);
					}
					else if (amplitude.DebugMessages)
					{
						UnityEngine.Debug.Log("AMP: Successfully submitted analytics events");
					}
				}
				finally
				{
					if (num < 0 && this.<www>5__2 != null)
					{
						((IDisposable)this.<www>5__2).Dispose();
					}
				}
				this.<www>5__2 = null;
			}
			catch (Exception exception)
			{
				this.<>1__state = -2;
				this.<>t__builder.SetException(exception);
				return;
			}
			this.<>1__state = -2;
			this.<>t__builder.SetResult();
		}

		// Token: 0x0600236D RID: 9069 RVA: 0x000C9EC0 File Offset: 0x000C80C0
		[DebuggerHidden]
		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			this.<>t__builder.SetStateMachine(stateMachine);
		}

		// Token: 0x04002552 RID: 9554
		public int <>1__state;

		// Token: 0x04002553 RID: 9555
		public AsyncVoidMethodBuilder <>t__builder;

		// Token: 0x04002554 RID: 9556
		public Amplitude <>4__this;

		// Token: 0x04002555 RID: 9557
		public List<AmplitudeEvent> events;

		// Token: 0x04002556 RID: 9558
		private UnityWebRequest <www>5__2;

		// Token: 0x04002557 RID: 9559
		private object <>u__1;
	}
}
