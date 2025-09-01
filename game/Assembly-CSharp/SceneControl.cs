using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200012D RID: 301
public class SceneControl : MonoBehaviour
{
	// Token: 0x06000E13 RID: 3603 RVA: 0x00059E7B File Offset: 0x0005807B
	public void GoToFantasy()
	{
		if (PhotonNetwork.InRoom && !PhotonNetwork.IsMasterClient)
		{
			return;
		}
		PhotonNetwork.LoadLevel("Fantasy");
	}

	// Token: 0x06000E14 RID: 3604 RVA: 0x00059E96 File Offset: 0x00058096
	public void ReturnToLibrary()
	{
		SceneControl.GoToLibrary();
	}

	// Token: 0x06000E15 RID: 3605 RVA: 0x00059E9D File Offset: 0x0005809D
	public static void GoToLibrary()
	{
		if (PhotonNetwork.InRoom && !PhotonNetwork.IsMasterClient)
		{
			return;
		}
		if (MapManager.InLobbyScene)
		{
			return;
		}
		if (PhotonNetwork.InRoom)
		{
			PhotonNetwork.LoadLevel(MapManager.LobbySceneName);
			return;
		}
		SceneManager.LoadSceneAsync(MapManager.LobbySceneName, LoadSceneMode.Single);
	}

	// Token: 0x06000E16 RID: 3606 RVA: 0x00059ED4 File Offset: 0x000580D4
	public SceneControl()
	{
	}
}
