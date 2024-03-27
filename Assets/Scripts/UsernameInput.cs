using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsernameInput : MonoBehaviour
{
    public InputField OyuncuAdiGirme;
    
    private string playerName;

    public void SetPlayerName()
    {
        playerName = OyuncuAdiGirme.text;
        
        PlayerPrefs.SetString("KullaniciAdi", playerName);
        
    }
}
