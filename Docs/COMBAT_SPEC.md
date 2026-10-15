# SAVAŞ SİSTEMİ SPESİFİKASYONU (kayıt)
## Haptik (13 olay profili): hafif darbe kısa/hafif; orta belirgin; ağır kısa-güçlü; kalkan bloğu tok; savuşturma keskin; savunma kırma güçlü; kritik örüntülü; boss'a ağır kontrollü; hasar yeme uygun; son anda kaçınma uyarı; kombo dizisi; bitirici güçlü-ölçülü; boss yere vurma. Girdiler: silah türü, güç, çarpışma sonucu, zırh, olay önemi. Kapatılabilir; hafif/normal/güçlü profiller; sık tekrar rahatsız etmez; aynı anda olaylar birleşir/öncelik sırasına girer; desteklenmeyen cihazda güvenli başarısızlık; iOS-bağımsız arayüz.
## Saldırı gücü formülü: silah temel hasarı + hafif/ağır + karakter gücü + hazırlanma süresi + mesafe + hedef zırh/savunma + kombo etkisi + dayanıklılık durumu + denge değerleri.
## Havadan saldırı (7 tür): aşağı kılıç; ağır vuruş; ileri sıçrayış; saplama; bitirici; özel yetenek atılımı; iniş+savunma. Kısıtlar: kontrolsüz tekrar yasak; başlangıç mesafesi şart; hareketli hedef garanti değil; ağır silah yavaş hazırlanır; kaçırma = kısa açık; iniş animasyonu tam; hava yön değişimi sınırlı. Düşmanlar sıçrama/yana çekilmeyle kaçabilir — haksız okuma yasak.
## İleri atılım: 7 örnek; IŞINLAMA YASAK (gerçek zamanlı mesafe/hız/çarpışma); duvar/sütun/karakter çarpışması düzgün durur.
## Kombo sistemi: zincirler (hafif→ağır geçiş, savuşturma sonrası karşı, kaçınma sonrası hızlı, hava→yer devam, silaha özgü zincirler, boss savuşturma fırsatları); pencere zamanlamaya bağlı; rastgele düğme basma optimum değil; kombo göstergesi + sadeleştirme seçeneği.
## Silaha özgü kombo örnekleri (12 silah × 3-4 hareket kayıtlı — belgeden aynen).
## Mükemmel zamanlama: kalkan savuşturma; son anda kaçınma; karşı saldırı pencereleri; boss sonrası fırsat; kalkanla dengesizleştirme. Görsel+ses+haptik ayrımı. Adalet: çok dar pencere yasak.
## Sersemletme: ağır darbe sendeletir; kalkan itmesi; karşı saldırı kombo keser; ağır zırh dirençli; bazı boss bağışıklığı; SÜRESİZ KİLİTLEME YASAK.
## 5 bölge hasar: baş/miğfer, gövde, kol/omuz, bacak, kalkan korumalı. Ölçülü farklar; rastgele kritik yasak; mobil fizik yükü düşük (vuruş olayında tek sorgu).
## Düşman telegrafları: animasyon+ses; aşırı parlak ikon yasak (atmosfer).
## AI taktikleri: baskı/savunma-bekleme/kaçınma cezası/mesafe koruma/kalkanla yaklaşma/alan kontrolü/dayanıklılık tüketme; çoklu düşmanda kaçınma fırsatı + üst üste binme yasak; girdi gizli okuma yasak.
## Bitirici: silah/boss özel; sinematik kamera; hızlı kontrole dönüş; atlanabilir; sıkıcı tekrar yasak.
## Özel yetenekler: sınıflara bağlı; maliyet (beğleme/dayanıklılık); fark = hasardan çok stil.
## Kamera/sinematik: ağır saldırı tepkisi; savuşturma vurgusu; boss girişi; bitirici yakın plan; TÜMÜ azaltılabilir/kapatılabilir.
## Denge: ağır=hafif dengesi; savunma sınırlı; kaçınma sınırlı; kombo sonsuz kilitlemez; boss anlaşılır-zor; başarı = zamanlama+taktik. Değerler testle ayarlanır.
## Modüller (13): oyuncu kontrol; kamera+kilit; saldırı/kombo; silah/hasar; savunma/kaçınma; dayanıklılık/sağlık; AI; boss davranış; animasyon; titreşim; ses; mobil UI; kayıt. Çakışan sistem yasak.
## Performans: gereksiz fizik sorgusu az; vuruş algılama zaman aralıklı; efekt havuzlu; animasyon/model ölçülü; 3 grafik profili; gerçek cihaz testi (kare/ısınma/pil). "Kare hızı garantisi" vaadi yok.
## Test kriterleri (10): entegrasyon; derleme hatası yok; oyun içinde doğrulama; cihaz kontrol testi; senaryo testleri; hatalı hasar kontrolü; animasyon-ses senkronu; haptik cihaz testi; performans ölçümü; sorun giderme. Geçici modeller "geçici" işaretli.
## Geliştirme sırası (16): hareket/kamera/kontrol → saldırı+kilit → hasar → haptik+ses → savunma/kaçınma → dayanıklılık/sağlık → silah animasyonları → kombo+hava → karşı saldırı+sersemletme → AI → yetenek+bitirici → boss → gelişim → arena+10 seviye → 100 seviye → kayıt+optimizasyon.
