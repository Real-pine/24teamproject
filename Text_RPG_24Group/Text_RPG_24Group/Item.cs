using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Text_RPG_24Group;


public class Poition
{
    public string Name { get;}
    public int Value { get; }
    public string Desc { get;}
    public int Count { get; set; }

    public Poition(string name, int value, string desc,int count)
    {
        Name = name;
        Value = value;
        Desc = desc;
        Count = count;
    }
}
public class Item
{
    public string Name { get; }
    public int Type { get; }
    public int Value { get; }
    public string Desc { get; }
    public int Price { get; }

    public string DisplayTypeText
    {
        get
        {
            return Type == 0 ? "공격력" : "방어력";
        }
    }

    public Item(string name, int type, int value, string desc, int price)
    {
        Name = name;
        Type = type;
        Value = value;
        Desc = desc;
        Price = price;
    }

    

    public string ItemInfoText()
    {
        return $"{Name}  |  {DisplayTypeText} +{Value}  |  {Desc}";
    }
}