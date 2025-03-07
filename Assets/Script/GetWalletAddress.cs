using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GetWalletAddress : MonoBehaviour
{
    public TMP_Text _walletAddress;
    private void OnEnable()
    {
        _walletAddress.text = PlayerPrefs.GetString("Account");
    }
}
