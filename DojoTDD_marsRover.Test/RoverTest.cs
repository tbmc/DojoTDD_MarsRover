using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DojoTDD_marsRover.Test
{
    [TestClass]
    public class RoverTest
    {
        private Rover rover; 

        [TestInitialize]
        public void BeforeEach()
        {
            rover = new Rover();
        }

        [TestMethod]
        public void ShouldReturnCoordinatesWhenReceivedEmptyMessage()
        {
            var response = rover.sendCommand("");
            Assert.AreEqual("0:0:N", response);
        }

        [TestMethod]
        public void ShouldReturnCoordinatesWhenReceivedF()
        {
            var response = rover.sendCommand("F");
            Assert.AreEqual("0:1:N", response);
        }

        [TestMethod]
        public void ShouldReturnCoordinatesWhenReceivedR()
        {
            var response = rover.sendCommand("R");
            Assert.AreEqual("0:0:E", response);
        }

        [TestMethod]
        public void ShouldReturnCoordinatesWhenReceivedL()
        {
            var response = rover.sendCommand("L");
            Assert.AreEqual("0:0:O", response);
        }

        [TestMethod]
        public void ShouldReturnCoordinatesWhenReceivedRF()
        {
            var response = rover.sendCommand("RF");
            Assert.AreEqual("1:0:E", response);
        }

        [TestMethod]
        public void ShouldReturnCoordinatesWhenReceivedFRF()
        {
            var response = rover.sendCommand("FRF");
            Assert.AreEqual("1:1:E", response);
        }

        [TestMethod]
        public void ShouldReturnCoordinatesWhenReceivedFRRFRRF()
        {
            var response = rover.sendCommand("FRRFRRF");
            Assert.AreEqual("0:1:N", response);
        }

        [TestMethod]
        public void ShouldGoBackFirstWhenGoingOutsideGridNorth()
        {
            var response = rover.sendCommand("FFFFFFFFFF");
            Assert.AreEqual("0:0:N", response);
        }

        [TestMethod]
        public void ShouldGoBackLastWhenGoingOutsideGridSouth()
        {
            var response = rover.sendCommand("LLF");
            Assert.AreEqual("0:9:S", response);
        }

        [TestMethod]
        public void ShouldGoBackFirstWhenGoingOutsideGridEast()
        {
            var response = rover.sendCommand("RFFFFFFFFFF");
            Assert.AreEqual("0:0:E", response);
        }

        [TestMethod]
        public void ShouldGoBackLastWhenGoingOutsideGridWest()
        {
            var response = rover.sendCommand("LF");
            Assert.AreEqual("9:0:O", response);
        }

        [TestMethod]
        public void ShouldStopWhenEncounterAnObstacle()
        {
            rover.obstacles.Add((1,1));
            var response = rover.sendCommand("FRF");
            Assert.AreEqual("O:0:1:E", response);
        }
    }
}
