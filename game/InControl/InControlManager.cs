using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InControl
{
	// Token: 0x02000021 RID: 33
	public class InControlManager : SingletonMonoBehavior<InControlManager>
	{
		// Token: 0x0600013A RID: 314 RVA: 0x0000525C File Offset: 0x0000345C
		private void OnEnable()
		{
			if (base.EnforceSingleton)
			{
				return;
			}
			InputManager.InvertYAxis = this.invertYAxis;
			InputManager.SuspendInBackground = this.suspendInBackground;
			InputManager.EnableICade = this.enableICade;
			InputManager.EnableXInput = this.enableXInput;
			InputManager.XInputUpdateRate = (uint)Mathf.Max(this.xInputUpdateRate, 0);
			InputManager.XInputBufferSize = (uint)Mathf.Max(this.xInputBufferSize, 0);
			InputManager.EnableNativeInput = this.enableNativeInput;
			InputManager.NativeInputEnableXInput = this.nativeInputEnableXInput;
			InputManager.NativeInputEnableMFi = this.nativeInputEnableMFi;
			InputManager.NativeInputUpdateRate = (uint)Mathf.Max(this.nativeInputUpdateRate, 0);
			InputManager.NativeInputPreventSleep = this.nativeInputPreventSleep;
			if (this.logDebugInfo)
			{
				Logger.OnLogMessage -= InControlManager.LogMessage;
				Logger.OnLogMessage += InControlManager.LogMessage;
			}
			InputManager.SetupInternal();
			SceneManager.sceneLoaded -= this.OnSceneWasLoaded;
			SceneManager.sceneLoaded += this.OnSceneWasLoaded;
			if (this.dontDestroyOnLoad)
			{
				UnityEngine.Object.DontDestroyOnLoad(this);
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000535D File Offset: 0x0000355D
		private void OnDisable()
		{
			if (base.IsNotTheSingleton)
			{
				return;
			}
			SceneManager.sceneLoaded -= this.OnSceneWasLoaded;
			InputManager.ResetInternal();
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000537E File Offset: 0x0000357E
		private void Update()
		{
			if (base.IsNotTheSingleton)
			{
				return;
			}
			if (this.applicationHasQuit)
			{
				return;
			}
			if (this.updateMode == InControlUpdateMode.Default || (this.updateMode == InControlUpdateMode.FixedUpdate && Utility.IsZero(Time.timeScale)))
			{
				InputManager.UpdateInternal();
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000053B4 File Offset: 0x000035B4
		private void FixedUpdate()
		{
			if (base.IsNotTheSingleton)
			{
				return;
			}
			if (this.applicationHasQuit)
			{
				return;
			}
			if (this.updateMode == InControlUpdateMode.FixedUpdate)
			{
				InputManager.UpdateInternal();
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000053D6 File Offset: 0x000035D6
		private void OnApplicationFocus(bool focusState)
		{
			if (base.IsNotTheSingleton)
			{
				return;
			}
			if (this.applicationHasQuit)
			{
				return;
			}
			InputManager.OnApplicationFocus(focusState);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x000053F0 File Offset: 0x000035F0
		private void OnApplicationPause(bool pauseState)
		{
			if (base.IsNotTheSingleton)
			{
				return;
			}
			if (this.applicationHasQuit)
			{
				return;
			}
			InputManager.OnApplicationPause(pauseState);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000540A File Offset: 0x0000360A
		private void OnApplicationQuit()
		{
			if (base.IsNotTheSingleton)
			{
				return;
			}
			if (this.applicationHasQuit)
			{
				return;
			}
			InputManager.OnApplicationQuit();
			this.applicationHasQuit = true;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000542A File Offset: 0x0000362A
		private void OnSceneWasLoaded(Scene scene, LoadSceneMode loadSceneMode)
		{
			if (base.IsNotTheSingleton)
			{
				return;
			}
			if (this.applicationHasQuit)
			{
				return;
			}
			if (loadSceneMode == LoadSceneMode.Single)
			{
				InputManager.OnLevelWasLoaded();
			}
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00005448 File Offset: 0x00003648
		private static void LogMessage(LogMessage logMessage)
		{
			switch (logMessage.Type)
			{
			case LogMessageType.Info:
				Debug.Log(logMessage.Text);
				return;
			case LogMessageType.Warning:
				Debug.LogWarning(logMessage.Text);
				return;
			case LogMessageType.Error:
				Debug.LogError(logMessage.Text);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00005492 File Offset: 0x00003692
		public InControlManager()
		{
		}

		// Token: 0x0400012B RID: 299
		public bool logDebugInfo = true;

		// Token: 0x0400012C RID: 300
		public bool invertYAxis;

		// Token: 0x0400012D RID: 301
		[SerializeField]
		private bool useFixedUpdate;

		// Token: 0x0400012E RID: 302
		public bool dontDestroyOnLoad = true;

		// Token: 0x0400012F RID: 303
		public bool suspendInBackground;

		// Token: 0x04000130 RID: 304
		public InControlUpdateMode updateMode;

		// Token: 0x04000131 RID: 305
		public bool enableICade;

		// Token: 0x04000132 RID: 306
		public bool enableXInput;

		// Token: 0x04000133 RID: 307
		public bool xInputOverrideUpdateRate;

		// Token: 0x04000134 RID: 308
		public int xInputUpdateRate;

		// Token: 0x04000135 RID: 309
		public bool xInputOverrideBufferSize;

		// Token: 0x04000136 RID: 310
		public int xInputBufferSize;

		// Token: 0x04000137 RID: 311
		public bool enableNativeInput = true;

		// Token: 0x04000138 RID: 312
		public bool nativeInputEnableXInput = true;

		// Token: 0x04000139 RID: 313
		public bool nativeInputEnableMFi = true;

		// Token: 0x0400013A RID: 314
		public bool nativeInputPreventSleep;

		// Token: 0x0400013B RID: 315
		public bool nativeInputOverrideUpdateRate;

		// Token: 0x0400013C RID: 316
		public int nativeInputUpdateRate;

		// Token: 0x0400013D RID: 317
		private bool applicationHasQuit;
	}
}
