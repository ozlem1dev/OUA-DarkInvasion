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
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;

public class KartMek : MonoBehaviour
{
    public static GameObject towerPrefab;
    GameObject eventSystem;
    TowerPoints towerPoints;
    LevelControl levelControl;
    public List<GameObject> towerPrefabList=new List<GameObject>();
    private List<Kart> kartListesi = new List<Kart>();

    private List<Kart> aktifKartlar = new List<Kart>();

    [SerializeField] public List<Transform> Coordinates = new List<Transform>();

    [SerializeField] public List<GameObject> Prefabs = new List<GameObject>();
    
    private int oyuncuSeviyesi = 4; // Örnek olarak oyuncu seviyesi 4 olarak kabul edildi

    void Start()
    {
        
        eventSystem=GameObject.Find("Event System");
        levelControl=eventSystem.GetComponent<LevelControl>();
        towerPoints=eventSystem.GetComponent<TowerPoints>();
        // Kartları oluştur ve listeye ekle
        // --------------------------------------------------
        // Kule inşa etme kartları
        Kart okcuKulesiOlusturmaKarti = new()
        {
            ad = "Okçu Kulesi Oluşturma Kartı",
            aciklama = "Yeni bir okçu kulesi inşa etmek için kullanılır.",
            aktiflik = true,
            kalanAdet = 2,
            olasilik = 0.4f,
            minLevel = 1,
            maxLevel = 10,
            lvl = 5,
            cost = 5,
            prefabTower=towerPrefabList.FirstOrDefault(x=>x.name==("archer tower"))
        };
        okcuKulesiOlusturmaKarti.KartSecildiginde += OkcuKulesiOlusturmaKartiSecildi;
        kartListesi.Add(okcuKulesiOlusturmaKarti);


        Kart topcuKulesiOlusturmaKarti = new()
        {
            ad = "Topçu Kulesi Oluşturma Kartı",
            aciklama = "Yeni bir topçu kulesi inşa etmek için kullanılır.",
            aktiflik = true,
            kalanAdet = 2,
            olasilik = 0.1f,
            minLevel = 1,
            maxLevel = 10,
            lvl = 2,
            cost = 4,
            prefabTower = towerPrefabList.FirstOrDefault(x => x.name == ("catapult tower"))
        };
        topcuKulesiOlusturmaKarti.KartSecildiginde += TopcuKulesiOlusturmaKartiSecildi;
        kartListesi.Add(topcuKulesiOlusturmaKarti);


        Kart buyucuKulesiOlusturmaKarti = new()
        {
            ad = "Büyücü Kulesi Oluşturma Kartı",
            aciklama = "Yeni bir büyücü kulesi inşa etmek için kullanılır.",
            aktiflik = true,
            kalanAdet = 4,
            olasilik = 0.1f,
            minLevel = 1,
            maxLevel = 10,
            lvl = 2,
            cost = 4,
            prefabTower = towerPrefabList.FirstOrDefault(x => x.name == ("mage tower"))
        };
        buyucuKulesiOlusturmaKarti.KartSecildiginde += BuyucuKulesiOlusturmaKartiSecildi;
        kartListesi.Add(buyucuKulesiOlusturmaKarti);


        Kart balistaKulesiOlusturmaKarti = new()
        {
            ad = "Balista Kulesi Oluşturma Kartı",
            aciklama = "Yeni bir balista kulesi inşa etmek için kullanılır.",
            aktiflik = true,
            kalanAdet = 4,
            olasilik = 0.1f,
            minLevel = 1,
            maxLevel = 10,
            lvl = 2,
            cost = 4,
            prefabTower = towerPrefabList.FirstOrDefault(x => x.name == ("ballista tower"))
        };
        balistaKulesiOlusturmaKarti.KartSecildiginde += BalistaKulesiOlusturmaKartiSecildi;
        kartListesi.Add(balistaKulesiOlusturmaKarti);


        Kart arbaletKulesiOlusturmaKarti = new()
        {
            ad = "Arbalet Kulesi Oluşturma Kartı",
            aciklama = "Yeni bir arbalet kulesi inşa etmek için kullanılır.",
            aktiflik = true,
            kalanAdet = 4,
            olasilik = 0.1f,
            minLevel = 1,
            maxLevel = 10,
            lvl = 2,
            cost = 4,
            prefabTower = towerPrefabList.FirstOrDefault(x => x.name == ("arbalet tower"))
        };
        arbaletKulesiOlusturmaKarti.KartSecildiginde += ArbaletKulesiOlusturmaKartiSecildi;
        kartListesi.Add(arbaletKulesiOlusturmaKarti);


        Kart mancinikKulesiOlusturmaKarti = new()
        {
            ad = "Mancınık Kulesi Oluşturma Kartı",
            aciklama = "Yeni bir mancınık kulesi inşa etmek için kullanılır.",
            aktiflik = true,
            kalanAdet = 4,
            olasilik = 0.1f,
            minLevel = 1,
            maxLevel = 10,
            lvl = 2,
            cost = 4,
            prefabTower = towerPrefabList.FirstOrDefault(x => x.name == ("archer tower"))
        };
        mancinikKulesiOlusturmaKarti.KartSecildiginde += MancinikKulesiOlusturmaKartiSecildi;
        kartListesi.Add(mancinikKulesiOlusturmaKarti);

        //----------------------------------------------------------
        // Kule geliştirme kartları

        Kart tumKuleleriGelistir = new()
        {
            ad = "Tüm Kuleleri Geliştir",
            aciklama = "Oyundaki tüm kulelerin saldırı hasarını %10 arttırır.",
            aktiflik = true,
            kalanAdet = 2,
            olasilik = 0.5f,
            minLevel = 2,
            maxLevel = 10,
            lvl = 2,
            cost = 3
        };
        tumKuleleriGelistir.KartSecildiginde += TumKuleleriGelistirmeKartiSecildi;
        kartListesi.Add(tumKuleleriGelistir);


        Kart okcuKulesiGelistirmeKarti = new()
        {
            ad = "Okçu Kulesi Geliştirme Kartı",
            aciklama = "Mevcut bir okçu kulesini geliştirmek için kullanılır.",
            aktiflik = true,
            kalanAdet = 3,
            olasilik = 0.1f,
            minLevel = 2,
            maxLevel = 10,
            lvl = 2,
            cost = 4
        };
        okcuKulesiGelistirmeKarti.KartSecildiginde += OkcuKulesiGelistirmeKartiSecildi;
        kartListesi.Add(okcuKulesiGelistirmeKarti);


        Kart buyucuKulesiGelistirmeKarti = new()
        {
            ad = "Büyücü Kulesi Geliştirme Kartı",
            aciklama = "Mevcut bir büyücü kulesini geliştirmek için kullanılır.",
            aktiflik = true,
            kalanAdet = 3,
            olasilik = 0.1f,
            minLevel = 2,
            maxLevel = 10,
            lvl = 2,
            cost = 4
        };
        buyucuKulesiGelistirmeKarti.KartSecildiginde += BuyucuKulesiGelistirmeKartiSecildi;
        kartListesi.Add(buyucuKulesiGelistirmeKarti);


        Kart topcuKulesiGelistirmeKarti = new()
        {
            ad = "Topçu Kulesi Geliştirme Kartı",
            aciklama = "Mevcut bir topçu kulesini geliştirmek için kullanılır.",
            aktiflik = true,
            kalanAdet = 3,
            olasilik = 0.1f,
            minLevel = 2,
            maxLevel = 10,
            lvl = 2,
            cost = 4
        };
        topcuKulesiGelistirmeKarti.KartSecildiginde += TopcuKulesiGelistirmeKartiSecildi;
        kartListesi.Add(topcuKulesiGelistirmeKarti);


        Kart balistaKulesiGelistirmeKarti = new()
        {
            ad = "Balista Kulesi Geliştirme Kartı",
            aciklama = "Mevcut bir balista kulesini geliştirmek için kullanılır.",
            aktiflik = true,
            kalanAdet = 3,
            olasilik = 0.1f,
            minLevel = 2,
            maxLevel = 10,
            lvl = 2,
            cost = 4
        };
        balistaKulesiGelistirmeKarti.KartSecildiginde += TopcuKulesiGelistirmeKartiSecildi;
        kartListesi.Add(balistaKulesiGelistirmeKarti);


        Kart arbaletKulesiGelistirmeKarti = new()
        {
            ad = "Arbalet Kulesi Geliştirme Kartı",
            aciklama = "Mevcut bir arbalet kulesini geliştirmek için kullanılır.",
            aktiflik = true,
            kalanAdet = 3,
            olasilik = 0.1f,
            minLevel = 2,
            maxLevel = 10,
            lvl = 2,
            cost = 4
            
        };
        arbaletKulesiGelistirmeKarti.KartSecildiginde += ArbaletKulesiGelistirmeKartiSecildi;
        kartListesi.Add(arbaletKulesiGelistirmeKarti);


        Kart mancinikKulesiGelistirmeKarti = new()
        {
            ad = "Mancınık Kulesi Geliştirme Kartı",
            aciklama = "Mevcut bir mancınık kulesini geliştirmek için kullanılır.",
            aktiflik = true,
            kalanAdet = 3,
            olasilik = 0.1f,
            minLevel = 2,
            maxLevel = 10,
            lvl = 2,
            cost = 4
        };
        mancinikKulesiGelistirmeKarti.KartSecildiginde += MancinikKulesiGelistirmeKartiSecildi;
        kartListesi.Add(mancinikKulesiGelistirmeKarti);

        // ---------------------------------------------------------
        // Karakter güçlendirme kartları
        Kart karakterinSilahiniGelistirmeKarti = new()
        {
            ad = "Karakterin Silahını Geliştirme Kartı",
            aciklama = "Karakterin düz vuruşlarıyla verdiği hasar 10% artar.",
            aktiflik = true,
            kalanAdet = 5,
            olasilik = 0.6f,
            minLevel = 1,
            maxLevel = 4,
            lvl = 1,
            cost = 2
        };
        karakterinSilahiniGelistirmeKarti.KartSecildiginde += KarakterinSilahiniGelistirmeKartiSecildi;
        kartListesi.Add(karakterinSilahiniGelistirmeKarti);


        //---------------------------------------------------------
        // Tuzak kartları
        Kart yavaslatmaTuzagiKarti = new()
        {
            ad = "Yavaşlatma Tuzağı Kartı",
            aciklama = "Düşmanları yavaşlatmak için kullanılır.",
            aktiflik = false,
            kalanAdet = 2,
            olasilik = 0.4f,
            minLevel = 1,
            maxLevel = 4,
            lvl = 2,
            cost = 4
        };
        yavaslatmaTuzagiKarti.KartSecildiginde += YavaslatmaTuzagiKartiSecildi;
        kartListesi.Add(yavaslatmaTuzagiKarti);

        

        // ---------------------------------------------------------------------------
        // Kartları rastgele yerleştir
        DisplayCards();
    }

    private void DisplayCards()
    {
        aktifKartlar.Clear();

        List<int> kullanilanIndexler = new List<int>(); // Kullanılan kart indekslerini tutmak için liste
       
        for (int i = 0; i < Coordinates.Count; i++)
        {
            // Kartı Instantiate et ve gerekli özellikleri ayarla
            Kart secilenKart = SecilenKartiGetir(kullanilanIndexler);
            GameObject cardObject = Instantiate(Prefabs[secilenKart.lvl - 1], Coordinates[i].position, Quaternion.identity, gameObject.transform);

            // Event sistemini kullanarak tıklama işlemini dinle
            Button kartButton = cardObject.GetComponent<Button>();
            kartButton.onClick.AddListener(() => secilenKart.OnKartSecildiginde());

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
                kart.kalanAdet--;
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
    }
    // Kart seçildiğinde çağrılacak olan olay işleyicileri
    private void OkcuKulesiOlusturmaKartiSecildi()
    {
        SelectCreatedTowarCardSetTrue();
        var x=kartListesi.FirstOrDefault(x => x.ad == "Okçu Kulesi Oluşturma Kartı");
        towerPrefab = x.prefabTower;
        Debug.Log("Okçu Kulesi Oluşturma Kartı seçildi.");
        // Kartın seçildiğinde yapılması gereken işlemler burada gerçekleştirilir
    }
    private void OkcuKulesiGelistirmeKartiSecildi()
    {
         
        Debug.Log("Okçu Kulesi Geliştirme Kartı seçildi.");
        // Kartın seçildiğinde yapılması gereken işlemler burada gerçekleştirilir
    }

    private void YavaslatmaTuzagiKartiSecildi()
    {
        Debug.Log("Yavaşlatma Tuzağı Kartı seçildi.");
        // Kartın seçildiğinde yapılması gereken işlemler burada gerçekleştirilir
    }

    private void KarakterinSilahiniGelistirmeKartiSecildi()
    {
        
    }

    private void MancinikKulesiGelistirmeKartiSecildi()
    {
       
    }

    private void ArbaletKulesiGelistirmeKartiSecildi()
    {
        
    }

    private void TopcuKulesiGelistirmeKartiSecildi()
    {
        
    }

    private void BuyucuKulesiGelistirmeKartiSecildi()
    {
       
    }

    private void MancinikKulesiOlusturmaKartiSecildi()
    {
        var x = kartListesi.FirstOrDefault(x => x.ad == "Mancınık Kulesi Oluşturma Kartı");
        towerPrefab = x.prefabTower;
        SelectCreatedTowarCardSetTrue();
    }

    private void ArbaletKulesiOlusturmaKartiSecildi()
    {
        var x = kartListesi.FirstOrDefault(x => x.ad == "Arbalet Kulesi Oluşturma Kartı");
        towerPrefab = x.prefabTower;
        SelectCreatedTowarCardSetTrue();
        
    }

    private void BalistaKulesiOlusturmaKartiSecildi()
    {
        var x = kartListesi.FirstOrDefault(x => x.ad == "Balista Kulesi Oluşturma Kartı");
        towerPrefab = x.prefabTower;
        SelectCreatedTowarCardSetTrue();
        
    }

    private void TopcuKulesiOlusturmaKartiSecildi()
    {
        var x = kartListesi.FirstOrDefault(x => x.ad == "Topçu Kulesi Oluşturma Kartı");
        towerPrefab = x.prefabTower;
        SelectCreatedTowarCardSetTrue();
        
    }

    private void BuyucuKulesiOlusturmaKartiSecildi()
    {
        var x = kartListesi.FirstOrDefault(x => x.ad == "Büyücü Kulesi Oluşturma Kartı");
        towerPrefab = x.prefabTower;
        SelectCreatedTowarCardSetTrue();
        
    }

    private void TumKuleleriGelistirmeKartiSecildi()
    {
        
    }
    //public void TextboxTiklandi(int textboxIndex)
    //{
    //    if (textboxIndex >= 0 && textboxIndex < aktifKartlar.Count)
    //    {
    //        GameObject clickedTextObject = textBoxlar[textboxIndex].gameObject;
    //        TextMeshProUGUI clickedText = clickedTextObject.GetComponent<TextMeshProUGUI>();

    //        string kartAdi = clickedText.text;
    //        Kart secilenKart = aktifKartlar.Find(kart => kart.ad == kartAdi);

    //        if (secilenKart != null)
    //        {
    //            // Kart bulundu, işlemleri gerçekleştirin
    //            secilenKart.OnKartSecildiginde();
    //        }
    //    }
    //}
}






