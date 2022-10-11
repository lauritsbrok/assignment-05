using System;
using System.Collections.Generic;

namespace GildedRose
{
    public class Program {
        public IList<NormalItem> Items;
        public static void Main(string[] args)
        {
            System.Console.WriteLine("OMGHAI!");

            var app = new Program() {
                Items = new List<NormalItem> {
                    new NormalItem { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 },
                    new Cheese { Name = "Aged Brie", SellIn = 2, Quality = 0 },
                    new NormalItem { Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7 },
                    new Legendary { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 },
                    new Legendary { Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80 },
                    new BackstagePass { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20 },
                    new BackstagePass {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 49 },
                    new BackstagePass {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 49 },
                    // this conjured item does not work properly yet
                    new NormalItem { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 }
                }
            };

            for (var i = 0; i < 31; i++) {
                Console.WriteLine("-------- day " + i + " --------");
                Console.WriteLine("name, sellIn, quality");
                foreach (var item in app.Items) {
                    Console.WriteLine(item.Name + ", " + item.SellIn + ", " + item.Quality);
                }
                Console.WriteLine("");
                app.UpdateQuality();
            }
        }

        public void UpdateQuality() {
            foreach (var item in Items) {
                item.UpdateQuality();
            }
        }
    }
}

public class Item
{
    public string Name { get; set; }

    public int SellIn { get; set; }

    public int Quality { get; set; }
}
public class NormalItem : Item {
    protected int QualityDecrease = -1;
    public virtual void UpdateQuality(bool SellInAfter = false) { //For some reason, all items except backstage passes, have their 'sellIn' value decreased before calculating their next quality.
        if (SellInAfter == false) SellIn--;

        DetermineQualityDecrease();
        Quality += QualityDecrease;
        if (Quality < 0) Quality = 0;
        if (Quality > 50) Quality = 50;

        if (SellInAfter) SellIn--;
    }

    public virtual void DetermineQualityDecrease() {
        if (SellIn < 0) {
            QualityDecrease = -2;
        }
    }
}

public class Cheese : NormalItem {
    public override void DetermineQualityDecrease() {
        if (SellIn >= 0) {
            QualityDecrease = 1;
        }
        else {
            QualityDecrease = 2;
        }
    }
}
public class Legendary : NormalItem {
    public override void UpdateQuality(bool SellInAfter = false) {
    }
}
public class BackstagePass : NormalItem {
    public override void UpdateQuality(bool SellInAfter = false) {
        base.UpdateQuality(true);

        if (SellIn < 0 ) {
            Quality = 0;
        }
    }
    public override void DetermineQualityDecrease() {
        if (SellIn < 0) {
            QualityDecrease = 0;
        }
        else if (SellIn <= 5) {
            QualityDecrease = 3;
        }
        else if (SellIn <= 10) {
            QualityDecrease = 2;
        }
        else {
            QualityDecrease = 1;
        }
    }
}

public class Conjured : NormalItem {
    public override void DetermineQualityDecrease() {
        QualityDecrease = -2;
        if (SellIn < 0) {
            QualityDecrease = -4;
        }
    }
}