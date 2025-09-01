using System;
using UnityEngine;

// Token: 0x02000010 RID: 16
public class ES3Defaults : ScriptableObject
{
	// Token: 0x06000143 RID: 323 RVA: 0x0000588C File Offset: 0x00003A8C
	public ES3Defaults()
	{
	}

	// Token: 0x04000029 RID: 41
	[SerializeField]
	public ES3SerializableSettings settings = new ES3SerializableSettings();

	// Token: 0x0400002A RID: 42
	public bool addMgrToSceneAutomatically;

	// Token: 0x0400002B RID: 43
	public bool autoUpdateReferences = true;

	// Token: 0x0400002C RID: 44
	public bool addAllPrefabsToManager = true;

	// Token: 0x0400002D RID: 45
	public int collectDependenciesDepth = 4;

	// Token: 0x0400002E RID: 46
	public int collectDependenciesTimeout = 10;

	// Token: 0x0400002F RID: 47
	public bool updateReferencesWhenSceneChanges = true;

	// Token: 0x04000030 RID: 48
	public bool updateReferencesWhenSceneIsSaved = true;

	// Token: 0x04000031 RID: 49
	public bool updateReferencesWhenSceneIsOpened = true;

	// Token: 0x04000032 RID: 50
	[Tooltip("Folders listed here will be searched for references every time the reference manager is refreshed. Path should be relative to the project folder.")]
	public string[] referenceFolders = new string[0];

	// Token: 0x04000033 RID: 51
	public bool logDebugInfo;

	// Token: 0x04000034 RID: 52
	public bool logWarnings = true;

	// Token: 0x04000035 RID: 53
	public bool logErrors = true;
}
