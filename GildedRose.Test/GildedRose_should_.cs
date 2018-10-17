using System.Collections.Generic;
using Xunit;

namespace GildedRose.Test
{
    public class GildedRose_should_
    {
        [Fact]
        public void lower_quality_at_the_end_of_each_day()
        {
            var item = new Item
            {
                Name = "Fact",
                Quality = 3,
                SellIn = 2
            };
            var gr = new GildedRose(new List<Item> { item });

            gr.UpdateQuality();

            Assert.Equal(2, item.Quality);
        }

        [Fact]
        public void lower_sellin_at_the_end_of_the_day()
        {
            var item = new Item
            {
                Name = "Fact",
                Quality = 3,
                SellIn = 3
            };

            var gr = new GildedRose(new List<Item> { item });

            gr.UpdateQuality();

            Assert.Equal(2, item.SellIn);
        }

        [Fact]
        public void lower_quality_twice_as_fast_after_sell_by()
        {
            var item = new Item
            {
                Name = "Fact",
                Quality = 3,
                SellIn = 0
            };

            var gr = new GildedRose(new List<Item> { item });

            gr.UpdateQuality();

            Assert.Equal(1, item.Quality);
        }

        [Fact]
        public void not_decrease_quality_below_zero()
        {
            var item = new Item
            {
                Name = "Fact",
                Quality = 0,
                SellIn = 0
            };

            var gr = new GildedRose(new List<Item> { item });

            gr.UpdateQuality();

            Assert.Equal(0, item.Quality);
        }

        [Fact]
        public void increase_aged_brie_quality_as_sellin_date_decreases()
        {
            var item = new Item
            {
                Name = "Aged Brie",
                Quality = 1,
                SellIn = 5
            };

            var gr = new GildedRose(new List<Item> { item });

            gr.UpdateQuality();

            Assert.Equal(2, item.Quality);
        }

        [Fact]
        public void never_increase_quality_above_50()
        {
            var item = new Item
            {
                Name = "Aged Brie",
                Quality = 50,
                SellIn = 5
            };

            var gr = new GildedRose(new List<Item> { item });

            gr.UpdateQuality();

            Assert.Equal(50, item.Quality);
        }

        //"Backstage passes" increases in Quality as it's SellIn value approaches
        [Fact]
        public void increment_backstage_passes_quality__by_1_when_sellin_greater_than_10()
        {
            var item = new Item
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                Quality = 5,
                SellIn = 11
            };

            var gr = new GildedRose(new List<Item> { item });

            gr.UpdateQuality();

            Assert.Equal(6, item.Quality);
        }

        //Quality increases by 2 when there are 10 days or less
        [Theory]
        [InlineData(10)]
        [InlineData(9)]
        public void increment_backstage_passes_quality_by_2_when_sellin_less_than_or_equal_to_10(int sellIn)
        {
            var item = new Item
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                Quality = 5,
                SellIn = sellIn
            };

            var gr = new GildedRose(new List<Item> { item });

            gr.UpdateQuality();

            Assert.Equal(7, item.Quality);
        }

        // by 3 when there are 5 days or less 
        [Theory]
        [InlineData(4)]
        [InlineData(5)]
        public void increment_backstage_passes_quality_by_3_when_sellin_less_than_or_equal_to_5(int sellIn)
        {
            var item = new Item
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                Quality = 5,
                SellIn = sellIn
            };

            var gr = new GildedRose(new List<Item> { item });

            gr.UpdateQuality();

            Assert.Equal(8, item.Quality);
        }

        [Fact]
        public void set_quality_to_0_after_SellIn_equals_zero()
        {
            var item = new Item
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                Quality = 5,
                SellIn = 0,
            };

            var gr = new GildedRose(new List<Item> { item });

            gr.UpdateQuality();

            Assert.Equal(0, item.Quality);
        }

        [Fact]
        public void not_alter_the_quality_of_legendary_items()
        {
            var item = new Item
            {
                Name = "Sulfuras, Hand of Ragnaros",
                Quality = 80,
                SellIn = 3,
            };

            var gr = new GildedRose(new List<Item> { item });

            gr.UpdateQuality();

            Assert.Equal(80, item.Quality);
        }

        [Fact]
        public void not_alter_the_sellin_of_legendary_items()
        {
            var item = new Item
            {
                Name = "Sulfuras, Hand of Ragnaros",
                Quality = 80,
                SellIn = 3,
            };

            var gr = new GildedRose(new List<Item> { item });

            gr.UpdateQuality();

            Assert.Equal(3, item.SellIn);
        }

        [Fact]
        public void lower_the_quality_twice_as_fast_for_conjured_items()
        {
            var item = new Item
            {
                Name = "Conjured - Potion",
                Quality = 10,
                SellIn = 3,
            };

            var gr = new GildedRose(new List<Item> { item });

            gr.UpdateQuality();

            Assert.Equal(8, item.Quality);
        }
    }
}
