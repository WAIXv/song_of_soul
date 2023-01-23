using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlunge : PlayerAction
{
    public PlayerPlunge(PlayerController playerController) : base(playerController) { }

    private float plungeStartPositionY;
    private float plungeDistance;

    public int plungeStrength;

    public bool hasPlunged;
    public override void StateStart(EPlayerState oldState)
    {
        //if (hasPlunged)
        //{
        //    return;
        //}
        //hasPlunged = true;
        //Debug.Log("start plunging" + Time.time);

        // ��ֱ����
        playerController.setRigidGravityScale(0f);
        playerController.gravityLock = true;
        playerController.setRigidVelocity(new Vector2(0, -1 * playerController.playerInfo.plungeSpeed));


        plungeStrength = 0;

        plungeDistance = 0.0f;
        plungeStartPositionY = playerController.transform.position.y;
    }

    public override void StateUpdate()
    {
        float positionY = playerController.transform.position.y;
        plungeDistance = plungeStartPositionY - positionY;

        // ����Strength
        int i = plungeStrength;
        while (i < playerController.plungeStrengthArr.Length - 1 && plungeDistance > playerController.plungeStrengthArr[i + 1])
        {
            // Debug.Log(plungeStrength);
            i++;
        }
        plungeStrength = i;

        // �� DestructiblePlatform ������� willBreakGround���ڴ˸���animator param
        // playerController.PlayerAnimator.SetBool(playerController.animatorParamsMapping.WillBreakGroundParamHash, willBreakGround);
    }

    public override void StateEnd(EPlayerState newState)
    {
        playerController.gravityLock = false;
        playerController.setRigidGravityScaleToNormal();



        //if (plungeDistance > 2.57f || (plungeDistance < 2.53f && plungeDistance > 0.01f))
        //{

        //    Debug.Log("���ҽ���");
        //    
        //}
        //    hasPlunged= false;

        if (plungeStrength == 3)
        {
            playerController.plunge.Play();
        }


        Debug.Log("Landed! Plunge strength:" + plungeStrength + "Distance:" + plungeDistance);
        plungeStrength = 0;
        plungeDistance = 0.0f;


    }

}
