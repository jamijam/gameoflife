using NUnit.Framework;

namespace ConsoleApplication {

    [TestFixture]
    public class AccountTest
    {
        [Test]
        public void ShouldInitialiseGridWithCorrectSize()
        {
            var filePath = "./Seed";
            var grid = new Grid(filePath);

            Assert.AreEqual(39, grid.Size);
        }

        [Test]
        public void ShouldInitialiseGridWithCorrectEntries()
        {
            var filePath = "./Seed";
            var grid = new Grid(filePath);

            Assert.IsFalse(grid.IsCellAliveAt(0, 0));
            Assert.IsTrue(grid.IsCellAliveAt(5, 1));
        }

        [Test]
        public void ShouldKeepCellAliveWhen2LiveNeighbours(){
            var filePath = "./Seed";
            var grid = new Grid(filePath);

            Assert.AreEqual(1, grid.DetermineFate(3, 13));
        }

        [Test]
        public void ShouldKeepCellAliveWhen3LiveNeighbours(){
            var filePath = "./Seed";
            var grid = new Grid(filePath);

            Assert.AreEqual(1, grid.DetermineFate(5, 1));
        }

        [Test]
        public void ShouldKillWhen4LiveNeighbours(){
            var filePath = "./Seed";
            var grid = new Grid(filePath);

            Assert.AreEqual(0, grid.DetermineFate(5, 24));
        }

        [Test]
        public void ShouldComeAliveWhen3LiveNeighbours(){
            var filePath = "./Seed";
            var grid = new Grid(filePath);

            Assert.AreEqual(1, grid.DetermineFate(6, 12));
        }

        [Test]
        public void ShouldCoundNumberOfLiveNeighbours(){
            var filePath = "./Seed";
            var grid = new Grid(filePath);

            Assert.AreEqual(3, grid.NumberOfLiveNeighbours(5, 2));
        }        
    }
}
