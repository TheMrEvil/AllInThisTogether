using System;
using InControl;
using UnityEngine;

namespace BindingsExample
{
	// Token: 0x02000008 RID: 8
	public class BindingsExample : MonoBehaviour
	{
		// Token: 0x0600001B RID: 27 RVA: 0x000028B3 File Offset: 0x00000AB3
		private void OnEnable()
		{
			this.playerActions = PlayerActions.CreateWithDefaultBindings();
			this.LoadBindings();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000028C6 File Offset: 0x00000AC6
		private void OnDisable()
		{
			this.playerActions.Destroy();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000028D3 File Offset: 0x00000AD3
		private void Start()
		{
			this.cachedRenderer = base.GetComponent<Renderer>();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000028E4 File Offset: 0x00000AE4
		private void Update()
		{
			base.transform.Rotate(Vector3.down, 500f * Time.deltaTime * this.playerActions.Move.X, Space.World);
			base.transform.Rotate(Vector3.right, 500f * Time.deltaTime * this.playerActions.Move.Y, Space.World);
			Color a = this.playerActions.Fire.IsPressed ? Color.red : Color.white;
			Color b = this.playerActions.Jump.IsPressed ? Color.green : Color.white;
			this.cachedRenderer.material.color = Color.Lerp(a, b, 0.5f);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000029A5 File Offset: 0x00000BA5
		private void SaveBindings()
		{
			this.saveData = this.playerActions.Save();
			PlayerPrefs.SetString("Bindings", this.saveData);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000029C8 File Offset: 0x00000BC8
		private void LoadBindings()
		{
			if (PlayerPrefs.HasKey("Bindings"))
			{
				this.saveData = PlayerPrefs.GetString("Bindings");
				this.playerActions.Load(this.saveData);
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000029F7 File Offset: 0x00000BF7
		private void OnApplicationQuit()
		{
			PlayerPrefs.Save();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002A00 File Offset: 0x00000C00
		private void OnGUI()
		{
			float num = 10f;
			GUI.Label(new Rect(10f, num, 300f, num + 22f), "Last Input Type: " + this.playerActions.LastInputType.ToString());
			num += 22f;
			GUI.Label(new Rect(10f, num, 300f, num + 22f), "Last Device Class: " + this.playerActions.LastDeviceClass.ToString());
			num += 22f;
			GUI.Label(new Rect(10f, num, 300f, num + 22f), "Last Device Style: " + this.playerActions.LastDeviceStyle.ToString());
			num += 22f;
			int count = this.playerActions.Actions.Count;
			for (int i = 0; i < count; i++)
			{
				PlayerAction playerAction = this.playerActions.Actions[i];
				string text = playerAction.Name;
				if (playerAction.IsListeningForBinding)
				{
					text += " (Listening)";
				}
				text = text + " = " + playerAction.Value.ToString();
				GUI.Label(new Rect(10f, num, 500f, num + 22f), text);
				num += 22f;
				int count2 = playerAction.Bindings.Count;
				for (int j = 0; j < count2; j++)
				{
					BindingSource bindingSource = playerAction.Bindings[j];
					GUI.Label(new Rect(75f, num, 300f, num + 22f), bindingSource.DeviceName + ": " + bindingSource.Name);
					if (GUI.Button(new Rect(20f, num + 3f, 20f, 17f), "-"))
					{
						playerAction.RemoveBinding(bindingSource);
					}
					if (GUI.Button(new Rect(45f, num + 3f, 20f, 17f), "+"))
					{
						playerAction.ListenForBindingReplacing(bindingSource);
					}
					num += 22f;
				}
				if (GUI.Button(new Rect(20f, num + 3f, 20f, 17f), "+"))
				{
					playerAction.ListenForBinding();
				}
				if (GUI.Button(new Rect(50f, num + 3f, 50f, 17f), "Reset"))
				{
					playerAction.ResetBindings();
				}
				num += 25f;
			}
			if (GUI.Button(new Rect(20f, num + 3f, 50f, 22f), "Load"))
			{
				this.LoadBindings();
			}
			if (GUI.Button(new Rect(80f, num + 3f, 50f, 22f), "Save"))
			{
				this.SaveBindings();
			}
			if (GUI.Button(new Rect(140f, num + 3f, 50f, 22f), "Reset"))
			{
				this.playerActions.Reset();
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002D30 File Offset: 0x00000F30
		public BindingsExample()
		{
		}

		// Token: 0x04000012 RID: 18
		private Renderer cachedRenderer;

		// Token: 0x04000013 RID: 19
		private PlayerActions playerActions;

		// Token: 0x04000014 RID: 20
		private string saveData;
	}
}
