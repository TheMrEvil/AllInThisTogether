using System;
using Photon.Pun;
using UnityEngine;

// Token: 0x0200006C RID: 108
public class AINetworkedLight : AINetworked
{
	// Token: 0x0600041B RID: 1051 RVA: 0x0002064C File Offset: 0x0001E84C
	public override void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		this.SendIndex++;
		EntityHealth health = this.control.health;
		AIMovement movement = base.Control.Movement;
		if (stream.IsWriting)
		{
			bool flag = this.SendIndex % 4 == 0;
			stream.SendNext(flag);
			if (!flag)
			{
				return;
			}
			stream.SendNext(this.control.TeamID);
			if (health != null)
			{
				stream.SendNext(health.health);
				stream.SendNext(health.shield);
				stream.SendNext(health.isDead);
			}
			if (movement != null)
			{
				stream.SendNext(movement.GetPosition());
				stream.SendNext(movement.GetRotation());
				if (movement.HasVelocity)
				{
					stream.SendNext(movement.GetVelocity());
				}
			}
			stream.SendNext((base.Control.currentTarget == null) ? -1 : base.Control.currentTarget.net.view.ViewID);
			stream.SendNext((base.Control.allyTarget == null) ? -1 : base.Control.allyTarget.net.view.ViewID);
		}
		else
		{
			if (!(bool)stream.ReceiveNext())
			{
				return;
			}
			int newTeam = (int)stream.ReceiveNext();
			this.control.ChangeTeam(newTeam);
			if (health != null)
			{
				int hp = (int)stream.ReceiveNext();
				float shield = (float)stream.ReceiveNext();
				bool dead = (bool)stream.ReceiveNext();
				health.UpdateHealthFromNetwork(hp, shield, health.shieldDelay, dead);
			}
			if (movement != null)
			{
				Vector3 pos = (Vector3)stream.ReceiveNext();
				Quaternion rot = (Quaternion)stream.ReceiveNext();
				Vector3 vel = Vector3.zero;
				if (movement.HasVelocity)
				{
					vel = (Vector3)stream.ReceiveNext();
				}
				movement.UpdateFromNetwork(pos, rot, vel);
			}
			base.Control.SetTargetByViewID((int)stream.ReceiveNext());
			base.Control.SetAllyTargetByViewID((int)stream.ReceiveNext());
		}
		base.OnPhotonSerializeView(stream, info);
	}

	// Token: 0x0600041C RID: 1052 RVA: 0x0002089E File Offset: 0x0001EA9E
	public AINetworkedLight()
	{
	}
}
