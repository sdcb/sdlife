using sdlife.web.Services.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace sdlife.web.unittest.Services.PinYinTest
{
    public class PinYinAll
    {
        private readonly PinYinConverter pinYin;

        public PinYinAll()
        {
            pinYin = new PinYinConverter();
        }

        [Theory]
        [InlineData('我', "Wo")]
        [InlineData('A', "A")]
        public void GetCharPinYin(char ch, string expectedPinYin)
        {
            var actualPinYin = pinYin.GetCharPinYin(ch);
            Assert.Equal(expectedPinYin, actualPinYin);
        }

        [Theory]
        [InlineData("我是中国人", "WoShiZhongGuoRen")]
        [InlineData(null, null)]
        [InlineData("\0A.领导", "\0A.LingDao")]
        public void GetStringPinYin(string word, string expectedPinYin)
        {
            var actualPinYin = pinYin.GetStringPinYin(word);
            Assert.Equal(expectedPinYin, actualPinYin);
        }

        [Theory]
        [InlineData("我是中国人", "WSZGR")]
        [InlineData(null, null)]
        [InlineData("\0A.领导", "\0A.LD")]
        public void GetStringCapitalPinYin(string word, string expectedPinYin)
        {
            var actualPinYin = pinYin.GetStringCapitalPinYin(word);
            Assert.Equal(expectedPinYin, actualPinYin);
        }
    }
}
