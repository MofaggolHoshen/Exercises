using System.Diagnostics.CodeAnalysis;

namespace AttributesForNullState
{
    [TestClass]
    public class AllowNullAndDisallowNull
    {
        [TestMethod]
        public void AllowNullTeat_1()
        {
            var ab = new Ab().ScreenName = null;
        }
    }

    public class Ab
    {
        [AllowNull]
        public string ScreenName
        {
            get => _screenName;
            set => _screenName = value;
        }
        private string _screenName = GenerateRandomScreenName(null);


        private static string GenerateRandomScreenName([DisallowNull]string? name)
        {
            return "Mofaggol";
        }

        
    }
        
}