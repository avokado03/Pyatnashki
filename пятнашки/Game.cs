using System;

namespace пятнашки
{ 
    /// <summary>
    /// Класс с методами для управления игрой
    /// </summary>
    public class Game
    {
        private int size, space_x, space_y;
        private int[,] map;
        private static Random rnd = new Random();

        public Game(int size)
        {
            this.size = SetSize(size);
            map = new int[size, size];
        }

        /// <summary>
        /// Установка размера поля
        /// </summary>
        private int SetSize(int size)
        {
            if (size < 2) return 2;
            if (size > 5) return 5;
            return size;
        }

        /// <summary>
        /// Инициализация новой игры
        /// </summary>
        public void Start()
        {
            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                    map[x, y] = CoordsToPosition(x, y) + 1;
            space_x = size - 1;
            space_y = size - 1;
            map[space_x, space_y] = 0;
        }

        /// <summary>
        /// Сдвинуть плашки
        /// </summary>
        public void Shift(int position)
        {
            int x, y;
            РositionТoСoords(position, out x, out y);
            if (Math.Abs(space_x - x) + Math.Abs(space_y - y) != 1)
                return;
            map[space_x, space_y] = map[x, y];
            map[x, y] = 0;
            space_x = x;
            space_y = y;
        }

        /// <summary>
        /// Перемешивание плашек
        /// </summary>
        public void ShiftRandom()
        {
            //1 вариант(много ходов лишних) shift(rnd.Next(0, size * size)); 
            int a = rnd.Next(0, 4);
            int x = space_x;
            int y = space_y;
            switch (a)
            {
                case 0: x--; break;
                case 1: x++; break;
                case 2: y--; break;
                case 3: y++; break;
            }
            Shift(CoordsToPosition(x, y));
        }

        /// <summary>
        /// Проверка порядка расположения плашек
        /// </summary>
        public bool CheckNumber()
        {
            if (!(space_x == size - 1 && space_y == size - 1))
                return false;
            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                    if (!(x == size - 1 && y == size - 1))                               
                    if (map[x, y] != CoordsToPosition(x, y) + 1)
                        return false;
            return true;
        }

        /// <summary>
        /// Получение номера плашки
        /// </summary>
        public int GetNumber(int position)
        {
            int x, y;
            РositionТoСoords(position, out x, out y);
            if (x < 0 || x >= size) return 0;
            if (y < 0 || y >= size) return 0;
            return map[x, y];
        }

        /// <summary>
        /// Получение позиции по координатам
        /// </summary>
        private int CoordsToPosition(int x, int y)
        {
            if (x < 0) x = 0;
            if (x > size - 1) x = size - 1;
            if (y < 0) x = 0;
            if (y > size - 1) y = size - 1;
            return y * size + x;
        }

        /// <summary>
        /// Получение координат по позиции
        /// </summary>
        private void РositionТoСoords(int position, out int x, out int y)
        {
            if (position < 0) position = 0;
            if (position > size * size - 1) position = size * size - 1;
            x = position % size;
            y = position / size;
        }
    }
}
