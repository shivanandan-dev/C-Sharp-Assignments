namespace MathApp {
    public class MathOperations {
        public int Add(int x, int y) => x + y;
        public int Subtract(int x, int y) => x - y;
        public int Multiply(int x, int y) => x * y;
        public double Divide(double x, double y) {
            if (y == 0) throw new DivideByZeroException();
            return x / y;
        }
    }
}