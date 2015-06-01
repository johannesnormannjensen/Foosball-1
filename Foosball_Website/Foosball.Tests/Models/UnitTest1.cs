
using Foosball.Models.FoosballModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Foosball.Tests.Models
{
    [TestClass]
    public class PlayerTest
    {

        [TestMethod]
        public void TestWinLowChance()
        {
            int opponentElo = 1600;
            Player player = new Player { Elo = 1400 };

            Assert.AreEqual(24, player.ChanceToWin(opponentElo) * 100, 1);
        }
        [TestMethod]
        public void TestWinHighChance()
        {
            int opponentElo = 1400;
            Player player = new Player { Elo = 1600 };

            Assert.AreEqual(76, player.ChanceToWin(opponentElo) * 100, 1);
        }
        [TestMethod]
        public void TestWinEqualChance()
        {
            int opponentElo = 1500;
            Player player = new Player { Elo = 1500 };

            Assert.AreEqual(50, player.ChanceToWin(opponentElo) * 100, 1);
        }


        [TestMethod]
        public void TestLittleEloWin()
        {
            int opponentElo = 1400;
            Player player = new Player { Elo = 1600 };
            player.CalculateEloWin(opponentElo);

            Assert.AreEqual(1608, player.Elo);
        }
        [TestMethod]
        public void TestMuchEloWin()
        {
            int opponentElo = 1600;
            Player player = new Player { Elo = 1400 };
            player.CalculateEloWin(opponentElo);

            Assert.AreEqual(1424, player.Elo);
        }
        [TestMethod]
        public void TestEqualEloWin()
        {
            int opponentElo = 1400;
            Player player = new Player { Elo = 1400 };
            player.CalculateEloWin(opponentElo);

            Assert.AreEqual(1416, player.Elo);
        }

        [TestMethod]
        public void TestLittleEloLose()
        {
            int opponentElo = 1600;
            Player player = new Player { Elo = 1400 };
            player.CalculateEloLose(opponentElo);

            Assert.AreEqual(1392, player.Elo);
        }
        [TestMethod]
        public void TestMuchEloLose()
        {
            int opponentElo = 1400;
            Player player = new Player { Elo = 1600 };
            player.CalculateEloLose(opponentElo);

            Assert.AreEqual(1576, player.Elo);
        }
        [TestMethod]
        public void TestEqualEloLose()
        {
            int opponentElo = 1500;
            Player player = new Player { Elo = 1500 };
            player.CalculateEloLose(opponentElo);

            Assert.AreEqual(1484, player.Elo);
        }



    }
}

