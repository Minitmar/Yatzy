namespace Yatzy_lib
{
    public class Resultat
    {
        public int Poeng { get; set; }
        public Kategori Kategori { get; set; }

        public Resultat(int poeng, Kategori kategori)
        {
            Poeng = poeng;
            Kategori = kategori;
        }
    }
}