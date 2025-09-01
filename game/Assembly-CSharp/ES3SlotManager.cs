using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000152 RID: 338
public class ES3SlotManager : MonoBehaviour
{
	// Token: 0x06000F01 RID: 3841 RVA: 0x0005F620 File Offset: 0x0005D820
	protected virtual void OnEnable()
	{
		this.slotTemplate.SetActive(false);
		this.DestroySlots();
		this.InstantiateSlots();
	}

	// Token: 0x06000F02 RID: 3842 RVA: 0x0005F63C File Offset: 0x0005D83C
	protected virtual void InstantiateSlots()
	{
		List<ValueTuple<string, DateTime>> list = new List<ValueTuple<string, DateTime>>();
		if (!ES3.DirectoryExists(this.slotDirectory))
		{
			return;
		}
		string[] files = ES3.GetFiles(this.slotDirectory);
		for (int i = 0; i < files.Length; i++)
		{
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(files[i]);
			DateTime timestamp = ES3.GetTimestamp(this.GetSlotPath(fileNameWithoutExtension));
			list.Add(new ValueTuple<string, DateTime>(fileNameWithoutExtension, timestamp));
		}
		list = (from x in list
		orderby x.Item2 descending
		select x).ToList<ValueTuple<string, DateTime>>();
		foreach (ValueTuple<string, DateTime> valueTuple in list)
		{
			this.InstantiateSlot(valueTuple.Item1, valueTuple.Item2);
		}
	}

	// Token: 0x06000F03 RID: 3843 RVA: 0x0005F718 File Offset: 0x0005D918
	public virtual GameObject InstantiateSlot(string slotName, DateTime timestamp)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.slotTemplate, this.slotTemplate.transform.parent);
		this.slots.Add(gameObject);
		gameObject.SetActive(true);
		ES3Slot component = gameObject.GetComponent<ES3Slot>();
		component.nameLabel.text = slotName.Replace('_', ' ');
		if (timestamp == ES3SlotManager.falseDateTime)
		{
			component.timestampLabel.text = "";
		}
		else
		{
			component.timestampLabel.text = timestamp.ToString("yyyy-MM-dd") + "\n" + timestamp.ToString("HH:mm:ss");
		}
		return gameObject;
	}

	// Token: 0x06000F04 RID: 3844 RVA: 0x0005F7BD File Offset: 0x0005D9BD
	public virtual void ShowErrorDialog(string errorMessage)
	{
		this.errorDialog.transform.Find("Dialog Box/Message").GetComponent<TMP_Text>().text = errorMessage;
		this.errorDialog.SetActive(true);
	}

	// Token: 0x06000F05 RID: 3845 RVA: 0x0005F7EC File Offset: 0x0005D9EC
	protected virtual void DestroySlots()
	{
		foreach (GameObject obj in this.slots)
		{
			UnityEngine.Object.Destroy(obj);
		}
	}

	// Token: 0x06000F06 RID: 3846 RVA: 0x0005F83C File Offset: 0x0005DA3C
	public virtual string GetSlotPath(string slotName)
	{
		return this.slotDirectory + Regex.Replace(slotName, "\\s+", "_") + this.slotExtension;
	}

	// Token: 0x06000F07 RID: 3847 RVA: 0x0005F85F File Offset: 0x0005DA5F
	public ES3SlotManager()
	{
	}

	// Token: 0x06000F08 RID: 3848 RVA: 0x0005F896 File Offset: 0x0005DA96
	// Note: this type is marked as 'beforefieldinit'.
	static ES3SlotManager()
	{
	}

	// Token: 0x04000CB2 RID: 3250
	[Tooltip("Shows a confirmation if this slot already exists when we select it.")]
	public bool showConfirmationIfExists = true;

	// Token: 0x04000CB3 RID: 3251
	[Tooltip("Whether the Create new slot button should be visible.")]
	public bool showCreateSlotButton = true;

	// Token: 0x04000CB4 RID: 3252
	[Tooltip("Whether we should automatically create an empty save file when the user creates a new save slot. This will be created using the default settings, so you should set this to false if you are using ES3Settings objects.")]
	public bool autoCreateSaveFile;

	// Token: 0x04000CB5 RID: 3253
	[Space(16f)]
	[Tooltip("The name of a scene to load after the user chooses a slot.")]
	public string loadSceneAfterSelectSlot;

	// Token: 0x04000CB6 RID: 3254
	[Space(16f)]
	[Tooltip("An event called after a slot is selected, but before the scene specified by loadSceneAfterSelectSlot is loaded.")]
	public UnityEvent onAfterSelectSlot;

	// Token: 0x04000CB7 RID: 3255
	[Space(16f)]
	[Tooltip("The subfolder we want to store our save files in. If this is a relative path, it will be relative to Application.persistentDataPath.")]
	public string slotDirectory = "slots/";

	// Token: 0x04000CB8 RID: 3256
	[Tooltip("The extension we want to use for our save files.")]
	public string slotExtension = ".es3";

	// Token: 0x04000CB9 RID: 3257
	[Space(16f)]
	[Tooltip("The template we'll instantiate to create our slots.")]
	public GameObject slotTemplate;

	// Token: 0x04000CBA RID: 3258
	[Tooltip("The dialog box for creating a new slot.")]
	public GameObject createDialog;

	// Token: 0x04000CBB RID: 3259
	[Tooltip("The dialog box for displaying an error to the user.")]
	public GameObject errorDialog;

	// Token: 0x04000CBC RID: 3260
	public static string selectedSlotPath = null;

	// Token: 0x04000CBD RID: 3261
	public List<GameObject> slots = new List<GameObject>();

	// Token: 0x04000CBE RID: 3262
	private static DateTime falseDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

	// Token: 0x02000549 RID: 1353
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		// Token: 0x06002448 RID: 9288 RVA: 0x000CDABB File Offset: 0x000CBCBB
		// Note: this type is marked as 'beforefieldinit'.
		static <>c()
		{
		}

		// Token: 0x06002449 RID: 9289 RVA: 0x000CDAC7 File Offset: 0x000CBCC7
		public <>c()
		{
		}

		// Token: 0x0600244A RID: 9290 RVA: 0x000CDACF File Offset: 0x000CBCCF
		internal DateTime <InstantiateSlots>b__14_0([TupleElementNames(new string[]
		{
			"Name",
			"Timestamp"
		})] ValueTuple<string, DateTime> x)
		{
			return x.Item2;
		}

		// Token: 0x0400269A RID: 9882
		public static readonly ES3SlotManager.<>c <>9 = new ES3SlotManager.<>c();

		// Token: 0x0400269B RID: 9883
		[TupleElementNames(new string[]
		{
			"Name",
			"Timestamp"
		})]
		public static Func<ValueTuple<string, DateTime>, DateTime> <>9__14_0;
	}
}
