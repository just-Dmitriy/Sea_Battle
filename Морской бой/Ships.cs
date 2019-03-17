using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Морской_бой
{
    class Ships
    {
        public int _one_desk = 4;
        public int _two_desk = 3;
        public int _three_desk = 2;
        public int _four_desk = 1;
        public int _ALL_desk = 20;

        /// <summary>
        /// Метод сброса значений количества кораблей по умолчанию
        /// </summary>
        public void Default ()
        {
        _one_desk = 4;
        _two_desk = 3;
        _three_desk = 2;
        _four_desk = 1;
        _ALL_desk = 20;
        }
    }
}
