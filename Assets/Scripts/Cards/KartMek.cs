using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Cursor = UnityEngine.Cursor;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;

public class KartMek : MonoBehaviour
{
    public GameObject gPanel;
    public GameObject characterPanel;
    public GameObject menuButton;
    public static GameObject towerPrefab;

    GameObject eventSystem;
    TowerPoints towerPoints;
    LevelControl levelControl;

    public List<GameObject> towerPrefabList = new List<GameObject>();
    public List<GameObject> bulletPrefabList = new List<GameObject>();
    private List<Kart> kartListesi = new List<Kart>();

    private List<Kart> aktifKartlar = new List<Kart>();

    [SerializeField] public List<Transform> Coordinates = new List<Transform>();

    [SerializeField] public List<GameObject> Prefabs = new List<GameObject>();

    List<GameObject> cardObjectList = new List<GameObject>();

    //public static List<GameObject> createdTowers = new List<GameObject>();
    public GameObject soldier;
    public GameObject grenade;
    public GameObject granedeGun;
    public GameObject bullet;

    public List<Sprite> kartImageList = new List<Sprite>();
    void Awake()
    {

        eventSystem = GameObject.Find("Event System");
        levelControl = eventSystem.GetComponent<LevelControl>();
        towerPoints = eventSystem.GetComponent<TowerPoints>();
        // Kartları oluştur ve listeye ekle
        // --------------------------------------------------
        // Kule inşa etme kartları
        Kart okcuKulesiOlusturmaKarti = new()
        {
            ad = "Okçu Kulesi Oluşturma Kartı",
            aciklama = "Yeni bir okçu kulesi inşa etmek için kullanılır.",
            aktiflik = true,
            kalanAdet = 3,
            olasilik = 0.9f,
            minLevel = 1,
            maxLevel = 21,
            lvl = 1,
            cost = 2,
            prefabTower = towerPrefabList.FirstOrDefault(x => x.name == ("OkcuKulesi")),
            gorsel = kartImageList.FirstOrDefault(x => x.name == ("OkcuKulesiOlusturma_0"))

        };
        okcuKulesiOlusturmaKarti.KartSecildiginde += OkcuKulesiOlusturmaKartiSecildi;
        kartListesi.Add(okcuKulesiOlusturmaKarti);


        Kart arbaletKulesiOlusturmaKarti = new()
        {
            ad = "Arbalet Kulesi Oluşturma Kartı",
            aciklama = "Yeni bir arbalet kulesi inşa etmek için kullanılır.",
            aktiflik = true,
            kalanAdet = 3,
            olasilik = 0.9f,
            minLevel = 3,
            maxLevel = 21,
            lvl = 2,
            cost = 4,
            prefabTower = towerPrefabList.FirstOrDefault(x => x.name == ("ArbaletKulesi")),
            gorsel = kartImageList.FirstOrDefault(x => x.name == ("ArbaletKulesiOlusturma_0"))
        };
        arbaletKulesiOlusturmaKarti.KartSecildiginde += ArbaletKulesiOlusturmaKartiSecildi;
        kartListesi.Add(arbaletKulesiOlusturmaKarti);


        Kart topcuKulesiOlusturmaKarti = new()
        {
            ad = "Topçu Kulesi Oluşturma Kartı",
            aciklama = "Yeni bir topçu kulesi inşa etmek için kullanılır.",
            aktiflik = true,
            kalanAdet = 3,
            olasilik = 0.9f,
            minLevel = 4,
            maxLevel = 21,
            lvl = 3,
            cost = 6,
            prefabTower = towerPrefabList.FirstOrDefault(x => x.name == ("TopcuKulesi")),
            gorsel = kartImageList.FirstOrDefault(x => x.name == ("TopcuKulesiOlusturma_0"))
        };
        topcuKulesiOlusturmaKarti.KartSecildiginde += TopcuKulesiOlusturmaKartiSecildi;
        kartListesi.Add(topcuKulesiOlusturmaKarti);


        Kart zehirKulesiOlusturmaKarti = new()
        {
            ad = "Zehir Kulesi Oluşturma Kartı",
            aciklama = "Yeni bir zehir kulesi inşa etmek için kullanılır.",
            aktiflik = true,
            kalanAdet = 2,
            olasilik = 0.3f,
            minLevel = 5,
            maxLevel = 21,
            lvl = 4,
            cost = 8,
            prefabTower = towerPrefabList.FirstOrDefault(x => x.name == ("ZehirKulesi")),
            gorsel = kartImageList.FirstOrDefault(x => x.name == ("ZehirKulesiOlusturma_0"))
        };
        zehirKulesiOlusturmaKarti.KartSecildiginde += ZehirKulesiOlusturmaKartiSecildi;
        kartListesi.Add(zehirKulesiOlusturmaKarti);


        Kart buyucuKulesiOlusturmaKarti = new()
        {
            ad = "Büyücü Kulesi Oluşturma Kartı",
            aciklama = "Yeni bir büyücü kulesi inşa etmek için kullanılır.",
            aktiflik = true,
            kalanAdet = 2,
            olasilik = 0.2f,
            minLevel = 6,
            maxLevel = 21,
            lvl = 5,
            cost = 10,
            prefabTower = towerPrefabList.FirstOrDefault(x => x.name == ("BuyucuKulesi")),
            gorsel = kartImageList.FirstOrDefault(x => x.name == ("BuyucuKulesiOlusturma_0"))
        };
        buyucuKulesiOlusturmaKarti.KartSecildiginde += BuyucuKulesiOlusturmaKartiSecildi;
        kartListesi.Add(buyucuKulesiOlusturmaKarti);




        //----------------------------------------------------------
        // Kule geliştirme kartları

        Kart tumKuleleriGelistir = new()
        {
            ad = "Tüm Kuleleri Geliştir",
            aciklama = "Oyundaki tüm kulelerin saldırı hasarını %10 arttırır.",
            aktiflik = true,
            kalanAdet = 2,
            olasilik = 0.1f,
            minLevel = 15,
            maxLevel = 21,
            lvl = 5,
            cost = 10,
            value = 10,
            gorsel = kartImageList.FirstOrDefault(x => x.name == ("TumKuleleriGelistirme_0"))
        };
        tumKuleleriGelistir.KartSecildiginde += TumKuleleriGelistirmeKartiSecildi;
        kartListesi.Add(tumKuleleriGelistir);


        Kart okcuKulesiGelistirmeKarti = new()
        {
            ad = "Okçu Kulesi Geliştirme Kartı",
            aciklama = "Mevcut olan ve sonradan oluşturacağınız okçu kulelerinin hasarını %10 arttırır.",
            aktiflik = true,
            kalanAdet = 3,
            olasilik = 0.4f,
            minLevel = 2,
            maxLevel = 21,
            lvl = 1,
            cost = 1,
            value = 10,
            prefabTower = bulletPrefabList.FirstOrDefault(x => x.name == ("Ok")),
            gorsel = kartImageList.FirstOrDefault(x => x.name == ("OkcuKulesiGelistirme_0"))

        };
        okcuKulesiGelistirmeKarti.KartSecildiginde += OkcuKulesiGelistirmeKartiSecildi;
        kartListesi.Add(okcuKulesiGelistirmeKarti);


        Kart arbaletKulesiGelistirmeKarti = new()
        {
            ad = "Arbalet Kulesi Geliştirme Kartı",
            aciklama = "Mevcut olan ve sonradan oluşturacağınız arbalet kulelerinin hasarını %10 arttırır.",
            aktiflik = true,
            kalanAdet = 3,
            olasilik = 0.4f,
            minLevel = 4,
            maxLevel = 21,
            lvl = 2,
            cost = 2,
            value = 10,
            prefabTower = bulletPrefabList.FirstOrDefault(x => x.name == ("Mizrak")),
            gorsel = kartImageList.FirstOrDefault(x => x.name == ("ArbaletKulesiGelistirme_0"))

        };
        arbaletKulesiGelistirmeKarti.KartSecildiginde += ArbaletKulesiGelistirmeKartiSecildi;
        kartListesi.Add(arbaletKulesiGelistirmeKarti);


        Kart topcuKulesiGelistirmeKarti = new()
        {
            ad = "Topçu Kulesi Geliştirme Kartı",
            aciklama = "Mevcut olan ve sonradan oluşturacağınız topçu kulelerinin hasarını %10 arttırır.",
            aktiflik = true,
            kalanAdet = 3,
            olasilik = 0.4f,
            minLevel = 5,
            maxLevel = 21,
            lvl = 3,
            cost = 3,
            value = 10,
            prefabTower = bulletPrefabList.FirstOrDefault(x => x.name == ("Top")),
            gorsel = kartImageList.FirstOrDefault(x => x.name == ("TopcuKulesiGelistirme_0"))
        };
        topcuKulesiGelistirmeKarti.KartSecildiginde += TopcuKulesiGelistirmeKartiSecildi;
        kartListesi.Add(topcuKulesiGelistirmeKarti);


        Kart zehirKulesiGelistirmeKarti = new()
        {
            ad = "Zehir Kulesi Geliştirme Kartı",
            aciklama = "Mevcut olan ve sonradan oluşturacağınız zehir kulelerinin hasarını %10 arttırır.",
            aktiflik = true,
            kalanAdet = 2,
            olasilik = 0.3f,
            minLevel = 6,
            maxLevel = 21,
            lvl = 4,
            cost = 4,
            value = 10,
            prefabTower = bulletPrefabList.FirstOrDefault(x => x.name == ("Poison")),
            gorsel = kartImageList.FirstOrDefault(x => x.name == ("ZehirKulesiGelistirme_0"))
        };
        zehirKulesiGelistirmeKarti.KartSecildiginde += ZehirKulesiGelistirmeKartiSecildi;
        kartListesi.Add(zehirKulesiGelistirmeKarti);

        Kart buyucuKulesiGelistirmeKarti = new()
        {
            ad = "Büyücü Kulesi Geliştirme Kartı",
            aciklama = "Mevcut olan ve sonradan oluşturacağınız büyücü kulelerinin hasarını %10 arttırır.",
            aktiflik = true,
            kalanAdet = 2,
            olasilik = 0.2f,
            minLevel = 7,
            maxLevel = 21,
            lvl = 5,
            cost = 5,
            value = 10,
            prefabTower = bulletPrefabList.FirstOrDefault(x => x.name == ("Buyu")),
            gorsel = kartImageList.FirstOrDefault(x => x.name == ("BuyucuKulesiGelistirme_0"))
        };
        buyucuKulesiGelistirmeKarti.KartSecildiginde += BuyucuKulesiGelistirmeKartiSecildi;
        kartListesi.Add(buyucuKulesiGelistirmeKarti);




        // ---------------------------------------------------------
        // Karakter güçlendirme kartları
        Kart silahHasariGelistirmeKarti = new()
        {
            ad = "Saldırı Gücü Geliştirme Kartı",
            aciklama = "Karakterin mermilerinin verdiği hasar 12% artar.",
            aktiflik = true,
            kalanAdet = 1,
            olasilik = 0.3f,
            minLevel = 1,
            maxLevel = 7,
            lvl = 3,
            cost = 3,
            value = 12,
            gorsel = kartImageList.FirstOrDefault(x => x.name == ("Hasar_0"))
        };
        silahHasariGelistirmeKarti.KartSecildiginde += SilahHasariGelistirmeKartiSecildi;
        kartListesi.Add(silahHasariGelistirmeKarti);


        Kart silahHiziGelistirmeKarti = new()
        {
            ad = "Saldırı Hızı Geliştirme Kartı",
            aciklama = "Karakterin ateş etme hızı 15% artar.",
            aktiflik = true,
            kalanAdet = 1,
            olasilik = 0.3f,
            minLevel = 1,
            maxLevel = 7,
            lvl = 3,
            cost = 3,
            value = 15,
            gorsel = kartImageList.FirstOrDefault(x => x.name == ("AtisHizi_0"))
        };
        silahHiziGelistirmeKarti.KartSecildiginde += SilahHiziGelistirmeKartiSecildi;
        kartListesi.Add(silahHiziGelistirmeKarti);


        Kart bombaHasariGelistirmeKarti = new()
        {
            ad = "Yetenek Gücü Geliştirme Kartı",
            aciklama = "Karakterin el bombası ile verdiği hasar 12% artar.",
            aktiflik = true,
            kalanAdet = 1,
            olasilik = 0.3f,
            minLevel = 1,
            maxLevel = 7,
            lvl = 3,
            cost = 3,
            value = 12,
            gorsel = kartImageList.FirstOrDefault(x => x.name == ("BombaHasari_0"))
        };
        bombaHasariGelistirmeKarti.KartSecildiginde += BombaHasariGelistirmeKartiSecildi;
        kartListesi.Add(bombaHasariGelistirmeKarti);


        Kart bombaDolmaHiziArttirmaGelistirmeKarti = new()
        {
            ad = "Bekleme Süresi Azaltma Kartı",
            aciklama = "Karakterin el bombası bekleme süresi 15% azalır.",
            aktiflik = true,
            kalanAdet = 1,
            olasilik = 0.3f,
            minLevel = 1,
            maxLevel = 7,
            lvl = 3,
            cost = 3,
            value = 15,
            gorsel = kartImageList.FirstOrDefault(x => x.name == ("BombaTime_0"))
        };
        bombaDolmaHiziArttirmaGelistirmeKarti.KartSecildiginde += BombaDolmaHiziArttirmaGelistirmeKartiSecildi;
        kartListesi.Add(bombaDolmaHiziArttirmaGelistirmeKarti);


        Kart canGelistirmeKarti = new()
        {
            ad = "Maksimum Can Arttırma Kartı",
            aciklama = "Karakterin taban canı 20% artar.",
            aktiflik = true,
            kalanAdet = 1,
            olasilik = 0.3f,
            minLevel = 1,
            maxLevel = 7,
            lvl = 3,
            cost = 3,
            value = 20,
            gorsel = kartImageList.FirstOrDefault(x => x.name == ("CanGelistirme"))
        };
        canGelistirmeKarti.KartSecildiginde += CanGelistirmeKartiSecildi;
        kartListesi.Add(canGelistirmeKarti);


        Kart lvl2silahHasariGelistirmeKarti = new()
        {
            ad = "Saldırı Gücü Geliştirme Kartı",
            aciklama = "Karakterin mermilerinin verdiği hasar 18% artar.",
            aktiflik = true,
            kalanAdet = 1,
            olasilik = 0.3f,
            minLevel = 8,
            maxLevel = 14,
            lvl = 4,
            cost = 6,
            value = 18,
            gorsel = kartImageList.FirstOrDefault(x => x.name == ("Hasar2_0"))
        };
        lvl2silahHasariGelistirmeKarti.KartSecildiginde += lvl2SilahHasariGelistirmeKartiSecildi;
        kartListesi.Add(lvl2silahHasariGelistirmeKarti);


        Kart lvl2silahHiziGelistirmeKarti = new()
        {
            ad = "Saldırı Hızı Geliştirme Kartı",
            aciklama = "Karakterin ateş etme hızı 20% artar.",
            aktiflik = true,
            kalanAdet = 1,
            olasilik = 0.3f,
            minLevel = 8,
            maxLevel = 14,
            lvl = 4,
            cost = 6,
            value = 20,
            gorsel = kartImageList.FirstOrDefault(x => x.name == ("AtisHizi2_0"))
        };
        lvl2silahHiziGelistirmeKarti.KartSecildiginde += lvl2SilahHiziGelistirmeKartiSecildi;
        kartListesi.Add(lvl2silahHiziGelistirmeKarti);


        Kart lvl2bombaHasariGelistirmeKarti = new()
        {
            ad = "Yetenek Gücü Geliştirme Kartı",
            aciklama = "Karakterin el bombası ile verdiği hasar 18% artar.",
            aktiflik = true,
            kalanAdet = 1,
            olasilik = 0.3f,
            minLevel = 8,
            maxLevel = 14,
            lvl = 4,
            cost = 6,
            value = 18,
            gorsel = kartImageList.FirstOrDefault(x => x.name == ("BombaHasari2_0"))
        };
        lvl2bombaHasariGelistirmeKarti.KartSecildiginde += lvl2BombaHasariGelistirmeKartiSecildi;
        kartListesi.Add(lvl2bombaHasariGelistirmeKarti);


        Kart lvl2bombaDolmaHiziArttirmaGelistirmeKarti = new()
        {
            ad = "El Bombası Bekleme Süresi Geliştirme Kartı",
            aciklama = "Karakterin el bombası bekleme süresi 20% azalır.",
            aktiflik = true,
            kalanAdet = 1,
            olasilik = 0.3f,
            minLevel = 8,
            maxLevel = 14,
            lvl = 4,
            cost = 6,
            value = 20,
            gorsel = kartImageList.FirstOrDefault(x => x.name == ("BombaTime2_0"))
        };
        lvl2bombaDolmaHiziArttirmaGelistirmeKarti.KartSecildiginde += lvl2BombaDolmaHiziArttirmaGelistirmeKartiSecildi;
        kartListesi.Add(lvl2bombaDolmaHiziArttirmaGelistirmeKarti);


        Kart lvl2canGelistirmeKarti = new()
        {
            ad = "Maksimum Can Arttırma Kartı",
            aciklama = "Karakterin taban canı 30% artar.",
            aktiflik = true,
            kalanAdet = 1,
            olasilik = 0.3f,
            minLevel = 8,
            maxLevel = 14,
            lvl = 4,
            cost = 6,
            value = 30,
            gorsel = kartImageList.FirstOrDefault(x => x.name == ("CanGelistirme2"))
        };
        lvl2canGelistirmeKarti.KartSecildiginde += lvl2CanGelistirmeKartiSecildi;
        kartListesi.Add(lvl2canGelistirmeKarti);






        Kart lvl3silahHasariGelistirmeKarti = new()
        {
            ad = "Saldırı Gücü Geliştirme Kartı",
            aciklama = "Karakterin mermilerinin verdiği hasar 25% artar.",
            aktiflik = true,
            kalanAdet = 1,
            olasilik = 0.3f,
            minLevel = 15,
            maxLevel = 21,
            lvl = 5,
            cost = 9,
            value = 25,
            gorsel = kartImageList.FirstOrDefault(x => x.name == ("Hasar3_0"))
        };
        lvl3silahHasariGelistirmeKarti.KartSecildiginde += lvl3SilahHasariGelistirmeKartiSecildi;
        kartListesi.Add(lvl3silahHasariGelistirmeKarti);


        Kart lvl3silahHiziGelistirmeKarti = new()
        {
            ad = "Saldırı Hızı Geliştirme Kartı",
            aciklama = "Karakterin ateş etme hızı 25% artar.",
            aktiflik = true,
            kalanAdet = 1,
            olasilik = 0.3f,
            minLevel = 15,
            maxLevel = 21,
            lvl = 5,
            cost = 9,
            value = 25,
            gorsel = kartImageList.FirstOrDefault(x => x.name == ("AtisHizi3_0"))
        };
        lvl3silahHiziGelistirmeKarti.KartSecildiginde += lvl3SilahHiziGelistirmeKartiSecildi;
        kartListesi.Add(lvl3silahHiziGelistirmeKarti);


        Kart lvl3bombaHasariGelistirmeKarti = new()
        {
            ad = "Yetenek Gücü Geliştirme Kartı",
            aciklama = "Karakterin el bombası ile verdiği hasar 25% artar.",
            aktiflik = true,
            kalanAdet = 1,
            olasilik = 0.3f,
            minLevel = 15,
            maxLevel = 21,
            lvl = 5,
            cost = 9,
            value = 25,
            gorsel = kartImageList.FirstOrDefault(x => x.name == ("BombaHasari3_0"))
        };
        lvl3bombaHasariGelistirmeKarti.KartSecildiginde += lvl3BombaHasariGelistirmeKartiSecildi;
        kartListesi.Add(lvl3bombaHasariGelistirmeKarti);


        Kart lvl3bombaDolmaHiziArttirmaGelistirmeKarti = new()
        {
            ad = "El Bombası Bekleme Süresi Geliştirme Kartı",
            aciklama = "Karakterin el bombası bekleme süresi 25% azalır.",
            aktiflik = true,
            kalanAdet = 1,
            olasilik = 0.3f,
            minLevel = 15,
            maxLevel = 21,
            lvl = 5,
            cost = 9,
            value = 25,
            gorsel = kartImageList.FirstOrDefault(x => x.name == ("BombaTime3_0"))
        };
        lvl3bombaDolmaHiziArttirmaGelistirmeKarti.KartSecildiginde += lvl3BombaDolmaHiziArttirmaGelistirmeKartiSecildi;
        kartListesi.Add(lvl3bombaDolmaHiziArttirmaGelistirmeKarti);


        Kart lvl3canGelistirmeKarti = new()
        {
            ad = "Maksimum Can Arttırma Kartı",
            aciklama = "Karakterin taban canı 40% artar.",
            aktiflik = true,
            kalanAdet = 1,
            olasilik = 0.3f,
            minLevel = 15,
            maxLevel = 21,
            lvl = 5,
            cost = 9,
            value = 40,
            gorsel = kartImageList.FirstOrDefault(x => x.name == ("CanGelistirme3"))
        };
        lvl3canGelistirmeKarti.KartSecildiginde += lvl3CanGelistirmeKartiSecildi;
        kartListesi.Add(lvl3canGelistirmeKarti);
    }


    public void DisplayCards()
    {
        soldier.SetActive(false);

        foreach (var item in cardObjectList)
        {
            Destroy(item);
        }
        aktifKartlar.Clear();

        List<int> kullanilanIndexler = new List<int>(); // Kullanılan kart indekslerini tutmak için liste

        for (int i = 0; i < Coordinates.Count; i++)
        {
            // Kartı Instantiate et ve gerekli özellikleri ayarla
            Kart secilenKart = SecilenKartiGetir(kullanilanIndexler);
            GameObject cardObject = Instantiate(Prefabs[secilenKart.lvl - 1], Coordinates[i].position, Quaternion.identity, gameObject.transform);

            cardObjectList.Add(cardObject);

            // Event sistemini kullanarak tıklama işlemini dinle
            Button kartButton = cardObject.GetComponent<Button>();
            kartButton.onClick.AddListener(() => secilenKart.OnKartSecildiginde());
            if (secilenKart.cost > eventSystem.GetComponent<Spawner>().coin)
            {
                kartButton.enabled = false;
            }
            else
            {
                kartButton.enabled = true;
            }
            if (secilenKart == null)
            {
                continue; // Diğer Kartlara geç
            }
            TextMeshProUGUI[] textBox = cardObject.GetComponentsInChildren<TextMeshProUGUI>();
            if (textBox.Length >= 2)
            {
                textBox[0].text = secilenKart.ad; // İlk(Title) TextBox'ı doldur
                textBox[1].text = secilenKart.aciklama; // İkinci(Description) TextBox'ı doldur
                textBox[2].text = secilenKart.cost.ToString(); // Üçücüncü(Cost) TextBox'ı doldur
            }
            else
            {
                Debug.Log("TextBoxlar bulunamadı!");
            }

            Image[] image = cardObject.GetComponentsInChildren<Image>();
            if (image.Length >= 2)
            {
                image[1].sprite = secilenKart.gorsel; // İlk(Title) TextBox'ı doldur
            }
            else
            {
                Debug.Log("TextBoxlar bulunamadı!");
            }
            // Seçilen kartı aktif kartlara ekle
            aktifKartlar.Add(secilenKart);
            kullanilanIndexler.Add(secilenKart.indeks);
        }
    }

    Kart SecilenKartiGetir(List<int> kullanilanIndexler)
    {
        List<Kart> kullanilabilirKartlar = new List<Kart>();

        foreach (Kart kart in kartListesi)
        {
            if (kart.aktiflik && kart.kalanAdet > 0 && kart.minLevel <= levelControl.currentLevel && kart.maxLevel >= levelControl.currentLevel && !kullanilanIndexler.Contains(kart.indeks))
            {
                kullanilabilirKartlar.Add(kart);
            }
        }

        if (kullanilabilirKartlar.Count == 0)
        {
            return null; // Kullanılabilir kart yoksa null döndür
        }
        // Kartların olasılıklarını hesapla
        float toplamOlasilik = 0f;
        foreach (Kart kart in kullanilabilirKartlar)
        {
            toplamOlasilik += kart.olasilik;
        }

        // Rastgele bir olasılık değeri seç
        float rastgeleOlasilik = Random.Range(0f, toplamOlasilik);

        float toplam = 0f;
        foreach (Kart kart in kullanilabilirKartlar)
        {
            toplam += kart.olasilik;
            if (rastgeleOlasilik <= toplam)
            {
                // Kartın kalan adetini kontrol et

                if (kart.kalanAdet == 0)
                {
                    kart.aktiflik = false;
                }
                return kart;
            }
        }

        return null;
    }

    void SelectCreatedTowarCardSetTrue()
    {
        towerPoints.isSelectCreatedTowerCard = true;
        menuButton.GetComponent<MenuButton>().Resett();
        gPanel.SetActive(false);//xxx
    }

    public void BackToCharacter()
    {
        gameObject.SetActive(false);
        gPanel.SetActive(true);
        soldier.SetActive(true);
        characterPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        menuButton.GetComponent<MenuButton>().Resett();
        eventSystem.GetComponent<Spawner>().control = false;
    }

    void SelectCreatedTowarCardAssignPrefab(string name)
    {
        SelectCreatedTowarCardSetTrue();
        Kart tower = kartListesi.FirstOrDefault(x => x.ad == name);
        if (tower != null)
        {
            CoinUpdate(tower);
            tower.kalanAdet--; // Kartın adedini azalt
            if (tower.kalanAdet == 0)
            {
                tower.aktiflik = false; // Kartın aktiflik durumunu güncelle
            }
            towerPrefab = tower.prefabTower;
            Debug.Log(tower.ad + " seçildi");
        }
    }

    void SelectUpgradeTowerCard(string name)
    {
        Kart bullet = kartListesi.FirstOrDefault(x => x.ad == name);
        if (bullet != null)
        {
            CoinUpdate(bullet);
            bullet.kalanAdet--; // Kartın adedini azalt
            if (bullet.kalanAdet == 0)
            {
                bullet.aktiflik = false; // Kartın aktiflik durumunu güncelle
            }

            if (bullet.prefabTower.name == "Ok")
            {
                bullet.prefabTower.GetComponent<OkcuBullet>().setDamage(bullet.value);
            }
            else if (bullet.prefabTower.name == "Mizrak")
            {
                bullet.prefabTower.GetComponent<ArbaletBullet>().setDamage(bullet.value);
            }
            else if (bullet.prefabTower.name == "Top")
            {
                bullet.prefabTower.GetComponent<TopcuBullet>().setDamage(bullet.value);
            }
            else if (bullet.prefabTower.name == "Poison")
            {
                bullet.prefabTower.GetComponent<ZehirBullet>().setDamage(bullet.value);
            }
            else if (bullet.prefabTower.name == "Buyu")
            {
                bullet.prefabTower.GetComponent<BuyucuBullet>().setDamage(bullet.value);
            }

            BackToCharacter();
            Debug.Log(bullet.ad + " seçildi");
        }
        BackToCharacter();
    }

    void SilahHasari(string name, int cardLvl)
    {
        Kart kart = kartListesi.FirstOrDefault(x => x.ad == name && x.lvl == cardLvl);
        if (kart != null)
        {
            CoinUpdate(kart);
            kart.kalanAdet--;
            if (kart.kalanAdet == 0)
            {
                kart.aktiflik = false;
            }

            bullet.GetComponent<BulletScript>().UpdateAmmoDamage((int)kart.value);
        }
        BackToCharacter();
        Debug.Log(kart.ad + " seçildi");
    }

    void SilahHizi(string name, int cardLvl)
    {
        Kart kart = kartListesi.FirstOrDefault(x => x.ad == name && x.lvl == cardLvl);
        if (kart != null)
        {
            CoinUpdate(kart);
            kart.kalanAdet--;
            if (kart.kalanAdet == 0)
            {
                kart.aktiflik = false;
            }
            float bullletSpeed = soldier.GetComponent<CharacterFire>().fireCooldown;
            soldier.GetComponent<CharacterFire>().fireCooldown = (100 - kart.value) * bullletSpeed / 100;
        }
        BackToCharacter();
        Debug.Log(kart.ad + " seçildi");
    }

    void BombaHasari(string name, int cardLvl)
    {
        Kart kart = kartListesi.FirstOrDefault(x => x.ad == name && x.lvl == cardLvl);
        if (kart != null)
        {
            CoinUpdate(kart);
            kart.kalanAdet--;
            if (kart.kalanAdet == 0)
            {
                kart.aktiflik = false;
            }
            grenade.GetComponent<GrenadePrefabScript>().UpdateGrenadeDamage((int)kart.value);
        }
        BackToCharacter();
        Debug.Log(kart.ad + " seçildi");
    }

    void BombaDolmaHizi(string name, int cardLvl)
    {
        Kart kart = kartListesi.FirstOrDefault(x => x.ad == name && x.lvl == cardLvl);
        if (kart != null)
        {
            CoinUpdate(kart);
            kart.kalanAdet--;
            if (kart.kalanAdet == 0)
            {
                kart.aktiflik = false;
            }
            float grrenadedamage = granedeGun.GetComponent<CharacterSkill>().reloadMana;
            granedeGun.GetComponent<CharacterSkill>().reloadMana = (100 + kart.value) * grrenadedamage / 100;
        }

        BackToCharacter();
        Debug.Log(kart.ad + " seçildi");
    }

    void CanGelistir(string name, int cardLvl)
    {
        Kart kart = kartListesi.FirstOrDefault(x => x.ad == name && x.lvl == cardLvl);
        if (kart != null)
        {
            CoinUpdate(kart);
            kart.kalanAdet--;
            if (kart.kalanAdet == 0)
            {
                kart.aktiflik = false;
            }
            CharacterHealth characterHealth = soldier.GetComponent<CharacterHealth>();
            characterHealth.maxHealth = (100 + (int)kart.value) * characterHealth.maxHealth / 100;
        }

        BackToCharacter();
        Debug.Log(kart.ad + " seçildi");
    }



    // Kart seçildiğinde çağrılacak olan olay işleyicileri
    private void OkcuKulesiOlusturmaKartiSecildi()
    {
        SelectCreatedTowarCardAssignPrefab("Okçu Kulesi Oluşturma Kartı");
    }

    private void ArbaletKulesiOlusturmaKartiSecildi()
    {
        SelectCreatedTowarCardAssignPrefab("Arbalet Kulesi Oluşturma Kartı");
    }

    private void TopcuKulesiOlusturmaKartiSecildi()
    {
        SelectCreatedTowarCardAssignPrefab("Topçu Kulesi Oluşturma Kartı");
    }

    private void ZehirKulesiOlusturmaKartiSecildi()
    {
        SelectCreatedTowarCardAssignPrefab("Zehir Kulesi Oluşturma Kartı");
    }

    private void BuyucuKulesiOlusturmaKartiSecildi()
    {
        SelectCreatedTowarCardAssignPrefab("Büyücü Kulesi Oluşturma Kartı");
    }





    private void OkcuKulesiGelistirmeKartiSecildi()
    {
        SelectUpgradeTowerCard("Okçu Kulesi Geliştirme Kartı");
    }

    private void ArbaletKulesiGelistirmeKartiSecildi()
    {
        SelectUpgradeTowerCard("Arbalet Kulesi Geliştirme Kartı");
    }

    private void TopcuKulesiGelistirmeKartiSecildi()
    {
        SelectUpgradeTowerCard("Topçu Kulesi Geliştirme Kartı");
    }

    private void ZehirKulesiGelistirmeKartiSecildi()
    {
        SelectUpgradeTowerCard("Zehir Kulesi Geliştirme Kartı");
    }

    private void BuyucuKulesiGelistirmeKartiSecildi()
    {
        SelectUpgradeTowerCard("Büyücü Kulesi Geliştirme Kartı");
    }


    private void TumKuleleriGelistirmeKartiSecildi()
    {
        SelectUpgradeTowerCard("Okçu Kulesi Geliştirme Kartı");
        SelectUpgradeTowerCard("Arbalet Kulesi Geliştirme Kartı");
        SelectUpgradeTowerCard("Topçu Kulesi Geliştirme Kartı");
        SelectUpgradeTowerCard("Zehir Kulesi Geliştirme Kartı");
        SelectUpgradeTowerCard("Büyücü Kulesi Geliştirme Kartı");
    }




    private void SilahHasariGelistirmeKartiSecildi()
    {
        SilahHasari("Saldırı Gücü Geliştirme Kartı", 3);
    }

    private void SilahHiziGelistirmeKartiSecildi()
    {
        SilahHizi("Saldırı Hızı Geliştirme Kartı", 3);
    }

    private void BombaHasariGelistirmeKartiSecildi()
    {
        BombaHasari("Yetenek Gücü Geliştirme Kartı", 3);
    }

    private void BombaDolmaHiziArttirmaGelistirmeKartiSecildi()
    {
        BombaDolmaHizi("Bekleme Süresi Azaltma Kartı", 3);
    }

    private void CanGelistirmeKartiSecildi()
    {
        CanGelistir("Maksimum Can Arttırma Kartı", 3);
    }



    private void lvl2SilahHasariGelistirmeKartiSecildi()
    {
        SilahHasari("Saldırı Gücü Geliştirme Kartı", 4);
    }

    private void lvl2SilahHiziGelistirmeKartiSecildi()
    {
        SilahHizi("Saldırı Hızı Geliştirme Kartı", 4);
    }
    private void lvl2BombaHasariGelistirmeKartiSecildi()
    {
        BombaHasari("Yetenek Gücü Geliştirme Kartı", 4);
    }
    private void lvl2BombaDolmaHiziArttirmaGelistirmeKartiSecildi()
    {
        BombaDolmaHizi("Bekleme Süresi Azaltma Kartı", 4);
    }
    private void lvl2CanGelistirmeKartiSecildi()
    {
        CanGelistir("Maksimum Can Arttırma Kartı", 4);
    }



    private void lvl3SilahHasariGelistirmeKartiSecildi()
    {
        SilahHasari("Saldırı Gücü Geliştirme Kartı", 5);
    }
    private void lvl3SilahHiziGelistirmeKartiSecildi()
    {
        SilahHizi("Saldırı Hızı Geliştirme Kartı", 5);
    }
    private void lvl3BombaHasariGelistirmeKartiSecildi()
    {
        BombaHasari("Yetenek Gücü Geliştirme Kartı", 5);
    }
    private void lvl3BombaDolmaHiziArttirmaGelistirmeKartiSecildi()
    {
        BombaDolmaHizi("Bekleme Süresi Azaltma Kartı", 5);
    }
    private void lvl3CanGelistirmeKartiSecildi()
    {
        CanGelistir("Maksimum Can Arttırma Kartı", 5);
    }
    private void CoinUpdate(Kart kart)
    {
        eventSystem.GetComponent<Spawner>().coin -= kart.cost;
        eventSystem.GetComponent<Spawner>().coinText[0].text = eventSystem.GetComponent<Spawner>().coin.ToString();
    }

}






