# PERFORMANS VE QA (kayıt)
## Hedef: hedef cihazlarda kararlı 60 FPS; düşük donanım için 30 FPS seçeneği; grafik cihaz kapasitesine göre; uzun savaşta ısınma/bellek/düşüş İZLENİR (termal izleme + dinamik düşürme); kare dalgalanması azaltılır; yükleme kısa tutulur.
## Grafik optimizasyonu: LOD; görünmeyen çizim azaltımı; doku/bellek; gölge/ışık/partikül profillere bağlı; savaş efektleri performansı bozmaz; arena kalitesi korunur.
## Savaş optimizasyonu: animasyon+AI profil; havuzlama; çarpışma optimizasyonu; fizik/ragdoll/partikül cihaz sınırıyla; savaş hissi optimizasyonda BOZULMAZ.
## Android uyum: çözünürlük/oran matrisi; dokunmatik doğruluk; arka plana alma+yeniden açma testi; ses/titreşim/izin davranışları; desteklenen Android sürümleri+cihaz gereksinimleri AÇIK belirlenir (min SDK 24 zaten); iOS-mimari korunur.
## QA sınıflandırma: kritik/yüksek/orta/düşük; çökme kayıtları+loglar; düzeltilen hatanın testi YENİDEN koşar; test edilmeyen özellik "başarılı" işaretlenmez; bilinen sorunlar+cihaz sınırları raporlanır.
## Teslimat kalitesi: Console hata incelemesi; eksik referans; kullanılmayan kritik kaynak; sahne geçiş+kayıt+başlangıç doğrulama; derleme öncesi yapılandırma kontrolü; GERÇEK CİHAZ TESTİ olmadan "sorunsuz" iddiası YASAK.
## Test senaryoları (11, belgeden aynen): ilk açılış+ilk savaş; 100 savaş ilerlemesi; boss+özel hareket; karakter/silah/zırh değişimi; mağaza/envanter/yükseltme; görev/başarım/ödül; kayıt+ani kapanma; çevrimdışı; uzun oturum+bellek; satın alım hata senaryoları; farklı grafik ayarı+cihaz.
