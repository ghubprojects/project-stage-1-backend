using MISA.SME.Domain;

namespace MISA.SME.Application
{
    /// <summary>
    /// Lớp hỗ trợ chuyển đổi giới tính thành resource tương ứng
    /// </summary>
    public static class GenderHelper
    {
        /// <summary>
        /// Chuyển đổi giới tính thành resource tương ứng
        /// </summary>
        /// <param name="gender">Giới tính cần chuyển đổi</param>
        /// <returns>Giá trị tương ứng trong resource</returns>
        /// <remarks>Created by: ttanh (02/10/2023)</remarks>
        public static string ConvertToResource(Gender gender)
        {
            switch (gender)
            {
                case Gender.Male:
                    return "Nam";
                case Gender.Female:
                    return "Nữ";
                case Gender.Other:
                    return "Khác";
                default:
                    throw new ArgumentOutOfRangeException(nameof(gender), "Giá trị giới tính không hợp lệ");
            }
        }
    }
}
