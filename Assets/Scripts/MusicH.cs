using UnityEngine;

public class MusicH : MonoBehaviour
{
    public void BossFight(){
        MusicManager.Instance.PlayMusic("Boss");
    }
    public void Candle(){
        SoundManager.Instance.PlaySound3D("Candle", transform.position);
    }
    public void WoodenDoor(){
        SoundManager.Instance.PlaySound2D("WoodenDoor");
    }
    public void MetalDoor(){
        SoundManager.Instance.PlaySound2D("MetalDoor");
    }
}
