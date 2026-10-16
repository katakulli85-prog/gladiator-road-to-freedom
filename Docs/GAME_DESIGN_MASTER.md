# GLADIATOR: ROAD TO FREEDOM — ANA TASARIM BELGESİ (GAME_DESIGN_MASTER)

Sürüm: v1 · Tarih: 16 Ekim 2026 · Durum: Taslak, kullanıcı onayı bekliyor

Bu belge oyunun **tek ana planıdır**. Diğer `Docs/` belgeleri ayrıntı kaynağıdır; bu belgeyle çelişirlerse
**bu belge geçerlidir** ve çelişki aşağıdaki "Çelişkiler" bölümünde açıkça yazılıdır.

Kısaltmalar: **[KARAR]** = kesinleşti · **[ÖNERİ]** = önerildi, onay bekliyor · **[AÇIK]** = kullanıcıyla netleştirilecek

---

## 1. ANA FİKİR

- Oyuncu, arenada savaşan **köle bir gladyatördür**. Her zafer onu özgürlüğe bir adım yaklaştırır. **[KARAR]**
- Ruh olarak **Spartacus'tan ilham alır ama kopya değildir.** Hiçbir isim, karakter, sahne, diyalog veya
  olay örgüsü Spartacus'tan (dizi, film, tarih anlatısı) alınmaz. Tüm karakterler, isimler ve hikâye özgündür.
  Telif açısından güvenli olmak zorunludur. **[KARAR]**
- Mevcut hikâye çerçevesi korunur: **"Yüzler Yolu"** — Lanista Sornus'un 100 zaferlik yolculuğu; 100. zaferi
  kazanan özgürlük belgesini alır. Ayrıntı: `STORY_CINEMATIC_DESIGN.md`. **[KARAR — mevcut belgeden]**
- Tür: çevrimdışı, tek oyunculu, üçüncü şahıs kameralı 3D arena dövüş oyunu. Platform: Android (iOS'a açık mimari).

## 2. YAPI: 100 SAVAŞ

| Savaş | Tür | Açıklama |
|---|---|---|
| 1-9, 11-19, … 91-99 | Normal | Kademeli artan zorluk |
| 10, 20, 30, 40, 50, 60, 70, 80, 90 | **BOSS** | Normal düşmandan belirgin şekilde zor |
| 100 | **FİNAL BOSS** | Maksimum zorluk; yenince oyun biter, özgürlük kazanılır |

**[KARAR]** 9 normal savaş + 1 boss = 10 savaşlık bir bölüm. Toplam 10 bölüm.

### Zorluk nasıl artar

Zorluk iki katmanla artar (iki görüş birleştirildi, bkz. Çelişki #3):

1. **Sayısal katman:** can, hasar, hız.
2. **Davranış katmanı:** düşman daha akıllı saldırır, kombo yapar, saldırıyı gizler, açığını kapatır.

Başlangıç formülü **[ÖNERİ]** — `n` = savaş numarası; değerler oyun testinde ayarlanır:

| Değer | Normal düşman | Boss (10, 20 … 90) | Final (100) |
|---|---|---|---|
| Can | `60 × (1 + 0.04 × (n−1))` | normal × **2.5** | normal × **4** |
| Vuruş hasarı | `7 × (1 + 0.03 × (n−1))` | normal × **1.5** | normal × **1.8** |
| Hareket/saldırı hızı | `+%0.5 / savaş` (üst sınır +%40) | normal + **%15** | normal + **%25** |
| Ek | — | çok aşamalı davranış, özel saldırı | 3 aşama, sinematik giriş, zırh hasar katmanları |

Örnek: 1. savaş düşmanı ~60 can, 49. savaş ~175 can; 50. savaş boss'u ~445 can (178 × 2.5); 100. savaş final boss'u ~1190 can (298 × 4).
Oyuncu da altın/ekipmanla güçlendiği için gerçek his dengede kalır.

Boss'lar, gerçek model gelene kadar **daha iri ve farklı renkte** gösterilir (geçici, "geçici" diye işaretli).

## 3. KÜLTÜRLER / BÖLGELER

**[KARAR]** Rakipler farklı kültürlerden gelir. Her kültürün kendi dövüş tarzı, silahı ve görünümü vardır.
**Tüm dövüşler arenalarda geçer.**

| Kültür | Dövüş tarzı | Tipik silah | Görsel kimlik |
|---|---|---|---|
| Antik Roma | Dengeli, disiplinli, kalkanla baskı | Gladius + scutum, trident + ağ | Mermer, kum, meşale, kırmızı-bronz |
| Antik Mısır | Akıcı, kavisli savuruşlar | Eğri kılıç (khopesh), mızrak | Altın, kireç taşı, çöl, lacivert |
| Antik Çin | Hızlı, çevik, uzun kombolar | Düz kılıç, mızrak/sopa | Kırmızı sütun, taş avlu, ipek |
| İnka | Ezici darbeler, dayanıklılık | Topuz, sapan, mızrak | And dağları, teraslar, güneş motifi |
| Bozkır / Türk Töresi | Ağır, güçlü, karşı atak | Eğri kılıç + savaş baltası, yay | Otağ, kurt motifi, gün batımı |
| Arap | Hızlı yan hareket, çift silah | Eğri kılıç (sayf), mızrak, hançer | Kum taşı, kemerler, çöl gecesi |
| Kuzey Amerika yerlileri ("Kızılderili") | Çevik, sıçramalı, ani yakın dövüş | Tomahawk, mızrak, bıçak | Orman/ova, ahşap totem, tüy ve deri |

**Saygı kuralı [KARAR]:** Her kültür özgün ve saygılı tasvir edilir. Kalıp yargı, karikatür, kutsal sembollerin
süs olarak kullanımı yoktur. Fantastik yorumlar tarihî gerçek gibi sunulmaz (mevcut belgelerdeki kural).

### Kültürlerin 100 savaşa dağılımı **[ÖNERİ — onay bekliyor]**

| Bölüm | Savaşlar | Arena / rakip kültürü | Bölüm boss'u |
|---|---|---|---|
| 1 | 1-10 | Roma (başlangıç, eğitim) | Arena Celladı |
| 2 | 11-20 | Mısır | Demir Muhafız |
| 3 | 21-30 | Arap | Kanlı Şampiyon |
| 4 | 31-40 | Çin | Kara Kalkan |
| 5 | 41-50 | İnka | Arena'nın Kasabı |
| 6 | 51-60 | Kuzey Amerika yerlileri | Kızıl General |
| 7 | 61-70 | Bozkır / Türk Töresi | Kırılmaz (ad onay bekliyor) |
| 8 | 71-80 | Karışık: her kültürün elitleri | Gölge Cellat |
| 9 | 81-90 | Karışık: şampiyonlar turnuvası | İmparatorun Şampiyonu |
| 10 | 91-100 | Bozkıra dönüş — final | **Börü Han** (Bozkırların Yenilmezi) |

Boss isimleri mevcut `ENEMY_PLAN.md`'den alınmıştır; kültüre göre yeniden adlandırılabilir **[AÇIK]**.

## 4. DÖVÜŞ SİSTEMİ

**[KARAR]** Ritim: **vur → blokla/savuştur → karşı vur → kombo.** Tek vuruşta ölme **yok.**

- **Hafif saldırı:** hızlı, düşük hasar, az stamina. 3'lü kombo; 3. vuruş bitirici (×1.6).
- **Ağır saldırı:** yavaş hazırlanır, yüksek hasar, çok stamina, düşmanı sendeletir.
- **Parry (savuşturma):** doğru zamanda basılırsa hasar engellenir, düşman kısa süre sersemler →
  karşı saldırı fırsatı. Pencere çok dar olmaz (adalet kuralı).
- **Stamina:** saldırı ve parry stamina harcar; bitince oyuncu bekleyip toparlanmak zorunda kalır.
  Rastgele tuşa basmak en iyi strateji olamaz.
- **Hasar dengesi hedefi:** 1. savaşta düşman **4-5 iyi vuruşta** ölür. İleri savaşlarda oyuncunun ekipmanı
  büyüdükçe bu oran korunur.
- **Vuruş menzili:** gerçekçi (~2-2.5 m) ve **oyuncunun baktığı yöne** göre; arkadaki düşmana vurulmaz.
- **Hasar zamanlaması:** hasar tuşa basıldığı anda değil, **savuruşun ortasında** uygulanır (animasyonla uyumlu).

Mevcut kodla ilişkisi: 16 Ekim'deki `PlayerCombat` değişikliği stamina maliyetini kaldırdı, hasarı anında
uyguladı ve menzili ~3.9 m'ye çıkardı → bu bölümle çelişiyor; **Yol haritası adım 1'de düzeltilecek.**

Ayrıntılı uzun vadeli hedefler (havadan saldırı, bölgesel hasar, bitiriciler, 13 haptik profili): `COMBAT_SPEC.md`.

## 5. DÜŞMAN ÇEŞİTLİLİĞİ

**[KARAR]** Herkes aynı dövüşmez. Temel düşman tipleri (arketip):

| Tip | Can | Hız | Hasar | Oyuncuya öğrettiği |
|---|---|---|---|---|
| Hızlı-zayıf | Düşük | Yüksek | Düşük | Parry zamanlaması |
| Yavaş-güçlü | Yüksek | Düşük | Yüksek | Kaçınma, ağır vuruşu bekleme |
| Kalkanlı savunmacı | Orta | Orta | Orta | Ağır saldırıyla savunma kırma |
| Mesafeli (mızrak) | Orta | Orta | Orta | Mesafe kapatma |
| Kombocu | Orta | Yüksek | Orta | Kombo arasında açık bulma |
| Taktikçi | Orta | Orta | Orta | Ritim değiştirme |

Bir düşman = **kültür** (görünüm + silah + tarz) × **arketip** (davranış) × **kademe** (savaş numarasına göre güç).
Böylece 100 rakip, az sayıda modelle farklı hissettirir. Mevcut 100 düşmanlık liste: `ENEMY_PLAN.md`.

## 6. GELİŞİM

- **Altın:** savaş kazanınca kazanılır; ekipman alma ve geliştirme için harcanır. **[KARAR]**
- **Ekipman / zırh:** miğfer, göğüs, kol, bacak, kalkan yuvaları; zırh **canı veya hasar azaltmayı**, silah
  **hasarı** artırır. Giyilen ekipman 3D modelde görünür. Ayrıntı: `EQUIPMENT_PLAN.md`. **[KARAR]**
- **Silah seçimi:** oyuncu silahını seçer (kılıç+kalkan, mızrak, çift kılıç, balta…). Her silahın hız/menzil/hasar
  farkı vardır. **[KARAR]**
- **Seviye (XP):** savaşlardan XP; seviye atlayınca can/stamina küçük artış. **[KARAR]**
- **Kalıcı kayıt:** ilerleme, altın, ekipman, ayarlar telefonda güvenli saklanır (atomik yazım + 3 yedek;
  kod hazır: `SaveManager.cs`). **[KARAR]**
- **Güç-parite kuralı:** parayla alınan hiçbir şey zorunlu değil; 100 savaşın tamamı ücretsiz bitirilebilir.
  (`ECONOMY_IAP_DESIGN.md`) **[KARAR — mevcut belgeden]**

## 7. AÇIK KALAN KARARLAR (kullanıcıyla netleştirilecek)

| # | Konu | Seçenekler | Mevcut belgelerdeki öneri |
|---|---|---|---|
| A1 | **Ölünce ne olur?** | (a) o savaşı tekrar et (b) bölüm başına dön (c) baştan başla | (a) Kalıcı ilerleme silinmez, savaş tekrar edilir — `DEFEAT_RETRY_DESIGN.md` |
| A2 | **Ödül döngüsü** | Her zafer: altın + XP; boss: özel silah/zırh/malzeme | Belgelerde ayrıntılı (`QUESTS_REWARDS_DESIGN.md`) |
| A3 | **Can yenileme** | Her savaş tam canla mı başlar? Savaş içi iksir olacak mı? | Tüketilebilir can eşyası önerilmiş |
| A4 | **Hikâye / ara sahneler** | Sadece metin+altyazı mı, tam sinematik mi? | 3 tam sinematik (bölüm 1, 7, 10) + kısa kamera pasajları |
| A5 | **Ses / müzik** | Telifsiz paketlerden; seslendirme olacak mı? | Başta metin + altyazı, seslendirme sonra |
| A6 | **Para kazanma modeli** | Bedava / reklamlı / uygulama içi satış / ücretli | Bedava + isteğe bağlı satış + isteğe bağlı ödüllü reklam |
| A7 | **Tutorial (eğitim)** | İlk savaş eğitim mi, ayrı eğitim alanı mı? | Aşamalı eğitim, 1. savaşa gömülü (`FIRST_LAUNCH_FLOW.md`) |
| A8 | **Arenalar kültüre göre değişsin mi?** | Her bölümde o kültürün arenası / tek büyük arena + farklı rakipler | Her bölümde yeni arena (`ARENA_PLAN.md`) |
| A9 | **Kültür sırası** | Bölüm 3'teki tablo | Onay bekliyor |
| A10 | **Oynanabilir karakterler** | Kaç gladyatör seçilebilir? Kadın gladyatör olacak mı? | 6 başlangıç tarzı; kadın ve erkek modeller |

## 8. GRAFİK VE KAYNAK NOTU

- Gerçekçi **3D modeller, zırhlar, animasyonlar ve sesler kodla üretilmez.** Lisanslı veya telifsiz hazır
  kaynaklardan gelir, sonra koda bağlanır. **[KARAR]**
- Kaynaklar:
  - **Modeller:** Unity Asset Store, Sketchfab (lisansı kontrol edilerek), Quaternius / Kenney / PolyHaven (CC0, bedava)
  - **Animasyonlar:** Mixamo (bedava; yürüme, saldırı, blok, ölme)
  - **Ortam / doku:** PolyHaven, Quixel Megascans
  - **Ses:** Freesound (lisansa bakılarak), Unity Asset Store
- **Lisans kaydı zorunlu:** her dış varlığın kaynağı ve lisansı `Docs/ASSET_LICENSES.md`'ye yazılır (dosya ilk varlıkla birlikte açılacak).
- **Boyut hedefi:** bitmiş oyun yaklaşık **1-2 GB**; optimizasyonla küçültülür:
  - **Ortak iskelet + değişen zırh/renk:** bütün gladyatörler aynı iskeleti ve animasyonları paylaşır.
  - Doku sıkıştırma (ASTC), LOD, seyirciler için düşük maliyetli görüntü.
  - Play Store ilk indirme sınırını aşarsa **Play Asset Delivery** ile içerik sonradan indirilir.
- Şu anki APK ~21 MB (yalnızca kod + kapsül karakterler).

## 9. YOL HARİTASI (bu sırayla)

| Adım | İş | Kim yapar | Bitti sayılması için |
|---|---|---|---|
| **1** | **Dövüş dengesi:** stamina, savuruş ortası hasar, yöne bağlı menzil, 4-5 vuruşta ölüm, parry→karşı saldırı | Kod (Claude) | Telefonda test: tek vuruşta ölüm yok, ritim hissediliyor |
| **2** | **100 savaş + boss zorluk sistemi:** savaş sayacı, formül, her 10'da boss, 100'de final, zafer/yenilgi akışı, kayıt | Kod (Claude) | 1→100 ilerlenebiliyor; boss'lar belirgin zor; kapat-aç ilerleme korunuyor |
| **3** | **Ekipman / altın / seviye:** ödül, dükkân/demirhane ekranı, silah seçimi | Kod (Claude) | Kazanılan altınla alınan ekipman dövüşü değiştiriyor |
| **4** | **Kültürlere göre düşman tipleri:** arketip davranışları, kültür veri tablosu | Kod (Claude) | Farklı kültür rakipleri farklı dövüşüyor |
| **5** | **Grafik / model / ses entegrasyonu:** URP + IL2CPP/ARM64 geçişi, modeller, Mixamo animasyonları, arenalar, sesler | Ortak: kaynak seçimi kullanıcı + entegrasyon Claude | Gerçek cihazda kabul edilebilir kare hızı, lisans kaydı tam |

Mağazaya çıkış öncesi (adım 5 sonrası): AAB formatı, kalıcı imza anahtarı (GitHub Secrets), hedef API seviyesi kontrolü, 64-bit (IL2CPP + ARM64).

---

## ÇELİŞKİLER (mevcut belgelerle)

| # | Konu | Eski belge | Bu belge | Çözüm |
|---|---|---|---|---|
| 1 | **Bölge listesi** | `ARENA_PLAN.md`: 10 bölge (Roma, Mısır, Türk-Hun, Çin, **Japonya, Bizans,** İnka, **Afrika, Yunan, Pers**) | 7 kültür; **Arap** ve **Kuzey Amerika yerlileri** yeni; Japonya, Bizans, Afrika, Yunan, Pers listede yok | Kullanıcının 16 Ekim listesi esas alındı. Eski bölgeler çıkarılsın mı, "karışık" bölümlere mi konsun? **[AÇIK]** |
| 2 | **Hikâye bölümleri** | `STORY_CINEMATIC_DESIGN.md`: bölümler eski 10 bölgeye bağlı (ör. Bölüm 2 Yunan, Bölüm 6 İstanbul) | Bölüm 3'teki yeni sıra | A9 onaylanınca hikâye belgesi yeni sıraya uyarlanacak |
| 3 | **Zorluk nasıl artar** | `ENEMY_PLAN.md`: "zorluk can artırmakla DEĞİL, AI/taktikle" | Boss: daha çok can, sert vuruş, hız | İkisi birleştirildi: sayı + davranış birlikte (Bölüm 2) |
| 4 | **Görsel hedef** | `PROJECT_BRIEF.md`: "sinematik stilize realizm (fotogerçekçi değil)" · `VISUAL_DESIGN.md`: "gerçekçi" | Gerçekçi, sinematik | Mobil performans için **"gerçekçi ama optimize"** önerilir; fotogerçeklik değil **[AÇIK]** |
| 5 | **Maliyet** | `PROJECT_BRIEF.md`: hedef 0 $, yalnız CC0 öncelikli | Bazı modeller ücretli olabilir | Kural korunur: **her ücretli adım önce kullanıcı onayı** |
| 6 | **Depo gizliliği** | `PROJECT_BRIEF.md`: "PRIVATE" | Depo şu an **PUBLIC** (16 Ekim'de doğrulandı) | Kullanıcı karar versin: private yapmak (GitHub Actions dakika limiti değişir) veya brief'i düzeltmek **[AÇIK]** |
| 7 | **Eksik belge** | `PROJECT_BRIEF.md` indeksinde `APK_DELIVERY_PLAN` var | Depoda bu dosya yok | Gerekirse yeniden yazılır |
| 8 | **Kod ↔ dövüş tasarımı** | `PlayerCombat.cs` (16 Ekim refactor) | Bölüm 4 | Yol haritası adım 1'de düzeltilecek |
| 9 | **Final boss adı** | `CHARACTER_ROSTER.md`: "Börü Han" hem premium karakter hem final boss | Final boss = Börü Han | Eski öneri geçerli: final boss yalnız rakip; premium listeden çıkar veya "Genç Börü" **[AÇIK]** |
