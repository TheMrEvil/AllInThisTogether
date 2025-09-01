using System;
using Steamworks;
using TMPro;
using UnityEngine;

// Token: 0x02000171 RID: 369
public class JoinCodeInput : MonoBehaviour
{
	// Token: 0x06000FD2 RID: 4050 RVA: 0x00063C1F File Offset: 0x00061E1F
	private void Awake()
	{
		this.m_GamepadTextInputDismissed = Callback<GamepadTextInputDismissed_t>.Create(new Callback<GamepadTextInputDismissed_t>.DispatchDelegate(this.OnGamepadTextDismissed));
	}

	// Token: 0x06000FD3 RID: 4051 RVA: 0x00063C38 File Offset: 0x00061E38
	public void JoinCodeEntered()
	{
		if (PlatformSetup.Initialized)
		{
			try
			{
				SteamUtils.ShowGamepadTextInput(EGamepadTextInputMode.k_EGamepadTextInputModeNormal, EGamepadTextInputLineMode.k_EGamepadTextInputLineModeSingleLine, "", 1000U, "");
			}
			catch (Exception)
			{
			}
		}
	}

	// Token: 0x06000FD4 RID: 4052 RVA: 0x00063C78 File Offset: 0x00061E78
	private void OnGamepadTextDismissed(GamepadTextInputDismissed_t pCallback)
	{
		Debug.Log("Got text input dismissed!");
		if (!pCallback.m_bSubmitted)
		{
			return;
		}
		SteamUtils.GetEnteredGamepadTextLength();
		string text;
		SteamUtils.GetEnteredGamepadTextInput(out text, pCallback.m_unSubmittedText + 1U);
		Debug.Log("User entered text: " + text);
		if (this.InputField != null && this.InputField.isFocused)
		{
			this.InputField.text = text;
		}
	}

	// Token: 0x06000FD5 RID: 4053 RVA: 0x00063CE5 File Offset: 0x00061EE5
	public JoinCodeInput()
	{
	}

	// Token: 0x04000DF1 RID: 3569
	public TMP_InputField InputField;

	// Token: 0x04000DF2 RID: 3570
	protected Callback<GamepadTextInputDismissed_t> m_GamepadTextInputDismissed;
}
