namespace Task1 {
    public class MemoryEater {
        private readonly List<int[]> memAlloc = new List<int[]>();

        public void Allocate() {
            while (true) {
                memAlloc.Add(new int[1000]);
                Thread.Sleep(10);
            }
        }
    }

    class Program {
        static void Main(string[] args) {
            var me = new MemoryEater();
            me.Allocate();
        }
    }
}
