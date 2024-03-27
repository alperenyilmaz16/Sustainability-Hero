using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsernameOutput : MonoBehaviour
{
    public Text kullaniciAdiText;

    void Start()
    {
        
        string kullaniciAdi = PlayerPrefs.GetString("KullaniciAdi", "Oyuncu");

        
        kullaniciAdiText.text = "Hoþ geldin, " + kullaniciAdi + "!";
    }
}
