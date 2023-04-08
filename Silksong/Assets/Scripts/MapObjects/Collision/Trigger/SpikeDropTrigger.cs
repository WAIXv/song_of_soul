using UnityEngine;
/// <summary>
/// �����̵Ĵ�����
/// </summary>���ߣ����
public class SpikeDropTrigger : Trigger2DBase
{
    public DropSpikeCollider spike;
    protected override void enterEvent()
    {
        if(spike)
        {
            spike.drop();
            Destroyed_GamingSave gamingSave;
            if (TryGetComponent(out gamingSave) &&!gamingSave.ban)
            {
                gamingSave.saveGamingData(true);
            }
        }
    }
}
