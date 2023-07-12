using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kart
{
    public string ad { get; set; } = string.Empty;
    public string aciklama { get; set; } = string.Empty;
    public bool aktiflik { get; set; } = true;
    public int kalanAdet { get; set; } = 0;
    public int indeks { get; set; } = 0;
    public float olasilik { get; set; }
    public int minLevel { get; set; }
    public int maxLevel { get; set; }
    public int lvl { get; set; }
    public int cost { get; set; }
    public float value { get; set; }
    public GameObject prefabTower;
    public Sprite gorsel { get; set; }

    public delegate void KartSecildigindeHandler();
    public event KartSecildigindeHandler KartSecildiginde;

    private static int kartSayac = 0;
    public Kart()
    {
        indeks = kartSayac;
        kartSayac++;
    }
    public void OnKartSecildiginde()
    {
        if (KartSecildiginde != null)
        {
            KartSecildiginde.Invoke();
        }
    }
}

