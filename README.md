# Carrot Clicker

![Unity](https://img.shields.io/badge/Unity-6000.6.2f1-000000?style=for-the-badge&logo=unity&logoColor=white)
![Language](https://img.shields.io/github/languages/top/BayBaris/Carrot-Clicker?style=for-the-badge)
![Platform](https://img.shields.io/badge/Platform-Android-3DDC84?style=for-the-badge&logo=android&logoColor=white)
![Stars](https://img.shields.io/github/stars/BayBaris/Carrot-Clicker?style=for-the-badge)
![License](https://img.shields.io/github/license/BayBaris/Carrot-Clicker?style=for-the-badge)
![Last Commit](https://img.shields.io/github/last-commit/BayBaris/Carrot-Clicker?style=for-the-badge)

Havucuna dokun, tavşanlarını çalıştır, havuç imparatorluğunu büyüt. Unity ile geliştirilmiş, dokunmatik ekran odaklı bir idle/clicker oyunu.

---

## 🍞 Hakkında

Carrot Clicker, bir Udemy kursu kapsamında geliştirdiğim mobil clicker oyunu. Ekrandaki havuca dokundukça havuç toplanır, toplanan havuçlarla marketten yükseltmeler alınır ve oyun sen dokunmasan da havuç üretmeye devam eder.

**Özellikler**

- Havuca dokunarak havuç toplama, dokunma anında büyüyüp küçülen havuç animasyonu
- Dokunuşlarla dolan bir çılgınlık (frenzy) barı; bar dolunca süreli çarpan bonusu
- Dokunulan noktada beliren bonus partikülleri
- Market sistemi: seviye başına fiyatı artan yükseltmeler
- Havucun etrafında dönen tavşanlar (BunnyClicker) ve pasif üretim sağlayan BunnyClicker+
- İlerlemenin cihazda saklanması ve menüden sıfırlama
- Ana menü ve oyun sahnesi arasında geçiş

## Oynanış

| Öğe | Açıklama |
|---|---|
| Havuca dokun | Havuç sayısı artar, çılgınlık barı dolar |
| Çılgınlık barı | Dolunca 5 saniyelik çarpan bonusu başlar |
| BunnyClicker | Her seviye saniyede 0,1 havuç; seviye başına fiyat artışı ×1,07 |
| BunnyClicker+ | Her seviye saniyede 0,5 havuç; seviye başına fiyat artışı ×1,10 |

Oyun dokunmatik girişle çalışır; fare girişi kodda devre dışı bırakılmış, bu yüzden masaüstünde tıklamaya yanıt vermez.

## 🍞 Teknik Bilgiler

| | |
|---|---|
| Motor | Unity 6000.6.2f1 |
| Dil | C# |
| Platform | Android (min. SDK 25) |
| Giriş | Dokunmatik |
| Animasyon | LeanTween |
| Arayüz | TextMesh Pro, uGUI |

Mimari notları:

- **Event tabanlı iletişim:** Yöneticiler birbirini doğrudan çağırmak yerine statik `Action` event'leri ile haberleşir (`InputManager`, `Carrot`, `ShopManager`)
- **ScriptableObject:** Yükseltmeler (`UpgradeSO`) veri olarak tanımlanır; fiyat formülü `taban × katsayı^seviye`
- **Object pooling:** Bonus partikülleri `ObjectPool`, tavşanlar ise başlangıçta oluşturulan bir havuz ile yönetilir
- **Kayıt sistemi:** Havuç sayısı ve yükseltme seviyeleri `PlayerPrefs` ile saklanır

| Script | Görev |
|---|---|
| `CarrotManager` | Havuç sayısı, satın alma, çarpan, kayıt |
| `InputManager` | Dokunuş algılama ve raycast |
| `Carrot` | Havuç animasyonu, çılgınlık barı |
| `ShopManager` / `UpgradeButton` | Market ve yükseltme butonları |
| `AutoClickManager` | Dönen tavşanlar ve otomatik üretim |
| `UpgradeManager` | Pasif üretim hesabı |
| `BonusParticlesManager` | Bonus partikülleri ve pooling |

## Teşekkürler

Bu proje öğrenme amaçlı bir kurs çalışmasıdır. Kullanılan içerikler için teşekkürler:

- **LeanTween** — Russell Savage (Dented Pixel), MIT lisansı
- **Dark UI** ve **Design Toolbox** — arayüz sprite ve font paketleri
- Kursu hazırlayan eğitmen ve Udemy topluluğu

---

## 💃 Benimle İletişim

[![LinkedIn](https://img.shields.io/badge/LinkedIn-0A66C2?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/baybarisk)
[![GitHub](https://img.shields.io/badge/GitHub-181717?style=for-the-badge&logo=github&logoColor=white)](https://github.com/BayBaris)
[![Instagram](https://img.shields.io/badge/Instagram-E4405F?style=for-the-badge&logo=instagram&logoColor=white)](https://www.instagram.com/baybarisk/)
[![itch.io](https://img.shields.io/badge/itch.io-FA5C5C?style=for-the-badge&logo=itchdotio&logoColor=white)](https://baybaris.itch.io/)

Barış Karakaya · Bilgisayar Mühendisi · Game Designer · Gamification
