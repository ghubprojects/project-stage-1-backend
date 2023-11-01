namespace MISA.SME.Domain
{
    public class Calculator
    {
        #region Method

        /// <summary>
        /// Hàm cộng 2 số nguyên
        /// </summary>
        /// <param name="x">Toán hạng 1</param>
        /// <param name="y">Toán hạng 2</param>
        /// <returns>Tổng 2 số nguyên</returns>
        /// Created by: ttanh (13/09/2023)
        public long Add(int x, int y) => x + (long)y;

        /// <summary>
        /// Hàm tính tổng các số không âm trong chuỗi
        /// </summary>
        /// <param name="input">Chuỗi số cách nhau bởi dấu ','</param>
        /// <returns>Tổng các số không âm</returns>
        /// <exception cref="Exceptions">
        /// Nếu chuỗi ko đúng định dạng: Chuỗi không hợp lệ
        /// Nếu chuỗi chứa số hạng âm: Không chấp nhận toán hạng âm: num1, num2, ...
        /// </exception>
        /// Created by: ttanh (13/09/2023)
        public long Add(string input)
        {
            // nếu input là null hoặc chuỗi rỗng, trả về tổng bằng 0
            if (string.IsNullOrWhiteSpace(input))
                return 0;

            // mảng lưu các chuỗi toán hạng
            var numbers = input.Split(',');

            // biến lưu kết quả
            long result = 0;

            // mảng lưu các số âm
            var negatives = new List<long>();

            foreach (string number in numbers)
            {
                // chuyển từ chuỗi sang số nguyên
                if (long.TryParse(number.Trim(), out long value))
                {
                    if (value < 0)
                        negatives.Add(value);
                    else
                        result += value;
                }
                else
                    throw new ArgumentException("Chuỗi không hợp lệ");
            }

            // nếu có toán hạng âm, đưa ra exception
            if (negatives.Count > 0)
                throw new ArgumentException($"Không chấp nhận toán hạng âm: {string.Join(", ", negatives)}");

            return result;
        }

        /// <summary>
        /// Hàm trừ 2 số nguyên
        /// </summary>
        /// <param name="x">Toán hạng 1</param>
        /// <param name="y">Toán hạng 2</param>
        /// <returns>Hiệu 2 số nguyên</returns>
        /// Created by: ttanh (13/09/2023)
        public long Sub(int x, int y) => x - (long)y;

        /// <summary>
        /// Hàm nhân 2 số nguyên
        /// </summary>
        /// <param name="x">Toán hạng 1</param>
        /// <param name="y">Toán hạng 1</param>
        /// <returns>Tích 2 số nguyên</returns>
        /// Created by: ttanh (13/09/2023)
        public long Mul(int x, int y) => x * (long)y;

        /// <summary>
        /// Hàm chia 2 số nguyên
        /// </summary>
        /// <param name="x">Toán hạng 1</param>
        /// <param name="y">Toán hạng 2</param>
        /// <returns>Thương 2 số nguyên</returns>
        /// <exception cref="DivideByZeroException"></exception>
        /// Created by: ttanh (13/09/2023)
        public double Div(int x, int y)
        {
            if (y == 0)
                throw new DivideByZeroException("Không thể chia cho 0!");
            return x / (double)y;
        }

        #endregion
    }
}
