using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine.Events;

// Token: 0x0200010B RID: 267
public class StateControl : MonoBehaviourPunCallbacks
{
	// Token: 0x06000C9C RID: 3228 RVA: 0x0005142B File Offset: 0x0004F62B
	private void Awake()
	{
		StateControl.controllers.Add(this);
	}

	// Token: 0x06000C9D RID: 3229 RVA: 0x00051438 File Offset: 0x0004F638
	public void ChangeState(float val)
	{
		if (this.stateType != StateType.Number || val == this.currentNumber)
		{
			return;
		}
		StateManager.SetValue(this.stateKey, val);
	}

	// Token: 0x06000C9E RID: 3230 RVA: 0x0005145D File Offset: 0x0004F65D
	public void ChangeState(bool val)
	{
		if (this.stateType != StateType.Bool || val == this.currentBool)
		{
			return;
		}
		StateManager.SetValue(this.stateKey, val);
	}

	// Token: 0x06000C9F RID: 3231 RVA: 0x00051484 File Offset: 0x0004F684
	public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
	{
		bool flag = false;
		foreach (DictionaryEntry dictionaryEntry in propertiesThatChanged)
		{
			flag |= (dictionaryEntry.Key.ToString() == this.stateKey);
		}
		if (!flag)
		{
			return;
		}
		this.CheckState();
	}

	// Token: 0x06000CA0 RID: 3232 RVA: 0x000514F4 File Offset: 0x0004F6F4
	public override void OnJoinedRoom()
	{
		this.CheckState();
	}

	// Token: 0x06000CA1 RID: 3233 RVA: 0x000514FC File Offset: 0x0004F6FC
	public void Reset()
	{
		if (this.stateType == StateType.Number)
		{
			StateManager.SetValue(this.stateKey, this.defaultNumber);
			this.ApplyState(this.defaultNumber);
		}
		if (this.stateType == StateType.Bool)
		{
			StateManager.SetValue(this.stateKey, this.defaultBool);
			this.ApplyState(this.defaultBool);
		}
	}

	// Token: 0x06000CA2 RID: 3234 RVA: 0x00051560 File Offset: 0x0004F760
	private void CheckState()
	{
		if (!PhotonNetwork.InRoom)
		{
			return;
		}
		if (this.stateType == StateType.Bool)
		{
			this.ApplyState(StateManager.GetBool(this.stateKey, this.defaultBool));
			return;
		}
		if (this.stateType == StateType.Number)
		{
			this.ApplyState(StateManager.GetNumber(this.stateKey, this.defaultNumber));
		}
	}

	// Token: 0x06000CA3 RID: 3235 RVA: 0x000515B5 File Offset: 0x0004F7B5
	private void ApplyState(float val)
	{
		if (val == this.currentNumber)
		{
			return;
		}
		this.currentNumber = val;
		this.numberEvent.Invoke(val);
		if (this.numberAction != null)
		{
			this.numberAction(val);
		}
	}

	// Token: 0x06000CA4 RID: 3236 RVA: 0x000515E8 File Offset: 0x0004F7E8
	private void ApplyState(bool val)
	{
		if (val == this.currentBool)
		{
			return;
		}
		this.currentBool = val;
		if (val)
		{
			this.OnActivate.Invoke();
		}
		else
		{
			this.OnDeactivate.Invoke();
		}
		if (this.boolAction != null)
		{
			this.boolAction(val);
		}
	}

	// Token: 0x06000CA5 RID: 3237 RVA: 0x00051635 File Offset: 0x0004F835
	private void OnDestroy()
	{
		StateControl.controllers.Remove(this);
	}

	// Token: 0x06000CA6 RID: 3238 RVA: 0x00051644 File Offset: 0x0004F844
	public static void ResetToDefault()
	{
		foreach (StateControl stateControl in StateControl.controllers)
		{
			stateControl.Reset();
		}
	}

	// Token: 0x06000CA7 RID: 3239 RVA: 0x00051694 File Offset: 0x0004F894
	public StateControl()
	{
	}

	// Token: 0x06000CA8 RID: 3240 RVA: 0x0005169C File Offset: 0x0004F89C
	// Note: this type is marked as 'beforefieldinit'.
	static StateControl()
	{
	}

	// Token: 0x04000A0B RID: 2571
	public string stateKey;

	// Token: 0x04000A0C RID: 2572
	public StateType stateType;

	// Token: 0x04000A0D RID: 2573
	private static List<StateControl> controllers = new List<StateControl>();

	// Token: 0x04000A0E RID: 2574
	public float defaultNumber;

	// Token: 0x04000A0F RID: 2575
	public float currentNumber;

	// Token: 0x04000A10 RID: 2576
	public StateControl.FloatEvent numberEvent;

	// Token: 0x04000A11 RID: 2577
	public Action<float> numberAction;

	// Token: 0x04000A12 RID: 2578
	public bool defaultBool;

	// Token: 0x04000A13 RID: 2579
	public bool currentBool;

	// Token: 0x04000A14 RID: 2580
	public UnityEvent OnActivate;

	// Token: 0x04000A15 RID: 2581
	public UnityEvent OnDeactivate;

	// Token: 0x04000A16 RID: 2582
	public Action<bool> boolAction;

	// Token: 0x0200050F RID: 1295
	[Serializable]
	public class FloatEvent : UnityEvent<float>
	{
		// Token: 0x060023AF RID: 9135 RVA: 0x000CB9EE File Offset: 0x000C9BEE
		public FloatEvent()
		{
		}
	}
}
