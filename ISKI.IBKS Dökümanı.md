**ISKI.IBKS PROJESİ FULL DÖKÜMANI**

Bu proje Çevre ve Şehircilik Bakanlığı tarafından tüm atıksu arıtması yapan firmalardan talep edilmiştir. Biz İSKİ olarak kendi firmamıza ait bu projeyi yapmaktayız.

Projenin temel amacı; sahada bulunan numune cihazı ile PLC arasındaki operasyonel süreçleri dijital ortama taşıyarak merkezi bir masaüstü uygulaması üzerinden uçtan uca yönetmektir. Bu kapsamda projenin temel işlevleri şunlardır:

* **Anlık İzleme ve Kontrol:** PLC'den gelen verilerin masaüstü uygulaması üzerinden gerçek zamanlı olarak takip edilmesi.  
* **Veri Entegrasyonu (SAIS API):** Elde edilen verilerin güvenli bir şekilde bulut tabanlı SAIS API’ye aktarılması ve sunucu üzerinde geliştirilen API servislerinin SAIS API tarafından çift yönlü olarak tüketilebilir hale getirilmesi.  
* **Akıllı Alarm ve Bildirim Sistemi:** Sistemde oluşabilecek kritik durumların ve arızaların (alarm) anlık olarak tespit edilmesi ve sistemde tanımlı seçili kullanıcılara otomatik **e-posta (mail)** yoluyla bildirilmesi.  
* **Gelişmiş Raporlama:** Kaydedilen tarihsel verilerin kullanıcı dostu bir arayüz üzerinden analiz edilmesi ve istenilen kriterlere göre raporlanabilmesi.  
* **Esnek Yapılandırma:** Uygulama parametrelerinin ve sistem ayarlarının "Ayarlar" paneli üzerinden dinamik olarak değiştirilebilmesi ve özelleştirilebilmesi.

Projenin çalışacağı hali hazırda 18 tesis bulunmaktadır.

Her tesiste sürekli numune alan ve numune değerleri ölçen bir kabin bulunuyor.

**Kabinde Ölçümü Yapılan Sensörler**

**Analog Sensörler**: Tesis Debi, Akış Hızı, Ph, İletkenlik, Çözünmüş Oksijen, Koi, Akm, Kabin İçi Sıcaklık, Pompa 1 Hz, Pompa 2 Hz.

**Opsiyonel Analog Sensörler:** Bu sensörler tesisten tesise opsiyonel olarak kullanılmaktadır;

Deşarj Debi, Harici Debi, Harici Debi 2

**Dijital Sensörler:** Kapı (Açık/Kapalı), Duman (Var/Yok), Su Baskını (Var/Yok), Acil Stop (Var/Yok), Pompa 1 Termik (Var/Yok), Pompa 2 Termik (Var/Yok), Temiz Su Pompa Termik (Var/Yok), Yıkama Tankı (Dolu/Boş), Enerji (Var/Yok)

**PLC Programı Hakkında**

PLC Programı tüm kabinlerde aynıdır.

DB’ler ve Tag Adress’leri tüm kabinlerde aynıdır.

1. **Sistem Akışı**  
   1. **Kurulum Ekranı**

Program yüklendikten sonra 1 kereye mahsus programın kullanılabilmesi için ayarların girilmesi gerekmektedir. Ayarlar girilmeden program başlamayacaktır. Ayarlar 4 sayfadan oluşmaktadır

1. **PLC Ayarları Sayfası:** Operatör PLC IP, Rack, Slot bilgilerini manuel olarak girer. Kabinde kullanılacak sensör listesi operatör tarafından seçilir.  
   2. **SAIS API Ayarları Sayfası:** SAIS API adresi default olarak: [https://entegrationsais.csb.gov.tr/](https://entegrationsais.csb.gov.tr/) olarak tutulur fakat operatör isterse kurulum ekranında burayı da değiştirebilir. SAIS API kullanımı için gerekli olan kullanıcı adı ve şifre operatör tarafından girilir.  
      3. **İstasyon Ayarları Sayfası:** Her istasyona ait IstasyonId (GUID) bulunmaktadır. Operatör İstasyon Id ve İstasyon Adı bilgileri girer.  
      4. **Kalibrasyon Ayarları Sayfası:** Sistemde bulunan 2 sensör için 2 noktalı kalibrasyon işlemi gerçekleştirilir. Operatör, her bir sensöre ait kalibrasyon noktalarının referans değerlerini ve her noktanın kaç saniye süreceğini sisteme girer.  
      5. **Mail Sunucusu Ayarlar Sayfası:** Operatör sistemde kullanılacak mail bildirim sistemi için gerekli olan SMTP Mail Sunucusu bilgilerini girer.

Operatör tarafından tüm bilgiler zorunlu şekilde doldurulup “Kaydet” butonuna basıldığında sistem girilen tüm bilgileri gerekli config dosyalarına ekle/değiştir yaparak ekler ve bir Bildirim Ekranı operatörü karşılar. 

2. **Bildirim Ekranı**   
- [ ] PLC Bilgileri Kaydedildi  
- [ ] SAIS API Bilgileri Kaydedildi  
- [ ] İstasyon Bilgileri Kaydedildi  
- [ ] Mail Sunucu Bilgileri Kaydedildi  
- [ ] SAIS: API’ye giriş yapıldı  
- [ ] SAIS: İstasyon Bilgileri SAIS’ten alındı ve kaydedildi

Bu ekranın ekranda kalma süresi bağlantı durumuna göre değişir. Ve ardından programımız açılır.

2. **Anasayfa**

Kurulum ekranı ve bildirim ekranı geçtikten sonra programımız default olarak Anasayfa ekranı ile açılır. Bu ekranda 4 farklı tür şey gösterilecek. Bunlar;

**Liste Şeklinde Analog Sensörler:** Analog Sensör İsmi | Anlık Değer \- birim | Saatlik Ortalama Değer \- birim

**Liste Şeklinde Dijital Sensörler:** Dijital Sensör İsmi | Dijital Sensör İsmi \- Değer

**PLC Sistem Statüsü:** Bağlantı Durumu: (Bağlı / Bağlı Değil) | Bağlantı Süresi: PLC’ye bağlı olunmuş süre | Günlük Yıkamaya Kalan Süre: PLC DB43’ten çekilecek | Haftalık Yıkamaya Kalan Süre: PLC DB43’ten çekilecek | Sistem Saati: PLC DB43’ten çekilecek

**Komponent:** API Sağlık Durumu

* **API Bağlantı Durumu:** Sağlıklı / Sağlıksız  
* **Son Veri Gönderim Tarihi:** dd.MM.yyyy  
* **Son pH Kalibrasyon Tarihi:** dd.MM.yyyy  
* **Son İletkenlik Kalibrasyon Tarihi:** [dd.MM](http://dd.MM).yyyy

3. **Simülasyon**

Bu sayfa, sahadaki operasyonun dijital bir ikizini sunarak verilerin görsel bir hiyerarşi içinde takip edilmesini sağlar:

**SCADA Görselleştirmesi:** Numune cihazı ve PLC sistemine ait kabinin yüksek çözünürlüklü bir SCADA görüntüsü üzerinden sistemin fiziksel yapısı dijital ortama yansıtılacaktır.

**Gerçek Zamanlı Veri Entegrasyonu:** Ana sayfada yer alan tüm nümerik veriler, bu ekran üzerinde ilgili ekipmanların (sensörler, motorlar, vanalar vb.) üzerine konumlandırılarak dinamik olarak gösterilecektir.

**Görsel Durum Takibi:** Değerlerin sadece sayısal olarak değil, aynı zamanda cihazların çalışma durumlarına (aktif, pasif, arıza) göre renk ve animasyon değişimleriyle görsel olarak izlenmesi sağlanacaktır.

**Sezgisel Operasyon Yönetimi:** Karmaşık verilerin, kabin üzerindeki gerçek konumlarında gösterilmesi sayesinde operatörün saha durumunu anlık olarak ve hata payı düşük bir şekilde analiz etmesine imkan tanınacaktır.

4. **Kalibrasyon**

2 Adet Sensör Kalibrasyona ihtiyaç duyar; Ph ve İletkenlik. Her iki sensörün de 2 noktalı kalibrasyonu yapılır; Zero ve Span. Bu sayfada bu kalibrasyonları başlatılabilmesi ve izlenmesi içindir. 

**Örnek:** 

**Ph : Zero | Span**

Ph kalibrasyonunun tamamlanabilmesi için sırasıyla Zero-Span yapılmalıdır. Zero tıklanır ve daha önceden girilmiş süre boyunca Ph ın anlık değeri chart’a işlenir. Eğer Ph’ın anlık değeri maksimum %10 taşma payı ile belirlenmiş referans değere yakın olmalıdır. Eğer şart sağlanıyorsa chartta line yeşil sağlanmıyorsa line kırmızı gözükür. Süre boyunca ekrana şu değerler gösterilir:

**Zero Ref: \- | Zero Meas: \-  | Zero Diff %: \- | Zero Std: \-** 

Zero kalibrasyonu bittiğinde bittiğine dair bilgi verilir ve sistem Span kalibrasyonunun başlatılmasını bekler. 

Span kalibrasyonu operatör tarafından butona basılarak başlatılır aynı işlem span için tekrarlanır ve ekranda şu değerler zerodakilerle birlikte gösterilir:

**Zero Ref: \- | Zero Meas: \-  | Zero Diff %: \- | Zero Std: \-** 

**Span Ref: \- | Span Meas: \-  | Span Diff %: \- | Span Std: \-** 

Süre bittiğinde sonuçlar SAIS API SendCalibration endpointine gönderilir. Sonuç ile alakalı operatöre bilgi verilir.

5. **Mail**

Uygulama içerisinde yer alan özelleştirilmiş **Mail Sayfası** aracılığıyla aşağıdaki operasyonlar gerçekleştirilecektir:

**Kullanıcı Yönetimi:** Alarm bildirimlerini alacak teknik personel ve yöneticilerin sisteme eklenmesi, bilgilerinin düzenlenmesi ve güncel listelerin yönetilmesi sağlanacaktır.

**Dinamik Alarm Tanımlama:** PLC'den gelen veriler için özel eşik değerleri (threshold) belirlenerek, bu limitlerin aşılması durumunda otomatik yeni alarm durumlarının oluşturulması sağlanacaktır.

**Merkezi İzleme:** Sistemde oluşan tüm alarmlar, hem anlık hem de geçmişe dönük olarak tek bir merkezi ekran üzerinden takip edilebilecektir.

**Akıllı Eşleştirme:** Hangi alarm tipinin (teknik, sistemsel veya operasyonel), hangi kullanıcı grubuna mail olarak iletileceği esnek bir şekilde yapılandırılabilecektir.

6. **Raporlama**

Bu sayfada önce raporlanmak istenen veri türü seçilir, bu veri türleri aşağıdaki gibidir:

Anlık Veriler, Kalibrasyon Verileri, Numune Verileri, Eksik Veriler, Log Kayıtları

Rapor türü seçildikten sonra tarih aralığı seçilir, sıralama tipi yeniden eskiye \- eskiden yeniye seçilir (default: yeniden eskiye) ve “Oluştur” butonuna basılır. Oluşan rapor ekranda gösterilir ve Excel \- PDF şeklinde çıktı alınabilir.

7. **Ayarlar**

**PLC Ayarları Sayfası:** PLC IP, Rack, Slot bilgileri değiştirilebilir, PLC’ye anlık ping atılıp test yapılabilir.

**SAIS API Ayarları Sayfası:** SAIS API adresi ve TOKEN almak için kullanılan kullanıcı adı şifre bilgileri değiştirilebilir.

**İstasyon Ayarları Sayfası:** İstasyona ait; IstasyonId, İstasyon Adı, bizim sağlamış olduğumuz API adresimiz, bizim sağlamış olduğumuz API Portumuz, SAIS’in bizim Apimize istek atarken Basic Auth’de kullanacağı Kullanıcı adı ve şifre bilgileri buradan ayarlanacaktır. 

**Kalibrasyon Ayarları Sayfası:** Sistemde bulunan 2 sensör için 2 noktalı kalibrasyon işlemi gerçekleştirilir. Operatör, her bir sensöre ait kalibrasyon noktalarının referans değerlerini ve her noktanın kaç saniye süreceğini sisteme girer.

**Mail Sunucusu Ayarlar Sayfası:** SMTP ayarları burada bulunuyor; SSL, Host, Kullanıcı Adı, Şifre, Doğrulama özellikleri değiştirilebiliyor. Deneme maili atabilmek için burada bir alan bulunuyor.

8. **API**

SAIS kendi sisteminden bizim sunucumuza erişmek istiyor. Bunun için kendi tarafımızda bir API’miz olacak. Örnek request ve cevaplar aşağıda belirtilmiştir.

API’de kullanılan doğrulama: Basic Auth (Her sorguda gerekmektedir)

**SAIS’e Sunulacak Servisler:**

**GetServerDateTime:** Bu servis PLC saatimizi sorgulayıp döndürür

Servis Request [https://{DOMAIN}/GetServerDateTime?stationId=e253fd69-b825-42b5-92f5-526322e41524](https://{DOMAIN}/GetServerDateTime?stationId=e253fd69-b825-42b5-92f5-526322e41524)

Servis Response { "result": true, "message": null, "objects": "2020-10-17T10:18:12" }

**GetData:** Bu servis SAIS’ten API yazılımımıza talep edilir verilen iki tarih arasındaki, verilen periyottaki verileri talep etmesi için kullanılır. SAIS API bize aşağıdaki gibi bir talep ile gelecek.

Servis Request https://{DOMAIN}/GetData?stationId=e253fd69-b825-42b5-92f5-526322e41524\&startDate=2019-12-19 10:00:00\&endDate=2019-12-19 10:00:00\&Period=1 

Servis Response { "result": true, "message": null, "objects": \[ { "Period": 1, "ReadTime": "2019-12-19T10:00:00", "AKM": 12.0, "AKM\_Status": 1, "Debi": 25.0, "Debi\_Status": 1, "KOi": 12.0, "KOi\_Status": 1, "pH": 1.0, "pH\_Status": 1 }, { "Period": 1, "ReadTime": "2019-12-19T10:01:00", "AKM": 13.0, "AKM\_Status": 1, "Debi": 24.0, "Debi\_Status": 1, "KOi": 13.0, "KOi\_Status": 1, "pH": 1.0, "pH\_Status": 1 }, ……… \] }

**GetInstantData:** Anlık olarak okuduğumuz değerleri döndürür.

Servis Request https://{DOMAIN}/GetInstantData?stationId=e253fd69-b825-42b5-92f5-526322e41525 

Servis Response { "result": true, "message": null, "objects": { "Period": 1, "ReadTime": "2020-10-18T10:23:00", "AKM": 13.0, "AKM\_Status": 1, "Debi": 24.0, "Debi\_Status": 1, "KOi": 13.0, "KOi\_Status": 1, "pH": 1.0, "pH\_Status": 1 } } }

**GetLastDataDate:** En son veritabanımıza kaydettiğimiz dakikalık verinin tarihini döndürür.

Servis Request [https://{DOMAIN}/GetLastDataDate?stationId=e253fd69-b825-42b5-92f5-526322e41524](https://{DOMAIN}/GetLastDataDate?stationId=e253fd69-b825-42b5-92f5-526322e41524)

Servis Response { "result": true, "message": null, "objects": "2020-10-17T10:18:12" }

**GetChannelInformation:** Bu servis kanal bilgilerini döndürür

Servis Request https://{DOMAIN}/GetChannelInformation?stationId=e253fd69-b825-42b5-92f5-526322e41524

Servis Response { "result": true, "message": null, "objects": \[ { "id": "30b56740-ddda-4928-8143-38710a44c457", "Brand": "Global Marka", "BrandModel": "Atık Su AKM Ölçüm Cihazı", "FullName": "INV2020101812423644 | AKM Ölçer", "Parameter": "AKM", "ParameterText": "AKM", "Unit": "5f0b9d7e-f28b-4fc2-8a6d-66a347ae36b5", "UnitText": "--", "IsActive": false, "ChannelMinValue": 5, "ChannelMaxValue": 8888, "ChannelNumber": 6, "CalibrationFormulaA": 0, "CalibrationFormulaB": 1, "SerialNumber": "99988877766555" }, { "id": "ab712f7e-34a8-40b8-9aa1-3101a6c8f55a", "Brand": "Ölçüm Cihazı Global Marka", "BrandModel": "Ölçüm Cihazı Global Model", "FullName": "INV2020101812443515 | Debi Ölçer", "Parameter": "Debi", "ParameterText": "Debi", "Unit": "ae703067-c16e-4c2a-a0a0-486937aeb340", "UnitText": "m³/saat", "IsActive": true, "ChannelMinValue": 0, "ChannelMaxValue": 9999, "ChannelNumber": 4, "CalibrationFormulaA": 1, "CalibrationFormulaB": 0, "SerialNumber": "1122334455" }, ……… \] }

**GetStationInformation:** İstasyon bilgilerini döndürür

Servis Request [https://{DOMAIN}/GetStationInformation?stationId=e253fd69-b825-42b5-92f5-526322e41524](https://{DOMAIN}/GetStationInformation?stationId=e253fd69-b825-42b5-92f5-526322e41524)

Servis Response { "result": true, "message": null, "objects": { "StationId": "edf10dfd-5fab-460b-b2fd-66b67da7a489", "Code": "0337000", "Name": "Test Sais İstasyonu", "DataPeriodMinute": 1, "LastDataDate": null, "ConnectionDomainAddress": "test.test.net", "ConnectionPort": "443" "ConnectionUser": "sais", "ConnectionPassword": "saistestpass", "Company": "Test Tesisi", "BirtDate": "2019-12-01T00:00:00", "SetupDate": "2019-12-01T00:00:00", "Adress": null, "Software": null } }

**GetCalibration:** Kalibrasyon sorgulama servisi

Servis Request https://{DOMAIN}/GetCalibration?stationId=e253fd69-b825-42b5-92f5-526322e41524\&startDate=2020-10-19 10:10:00\&endDate=2019-10-10 12:00:00 

Servis Response { "result": true, "message": null, "objects": \[ { "StationId": "edf10dfd-5fab-460b-b2fd-66b67da7a489", "DBColumnName": "AKM", "CalibrationDate": "2020-10-20T10:00:00", "ZeroRef": 0.0, "ZeroMeas": \-0.48, "ZeroDiff": \-48.0, "ZeroSTD": 0.0, "SpanRef": 144.76, "SpanMeas": 148.59, "SpanDiff": 2.65, "SpanSTD": 0.53, "ResultFactor": 0.97, "ResultZero": true, "ResultSpan": true, "Result": false } \] }

**GetPowerOffTimes:** Verilen iki tarih arasında kapalı olunan saatleri döndürür.

Servis Request https://{DOMAIN}/GetPowerOffTimes?stationId=e253fd69-b825-42b5-92f5-526322e41524\&startDate=2019-11-10 10:10:00\&endDate=2019-12-30 12:00:00 

Servis Response { "result": true, "message": null, "objects": \[ { "stationId": "e253fd69-b825-42b5-92f5-526322e41524", "startDate":"2019-12-01 12:26:00", "endDate":"2019-12-03 15:42:00" }, { "stationId": "e253fd69-b825-42b5-92f5-526322e41524", "startDate":"2019-12-01 18:26:00", "endDate":"2019-12-03 21:42:00" } \] }

**GetLog:** Sistemde oluşturulmuş olan logları verilen tarihler arasında geri döndürür.

Servis Request https://{DOMAIN}/GetLog?stationId=e253fd69-b825-42b5-92f5-526322e41524\&startDate=2019-10-10 10:10:00\&endDate=2019-10-15 12:00:00 

Servis Response { "result": true, "message": null, "objects": \[ { "logTitle":"AKM Kanal hesaplama parametresi değiştirildi", "LogDescription":"AKM kanalı y=A+Bx ayarında A parametresi 0,03 ten 0,023 çevrildi. Bilgi Merkezden alındı”, "LogCreatedDate":"2019-10-10T11:45:00" }, { "logTitle":"İstasyon Enerji Kesintisi", "LogDescription": "İstasyonda 2 saat boyunca enerji kesintisi olmuştur.”, "LogCreatedDate":"2019-10-12T11:45:00" } \] }

**StartSample:** Bu servis tetiklendiğinde PLC NumuneTetik Bitine true gönderilerek sistemin numune alması sağlanır ve cevaba göre true veya false döndürülür.

Servis Request 

https://{DOMAIN}/StartSample?StationId= e253fd69-b825-42b5-92f5-526322e41524\&Code=10687152

Servis Response { "result": true, "message": null, "objects": true } 

9. **SAIS API Servisi Kullanımı**

SAIS API’si şu servisleri sunmaktadır:

**Zaman Servisleri**

* Sunucu Saati Sorgulama Servisi

**İstasyon Servisleri**

* İstasyon Bilgileri Sorgulama Servisleri  
* İstasyon Host ve Kullanıcı Bilgileri Güncelleme Servisi

**Kanal Servisleri**

* Kanal Bilgileri Döndüren Servis  
* Kanal Bilgilerini Güncelleyen Servis

**Genel Bilgiler Servisleri**

* Parametre Bilgilerini Dönen Servis  
* Birim Bilgilerini Dönen Servis  
* Veri Durum Kodlarını Döndüren Servis  
* Diagnostik Tiplerini Dönen Servis

**Veri Servisleri**

* Veri Gönderme Servisi  
* En Son Merkeze Gönderilen Veri Tarihi Sorgulama Servisi  
* Verilen İki Tarih Arası Veri Sorgulama Servisi  
* Veri Durum Kodlarını Döndüren Servis  
* Eksik Veri Sorgulama Servisi

**Diagnostik Servisleri**

* Diagnostik Gönderme Servisi  
* Tip Numarası ile Diagnostik Gönderme Servisi  
* İstasyon Açılma Kapanma Tarihlerini Bildiren Servis

**Kalibrasyon Servisleri**

* Kalibrasyon Kaydı Gönderme Servisi  
* Merkeze Gönderilen Kalibrasyon Kayıtlarını Sorgulama Servisi

**Numune Servisleri**

* Numune Alımına Başlandı Bildirimi Servisi  
* Numune Alınırken Sorun Oluştu Servisi  
* Numune Alımı Yapıldı Bildirimi Servisi

\! NOT: Bütün servislerde dikkat edilmesi gereken ilk konu servis sonuçları “ResultStatus” nesnesi geri döndürmektedir. ResultStatus objesi 3 özellik (Property) barındırmaktadır. ResultStatus Nesnesi Yapısı public class ResultStatus { public bool result { get; set; } public string message { get; set; } public object objects { get; set; } } • result – çağırılan servisin sonucunun başarılı olup olmadığı cevabını verir. • message – çağırılan servisin sonucu hakkı bilgi mesajı geri döndürmektedir. • objects – çağırılan servisden dönen nesne veya nesneler bu nesne içerisinde geri döndürülmektedir. Object nesnesi birden fazla nesne içerebilmektedir. Nesneler Dizi (Array) halinde de geri dönebilir. 

**3.1 Güvenlik Servisleri**

**3.1.1 Login**

Bu dokümandaki tüm web servisleri kullanabilmek için önce login servisi çalıştırılmalı, ardından login

servisinin verdiği bilgiler ile servisler çalıştırılmalıdır. Login olabilmek için servise kullanıcı adınız ve

şifrenizin 2 kez MD5 li olarak şifrelenmiş hali ile göndermeniz gerekmektedir.Login olunduktan sonra

servis size 30 dakika geçerli olan bir ticket id si geri döndürecektir.

Bu ticket id’yi header’a JSON Serialize şekilde aşağıdaki gibi eklemeniz gerekmektedir.

{ TicketId \= "F942E943-3903-45D3-9A19-0191E3C0E394" }

Ticket süresi dolunca tekrar login olmanız gerekmektedir. Ticket süresi bittiğinde sistem size 401 kodlu

Unauthorized cevabı verecektir. Bu servis yapısı sizlerin verisini korumak ve güvenlik için önemlidir.

Servis Adı Servis Metot Tipi

Login Servisi POST/GET

**Servis URL**

http://entegrationsais.csb.gov.tr/Security/login

**Servis Request**

http://entegrationsais.csb.gov.tr/Security/login

**Servis Request Örneği**

http://entegrationsais.csb.gov.tr/Security/login

**Servis Body Raw**

{

 "username": "username",

 "password": "userpassword"

}

**Servis Body Raw Örneği**

{

 "username":"testuser",

 "password":"d440aed189a13ff970dac7e7e8f987b2",

}

**Servis Response**

{

"result": true,

"message": "TicketId is Lives",

"objects": {

"TicketId": "7b52f61b-8b11-4139-91db-f155e71e3d0d",

"DeviceId": null,

"User": {

"loginname": "infoline",

…

}

}

}

**3.2 Zaman Servisleri**

**3.2.1 Sunucu Saati Sorgulama Servisi**

Bu servis sunucu ile kabin içerisindeki analizör, bilgisayarlar ve io kartlar gibi saat içeren tüm dijital

cihazların sunucu ile aynı saate sahip olabilmesi için kullanılması gerekmektedir. Merkez ile SAİS kabini,

SAİS analizörleri ve yazılımın çalıştığı bilgisayarlar bu saat ile birebir uyumlu çalışması gerekmektedir.

Bu saat bilgisi BAKANLIK Merkez sunucularının Merkez saatidir.

Bu servisin tek amacı; BAKANLIK – SAİS Bilgisayarı – SAİS içerisindeki cihazlar ve analizörler hepsi aynı

saate sahip olması ve saat kaymalarından herhangi bir problem olmasını engellemek içindir. Bütün

cihazların, bilgisayarların saatleri BAKANLIK sunucuları ile senkron çalışacaktır. Kendi cihazlarınızda

GMT, GMT+2, GMT+3 saatlerine dikkat ederek geliştirme yapılması gerekmektedir. BAKANLIK Merkez

sunucu saatleri Türkiye yerel saatini verecektir.

Veri gönderimini sağlayan bilgisayar ile Bakanlık Merkez Sunucusu arasında 10sn ve üzeri fark olması

durumunda. Bilgisayar saat bilgisi ile merkez sunucu saat bilgisiyle güncellenecektir. Her 3 saatte bir bu

işlem yapılmalıdır\!

**Servis Adı Servis Metot Tipi**

**Sunucu Saati Sorgulama Servisi POST**

**Servis URL**

http://entegrationsais.csb.gov.tr/SAIS/GetServerDateTime

**Servis Request**

http://entegrationsais.csb.gov.tr/SAIS/GetServerDateTime

**Servis Request Örneği**

http://entegrationsais.csb.gov.tr/SAIS/GetServerDateTime

**Servis Body Raw**

(Yok)

**Servis Body Raw Örneği**

(Yok)

**Servis Response**

{

 "result": true,

 "message": null,

 "objects": "2020-10-17T10:18:12"

}

**3.3 SAİS Bilgi Servisleri**

**3.3.1 SAİS Bilgi Bilgileri Sorgulama Servisleri**

Bu servis istasyon ile ilgili son güncel bilgileri sorgulama servisidir. Devamlı sorgulama ile merkez ile

kabin yazılımı içerisindeki istasyon bilgilerinizi güncel tutmanız için kullanılmalıdır.

**Servis Adı Servis Metot Tipi**

**İstasyon Bilgileri Sorgulama Servisleri POST**

**Servis URL**

http://entegrationsais.csb.gov.tr/SAIS/GetStationInformation

**Servis Request**

http://entegrationsais.csb.gov.tr/SAIS/GetStationInformation?stationId={istasyon id’si}

**Servis Request Örneği**

http://entegrationsais.csb.gov.tr/SAIS/GetStationInformation?stationId= BF9A34E1-1CA7-4CBE-B5E7-E45BC52A5857

**Servis Body Raw**

(Yok)

**Servis Body Raw Örneği**

(Yok)

**Servis Response**

{

 "result": true,

 "message": null,

 "objects": {

 "StationId": "edf10dfd-5fab-460b-b2fd-66b67da7a489",

 "Code": "0337000",

 "Name": "Test Sais İstasyonu",

 "DataPeriodMinute": 1,

 "LastDataDate": 2019-12-01T00:00:00,

 "ConnectionDomainAddress": "istanbul.ssl",

 "ConnectionPort": "443",

 "ConnectionUser": "infoline",

 "ConnectionPassword": "infoline123",

 "Company": "Test Tesisi",

 "BirtDate": "2019-12-01T00:00:00",

 "SetupDate": "2019-12-01T00:00:00",

 "Adress": İstanbul,

 "Software": test yazılımı

 }

}

**3.3.2 İstasyon Host ve Kullanıcı Bilgileri Güncelleme Servisi**

Bu servis, istasyonun içerisinde geliştirdiğiniz kabin yazılımının

\- Dış erişim IP/Host Bilgisini

\- Kabin yazılımı Kullanıcı Adı ve Şifre Bilgisini

Merkezi yazılımda güncelleme yapmanızı sağlar.

**Servis Adı Servis Metot Tipi**

**İstasyon Host ve Kullanıcı Bilgileri Güncelleme Servisi POST**

**Servis URL**

http://entegrationsais.csb.gov.tr/SAIS/SendHostChanged

**Servis Request**

http://entegrationsais.csb.gov.tr/SAIS/SendHostChanged

**Servis Request Örneği**

http://entegrationsais.csb.gov.tr/SAIS/SendHostChanged

**Servis Body Raw**

{

"StationId":"{İstasyon İd’si}",

"ConnectionUser": "{Kabin yazılımı kullanıcı adı}",

"ConnectionPassword": "{Kabin yazılımı kullanıcı Şifresi}",

"ConnectionDomainAddress": "{Kabin Yazılımı Erişim Host veya IP Numarası}",

"ConnectionPort": "{Kabin Yazılımı Erişim için kullanılan PORT bilgisi}"

}

**Servis Body Raw Örneği**

{

"StationId": "BF9A34E1-1CA7-4CBE-B5E7-E55BC52A3836",

"ConnectionUser": "testuser",

"ConnectionPassword": "testpassword",

"ConnectionDomainAddress": "192.168.1.1",

"ConnectionPort": "443",

}

**Servis Response**

{

 "result": true,

 "message": "SAİS bağlantı bilgileri başarı ile güncellendi.",

 "objects": null

}

**3.4 Kanal Servisleri**

**3.4.1 Kanal Bilgileri Döndüren Servis**

Bu servis, SAİS veri gönderen cihazların kanal bilgilerini, merkezdeki en son güncel durumu ile

döndüren servistir. Kanal Bilgileri, Merkezden periyodik olarak sorgulama yapılarak SAİS yazılımında

güncellenecektir. En geç saat te 1 kez olmak şartı ile kanal sorgulamasını yapıp değişiklikleri

güncellemeniz gerekmektedir. Kesinti, iletişim ve benzeri arıza durumlarında yazılım Merkeze eriştiği

ilk anda sorgulamasını yapması gerekmektedir. Kanal hesaplama parametrelerini de bu servisten

alabilirsiniz.

**Servis Adı Servis Metot Tipi**

**Kanal Bilgileri Döndüren Servis POST**

**Servis URL**

http://entegrationsais.csb.gov.tr/SAIS/GetChannelInformationByStationId

**Servis Request**

http://entegrationsais.csb.gov.tr/SAIS/GetChannelInformationByStationId?stationId={istasyon id’si}

**Servis Request Örneği**

[http://entegrationsais.csb.gov.tr/SAIS/GetChannelInformationByStationId?stationId=BF9A34E1-1CA7-4CBE-B5E7-E45BC52A3857](http://entegrationsais.csb.gov.tr/SAIS/GetChannelInformationByStationId?stationId=BF9A34E1-1CA7-4CBE-B5E7-E45BC52A3857)

**Servis Body Raw**

(Yok)

**Servis Body Raw Örneği**

(Yok)

**Servis Response**

{

 "result": true,

 "message": null,

 "objects": \[

 {

 "Brand": null,

 "BrandModel": null,

 "FullName": "INV2019122715225994 | \- \- Analizör",

 "Parameter": "pH",

 "ParameterText": "pH",

 "Unit": "48155fb8-61d5-43c5-82bd-46a99223f907",

 "UnitText": "--",

 "IsActive": true,

 "ChannelMinValue": 0.0,

 "ChannelMaxValue": 200.0,

 "ChannelNumber": 4,

 "CalibrationFormulaA": 1.0,

 "CalibrationFormulaB": 0.0,

 "SerialNumber": "2019122715225916"

 },

 ………

 \]

}

**Response Objesi Tanıtımı**

"Brand": Marka Adını Geri Döndürür, \- string

"BrandModel": Model Bilgisini Döndürür,- string

"FullName": Kanal İsmini döndürür, uzun verir seri numarası ve adı halinde, \- string

"Parameter": Kanal Parametresinin servis ve veri tabanı adını döndürür, \- string

"ParameterText": Kanal Parametresinin açıklamasını döndürür, \- string

"Unit": Kanalın hangi birimde veri gönderdiğinin birim id’si ile döndürür, \- Guid

"UnitText": Birimin text halini döndürür \- string

"IsActive": Kanalın aktif olup olmadığı bilgisini döndürür, \- bool

"ChannelMinValue": Kanalın ölçebileceği Min değer, \- double

"ChannelMaxValue": Kanalın ölçebileceği Maks değer, \- double

"ChannelNumber": Kanalın numarası, short

"CalibrationFormulaA": Kanalın Hesaplama Parametre A (Toplam Değeri) y=A+Bx Değeri, \- double

"CalibrationFormulaB": Kanalın Hesaplama Parametre B (Çarpan Değeri) y=A+Bx Değeri, \- double

"SerialNumber": Analizörün seri numarası \- string

**3.4.2 Kanal Bilgilerini Güncelleyen Servis**

Bu servis, merkezdeki veri tabanında bulunan kanal bilgilerini güncellemek için kullanılır. Kanal bilgileri

periyodik olarak veya değişim gördüğünde merkezden de güncellenmesi gerekmektedir.

Servis aracılığı ile sadece “Servis Body Raw” örneğinde belirtilen alanlar güncellenebilmektedir.

Bu bilgiler Kanal bazındaki bilgilerdir. Ekipman markası, modeli ve Seri Numarası gibi bilgileri

güncellemek için web sitesi üzerinden merkez yazılımdan güncelleme işlemi gerçekleştirebilirsiniz.

**Servis Adı Servis Metot Tipi**

**Kanal Bilgilerini Güncelleyen Servis POST**

**Servis URL**

http://entegrationsais.csb.gov.tr/SAIS/UpdateChannelInformation

**Servis Request**

http://entegrationsais.csb.gov.tr/SAIS/UpdateChannelInformation

**Servis Request Örneği**

http://entegrationsais.csb.gov.tr/SAIS/UpdateChannelInformation

**Servis Body Raw**

{

 "id": "{Kanal id Bilgisi}",

 "Unit": "{Unit}",

 "IsActive": {Aktif mi Pasif mi ?},

 "ChannelMinValue": {Min. Ölçüm Değeri},

 "ChannelMaxValue": {Max. Ölçüm Değeri.},

 "ChannelNumber": {Kanal No},

 "CalibrationFormulaA": {Kalibrasyon A Parametresi ( Toplama ) },

 "CalibrationFormulaB": {Kalibrasyon B Parametresi ( Çarpan ) }

}

**Servis Body Raw Örneği**

{

 "id": "30b56740-ddda-4928-8143-38710a44c457",

 "Unit": "5f0b9d7e-f28b-4fc2-8a6d-66a347ae36b5",

 "IsActive": false,

 "ChannelMinValue": 5,

 "ChannelMaxValue": 8888,

 "ChannelNumber": 6,

 "CalibrationFormulaA": 0,

 "CalibrationFormulaB": 1

}

**Servis Response**

{

 "result": true,

 "message": "Monitör güncelleme işlemi başarıyla tamamlandı \!.",

 "objects": null

}

**3.5 Genel Bilgiler Servisleri**

**3.5.1 Parametre Bilgilerini Dönen Servis**

Parametre servisi, gönderilecek olan verilerin yazılış tiplerini ve merkezdeki veri tabanındaki kolon

isimlerini geri döndüren servistir. Veri gönderirken bu isimler kullanılarak gönderilmesi

gerekmektedir.

**Servis Adı Servis Metot Tipi**

**Parametre Bilgilerini Dönen Servis POST**

**Servis URL**

http://entegrationsais.csb.gov.tr/SAIS/GetParameters

**Servis Request**

http://entegrationsais.csb.gov.tr/SAIS/GetParameters

**Servis Request Örneği**

http://entegrationsais.csb.gov.tr/SAIS/GetParameters

**Servis Body Raw**

(Yok)

**Servis Body Raw Örneği**

(Yok)

**Servis Response**

{

 "result": true,

 "message": null,

 "objects": \[

 {

 "Parameter": "AKM",

 "ParameterText": "AKM",

 "MonitorType": 0,

 "MonitorTypeText": "Ana Ölçüm Kanal"

 },

 {

 "Parameter": "CozunmusOksijen",

 "ParameterText": "Çözünmüs Oksijen",

 "MonitorType": 0,

 "MonitorTypeText": "Ana Ölçüm Kanal"

 },

 {

 "Parameter": "Debi",

 "ParameterText": "Debi",

 "MonitorType": 0,

 "MonitorTypeText": "Ana Ölçüm Kanal"

 },

 {

 "Parameter": "KOi",

 "ParameterText": "KOi",

 "MonitorType": 0,

 "MonitorTypeText": "Ana Ölçüm Kanal"

 },

 ………….

 \]

}

**3.5.2 Birim Bilgilerini Dönen Servis**

Birimler servisi, birimlerin tutulduğu yazı ve kimlik bilgilerinin geri döndürüldüğü servistir. Veri

gönderme, kalibrasyon verisi gönderme gibi durumlarda birim gönderirken bu kimlik bilgileri ile

gönderilmesi ve okunması gerekmektedir.

**Servis Adı Servis Metot Tipi**

**Birim Bilgilerini Dönen Servis POST**

**Servis URL**

http://entegrationsais.csb.gov.tr/SAIS/GetUnits

**Servis Request**

http://entegrationsais.csb.gov.tr/SAIS/GetUnits

**Servis Request Örneği**

http://entegrationsais.csb.gov.tr/SAIS/GetUnits

**Servis Body Raw**

(Yok)

**Servis Body Raw Örneği**

(Yok)

**Servis Response**

{

 "result": true,

 "message": null,

 "objects": \[

 {

 "id": "a51f47b6-32d0-4ca8-8242-df78240ef5a0",

 "Name": "-"

 },

 {

 "id": "a1264a44-c665-4503-bc89-5f83705ed269",

 "Name": "ml/l"

 },

 {

 "id": "e5ef01a5-6238-4dd9-8ff3-371a6956d2a8",

 "Name": "m3/t"

 },

 …………

 \]

}

**3.6 Veri Servisleri**

**3.6.1 Veri Gönderme Servisi**

Veri göndermek için kullanılan servistir. Ölçümleri bu servis ile gönderimi yapılacaktır.

Veri göndermeden önce önemli noktalar;

\- "Stationid": "{istasyon id’si}",

\- "Readtime": "{Okuma zamanı}",

\- "SoftwareVersion": "{kabin yazılımı versiyonu}",

\- "Period": "{ölçüm periyodu aşağıda listede belirtilmiştir. }"

**Periyot Kodları Listesi.**

**Periyot Kodu Periyot Süresi**

1 1 Dakika

2 5 Dakika

4 30 Dakika

8 Saatlik

16 Günlük

32 Aylık

Not: SAİS için veri gönderme işlemlerinde aksi belirtilmedikçe sadece **1** dakikalık periyot merkeze

aktarılacaktır.

Bu parametreler dikkatli verilmesi gerekmektedir.

Bunun yanında hangi parametreler ölçülüyor ise o parametre ismi ile obje doldurulur.

Örneğin “KOI” ve “AKM” gönderiliyorsa üstteki parametreler ve statusları ile birlikte bu iki parametre

gönderilmelidir. Aşağıdaki gibi olması gerekmektedir;

{

 "Stationid": "{istasyon id’si}",

 "Readtime": "2019-12-19T10:00:00",

 "SoftwareVersion": "{kabin yazılımı versiyonu}",

 "Period": "1",

 "KOI ": "34",

 "KOIStatus": "1",

 "AKM ": "32",

 "AKMStatus ": "0",

}

\!\!\! Veri gönderme servisi UPDATE işlemi içermemektedir.

\!\!\! 48 saat geçmiş veriler merkez tarafından kabul edilmeyecektir.

\!\!\! 48 saati aşmış verilerin gönderimi hiçbir şekilde gönderilmeyecektir.

\!\!\! Veri periyodu 1 dk dışında hiçbir veri kabul edilmeyecektir.

\!\!\! Sistemde var olan status kodları dışında gönderilen kodlar kabul edilmeyecektir.

\!\!\! Tanımlı olmayan bir kanal için gönderilen veriler kabul edilmeyecektir.

\!\!\! Değeri olan ama Status bilgisi olmayan veriler kabul edilmeyecektir.

**Servis Adı Servis Metot Tipi**

**Veri Gönderme Servisi POST**

**Servis URL**

http://entegrationsais.csb.gov.tr/SAIS/SendData

**Servis Request**

http://entegrationsais.csb.gov.tr/SAIS/SendData

**Servis Request Örneği**

http://entegrationsais.csb.gov.tr/SAIS/SendData

**Servis Body Raw**

{

"Stationid": "{istasyon id’si}",

"Readtime": "{Okuma zamanı}",

"SoftwareVersion": "{kabin yazılımı versiyonu}",

"Period": "{ölçüm periyodu dk cinsinden}",

"AkisHizi": "{Akış hızı ölçüm değeri}",

"AkisHizi\_Status": "{Akış Hızı Status değeri}",

"AKM": "{AKM ölçüm değeri}",

"AKM\_Status":"{Askıda katı madde status değeri}",

"CozunmusOksijen": "{Çözünmüş oksijen ölçüm değeri}",

"CozunmusOksijen\_Status": "{Çözünmüş oksijen status değeri}",

"Debi": "{Debi ölçüm değeri}",

"Debi\_Status": "{Debi status değeri}",

"KOi": "{Kimyasal Oksijen İhtiyacı ölçüm değeri}",

"KOi\_Status": "{Kimyasal Oksijen İhtiyacı status değeri}",

"pH": "{pH ölçüm değeri}",

"pH\_Status": "{pH status değeri}",

"Sicaklik": "{Sıcaklık ölçüm değeri}",

"Sicaklik\_Status": "{Sıcaklık status değeri}"

}

**Servis Body Raw Örneği**

{

"Readtime": "2020-10-18T10:22:00",

"Stationid": "edf10dfd-5fab-460b-b2fd-66b67da7a489",

"SoftwareVersion": "Historyv2",

"period":1,

"KOi":12,

"KOi\_Status":1,

"Debi":25,

"Debi\_Status":1,

"AKM":12,

"AKM\_Status":1,

"pH":1,

"ph\_Status":1

}

**Servis Response**

{

 "result": true,

 "message": "1",

 "objects": {

 "Period": 1,

 "ReadTime": "2020-10-18T10:22:00",

 "AKM": 12.0,

 "AKM\_Status": 1,

 "Debi": 25.0,

 "Debi\_Status": 1,

 "KOi": 12.0,

 "KOi\_Status": 1,

 "pH": 1.0,

 "pH\_Status": 1

 }

}

**3.6.2 En Son Merkeze Gönderilen Veri Sorgulama Servisi**

Merkeze kabinden gönderilen en son verinin saat bilgisini döndüren servistir. Gönderimlerde bir

sorun yaşanır ise bu servisten dönen tarih gerekli kullanılabilir.

• 1 Dakikalık veri sorgulamak için period nesnesine “1” verilmeli

• 5 Dakikalık veri sorgulamak için period nesnesine “2” verilmeli

• 30 Dakikalık ortalama veri sorgulamak için period nesnesine “4” verilmeli

• 1 Saatlik ortalama veri sorgulamak için period nesnesine “8” verilmeli

• 1 Günlük ortalama veri sorgulamak için period nesnesine “16” verilmeli

**Periyot Kodu Periyot Süresi**

1 1 Dakika

2 5 Dakika

4 30 Dakika

8 Saatlik

16 Günlük

32 Aylık

**Servis Adı Servis Metot Tipi**

**En Son Merkeze Gönderilen Veri Tarihi Sorgulama Servisi POST**

**Servis URL**

http://entegrationsais.csb.gov.tr/SAIS/GetLastData

**Servis Request**

http://entegrationsais.csb.gov.tr/SAIS/GetLastData?stationId={SAİSid}\&period={Periyod}

**Servis Request Örneği**

http://entegrationsais.csb.gov.tr/SAIS/GetLastData?stationId=edf10dfd-5fab-460b-b2fd-66b67da7a489\&period=1

**Servis Body Raw**

(Yok)

**Servis Body Raw Örneği**

(Yok)

**Servis Response**

{

 "result": true,

 "message": null,

 "objects": {

 "Period": 1,

 "ReadTime": "2020-10-18T10:22:00",

 "AKM": 12.0,

 "AKM\_Status": 1,

 "Debi": 25.0,

 "Debi\_Status": 1,

 "KOi": 12.0,

 "KOi\_Status": 1,

 "pH": 1.0,

 "pH\_Status": 1

 }

}

**3.6.3 Eksik Veri Sorgulama Servisi**

Merkeze, SAİS kabininden gelmesi gereken fakat gelmemiş olan eksik verilerin sorgulandığı servistir.

Son 48 saat içerisinde gelmesi gereken fakat gelmeyen verilerin tarihlerinin listesini verir.

**Servis Adı Servis Metot Tipi**

**İki Tarih Arası Eksik Veri Sorgulama Servisi POST**

**Servis URL**

http://entegrationsais.csb.gov.tr/SAIS/GetMissingDates

**Servis Request**

http://entegrationsais.csb.gov.tr/SAIS/GetMissingDates?stationId={SAIS id’si}

**Servis Request Örneği**

http://entegrationsais.csb.gov.tr/SAIS/GetMissingDates?stationId=e253fd69-b825-42b5-92f5-526322e41524

**Servis Body Raw**

(Yok)

**Servis Body Raw Örneği**

(Yok)

**Servis Response**

{

 "result": true,

 "message": null,

 "objects": {

 "StartDate": "2020-11-24T00:00:00",

 "EndDate": "2020-11-26T00:00:00",

 "MissingDates": \[

 "2020-11-24T00:55:00",

 "2020-11-24T02:14:00",

 "2020-11-24T03:36:00",

 "2020-11-24T16:08:00",

 "2020-11-24T18:46:00",

 "2020-11-24T18:47:00"

 \]

 }

}

**3.6.4 Verilen İki Tarih Arası Veri Sorgulama Servisi**

Merkeze, SAİS kabininden gönderilen ve verilen iki tarih arası tüm verileri döndüren servistir.

Gönderilen verilerin hesaplanmış parametrelerini buradan da sorgulayabilirsiniz.

Burada veriyi çekerken, dakikalık mı, saatlik mi veya günlük mü olduğu belirtilerek çekilecektir. Bunun

için parametre olarak period alanını kullanmanız gerekmektedir. Aşağıda period alanı için ilgili bilgiler

bulunmaktadır.

• 1 Dakikalık veri sorgulamak için period nesnesine “1” verilmeli

• 5 Dakikalık ortalama veri sorgulamak için period nesnesine “2” verilmeli

• 30 Dakikalık ortalama veri sorgulamak için period nesnesine “4” verilmeli

• 1 Saatlik ortalama veri sorgulamak için period nesnesine “8” verilmeli

• 1 Günlük ortalama veri sorgulamak için period nesnesine “16” verilmeli

**Periyot Kodu Periyot Süresi**

1 1 Dakika

2 5 Dakika

4 30 Dakika

8 Saatlik

16 Günlük

32 Aylık

**Servis Adı Servis Metot Tipi**

**Verilen iki Tarih Arası Veri Sorgulama Servisi POST**

**Servis URL**

http://entegrationsais.csb.gov.tr/SAIS/GetDataByBetweenTwoDate

**Servis Request**

[http://entegrationsais.csb.gov.tr/SAIS/GetDataByBetweenTwoDate?stationId={SAİS](http://entegrationsais.csb.gov.tr/SAIS/GetDataByBetweenTwoDate?stationId={SAİS) id’si}\&period={periyod}\&startDate={küçük tarih}\&endDate={büyük tarih}

**Servis Request Örneği**

http://entegrationsais.csb.gov.tr/SAIS/GetDataByBetweenTwoDate?stationId=e253fd69-b825-42b5-92f5-526322e41524\&period=1\&startDate=2020-10-18 10:22:00\&endDate=2020-10-18 10:23:00

**Servis Body Raw**

(Yok)

**Servis Body Raw Örneği**

(Yok)

**Servis Response**

{

 "result": true,

 "message": null,

 "objects": \[

 {

 "Period": 1,

 "ReadTime": "2020-10-18T10:22:00",

 "AKM": 12.0,

 "AKM\_Status": 1,

 "Debi": 25.0,

 "Debi\_Status": 1,

 "KOi": 12.0,

 "KOi\_Status": 1,

 "pH": 1.0,

 "pH\_Status": 1

 },

 {

 "Period": 1,

 "ReadTime": "2020-10-18T10:23:00",

 "AKM": 13.0,

 "AKM\_Status": 1,

 "Debi": 24.0,

 "Debi\_Status": 1,

 "KOi": 13.0,

 "KOi\_Status": 1,

 "pH": 1.0,

 "pH\_Status": 1

 }

 \]

}

**3.6.5 Veri Durum Kodlarını Döndüren Servis**

Verilerin ölçüm durumlarında, ortalama alma durumların ve benzeri tüm durumlarda gönderilmeden

önce işaretlenmiş data bilgilerini döndürür.

NOT \!\!\!: SAİS Tesisinde aşağıdaki kuralların ve durumların hepsi tanımlı olması ve kurallar dahilinde

çalıştırılıyor olması zorunludur. Aşağıdaki durum kodları dışında hiçbir durum kodları kabul

edilmeyecektir.

**Servis Adı Servis Metot Tipi**

**Veri Durum Kodlarını Döndüren Servis POST**

**Servis URL**

http://entegrationsais.csb.gov.tr/SAIS/GetDataStatusDescription

**Servis Request**

http://entegrationsais.csb.gov.tr/SAIS/GetDataStatusDescription

**Servis Request Örneği**

http://entegrationsais.csb.gov.tr/SAIS/GetDataStatusDescription

**Servis Body Raw**

(YOK)

**Servis Body Raw Örneği**

(YOK)

**Servis Response**

\[

 {

 "StatusCode": 0,

 "StatusName": "VeriYok",

 "StatusDescription": "Veri Yok",

 "IsValid": false

 },

 {

 "StatusCode": 1,

 "StatusName": "VeriGecerli",

 "StatusDescription": "Veri Gecerli",

 "IsValid": true

 },

 {

 "StatusCode": 2,

 "StatusName": "OlcumYok",

 "StatusDescription": "Olcum Yok",

 "IsValid": false

 },

 {

 "StatusCode": 3,

 "StatusName": "\<Ort",

 "StatusDescription": "Ortalama Altinda",

 "IsValid": false

 },

 ………..

 \]

}

Status Kodu \- Status Kod Adı \- Açıklama

0 \- VeriYok \- Veri toplama ve kayıt sisteminin çalışmadığı durum. Bu durumda

ölçüm verisi \-9999 olarak yazılabilir.

1 \- VeriGecerli \- Geçerli Ölçüm Verisi (Yönetmelikte geçerli kılınan %80 Geçerli veri

olması durumunda)

4 \- Gecersiz \- Ölçüm verisinin geçersiz olduğu durum. Cihazın ölçüm kanalının

Alarm durumu

35 \- Nokta1Kalibrasyon \- 1\. Nokta kalibrasyonu. 1\. Noktada kalibrasyon yapılırken

kullanılacak durum bilgisi.

36 \- Nokta2Kalibrasyon \- 2\. Nokta kalibrasyonu. 2\. Noktada kalibrasyon yapılırken

kullanılacak durum bilgisi.

7 \- KalLimitDisi \- Kalibrasyon uyarı eşiği, Ölçüm kanalındaki veriler durum olarak "19"

de kalacak ve kanalın kalibrasyon sonrasındaki ölçüm verileri

ortalamaya katılacaktır. Ancak ardı ardına 5 defa bu kanal için

yapılan kalibrasyon kontrolünde tekrarlanan Kalibrasyon Limit Dışı "

durumu olursa ölçüm kanalı "9" durumuna geçecek ve kalibrasyon

hatası " olacaktır. Ölçüm kanalı bu duruma geçtiğinde değerler

geçersiz sayılacaktır.

8 \- IletişimHatasi \- Cihaz ile haberleşme kurulamıyor veya iletişim kurulmasına rağmen

ölçüm kanalından veri gelmiyor ise bu durum kodu kullanılacaktır.

9 \- SistemKal \- Şu an da analizörün kalibrasyonda olduğu bilgisini verir.

12 \- Alarm \- Cihazların ölçümünü etkileyecek herhangi bir alarm oluştuğunda mevcut

durum kodlarından herhangi biri ile eşleştirilemiyorsa alarm olarak

tanımlanmalıdır.

15 \- Purge \- Cihaz Purge durumuna geçtiğinde bu statüs verilir.

19 \- KalHatasi \- Kalibrasyon uyarı eşiklerinin aşılması durumunda verinin durumu

kalibrasyon hatası olarak işaretlenecek ve bir sonraki kalibrasyona

kadar bu durum ölçüm kanalı için sabit olarak kalacak. Cihaz ölçüm

kanalı veriyi yazacaktır.

21 \- AkisYok \- Akış ölçerde hata olduğu durumlarda bu kod kullanılacaktır.

22 \- DesarjYok \- Deşarj olmadığında bu statusü verecektir.

23 \- Yikama \- Günlük Yıkama sırasında bu statüs verilecektir.

24 \- HaftalikYikama \- Haftalık Yıkama sırasında bu statüs verilecektir.

25 \- IstasyonBakimda \- İstasyon bakımda ise bu statsü verilecektir.

26 \- TesisBakimda \- Tesis bakıma alındığında bu statüs verilecektir.

39 \- OlcumAraligiDisinda \- Ölçüm ölçülebilecek aralık dışına çıktığında (üzerine veya altına) bu

durum kodu kullanılacaktır.

30 \- CihazBakımda \- Cihaz bakımda ise bu statüs verilecektir.

31 \- Debi Arızası \- Debide arıza olduğunda bu statüs verilecektir.

200 \- GecersizYikama \- Eksik veya Geçersiz Yıkama bilgisi.

201 \- GecersizHaftalikYikama \- Eksik veya Geçersiz Haftalık Yıkama bilgisi.

202 \- GecersizKalibrasyon \- Eksik veya Geçersiz Aylık Kalibrasyon bilgisi.

203 \- Gecersiz AkisHizi \- Geçersiz Akış Hızı değeri.

204 \- Gecersiz Debi \- Geçersiz Debi Değeri

205 \- Tekrar Veri \- Ard ardına tekrar eden mükerrer veri.

206 \- Geçersiz Ölçüm Birimi

Uygun olmayan birim ile ölçüm yapılamaz.

Not: Yukarıda belirtilen durum kodları içerisinde aynı anda birden fazla durum oluştuğu durumlarda

öncelikli olan durum kodları kırmızı renklendirilerek belirtilmiştir. Kırmızı renk ile belirtilen kodlar;

• 15\) Purge

• 23\) Yikama

• 24\) HaftalikYikama

• 31\) Debi Arızası

Bu durum kodlarının aynı anda gelmesi durumunda ise herhangi birisi seçilebilir.

**3.7 Diagnostik Servisleri**

**3.7.1 Diagnostik Tiplerini Dönen Servis**

Bu servis diagnostik(Çevrimiçi, Çevrimdışı vb.) istasyon durumlarının ve kodlarının döndürüldüğü

servistir.Döndürülen tip numaraları ile istasyonun durumları merkeze gönderilecektir.

**Servis Adı Servis Metot Tipi**

**Diagnostik Tiplerinin Dönen Servis POST**

**Servis URL**

http://entegrationsais.csb.gov.tr/SAIS/GetDiagnosticTypes

**Servis Request**

http://entegrationsais.csb.gov.tr/SAIS/GetDiagnosticTypes

**Servis Request Örneği**

http://entegrationsais.csb.gov.tr/SAIS/GetDiagnosticTypes

**Servis Body Raw**

(Yok)

**Servis Body Raw Örneği**

(Yok)

**Servis Response**

{

 "result": true,

 "message": null,

 "objects": \[

 {

 "DiagnosticTypeNo": 1,

 "DiagnosticTypeName": "Çevrimiçi",

 "DiagnosticLevel": 4,

 "DiagnosticLevel\_Title": "Başarılı"

 },

 {

 "DiagnosticTypeNo": 2,

 "DiagnosticTypeName": "Çevrimdışı",

 "DiagnosticLevel": 0,

 "DiagnosticLevel\_Title": "Alarm"

 },

 ………………………

 \]

}

**3.7.2 Diagnostik Gönderme Servisi**

Bu servis istasyonun durumu ve alarmları ile ilgili açıklama belirtilerek sistemde durumunun

kaydedilmesini sağlayan servistir.

**Servis Adı Servis Metot Tipi**

**İstasyon Diagnostik Gönderme Servisi POST**

**Servis URL**

http://entegrationsais.csb.gov.tr/SAIS/SendDiagnostic

**Servis Request**

http://entegrationsais.csb.gov.tr/SAIS/SendDiagnostic

**Servis Request Örneği**

http://entegrationsais.csb.gov.tr/SAIS/SendDiagnostic

**Servis Body Raw**

{

"stationId": "{İstasyon id’si}",

"details": "{Açıklama}",

"startDate": {Diagnostik Tarihi”}

}

**Servis Body Raw Örneği**

{

 "stationId": "b8cb8db6-9ee4-40a3-a6c6-7855b4da22da",

 "details": "Deneme amaçlı Kayıt Atılıyor",

 "startDate": "2020-02-10 12:15:00"

}

**Servis Response**

{

 "result": true,

 "message": "Diagnostik kaydetme işlemi başarılı.",

 "objects": null

}

**3.7.3 İstasyon Açılma Kapanma Tarihlerini Bildiren Servis**

Bu servis, istasyon açıldığında son kapanma tarihini ve açılma tarihini göndererek sistemde istasyonun

kapalı olduğu süreyi tutar.

**Servis Adı Servis Metot Tipi**

**İstasyon Açılma Kapanma Tarihlerini Bildiren Servis POST**

**Servis URL**

http://entegrationsais.csb.gov.tr/SAIS/SendPowerOffTime

**Servis Request**

http://entegrationsais.csb.gov.tr/SAIS/SendPowerOffTime

**Servis Request Örneği**

http://entegrationsais.csb.gov.tr/SAIS/SendPowerOffTime

**Servis Body Raw**

{

"stationId": "{İstasyon id’si}",

"startTime": "{İstasyon kapanma tarihi}",

"endTime": "{İstasyon açılma tarihi}",

}

**Servis Body Raw Örneği**

{

"stationId": "95CEA834-EAE4-4EEF-8917-B52B1423BD23",

"startTime":"2019-12-01T10:00:00",

"endTime":"2019-12-01T12:00:00"

}

**Servis Response**

{

 "result": true,

 "message": "SAİS kapanma ve açılma tarihleri kaydedildi.",

 "objects": null

}

**3.7.4 Tip Numarası ile Diagnostik Gönderme Servisi**

Bu servis “3.7.1 Diagnostik Tiplerini Dönen Servis” bölümünde döndürülen diagnostik bilgileri ile

istasyonun hangi durumda olduğunun kaydının tutulma işleminin gerçekleşeceği servistir. Ayırıcı

durum ile beraber açıklama da gönderilebilir.

**Servis Adı Servis Metot Tipi**

Diagnostik Tip Numarası ile Diagnostik Gönderme Servisi POST

**Servis URL**

http://entegrationsais.csb.gov.tr/SAIS/SendDiagnosticWithTypeNo

**Servis Request**

http://entegrationsais.csb.gov.tr/SAIS/SendDiagnosticWithTypeNo

**Servis Request Örneği**

http://entegrationsais.csb.gov.tr/SAIS/SendDiagnosticWithTypeNo

**Servis Body Raw**

{

"stationId": "{İstasyon id’si}",

"details": "{Açıklama}",

"DiagnosticTypeNo": "{Diagnostik Tip Numarası}",

"startDate": "{Diagnostik Oluşma Zamanı}"

}

**Servis Body Raw Örneği**

{

"stationId": "95CEA834-EAE4-4EEF-8917-B52B1423BD23",

"details":"Deneme amaçlı",

"DiagnosticTypeNo":"119"

"startDate":"2020-02-10 12:15:00"

}

**Servis Response**

{

 "result": true,

 "message": "Diagnostik kaydetme işlemi başarılı",

 "objects": null

}

**3.8 Kalibrasyon Servisleri**

**3.8.1 Kalibrasyon Kaydı Gönderme Servisi**

Bu serviste istasyonda bulunan cihazlara yapılan kalibrasyonların bilgilerinin (Referans değer, Ölçüm

değeri, Fark değeri) kaydedilme işlemi gerçekleştirilir.

Servis Adı Servis Metot Tipi

Kalibrasyon Kaydı Gönderme Servisi POST

**Servis URL**

http://entegrationsais.csb.gov.tr/SAIS/SendCalibration

**Servis Request**

http://entegrationsais.csb.gov.tr/SAIS/SendCalibration

**Servis Request Örneği**

http://entegrationsais.csb.gov.tr/SAIS/SendCalibration

**Servis Body Raw**

{

 "CalibrationDate": "{Kalibrasyon Tarihi}",

 "StationId": "{SAİS id}",

 "DBColumnName":"{Parametre Adı}",

 "ZeroRef" : "{Zero Ref Değeri}",

 "ZeroMeas" : "{Zero Meas Değeri}",

 "ZeroDiff" : "{Zero Diff Değeri}",

 "ZeroSTD" : "{Zero Standart Sapma Değeri}",

 "SpanRef" : "{Span Ref Değeri}",

 "SpanMeas" : "{Span Meas Değeri}”,

 "SpanDiff" : "{Span Diff Değeri}",

 "SpanSTD" : "{Span Standart Sapma Değeri}",

 "ResultFactor" : "{Factor Sonuç Değeri }",

 "ResultZero" : "{Zero Sonucu true veya false}",

 "ResultSpan" : "{Span Sonucu true veya false}"

}

**Servis Body Raw Örneği**

{

 "CalibrationDate": "2019-12-24T10:00:00",

 "StationId": "a8b6f187-63cb-4b14-87c6-3d915291a722",

 "DBColumnName":"KOi",

 "ZeroRef" : 0,

 "ZeroMeas" : \-0.48,

 "ZeroDiff" : \-0.48,

 "ZeroSTD" : 0,

 "SpanRef" : 144.76,

 "SpanMeas" : 148.59,

 "SpanDiff" : 2.65,

 "SpanSTD" : 0.53,

 "ResultFactor" : 0.97,

 "ResultZero" : true,

 "ResultSpan" : true

}

**Servis Response**

{

 "result": true,

 "message": "Kalibrasyon Kaydetme işlemi başarıyla tamamlandı.",

 "objects": null

}

**3.8.2 Merkeze Gönderilen Kalibrasyon Kayıtlarını Sorgulama Servisi**

Bu servis, tetiklenirken göndereceğiniz tarih aralığında istasyona yapılan kalibrasyonların listelendiği

servistir.

**Servis Adı Servis Metot Tipi**

**Kalibrasyon Kayıtlarını Sorgulama Servisi POST**

**Servis URL**

http://entegrationsais.csb.gov.tr/SAIS/GetCalibration

**Servis Request**

http://entegrationsais.csb.gov.tr/SAIS/ GetCalibration?stationId={SAİS id’si}\&startDate={ilk tarih}\&endDate={son tarih}

**Servis Request Örneği**

http://entegrationsais.csb.gov.tr/SAIS/GetCalibration?stationId=f8cb8db6-9ee4-40a3-a6c6-7855b4da22da\&startDate=2019-01-01\&endDate=2019-02-01

**Servis Body Raw**

(Yok)

**Servis Body Raw Örneği**

(Yok)

**Servis Response**

{

 "result": true,

 "message": null,

 "objects": \[

 {

 "StationId": "b8cb8db6-9ee4-40a3-a6c6-7855b4da22da",

 "DBColumnName": "KOi",

 "CalibrationDate": "2019-01-08T11:47:00",

 "ZeroRef": 7.0,

 "ZeroMeas": 7.22,

 "ZeroDiff": 7.22,

 "ZeroSTD": 0.0,

 "SpanRef": 7.0,

 "SpanMeas": 9.94,

 "SpanDiff": 41.99999,

 "SpanSTD": 0.0,

 "ResultFactor": 0.0,

 "ResultZero": false,

 "ResultSpan": false,

 "Result": false,

 "id": "ffca7b17-14d7-4263-88a0-fc67b1a734db",

 "created": "2020-08-26T10:32:01.183",

 "changed": null,

 "changedby": null,

 "createdby": null

 }

 \]

}

**3.9 Numune Servisleri**

Numune alımı tetikleme süreci sınır aşımı gerçekleştiğindeki senaryodan farklı işleyecektir. Sınır

aşımı gerçekleştiğinde sektöre göre farklılık gösteren 2 saat veya 24 saat süren numune alma süreci

olması gerektiği gibi çalışacaktır. Merkezden numune alımı için SAİS kabinine istek gönderirse

kabindeki alınacak anlık numune süresi 1 saat olacaktır. Anlık numune süreci 1 saat içerisinde

numune alımını tamamlamış olacaktır.

**3.9.1 SAİS Kabininde Numune Alımına Başlandı (Tetikleme/Görev Nedeniyle)**

Bu servis, tetikleme ile SAİS’den numune talep edildiğinde sıraya koyulan görevin işletilmesinin

ardından kullanılacaktır. Görev listesinde bekleyen numune alım işlemi devreye girdiğinde ve

kabinden numune alınmaya başlandığında bildirim yapılacaktır.

Tetikleme yapıldığı anda size bir Numune kodu verilmekte ve bu servis çağırılırken bu numune

kodunu kullanmanız gerekmektedir.

**Servis Adı Servis Metot Tipi**

**Numune Alımına Başlandı Servisi POST**

**Servis URL**

http://entegrationsais.csb.gov.tr/SAIS/SampleRequestStart

**Servis Request**

http://entegrationsais.csb.gov.tr/SAIS/SampleRequestStart

**Servis Request Örneği**

http://entegrationsais.csb.gov.tr/SAIS/SampleRequestStart

**Servis Body Raw**

{

 "StationId": "{İstasyon id’si}",

 "SampleCode": "{Numune Kodu}"

}

**Servis Body Raw Örneği**

{

 "StationId": "C3D62B3B-90EB-4FD0-8E87-905067297BB0",

 "SampleCode": "15482653"

}

**Servis Response**

{

 "result": true,

 "message": "Numune alım süreci başarıyla Merkeze bildirildi.",

 "objects": null

}

**3.9.2 SAİS Kabininde Numune Alımına Başlandı Bildirimi Servisi**

Bu servis, tesisin numune aldığında merkeze numune alımına başladığı bilgisini göndermek için

kullanılır. Tesisi numune almaya başladığında bu servisi çağırır.

Sonuç olarak objects parametresi içerisinde size numune için bir Numune Kodu (SampleCode) bilgisi

geri dönmektedir.

Daha sonrasında;

• Numune İşlemi tamamlandığında,

• Numune İşlemi gerçekleşirken Sorun Oluştuğunda

Bu servislere size bildirilen Numune Kodu ile gelmeniz beklenmektedir.

Bir numune durumu sadece 1 defa güncellenebilir. O da “Numune İşlemi Tamamlandı” veya

“Numune İşlemi gerçekleşirken Sorun Oluştu” olarak servisler aracılığıyla güncellenebilir.

**Servis Adı Servis Metot Tipi**

**Sınır Aşımı Nedeniyle Numune Alımına Başlandı Servisi POST**

**Servis URL**

http://entegrationsais.csb.gov.tr/SAIS/SampleRequestLimitOver

**Servis Request**

http://entegrationsais.csb.gov.tr/SAIS/SampleRequestLimitOver

**Servis Request Örneği**

http://entegrationsais.csb.gov.tr/SAIS/SampleRequestLimitOver

**Servis Body Raw**

{

 "StationId": "{İstasyon id’si}",

 "Parameter": "{Numune alınmasına sebep olan parametre bilgisi}"

}

**Servis Body Raw Örneği**

{

 "StationId": "C3D62B3B-90EB-4FD0-8E87-905067297BB0",

 "Parameter": "KOi"

}

**Servis Response**

{

 "result": true,

 "message": "Numune alımı merkeze başarıyla bildirildi.",

 "objects": "11629065"

}

**3.9.3 Numune Alım İşlemi Tamamlandı Bildirimi Servisi**

Bu servis, tesisi numune alımı işlemi başlayıp belirlenen saat sonunda başarılı bir şekilde numune alım

işlemi bittiğinde kullanılacaktır. Bu bildirimin ardından, Merkez ve İl Müdürlerine bildirimin

yapılacaktır.

**Servis Adı Servis Metot Tipi**

**Numune Alımı Yapıldı Servisi POST**

**Servis URL**

[http://entegrationsais.csb.gov.tr/SAIS/SampleRequestComplete](http://entegrationsais.csb.gov.tr/SAIS/SampleRequestComplete)

**Servis Request**

http://entegrationsais.csb.gov.tr/SAIS/SampleRequestComplete

**Servis Request Örneği**

http://entegrationsais.csb.gov.tr/SAIS/SampleRequestComplete

**Servis Body Raw**

{

 "StationId": "{İstasyon id’si}",

 "SampleCode": "{Numunenin alındığında size verilen kod bilgisi}"

}

**Servis Body Raw Örneği**

{

 "stationId": "C3D62B3B-90EB-4FD0-8E87-905067297BB0",

 "SampleCode": "11629065"

}

**Servis Response**

{

 "result": true,

 "message": "Numune süreci güncelleme işlemi başarıyla tamamlandı.",

 "objects": null

}

**3.9.4 Numune Alımı Yapılırken Bir Sorun Oluştu**

Bu servis, tesisi numune alımı yapıp bitirdiğinde numune alımının bittiği bilgisini merkeze bildirmesi

için kullanılır.

**Servis Adı Servis Metot Tipi**

**Numune Alımı Yapıldı Servisi POST**

**Servis URL**

http://entegrationsais.csb.gov.tr/SAIS/SampleRequestError

**Servis Request**

http://entegrationsais.csb.gov.tr/SAIS/SampleRequestError

**Servis Request Örneği**

http://entegrationsais.csb.gov.tr/SAIS/SampleRequestError

**Servis Body Raw**

{

 "StationId": "{İstasyon id’si}",

 "SampleCode": "{Numunenin alındığında size verilen kod bilgisi}"

}

**Servis Body Raw Örneği**

{

 "StationId": "C3D62B3B-90EB-4FD0-8E87-905067297BB0",

 "SampleCode": "11629065"

}

**Servis Response**

{

 "result": true,

 "message": "Numune süreci güncelleme işlemi başarıyla tamamlandı.",

 "objects": null

}

şeklinde kullanılacaktır.

PLC Bilgileri ve Adresler

PLC: S71200

Rack: 0

Slot: 1

Ip Adresi: 10.33.3.253

Veri Çekilecek Bloklar: DB41, DB42, DB43

**DB41**

İçerdiği tagların hepsi **Real** türündedir buna göre veri çekiminde işlem yapılmalıdır. Aşağıda Tag isimleri ve adresleri verilmiştir.

**Akm** → `36`  
**TesisDebi** → `0`  
**TesisGunlukDebi** → `12`  
**DesarjDebi** → `60`  
**HariciDebi** → `52`  
**HariciDebi2** → `56`  
**NumuneHiz** → `4`  
**NumuneDebi** → `8`  
**Ph** → `16`  
**Iletkenlik** → `20`  
**CozunmusOksijen** → `24`  
**NumuneSicaklik** → `28`  
**Koi** → `32`  
**KabinSicaklik** → `40`  
**KabinNem** → `44`  
**Pompa1Hz** → `140`  
**Pompa2Hz** → `144`  
**UpsCikisVolt** → `148`  
**UpsGirisVolt** → `152`  
**UpsKapasite** → `156`  
**UpsSicaklik** → `160`  
**UpsYuk** → `164`

**DB42**

İçerdiği tagların hepsi **bit** türündedir buna göre veri çekiminde işlem yapılmalıdır. Aşağıda Tag isimleri ve adresleri verilmiştir.

**Kabin\_Oto** → `0.0`  
**Kabin\_Bakim** → `0.1`  
**Kabin\_Kalibrasyon** → `0.2`  
**Kabin\_Duman** → `0.3`  
**Kabin\_SuBaskini** → `0.4`  
**Kabin\_KapiAcildi** → `0.5`  
**Kabin\_EnerjiYok** → `0.6`  
**Kabin\_AcilStopBasili** → `0.7`  
**Kabin\_HaftalikYikamada** → `1.0`  
**Kabin\_SaatlikYikamada** → `1.1`  
**Pompa1Termik** → `1.2`  
**Pompa2Termik** → `1.3`  
**Pompa3Termik** → `1.4`  
**TankDolu** → `1.5`  
**Pompa1Calisiyor** → `1.6`  
**Pompa2Calisiyor** → `1.7`  
**Pompa3Calisiyor** → `2.0`  
**AkmTetik** → `2.1`  
**KoiTetik** → `2.2`  
**PhTetik** → `2.3`  
**ManuelTetik** → `2.4`  
**SimNumuneTetik** → `2.5`

**DB43**

İçerdiği tagların adresleri ve türleri verilmiştir. Buna göre veri çekiminde işlem yapılmalıdır. Aşağıda Tag isimleri ve adresleri verilmiştir.

**SystemTime** → `0 -> DTL`  
**WeeklyWashDay** → `14 -> Byte`  
**WeeklyWashHour** → `15 -> Byte`  
**DailyWashHour** → `16 -> Byte`  
**Minute** → `17 -> Byte`  
**Second** → `18 -> Byte`

