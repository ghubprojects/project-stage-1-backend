namespace MISA.SME.Domain.UnitTests
{
    [TestFixture]
    public class CalculatorTests
    {
        #region Fields
        /// <summary>
        /// Đối tượng test
        /// </summary>
        private readonly Calculator _calculator;
        #endregion

        #region Constructor

        public CalculatorTests()
        {
            _calculator = new Calculator();
        }

        #endregion

        #region Unit Test

        /// <summary>
        /// Hàm unit test cộng 2 số nguyên
        /// </summary>
        /// <param name="x">Toán hạng 1</param>
        /// <param name="y">Toán hạng 2</param>
        /// <param name="expectedResult">Kết quả mong đợi</param>
        /// Created by: ttanh (13/09/2023)
        [TestCase(1, 2, 3)]
        [TestCase(2, 3, 5)]
        [TestCase(2, -3, -1)]
        public void Add_ValidInput_Sum2Digits(int x, int y, long expectedResult)
        {
            var actualResult = _calculator.Add(x, y);

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        /// <summary>
        /// Hàm unit test tính tổng các số không âm trong chuỗi chứa ký tự khác số
        /// </summary>
        /// <param name="input">Chuỗi số cách nhau bởi dấu ','</param>
        /// <param name="expectedResult">Thông báo lỗi mong đợi</param>
        /// Created by: ttanh (13/09/2023)
        [TestCase("a1c", "Chuỗi không hợp lệ")]
        [TestCase("1,, 2, @", "Chuỗi không hợp lệ")]
        [TestCase("0, 4,5, -a", "Chuỗi không hợp lệ")]
        public void Add_InvalidString_ThrowException(string input, string expectedResult)
        {
            try
            {
                var actualResult = _calculator.Add(input);
            }
            catch (Exception ex)
            {
                Assert.That(ex.Message, Is.EqualTo(expectedResult));
            }
        }

        /// <summary>
        /// Hàm unit test tính tổng các số không âm trong chuỗi chứa số hạng âm
        /// </summary>
        /// <param name="input">Chuỗi số cách nhau bởi dấu ','</param>
        /// <param name="expectedResult">Thông báo lỗi mong đợi</param>
        /// Created by: ttanh (13/09/2023)
        [TestCase("1, -2, -3", "Không chấp nhận toán hạng âm: -2, -3")]
        [TestCase("3,-4, -5,-9,10", "Không chấp nhận toán hạng âm: -4, -5, -9")]
        [TestCase("1, -3", "Không chấp nhận toán hạng âm: -3")]
        public void Add_HasNegativeNumbers_ThrowException(string input, string expectedResult)
        {
            try
            {
                var actualResult = _calculator.Add(input);
            }
            catch (Exception ex)
            {
                Assert.That(ex.Message, Is.EqualTo(expectedResult));
            }
        }

        /// <summary>
        /// Hàm unit test tính tổng các số không âm trong chuỗi hợp lệ
        /// </summary>
        /// <param name="str">Chuỗi số cách nhau bởi dấu ','</param>
        /// <param name="expectedResult">Kết quả mong đợi</param>
        /// Created by: ttanh (13/09/2023)
        [TestCase("", 0)]
        [TestCase("1", 1)]
        [TestCase("1,2,3", 6)]
        [TestCase("1, 2, 3", 6)]
        [TestCase("3 , 5, 7, 9 , 11", 35)]
        [TestCase($"2147483647, 2147483647", 2L * int.MaxValue)]
        public void Add_ValidString_SumDigits(string str, long expectedResult)
        {
            var actualResult = _calculator.Add(str);

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        /// <summary>
        /// Hàm unit test trừ 2 số nguyên
        /// </summary>
        /// <param name="x">Toán hạng 1</param>
        /// <param name="y">Toán hạng 2</param>
        /// <param name="expectedResult">Kết quả mong đợi</param>
        /// Created by: ttanh (13/09/2023)
        [TestCase(1, 2, -1)]
        [TestCase(2, 3, -1)]
        [TestCase(int.MaxValue, int.MinValue, (long)2 * int.MaxValue + 1)]
        public void Sub_ValidInput_Sub2Digits(int x, int y, long expectedResult)
        {
            var actualResult = _calculator.Sub(x, y);

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        /// <summary>
        /// Hàm unit test nhân 2 số nguyên
        /// </summary>
        /// <param name="x">Toán hạng 1</param>
        /// <param name="y">Toán hạng 2</param>
        /// <param name="expectedResult">Kết quả mong đợi</param>
        /// Created by: ttanh (13/09/2023)
        [TestCase(1, 2, 2)]
        [TestCase(2, 3, 6)]
        [TestCase(int.MinValue, int.MaxValue, (long)int.MinValue * int.MaxValue)]
        public void Mul_ValidInput_Mul2Digits(int x, int y, long expectedResult)
        {
            var actualResult = _calculator.Mul(x, y);

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }


        /// <summary>
        /// Hàm unit test chia cho 0
        /// </summary>
        /// Created by: ttanh (13/09/2023)
        [Test]
        public void Div_DivideByZero_ThrowException()
        {
            // Arrange
            var x = 2;
            var y = 0;
            var exceptionMessage = "Không thể chia cho 0!";

            // Act
            try
            {
                var actualResult = _calculator.Div(x, y);
            }
            catch (Exception ex)
            {
                //Assert
                Assert.That(ex.Message, Is.EqualTo(exceptionMessage));
            }
        }

        /// <summary>
        /// Hàm unit test chia 2 số nguyên
        /// </summary>
        /// <param name="x">Toán hạng 1</param>
        /// <param name="y">Toán hạng 2</param>
        /// <param name="expectedResult">Kết quả mong đợi</param>
        /// Created by: ttanh (13/09/2023)
        [TestCase(1, 2, 0.5)]
        [TestCase(2, 3, (double)2 / 3)]
        [TestCase(2, 3, 0.66666666)]
        public void Div_ValidInput_Div2Digits(int x, int y, double expectedResult)
        {
            var actualResult = _calculator.Div(x, y);

            Assert.That(Math.Abs(expectedResult - actualResult), Is.LessThan(10e-6));
        }

        #endregion

    }
}
