# GLADIATOR: ROAD TO FREEDOM — PROJECT BRIEF (v2)

Projenin tek dogruluk kaynağı. Yeni oturumda bu dosyayı yapay zekâya yapıştır.

## Kimlik
- Depo: katakulli85-prog/gladiator-road-to-freedom (PRIVATE, dal: main)
- Android öncelik; iOS'a hazır mimari. Çevrimdışı tek oyunculu. 100 savaş = özgürlük.
- Geliştirme: telefon tarayıcısı + GitHub Actions (bulut derleme). Maliyet hedefi 0 $ (ücretli adım = önce onay).

## Değimez kurallar
- Gizli bilgiler yalnız GitHub Secrets'ta; koda/repoya/sohbete girmez.
- main her zaman derlenebilir. Test edilmeyen özellik "tamamlanmış" sayılmaz.
- Teslimatlar tek bütünleşik blok; Codespaces'te Chrome çevirisi KAPALI.
- Özgün içerik; hiçbir mevcut dizi/oyundan kopya yok.
- Görsel hedef: sinematik stilize realizm (fotogerçekçi değil). Kalite > boyut.

## Teknik kararlar
| Konu | Karar |
|---|---|
| Unity | 2022.3.55f1 (sabit), imaj unityci/editor:ubuntu-2022.3.55f1-android-3 |
| APK ayarları | minSdk 24, ARMv7+ARM64, yatay ekran, paket com.katakulli85prog.gladiatorrtf |
| Sahne/arena üretimi | Editor/Generators kod script'leri (Unity Editor telefonda yok) |
| Render/IL2CPP/URP | Geçişli: şimdilik built-in+Mono; URP+IL2CPP Faz 11 öncesi |

## Kutu ilerlemesi (Faz 0)
- [x] Kutu 1 depo kontrolü · [x] Kutu 2 Unity hesabı · [x] Kutu 3 iskelet (614c5f9)
- [x] Kutu 4 .alf üretimi YEŞİL (deneme #7, 6 dk; çözülen: disk doldu, Unity yolu, donma→canlı log+15 dk tavan)
- [ ] Kutu 5 .alf→.ulf (license.unity3d.com/manual) — .alf artifact 14 gün geçerli!
- [ ] Kutu 6 UNITY_LICENSE secret · [ ] Kutu 7 ilk APK · [ ] Kutu 8 cihaz kurulumu
- [ ] Kutu 9 tekrar koşusu + dakika raporu · [ ] Kutu 10 v0.1 Release (APK+ZIP)

## Faz planı (özet)
0 hat kanıtı → 3 menü+akış → 4-5 hareket/kamera/kontroller(+haptik temel) → 6 dövüş çekirdeği → 7 AI → 8 arena+ilk 10 savaş(+eğitim) → 9 envanter/ekonomi/kayıt v1 → 10 100 savaş+boss'lar+hikâye → 11 görsel/ses/performans(URP,ekran profilleri) → 12 kayıt sağlamlaştırma → 13 IAP+yayın APK(Play 25 $ onaylı, ödeme adımında teyit) → 14 v1.0 teslim.

## Doküman indeksi (Docs/)
**ANA PLAN: GAME_DESIGN_MASTER.md** — çelişkide o geçerlidir.
EQUIPMENT_PLAN, ENEMY_PLAN, CHARACTER_ROSTER, VISUAL_DESIGN, ARENA_PLAN, COMBAT_SPEC,
SCREEN_SETTINGS, FIRST_LAUNCH_FLOW, ITEM_INVENTORY_DESIGN, ECONOMY_IAP_DESIGN,
PRODUCTION_UPGRADE_DESIGN, QUESTS_REWARDS_DESIGN, DEFEAT_RETRY_DESIGN,
STORY_CINEMATIC_DESIGN, ACCESSIBILITY_DESIGN, SAVE_DATA_SECURITY,
PERFORMANCE_QA_PLAN, APK_DELIVERY_PLAN. (Not: bu kayıtlar sohbetteki orijinal belgelerin karar bütünlüğünü koruyan özetleridir.)

## Açık kararlar (bekliyor)
Börü Han premium-vs-final çakışması; boss70 "Kırılmaz" adı; İnka/Japonya "efsane seferi" kurgusu; efsanevi makası ~%10-15; dayanıklılık 1.0'da kapalı önerisi; Release'lerde APK+ZIP politikası; test telefonu modeli bilinmiyor.
