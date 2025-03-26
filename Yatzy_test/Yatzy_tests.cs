using System.ComponentModel;
using Yatzy_lib;

namespace Yatzy_test
{
    [TestClass]
    public sealed class Yatzy_tests
    {
        [TestMethod]
        [DataRow("1,1,1", DisplayName = "Too few numbers")]
        [DataRow("1,1,1,1,1,1", DisplayName = "Too many numbers")]
        [DataRow("", DisplayName = "Empty throw")]
        [DataRow("1,1,1,1,7", DisplayName = "Invalid number - too high")]
        [DataRow("1,1,1,1,0", DisplayName = "Invalid number - too low")]
        [DataRow(null, DisplayName = "Null throw")]
        [DataRow("1 1 1 1 1", DisplayName = "Invalid format - no commas")]
        [DataRow("1,1 2,1,1", DisplayName = "Invalid format - one missing comma")]
        public void Test_PreparingDiceArray_InvalidCases(string kast)
        {
            Assert.ThrowsException<ArgumentException>(() => YatzyPoengberegner.PrepareThrow(kast));
        }

        [TestMethod]
        [DataRow("1,1,1,1,1", new [] { 1,1,1,1,1 }, DisplayName = "Only ones")]
        [DataRow("1,2,3,4,5", new[] { 1, 2, 3, 4, 5 }, DisplayName = "1 to 5")]
        [DataRow("6,6,6,6,6", new[] { 6, 6, 6, 6, 6 }, DisplayName = "Only sixes")]
        [DataRow("6,4,2,4,6", new[] { 6, 4, 2, 4, 6 }, DisplayName = "6 4 2 4 6")]
        public void Test_PreparingDiceArray_ValidCases(string kast, int[] expectedArray)
        {
            var acutal = YatzyPoengberegner.PrepareThrow(kast);

            CollectionAssert.AreEqual(expectedArray, acutal, "kast:'<{0}>' expectedArray:<{1}>", [kast, expectedArray]);
        }

        [TestMethod]
        [DataRow("1,1,1,1,1", 50, DisplayName = "Yatzy with ones gives 50 points")]
        [DataRow("5,5,5,5,5", 50, DisplayName = "Yatzy with fives gives 50 points")]
        [DataRow("1,1,1,1,2", 0, DisplayName = "One away gives 0 points") ]
        [DataRow("1,2,3,4,5", 0, DisplayName = "No repeates gives 0 points")]
        public void Test_PointCalculation_Yatzy(string kast, int expectedPoints)
        {
            var yatzyPoengberegner = new YatzyPoengberegner();
            var actual = yatzyPoengberegner.BeregnPoeng(kast, Kategori.Yatzy);
            // Assert
            Assert.AreEqual(expectedPoints, actual, "kast:'<{0}>' expectedPoints:<{1}>", [kast, expectedPoints]);
        }

        

        [TestMethod]
        [DataRow("1,1,1,1,1", 5, Kategori.Enere, DisplayName = "Five ones gives 5 points")]
        [DataRow("2,2,2,2,2", 10, Kategori.Toere, DisplayName = "Five twos gives 10 points")]
        [DataRow("3,3,3,3,3", 15, Kategori.Treere, DisplayName = "Five threes gives 15 points")]
        [DataRow("4,4,4,4,4", 20, Kategori.Firere, DisplayName = "Five fours gives 20 points")]
        [DataRow("5,5,5,5,5", 25, Kategori.Femmere, DisplayName = "Five fives gives 25 points")]
        [DataRow("6,6,6,6,6", 30, Kategori.Seksere, DisplayName = "Five sixes gives 30 points")]
        [DataRow("1,2,3,4,5", 1, Kategori.Enere, DisplayName = "1 one gives 1 point")]
        [DataRow("1,2,3,4,5", 2, Kategori.Toere, DisplayName = "1 two gives 2 points")]
        [DataRow("1,2,3,4,5", 3, Kategori.Treere, DisplayName = "1 three gives 3 points")]
        [DataRow("1,2,3,4,5", 4, Kategori.Firere, DisplayName = "1 four gives 4 points")]
        [DataRow("1,2,3,4,5", 5, Kategori.Femmere, DisplayName = "1 five gives 5 points")]
        [DataRow("1,2,3,4,6", 6, Kategori.Seksere, DisplayName = "1 six gives 6 points")]
        [DataRow("2,2,3,4,5", 0, Kategori.Enere, DisplayName = "0 ones gives 0 points")]
        [DataRow("1,1,3,4,5", 0, Kategori.Toere, DisplayName = "0 twos gives 0 points")]
        [DataRow("2,2,4,4,5", 0, Kategori.Treere, DisplayName = "0 threes gives 0 points")]
        [DataRow("2,2,3,3,5", 0, Kategori.Firere, DisplayName = "0 fours gives 0 points")]
        [DataRow("2,2,3,4,4", 0, Kategori.Femmere, DisplayName = "0 fives gives 0 points")]
        [DataRow("2,2,3,4,5", 0, Kategori.Seksere, DisplayName = "0 sixes gives 0 points")]
        public void Test_PointCalculation_Tall(string kast, int expectedPoints, Kategori TallKategori)
        {
            var yatzyPoengberegner = new YatzyPoengberegner();
            var actual = yatzyPoengberegner.BeregnPoeng(kast, TallKategori);
            // Assert
            Assert.AreEqual(expectedPoints, actual, "kast:'<{0}>', expectedPoints:<{1}>, TallKategori: <{2}>", [kast, expectedPoints, TallKategori]);
        }

        [TestMethod]
        [DataRow("1,1,1,1,1", 2, DisplayName = "Five ones = pair of ones -> 2 points")]
        [DataRow("2,2,1,1,1", 4, DisplayName = "2,2,1,1,1 = pair of twos -> 4 points")]
        [DataRow("2,2,2,1,1", 4, DisplayName = "2,2,2,1,1 = pair of twos -> 4 points")]
        [DataRow("1,2,3,4,5", 0, DisplayName = "No pairs -> 0 points")]
        [DataRow("2,2,6,6,1", 12, DisplayName = "2,2,6,6,1 = pair of sixes -> 12 points")]
        public void Test_PointCalculation_EttPar(string kast, int expectedPoints)
        {
            var yatzyPoengberegner = new YatzyPoengberegner();
            var actual = yatzyPoengberegner.BeregnPoeng(kast, Kategori.EttPar);
            // Assert
            Assert.AreEqual(expectedPoints, actual, "kast:'<{0}>', expectedPoints:<{1}>", [kast, expectedPoints]);
        }

        [TestMethod]
        [DataRow("1,1,1,1,1", 0, DisplayName = "Five ones = only one pair of ones -> 0 points")]
        [DataRow("2,2,1,1,1", 6, DisplayName = "2,2,1,1,1 = two pairs -> 6 points")]
        [DataRow("2,2,2,1,1", 6, DisplayName = "2,2,2,1,1 = two pairs -> 6 points")]
        [DataRow("1,2,3,4,5", 0, DisplayName = "No pairs -> 0 points")]
        [DataRow("2,2,6,6,1", 16, DisplayName = "2,2,6,6,1 = two pairs -> 16 points")]
        [DataRow("2,1,6,6,6", 0, DisplayName = "2,1,6,6,6 = one pair -> 0 points")]
        public void Test_PointCalculation_ToPar(string kast, int expectedPoints)
        {
            var yatzyPoengberegner = new YatzyPoengberegner();
            var actual = yatzyPoengberegner.BeregnPoeng(kast, Kategori.ToPar);
            // Assert
            Assert.AreEqual(expectedPoints, actual, "kast:'<{0}>', expectedPoints:<{1}>", [kast, expectedPoints]);
        }


        [TestMethod]
        [DataRow("1,1,1,1,1", 3, DisplayName = "Three of a kind with ones -> 3 points")]
        [DataRow("2,2,2,1,1", 6, DisplayName = "Three of a kind with twos -> 6 points")]
        [DataRow("3,3,3,4,5", 9, DisplayName = "Three of a kind with threes -> 9 points")]
        [DataRow("4,4,4,4,5", 12, DisplayName = "Three of a kind with fours -> 12 points")]
        [DataRow("5,5,5,5,5", 15, DisplayName = "Three of a kind with fives -> 15 points")]
        [DataRow("6,6,6,6,6", 18, DisplayName = "Three of a kind with sixes -> 18 points")]
        [DataRow("1,2,3,4,5", 0, DisplayName = "No three of a kind -> 0 points")]
        public void Test_PointCalculation_TreLike(string kast, int expectedPoints)
        {
            var yatzyPoengberegner = new YatzyPoengberegner();
            var actual = yatzyPoengberegner.BeregnPoeng(kast, Kategori.TreLike);
            // Assert
            Assert.AreEqual(expectedPoints, actual, "kast:'<{0}>', expectedPoints:<{1}>", [kast, expectedPoints]);
        }

        [TestMethod]
        [DataRow("1,1,1,1,1", 4, DisplayName = "Four of a kind with ones -> 4 points")]
        [DataRow("2,2,2,2,1", 8, DisplayName = "Four of a kind with twos -> 8 points")]
        [DataRow("3,3,3,3,5", 12, DisplayName = "Four of a kind with threes -> 12 points")]
        [DataRow("4,4,4,4,4", 16, DisplayName = "Four of a kind with fours -> 16 points")]
        [DataRow("5,5,5,5,5", 20, DisplayName = "Four of a kind with fives -> 20 points")]
        [DataRow("6,6,6,6,6", 24, DisplayName = "Four of a kind with sixes -> 24 points")]
        [DataRow("1,2,3,4,5", 0, DisplayName = "No four of a kind -> 0 points")]
        public void Test_PointCalculation_FireLike(string kast, int expectedPoints)
        {
            var yatzyPoengberegner = new YatzyPoengberegner();
            var actual = yatzyPoengberegner.BeregnPoeng(kast, Kategori.FireLike);
            // Assert
            Assert.AreEqual(expectedPoints, actual, "kast:'<{0}>', expectedPoints:<{1}>", [kast, expectedPoints]);
        }

        [TestMethod]
        [DataRow("1,1,1,2,2", 7, DisplayName = "Full house with ones and twos -> 7 points")]
        [DataRow("3,3,3,2,2", 13, DisplayName = "Full house with threes and twos -> 13 points")]
        [DataRow("4,4,4,5,5", 22, DisplayName = "Full house with fours and fives -> 22 points")]
        [DataRow("6,6,6,1,1", 20, DisplayName = "Full house with sixes and ones -> 20 points")]
        [DataRow("1,1,1,1,1", 0, DisplayName = "Not a full house -> 0 points")]
        public void Test_PointCalculation_FulltHus(string kast, int expectedPoints)
        {
            var yatzyPoengberegner = new YatzyPoengberegner();
            var actual = yatzyPoengberegner.BeregnPoeng(kast, Kategori.FulltHus);
            // Assert
            Assert.AreEqual(expectedPoints, actual, "kast:'<{0}>' expectedPoints:<{1}>", [kast, expectedPoints]);
        }

        [TestMethod]
        [DataRow("1,2,3,4,5", 15, DisplayName = "Liten straight -> 15 points")]
        [DataRow("2,3,4,5,6", 0, DisplayName = "Not a liten straight -> 0 points")]

        public void Test_PointCalculation_LitenStraight(string kast, int expectedPoints)
        {
            var yatzyPoengberegner = new YatzyPoengberegner();
            var actual = yatzyPoengberegner.BeregnPoeng(kast, Kategori.LitenStraight);
            // Assert
            Assert.AreEqual(expectedPoints, actual, "kast:'<{0}>', expectedPoints:<{1}>", [kast]);
        }

        [TestMethod]
        [DataRow("2,3,4,5,6", 20, DisplayName = "Stor straight -> 20 points")]
        [DataRow("1,2,3,4,5", 0, DisplayName = "Not a stor straight -> 0 points")]

        public void Test_PointCalculation_StorStraight(string kast, int expectedPoints)
        {
            var yatzyPoengberegner = new YatzyPoengberegner();
            var actual = yatzyPoengberegner.BeregnPoeng(kast, Kategori.StorStraight);
            // Assert
            Assert.AreEqual(expectedPoints, actual, "kast:'<{0}>', expectedPoints:<{1}>", [kast, expectedPoints]);
        }

        [TestMethod]
        [DataRow("1,2,3,4,5", 15, DisplayName = "Sum of 1,2,3,4,5 is 15")]
        [DataRow("6,6,6,6,6", 30, DisplayName = "Sum of 6,6,6,6,6 is 30")]
        [DataRow("1,1,1,1,1", 5, DisplayName = "Sum of 1,1,1,1,1 is 5")]
        [DataRow("2,3,4,5,6", 20, DisplayName = "Sum of 2,3,4,5,6 is 20")]
        [DataRow("5,5,5,5,5", 25, DisplayName = "Sum of 5,5,5,5,5 is 25")]
        public void Test_PointCalculation_Sjanse(string kast, int expectedPoints)
        {
            var yatzyPoengberegner = new YatzyPoengberegner();
            var actual = yatzyPoengberegner.BeregnPoeng(kast, Kategori.Sjanse);
            // Assert
            Assert.AreEqual(expectedPoints, actual, "kast:'<{0}>' expectedPoints:<{1}>", [kast, expectedPoints]);
        }

        [TestMethod]
        [DataRow("1,1,1,1,1", 50, Kategori.Yatzy, DisplayName = "1,1,1,1,1 -> Best possible result is Yatzy for 50 points")]
        [DataRow("2,2,2,2,2", 50, Kategori.Yatzy, DisplayName = "2,2,2,2,2 -> Best possible result is Yatzy for 50 points")]
        [DataRow("1,2,3,4,5", 15, Kategori.LitenStraight, DisplayName = "1,2,3,4,5 -> Best possible result is Liten Straight for 15 points")]
        [DataRow("1,2,3,4,6", 16, Kategori.Sjanse, DisplayName = "1,2,3,4,6 -> Best possible result is Sjanse for 16 points")]
        public void Test_MaxCalculation(string kast, int expectedPoints, Kategori expectedKategori)
        {
            var yatzyPoengberegner = new YatzyPoengberegner();
            var actual = yatzyPoengberegner.Max(kast);
            
            Assert.AreEqual(expectedPoints, actual.Poeng, "kast:'<{0}>' expectedPoints:<{1}>", [kast, expectedPoints]);
            Assert.AreEqual(expectedKategori, actual.Kategori, "kast:'<{0}>' expectedKategori:<{1}>", [kast, expectedKategori]);
        }





    }
}
