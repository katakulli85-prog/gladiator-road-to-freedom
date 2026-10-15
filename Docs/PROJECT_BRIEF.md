# GLADIATOR: ROAD TO FREEDOM — PROJECT BRIEF

Bu dosya projenin tek dogruluk kaynağidir. Yeni bir sohbet oturumu açtığında bu dosyanın tamamını yapay zekâya yapıştır.

## 1. Kimlik
- Depo: katakulli85-prog/gladiator-road-to-freedom (PRIVATE)
- Platform: Android (öncelik); mimari iOS'a hazır tutulacak
- Çevrimdışı, tek oyunculu; oyuncu 100 savaş kazanarak özgürlüğe ulaşır
- Özgün hikâye ve karakterler; başka oyunlardan içerik kopyalanmaz
- Geliştirme ortamı: Android telefon + tarayıcı (Codespaces) + GitHub Actions (bulut derleme)

## 2. Değişmez kurallar
- Maliyet hedefi 0 $; ücretli her adım için önce kullanıcı onayı
- Gizli bilgiler (lisans, şifre, anahtar) yalnız GitHub Secrets'ta; koda ve repoya girmez
- main dalı her zaman derlenebilir olmalı
- Test edilmeyen hiçbir özellik "tamamlanmış" sayılmaz
- Her teslimat TEK bütünleşik bloktur; kullanıcı terminali tek yapıştırma + tek komutla çalıştırır
- Her aşama çıkışında çalışan, kurulabilir bir sürüm vardır

## 3. Teknik kararlar (Kutu 3 itibarıyla)
| Konu | Karar |
|---|---|
| Unity sürümü | 2022.3.55f1 (ProjectVersion.txt ve iş akışlarında sabit) |
| Derleme hattı | GitHub Actions + GameCI (unity-builder v4), Docker imajı: unityci/editor:ubuntu-2022.3.55f1-android-3 |
| Render | Built-in pipeline (geçici) → URP ileride eklenecek |
| Script backend | Mono (geçici) → IL2CPP ileride |
| Sahne üretimi | Kodla: Assets/Editor/Bootstrap.cs (BuildCommand.PerformBuild) |
| APK çıktısı | build/Android/Android.apk → artifact (14 gün) |
| Kalıcı sürüm | GitHub Releases (APK + proje ZIP) |
| Android ayarları | minSdk 24, ARMv7+ARM64, yatay ekran, paket: com.katakulli85prog.gladiatorrtf |
| Sürüm sabitleme | ProjectVersion.txt + manifest.json + workflow env (üçü birlikte) |

## 4. Kutu ilerlemesi (Faz 0 — teknik deneme)
- [x] Kutu 1: Depo kontrolü (private, yalnız README, dal: main)
- [x] Kutu 2: Unity hesabı (ücretsiz Personal)
- [ ] Kutu 3: Proje iskeleti + iki iş akışı (bu commit)
- [ ] Kutu 4: activation iş akışını elle çalıştır → .alf artifact'i
- [ ] Kutu 5: license.unity3d.com/manual ile .alf → .ulf
- [ ] Kutu 6: .ulf içeriği → UNITY_LICENSE secret
- [ ] Kutu 7: build-android iş akışı ilk koşu (lisans bulutta doğrulanır, ilk APK)
- [ ] Kutu 8: APK'yı telefona kur, test ekranını gör, fotoğraf kanıtı
- [ ] Kutu 9: İkinci koşu (tekrarlanabilirlik) + dakika/süre ölçüm raporu
- [ ] Kutu 10: v0.1 etiketi + GitHub Release (APK kalıcı) + Faz 0 raporu

Faz 0 başarı kriteri: Kutu 9'daki iki yeşil koşu + APK'nın telefonda açılması + ölçüm raporu.
Faz 0 başarısız olursa: halka, log ve çözüm seçenekleri raporlanır; oyun geliştirmeye geçilmez.

## 5. 14 aşamalık yol haritası (özet)
1. Tasarım belgesi + teknik fizibilite (Faz 0 = bu kutular)
2. Proje yapısı + Unity mimarisi
3. Ana menü ve oyun akışı
4. Hareket + kamera
5. Mobil kontroller (saldırı/ağır/blok/kaçınma/kilit)
6. Dövüş mekanikleri (hasar, stamina, hit-stop)
7. Düşman yapay zekâsı
8. İlk arena + ilk 10 savaş
9. Silah, zırh, altın, mağaza, kayıt v1
10. 100 savaşa genişleme (veri tablosu + denge simülasyonu)
11. Görsel (URP), ses, performans
12. Kayıt sağlamlaştırma + hata düzeltme
13. Yayın APK'sı (imzalı) + gerçek cihaz testleri
14. Son kontrol, v1.0 teslimat (Release: APK + ZIP)

Her aşama çıkışı: etiket + Release + CHANGELOG + bu dosyanın ilerleme bölümünün güncellenmesi.
