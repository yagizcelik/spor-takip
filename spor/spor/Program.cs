using System;
using System.Collections.Generic;
using System.Text;

namespace SporTakipUygulamasi
{
    public class Kullanici
    {
        public int KullaniciId { get; set; }
        public string KullaniciAdi { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Sifre { get; set; }

        public Kullanici(int id, string kullaniciAdi, string ad, string soyad, string sifre)
        {
            KullaniciId = id;
            KullaniciAdi = kullaniciAdi;
            Ad = ad;
            Soyad = soyad;
            Sifre = sifre;
        }

        public override string ToString()
        {
            return $"ID: {KullaniciId}, Kullanıcı: {KullaniciAdi}, Ad Soyad: {Ad} {Soyad}";
        }
    }

    public class Aktivite
    {
        public int AktiviteId { get; set; }
        public string AktiviteTuru { get; set; }
        public DateTime Tarih { get; set; }
        public double Sure { get; set; }
        public double Mesafe { get; set; }

        public Aktivite(int id, string turu, DateTime tarih, double sure, double mesafe)
        {
            AktiviteId = id;
            AktiviteTuru = turu;
            Tarih = tarih;
            Sure = sure;
            Mesafe = mesafe;
        }

        public override string ToString()
        {
            return $"Aktivite ID: {AktiviteId}\nTuru: {AktiviteTuru}\nTarih: {Tarih}\nSüre: {Sure} dk\nMesafe: {Mesafe} km";
        }
    }

    public class SporTakipSistemi
    {
        public List<Aktivite> AktiviteListesi { get; set; }
        public List<Kullanici> KullaniciListesi { get; set; }
        private int nextAktiviteId = 1;

        public SporTakipSistemi()
        {
            AktiviteListesi = new List<Aktivite>();
            KullaniciListesi = new List<Kullanici>();
        }
        public void AktiviteEkle(int kullaniciId)
        {
            Console.Write("Aktivite Türü (Örnek: Koşu, Bisiklet, Yüzme): ");
            string turu = Console.ReadLine();
            Console.Write("Aktivite süresini dakika cinsinden giriniz: ");
            if (!double.TryParse(Console.ReadLine(), out double sure))
            {
                Console.WriteLine("Geçersiz süre!");
                return;
            }
            Console.Write("Aktivite mesafesini (km) giriniz: ");
            if (!double.TryParse(Console.ReadLine(), out double mesafe))
            {
                Console.WriteLine("Geçersiz mesafe!");
                return;
            }
            DateTime tarih = DateTime.Now;
            Aktivite aktivite = new Aktivite(nextAktiviteId++, turu, tarih, sure, mesafe);
            AktiviteListesi.Add(aktivite);
            Console.WriteLine("\nAktivite başarıyla eklendi:");
            Console.WriteLine(aktivite);
        }

        public void AktiviteAra()
        {
            Console.Write("Aradığınız aktivite türünü giriniz: ");
            string kelime = Console.ReadLine();
            bool bulundu = false;
            foreach (var a in AktiviteListesi)
            {
                if (a.AktiviteTuru.IndexOf(kelime, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    Console.WriteLine(a);
                    Console.WriteLine(new string('-', 30));
                    bulundu = true;
                }
            }
            if (!bulundu)
            {
                Console.WriteLine("Aradığınız türde aktivite bulunamadı.");
            }
        }

        public void AktiviteleriListele()
        {
            if (AktiviteListesi.Count == 0)
            {
                Console.WriteLine("Henüz eklenmiş aktivite bulunmamaktadır.");
                return;
            }
            foreach (var a in AktiviteListesi)
            {
                Console.WriteLine(a);
                Console.WriteLine(new string('-', 30));
            }
        }

        public void AktiviteIstatistikleri()
        {
            if (AktiviteListesi.Count == 0)
            {
                Console.WriteLine("Henüz aktivite eklenmemiş.");
                return;
            }
            double toplamSure = 0, toplamMesafe = 0;
            foreach (var a in AktiviteListesi)
            {
                toplamSure += a.Sure;
                toplamMesafe += a.Mesafe;
            }
            Console.WriteLine($"Toplam Aktivite Sayısı: {AktiviteListesi.Count}");
            Console.WriteLine($"Toplam Süre: {toplamSure} dakika");
            Console.WriteLine($"Toplam Mesafe: {toplamMesafe} km");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            PerformLogin();
            SporTakipSistemi sistem = new SporTakipSistemi();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n--- Spor Takip Uygulaması ---");
                Console.WriteLine("1. Aktivite Ekle");
                Console.WriteLine("2. Aktivite Ara");
                Console.WriteLine("3. Aktiviteleri Listele");
                Console.WriteLine("4. Aktivite İstatistikleri");
                Console.WriteLine("5. Çıkış");
                Console.Write("Seçiminiz: ");
                string secim = Console.ReadLine();
                Console.WriteLine();

                switch (secim)
                {
                    case "1":
                        sistem.AktiviteEkle(1);
                        break;
                    case "2":
                        sistem.AktiviteAra();
                        break;
                    case "3":
                        sistem.AktiviteleriListele();
                        break;
                    case "4":
                        sistem.AktiviteIstatistikleri();
                        break;
                    case "5":
                        Console.WriteLine("Sistemden çıkılıyor...");
                        return;
                    default:
                        Console.WriteLine("Geçersiz seçim, lütfen tekrar deneyiniz.");
                        break;
                }
                Console.WriteLine("\nDevam etmek için bir tuşa basınız...");
                Console.ReadKey();
            }
        }
        static void PerformLogin()
        {
            int maxAttempts = 3;
            int attempt = 0;
            bool isAuthenticated = false;
            while (attempt < maxAttempts && !isAuthenticated)
            {
                Console.Clear();
                Console.Write("Kullanıcı Girişi: ");
                string username = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(username))
                {
                    Console.WriteLine("Kullanıcı giriş adı boş olamaz.");
                    attempt++;
                    Console.WriteLine("Devam etmek için bir tuşa basınız...");
                    Console.ReadKey();
                    continue;
                }
                Console.Write("Şifre: ");
                string password = ReadPassword();
                if (string.IsNullOrEmpty(password))
                {
                    Console.WriteLine("Şifre boş olamaz.");
                    attempt++;
                    Console.WriteLine("Devam etmek için bir tuşa basınız...");
                    Console.ReadKey();
                    continue;
                }
                if (username.Equals("yagiz", StringComparison.OrdinalIgnoreCase) && password == "yazoo")
                {
                    isAuthenticated = true;
                    Console.WriteLine("\nGiriş başarılı ana menüye yönlendiriliyorsunuz...");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("\nGiriş bilgileri hatalı tekrar deneyiniz.");
                    attempt++;
                    if (attempt < maxAttempts)
                    {
                        Console.WriteLine($"Kalan deneme hakkınız: {maxAttempts - attempt}");
                    }
                    Console.WriteLine("Devam etmek için bir tuşa basınız...");
                    Console.ReadKey();
                }
            }
            if (!isAuthenticated)
            {
                Console.WriteLine("Çok fazla hatalı giriş denemesi program sonlandırılıyor.");
                Environment.Exit(0);
            }
        }
        static string ReadPassword()
        {
            StringBuilder password = new StringBuilder();
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    Console.Write("\b \b");
                    password.Remove(password.Length - 1, 1);
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    Console.Write("*");
                    password.Append(key.KeyChar);
                }
            } while (key.Key != ConsoleKey.Enter);
            Console.WriteLine();
            return password.ToString();
        }
    }
}