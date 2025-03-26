using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzy_lib
{
    public static class KategoriBergner
    {
        public static int BeregnTallKategori(int[] kast, int tall)
        {
            if(tall < 1 || tall > 6)
            {
                throw new ArgumentOutOfRangeException("Tallet må være mellom 1 og 6");
            }

            return kast.Where(x => x == tall).Sum();
        }

        public static int BeregnXParKategori(int[] kast, int antallPar)
        {
            if(antallPar > 3 || antallPar < 1)
            {
                throw new ArgumentOutOfRangeException("Antall par må være 1 eller 2.");
            }

            var muligePar = kast.GroupBy(x => x).Where(x => x.Count() >= 2).Select(x => x.Key);

            if (muligePar.Count() < antallPar)
            {
                return 0;
            }

            var hoyesteXPar = muligePar.OrderByDescending(x => x).Take(antallPar);
            return hoyesteXPar.Select(x => x * 2).Sum();
        }

        public static int BeregnXLikeKategori(int[] kast, int antallLike)
        {
            if(antallLike > 5 || antallLike < 3)
            {
                throw new ArgumentOutOfRangeException("Antall like må være mellom 3 og 5");
            }

            var resultatTall = kast.GroupBy(x => x).Where(x => x.Count() >= antallLike).Select(x => x.Key).FirstOrDefault(0);

            if (resultatTall == 0)
            {
                return 0;
            }

            //om antallLike er 5, så er det yatzy. Yatzy gir 50 poeng.
            return antallLike == 5 ? 50 : resultatTall * antallLike;


        }

        public static int BeregnFulltHusKategori(int[] kast)
        {
            var grupper = kast.GroupBy(x => x).Select(x => x.Count());
            if (grupper.Contains(3) && grupper.Contains(2))
            {
                return kast.Sum();
            }
            return 0;
        }

        public static int BeregnStraightKategori(int[] kast, Kategori typeStraight)
        {
            if (!typeStraight.Equals(Kategori.StorStraight) && !typeStraight.Equals(Kategori.LitenStraight) )
            {
                throw new ArgumentOutOfRangeException("typeStraight kan kun være kategori litenStraight eller kategori StorStraight");
            }

            var sortertKastStr = string.Join(",", kast.OrderBy(x => x));

            return sortertKastStr switch
            {
                "1,2,3,4,5" => typeStraight.Equals(Kategori.LitenStraight) ? 15 : 0,
                "2,3,4,5,6" => typeStraight.Equals(Kategori.StorStraight) ? 20 : 0,
                _ => 0
            };
        }

        public static int BeregnSjanseKategori(int[] kast)
        {
            return kast.Sum();
        }

        public static int BeregnYatzyKategori(int[] kast)
        {
            return BeregnXLikeKategori(kast, 5);
        }
    }
}
