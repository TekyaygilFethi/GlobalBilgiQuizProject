// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

int sayi = 7;



// int, float, double, bool
void Set(ref int sayi)
{
    sayi = 5;
}

Console.WriteLine(sayi);
Set(ref sayi);
Console.WriteLine(sayi);




void SetKedi(Kedi kedi)
{
    kedi.Name = "Pamuk";
}

var husniye = new Kedi()
{
    Name = "Hüsniye"
};

Console.WriteLine(husniye.Name);
SetKedi(husniye);
Console.WriteLine(husniye.Name);


class Kedi
{
    public string Name { get; set; }
}