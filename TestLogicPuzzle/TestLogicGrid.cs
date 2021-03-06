﻿using System.Collections.Generic;

using LogicPuzzle;
using Xunit;

namespace TestLogicPuzzle
{
    internal enum Fish
    {
        Haddock,
        Plaice,
        Cod,
        Rock,
        Flake
    }

    internal enum Currency
    {
        GBP,
        EUR,
        USD,
        AUD,
        KRW
    }

    public class TestLogicGrid
    {
        private LogicGrid<Fish, Currency> _grid;

        [Fact]
        public void TestSetOneFalse()
        {
            _grid = new LogicGrid<Fish, Currency>();
            _grid[Fish.Haddock, Currency.EUR] = false;
            Assert.False(_grid[Fish.Haddock, Currency.EUR]);
        }

        [Fact]
        public void TestSetOneTrue()
        {
            _grid = new LogicGrid<Fish, Currency>();
            _grid[Fish.Haddock, Currency.EUR] = true;
            Assert.True(_grid[Fish.Haddock, Currency.EUR]);
            Assert.False(_grid[Fish.Haddock, Currency.KRW]);
            Assert.False(_grid[Fish.Cod, Currency.EUR]);
        }

        [Fact]
        public void TestSetFourFalse()
        {
            _grid = new LogicGrid<Fish, Currency>();
            foreach (Fish fish in new List<Fish>{Fish.Cod, Fish.Flake, Fish.Plaice, Fish.Rock}) {
                _grid[fish, Currency.EUR] = false;
            }
            Assert.True(_grid[Fish.Haddock, Currency.EUR]);
        }
    }
}
