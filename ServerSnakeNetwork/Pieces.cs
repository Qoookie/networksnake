namespace DuchosalN
{
    public struct Pieces
    {
        public string ID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Ecran { get; set; }

        public Pieces(int x, int y, int ecran, string id)
        {
            X = x;
            Y = y;
            Ecran = ecran;
            ID = id;
        }

    }
}
