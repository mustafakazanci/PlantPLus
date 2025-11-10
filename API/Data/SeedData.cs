using Microsoft.EntityFrameworkCore;
using PlantDiseaseApi.Models;

namespace PlantDiseaseApi.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Diseases.Any()) return;

                var diseaseData = new Dictionary<string, (string Name, string Symptoms, string Solution, string Cause, string Prevention)>
                {
                    // --- BİBER ---
                    ["biber_bakteriyel_leke"] =
                    (
                        "Biber - Bakteriyel Leke",
                        "Yaprakta sulu, koyu lekeler; ilerleyen aşamada gövde ve meyvede yaralar.",
                        "Bakır hidroksit veya bakır sülfat içeren bakterisitler kullanılmalıdır. Hastalıklı fideler hemen imha edilmelidir.",
                        "Xanthomonas campestris pv. vesicatoria adlı bakteriden kaynaklanır. Yüksek nem, yağmurlama sulama ve sıcak hava (24-30°C) yayılımını hızlandırır. Bakteri, enfekte tohumlar ve bitki artıklarıyla taşınır.",
                        "Sertifikalı ve hastalıksız tohum kullanın. Yaprakları ıslatan üstten sulama yöntemlerinden kaçının (damla sulama tercih edin). Bitki rotasyonu uygulayın ve aletleri dezenfekte edin."
                    ),
                    ["biber_saglikli"] =
                    (
                        "Biber - Sağlıklı",
                        "Yapraklar canlı ve koyu yeşil renkte, lekesiz ve deformasyonsuzdur. Bitki genel olarak dinç bir görünüme sahiptir.",
                        "Mevcut bakım rutininize (dengeli sulama, gübreleme) devam edin. Bitkiyi düzenli olarak gözlemleyerek olası sorunları erken teşhis edin.",
                        "Bitkinin ihtiyaç duyduğu su, besin ve güneş ışığının dengeli bir şekilde sağlanmasından kaynaklanır.",
                        "Aşırı veya yetersiz sulamadan kaçının. Toprağın besin açısından zengin olduğundan emin olun ve aşırı gübrelemeden sakının."
                    ),

                    // --- PATATES ---
                    ["patates_erken_yaniklik"] =
                    (
                        "Patates - Erken Yanıklık",
                        "Genellikle yaşlı alt yapraklarda başlayan, iç içe geçmiş halkalar şeklinde (hedef tahtası gibi) koyu kahverengi lekeler.",
                        "Mancozeb veya Chlorothalonil içeren koruyucu fungisitler düzenli olarak uygulanmalıdır. Sulama ve gübrelemeye dikkat ederek bitki stresini azaltın.",
                        "Alternaria solani adlı mantardan kaynaklanır. Özellikle bitki stres altındayken (besin eksikliği, kuraklık) veya yaşlandığında ortaya çıkar. Nemli havalarda ve yaprak ıslaklığında yayılır.",
                        "Hastalıklı bitki artıklarını sezon sonunda tarladan temizleyin. En az 2 yıllık bitki rotasyonu uygulayın (aynı yere domates, patates, biber ekmeyin). Bitkileri aşırı sık dikmeyin."
                    ),
                    ["patates_gec_yaniklik"] =
                    (
                        "Patates - Geç Yanıklık",
                        "Yaprak kenarlarında ve uçlarında düzensiz, sulu ve soluk yeşil lekeler. Nemli havalarda yaprak altında beyaz bir küf tabakası oluşur. Yumrularda kuru, kahverengi çürüklükler görülür.",
                        "Mancozeb gibi koruyucu fungisitler, hastalık görülmeden önce veya ilk belirtilerde uygulanmalıdır. Hastalıklı bitkiler ve yumrular imha edilmelidir.",
                        "Phytophthora infestans adlı mantar benzeri bir organizmadan kaynaklanır. Serin (15-20°C) ve aşırı nemli hava koşullarında çok hızlı yayılır ve salgın yapabilir.",
                        "Sertifikalı ve hastalıksız tohumluk yumrular kullanın. Yaprakları ıslatan sulama yöntemlerinden kaçının. Bitkiler arasında iyi bir hava sirkülasyonu sağlayın ve hastalıklı yumruları kesinlikle depolamayın."
                    ),
                    ["patates_saglikli"] =
                    (
                        "Patates - Sağlıklı",
                        "Yapraklar lekesiz ve sağlıklı yeşil renktedir. Bitki büyümesi normaldir.",
                        "Rutin bakıma devam edin. Toprak nemini kontrol altında tutun.",
                        "Doğru tarım uygulamaları ve sağlıklı tohumluk kullanımı.",
                        "Aşırı sulamadan ve tarlada su birikmesinden kaçının."
                    ),

                    // --- DOMATES ---
                    ["domates_bakteriyel_leke"] =
                    (
                        "Domates - Bakteriyel Leke",
                        "Yapraklarda, meyvelerde ve gövdede küçük, koyu renkli, hafifçe kabarık ve sulu görünümlü lekeler.",
                        "Bakır içerikli bakterisitler koruyucu olarak etkilidir. Hastalık ilerlemişse kontrolü çok zordur. Hastalıklı bitkiler sökülmelidir.",
                        "Xanthomonas türü bakterilerden kaynaklanır. Sıcak ve nemli koşullarda, özellikle yağmur veya sulama suyunun sıçramasıyla yayılır.",
                        "En az 3 yıllık bitki rotasyonu uygulayın. Damla sulama kullanın. Aletleri ve serayı dezenfekte edin. Tarlada çalışırken bitkiler ıslak olmamalıdır."
                    ),
                    ["domates_erken_yaniklik"] =
                    (
                        "Domates - Erken Yanıklık",
                        "Yaşlı yapraklarda hedef tahtası şeklinde, iç içe geçmiş halkaları olan koyu kahverengi lekeler. Lekelerin etrafında genellikle sarı bir hale bulunur.",
                        "Chlorothalonil veya mancozeb içeren fungisitler kullanılabilir. Hastalıklı alt yapraklar düzenli olarak budanmalı ve imha edilmelidir.",
                        "Alternaria solani adlı mantardan kaynaklanır. Topraktan bitkiye sıçrayan su damlacıklarıyla veya hastalıklı bitki artıklarıyla yayılır.",
                        "Toprağa malçlama yaparak suyun sıçramasını engelleyin. Bitki rotasyonu uygulayın. Bitkileri stresten (kuraklık, besin eksikliği) koruyun."
                    ),
                    ["domates_gec_yaniklik"] =
                    (
                        "Domates - Geç Yanıklık",
                        "Yapraklarda büyük, düzensiz, sulu ve gri-yeşil lekeler. Yaprak altında beyaz küf oluşumu tipiktir. Meyvelerde büyük, sert ve kahverengi lekeler görülür.",
                        "Koruyucu fungisitler (mancozeb, chlorothalonil) hastalık başlamadan önce uygulanmalıdır. Hastalıklı bitkiler derhal yok edilmelidir.",
                        "Phytophthora infestans'tan kaynaklanır. Serin ve aşırı nemli havalarda çok hızlı yayılır ve tüm ürünü birkaç gün içinde yok edebilir.",
                        "Bitkiler arasında hava akışını sağlamak için yeterli boşluk bırakın. Sabah erken saatlerde ve topraktan sulama yapın. Patates tarlalarına yakın ekim yapmaktan kaçının."
                    ),
                    ["domates_yaprak_kufu"] =
                    (
                        "Domates - Yaprak Küfü",
                        "Yaprakların üst yüzeyinde soluk yeşil veya sarı lekeler, alt yüzeyinde ise bu lekelere denk gelen yerlerde zeytin yeşili-kahverengi kadifemsi bir küf tabakası.",
                        "Seradaki havalandırmayı artırarak nemi düşürün. Kükürt veya bakır içerikli fungisitler kullanılabilir.",
                        "Passalora fulva (Fulvia fulva) adlı mantardan kaynaklanır. Özellikle yüksek nem (%85 üzeri) ve yetersiz havalandırmaya sahip seralarda yaygındır.",
                        "Sera nemini %85'in altında tutmaya çalışın. Gece ve gündüz sıcaklık farkını azaltın. Dayanıklı çeşitler ekin ve bitkileri birbirine çok yakın dikmeyin."
                    ),
                    ["domates_septoria_yaprak_lekesi"] =
                    (
                        "Domates - Septoria Yaprak Lekesi",
                        "Genellikle alt yapraklarda başlayan, ortası gri veya ten rengi, kenarları koyu kahverengi olan küçük, dairesel lekeler. Lekelerin ortasında küçük siyah noktacıklar (piknitler) görülür.",
                        "Chlorothalonil veya mancozeb içeren fungisitler etkilidir. Hastalıklı alt yapraklar hemen toplanıp imha edilmelidir.",
                        "Septoria lycopersici adlı mantardan kaynaklanır. Ilıman ve nemli havalarda, özellikle sulama suyunun sıçramasıyla yayılır.",
                        "Bitki rotasyonu yapın. Toprağa malçlama yapın. Bitkiler arasında hava akışını sağlayacak şekilde aralık bırakın ve alttan sulama yapın."
                    ),
                    ["domates_orumcek_akari_iki_noktali"] =
                    (
                        "Domates - Örümcek Akarı (İki Noktalı)",
                        "Yaprakların üst yüzeyinde minik sarı veya beyaz noktacıklar. Yaprak altında ince ipeksi ağlar. İleri aşamada yapraklar sararır, bronzlaşır ve kurur.",
                        "Abamectin gibi spesifik akarisitler (akar öldürücüler) veya kükürtlü preparatlar kullanılabilir. Biyolojik mücadele için yırtıcı akarlar (Phytoseiulus persimilis) salınabilir.",
                        "Tetranychus urticae adlı akar türünden kaynaklanır. Sıcak, kuru ve havalandırması zayıf ortamları severler. Çok hızlı çoğalırlar.",
                        "Seradaki nemi yüksek tutun. Bitkileri düzenli olarak tazyikli suyla yıkayarak akarları uzaklaştırın. Yabancı ot kontrolü yapın."
                    ),
                    ["domates_hedef_leke"] =
                    (
                        "Domates - Hedef Leke",
                        "Yapraklarda, gövdede ve meyvelerde iç içe geçmiş halkalar şeklinde (hedef tahtası) koyu renkli lekeler.",
                        "Koruyucu fungisitler (chlorothalonil, mancozeb) kullanılabilir.",
                        "Corynespora cassiicola adlı mantardan kaynaklanır. Sıcak ve nemli koşullarda ortaya çıkar.",
                        "Sera nemini düşürün ve iyi havalandırma sağlayın. Bitki rotasyonu uygulayın."
                    ),


                    ["domates_sari_yaprak_bukulme_virusu"] =
                    (
                        "Domates - Sarı Yaprak Kıvırcıklık Virüsü (TYLCV)",
                        "Genç yapraklarda şiddetli sararma, küçülme ve yukarı doğru kıvrılma. Bitkide bodurlaşma ve çiçek dökülmesi görülür.",
                        "Tedavisi yoktur. Tek mücadele yöntemi, virüsü taşıyan beyaz sinekleri kontrol etmektir. Hastalıklı bitkiler derhal köklenerek poşete konulmalı ve imha edilmelidir.",
                        "Begomovirus ailesinden bir virüsten kaynaklanır ve tütün beyaz sineği (Bemisia tabaci) ile taşınır.",
                        "Seranın giriş ve havalandırma açıklıklarına böcek tülü kullanın. Beyaz sinekleri izlemek ve yakalamak için sarı yapışkan tuzaklar asın. Dayanıklı çeşitler tercih edin."
                    ),
                    ["domates_mozaik_virusu"] =
                    (
                        "Domates - Mozaik Virüsü (ToMV)",
                        "Yapraklarda açık ve koyu yeşil alanların oluşturduğu mozaik şeklinde desenler. Yapraklarda deformasyon, küçülme ve bitkide genel bir bodurluk görülür.",
                        "Kimyasal bir tedavisi yoktur. Hastalıklı bitkiler hemen sökülüp imha edilmelidir. ",
                        "Tomato mosaic virus'ten (ToMV) kaynaklanır. Çok bulaşıcıdır ve mekanik olarak (eller, aletler, giysiler) veya enfekte tohumlarla yayılır.",
                        "Tarlada veya serada çalışırken sigara içmeyin (tütün bitkisi de bu virüsü taşır). Sık sık ellerinizi ve aletlerinizi dezenfekte edin. Sertifikalı, virüsten arındırılmış tohum kullanın."
                    ),
                    ["domates_saglikli"] =
                    (
                        "Domates - Sağlıklı",
                        "Yapraklar lekesiz ve sağlıklı yeşil renktedir. Bitki büyümesi ve meyve gelişimi normaldir.",
                        "Rutin bakıma devam edin. Bitkiyi düzenli olarak kontrol ederek olası sorunları erken fark edin.",
                        "Doğru sulama, gübreleme, budama ve hastalık/zararlı kontrolü gibi iyi tarım uygulamalarından kaynaklanır.",
                        "Bitkileri strese sokacak aşırı sıcak, soğuk, kuraklık veya aşırı sulama gibi koşullardan kaçının."
                    )
                }; 

                var solutionsCache = new Dictionary<string, Solution>();
                var diseasesCache = new Dictionary<string, Disease>();

                foreach (var (label, (name, symptoms, solution, cause, prevention)) in diseaseData)
                {
                    if (!solutionsCache.ContainsKey(solution))
                    {
                        var newSolution = new Solution { Title = $"{name} için Çözüm Önerileri", Description = solution };
                        solutionsCache.Add(solution, newSolution);
                        context.Solutions.Add(newSolution);
                    }
                    var currentSolution = solutionsCache[solution];

                    if (!diseasesCache.ContainsKey(name))
                    {
                        var newDisease = new Disease
                        {
                            PredictionLabel = label.ToLower(),
                            Name = name,
                            Description = $"{name}, bitkinin verimini ve sağlığını doğrudan etkileyen bir durumdur.",
                            Symptoms = symptoms,
                            Cause = cause,
                            Prevention = prevention
                        };
                        diseasesCache.Add(name, newDisease);
                        context.Diseases.Add(newDisease);
                    }
                    var currentDisease = diseasesCache[name];

                    if (currentDisease.DiseaseSolutions == null || !currentDisease.DiseaseSolutions.Any(ds => ds.SolutionId == currentSolution.Id))
                    {
                        context.DiseaseSolutions.Add(new DiseaseSolution { Disease = currentDisease, Solution = currentSolution });
                    }
                }

                if (!context.Tips.Any())
                {
                    var tips = new[]
                    {
                        // --- Domates İpuçları ---
                        new Tip { PlantName = "Domates", Content = "Domates biraz nazlıdır ama bizle kolay! Ona bol bol güneş verin, o da size lezzet versin." },
                        new Tip { PlantName = "Domates", Content = "Sabahları sulanan domatesin keyfi yerine gelir. Yaprakları değil, toprağı sulamayı unutma!" },
                        new Tip { PlantName = "Domates", Content = "Domatesler desteklenmeyi sever. Onlara bir sırık veya kafes vererek 'omuz verin', boyları uzasın." },
                        new Tip { PlantName = "Domates", Content = "Fazla yaprakları budamaktan çekinme. Bu, kuaföre gitmek gibidir, hem hava alır hem meyveleri güzelleşir." },

                        // --- Biber İpuçları ---
                        new Tip { PlantName = "Biber", Content = "Biberler sıcak kanlıdır! Soğuk havaları pek sevmezler, onları ılık tutmaya çalışın." },
                        new Tip { PlantName = "Biber", Content = "Acı biber mi yetiştiriyorsun? Unutma, biraz su stresi onları daha da 'sinirli' ve acı yapabilir!" },
                        new Tip { PlantName = "Biber", Content = "Biberlerin toprağı nemli kalsın ama göle dönmesin. Ayaklarının suda kalmasından hiç hoşlanmazlar." },

                        // --- Patates İpuçları ---
                        new Tip { PlantName = "Patates", Content = "Patatesler sürprizleri sever! Onları toprağın altına gizleyin ve hasat zamanı hazine avına çıkın." },
                        new Tip { PlantName = "Patates", Content = "Bitki boyu uzadıkça diplerine toprak yığ. Ne kadar çok toprak, o kadar çok patates demektir!" },
                        new Tip { PlantName = "Patates", Content = "Patates çiçeklerini koparmak, bitkinin enerjisini yumrulara vermesine yardımcı olur. Bırakın çiçeği, bize patatesi lazım!" }
                    };
                    context.Tips.AddRange(tips);

                }

                context.SaveChanges();
            }
        }
    }
}