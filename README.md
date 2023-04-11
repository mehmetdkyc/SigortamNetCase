# SigortamNetCase
Case çalışmasında istenilen API'da ilgili güncelleme silme ekleme tüm kullanıcıları getirme ve id'ye göre getirme bulunmaktadır.
Ekleme ve güncelleme işlemlerinde ilk olarak gönderilen veriler eğer mersis apisinden true olarak gelir ise işleme devam etmektedir aksi taktirde badrequest hatası dönmektedir.
Diğer işlemler için model bazında validasyon mevcuttur.
UI Tarafında da cshtmlin vermiş olduğu bazı required validasyonu kullanıldı. Onun dışında customervalidator olan dinamik bir validasyonda da ilgili kontrol sağlanıyor.
Swagger olarak kullanımı mevcut.
Api için .Net Coreun vermiş olduğu ILogger sınıfını kullandım. Nlog ta kullanabilirdim ama diğerini tercih ettim.
UI Tarafında çok tasarıma dikkat etmedim.

