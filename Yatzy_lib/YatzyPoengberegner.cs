using static Yatzy_lib.Kategori;
using static Yatzy_lib.KategoriBergner;

namespace Yatzy_lib
{
    public class YatzyPoengberegner
    {

        public int BeregnPoeng(string kast, Kategori kategori)
        {
            int[] terninger = PrepareThrow(kast);

            return kategori switch
            {
                Enere => BeregnTallKategori(terninger, 1),
                Toere => BeregnTallKategori(terninger, 2),
                Treere => BeregnTallKategori(terninger, 3),
                Firere => BeregnTallKategori(terninger, 4),
                Femmere => BeregnTallKategori(terninger, 5),
                Seksere => BeregnTallKategori(terninger, 6),
                EttPar => BeregnXParKategori(terninger, 1),
                ToPar => BeregnXParKategori(terninger, 2),
                TreLike => BeregnXLikeKategori(terninger, 3),
                FireLike => BeregnXLikeKategori(terninger, 4),
                LitenStraight => BeregnStraightKategori(terninger, LitenStraight),
                StorStraight => BeregnStraightKategori(terninger, StorStraight),
                FulltHus => BeregnFulltHusKategori(terninger),
                Sjanse => BeregnSjanseKategori(terninger),
                Yatzy => BeregnYatzyKategori(terninger),
                _ => throw new ArgumentOutOfRangeException("Valgt kategori finnes ikke."),
            };

        }

        public Resultat Max(string kast)
        {

            Kategori[] AlleKategorier =
        [
            Enere,
            Toere,
            Treere,
            Firere,
            Femmere,
            Seksere,
            EttPar,
            ToPar,
            TreLike,
            FireLike,
            LitenStraight,
            StorStraight,
            FulltHus,
            Sjanse,
            Yatzy
        ];

            return AlleKategorier.Select(x => new Resultat(BeregnPoeng(kast, x), x)).OrderByDescending(x => x.Poeng).First();
        }

        public static int[] PrepareThrow(string kast)
        {
            if (string.IsNullOrEmpty(kast))
            {
                throw new ArgumentException("Kast kan ikke være null eller tom streng");
            }
            if (!kast.Contains(','))
            {
                throw new ArgumentException("Kast er feil formatert.");
            }
            if (kast.Split(',').Length != 5)
            {
                throw new ArgumentException("Kast må inneholde 5 terninger.");
            }

            int[] kastArray = kast.Split(',').Select(int.Parse).ToArray();

            if (kastArray.Any(x => x < 1 || x > 6))
            {
                throw new ArgumentException("Kast må være mellom 1 og 6.");
            }

            return kastArray;
        }

    }
}
